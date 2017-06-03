using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using DataAccessLayer.Interfaces.Invoices;
using V6SqlConnect;
using V6Structs;

namespace DataAccessLayer.Implementations.Invoices
{
    public class Invoice84Services : IInvoice84Services
    {
        public bool InsertInvoice(int UserId, V6TableStruct AMStruct, V6TableStruct ADStruct, SortedDictionary<string, object> am, List<SortedDictionary<string, object>> adList, out string message)
        {
            var insert_am_sql = SqlGenerator.GenInsertAMSql(UserId, AMStruct, am);
            SqlTransaction TRANSACTION = SqlConnect.CreateSqlTransaction(AMStruct.TableName);

            //Delete AD
            SortedDictionary<string, object> keys = new SortedDictionary<string, object>()
            {
                {"STT_REC",am["STT_REC"]}
            };
            var deleteAdSql = SqlGenerator.GenDeleteSql(ADStruct, keys);
            SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, deleteAdSql);
            //Delete AM
            var deleteAMSql = SqlGenerator.GenDeleteSql(AMStruct, keys);
            SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, deleteAMSql);


            var insert_success = SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, insert_am_sql) > 0;
            var j = 0;
            foreach (SortedDictionary<string, object> adRow in adList)
            {
                var adSql = SqlGenerator.GenInsertAMSql(UserId, ADStruct, adRow);
                j += (SqlConnect.ExecuteNonQuery(TRANSACTION, CommandType.Text, adSql) > 0 ? 1 : 0);
            }
            if (insert_success && j == adList.Count)
            {
                TRANSACTION.Commit();
                try
                {
                    SqlParameter[] pList =
                    {
                        new SqlParameter("@Stt_rec", am["STT_REC"].ToString()),
                        new SqlParameter("@Ma_ct", am["MA_CT"].ToString()),
                        new SqlParameter("@Ma_nt", am["MA_NT"].ToString()),
                        new SqlParameter("@Mode", "M"),
                        new SqlParameter("@nKieu_Post", am["KIEU_POST"].ToString()),
                        new SqlParameter("@UserID", UserId),
                        new SqlParameter("@Save_voucher", "1")
                    };

                    //V6BusinessHelper.ExecuteProcedureNoneQuery("VPA_IXA_POST_MAIN", pList);
                    var result = SqlConnect.ExecuteNonQuery(CommandType.StoredProcedure, "VPA_IXA_POST_MAIN", pList);
                    message = string.Format("Success, ({0} affected).", result);
                    return true;

                    //TRANSACTION.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                    message = "POST lỗi: " + message;
                    //TRANSACTION.Rollback();
                    return false;
                }
            }
            else//
            {
                TRANSACTION.Rollback();

                
                TRANSACTION.Rollback();
                message = "Rollback: "
                    + (!insert_success ? "Thêm AM không thành công." : "")
                    + (j != adList.Count ? "Thêm AD không hoàn tất." : "");
                
            }
            return false;
        }
    }
}

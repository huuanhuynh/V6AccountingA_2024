using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DataAccessLayer.Interfaces.Business;
using V6SqlConnect;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;

namespace DataAccessLayer.Implementations.Business
{
    public class BusinessServices : IBusinessServices
    {
        public DateTime GetServerDateTime()
        {
            return SqlConnect.GetServerDateTime();
        }
        public DataTable SelectTable(string tableName)
        {
            return SqlConnect.SelectTable(tableName);
        }

        public V6SelectResult Select(string tableName, string fields, string where, string group, string sort)
        {
            var result = SqlConnect.Select(tableName, fields, where, group, sort);
            return result;
        }

        public V6SelectResult SelectPaging(string tableName, string fields, int page, int size, string where, string sort, bool ascending)
        {
            return SqlConnect.SelectPaging(tableName, fields, page, size, where, sort, ascending);
        }



        private void CheckIdentifier(string name)
        {
            if (name.Contains("'") || name.Contains(" ") || name.Contains("(")
                || name.Contains(".") || name.Contains(" "))
                throw new ArgumentException("Identifier expected.", "name");
        }
        public DataSet ExecuteProcedure(string procName, Dictionary<string, string> parameters)
        {
            CheckIdentifier(procName);
            List<SqlParameter> plist = new List<SqlParameter>();
            foreach (KeyValuePair<string, string> parameter in parameters)
            {
                plist.Add(new SqlParameter(parameter.Key, parameter.Value));
            }
            return SqlConnect.ExecuteDataset(CommandType.StoredProcedure, procName, plist.ToArray());
        }

        public object ExecuteProcedureScalar(string procName, Dictionary<string, string> parameters)
        {
            CheckIdentifier(procName);
            List<SqlParameter> plist = new List<SqlParameter>();
            foreach (KeyValuePair<string, string> parameter in parameters)
            {
                plist.Add(new SqlParameter(parameter.Key, parameter.Value));
            }
            var result = SqlConnect.ExecuteScalar(CommandType.StoredProcedure, procName, plist.ToArray());
            return result;
        }

        public int ExecuteProcedureNoneQuery(string procName, Dictionary<string, string> parameters)
        {
            CheckIdentifier(procName);
            List<SqlParameter> plist = new List<SqlParameter>();
            foreach (KeyValuePair<string, string> parameter in parameters)
            {
                plist.Add(new SqlParameter(parameter.Key, parameter.Value));
            }
            return SqlConnect.ExecuteNonQuery(CommandType.StoredProcedure, procName, plist.ToArray());
        }

        public int Update(string tableName, SortedDictionary<string, object> data, SortedDictionary<string, object> keys)
        {
            throw new NotImplementedException();
        }
        
        public Dictionary<string, string> GetHideColumns(string tableName)
        {
            throw new NotImplementedException();
        }

        public DataTable GetLoDate(string mavt, string makho, string sttRec, DateTime ngayct)
        {
            mavt = mavt.Replace("'", "''");
            makho = makho.Replace("'", "''");
            SqlParameter[] plist = new[]
            {
                new SqlParameter("@cKey1", string.Format("Ma_vt = '"+mavt+"' and Ma_kho = '"+makho+"'")),
                new SqlParameter("@cKey2", ""),
                new SqlParameter("@cKey3", ""),
                new SqlParameter("@cStt_rec", sttRec),
                new SqlParameter("@dBg", ngayct.Date)
            };
            return SqlConnect.ExecuteDataset(CommandType.StoredProcedure, "VPA_EdItems_DATE_STT_REC", plist).Tables[0];
        }
        
        public DataTable GetViTri(string mavt, string makho, string sttRec, DateTime ngayct)
        {
            mavt = mavt.Replace("'", "''");
            makho = makho.Replace("'", "''");

            string keys = "";
            if (makho == "")
            {
                if (mavt == "")
                {
                    keys = "1=1";
                }
                else
                {
                    keys = string.Format("Ma_vt = '" + mavt + "'");
                }

            }
            else
            {
                if (mavt == "")
                {
                    keys = string.Format(" Ma_kho = '" + makho + "'");
                }
                else
                {
                    keys = string.Format("Ma_vt = '" + mavt + "' and Ma_kho = '" + makho + "'");
                }
            }
            
            SqlParameter[] plist = new[]
            {
                new SqlParameter("@cKey1", keys),
                new SqlParameter("@cKey2", ""),
                new SqlParameter("@cKey3", ""),
                new SqlParameter("@cStt_rec", sttRec),
                new SqlParameter("@dBg", ngayct.Date)
            };
            return SqlConnect.ExecuteDataset(CommandType.StoredProcedure, "VPA_EdItems_VITRI_STT_REC", plist).Tables[0];
        }
        
        public DataTable GetViTriLoDate(string mavt, string makho, string sttRec, DateTime ngayct)
        {
            mavt = mavt.Replace("'", "''");
            makho = makho.Replace("'", "''");

            string keys = "";
            if (makho == "")
            {
                if (mavt == "")
                {
                    keys = "1=1";
                }
                else
                {
                    keys = string.Format("Ma_vt = '" + mavt + "'");
                }

            }
            else
            {
                if (mavt == "")
                {
                    keys = string.Format(" Ma_kho = '" + makho + "'");
                }
                else
                {
                    keys = string.Format("Ma_vt = '" + mavt + "' and Ma_kho = '" + makho + "'");
                }
            }

            SqlParameter[] plist = new[]
            {
                new SqlParameter("@cKey1", keys),
                new SqlParameter("@cKey2", ""),
                new SqlParameter("@cKey3", ""),
                new SqlParameter("@cStt_rec", sttRec),
                new SqlParameter("@dBg", ngayct.Date)
            };
            return SqlConnect.ExecuteDataset(CommandType.StoredProcedure, "VPA_EdItems_VITRI_DATE_STT_REC", plist).Tables[0];
        }

        public DataTable GetLoDate13(string mavt, string makho, string malo, string sttRec, DateTime ngayct)
        {
            mavt = mavt.Replace("'", "''");
            makho = makho.Replace("'", "''");
            malo = malo.Replace("'", "''");

            SqlParameter[] plist = new[]
            {
                new SqlParameter("@cKey1", string.Format("Ma_vt = '"+mavt+"' and Ma_kho = '"+makho+"' and Ma_lo = '"+malo+"'")),
                new SqlParameter("@cKey2", ""),
                new SqlParameter("@cKey3", ""),
                new SqlParameter("@cStt_rec", sttRec),
                new SqlParameter("@dBg", ngayct.Date)
            };
            return SqlConnect.ExecuteDataset(CommandType.StoredProcedure, "VPA_EdItems_DATE_STT_REC", plist).Tables[0];
        }
        public DataTable GetLoDateAll(string mavt_in, string makho_in, string malo_in, string sttRec, DateTime ngayct)
        {
            //mavt = mavt.Replace("'", "''");
            //makho = makho.Replace("'", "''");
            //malo = malo.Replace("'", "''");

            SqlParameter[] plist = new[]
            {
                new SqlParameter("@cKey1", string.Format("Ma_vt in ("+mavt_in+") and Ma_kho in ("+makho_in+") and Ma_lo in ("+malo_in+")")),
                new SqlParameter("@cKey2", ""),
                new SqlParameter("@cKey3", ""),
                new SqlParameter("@cStt_rec", sttRec),
                new SqlParameter("@dBg", ngayct.Date)
            };
            return SqlConnect.ExecuteDataset(CommandType.StoredProcedure, "VPA_EdItems_DATE_STT_REC", plist).Tables[0];
        }
        
        public DataTable GetViTri13(string mavt, string makho, string mavitri, string sttRec, DateTime ngayct)
        {
            mavt = mavt.Replace("'", "''");
            makho = makho.Replace("'", "''");
            mavitri = mavitri.Replace("'", "''");

            SqlParameter[] plist = new[]
            {
                new SqlParameter("@cKey1", string.Format("Ma_vt = '"+mavt+"' and Ma_kho = '"+makho+"' and Ma_vitri = '"+mavitri+"'")),
                new SqlParameter("@cKey2", ""),
                new SqlParameter("@cKey3", ""),
                new SqlParameter("@cStt_rec", sttRec),
                new SqlParameter("@dBg", ngayct.Date)
            };
            return SqlConnect.ExecuteDataset(CommandType.StoredProcedure, "VPA_EdItems_VITRI_STT_REC", plist).Tables[0];
        }
        
        public DataTable GetViTriLoDate13(string mavt, string makho, string malo, string mavitri, string sttRec, DateTime ngayct)
        {
            mavt = mavt.Replace("'", "''");
            makho = makho.Replace("'", "''");
            malo = malo.Replace("'", "''");
            mavitri = mavitri.Replace("'", "''");

            string keys = "";
            if (makho == "")
            {
                if (malo == "")
                {
                    if (mavitri == "")
                    {
                        keys =string.Format("Ma_vt = '" + mavt + "'");
                    }
                    else
                    {
                        keys = string.Format("Ma_vt = '" + mavt + "' and Ma_vitri = '" + mavitri + "'");
                    }

                }
                else
                {
                    if (mavitri == "")
                    {
                        keys = string.Format("Ma_vt = '" + mavt + "' and Ma_lo = '" + malo + "'");
                    }
                    else
                    {
                        keys = string.Format("Ma_vt = '" + mavt + "' and Ma_lo = '" + malo + "' and Ma_vitri = '" + mavitri + "'");
                    }

                    
                }
                
            }
            else
            {
                if (malo == "")
                {
                    if (mavitri == "")
                    {
                        keys = string.Format("Ma_vt = '" + mavt + "' and Ma_kho = '" + makho + "'");

                    }
                    else
                    {
                        keys =string.Format("Ma_vt = '" + mavt + "' and Ma_kho = '" + makho + "' and Ma_vitri = '" + mavitri + "'");
                    }

                }
                else
                {
                    if (mavitri == "")
                    {
                        keys =string.Format("Ma_vt = '" + mavt + "' and Ma_kho = '" + makho + "' and Ma_lo = '" + malo +"'");
                    }
                    else
                    {
                        keys =string.Format("Ma_vt = '" + mavt + "' and Ma_kho = '" + makho + "' and Ma_lo = '" + malo +"' and Ma_vitri = '" + mavitri + "'");
                    }


                }  
            }

            SqlParameter[] plist = new[]
            {   
               
                //new SqlParameter("@cKey1", string.Format("Ma_vt = '"+mavt+"' and Ma_kho = '"+makho+"' and Ma_lo = '"+malo+"' and Ma_vitri = '"+mavitri+"'")),
                new SqlParameter("@cKey1",keys),
                new SqlParameter("@cKey2", ""),
                new SqlParameter("@cKey3", ""),
                new SqlParameter("@cStt_rec", sttRec),
                new SqlParameter("@dBg", ngayct.Date)
            };
            return SqlConnect.ExecuteDataset(CommandType.StoredProcedure, "VPA_EdItems_VITRI_DATE_STT_REC", plist).Tables[0];
        }
        public DataTable GetViTriLoDateAll(string mavt_in, string makho_in, string malo_in, string mavitri_in, string sttRec, DateTime ngayct)
        {
            SqlParameter[] plist = new[]
            {
                new SqlParameter("@cKey1", string.Format("Ma_vt in ("+mavt_in+") and Ma_kho in ("+makho_in
                    +") and Ma_lo in ("+malo_in+") and Ma_vitri in ("+mavitri_in+")")),
                new SqlParameter("@cKey2", ""),
                new SqlParameter("@cKey3", ""),
                new SqlParameter("@cStt_rec", sttRec),
                new SqlParameter("@dBg", ngayct.Date)
            };
            return SqlConnect.ExecuteDataset(CommandType.StoredProcedure, "VPA_EdItems_VITRI_DATE_STT_REC", plist).Tables[0];
        }

        /// <summary>
        /// [VPA_CheckTonXuatAm] 1,'20160216','IND','STTREC','a.MA_VT=''VTLO1'' AND a.MA_KHO=',''
        /// </summary>
        /// <returns></returns>
        public DataTable GetStock(string mact, string mavt, string makho, string sttRec, DateTime ngayct)
        {
            //    @Type AS TINYINT, -- 0: Đầu kỳ, 1: Cuối kỳ
            //@Ngay_ct AS SmallDateTime, -- Ngày tính số dư đầu kỳ
            //@ma_ct varchar(50),
            //@Stt_rec char(13),
            //@Advance AS VARCHAR(8000) = '', -- Điều kiện lọc 
            //@OutputInsert VARCHAR(4000) = '' --Tên bảng nhận dữ liệu ra

            mavt = mavt.Replace("'", "''");
            makho = makho.Replace("'", "''");
            SqlParameter[] plist =
            {
                new SqlParameter("@Type", 1),
                new SqlParameter("@Ngay_ct", ngayct),
                new SqlParameter("@ma_ct", mact),
                new SqlParameter("@Stt_rec", sttRec),
                new SqlParameter("@Advance", string.Format("a.MA_VT='"+mavt+"' AND a.MA_KHO='"+makho+"'")),
                new SqlParameter("@OutputInsert", "")
            };
            return SqlConnect.ExecuteDataset(CommandType.StoredProcedure, "VPA_CheckTonXuatAm", plist).Tables[0];
        }
        
        public DataTable GetStockAll(string mact, string mavt_in, string makho_in, string sttRec, DateTime ngayct)
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@Type", 1),
                new SqlParameter("@Ngay_ct", ngayct),
                new SqlParameter("@ma_ct", mact),
                new SqlParameter("@Stt_rec", sttRec),
                new SqlParameter("@Advance", string.Format("a.MA_VT in ("+mavt_in+") AND a.MA_KHO in ("+makho_in+")")),
                new SqlParameter("@OutputInsert", "")
            };
            return SqlConnect.ExecuteDataset(CommandType.StoredProcedure, "VPA_CheckTonXuatAm", plist).Tables[0];
        }

        public V6TableStruct GetTableStruct(string tableName)
        {
            return V6SqlconnectHelper.GetTableStruct(tableName);
        }

        #region ==== CHANGE ====
        public void ChangeAll_Id(string madm, string oldCode, string newCode)
        {
            SqlParameter[] plist =
            {
                // Tuanmh 29/05/2016
                //@ma_dm varchar(64),
                //@old_value varchar(64),
                //@new_value varchar(64)

                new SqlParameter("@ma_dm", madm),
                new SqlParameter("@old_value", oldCode),
                new SqlParameter("@new_value", newCode)
            };

            SqlConnect.ExecuteNonQuery(CommandType.StoredProcedure, "VPA_ChangeListId", plist);
        }

        /// <summary>
        /// Thay đổi mã khách hàng
        /// </summary>
        /// <param name="oldCode">Mã cũ</param>
        /// <param name="newCode">Mã mới</param>
        /// <returns></returns>
        public void ChangeCustomeId(string oldCode, string newCode)
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@coCust", oldCode),
                new SqlParameter("@cnCust", newCode)
            };

            SqlConnect.ExecuteNonQuery(CommandType.StoredProcedure, "VPA_EditCustId", plist);
        }
        public void ChangeItemId(string oldCode, string newCode)
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@coItem", oldCode),
                new SqlParameter("@cnItem", newCode)
            };

            SqlConnect.ExecuteNonQuery(CommandType.StoredProcedure, "VPA_EditItemId", plist);
        }
        public void ChangeWarehouseId(string oldCode, string newCode)
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@coWh", oldCode),
                new SqlParameter("@cnWh", newCode)
            };

            SqlConnect.ExecuteNonQuery(CommandType.StoredProcedure, "VPA_EditWhId", plist);
        }

        public void ChangeDepartmentId(string oldCode, string newCode)
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@coCode", oldCode),
                new SqlParameter("@cnCode", newCode)
            };

            SqlConnect.ExecuteNonQuery(CommandType.StoredProcedure, "VPA_EditDepartmentId", plist);
        }
        public void ChangeUnitId(string oldCode, string newCode)
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@coWh", oldCode),
                new SqlParameter("@cnWh", newCode)
            };

            SqlConnect.ExecuteNonQuery(CommandType.StoredProcedure, "VPA_EditUnitId", plist);
        }
        public void ChangeJobId(string oldCode, string newCode)
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@coJob", oldCode),
                new SqlParameter("@cnJob", newCode)
            };

            SqlConnect.ExecuteNonQuery(CommandType.StoredProcedure, "VPA_EditJobId", plist);
        }

        public void ChangeAccountId(string oldCode, string newCode)
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@coAcc", oldCode),
                new SqlParameter("@cnAcc", newCode)
            };

            SqlConnect.ExecuteNonQuery(CommandType.StoredProcedure, "VPA_EditAccId", plist);
        }

        #endregion change
        
        #region ==== CHECK ====

        /// <summary>
        /// Kiểm tra mã tồn tại.
        /// </summary>
        /// <param name="madm"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool AllCheckExist(string madm, string value)
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@ma_dm", madm), 
                new SqlParameter("@value", value)
            };
            var result = SqlConnect.ExecuteScalar(CommandType.StoredProcedure, "VPA_CheckAllListId", plist);
            return ObjectAndString.ObjectToInt(result) == 1;

        }

        /// <summary>
        /// Kiểm tra trong bảng Altt có tên bảng trong Ma_dm hay không
        /// </summary>
        /// <param name="ma_dm"></param>
        /// <returns></returns>
        public bool CheckAltt(string ma_dm)
        {
            try
            {
                var sql = "select COUNT(ma_dm) count0 from altt where ma_dm=@ma_dm";
                var result = (int)SqlConnect.ExecuteScalar(CommandType.Text, sql, new SqlParameter("@ma_dm", ma_dm));
                return result == 1;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Kiểm tra xem có được thêm (status 1) hoặc sửa (status 0) hay không.
        /// VPA_isValidOneCode_Full
        /// </summary>
        /// <returns></returns>
        public bool IsValidOneCode_Full(string cInputTable, byte nStatus,
            string cInputField, string cpInput, string cOldItems)
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@cInputTable", cInputTable),
                new SqlParameter("@nStatus", nStatus),
                new SqlParameter("@cInputField", cInputField),
                new SqlParameter("@cpInput", cpInput),
                new SqlParameter("@cOldItems", cOldItems)
            };

            object result = 0;
            var aldmconfig = SqlConnect.Select("ALDM", "", "MA_DM='"+cInputTable+"'").Data;

            if (aldmconfig.Rows.Count == 1 && aldmconfig.Rows[0]["CHECK_LONG"].ToString() == "1")
            {
                result = SqlConnect.ExecuteScalar(CommandType.StoredProcedure, "VPA_isValidOneCode", plist);
            }
            else
            {
                result = SqlConnect.ExecuteScalar(CommandType.StoredProcedure, "VPA_isValidOneCode_Full", plist);
            }

            if (result != null && Convert.ToInt32(result) == 1) return true;
            return false;
        }

        public bool IsValidTwoCode_Full(string cInputTable, byte nStatus,
           string cInputField1, string cpInput1, string cOldItems1,
            string cInputField2, string cpInput2, string cOldItems2)
        {

            SqlParameter[] plist =
            {
                new SqlParameter("@cInputTable", cInputTable),
                new SqlParameter("@nStatus", nStatus),
                new SqlParameter("@cInputField1", cInputField1),
                new SqlParameter("@cInputField2", cInputField2),
                new SqlParameter("@cpInput1", cpInput1),
                new SqlParameter("@cpInput2", cpInput2),
                new SqlParameter("@cpInput1Old", cOldItems1),
                new SqlParameter("@cpInput2Old", cOldItems2)
            };
            object obj = SqlConnect.ExecuteScalar(CommandType.StoredProcedure, "VPA_isValidTwoCode_Full", plist);
            if (obj != null && Convert.ToInt32(obj) == 1) return true;

            return false;
        }

        /// <summary>
        /// Kiểm tra xem có được thêm (status 1) hoặc sửa (status 0) hay không.
        /// </summary>
        /// <param name="cInputTable"></param>
        /// <param name="nStatus"></param>
        /// <param name="cInputField1"></param>
        /// <param name="cpInput1"></param>
        /// <param name="cOldItems1"></param>
        /// <param name="cInputField2"></param>
        /// <param name="cpInput2"></param>
        /// <param name="cOldItems2"></param>
        /// <param name="cInputField3"></param>
        /// <param name="cpInput3"></param>
        /// <param name="cOldItems3"></param>
        /// <returns></returns>
        public bool IsValidThreeCode(string cInputTable, byte nStatus,
           string cInputField1, string cpInput1, string cOldItems1,
            string cInputField2, string cpInput2, string cOldItems2,
            string cInputField3, string cpInput3, string cOldItems3)
        {

            SqlParameter[] plist =
            {
                new SqlParameter("@cInputTable", cInputTable),
                new SqlParameter("@nStatus", nStatus),
                new SqlParameter("@cInputField1", cInputField1),
                new SqlParameter("@cInputField2", cInputField2),
                new SqlParameter("@cInputField3", cInputField3),
                new SqlParameter("@cpInput1", cpInput1),
                new SqlParameter("@cpInput2", cpInput2),
                new SqlParameter("@cpInput3", cpInput3),
                new SqlParameter("@cpInput1Old", cOldItems1),
                new SqlParameter("@cpInput2Old", cOldItems2),
                new SqlParameter("@cpInput3Old", cOldItems3)
            };

            object obj = SqlConnect.ExecuteScalar(CommandType.StoredProcedure, "VPA_isValidThreeCode", plist);
            if (obj != null && Convert.ToInt32(obj) == 1) return true;

            return false;
        }
        public bool IsValidTwoCode_OneDate(string cInputTable, byte nStatus,
          string cInputField1, string cpInput1, string cOldItems1,
           string cInputField2, string cpInput2, string cOldItems2,
           string cInputField3, string cpInput3, string cOldItems3)
        {

            SqlParameter[] plist =
            {
                new SqlParameter("@cInputTable", cInputTable),
                new SqlParameter("@nStatus", nStatus),
                new SqlParameter("@cInputField1", cInputField1),
                new SqlParameter("@cInputField2", cInputField2),
                new SqlParameter("@nInputField1", cInputField3),
                new SqlParameter("@cpInput1", cpInput1),
                new SqlParameter("@cpInput2", cpInput2),
                new SqlParameter("@npInput1", cpInput3),
                new SqlParameter("@cpInput1Old", cOldItems1),
                new SqlParameter("@cpInput2Old", cOldItems2),
                new SqlParameter("@npInput1Old", cOldItems3)
            };

            object obj = SqlConnect.ExecuteScalar(CommandType.StoredProcedure, "VPA_isValidTwoCode_OneDate", plist);
            if (obj != null && Convert.ToInt32(obj) == 1) return true;

            return false;
        }
        public bool IsValidThreeCode_OneDate(string cInputTable, byte nStatus,
          string cInputField1, string cpInput1, string cOldItems1,
           string cInputField2, string cpInput2, string cOldItems2,
           string cInputField3, string cpInput3, string cOldItems3,
           string cInputField4, string cpInput4, string cOldItems4)
        {

            SqlParameter[] plist =
            {
                new SqlParameter("@cInputTable", cInputTable),
                new SqlParameter("@nStatus", nStatus),
                new SqlParameter("@cInputField1", cInputField1),
                new SqlParameter("@cInputField2", cInputField2),
                new SqlParameter("@cInputField3", cInputField3),
                new SqlParameter("@nInputField1", cInputField4),
                new SqlParameter("@cpInput1", cpInput1),
                new SqlParameter("@cpInput2", cpInput2),
                new SqlParameter("@cpInput3", cpInput3),
                new SqlParameter("@npInput1", cpInput4),
                new SqlParameter("@cpInput1Old", cOldItems1),
                new SqlParameter("@cpInput2Old", cOldItems2),
                new SqlParameter("@cpInput3Old", cOldItems3),
                new SqlParameter("@npInput1Old", cOldItems4)
            };

            object obj = SqlConnect.ExecuteScalar(CommandType.StoredProcedure, "VPA_isValidThreeCode_OneDate", plist);
            if (obj != null && Convert.ToInt32(obj) == 1) return true;

            return false;
        }

        public bool IsValidEightCode_OneDate(string cInputTable, byte nStatus,
          string cInputField1, string cpInput1, string cOldItems1,
           string cInputField2, string cpInput2, string cOldItems2,
           string cInputField3, string cpInput3, string cOldItems3,
           string cInputField4, string cpInput4, string cOldItems4,
           string cInputField5, string cpInput5, string cOldItems5,
           string cInputField6, string cpInput6, string cOldItems6,
           string cInputField7, string cpInput7, string cOldItems7,
           string cInputField8, string cpInput8, string cOldItems8,
           string cInputField9, string cpInput9, string cOldItems9
            )
        {

            SqlParameter[] plist =
            {
                new SqlParameter("@cInputTable", cInputTable),
                new SqlParameter("@nStatus", nStatus),
                new SqlParameter("@cInputField1", cInputField1),
                new SqlParameter("@cInputField2", cInputField2),
                new SqlParameter("@cInputField3", cInputField3),
                new SqlParameter("@cInputField4", cInputField4),
                new SqlParameter("@cInputField5", cInputField5),
                new SqlParameter("@cInputField6", cInputField6),
                new SqlParameter("@cInputField7", cInputField7),
                new SqlParameter("@cInputField8", cInputField8),
                new SqlParameter("@nInputField1", cInputField9),
                new SqlParameter("@cpInput1", cpInput1),
                new SqlParameter("@cpInput2", cpInput2),
                new SqlParameter("@cpInput3", cpInput3),
                new SqlParameter("@cpInput4", cpInput4),
                new SqlParameter("@cpInput5", cpInput5),
                new SqlParameter("@cpInput6", cpInput6),
                new SqlParameter("@cpInput7", cpInput7),
                new SqlParameter("@cpInput8", cpInput8),
                new SqlParameter("@npInput1", cpInput9),
                new SqlParameter("@cpInput1Old", cOldItems1),
                new SqlParameter("@cpInput2Old", cOldItems2),
                new SqlParameter("@cpInput3Old", cOldItems3),
                new SqlParameter("@cpInput4Old", cOldItems4),
                new SqlParameter("@cpInput5Old", cOldItems5),
                new SqlParameter("@cpInput6Old", cOldItems6),
                new SqlParameter("@cpInput7Old", cOldItems7),
                new SqlParameter("@cpInput8Old", cOldItems8),
                new SqlParameter("@npInput1Old", cOldItems9)
            };

            object obj = SqlConnect.ExecuteScalar(CommandType.StoredProcedure, "VPA_isValidEightCode_OneDate", plist);
            if (obj != null && Convert.ToInt32(obj) == 1) return true;

            return false;
        }
        public bool IsValidThreeCode_OneNumeric(string cInputTable, byte nStatus,
           string cInputField1, string cpInput1, string cOldItems1,
            string cInputField2, string cpInput2, string cOldItems2,
            string cInputField3, string cpInput3, string cOldItems3,
            string nInputField1, Int32 npInput1, Int32 nOldItems1)
        {

            SqlParameter[] plist =
            {
                new SqlParameter("@cInputTable", cInputTable),
                new SqlParameter("@nStatus", nStatus),
                new SqlParameter("@cInputField1", cInputField1),
                new SqlParameter("@cInputField2", cInputField2),
                new SqlParameter("@cInputField3", cInputField3),
                new SqlParameter("@nInputField1", nInputField1),
                new SqlParameter("@cpInput1", cpInput1),
                new SqlParameter("@cpInput2", cpInput2),
                new SqlParameter("@cpInput3", cpInput3),
                new SqlParameter("@npInput1", npInput1),
                new SqlParameter("@cpInput1Old", cOldItems1),
                new SqlParameter("@cpInput2Old", cOldItems2),
                new SqlParameter("@cpInput3Old", cOldItems3),
                new SqlParameter("@npInput1Old", nOldItems1)
            };

            object obj = SqlConnect.ExecuteScalar(CommandType.StoredProcedure, "VPA_isValidThreeCode_OneNumeric", plist);
            if (obj != null && Convert.ToInt32(obj) == 1) return true;

            return false;
        }

        public bool IsValidFourCode_OneNumeric(string cInputTable, byte nStatus,
           string cInputField1, string cpInput1, string cOldItems1,
            string cInputField2, string cpInput2, string cOldItems2,
            string cInputField3, string cpInput3, string cOldItems3,
            string cInputField4, string cpInput4, string cOldItems4,
            string nInputField1, Int32 npInput1, Int32 nOldItems1)
        {

            SqlParameter[] plist =
            {
                new SqlParameter("@cInputTable", cInputTable),
                new SqlParameter("@nStatus", nStatus),
                new SqlParameter("@cInputField1", cInputField1),
                new SqlParameter("@cInputField2", cInputField2),
                new SqlParameter("@cInputField3", cInputField3),
                new SqlParameter("@cInputField4", cInputField4),
                new SqlParameter("@nInputField1", nInputField1),
                new SqlParameter("@cpInput1", cpInput1),
                new SqlParameter("@cpInput2", cpInput2),
                new SqlParameter("@cpInput3", cpInput3),
                new SqlParameter("@cpInput4", cpInput4),
                new SqlParameter("@npInput1", npInput1),
                new SqlParameter("@cpInput1Old", cOldItems1),
                new SqlParameter("@cpInput2Old", cOldItems2),
                new SqlParameter("@cpInput3Old", cOldItems3),
                new SqlParameter("@cpInput4Old", cOldItems4),
                new SqlParameter("@npInput1Old", nOldItems1)
            };

            object obj = SqlConnect.ExecuteScalar(CommandType.StoredProcedure, "VPA_isValidFourCode_OneNumeric", plist);
            if (obj != null && Convert.ToInt32(obj) == 1) return true;

            return false;
        }
        public bool IsValidFiveCode_TwoNumeric(string cInputTable, byte nStatus,
          string cInputField1, string cpInput1, string cOldItems1,
           string cInputField2, string cpInput2, string cOldItems2,
           string cInputField3, string cpInput3, string cOldItems3,
           string cInputField4, string cpInput4, string cOldItems4,
           string cInputField5, string cpInput5, string cOldItems5,
           string nInputField1, Int32 npInput1, Int32 nOldItems1,
            string nInputField2, Int32 npInput2, Int32 nOldItems2
            )
        {

            SqlParameter[] plist =
            {
                new SqlParameter("@cInputTable", cInputTable),
                new SqlParameter("@nStatus", nStatus),
                new SqlParameter("@cInputField1", cInputField1),
                new SqlParameter("@cInputField2", cInputField2),
                new SqlParameter("@cInputField3", cInputField3),
                new SqlParameter("@cInputField4", cInputField4),
                new SqlParameter("@cInputField5", cInputField5),
                new SqlParameter("@nInputField1", nInputField1),
                 new SqlParameter("@nInputField2", nInputField2),
                new SqlParameter("@cpInput1", cpInput1),
                new SqlParameter("@cpInput2", cpInput2),
                new SqlParameter("@cpInput3", cpInput3),
                new SqlParameter("@cpInput4", cpInput4),
                new SqlParameter("@cpInput5", cpInput5),
                new SqlParameter("@npInput1", npInput1),
                new SqlParameter("@npInput2", npInput2),
                new SqlParameter("@cpInput1Old", cOldItems1),
                new SqlParameter("@cpInput2Old", cOldItems2),
                new SqlParameter("@cpInput3Old", cOldItems3),
                new SqlParameter("@cpInput4Old", cOldItems4),
                new SqlParameter("@cpInput5Old", cOldItems5),
                new SqlParameter("@npInput1Old", nOldItems1),
                new SqlParameter("@npInput2Old", nOldItems2)
            };

            object obj = SqlConnect.ExecuteScalar(CommandType.StoredProcedure, "VPA_isValidFiveCode_TwoNumeric", plist);
            if (obj != null && Convert.ToInt32(obj) == 1) return true;

            return false;
        }
        public bool IsValidTwoCode_OneNumeric(string cInputTable, byte nStatus,
          string cInputField1, string cpInput1, string cOldItems1,
           string cInputField2, string cpInput2, string cOldItems2,
           string nInputField1, Int32 npInput1, Int32 nOldItems1)
        {

            SqlParameter[] plist =
            {
                new SqlParameter("@cInputTable", cInputTable),
                new SqlParameter("@nStatus", nStatus),
                new SqlParameter("@cInputField1", cInputField1),
                new SqlParameter("@cInputField2", cInputField2),
                new SqlParameter("@nInputField1", nInputField1),
                new SqlParameter("@cpInput1", cpInput1),
                new SqlParameter("@cpInput2", cpInput2),
                new SqlParameter("@npInput1", npInput1),
                new SqlParameter("@cpInput1Old", cOldItems1),
                new SqlParameter("@cpInput2Old", cOldItems2),
                new SqlParameter("@npInput1Old", nOldItems1)
            };

            object obj = SqlConnect.ExecuteScalar(CommandType.StoredProcedure, "VPA_isValidTwoCode_OneNumeric", plist);
            if (obj != null && Convert.ToInt32(obj) == 1) return true;

            return false;
        }

        public bool IsValidOneCode_OneDate(string cInputTable, byte nStatus,
           string cInputField1, string nInputField1, string cpInput1,
            string npInput1, string cpInput1Old, string npInput1Old)
        {

            SqlParameter[] plist =
            {
                new SqlParameter("@cInputTable", cInputTable),
                new SqlParameter("@nStatus", nStatus),
                new SqlParameter("@cInputField1", cInputField1),
                new SqlParameter("@nInputField1", nInputField1),
                new SqlParameter("@cpInput1", cpInput1),
                new SqlParameter("@npInput1", npInput1),
                new SqlParameter("@cpInput1Old", cpInput1Old),
                new SqlParameter("@npInput1Old", npInput1Old)
            };
            object obj = SqlConnect.ExecuteScalar(CommandType.StoredProcedure, "VPA_isValidOneCode_OneDate", plist);
            if (obj != null && Convert.ToInt32(obj) == 1) return true;

            return false;

        }
        public bool IsValidTwoCode_TwoNumeric(string cInputTable, byte nStatus,
         string cInputField1, string cpInput1, string cOldItems1,
          string cInputField2, string cpInput2, string cOldItems2,
          string nInputField1, Int32 npInput1, Int32 nOldItems1,
           string nInputField2, Int32 npInput2, Int32 nOldItems2
           )
        {

            SqlParameter[] plist =
            {
                new SqlParameter("@cInputTable", cInputTable),
                new SqlParameter("@nStatus", nStatus),
                new SqlParameter("@cInputField1", cInputField1),
                new SqlParameter("@cInputField2", cInputField2),
                new SqlParameter("@nInputField1", nInputField1),
                new SqlParameter("@nInputField2", nInputField2),
                new SqlParameter("@cpInput1", cpInput1),
                new SqlParameter("@cpInput2", cpInput2),
                new SqlParameter("@npInput1", npInput1),
                new SqlParameter("@npInput2", npInput2),
                new SqlParameter("@cpInput1Old", cOldItems1),
                new SqlParameter("@cpInput2Old", cOldItems2),
                new SqlParameter("@npInput1Old", nOldItems1),
                new SqlParameter("@npInput2Old", nOldItems2)
            };

            object obj = SqlConnect.ExecuteScalar(CommandType.StoredProcedure, "VPA_isValidTwoCode_TwoNumeric", plist);
            if (obj != null && Convert.ToInt32(obj) == 1) return true;

            return false;
        }

        /// <summary>
        /// Đã tồn tại mã trong bảng => true
        /// [VPA_isExistOneCode_List]
        /// </summary>
        /// <returns></returns>
        public bool IsExistOneCode_List(string cInputTableList, string cInputField, string cpInput)
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@cInputTable_list", cInputTableList),
                new SqlParameter("@cInputField", cInputField),
                new SqlParameter("@cpInput", cpInput)
            };

            object obj = SqlConnect.ExecuteScalar(CommandType.StoredProcedure, "VPA_isExistOneCode_List", plist);
            if (obj != null && (int)obj == 1) return true;

            return false;
        }

        public bool IsExistTwoCode_List(string cInputTableList, string cInputField1, string cpInput1,
              string cInputField2, string cpInput2)
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@cInputTable_list", cInputTableList),
                new SqlParameter("@cInputField1", cInputField1),
                new SqlParameter("@cpInput1", cpInput1),
                new SqlParameter("@cInputField2", cInputField1),
                new SqlParameter("@cpInput2", cpInput1)
            };

            object obj = SqlConnect.ExecuteScalar(CommandType.StoredProcedure, "VPA_isExistTwoCode_List", plist);
            if (obj != null && (int)obj == 1) return true;

            return false;
        }

        public bool IsExistThreeCode_List(string cInputTableList, string cInputField1, string cpInput1,
              string cInputField2, string cpInput2, string cInputField3, string cpInput3)
        {
            SqlParameter[] plist =
            {
                new SqlParameter("@cInputTable_list", cInputTableList),
                new SqlParameter("@cInputField1", cInputField1),
                new SqlParameter("@cpInput1", cpInput1),
                new SqlParameter("@cInputField2", cInputField1),
                new SqlParameter("@cpInput2", cpInput1),
                new SqlParameter("@cInputField3", cInputField3),
                new SqlParameter("@cpInput3", cpInput3)
            };

            object obj = SqlConnect.ExecuteScalar(CommandType.StoredProcedure, "VPA_isExistThreeCode_List", plist);
            if (obj != null && (int)obj == 1) return true;

            return false;
        }
        #endregion check

        public string GetSoCt(string mode, string voucherNo, string mact, string maDvcs, int userId)
        {
            SqlParameter[] prlist =
            {
                new SqlParameter("@Mode_VC", mode),
                new SqlParameter("@cVoucherNo", voucherNo),
                new SqlParameter("@cma_ct", mact),
                new SqlParameter("@cma_dvcs", maDvcs),
                new SqlParameter("@User_id", userId)
            };
            var result = SqlConnect.ExecuteScalar(
                CommandType.StoredProcedure,
                "VPA_GET_VoucherNo_Format",
                prlist)
                .ToString().Trim();


            return result;
        }

        public string GetNewSoCt(string masonb)
        {
            SqlParameter[] prlist =
            {
                new SqlParameter("@Ma_sonb", masonb)
            };
            var result = SqlConnect.ExecuteDataset(CommandType.StoredProcedure, "VPA_GetNewSoct", prlist);
            if (result.Tables.Count == 0) return "";
            var data = result.Tables[0];
            if (data.Rows.Count == 0) return "";
            var formatText = data.Rows[0]["TRANSFORM"].ToString().Trim();
            if (formatText == "") return "";
            var value = ObjectAndString.ObjectToDecimal(data.Rows[0]["SO_CT"]);
            var sResult = string.Format(formatText, value);
            return sResult;
        }

        public string GetNewSttRec(string mact)
        {
            if (mact.Length > 3) mact = mact.Substring(0, 3);
            var param = new SqlParameter("@pMa_ct", mact);
            string sttRec = SqlConnect.ExecuteScalar(CommandType.StoredProcedure, "VPA_sGet_stt_rec", param).ToString();
            if (string.IsNullOrEmpty(sttRec))
            {
                throw new Exception("Không tạo mới được.");
            }
            return sttRec;
        }

        public string GetNewLikeSttRec(string pMaCt, string pKhoa, string pLoai)
        {
            if (pMaCt.Length > 3) pMaCt = pMaCt.Substring(0, 3);
            var param = new SqlParameter("@pMa_ct", pMaCt);
            var param2 = new SqlParameter("@pKhoa", pKhoa);
            var param3 = new SqlParameter("@pLoai", pLoai);
            string sttRec = SqlConnect.ExecuteScalar(CommandType.StoredProcedure, "VPA_sGet_Key_Like_stt_rec", param, param2, param3).ToString();
            return sttRec;
        }

        public bool ExportBackupInvoice(string am, string ad, string ad2)
        {
            try
            {
                var data = new DataTable("Name");
                var saveAs = "path\\" + am + DateTime.Now.ToString("yyyyMMdd") + ".xls";
                V6Tools.V6Export.Data_Table.ToExcel(data, saveAs, "title", true);
                return true;
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("BusinessService ExportBackupInvoice: " + ex.Message, "DataAccessLayer");
                return false;
            }
        }

        public bool StartSqlConnect(string key, string iniLocation)
        {
            return SqlConnect.StartSqlConnect(key, iniLocation);
        }

    }
}

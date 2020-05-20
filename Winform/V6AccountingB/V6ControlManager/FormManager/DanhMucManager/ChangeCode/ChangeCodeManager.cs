using System.Collections.Generic;
using V6Init;
using V6Structs;

namespace V6ControlManager.FormManager.DanhMucManager.ChangeCode
{
    public static class ChangeCodeManager
    {
        public static ChangeCodeBase0 GetChangeCodeControl(string tableName, IDictionary<string, object> data)
        {
            var name = V6TableHelper.ToV6TableName(tableName);
            
            switch (name)
            {
                case V6TableName.Alkh:
                    return new KhachHangChangeCodeForm(data);
                case V6TableName.Alvt:
                    return new VatTuChangeCodeForm(data);
                case V6TableName.Alkho:
                    return new KhoChangeCodeForm(data);
                case V6TableName.Albp:
                    return new BoPhanChangeCodeForm(data);
                case V6TableName.Aldvcs:
                    return new DonViCoSoChangeCodeForm(data);
                case V6TableName.Alvv:
                    return new VuViecChangeCodeForm(data);
                case V6TableName.Altk0:
                    return new TaiKhoanChangeCodeForm(data);
                case V6TableName.Allo:
                    return new LoHangChangeCode(data);

                //{Tuanmh 29/05/2016 New function for all - lookup ALDM-> aldm.ma_dm=v6tablename,aldm.[value],aldm.f_name
                case V6TableName.Alsonb:
                case V6TableName.Albpht:
                case V6TableName.Altd:
                case V6TableName.Altd2:
                case V6TableName.Altd3:
                case V6TableName.Alku:
                case V6TableName.Alphi:
                case V6TableName.Alhd:
                case V6TableName.Alts:
                case V6TableName.Alcc:
                    return new AllChangeCodeForm(data, name.ToString());

                default:
                    //var tableName = name.ToString();
                    IDictionary<string, object> keys = new Dictionary<string, object>();
                    keys.Add("MA_DM", tableName);
                    var aldmConfig = ConfigManager.GetAldmConfig(tableName);
                    //var aldm = V6BusinessHelper.Select("Aldm", keys, "*").Data;
                    if (aldmConfig.HaveInfo)
                    {
                        if (aldmConfig.F6_TABLE != null && aldmConfig.F6_TABLE.Trim() != "")
                        {
                            return new AllChangeCodeForm(data, name.ToString());
                        }
                    }
                    break;
            }
            return null;
        }
    }
}

using Newtonsoft.Json;
using System;

public class SampleClass
{
    class UserDefinedData
    {
        public string DonViBanHang;
        public string MaSoThue;
        public string DiaChi;
    }
    public string Sample()
    {
        UserDefinedData udd = new UserDefinedData();
        udd.DonViBanHang = "ádaf";
        udd.MaSoThue = "àdasf";
        udd.DiaChi = "ấdf";

        string json = null;
        string msg = ObjectToJson(udd, out json);
        if (msg.Length > 0) // has error
            return msg;

        return msg;
    }


    static public string ObjectToJson(object obj, out string value)
    {
        value = "";
        try
        {
            value = JsonConvert.SerializeObject(obj);
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }
        return "";
    }
}
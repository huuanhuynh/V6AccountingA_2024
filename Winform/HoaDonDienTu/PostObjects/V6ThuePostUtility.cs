using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using V6Tools.V6Convert;

namespace V6ThuePost
{
    public class V6ThuePostUtility
    {
        public static ConfigLine ReadXmlLine(XmlTextReader reader)
        {
            ConfigLine config = new ConfigLine();
            config.Field = reader.GetAttribute("Field");
            config.Value = reader.GetAttribute("Value");
            config.FieldV6 = reader.GetAttribute("FieldV6");
            config.Type = reader.GetAttribute("Type");
            config.DataType = reader.GetAttribute("DataType");
            config.Format = reader.GetAttribute("Format");

            config.MA_TD2 = reader.GetAttribute("MA_TD2");
            config.MA_TD3 = reader.GetAttribute("MA_TD3");
            config.SL_TD1 = ObjectAndString.StringToDecimal(reader.GetAttribute("SL_TD1"));
            config.SL_TD2 = ObjectAndString.StringToDecimal(reader.GetAttribute("SL_TD2"));
            config.SL_TD3 = ObjectAndString.StringToDecimal(reader.GetAttribute("SL_TD3"));

            config.Note = reader.GetAttribute("Note");
            return config;
        }
    }
}

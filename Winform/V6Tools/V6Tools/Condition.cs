using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using V6Tools.V6Convert;

namespace V6Tools
{
    public class Condition
    {
        public Condition(string field, string oper, string value)
        {
            FIELD = field; OPER = oper; VALUE = value;
        }
        public string FIELD { get; set; }
        public string OPER { get; set; }
        public string VALUE { get; set; }
        public bool Check(DataRow data)
        {
            return ObjectAndString.CheckCondition(data[FIELD], OPER, VALUE);
        }
        public bool Check(DataGridViewRow data)
        {
            return ObjectAndString.CheckCondition(data.Cells[FIELD].Value, OPER, VALUE);
        }
        public bool Check(IDictionary<string, object> data)
        {
            if (data.ContainsKey(FIELD)) return ObjectAndString.CheckCondition(data[FIELD], OPER, VALUE);
            else if (data.ContainsKey(FIELD.ToUpper())) return ObjectAndString.CheckCondition(data[FIELD.ToUpper()], OPER, VALUE);
            else return false;
        }
        public bool Check(IDictionary<string, string> data)
        {
            if (data.ContainsKey(FIELD)) return ObjectAndString.CheckCondition(data[FIELD], OPER, VALUE);
            else if (data.ContainsKey(FIELD.ToUpper())) return ObjectAndString.CheckCondition(data[FIELD.ToUpper()], OPER, VALUE);
            else return false;
        }
        public override string ToString()
        {
            return string.Format("{0} {1} {2}", FIELD, OPER, VALUE);
        }
    }
}

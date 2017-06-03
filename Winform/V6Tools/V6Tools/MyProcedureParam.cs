//using System.Linq;

namespace V6Tools
{
    #region MyParamProcedure
    public class MyProcedureParam0
    {
        /// <summary>
        /// Lop dung de chua tham so Parammeter of Procedure duoi dang (stringName,stringValue)
        /// dung trong lop PassDataTableIntoProcedure
        /// </summary>

        string nameParam;
        public string NameParam
        {
            get { return nameParam; }
            set { nameParam = value; }
        }

        object valueParam;
        public object ValueParam
        {
            get { return valueParam; }
            set { valueParam = value; }
        }

        //public MyProcedureParam(string nameParam, object valueParam)
        //{
        //    this.nameParam = nameParam;
        //    this.valueParam = valueParam;
        //}

        //public MyProcedureParam(string nameParam)
        //{
        //    this.nameParam = nameParam;
        //}
    }
    #endregion
}

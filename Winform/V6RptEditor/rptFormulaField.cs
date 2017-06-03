using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace V6RptEditor
{
    public class rptFormulaField : rptField
    {
        #region ==== Object Name ====
        [Category("_crProperty")]
        [Description("Tên đối tượng")]
        public string ObjectName
        {
            get
            {
                return _obj.Name;
            }
        }
        #endregion
        #region ===== FormulaName =====
        [Description("Tên biểu thức")]
        [Category("_crProperty")]
        public string FormulaName
        {
            get { return _obj.DataSource.Name; }
        }
        #endregion

        #region ===== FormulaText =====
        private string formulaText = null;
        [Description("@Biểu thức: Có thể sửa tuân theo quy tắc crystal report formula")]
        [Category("_crProperty")]
        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor))]
        public string FormulaText
        {
            get
            {
                return formulaText;
            }
            set
            {
                if (value != formulaText)
                {
                    formulaText = value;
                    FormulaTextChanged(this, new rptFieldEvenArgs(value));
                }
            }
        }
        public delegate void eventFormulaTextChanged(object sender, rptFieldEvenArgs e);
        public event eventFormulaTextChanged FormulaTextChanged;
        #endregion

        public rptFormulaField(FieldObject obj):base(obj)
        {
            GetFormulaProperties(obj);
            this.FormulaTextChanged += rptField_FormulaTextChanged;
        }

        
        private void GetFormulaProperties(FieldObject obj)
        {   
            FormulaFieldDefinition formula =
                FormRptEditor._rpt.DataDefinition.FormulaFields[FormulaName];
            formulaText = formula.Text;
            
            //ParameterFieldDefinition parameter =
            //    Form1._rpt.DataDefinition.ParameterFields[formulaName];


            //switch (obj.DataSource.Kind)
            //{
            //    case CrystalDecisions.Shared.FieldKind.DatabaseField:
            //        break;
            //    case CrystalDecisions.Shared.FieldKind.FormulaField:
            //        break;
            //    case CrystalDecisions.Shared.FieldKind.GroupNameField:
            //        break;
            //    case CrystalDecisions.Shared.FieldKind.ParameterField:
            //        break;
            //    case CrystalDecisions.Shared.FieldKind.RunningTotalField:
            //        break;
            //    case CrystalDecisions.Shared.FieldKind.SQLExpressionField:
            //        break;
            //    case CrystalDecisions.Shared.FieldKind.SpecialVarField:
            //        break;
            //    case CrystalDecisions.Shared.FieldKind.SummaryField:
            //        break;
            //    default:
            //        break;
            //}
        }

        void rptField_FormulaTextChanged(object sender, rptFieldEvenArgs e)
        {
            FormRptEditor._rpt.DataDefinition.FormulaFields[FormulaName]
                .Text = e.Value;
        }
        
    }

    public class rptFieldEvenArgs : EventArgs
    {
        public string Value { get; set; }
        public rptFieldEvenArgs(string value)
        {
            Value = value;
        }
    }
}

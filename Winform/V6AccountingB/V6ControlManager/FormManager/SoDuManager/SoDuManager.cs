using System;
using V6ControlManager.FormManager.SoDuManager.Add_Edit;
using V6Init;
using V6Structs;

namespace V6ControlManager.FormManager.SoDuManager
{
    public class SoDuManager
    {
        public static SoDuAddEditControlVirtual GetAddEditControl(V6TableName tableName)
        {
            SoDuAddEditControlVirtual FormControl = null;
            switch (tableName)
            {
                case V6TableName.Abvt:
                    FormControl = new TonKhoDauKyAddEditForm();
                    break;
                case V6TableName.Abkh:
                    FormControl = new CongNoDauKyAddEditForm();
                    break;
                case V6TableName.Abtk:
                    FormControl = new TaiKhoanDauKyAddEditForm();
                    break;
                case V6TableName.Ablo:
                    FormControl = new LoDauKyAddEditForm();
                    break;
                case V6TableName.Althau:
                    FormControl = new AlThauAddEditControl();
                    break;
                case V6TableName.Alts:
                    FormControl = new AltsAddEditControl();
                    break;
                case V6TableName.Alcc:
                    FormControl = new AlccAddEditControl();
                    break;
                case V6TableName.Alpb:
                    FormControl = new AlpbAddEditControl();
                    break;
                case V6TableName.Aldmvt:
                    FormControl = new AldmvtAddEditControl();
                    break;
                case V6TableName.Acosxlt_aldmvt:
                    FormControl = new Acosxlt_aldmvtAddEditControl();
                    break;
                case V6TableName.Acosxlsx_aldmvt:
                    FormControl = new Acosxlsx_aldmvtSXDHAddEditForm();
                    break;
                case V6TableName.Alkmb:
                    FormControl = new AlkmbAddEditControl();
                    break;
                case V6TableName.Abspytcp:
                    FormControl = new AbspytcpAddEditForm();
                    break;
                case V6TableName.Acosxlt_abspytcp:
                    FormControl = new Acosxlt_abspytcpAddEditForm();
                    break;
                case V6TableName.Acosxlsx_abspytcp:
                    FormControl = new Acosxlsx_abspytcpAddEditForm();
                    break;
                case V6TableName.Abspdd:
                    FormControl = new AbspddAddEditForm();
                    break;
                case V6TableName.ACOSXLT_ABSPDD:
                    FormControl = new Acosxlt_abspddAddEditForm();
                    break;
                case V6TableName.Acosxlsx_abspdd:
                    FormControl = new Acosxlsx_abspddAddEditForm();
                    break;
                case V6TableName.Abvvkh:
                    FormControl = new AbvvkhAddEditForm();
                    break;
                case V6TableName.Abbpkh:
                    FormControl = new AbbpkhAddEditForm();
                    break;
                case V6TableName.Abvv:
                    FormControl = new AbvvAddEditForm();
                    break;
                case V6TableName.Abhdkh:
                    FormControl = new AbhdkhAddEditForm();
                    break;
                case V6TableName.Abphi:
                    FormControl = new AbphiAddEditForm();
                    break;
                case V6TableName.Abku:
                    FormControl = new AbkuAddEditForm();
                    break;
                case  V6TableName.Alinit:
                    FormControl = new AlinitAddEditControl();
                    break;
                case  V6TableName.Abntxt:
                    FormControl = new VaoChiTietTonKhoNTXTcontrol();
                    break;
                case  V6TableName.Acku:
                    FormControl = new ACKU_Form();
                    break;
                case V6TableName.Abtd:
                    FormControl = new ABTD_Form();
                    break;
                case V6TableName.Abtd2:
                    FormControl = new ABTD2_Form();
                    break;
                case V6TableName.Abtd3:
                    FormControl = new ABTD3_Form();
                    break;
                case V6TableName.Abvitri:
                    FormControl = new AbvitriAddEditForm();
                    break;

                case V6TableName.Acosxlt_alpbph:
                    FormControl = new Acosxlt_alpbphAddEditControl();
                    break;

                case V6TableName.Akhungck:
                    FormControl = new AkhungckAddEditControl();
                    break;
                case V6TableName.Abhhvt:
                    FormControl = new AbhhvtAddEditForm();
                    break;

                case V6TableName.Notable:
                    break;
                default:
                    throw new ArgumentOutOfRangeException("tableName");
            }
            if (FormControl == null)
            {
                throw new Exception(V6Text.NotSupported + "\n" + tableName);
            }
            FormControl.TableName = tableName;
            return FormControl;
        }
    }
}

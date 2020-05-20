using System;
using V6ControlManager.FormManager.SoDuManager.Add_Edit;
using V6Init;

namespace V6ControlManager.FormManager.SoDuManager
{
    public class SoDuManager
    {
        public static SoDuAddEditControlVirtual GetAddEditControl(string ma_dm)
        {
            AldmConfig aldm_config = ConfigManager.GetAldmConfig(ma_dm);
            //string formCode = aldm_config.FormCode;
            //if (formCode != null) formCode = formCode.ToUpper();

            SoDuAddEditControlVirtual FormControl;
            switch (ma_dm.ToUpper())
            {
                case "ABVT":
                    FormControl = new TonKhoDauKyAddEditForm();
                    break;
                case "ABKH":
                    FormControl = new CongNoDauKyAddEditForm();
                    break;
                case "ABTK":
                    FormControl = new TaiKhoanDauKyAddEditForm();
                    break;
                case "ABLO":
                    FormControl = new LoDauKyAddEditForm();
                    break;
                case "ALTHAU":
                    FormControl = new AlThauAddEditControl();
                    break;
                case "ALTS":
                    FormControl = new AltsAddEditControl();
                    break;
                case "ALCC":
                    FormControl = new AlccAddEditControl();
                    break;
                case "ALPB":
                    FormControl = new AlpbAddEditControl();
                    break;
                case "ALDMVT":
                case "V_ALDMVT":
                    FormControl = new AldmvtAddEditControl();
                    break;
                case "ACOSXLT_ALDMVT":
                    FormControl = new Acosxlt_aldmvtAddEditControl();
                    break;
                case "ACOSXLSX_ALDMVT":
                    FormControl = new Acosxlsx_aldmvtSXDHAddEditForm();
                    break;
                case "ALKMB":
                    FormControl = new AlkmbAddEditControl();
                    break;
                case "ABSPYTCP":
                    FormControl = new AbspytcpAddEditForm();
                    break;
                case "ACOSXLT_ABSPYTCP":
                    FormControl = new Acosxlt_abspytcpAddEditForm();
                    break;
                case "ACOSXLSX_ABSPYTCP":
                    FormControl = new Acosxlsx_abspytcpAddEditForm();
                    break;
                case "ABSPDD":
                    FormControl = new AbspddAddEditForm();
                    break;
                case "ACOSXLT_ABSPDD":
                    FormControl = new Acosxlt_abspddAddEditForm();
                    break;
                case "ACOSXLSX_ABSPDD":
                    FormControl = new Acosxlsx_abspddAddEditForm();
                    break;
                case "ABVVKH":
                    FormControl = new AbvvkhAddEditForm();
                    break;
                case "ABBPKH":
                    FormControl = new AbbpkhAddEditForm();
                    break;
                case "ABVV":
                    FormControl = new AbvvAddEditForm();
                    break;
                case "ABHDKH":
                    FormControl = new AbhdkhAddEditForm();
                    break;
                case "ABPHI":
                    FormControl = new AbphiAddEditForm();
                    break;
                case "ABKU":
                    FormControl = new AbkuAddEditForm();
                    break;
                case "ALINIT":
                    FormControl = new AlinitAddEditControl();
                    break;
                case "ABNTXT":
                    FormControl = new VaoChiTietTonKhoNTXTcontrol();
                    break;
                case "ACKU":
                    FormControl = new ACKU_Form();
                    break;
                case "ABTD":
                    FormControl = new ABTD_Form();
                    break;
                case "ABTD2":
                    FormControl = new ABTD2_Form();
                    break;
                case "ABTD3":
                    FormControl = new ABTD3_Form();
                    break;
                case "ABVITRI":
                    FormControl = new AbvitriAddEditForm();
                    break;

                case "ACOSXLT_ALPBPH":
                    FormControl = new Acosxlt_alpbphAddEditControl();
                    break;

                case "AKHUNGCK":
                    FormControl = new AkhungckAddEditControl();
                    break;
                case "ABHHVT":
                    FormControl = new AbhhvtAddEditForm();
                    break;
                case "ABNGHI":
                    FormControl = new AbnghiBsAddEditForm();
                    break;

                //case V6TableName.None:
                    //break;
                default:
                    if (aldm_config.HaveInfo)
                    {
                        if (aldm_config.IS_ALDM)
                        {
                            FormControl = new SoDuAddEditControlDynamicForm(ma_dm, aldm_config);
                            break;
                        }
                    }
                    throw new ArgumentOutOfRangeException("ma_dm:" + ma_dm);
            }
            if (FormControl == null)
            {
                throw new Exception(V6Text.NotSupported + "\n" + ma_dm);
            }
            FormControl._MA_DM = ma_dm.ToUpper();
            return FormControl;
        }
    }
}

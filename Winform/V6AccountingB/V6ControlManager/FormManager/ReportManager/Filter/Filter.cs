using V6ControlManager.FormManager.ReportManager.Filter.NhanSu;
using V6ControlManager.FormManager.ReportManager.Filter.Xuly;
using V6Controls.Forms;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public static class Filter
    {
        public static FilterBase GetFilterControl(string program)
        {
            program = program.Trim().ToUpper();
            V6ControlFormHelper.AddLastAction("GetFilterControl " + program);
            FilterBase result = null;
            switch (program)
            {
                    #region ==== Nhân sự ====

                case "HPRCONGCT":
                    result = new HPRCONGCT_Filter();
                    break;
                case "ERLISTNV":
                    result = new ERLISTNV_Filter();

                    #endregion nhân sự

                    #region ==== Phải thu ====

                    #region === Báo cáo bán hàng ===

                    break;
                case "ASOTH1": //Báo cáo bán hàng chi tiết theo mặt hàng
                    // result = new BaoCaoBanHangTheoChiTietMatHangFilter();
                    result = new ASOTH1();
                    break;
                case "ASOTH1F5": //Báo cáo bán hàng chi tiết theo mặt hàng
                    result = new ASOTH1F5();
                    break;
                case "ASOBK1": //Bản kê hóa đơn bán hàng
                    result = new ASOBK1();
                    break;
                case "ASOBK2": //Bản kê hóa đơn 1 mặt hàng
                    result = new ASOBK2();
                    break;
                case "VVP007": //Báo cáo bán hàng chi tiết (trừ trả lại)
                    result = new FilterBase();
                    break;
                case "VVP008": //Báo cáo đăng ký
                    result = new FilterBase();
                    break;
                case "AHINTH3": //Báo cáo hàng xuất tặng 2 chỉ tiêu
                    result = new AHINTH3();
                    break;
                case "AKSOTH1": //Báo cáo tổng hợp theo tháng, nhớm theo v
                    result = new AKSOTH1();
                    break;
                case "ASOBK14":
                    result = new ASOBK14();
                    break;
                case "AINBK11": //Báo cáo hàng nhập
                    result = new AINBK11();
                    break;
                case "ASOBK11": //So sánh giá bán, giá nhập
                    result = new FilterBase();
                    break;
                case "ASOBK12": //Báo cáo hàng chi tiết
                    result = new ASOBK12();
                    break;
                case "ASOBK13": //So sánh giá bán, bảng giá bán
                    result = new FilterBase();
                    break;
                case "AKSOTH3": //Báo cáo tổng hợp theo bộ phận
                    result = new FilterBase();
                    break;
                case "AHINTH3F5": //Báo cáo hàng xuất tặng 2 chỉ tiêu
                    result = new AHINTH3F5();
                    break;
                case "AVSO003":
                    result = new AVSO003();
                    break;
                case "ASOBK3":
                    result = new ASOBK3();
                    break;
                case "ASOBK3F5":
                    result = new ASOBK3F5();
                    break;
                case "AHINTH1":
                    result = new AHINTH1();
                    break;
                case "AKSOTH2":
                    result = new AKSOTH2();
                    break;
                case "AKSOTH2F5":
                    result = new AKSOTH2F5();
                    break;
                case "AFABCTSTH1":
                    result = new AFABCTSTH1();
                    break;
                case "AFABCTSTH2":
                    result = new AFABCTSTH2();

                    #endregion báo cáo bán hàng

                    #region ==== Cập nhật số dư ====

                    break;
                case "ARSD0_AR0":
                    result = new ARSD0_AR0();
                    break;
                case "AARBPKH2N":
                    result = new AARBPKH2N();
                    break;
                case "ACOVVKH2N":
                    result = new ACOVVKH2N();

                    #endregion Cập nhật số dư

                    #region === Báo cáo quản trị bán hàng ===

                    break;
                case "ASOBK3VV": //Bản kê hóa đơn nhóm theo vụ việc
                    result = new FilterBase();
                    break;
                case "ASOTH2": //Báo cáo ds bán hàng theo kh vv nv...
                    result = new ASOTH2();
                    break;
                case "ASOTH3": //Báo cáo 
                    result = new ASOTH3();
                    break;
                case "ASOTH3F5":
                    result = new ASOTH3F5();
                    break;
                case "ASOTH4":
                    result = new ASOTH4();
                    break;
                case "ASOTH5":
                    result = new ASOTH5();
                    break;
                case "ASOTH2F5":
                    result = new ASOTH2F5();
                    break;
                case "ASOTH6":
                    result = new ASOTH6();
                    break;
                case "ASOTH6F5":
                    result = new ASOTH6F5();
                    break;
                case "ASOTH7":
                    result = new ASOTH7();
                    break;
                case "ASOTH8":
                    result = new ASOTH8();

                    #endregion báo cáo quản trị bán hàng

                    #region === Báo cáo quản trị bán hàng 1 ===

                    break;
                case "AVSO001": //Bản kê hóa đơn bán hàng th
                    result = new AVSO001();
                    break;
                case "AVSO002": //Bản kê hóa đơn bán hàng tra lai
                    result = new AVSO002();
                    break;
                case "AVSO002F5": //Bản kê hóa đơn bán hàng tra lai
                    result = new AVSO002F5();
                    break;
                case "AVSO004":
                    result = new AVSO004();
                    break;
                case "AVSO006":
                    result = new AVSO006();
                    break;
                case "AVBCNH70A":
                    result = new AVBCNH70A();
                    break;
                case "AKSOTH5":
                    result = new AKSOTH5();
                    break;
                case "AKSOTH6":
                    result = new AKSOTH6();

                    #endregion báo cáo quản trị bán hàng 1

                    #region === Báo cáo công nợ khách hàng ===

                    break;
                case "AARSO1": //Sổ chi tiết công nợ của một khách hàng                                              
                    result = new AARSO1();
                    break;
                case "AARCD1": //Cân đối 1 tài khoản
                    result = new AARCD1();
                    break;
                case "AARCD1F5": //Cân đối 1 tài khoản
                    result = new AARCD1F5();
                    break;
                case "AARCD2": //Cân đối nhiều tài khoản
                    result = new AARCD2();
                    break;
                case "AARCD2F5":
                    result = new AARCD2F5();

                    break;
                case "AGLSO1": //Sổ chi tiết 1 tài khoản
                    result = new AGLSO1();
                    break;
                case "AARSD1DK":
                    result = new AARSD1DK();
                    break;
                case "AARSD1CK":
                    result = new AARSD1CK();
                    break;
                case "AARSD2":
                    result = new AARSD2();
                    break;
                case "AARTH1":
                    result = new AARTH1();
                    break;
                case "AARTH1F5":
                    result = new AARTH1F5();
                    break;
                case "AARSO1T":
                    result = new AARSO1T();
                    break;
                case "AARSO1TF5":
                    result = new AARSO1TF5();
                    break;
                case "AARSO1TF10":
                    result = new AARSO1TF10();

                    #endregion Báo cáo công nợ khách hàng

                    #region === Báo cáo quản trị hóa đơn   ===

                    break;
                case "AARTTBK1":
                    result = new AARTTBK1();
                    break;
                case "AARTTBK2":
                    result = new AARTTBK2();
                    break;
                case "AARTTBK3":
                    result = new AARTTBK3();
                    break;
                case "AARTTBK4":
                    result = new FilterBase();
                    break;
                case "AARTTBK1A":
                    result = new AARTTBK1A();
                    break;
                case "AARTTBK1B":
                    result = new AARTTBK1B();
                    break;
                case "AARTTV001":
                    result = new AARTTV001();
                    break;
                case "ATSOBK1T":
                    result = new ATSOBK1T();
                    break;
                case "ASOBK4":
                    result = new ASOBK4();
                    break;
                case "AARTTBK8":
                    result = new AARTTBK8();
                    break;
                case "AARTTBK7":
                    result = new AARTTBK7();

                    #endregion Báo cáo quản trị hóa đơn

                    #region ==== Cập nhật số liệu ====

                    break;
                case "ARSD_AR":
                    result = new ARSD_AR();
                    break;
                case "ARCMO_AR":
                    result = new ARCMO_AR();
                    break;
                case "ARCMO_ARF9":
                    result = new ARCMO_ARF9();

                    break;
                case "APDMO_AP":
                    result = new APDMO_AP();
                    break;
                case "APDMO_APF9":
                    result = new APDMO_APF9();

                    #endregion Cập nhật số liệu

                    #region === In danh mục từ điển ===

                    break;
                case "ASOBCGIA2":
                    result = new ASOBCGIA2();

                    #endregion === In danh mục từ điển ===

                    #region === BC khách hàng - vụ việc ===

                    break;
                case "ACOVVKHCD3":
                    result = new ACOVVKHCD3();
                    break;
                case "ACOVVKHBK4":
                    result = new ACOVVKHBK4();
                    break;
                case "ACOVVSD1DK":
                    result = new ACOVVSD1DK();
                    break;
                case "ACOVVSD1CK":
                    result = new ACOVVSD1CK();

                    #endregion === BC khách hàng - vụ việc ===

                    #endregion phải thu

                    #region ==== Phải trả ====

                    #region ==== Cập nhật số dư ====

                    break;
                case "ARSD0_AP0":
                    result = new ARSD0_AP0();

                    #endregion Cập nhật số dư

                    #region ==== Cập nhật số liệu ====

                    break;
                case "ARSD_AP":
                    result = new ARSD_AP();

                    #endregion Cập nhật số liệu

                    #region === Báo cáo quản trị hóa đơn   ===

                    break;
                case "AAPTTBK1":
                    result = new AAPTTBK1();
                    break;
                case "AAPTTBK2":
                    result = new AAPTTBK2();
                    break;
                case "AAPTTBK3":
                    result = new AAPTTBK3();
                    //break; case "AAPTTBK4":
                    //    result = new FilterBase();
                    //break; case "AAPTTBK1A":
                    //    result = new AARTTBK1A();
                    //break; case "AAPTTBK1B":
                    //    result = new AARTTBK1B();

                    #endregion Báo cáo quản trị hóa đơn

                    #region === Báo cáo công nợ phải trả   ===

                    break;
                case "AAPSO1T":
                    result = new AAPSO1T();
                    break;
                case "AAPSO1TF5":
                    result = new AAPSO1TF5();
                    break;
                case "AAPSO1TF10":
                    result = new AAPSO1TF10();

                    #endregion === Báo cáo công nợ phải trả   ===

                    #region === Báo cáo quản trị mua hàng  ===

                    break;
                case "APOBK3KH":
                    result = new APOBK3KH();
                    break;
                case "APOBK3NX":
                    result = new APOBK3NX();
                    break;
                case "APOBK3VV":
                    result = new APOBK3VV();
                    break;
                case "APOBK4":
                    result = new APOBK4();

                    #endregion === Báo cáo quản trị mua hàng   ===

                    #endregion Phải trả

                    #region ==== Tiền mặt ====

                    #region === Báo cáo tiền mặt ===

                    break;
                case "AGLSO1A":
                    result = new AGLSO1A();
                    break;
                case "AGLSO1B":
                    result = new AGLSO1B();
                    break;
                case "AGLSO1T":
                    result = new AGLSO1T();
                    break;
                case "AGLSO1TF5":
                    result = new AGLSO1TF5();
                    break;
                case "AGLSO1TF10":
                    result = new AGLSO1TF10();

                    break;
                case "AGLSO3":
                    result = new AGLSO3();
                    break;
                case "AGLSO3C":
                    result = new AGLSO3C();
                    break;
                case "AGLSO3T":
                    result = new AGLSO3T();

                    break;
                case "AGLSO3B":
                    result = new AGLSO3B();
                    break;
                case "AGLTH1":
                    result = new AGLTH1();
                    break;
                case "AGLTH1F5":
                    result = new AGLTH1F5();
                    break;
                case "AGLCD2":
                    result = new AGLCD2();
                    break;
                case "AGLCD2F5":
                    result = new AGLCD2F5();
                    break;
                case "AGLCD2F5F5":
                    result = new AGLCD2F5F5();
                    break;
                case "AGLCD3":
                    result = new AGLCD3();
                    break;
                case "AGLSD2":
                    result = new AGLSD2();
                    break;
                case "AGLBK2":
                    result = new AGLBK2();
                    break;
                case "AGLTH2":
                    result = new AGLTH2();
                    break;
                case "ACACNTG":
                    result = new ACACNTG();
                    break;
                case "AGLSD1":
                    result = new AGLSD1();

                    #endregion Báo cáo tiền mặt

                    break;
                case "ACAKU2N":
                    result = new ACAKU2N();
                    break;
                case "ACAKU1":
                    result = new ACAKU1();
                    break;
                case "ACAKU2":
                    result = new ACAKU2();

                    #endregion Tiền mặt

                    #region ==== Tồn kho ====

                    #region === Báo cáo hàng tồn kho ===

                    break;
                case "AINCD1":
                    result = new AINCD1();
                    break;
                case "AINCD2":
                    result = new AINCD2();
                    break;
                case "AINCD1F5":
                    result = new AINCD1F5();
                    break;
                case "AINSD1":
                    result = new AINSD1();
                    break;
                case "AINSD2DK":
                    result = new AINSD2DK();
                    break;
                case "AINSD2CK":
                    result = new AINSD2CK();
                    break;
                case "AINSD3":
                    result = new AINSD3();
                    break;
                case "AINSO1":
                    result = new AINSO1();
                    break;
                case "AINSD2SLCK":
                    result = new AINSD2SLCK();
                    break;
                case "AINSD2SLDK":
                    result = new AINSD2SLDK();
                    break;
                case "AKBKVT":
                    result = new AKBKVT();
                    break;
                case "AINCD1SL":
                    result = new AINCD1SL();
                    break;
                case "AINCD1SLF5":
                    result = new AINCD1SLF5();
                    break;
                case "AKBKVTSL":
                    result = new AKBKVTSL();
                    break;
                case "AINSO1T":
                    result = new AINSO1T();
                    break;
                case "AINSO1TF5":
                    result = new AINSO1TF5();
                    break;
                case "AINSO1TF10":
                    result = new AINSO1TF10();
                    break;
                case "AINSO3T":
                    result = new AINSO3T();
                    break;
                case "AINSO3TF5":
                    result = new AINSO3TF5();
                    break;
                case "AINSO3TF10":
                    result = new AINSO3TF10();
                    break;
                case "AINGIATB1":
                    result = new AINGIATB1();
                    break;
                case "AINSD4":
                    result = new AINSD4();

                    #endregion Báo cáo hàng tồn kho

                    #region === Báo cáo quản trị tồn kho ===

                    break;
                case "AINCDHSDC":
                    result = new AINCDHSDC();
                    break;
                case "AINCDHSDD":
                    result = new AINCDHSDD();
                    break;
                case "AVIN005":
                    result = new AVIN005();
                    break;
                case "AVIN005A":
                    result = new AVIN005A();
                    break;
                case "AVINVTLC":
                    result = new AVINVTLC();
                    break;
                case "AINLOSO1":
                    result = new AINLOSO1();
                    break;
                case "AINCDHSDE":
                    result = new AINCDHSDE();
                    break;
                case "AINCDHSDF":
                    result = new AINCDHSDF();
                    break;
                case "AINCDHSDG":
                    result = new AINCDHSDG();
                    break;
                case "AINCDHSDGF5":
                    result = new AINCDHSDGF5();
                    break;
                case "AINCDVITRIA":
                    result = new AINCDVITRIA();
                    break;
                case "AINCDVITRIAF5":
                    result = new AINCDVITRIAF5();
                    break;
                case "AINCDHSDB":
                    result = new AINCDHSDB();
                    break;
                case "AINCDHSDA":
                    result = new AINCDHSDA();
                    break;
                case "AINCDHSDAF5":
                    result = new AINCDHSDAF5();
                    break;
                case "AINCDHSDEF5":
                    result = new AINCDHSDEF5();
                    break;
                case "AINSO3":
                    result = new AINSO3();
                    break;
                case "AINCDHSDG_NHOM":
                    result = new AINCDHSDG_NHOM();
                    break;
                case "AINCDHSDG_NHOMF5":
                    result = new AINCDHSDG_NHOMF5();

                    #endregion Báo cáo quản trị tồn kho

                    #region ==== Báo cáo hàng xuất kho ====

                    break;
                case "AINTH1X":
                    result = new AINTH1X();
                    break;
                case "AINBK12":
                    result = new AINBK12();
                    break;
                case "AINBK1B":
                    result = new AINBK1B();
                    break;
                case "AINTH1XF5":
                    result = new AINTH1XF5();
                    break;
                case "AINTH1X_LO":
                    result = new AINTH1X_LO();
                    break;
                case "AINTH1X_LOF5":
                    result = new AINTH1X_LOF5();
                    break;
                case "AINBK3XKH":
                    result = new AINBK3XKH();
                    break;
                case "AINBK3XVV":
                    result = new AINBK3XVV();
                    break;
                case "AINBK3XNX":
                    result = new AINBK3XNX();
                    break;
                case "AINBK3XVT":
                    result = new AINBK3XVT();
                    break;
                case "AINBK2X":
                    result = new AINBK2X();

                    #endregion Báo cáo hàng xuất kho

                    #region ==== Báo cáo hàng nhập kho ====

                    break;
                case "AINTH1":
                    result = new AINTH1();
                    break;
                case "AINTH1F5":
                    result = new AINTH1F5();
                    break;
                case "AINBK1A":
                    result = new AINBK1A();
                    break;
                case "AINTH1_LO":
                    result = new AINTH1_LO();
                    break;
                case "AINTH1_LOF5":
                    result = new AINTH1_LOF5();

                    #endregion Báo cáo hàng nhập kho

                    #region ==== Báo cáo quản trị hàng nhập kho ====

                    break;
                case "AINTH2":
                    result = new AINTH2();
                    break;
                case "AINTH2F5":
                    result = new AINTH2F5();
                    break;
                case "AINTH3":
                    result = new AINTH3();
                    break;
                case "AINTH3F5":
                    result = new AINTH3F5();

                    #endregion Báo cáo quản trị hàng nhập kho

                    #region ==== Báo cáo quản trị hàng xuất kho ====

                    break;
                case "AINTH2X":
                    result = new AINTH2X();
                    break;
                case "AINTH3X":
                    result = new AINTH3X();
                    break;
                case "AINTH2XF5":
                    result = new AINTH2XF5();
                    break;
                case "AINTH3XF5":
                    result = new AINTH3XF5();

                    #endregion Báo cáo quản trị hàng xuất kho

                    break;
                case "AINGIA_TB":
                    result = new AINGIA_TB();
                    break;
                case "HPAYROLLCALC":
                    result = new HPAYROLLCALC();
                    break;
                case "AINGIA_TBDD":
                    result = new AINGIA_TBDD();
                    break;
                case "AINGIA_NTXT":
                    result = new AINGIA_NTXT();
                    break;
                case "AINABVT2N":
                    result = new AINABVT2N();
                    break;
                case "AINABLO2N":
                    result = new AINABLO2N();
                    break;
                case "AINABVITRI2N":
                    result = new AINABVITRI2N();

                    #endregion Tồn kho

                    #region === Báo cáo mua hàng   ===

                    break;
                case "APOBK1":
                    result = new APOBK1();
                    break;
                case "APOBK2":
                    result = new APOBK2();
                    break;
                case "APOTH1":
                    result = new APOTH1();
                    break;
                case "APOTH1F5":
                    result = new APOTH1F5();
                    break;
                case "AGLSO3M":
                    result = new AGLSO3M();
                    break;
                case "APO001":
                    result = new APO001();
                    break;
                case "APO001F5":
                    result = new APO001F5();

                    #endregion Báo cáo mua hàng

                    #region === Báo cáo đơn đặt hàng   ===

                    break;
                case "ASVBK1":
                    result = new ASVBK1();
                    break;
                case "ASVBK2":
                    result = new ASVBK2();
                    break;
                case "ASVBK3":
                    result = new ASVBK3();
                    break;
                case "ASVBK4":
                    result = new ASVBK4();
                    break;
                case "ASVBK3F5":
                    result = new ASVBK3F5();

                    #endregion Báo cáo đơn đặt hàng

                    #region === Báo cáo quản trị mua hàng ===

                    break;
                case "APOTH3":
                    result = new APOTH3();
                    break;
                case "APOTH3F5":
                    result = new APOTH3F5();
                    break;
                case "APOTH2":
                    result = new APOTH2();
                    break;
                case "APOTH2F5":
                    result = new APOTH2F5();

                    #endregion Báo cáo quản trị mua hàng

                    #region ==== Tài sản cố định ====

                    break;
                case "AFATINHKH":
                    result = new AFATINHKH();
                    break;
                case "AFABCKH":
                    result = new AFABCKH();
                    break;
                case "AFABCTS":
                    result = new AFABCTS();
                    break;
                case "AFABCTANG":
                    result = new AFABCTANG();
                    break;
                case "AFABCTANGBP":
                    result = new AFABCTANGBP();
                    break;
                case "AFABCTANGNVBP":
                    result = new AFABCTANGNVBP();
                    break;
                case "AFABCGIAM":
                    result = new AFABCGIAM();
                    break;
                case "AFASUAKH":
                    result = new AFASUAKH();
                    break;
                case "AFAPBTS":
                    result = new AFAPBTS();

                    break;
                case "AFABTPBTSN":
                    result = new AFABTPBTSN();
                    break;
                case "AFABTPBTSN_F7":
                    result = new AFABTPBTSN_F7();
                    break;
                case "AFABCTSTH5":
                    result = new AFABCTSTH5();
                    break;
                case "AFABCTSTH6":
                    result = new AFABCTSTH6();
                    break;
                case "AFABCNV":
                    result = new AFABCNV();
                    break;
                case "AFATANGNG":
                    result = new AFATANGNG();
                    break;
                case "AFABCGIAMBP":
                    result = new AFABCGIAMBP();
                    break;
                case "AFAGIAMNG":
                    result = new AFAGIAMNG();
                    break;
                case "AFADCBP":
                    result = new AFADCBP();
                    break;
                case "AFADCCTBP":
                    result = new AFADCCTBP();
                    break;
                case "AFAPBTSBP":
                    result = new AFAPBTSBP();
                    break;
                case "AFATHETS":
                    result = new AFATHETS();
                    break;
                case "AFABCTGTH1":
                    result = new AFABCTGTH1();
                    break;
                case "AFABCTGTH2":
                    result = new AFABCTGTH2();
                    break;
                case "AFABCKHTH1":
                    result = new AFABCKHTH1();
                    break;
                case "AFABCTSNV":
                    result = new AFABCTSNV();
                    break;
                case "AFABCTSBP":
                    result = new AFABCTSBP();
                    break;
                case "AFABCTANGNV":
                    result = new AFABCTANGNV();
                    break;
                case "AFABCTSNVBP":
                    result = new AFABCTSNVBP();

                    #region BC khấu hao tscd

                    break;
                case "AFABCKHBP":
                    result = new AFABCKHBP();
                    break;
                case "AFABCKHNV":
                    result = new AFABCKHNV();

                    #endregion BC khấu hao tscd

                    #endregion Tài sản cố định

                    #region ==== Công cụ , dụng cụ ====

                    break;
                case "ATOTINHPB":
                    result = new ATOTINHPB();
                    break;
                case "ATOBCPB":
                    result = new ATOBCPB();
                    break;
                case "ATOBCCC":
                    result = new ATOBCCC();
                    break;
                case "ATOBCTANG":
                    result = new ATOBCTANG();
                    break;
                case "ATOBCGIAM":
                    result = new ATOBCGIAM();
                    break;
                case "ATOSUAPB":
                    result = new ATOSUAPB_Filter();
                    break;
                case "ACOSXLT_COTHE_GTSP":
                    result = new ACOSXLT_COTHE_GTSP();
                    break;
                case "ACOSXLT_COBANG_GTSPA":
                    result = new ACOSXLT_COBANG_GTSPA();
                    break;
                case "ACOSXLT_COBANG_GTSPB":
                    result = new ACOSXLT_COBANG_GTSPB();
                    break;
                case "ACOSXLT_COBANG_GTSP":
                    result = new ACOSXLT_COBANG_GTSP();
                    break;
                case "ATOBTPBCCN":
                    result = new ATOBTPBCCN();
                    break;
                case "ATOBTPBCCN_F7":
                    result = new ATOBTPBCCN_F7();
                    break;
                case "ATOTANGNG":
                    result = new ATOTANGNG();
                    break;
                case "ATODCBP":
                    result = new ATODCBP();
                    break;
                case "ATODCCTBP":
                    result = new ATODCCTBP();
                    break;
                case "ATOBCCCNV":
                    result = new ATOBCCCNV();
                    break;
                case "ATOBCCCBP":
                    result = new ATOBCCCBP();
                    break;
                case "ATOBCGIAMBP":
                    result = new ATOBCGIAMBP();
                    break;
                case "ATOBCTANGNV":
                    result = new ATOBCTANGNV();
                    break;
                case "ATOBCTANGBP":
                    result = new ATOBCTANGBP();
                    break;
                case "ATOBCTGTH1":
                    result = new ATOBCTGTH1();
                    break;
                case "ATOBCTGTH2":
                    result = new ATOBCTGTH2();
                    break;
                case "ATOBCPBBP":
                    result = new ATOBCPBBP();
                    break;
                case "ATOBCPBNV":
                    result = new ATOBCPBNV();
                    break;
                case "ATOPBVCC":
                    result = new ATOPBVCC();
                    break;
                case "ATOBCNV":
                    result = new ATOBCNV();
                    break;
                case "ATOBCCCTH5":
                    result = new ATOBCCCTH5();
                    break;
                case "ATOBCCCTH6":
                    result = new ATOBCCCTH6();
                    break;
                case "ATOBCCCTH2":
                    result = new ATOBCCCTH2();
                    break;
                case "ATOBCCCTH1":
                    result = new ATOBCCCTH1();
                    break;
                case "ATOBCPBTH1":
                    result = new ATOBCPBTH1();
                    break;
                case "ATOPBVCCBP":
                    result = new ATOPBVCCBP();

                    #endregion Công cụ , dụng cụ

                    #region ==== Hệ thống ====

                    break;
                case "AAPPR_SOA": //Hệ thống/Quản lý người sử dụng/C.Duyệt, xử lý hóa đơn bán hàng.
                    result = new AAPPR_SOA();
                    break;
                case "AAPPR_SOA2": //Hệ thống/Quản lý người sử dụng/K.Chuyển sang hóa đơn điện tử.
                    result = new AAPPR_SOA2();
                    break;
                case "AAPPR_SOA3": //Hệ thống/Quản lý người sử dụng/ In hóa đơn điện tử liên tục.
                    result = new AAPPR_SOA3_filter();
                    break;
                case "AGLAUTOSO_CT":
                    result = new XAGLAUTOSO_CT();
                    break;
                case "AGLAUTO_GL1":
                    result = new XAGLAUTO_GL1();
                    break;
                case "XLSALKH":
                    result = new XLSALKH();
                    break;
                case "XLSALVT":
                    result = new XLSALVT();
                    break;
                case "XLSPOA":
                    result = new XLSPOA_Filter();
                    break;
                case "XLSIND":
                    result = new XLSIND_Filter();
                    break;
                case "XLSIXA":
                    result = new XLSIXA_Filter();
                    break;
                case "XLSSOA":
                    result = new XLSSOA_Filter();

                    break;
                case "XLSSOH2":
                    result = new XLSSOH2_Filter();
                    break;
                case "XLSTA1":
                    result = new XLSTA1_Filter();
                    break;
                case "XLSCA1":
                    result = new XLSCA1_Filter();
                    //NHAN SU
                    break;
                case "XLSPRCONG2":
                    result = new XLSPRCONG2();
                    break;
                case "XLSHRPERSONAL":
                    result = new XLSHRPERSONAL();
                    break;
                case "XLSHRGENERAL2":
                    result = new XLSHRGENERAL2();
                    break;
                case "AAPPR_SOA1":
                    result = new AAPPR_SOA1();
                    break;
                case "AAPPR_SOA4":
                    result = new AAPPR_SOA4();
                    break;
                case "AAPPR_POH1":
                    result = new AAPPR_POH1();
                    break;
                case "AAPPR_INPUT_ALL":
                    result = new AAPPR_INPUT_ALL();
                    break;
                case "AAPPR_SOA_IN1":
                    result = new AAPPR_SOA_IN1();
                    break;
                case "AAPPR_SOC_IN1":
                    result = new AAPPR_SOC_IN1();
                    break;
                case "AAPPR_SOA_IN2":
                    result = new AAPPR_SOA_IN2();
                    break;
                case "AAPPR_SOA_IN3":
                    result = new AAPPR_SOA_IN3();
                    break;
                case "AAPPR_POA":
                    result = new AAPPR_POA();
                    break;
                case "AAPPR_IND":
                    result = new AAPPR_IND();
                    break;
                case "V6BACKUP1":
                    result = new V6BACKUP1();
                    break;
                case "V6LEVELDOWN":
                    result = new V6LEVELDOWN();
                    break;
                case "V6LEVELSET":
                    result = new V6LEVELSET();
                    break;
                case "AAPPR_XULY_SOA":
                    result = new AAPPR_XULY_SOA();
                    break;
                case "AAPPR_XULY_ALL":
                    result = new AAPPR_XULY_ALL_Filter();

                    #endregion he thong

                    #region ==== Tổng hợp ====

                    #region ---- Cập nhật số liệu ----

                    break;
                case "AGLCTKC":
                    result = new AGLCTKC();
                    break;
                case "AGLCTPB":
                    result = new AGLCTPB();
                    break;
                case "AGLTHUE30":
                    result = new AGLTHUE30();
                    break;
                case "AGLTHUE20":
                    result = new AGLTHUE20();
                    break;
                case "AGLABTK2N": //Ket chuyen tk
                    result = new AGLABTK2N();

                    #endregion Cập nhật số liệu

                    #region ---- Các bảng kê báo cáo tài khoản ----

                    break;
                case "AGLBK1":
                    result = new AGLBK1();
                    break;
                case "AGLAUTO":
                    result = new AGLAUTO();

                    #endregion Các bảng kê báo cáo tài khoản 

                    #region ---- Báo cáo tài chính ----

                    break;
                case "AGLCD1":
                    result = new AGLCD1();

                    break;
                case "AGLCD1F5":
                    result = new AGLCD1F5();
                    break;
                case "AGLCD1F5F5":
                    result = new AGLCD1F5F5();
                    break;
                case "AGLTCB":
                    result = new AGLTCB();
                    break;
                case "AGLTCC":
                    result = new AGLTCC();
                    break;
                case "AGLTMXLS":
                    result = new AGLTMXLS();
                    break;
                case "AGLTCD":
                    result = new AGLTCD();
                    break;
                case "AGLTCA":
                    result = new AGLTCA();

                    #endregion Báo cáo tài chính

                    #region ----  sổ theo HT nhật ký chung ----

                    break;
                case "AGLSNKT":
                    result = new AGLSNKT();
                    break;
                case "AGLSNKC":
                    result = new AGLSNKC();
                    break;
                case "AGLSNKBH":
                    result = new AGLSNKBH();
                    break;
                case "AGLSO2":
                    result = new AGLSO2();
                    break;
                case "AGLTH1T":
                    result = new AGLTH1T();
                    break;
                case "AGLTH1TF5":
                    result = new AGLTH1TF5();
                    break;
                case "AGLTH1TF10":
                    result = new AGLTH1TF10();

                    break;
                case "AGLSNKMH":
                    result = new AGLSNKMH();

                    #endregion  sổ theo HT nhật ký chung

                    break;
                case "AGLTHUEBK2TT156":
                    result = new AGLTHUEBK2TT156_Filter();
                    break;
                case "AGLTHUEBK3TT156":
                    result = new AGLTHUEBK3TT156();
                    break;
                case "AGLTHUEBK4":
                    result = new AGLTHUEBK4();


                    #region ---- CTGS ----

                    break;
                case "AGSCTGS01":
                    result = new AGSCTGS01();
                    break;
                case "AGSCTGS02":
                    result = new AGSCTGS02();

                    #endregion ctgs

                    #region ---- Báo cáo thuế ----

                    break;
                case "AGLQTTTNTT156":
                    result = new AGLQTTTNTT156();

                    #endregion ------ báo cáo thuế -------

                    #region ---- Báo cáo tài chính QĐ 15----

                    #endregion ------ Báo cáo tài chính QĐ 15 -------

                    #endregion Tổng hợp

                    #region ==== Chi phí giá thành ====

                    break;
                case "ACPDDCK":
                    result = new ACPDDCK(); // Giá thành sản phẩm
                    break;
                case "ACOABVV2N":
                    result = new ACOABVV2N(); // Kết chuyển vv
                    break;
                case "AARHDKH2N":
                    result = new AARHDKH2N(); // Kết chuyển hop dong - khach hang
                    break;
                case "ACOTKTH4":
                    result = new ACOTKTH4();

                    #region ==== Báo cáo theo vu việc ====

                    break;
                case "ACOVVBK2":
                    result = new ACOVVBK2();
                    //result = new ACOVVBK2(program);
                    break;
                case "ACOVVTH4":
                    result = new ACOVVTH4();

                    break;
                case "ACOVVTH1":
                    result = new ACOVVTH1();
                    break;
                case "ACOVVTH2":
                    result = new ACOVVTH2();
                    break;
                case "ACOVVTH1F5":
                    result = new ACOVVTH1F5();
                    break;
                case "ACOVVTH2F5":
                    result = new ACOVVTH2F5();

                    #endregion Báo cáo theo vu việc

                    #region ==== Báo cáo chi phí theo tài khoản ====

                    break;
                case "ACOTKTH3BC_N4":
                    result = new ACOTKTH3BC_N4();
                    break;
                case "ACOTKTH3BC_N1":
                    result = new ACOTKTH3BC_N1();
                    break;
                case "ACOTKTH3BC_N2":
                    result = new ACOTKTH3BC_N2();
                    break;
                case "ACOTKTH3BC_N3":
                    result = new ACOTKTH3BC_N3();
                    break;
                case "ACOTKTH2":
                    result = new ACOTKTH2();
                    break;
                case "AGLBK2BT":
                    result = new AGLBK2BT();

                    #endregion Báo cáo chi phí theo tài khoản

                    #region ==== Báo cáo phân tích chi phí ====

                    break;
                case "ACOPHIBK1":
                    result = new ACOPHIBK1();
                    break;
                case "ACOPHITH1":
                    result = new ACOPHITH1();
                    break;
                case "ACOPHITH1F5":
                    result = new ACOPHITH1F5();
                    break;
                case "ACOPHITH2":
                    result = new ACOPHITH2();
                    break;
                case "ACOPHITH2F5":
                    result = new ACOPHITH2F5();
                    break;
                case "ACOPHIBK2":
                    result = new ACOPHIBK2();
                    break;
                case "ACOTKBK2":
                    result = new ACOTKBK2();

                    #endregion ==== Báo cáo phân tích chi phí ====

                    #region ==== Giá thành sản phẩm liên tục ====

                    break;
                case "ACOLSXTH2":
                    result = new ACOLSXTH2();
                    break;
                case "ACOLSXTH1":
                    result = new ACOLSXTH1();

                    #endregion Giá thành sản phẩm liên tục  ====

                    #endregion Chi phí giá thành

                    #region ==== Danh mục ====

                    break;
                case "AARKH":
                    //result = new FilterDanhMuc("ALKH");
                    result = new AARKHFilterDanhMuc();
                    break;
                case "AINVT":
                    result = new FilterDanhMuc("ALVT");
                    break;
                case "AINLO":
                    result = new FilterDanhMuc("ALLO");
                    break;
                case "AINKHO":
                    result = new FilterDanhMuc("ALKHO");
                    break;
                case "ACATKNH":
                    result = new FilterDanhMuc("ALTKNH");
                    break;
                case "AARBP":
                    result = new FilterDanhMuc("ALBP");
                    break;
                case "AGLTK0":
                    result = new FilterDanhMuc("ALTK0");
                    break;
                case "AARNHKH":
                    result = new FilterDanhMuc("ALNHKH");
                    break;
                case "AARTHUE":
                    result = new FilterDanhMuc("ALTHUE");
                    break;
                case "AINNHVT":
                    result = new FilterDanhMuc("ALNHVT");
                    break;
                case "ACOVV":
                    result = new FilterDanhMuc("ALVV");
                    break;
                case "ACONHVV":
                    result = new FilterDanhMuc("ALNHVV");
                    break;
                case "AFANV":
                    result = new FilterDanhMuc("ALNV");
                    break;
                case "AFATGTS":
                    result = new FilterDanhMuc("ALTGTS");
                    break;
                case "AFABP":
                    result = new FilterDanhMuc("ALBP");
                    break;
                case "AFANHTS":
                    result = new FilterDanhMuc("ALNHTS");
                    break;
                case "ATOTGCC":
                    result = new FilterDanhMuc("ALTGCC");
                    break;
                case "ATOBP":
                    result = new FilterDanhMuc("ALBP");
                    break;
                case "ATONHCC":
                    result = new FilterDanhMuc("ALNHCC");
                    break;
                case "AGLDVCS":
                    result = new FilterDanhMuc("ALDVCS");
                    break;
                case "AGLTK2":
                    result = new FilterDanhMuc("ALTK2");
                    break;
                case "AGLDMKC":
                    result = new FilterDanhMuc("ALKC");
                    break;
                case "AGLDMPB":
                    result = new FilterDanhMuc("ALPB");
                    break;
                case "AGLNHTK":
                    result = new FilterDanhMuc("ALNHTK");
                    break;
                case "AGLNHTK0":
                    result = new FilterDanhMuc("ALNHTK0");
                    break;
                case "V6CT":
                    result = new FilterDanhMuc("ALCT");
                    break;
                case "V6OPTION":
                    result = new FilterDanhMuc("V6OPTION");
                    break;
                case "V6TGNT":
                    result = new FilterDanhMuc("ALTGNT");
                    break;
                case "V6NT":
                    result = new FilterDanhMuc("ALNT");

                    #endregion danh muc

                    #region ==== Kho - Vị trí ====

                    break;
                case "AINVITRI01":
                    result = new AINVITRI01_filter();
                    break;
                case "AINVITRI02":
                    result = new AINVITRI02_filter();
                    break;
                case "AINVITRI03AF7":
                    break;
                case "AINVITRI03BF7":
                    result = new AINVITRI03AF7_filter();

                    #endregion kho-vitri

                    #region ==== In liên tục ====

                    break;
                case "AAPPR_SOF_IN1":
                    result = new AAPPR_SOF_IN1();
                    break;
                case "AAPPR_TA1_IN1":
                    result = new AAPPR_TA1_IN1();
                    break;
                case "AAPPR_CA1_IN1":
                    result = new AAPPR_CA1_IN1();
                    break;
                case "AAPPR_BC1_IN1":
                    result = new AAPPR_BC1_IN1();
                    break;
                case "AAPPR_BN1_IN1":
                    result = new AAPPR_BN1_IN1();
                    break;
                case "AAPPR_POA_IN1":
                    result = new AAPPR_POA_IN1();
                    break;
                case "AAPPR_POB_IN1":
                    result = new AAPPR_POB_IN1();
                    break;
                case "AAPPR_IXC_IN1":
                    result = new AAPPR_IXC_IN1();
                    break;
                case "AAPPR_IND_IN1":
                    result = new AAPPR_IND_IN1();
                    break;
                case "AAPPR_IXA_IN1":
                    result = new AAPPR_IXA_IN1();
                    break;
                case "AAPPR_IXB_IN1":
                    result = new AAPPR_IXB_IN1();
                    break;

                case "AAPPR_EINVOICE1":
                    result = new AAPPR_EINVOICE1_Filter();
                    break;
                    #endregion In liên tục

                    
                case "AINVTBAR1":
                    result = new FilterDanhMuc("ALVT");
                    break;
                case "AINVTBAR2":
                    result = new FilterDanhMuc("ALLO");
                    break;
                case "AINVTBAR2F9":
                    result = new AINVTBAR2F9();
                    break;
                case "AINVTBAR3":
                    result = new AINVTBAR3();
                    break;
                case "AINVTBAR5":
                    result = new AINVTBAR5();

                    break;
                case "ACOSXLT_TINHGIA":
                    result = new ZACOSXLT_TINHGIA_Filter();

                    break;
                case "AMAP01":
                    result = new AMAP01Filter();
                    break;
                case "AMAP01A":
                    result = new AMAP01AFilter();
                    break;
                case "AMAP01B":
                    result = new AMAP01BFilter();
                    break;
                case "AMAP01C":
                    result = new AMAP01CFilter();
                    break;
                case "AMAP01D":
                    result = new AMAP01DFilter();
                    break;
                case "AMAP01E":
                    result = new AMAP01EFilter();
                    break;
                case "AMAP01F":
                    result = new AMAP01FFilter();
                    break;
                case "AMAP01G":
                    result = new AMAP01GFilter();
                    break;
                case "AMAP01H":
                    result = new AMAP01HFilter();
                    break;
                case "AMAP01I":
                    result = new AMAP01IFilter();
                    break;
                case "AMAP01K":
                    result = new AMAP01KFilter();
                    break;
                case "AVGLGSSO5A":
                    result = new AVGLGSSO5A();
                    break;
                case "AVGLGSSO5AF5":
                    result = new AVGLGSSO5AF5();
                    break;
                case "AGLGSSO4T":
                    result = new AGLGSSO4T();
                    break;
                case "AGLGSSO4TF5":
                    result = new AGLGSSO4TF5();
                    break;
                case "AGLGSSO4TF10":
                    result = new AGLGSSO4TF10();
                    break;
                case "AVGLGSSO6A":
                    result = new AVGLGSSO6A();
                    break;
                case "AVGLGSSO6AF5":
                    result = new AVGLGSSO6AF5();

                    break;
                case "ASENDMAIL":
                    result = new Sms.XASENDMAIL();
                    break;
                case "ASENDSMS":
                    result = new Sms.XASENDSMS();
                    break;
            }

            if (result == null) result = new FilterBase() { Visible = false };

            result.MyInitDynamic(program);

            return result;
        }

        public static ReportFilter44Base GetFilterControl44(string program)
        {
            return new ReportFilter44Base(program);
        }
    }
}

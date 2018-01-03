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
            switch (program)
            {
                #region ==== Nhân sự ====
                case "HPRCONGCT": return new HPRCONGCT_Filter();
                case "ERLISTNV": return new ERLISTNV_Filter();
                #endregion nhân sự

                #region ==== Phải thu ====
                #region === Báo cáo bán hàng ===
                case "ASOTH1"://Báo cáo bán hàng chi tiết theo mặt hàng
                              // return new BaoCaoBanHangTheoChiTietMatHangFilter();
                    return new ASOTH1();
                case "ASOTH1F5"://Báo cáo bán hàng chi tiết theo mặt hàng
                    return new ASOTH1F5();
                case "ASOBK1"://Bản kê hóa đơn bán hàng
                    return new ASOBK1();
                case "ASOBK2"://Bản kê hóa đơn 1 mặt hàng
                    return new ASOBK2();
                case "VVP007"://Báo cáo bán hàng chi tiết (trừ trả lại)
                    return new FilterBase();
                case "VVP008"://Báo cáo đăng ký
                    return new FilterBase();
                case "AHINTH3"://Báo cáo hàng xuất tặng 2 chỉ tiêu
                    return new AHINTH3();
                case "AKSOTH1"://Báo cáo tổng hợp theo tháng, nhớm theo v
                    return new AKSOTH1();
                case "ASOBK14":
                    return new ASOBK14();
                case "AINBK11"://Báo cáo hàng nhập
                    return new AINBK11();
                case "ASOBK11"://So sánh giá bán, giá nhập
                    return new FilterBase();
                case "ASOBK12"://Báo cáo hàng chi tiết
                    return new ASOBK12();
                case "ASOBK13"://So sánh giá bán, bảng giá bán
                    return new FilterBase();
                case "AKSOTH3"://Báo cáo tổng hợp theo bộ phận
                    return new FilterBase();
                case "AHINTH3F5"://Báo cáo hàng xuất tặng 2 chỉ tiêu
                    return new AHINTH3F5();
                case "AVSO003":
                    return new AVSO003();
                case "ASOBK3":
                    return new ASOBK3();
                case "ASOBK3F5":
                    return new ASOBK3F5();
                case "AHINTH1":
                    return new AHINTH1();
                case "AKSOTH2":
                    return new AKSOTH2();
                case "AKSOTH2F5":
                    return new AKSOTH2F5();
                case "AFABCTSTH1":
                    return new AFABCTSTH1();
                case "AFABCTSTH2":
                    return new AFABCTSTH2();

                #endregion báo cáo bán hàng
                #region ==== Cập nhật số dư ====
                case "ARSD0_AR0":
                    return new ARSD0_AR0();
                case "AARBPKH2N":
                    return new AARBPKH2N();
                case "ACOVVKH2N":
                    return new ACOVVKH2N();
                #endregion Cập nhật số dư
                #region === Báo cáo quản trị bán hàng ===
                case "ASOBK3VV"://Bản kê hóa đơn nhóm theo vụ việc
                    return new FilterBase();
                case "ASOTH2"://Báo cáo ds bán hàng theo kh vv nv...
                    return new ASOTH2();
                case "ASOTH3"://Báo cáo 
                    return new ASOTH3();
                case "ASOTH3F5":
                    return new ASOTH3F5();
                case "ASOTH4":
                    return new ASOTH4();
                case "ASOTH5":
                    return new ASOTH5();
                case "ASOTH2F5":
                    return new ASOTH2F5();
                case "ASOTH6":
                    return new ASOTH6();
                case "ASOTH6F5":
                    return new ASOTH6F5();
                case "ASOTH7":
                    return new ASOTH7();
                case "ASOTH8":
                    return new ASOTH8();

                #endregion báo cáo quản trị bán hàng
                #region === Báo cáo quản trị bán hàng 1 ===
                case "AVSO001"://Bản kê hóa đơn bán hàng th
                    return new AVSO001();
                case "AVSO002"://Bản kê hóa đơn bán hàng tra lai
                    return new AVSO002();
                case "AVSO002F5"://Bản kê hóa đơn bán hàng tra lai
                    return new AVSO002F5();
                case "AVSO004":
                    return new AVSO004();
                case "AVSO006":
                    return new AVSO006();
                case "AVBCNH70A":
                    return new AVBCNH70A();
                case "AKSOTH5":
                    return new AKSOTH5();
                case "AKSOTH6":
                    return new AKSOTH6();
                #endregion báo cáo quản trị bán hàng 1
                #region === Báo cáo công nợ khách hàng ===
                case "AARSO1"://Sổ chi tiết công nợ của một khách hàng                                              
                    return new AARSO1();
                case "AARCD1"://Cân đối 1 tài khoản
                    return new AARCD1();
                case "AARCD1F5"://Cân đối 1 tài khoản
                    return new AARCD1F5();
                case "AARCD2"://Cân đối nhiều tài khoản
                    return new AARCD2();
                case "AARCD2F5":
                    return new AARCD2F5();

                case "AGLSO1"://Sổ chi tiết 1 tài khoản
                    return new AGLSO1();
                case "AARSD1DK":
                    return new AARSD1DK();
                case "AARSD1CK":
                    return new AARSD1CK();
                case "AARSD2":
                    return new AARSD2();
                case "AARTH1":
                    return new AARTH1();
                case "AARTH1F5":
                    return new AARTH1F5();
                case "AARSO1T":
                    return new AARSO1T();
                case "AARSO1TF5":
                    return new AARSO1TF5();
                case "AARSO1TF10":
                    return new AARSO1TF10();
                #endregion Báo cáo công nợ khách hàng
                #region === Báo cáo quản trị hóa đơn   ===
                case "AARTTBK1":
                    return new AARTTBK1();
                case "AARTTBK2":
                    return new AARTTBK2();
                case "AARTTBK3":
                    return new AARTTBK3();
                case "AARTTBK4":
                    return new FilterBase();
                case "AARTTBK1A":
                    return new AARTTBK1A();
                case "AARTTBK1B":
                    return new AARTTBK1B();
                case "AARTTV001":
                    return new AARTTV001();
                case "ATSOBK1T":
                    return new ATSOBK1T();
                case "ASOBK4":
                    return new ASOBK4();
                case "AARTTBK8":
                    return new AARTTBK8();
                case "AARTTBK7":
                    return new AARTTBK7();
                #endregion Báo cáo quản trị hóa đơn
                #region ==== Cập nhật số liệu ====
                case "ARSD_AR":
                    return new ARSD_AR();
                case "ARCMO_AR":
                    return new ARCMO_AR();
                case "ARCMO_ARF9":
                    return new ARCMO_ARF9();

                case "APDMO_AP":
                    return new APDMO_AP();
                case "APDMO_APF9":
                    return new APDMO_APF9();
                #endregion Cập nhật số liệu
                #region === In danh mục từ điển ===
                case "ASOBCGIA2":
                    return new ASOBCGIA2();
                #endregion === In danh mục từ điển ===
                #region === BC khách hàng - vụ việc ===
                case "ACOVVKHCD3":
                    return new ACOVVKHCD3();
                case "ACOVVKHBK4":
                    return new ACOVVKHBK4();
                case "ACOVVSD1DK":
                    return new ACOVVSD1DK();
                case "ACOVVSD1CK":
                    return new ACOVVSD1CK();
                #endregion === BC khách hàng - vụ việc ===
                #endregion phải thu

                #region ==== Phải trả ====
                #region ==== Cập nhật số dư ====
                case "ARSD0_AP0":
                    return new ARSD0_AP0();
                #endregion Cập nhật số dư
                #region ==== Cập nhật số liệu ====
                case "ARSD_AP":
                    return new ARSD_AP();
                #endregion Cập nhật số liệu

                #region === Báo cáo quản trị hóa đơn   ===
                case "AAPTTBK1":
                    return new AAPTTBK1();
                case "AAPTTBK2":
                    return new AAPTTBK2();
                case "AAPTTBK3":
                    return new AAPTTBK3();
                //case "AAPTTBK4":
                //    return new FilterBase();
                //case "AAPTTBK1A":
                //    return new AARTTBK1A();
                //case "AAPTTBK1B":
                //    return new AARTTBK1B();
                #endregion Báo cáo quản trị hóa đơn

                #region === Báo cáo công nợ phải trả   ===
                case "AAPSO1T":
                    return new AAPSO1T();
                case "AAPSO1TF5":
                    return new AAPSO1TF5();
                case "AAPSO1TF10":
                    return new AAPSO1TF10();
                #endregion === Báo cáo công nợ phải trả   ===
                #region === Báo cáo quản trị mua hàng  ===
                case "APOBK3KH":
                    return new APOBK3KH();
                case "APOBK3NX":
                    return new APOBK3NX();
                case "APOBK3VV":
                    return new APOBK3VV();
                case "APOBK4":
                    return new APOBK4();
                #endregion === Báo cáo quản trị mua hàng   ===

                #endregion Phải trả

                #region ==== Tiền mặt ====
                #region === Báo cáo tiền mặt ===

                case "AGLSO1A":
                    return new AGLSO1A();
                case "AGLSO1B":
                    return new AGLSO1B();
                case "AGLSO1T":
                    return new AGLSO1T();
                case "AGLSO1TF5":
                    return new AGLSO1TF5();
                case "AGLSO1TF10":
                    return new AGLSO1TF10();

                case "AGLSO3":
                    return new AGLSO3();
                case "AGLSO3C":
                    return new AGLSO3C();
                case "AGLSO3T":
                    return new AGLSO3T();

                case "AGLSO3B":
                    return new AGLSO3B();
                case "AGLTH1":
                    return new AGLTH1();
                case "AGLTH1F5":
                    return new AGLTH1F5();
                case "AGLCD2":
                    return new AGLCD2();
                case "AGLCD2F5":
                    return new AGLCD2F5();
                case "AGLCD2F5F5":
                    return new AGLCD2F5F5();
                case "AGLCD3":
                    return new AGLCD3();
                case "AGLSD2":
                    return new AGLSD2();
                case "AGLBK2":
                    return new AGLBK2();
                case "AGLTH2":
                    return new AGLTH2();
                case "ACACNTG":
                    return new ACACNTG();
                case "AGLSD1":
                    return new AGLSD1();

                #endregion Báo cáo tiền mặt

                case "ACAKU2N":
                    return new ACAKU2N();
                case "ACAKU1":
                    return new ACAKU1();
                case "ACAKU2":
                    return new ACAKU2();
                #endregion Tiền mặt

                #region ==== Tồn kho ====
                #region === Báo cáo hàng tồn kho ===

                case "AINCD1":
                    return new AINCD1();
                case "AINCD2":
                    return new AINCD2();
                case "AINCD1F5":
                    return new AINCD1F5();
                case "AINSD1":
                    return new AINSD1();
                case "AINSD2DK":
                    return new AINSD2DK();
                case "AINSD2CK":
                    return new AINSD2CK();
                case "AINSD3":
                    return new AINSD3();
                case "AINSO1":
                    return new AINSO1();
                case "AINSD2SLCK":
                    return new AINSD2SLCK();
                case "AINSD2SLDK":
                    return new AINSD2SLDK();
                case "AKBKVT":
                    return new AKBKVT();
                case "AINCD1SL":
                    return new AINCD1SL();
                case "AINCD1SLF5":
                    return new AINCD1SLF5();
                case "AKBKVTSL":
                    return new AKBKVTSL();
                case "AINSO1T":
                    return new AINSO1T();
                case "AINSO1TF5":
                    return new AINSO1TF5();
                case "AINSO1TF10":
                    return new AINSO1TF10();
                case "AINSO3T":
                    return new AINSO3T();
                case "AINSO3TF5":
                    return new AINSO3TF5();
                case "AINSO3TF10":
                    return new AINSO3TF10();
                case "AINGIATB1":
                    return new AINGIATB1();
                case "AINSD4":
                    return new AINSD4();
                #endregion Báo cáo hàng tồn kho
                #region === Báo cáo quản trị tồn kho ===

                case "AINCDHSDC":
                    return new AINCDHSDC();
                case "AINCDHSDD":
                    return new AINCDHSDD();
                case "AVIN005":
                    return new AVIN005();
                case "AVIN005A":
                    return new AVIN005A();
                case "AVINVTLC":
                    return new AVINVTLC();
                case "AINLOSO1":
                    return new AINLOSO1();
                case "AINCDHSDE":
                    return new AINCDHSDE();
                case "AINCDHSDF":
                    return new AINCDHSDF();
                case "AINCDHSDG":
                    return new AINCDHSDG();
                case "AINCDHSDGF5":
                    return new AINCDHSDGF5();
                case "AINCDVITRIA":
                    return new AINCDVITRIA();
                case "AINCDVITRIAF5":
                    return new AINCDVITRIAF5();
                case "AINCDHSDB":
                    return new AINCDHSDB();
                case "AINCDHSDA":
                    return new AINCDHSDA();
                case "AINCDHSDAF5":
                    return new AINCDHSDAF5();
                case "AINCDHSDEF5":
                    return new AINCDHSDEF5();
                case "AINSO3":
                    return new AINSO3();
                case "AINCDHSDG_NHOM":
                    return new AINCDHSDG_NHOM();
                case "AINCDHSDG_NHOMF5":
                    return new AINCDHSDG_NHOMF5();

                #endregion Báo cáo quản trị tồn kho
                #region ==== Báo cáo hàng xuất kho ====
                case "AINTH1X":
                    return new AINTH1X();
                case "AINBK12":
                    return new AINBK12();
                case "AINBK1B":
                    return new AINBK1B();
                case "AINTH1XF5":
                    return new AINTH1XF5();
                case "AINTH1X_LO":
                    return new AINTH1X_LO();
                case "AINTH1X_LOF5":
                    return new AINTH1X_LOF5();
                case "AINBK3XKH":
                    return new AINBK3XKH();
                case "AINBK3XVV":
                    return new AINBK3XVV();
                case "AINBK3XNX":
                    return new AINBK3XNX();
                case "AINBK3XVT":
                    return new AINBK3XVT();
                case "AINBK2X":
                    return new AINBK2X();
                #endregion Báo cáo hàng xuất kho
                #region ==== Báo cáo hàng nhập kho ====
                case "AINTH1":
                    return new AINTH1();
                case "AINTH1F5":
                    return new AINTH1F5();
                case "AINBK1A":
                    return new AINBK1A();
                case "AINTH1_LO":
                    return new AINTH1_LO();
                case "AINTH1_LOF5":
                    return new AINTH1_LOF5();
                #endregion Báo cáo hàng nhập kho
                #region ==== Báo cáo quản trị hàng nhập kho ====
                case "AINTH2":
                    return new AINTH2();
                case "AINTH2F5":
                    return new AINTH2F5();
                case "AINTH3":
                    return new AINTH3();
                case "AINTH3F5":
                    return new AINTH3F5();
                #endregion Báo cáo quản trị hàng nhập kho
                #region ==== Báo cáo quản trị hàng xuất kho ====
                case "AINTH2X":
                    return new AINTH2X();
                case "AINTH3X":
                    return new AINTH3X();
                case "AINTH2XF5":
                    return new AINTH2XF5();
                case "AINTH3XF5":
                    return new AINTH3XF5();
                #endregion Báo cáo quản trị hàng xuất kho
                case "AINGIA_TB":
                    return new AINGIA_TB();
                case "HPAYROLLCALC":
                    return new HPAYROLLCALC();
                case "AINGIA_TBDD":
                    return new AINGIA_TBDD();
                case "AINGIA_NTXT":
                    return new AINGIA_NTXT();
                case "AINABVT2N":
                    return new AINABVT2N();
                case "AINABLO2N":
                    return new AINABLO2N();
                case "AINABVITRI2N":
                    return new AINABVITRI2N();
                #endregion Tồn kho

                #region === Báo cáo mua hàng   ===

                case "APOBK1":
                    return new APOBK1();
                case "APOBK2":
                    return new APOBK2();
                case "APOTH1":
                    return new APOTH1();
                case "APOTH1F5":
                    return new APOTH1F5();
                case "AGLSO3M":
                    return new AGLSO3M();
                case "APO001":
                    return new APO001();
                case "APO001F5":
                    return new APO001F5();
                #endregion Báo cáo mua hàng
                #region === Báo cáo đơn đặt hàng   ===
                case "ASVBK1":
                    return new ASVBK1();
                case "ASVBK2":
                    return new ASVBK2();
                case "ASVBK3":
                    return new ASVBK3();
                case "ASVBK4":
                    return new ASVBK4();
                case "ASVBK3F5":
                    return new ASVBK3F5();
                #endregion Báo cáo đơn đặt hàng
                #region === Báo cáo quản trị mua hàng ===
                case "APOTH3":
                    return new APOTH3();
                case "APOTH3F5":
                    return new APOTH3F5();
                case "APOTH2":
                    return new APOTH2();
                case "APOTH2F5":
                    return new APOTH2F5();
                #endregion Báo cáo quản trị mua hàng
                #region ==== Tài sản cố định ====
                case "AFATINHKH":
                    return new AFATINHKH();
                case "AFABCKH":
                    return new AFABCKH();
                case "AFABCTS":
                    return new AFABCTS();
                case "AFABCTANG":
                    return new AFABCTANG();
                case "AFABCTANGBP":
                    return new AFABCTANGBP();
                case "AFABCTANGNVBP":
                    return new AFABCTANGNVBP();
                case "AFABCGIAM":
                    return new AFABCGIAM();
                case "AFASUAKH":
                    return new AFASUAKH();
                case "AFAPBTS":
                    return new AFAPBTS();

                case "AFABTPBTSN":
                    return new AFABTPBTSN();
                case "AFABTPBTSN_F7":
                    return new AFABTPBTSN_F7();
                case "AFABCTSTH5":
                    return new AFABCTSTH5();
                case "AFABCTSTH6":
                    return new AFABCTSTH6();
                case "AFABCNV":
                    return new AFABCNV();
                case "AFATANGNG":
                    return new AFATANGNG();
                case "AFABCGIAMBP":
                    return new AFABCGIAMBP();
                case "AFAGIAMNG":
                    return new AFAGIAMNG();
                case "AFADCBP":
                    return new AFADCBP();
                case "AFADCCTBP":
                    return new AFADCCTBP();
                case "AFAPBTSBP":
                    return new AFAPBTSBP();
                case "AFATHETS":
                    return new AFATHETS();
                case "AFABCTGTH1":
                    return new AFABCTGTH1();
                case "AFABCTGTH2":
                    return new AFABCTGTH2();
                case "AFABCKHTH1":
                    return new AFABCKHTH1();
                case "AFABCTSNV":
                    return new AFABCTSNV();
                case "AFABCTSBP":
                    return new AFABCTSBP();
                case "AFABCTANGNV":
                    return new AFABCTANGNV();
                case "AFABCTSNVBP":
                    return new AFABCTSNVBP();
                #region BC khấu hao tscd
                case "AFABCKHBP":
                    return new AFABCKHBP();
                case "AFABCKHNV":
                    return new AFABCKHNV();
                #endregion BC khấu hao tscd
                #endregion Tài sản cố định
                #region ==== Công cụ , dụng cụ ====
                case "ATOTINHPB":
                    return new ATOTINHPB();
                case "ATOBCPB":
                    return new ATOBCPB();
                case "ATOBCCC":
                    return new ATOBCCC();
                case "ATOBCTANG":
                    return new ATOBCTANG();
                case "ATOBCGIAM":
                    return new ATOBCGIAM();
                case "ATOSUAPB":
                    return new ATOSUAPB_Filter();
                case "ACOSXLT_COTHE_GTSP":
                    return new ACOSXLT_COTHE_GTSP();
                case "ACOSXLT_COBANG_GTSPA":
                    return new ACOSXLT_COBANG_GTSPA();
                case "ACOSXLT_COBANG_GTSPB":
                    return new ACOSXLT_COBANG_GTSPB();
                case "ACOSXLT_COBANG_GTSP":
                    return new ACOSXLT_COBANG_GTSP();
                case "ATOBTPBCCN":
                    return new ATOBTPBCCN();
                case "ATOBTPBCCN_F7":
                    return new ATOBTPBCCN_F7();
                case "ATOTANGNG":
                    return new ATOTANGNG();
                case "ATODCBP":
                    return new ATODCBP();
                case "ATODCCTBP":
                    return new ATODCCTBP();
                case "ATOBCCCNV":
                    return new ATOBCCCNV();
                case "ATOBCCCBP":
                    return new ATOBCCCBP();
                case "ATOBCGIAMBP":
                    return new ATOBCGIAMBP();
                case "ATOBCTANGNV":
                    return new ATOBCTANGNV();
                case "ATOBCTANGBP":
                    return new ATOBCTANGBP();
                case "ATOBCTGTH1":
                    return new ATOBCTGTH1();
                case "ATOBCTGTH2":
                    return new ATOBCTGTH2();
                case "ATOBCPBBP":
                    return new ATOBCPBBP();
                case "ATOBCPBNV":
                    return new ATOBCPBNV();
                case "ATOPBVCC":
                    return new ATOPBVCC();
                case "ATOBCNV":
                    return new ATOBCNV();
                case "ATOBCCCTH5":
                    return new ATOBCCCTH5();
                case "ATOBCCCTH6":
                    return new ATOBCCCTH6();
                case "ATOBCCCTH2":
                    return new ATOBCCCTH2();
                case "ATOBCCCTH1":
                    return new ATOBCCCTH1();
                case "ATOBCPBTH1":
                    return new ATOBCPBTH1();
                case "ATOPBVCCBP":
                    return new ATOPBVCCBP();

                #endregion Công cụ , dụng cụ
                #region ==== Hệ thống ====
                case "AAPPR_SOA"://Hệ thống/Quản lý người sử dụng/C.Duyệt, xử lý hóa đơn bán hàng.
                    return new AAPPR_SOA();
                case "AAPPR_SOA2"://Hệ thống/Quản lý người sử dụng/K.Chuyển sang hóa đơn điện tử.
                    return new AAPPR_SOA2();
                case "AGLAUTOSO_CT":
                    return new XAGLAUTOSO_CT();
                case "XLSALKH":
                    return new XLSALKH();
                case "XLSALVT":
                    return new XLSALVT();
                case "XLSPOA":
                    return new XLSPOA_Filter();
                case "XLSIND":
                    return new XLSIND_Filter();
                case "XLSIXA":
                    return new XLSIXA_Filter();
                case "XLSSOA":
                    return new XLSSOA_Filter();
                case "XLSTA1":
                    return new XLSTA1_Filter();
                case "XLSCA1":
                    return new XLSCA1_Filter();
                    //NHAN SU
                case "XLSPRCONG2":
                    return new XLSPRCONG2();
                case "XLSHRPERSONAL":
                    return new XLSHRPERSONAL();
                case "AAPPR_SOA1":
                    return new AAPPR_SOA1();
                case "AAPPR_SOA_IN1":
                    return new AAPPR_SOA_IN1();
                case "AAPPR_SOC_IN1":
                    return new AAPPR_SOC_IN1();
                case "AAPPR_SOA_IN2":
                    return new AAPPR_SOA_IN2();
                case "AAPPR_SOA_IN3":
                    return new AAPPR_SOA_IN3();
                case "AAPPR_POA":
                    return new AAPPR_POA();
                case "AAPPR_IND":
                    return new AAPPR_IND();
                case "V6BACKUP1":
                    return new V6BACKUP1();
                case "V6LEVELDOWN":
                    return new V6LEVELDOWN();
                case "V6LEVELSET":
                    return new V6LEVELSET();
                case "AAPPR_XULY_SOA":
                    return new AAPPR_XULY_SOA();
                #endregion he thong

                #region ==== Tổng hợp ====
                #region ---- Cập nhật số liệu ----
                case "AGLCTKC":
                    return new AGLCTKC();
                case "AGLCTPB":
                    return new AGLCTPB();
                case "AGLTHUE30":
                    return new AGLTHUE30();
                case "AGLTHUE20":
                    return new AGLTHUE20();
                case "AGLABTK2N"://Ket chuyen tk
                    return new AGLABTK2N();
                #endregion Cập nhật số liệu
                #region ---- Các bảng kê báo cáo tài khoản ----
                case "AGLBK1":
                    return new AGLBK1();
                case "AGLAUTO":
                    return new AGLAUTO();

                #endregion Các bảng kê báo cáo tài khoản 
                #region ---- Báo cáo tài chính ----
                case "AGLCD1":
                    return new AGLCD1();

                case "AGLCD1F5":
                    return new AGLCD1F5();
                case "AGLCD1F5F5":
                    return new AGLCD1F5F5();
                case "AGLTCB":
                    return new AGLTCB();
                case "AGLTCC":
                    return new AGLTCC();
                case "AGLTCD":
                    return new AGLTCD();
                case "AGLTCA":
                    return new AGLTCA();
                #endregion Báo cáo tài chính
                #region ----  sổ theo HT nhật ký chung ----
                case "AGLSNKT":
                    return new AGLSNKT();
                case "AGLSNKC":
                    return new AGLSNKC();
                case "AGLSNKBH":
                    return new AGLSNKBH();
                case "AGLSO2":
                    return new AGLSO2();
                case "AGLTH1T":
                    return new AGLTH1T();
                case "AGLTH1TF5":
                    return new AGLTH1TF5();
                case "AGLTH1TF10":
                    return new AGLTH1TF10();

                case "AGLSNKMH":
                    return new AGLSNKMH();
                #endregion  sổ theo HT nhật ký chung
                case "AGLTHUEBK2TT156":
                    return new AGLTHUEBK2TT156();
                case "AGLTHUEBK3TT156":
                    return new AGLTHUEBK3TT156();
                case "AGLTHUEBK4":
                    return new AGLTHUEBK4();


                #region ---- CTGS ----
                case "AGSCTGS01":
                    return new AGSCTGS01();
                case "AGSCTGS02":
                    return new AGSCTGS02();
                #endregion ctgs
                #region ---- Báo cáo thuế ----
                case "AGLQTTTNTT156":
                    return new AGLQTTTNTT156();
                #endregion ------ báo cáo thuế -------
                #region ---- Báo cáo tài chính QĐ 15----

                #endregion ------ Báo cáo tài chính QĐ 15 -------
                #endregion Tổng hợp

                #region ==== Chi phí giá thành ====
                case "ACPDDCK":
                    return new ACPDDCK(); // Giá thành sản phẩm
                case "ACOABVV2N":
                    return new ACOABVV2N(); // Kết chuyển vv
                case "AARHDKH2N":
                    return new AARHDKH2N(); // Kết chuyển hop dong - khach hang
                case "ACOTKTH4":
                    return new ACOTKTH4();
                #region ==== Báo cáo theo vu việc ====
                case "ACOVVBK2":
                    return new ACOVVBK2();
                case "ACOVVTH4":
                    return new ACOVVTH4();

                case "ACOVVTH1":
                    return new ACOVVTH1();
                case "ACOVVTH2":
                    return new ACOVVTH2();
                case "ACOVVTH1F5":
                    return new ACOVVTH1F5();
                case "ACOVVTH2F5":
                    return new ACOVVTH2F5();
                #endregion Báo cáo theo vu việc
                #region ==== Báo cáo chi phí theo tài khoản ====
                case "ACOTKTH3BC_N4":
                    return new ACOTKTH3BC_N4();
                case "ACOTKTH3BC_N1":
                    return new ACOTKTH3BC_N1();
                case "ACOTKTH3BC_N2":
                    return new ACOTKTH3BC_N2();
                case "ACOTKTH3BC_N3":
                    return new ACOTKTH3BC_N3();
                case "ACOTKTH2":
                    return new ACOTKTH2();
                case "AGLBK2BT":
                    return new AGLBK2BT();
                #endregion Báo cáo chi phí theo tài khoản
                #region ==== Báo cáo phân tích chi phí ====

                case "ACOPHIBK1":
                    return new ACOPHIBK1();
                case "ACOPHITH1":
                    return new ACOPHITH1();
                case "ACOPHITH1F5":
                    return new ACOPHITH1F5();
                case "ACOPHITH2":
                    return new ACOPHITH2();
                case "ACOPHITH2F5":
                    return new ACOPHITH2F5();
                case "ACOPHIBK2":
                    return new ACOPHIBK2();
                case "ACOTKBK2":
                    return new ACOTKBK2();
                #endregion ==== Báo cáo phân tích chi phí ====
                #region ==== Giá thành sản phẩm liên tục ====
                case "ACOLSXTH2":
                    return new ACOLSXTH2();
                case "ACOLSXTH1":
                    return new ACOLSXTH1();
                #endregion Giá thành sản phẩm liên tục  ====
                #endregion Chi phí giá thành
                #region ==== Danh mục ====
                case "AARKH":
                    //return new FilterDanhMuc("ALKH");
                    return new AARKHFilterDanhMuc();
                case "AINVT":
                    return new FilterDanhMuc("ALVT");
                case "AINLO":
                    return new FilterDanhMuc("ALLO");
                case "AINKHO":
                    return new FilterDanhMuc("ALKHO");
                case "ACATKNH":
                    return new FilterDanhMuc("ALTKNH");
                case "AARBP":
                    return new FilterDanhMuc("ALBP");
                case "AGLTK0":
                    return new FilterDanhMuc("ALTK0");
                case "AARNHKH":
                    return new FilterDanhMuc("ALNHKH");
                case "AARTHUE":
                    return new FilterDanhMuc("ALTHUE");
                case "AINNHVT":
                    return new FilterDanhMuc("ALNHVT");
                case "ACOVV":
                    return new FilterDanhMuc("ALVV");
                case "ACONHVV":
                    return new FilterDanhMuc("ALNHVV");
                case "AFANV":
                    return new FilterDanhMuc("ALNV");
                case "AFATGTS":
                    return new FilterDanhMuc("ALTGTS");
                case "AFABP":
                    return new FilterDanhMuc("ALBP");
                case "AFANHTS":
                    return new FilterDanhMuc("ALNHTS");
                case "ATOTGCC":
                    return new FilterDanhMuc("ALTGCC");
                case "ATOBP":
                    return new FilterDanhMuc("ALBP");
                case "ATONHCC":
                    return new FilterDanhMuc("ALNHCC");
                case "AGLDVCS":
                    return new FilterDanhMuc("ALDVCS");
                case "AGLTK2":
                    return new FilterDanhMuc("ALTK2");
                case "AGLDMKC":
                    return new FilterDanhMuc("ALKC");
                case "AGLDMPB":
                    return new FilterDanhMuc("ALPB");
                case "AGLNHTK":
                    return new FilterDanhMuc("ALNHTK");
                case "AGLNHTK0":
                    return new FilterDanhMuc("ALNHTK0");
                case "V6CT":
                    return new FilterDanhMuc("ALCT");
                case "V6OPTION":
                    return new FilterDanhMuc("V6OPTION");
                case "V6TGNT":
                    return new FilterDanhMuc("ALTGNT");
                case "V6NT":
                    return new FilterDanhMuc("ALNT");
                #endregion danh muc

                #region ==== Kho - Vị trí ====
                case "AINVITRI01":
                    return new AINVITRI01_filter();
                case "AINVITRI02":
                    return new AINVITRI02_filter();
                case "AINVITRI03AF7":
                case "AINVITRI03BF7":
                    return new AINVITRI03AF7_filter();
                #endregion kho-vitri
                #region ==== In liên tục ====

                case "AAPPR_SOF_IN1":
                    return new AAPPR_SOF_IN1();
                case "AAPPR_TA1_IN1":
                    return new AAPPR_TA1_IN1();
                case "AAPPR_CA1_IN1":
                    return new AAPPR_CA1_IN1();
                case "AAPPR_BC1_IN1":
                    return new AAPPR_BC1_IN1();
                case "AAPPR_BN1_IN1":
                    return new AAPPR_BN1_IN1();
                case "AAPPR_POA_IN1":
                    return new AAPPR_POA_IN1();
                case "AAPPR_POB_IN1":
                    return new AAPPR_POB_IN1();
                case "AAPPR_IXC_IN1":
                    return new AAPPR_IXC_IN1();
                case "AAPPR_IND_IN1":
                    return new AAPPR_IND_IN1();
                case "AAPPR_IXA_IN1":
                    return new AAPPR_IXA_IN1();
                case "AAPPR_IXB_IN1":
                    return new AAPPR_IXB_IN1();

                #endregion In liên tục

                case "AINVTBAR1":
                    return new FilterDanhMuc("ALVT");
                case "AINVTBAR2":
                    return new FilterDanhMuc("ALLO");
                case "AINVTBAR2F9":
                    return new AINVTBAR2F9();
                case "AINVTBAR3":
                    return new AINVTBAR3();

                case "ACOSXLT_TINHGIA":
                    return new ZACOSXLT_TINHGIA_Filter();

                case "AMAP01":
                    return new AMAP01Filter();
                case "AMAP01A":
                    return new AMAP01AFilter();
                case "AMAP01B":
                    return new AMAP01BFilter();
                case "AMAP01C":
                    return new AMAP01CFilter();
                case "AMAP01D":
                    return new AMAP01DFilter();
                case "AMAP01E":
                    return new AMAP01EFilter();
                case "AMAP01F":
                    return new AMAP01FFilter();
                case "AMAP01G":
                    return new AMAP01GFilter();
                case "AMAP01H":
                    return new AMAP01HFilter();
                case "AMAP01I":
                    return new AMAP01IFilter();
                case "AMAP01K":
                    return new AMAP01KFilter();
                case "AVGLGSSO5A":
                    return new AVGLGSSO5A();
                case "AVGLGSSO5AF5":
                    return new AVGLGSSO5AF5();
                case "AGLGSSO4T":
                    return new AGLGSSO4T();
                case "AGLGSSO4TF5":
                    return new AGLGSSO4TF5();
                case "AGLGSSO4TF10":
                    return new AGLGSSO4TF10();
                case "AVGLGSSO6A":
                    return new AVGLGSSO6A();
                case "AVGLGSSO6AF5":
                    return new AVGLGSSO6AF5();

            }
            return new FilterBase() { Visible = false };
        }

        public static ReportFilter44Base GetFilterControl44(string program)
        {
            return new ReportFilter44Base(program);
        }
    }
}

namespace V6ControlManager.FormManager.ChungTuManager.InChungTu.Filter
{
    public static class InFilter
    {
        public static InChungTuFilterBase GetFilterControl(string program)
        {
            program = program.Trim().ToUpper();
            switch (program)
            {
                case "AARCTAR1"://Hóa đơn dịch vụ 21
                    return new AARCTAR1();

                case "ACACTTA1"://Phiếu thu 41
                    return new ACACTTA1();

                case "ACACTBC1"://46
                    return new ACACTBC1();

                case "ACACTCA1"://51
                    return new ACACTCA1();

                case "ACACTBN1"://56
                    return new ACACTBN1();

                case "APOCTPOA"://71
                    return new APOCTPOA();

                case "APOCTPOB"://72
                    return new APOCTPOB();
                case "APOCTPOC"://73
                    return new APOCTPOC();
                case "APOCTPOH":
                    return new APOCTPOH();
                case "APOCTINY"://94
                    return new APOCTINY();
                case "APOCTINT"://97
                    return new APOCTINT();

                case "AINCTIND"://74
                    return new AINCTIND();

                case "ASOCTSOF"://76
                    return new ASOCTSOF();

                case "ASOCTSOR"://Báo giá.
                    return new ASOCTSOR();

                case "ASOCTSOH":
                    return new ASOCTSOH();
                case "ASOCTIXY"://95
                    return new ASOCTIXY();
                case "ASOCTIXP"://96
                    return new ASOCTIXP();

                case "ASOCTSOA"://81
                    return new ASOCTSOA();
                case "AAPPR_SOA_IN1F9":
                    return new AAPPR_SOA_IN1F9();
                case "ASOCTSOB"://82
                    return new ASOCTSOB();
                case "ASOCTSOC"://83
                    return new ASOCTSOC();
                case "AAPPR_SOC_IN1F9":
                    return new AAPPR_SOC_IN1F9();
                case "AAPPR_SOA_IN2F9":
                    return new AAPPR_SOA_IN2F9();

                case "AINCTIXA"://84
                    return new AINCTIXA();
                case "AINCTIXB"://85
                    return new AINCTIXB();

                case "AINCTIXC"://86
                    return new AINCTIXC();
                case "AAPCTAP1"://31
                    return new AAPCTAP1();
                case "AAPCTAP2"://32
                    return new AAPCTAP2();
                case "AGLCTGL1"://11
                    return new AGLCTGL1();

                case "PRINT_AMAD":
                    return new PRINT_AMAD();
                case "PRINT_INFOR":
                    return new PRINT_INFOR();
                #region ==== In liên tục ====
                case "AAPPR_SOF_IN1F9":
                    return new AAPPR_SOF_IN1F9();
                case "AAPPR_TA1_IN1F9":
                    return new AAPPR_TA1_IN1F9();
                case "AAPPR_CA1_IN1F9":
                    return new AAPPR_CA1_IN1F9();
                case "AAPPR_BC1_IN1F9":
                    return new AAPPR_BC1_IN1F9();
                case "AAPPR_BN1_IN1F9":
                    return new AAPPR_BN1_IN1F9();
                case "AAPPR_POA_IN1F9":
                    return new AAPPR_POA_IN1F9();
                case "AAPPR_POB_IN1F9":
                    return new AAPPR_POB_IN1F9();
                case "AAPPR_IXC_IN1F9":
                    return new AAPPR_IXC_IN1F9();
                case "AAPPR_IND_IN1F9":
                    return new AAPPR_IND_IN1F9();
                case "AAPPR_IXA_IN1F9":
                    return new AAPPR_IXA_IN1F9();
                case "AAPPR_IXB_IN1F9":
                    return new AAPPR_IXB_IN1F9();
                    
                    #endregion In liên tục

            }
            if (program.StartsWith("APRINT_")) return new PRINT_FILTER();
            if (program.StartsWith("APRINT1_")) return new PRINT1_FILTER();
            return new InChungTuFilterBase();
        }
    }
}

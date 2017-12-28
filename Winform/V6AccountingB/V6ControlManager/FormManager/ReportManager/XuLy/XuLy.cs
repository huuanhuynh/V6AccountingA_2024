using V6ControlManager.FormManager.ReportManager.XuLy.NhanSu;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public static class XuLy
    {
        public static XuLyBase GetXuLyControl(string itemId, string program, string procedure, string reportFile, string reportCaption, string reportCaption2,
            string repFileF5, string repTitleF5, string repTitle2F5)
        {
            switch (program)
            {
                case "AAPPR_SOA":
                    return new AAPPR_SOA(itemId, program, procedure, reportFile, reportCaption, reportCaption2);
                case "AAPPR_SOA2":
                    return new AAPPR_SOA2(itemId, program, procedure, reportFile, reportCaption, reportCaption2);
                case "AAPPR_XULY_SOA":
                    return new AAPPR_XULY_SOA(itemId, program, procedure, reportFile, reportCaption, reportCaption2);
                case "AAPPR_SOA1":
                    return new AAPPR_SOA1(itemId, program, procedure, reportFile, reportCaption, reportCaption2);
                case "XLSALKH":
                    return new XLSALKH_Control(itemId, program, program, reportFile, program, reportCaption2);
                case "XLSALVT":
                    return new XLSALVT_Control(itemId, program, program, reportFile, program, reportCaption2);

               case "AAPPR_SOA_IN1":
                    return new AAPPR_SOA_IN1(itemId, program, procedure, reportFile, reportCaption, reportCaption2);
                case "AAPPR_SOC_IN1":
                    return new AAPPR_SOC_IN1(itemId, program, procedure, reportFile, reportCaption, reportCaption2);
                case "AAPPR_SOA_IN2":
                    return new AAPPR_SOA_IN2(itemId, program, procedure, reportFile, reportCaption, reportCaption2);
               case "AAPPR_SOA_IN3":
                    return new AAPPR_SOA_IN3(itemId, program, procedure, reportFile, reportCaption, reportCaption2);
                case "XLSPOA":
                    return new XLSPOA_Control(itemId, program, procedure, reportFile, reportCaption, reportCaption2);
                case "XLSIND":
                    return new XLSIND_Control(itemId, program, procedure, reportFile, reportCaption, reportCaption2);
                case "XLSIXA":
                    return new XLSIXA_Control(itemId, program, procedure, reportFile, reportCaption, reportCaption2);
                case "XLSSOA":
                    return new XLSSOA_Control(itemId, program, procedure, reportFile, reportCaption, reportCaption2);
                case "XLSTA1":
                    return new XLSTA1_Control(itemId, program, procedure, reportFile, reportCaption, reportCaption2);
                case "XLSCA1":
                    return new XLSCA1_Control(itemId, program, procedure, reportFile, reportCaption, reportCaption2);
                    //NHAN SU
                case "XLSPRCONG2":
                    return new XLSPRCONG2_Control(itemId, program, procedure, reportFile, reportCaption, reportCaption2);

                case "AFASUAKH":
                    return new AFASUAKH(itemId, program, procedure, reportFile, reportCaption, reportCaption2);
                case "ATOSUAPB":
                    return new ATOSUAPB(itemId, program, procedure, reportFile, reportCaption, reportCaption2);

                case "AFABTPBTSN":
                    return new AFABTPBTSN(itemId, program, procedure, reportFile, reportCaption, reportCaption2);
                case "AFATANGNG":
                    return new AFATANGNG(itemId, program, procedure, reportFile, reportCaption, reportCaption2);
                case "AFAGIAMNG":
                    return new AFAGIAMNG(itemId, program, procedure, reportFile, reportCaption, reportCaption2);
                case "AFADCBP":
                    return new AFADCBP(itemId, program, procedure, reportFile, reportCaption, reportCaption2);
                case "AFADCCTBP":
                    return new AFADCCTBP(itemId, program, procedure, reportFile, reportCaption, reportCaption2);
                case "ATODCBP":
                    return new ATODCBP(itemId, program, procedure, reportFile, reportCaption, reportCaption2);
                case "ATODCCTBP":
                    return new ATODCCTBP(itemId, program, procedure, reportFile, reportCaption, reportCaption2);
                case "AAPPR_POA":
                    return new AAPPR_POA(itemId, program, procedure, reportFile, reportCaption, reportCaption2);
                case "AAPPR_IND":
                    return new AAPPR_IND(itemId, program, procedure, reportFile, reportCaption, reportCaption2);
                case "AGLCTKC":
                    return new AGLCTKC(itemId, program, procedure, reportFile, reportCaption, reportCaption2);
                case "AGLCTPB":
                    return new AGLCTPB(itemId, program, procedure, reportFile, reportCaption, reportCaption2);
                case "ACPDDCK":
                    return new ACPDDCK(itemId, program, procedure, reportFile, reportCaption, reportCaption2);
                case "ATOBTPBCCN":
                    return new ATOBTPBCCN(itemId, program, procedure, reportFile, reportCaption, reportCaption2);
                case "ATOTANGNG":
                    return new ATOTANGNG(itemId, program, procedure, reportFile, reportCaption, reportCaption2);
                case "ARSD0_AR0":
                    return new ARSD0_AR0(itemId, program, procedure, reportFile, reportCaption, reportCaption2);
                case "ARSD0_AP0":
                    return new ARSD0_AP0(itemId, program, procedure, reportFile, reportCaption, reportCaption2);
                case "AGLTHUE30":
                    return new AGLTHUE30(itemId, program, procedure, reportFile, reportCaption, reportCaption2);
                case "AGLTHUE20":
                    return new AGLTHUE20(itemId, program, procedure, reportFile, reportCaption, reportCaption2); 
                case "ARSD_AR":
                    return new ARSD_AR(itemId, program, procedure, reportFile, reportCaption, reportCaption2);
                case "ARSD_AP":
                    return new ARSD_AP(itemId, program, procedure, reportFile, reportCaption, reportCaption2);
                case "V6LEVELDOWN":
                    return new V6LEVELDOWN(itemId, program, procedure, reportFile, reportCaption, reportCaption2);
                case "V6LEVELSET":
                    return new V6LEVELSET(itemId, program, procedure, reportFile, reportCaption, reportCaption2);
                case "V6IMXLS":
                    return new V6IMPORTXLS(itemId, program, procedure, reportFile, reportCaption, reportCaption2);

                case "AGLSO1T":
                    return new AGLSO1T(itemId, program, procedure, reportFile, reportCaption, reportCaption2);
                case "AGSCTGS01":
                    return new AGSCTGS01_Control(itemId, program, procedure, reportFile, reportCaption, reportCaption2);
                case "AGSCTGS02":
                    return new AGSCTGS02_Control(itemId, program, procedure, reportFile, reportCaption, reportCaption2);

                case "AARSO1T":
                    return new AARSO1T(itemId, program, procedure, reportFile, reportCaption, reportCaption2,
                        repFileF5, repTitleF5, repTitle2F5);

                case "AINSO1T":
                    return new AINSO1T(itemId, program, procedure, reportFile, reportCaption, reportCaption2,
                        repFileF5, repTitleF5, repTitle2F5);

                case "AINSO3T":
                    return new AINSO3T(itemId, program, procedure, reportFile, reportCaption, reportCaption2,
                        repFileF5, repTitleF5, repTitle2F5);
                
                case "AAPSO1T":
                    return new AAPSO1T(itemId, program, procedure, reportFile, reportCaption, reportCaption2,
                        repFileF5, repTitleF5, repTitle2F5);

                case "ARCMO_AR":
                    return new ARCMO_Control(itemId, program, procedure, reportFile, reportCaption, reportCaption2);
                case "ARCMO_ARF9":
                    return new ARCMO_ARF9Control(itemId, program, procedure, reportFile, reportCaption, reportCaption2);

                case "APDMO_AP":
                    return new APDMO_APControl(itemId, program, procedure, reportFile, reportCaption, reportCaption2);
                case "APDMO_APF9":
                    return new APDMO_APF9Control(itemId, program, procedure, reportFile, reportCaption, reportCaption2);

                case "AGLTH1T":
                    return new AGLTH1T(itemId, program, procedure, reportFile, reportCaption, reportCaption2);

                case "HPRCONGCT":
                    return new HPRCONGCT_XL1(itemId, program, procedure, reportFile, reportCaption, reportCaption2);
                case "ACOSXLT_TINHGIA":
                    return new ZACOSXLT_TINHGIA(itemId, program, procedure, reportFile, reportCaption, reportCaption2);

                case "AVGLGSSO5A":
                    return new AVGLGSSO5A(itemId, program, procedure, reportFile, reportCaption, reportCaption2,
                        repFileF5, repTitleF5, repTitle2F5);
                case "AGLGSSO4T":
                    return new AGLGSSO4T(itemId, program, procedure, reportFile, reportCaption, reportCaption2);

                case "AVGLGSSO6A":
                    return new AVGLGSSO6A(itemId, program, procedure, reportFile, reportCaption, reportCaption2,
                        repFileF5, repTitleF5, repTitle2F5);

                #region ==== In liên tục ====
                case "AAPPR_SOF_IN1":
                    return new AAPPR_SOF_IN1(itemId, program, procedure, reportFile, reportCaption, reportCaption2);
                case "AAPPR_TA1_IN1":
                    return new AAPPR_TA1_IN1(itemId, program, procedure, reportFile, reportCaption, reportCaption2);
                case "AAPPR_CA1_IN1":
                    return new AAPPR_CA1_IN1(itemId, program, procedure, reportFile, reportCaption, reportCaption2);
                case "AAPPR_BC1_IN1":
                    return new AAPPR_BC1_IN1(itemId, program, procedure, reportFile, reportCaption, reportCaption2);
                case "AAPPR_BN1_IN1":
                    return new AAPPR_BN1_IN1(itemId, program, procedure, reportFile, reportCaption, reportCaption2);
                case "AAPPR_POA_IN1":
                    return new AAPPR_POA_IN1(itemId, program, procedure, reportFile, reportCaption, reportCaption2);
                case "AAPPR_POB_IN1":
                    return new AAPPR_POB_IN1(itemId, program, procedure, reportFile, reportCaption, reportCaption2);
                case "AAPPR_IXC_IN1":
                    return new AAPPR_IXC_IN1(itemId, program, procedure, reportFile, reportCaption, reportCaption2);
                case "AAPPR_IND_IN1":
                    return new AAPPR_IND_IN1(itemId, program, procedure, reportFile, reportCaption, reportCaption2);
                case "AAPPR_IXA_IN1":
                    return new AAPPR_IXA_IN1(itemId, program, procedure, reportFile, reportCaption, reportCaption2);
                case "AAPPR_IXB_IN1":
                    return new AAPPR_IXB_IN1(itemId, program, procedure, reportFile, reportCaption, reportCaption2);
                    #endregion In liên tục


            }
            return new XuLyBase(itemId, program, procedure, reportFile, reportCaption, reportCaption2, false);
        }

        public static XuLyBase0 GetXuLyControlX(string itemId, string program, string procedure, string reportFile, string text)
        {
            // Các control là kế thừa của XuLyBase0. Có thể không dùng filter.
            switch (program)
            {
                case "AINGIA_TB":
                    return new AINGIA_TB(itemId, program, procedure, reportFile, text);
                case "HPAYROLLCALC":
                    return new HPAYROLLCALC(itemId, program, procedure, reportFile, text);
                case "AINABVT2N":
                    return new AINABVT2N(itemId, program, procedure, reportFile, text);
                case "AINABLO2N":
                    return new AINABLO2N(itemId, program, procedure, reportFile, text);
                case "AINABVITRI2N":
                    return new AINABVITRI2N(itemId, program, procedure, reportFile, text);
                case "ACAKU2N":
                    return new ACAKU2N(itemId, program, procedure, reportFile, text);
                case "AARBPKH2N":
                    return new AARBPKH2N(itemId, program, procedure, reportFile, text);
                case "ACOVVKH2N":
                    return new ACOVVKH2N(itemId, program, procedure, reportFile, text);
                case "ACOABVV2N":
                    return new ACOABVV2N(itemId, program, procedure, reportFile, text);
                case "AARHDKH2N":
                    return new AARHDKH2N(itemId, program, procedure, reportFile, text);
                case "AGLABTK2N":
                    return new AGLABTK2N(itemId, program, procedure, reportFile, text);
                case "AGLAUTOSO_CT":
                    return new XAGLAUTOSO_CT_Base(itemId, program, procedure, reportFile, text);
                case "ACACNTG":
                    return new ACACNTG(itemId, program, procedure, reportFile, text);
                case "V6BACKUP1":
                    return new V6BACKUP1(itemId, program, procedure, reportFile, text);

                case "V6COPYRA":
                    return new V6COPY_RA(itemId, program, procedure, reportFile, text);
                case "V6COPYRAALL":
                    return new V6COPY_RA_ALL(itemId, program, procedure, reportFile, text);
                case "V6CHECK_U1":
                    return new V6CHECK_U1(itemId, program, procedure, reportFile, text);
                case "V6HELP_QA":
                    return new V6HELP_QA(itemId, program, procedure, reportFile, text);
                case "AINGIA_TBDD":
                    return new AINGIA_TBDD(itemId, program, procedure, reportFile, text);
                case "AINGIA_NTXT":
                    return new AINGIA_NTXT(itemId, program, procedure, reportFile, text);

                case "AINVTBAR1":
                    return new AINVTBAR1_Control(itemId, program, procedure, reportFile, text);
                case "AINVTBAR2":
                    return new AINVTBAR2_Control(itemId, program, procedure, reportFile, text);

                case "AINVITRI01":
                    return new AINVITRI01(itemId, program, procedure, reportFile, text);
                case "AINVITRI01Draw":
                    return new AINVITRI01(itemId, program, procedure, reportFile, text);
                case "AINVITRI02":
                    return new AINVITRI02(itemId, program, procedure, reportFile, text);
                case "AINVITRI03":
                    return new AINVITRI03(itemId, program, procedure, reportFile, text);
                case "AINVITRI04":
                    return new AINVITRI04(itemId, program, procedure, reportFile, text);
                case "AINVITRI05": //Áp chi tiết vị trí.
                    return new AINVITRI05(itemId, program, procedure, reportFile, text);
                    
                case "HPRCONGCT":
                    return new HPRCONGCT_XL0(itemId, program, procedure, reportFile, text);

                case "AMAPEDIT":
                    return new AMAPEDIT(itemId, program, procedure, reportFile, text);
                case "AMAP01":
                    return new AMAPREPORT(itemId, program, procedure, reportFile, text);

                case "HRORGVIEW1":
                    return new HRORGVIEW1(itemId, program, procedure, reportFile, text);
                case "V6EDITALAB":
                    return new V6EDITALAB(itemId, program, procedure, reportFile, text);
            }
            return new XuLyBase0(itemId, program, procedure, reportFile, text);
        }
    }
}

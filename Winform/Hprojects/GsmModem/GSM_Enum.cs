using System;
using System.Collections.Generic;
using System.Text;

namespace GSM
{
    public enum CommandMode : int
    {
        PDU_Mode = 0, Text_Mode = 1
    }
    public enum ENUM_TP_DCS
    {
        /// <summary>
        /// Class0 flash sms
        /// </summary>
        Class0_UD_7bits = 240,
        /// <summary>
        /// Class0 flash sms
        /// </summary>
        Class0_UD_8bits = 0xf4,
        /// <summary>
        /// Class1 send to the mobile equipment or SM
        /// </summary>
        Class1_UD_7bits = 0xf1,
        /// <summary>
        /// Class1 send to the mobile equipment or SM
        /// </summary>
        Class1_UD_8bits = 0xf5,

        /// <summary>
        /// 0xf2 Class2 send to sim
        /// </summary>
        Class2_UD_7bits = 0xf2,
        /// <summary>
        /// Class2_UD_8bits = 0xf6 Class2 send to sim
        /// </summary>
        Class2_UD_8bits = 0xf6,
        //
        Class3_UD_7bits = 0xf3,
        Class3_UD_8bits = 0xf7,
        /// <summary>
        /// Default
        /// </summary>
        DefaultAlphabet = 0,
        //Email_Ind_Discard_Msg = 0xc2,
        //Email_Ind_Store_7bit_Msg = 210,
        //Email_Ind_Store_UCS2_Msg = 0xe2,
        //Fax_Ind_Discard_Msg = 0xc1,
        //Fax_Ind_Store_7bit_Msg = 0xd1,
        //Fax_Ind_Store_UCS2_Msg = 0xe1,
        //No_Email_Ind_Discard_Msg = 0xca,
        //No_Email_Ind_Store_7bit_Msg = 0xda,
        //No_Email_Ind_Store_UCS2_Msg = 0xea,
        //No_Fax_Ind_Discard_Msg = 0xc9,
        //No_Fax_Ind_Store_7bit_Msg = 0xd9,
        //No_Fax_Ind_Store_UCS2_Msg = 0xe9,
        //No_Other_Ind_Discard_Msg = 0xcb,
        //No_Other_Ind_Store_7bit_Msg = 0xdb,
        //No_Other_Ind_Store_UCS2_Msg = 0xeb,
        //No_Voicemail_Ind_Discard_Msg = 200,
        //No_Voicemail_Ind_Store_7bit_Msg = 0xd8,
        //No_Voicemail_Ind_Store_UCS2_Msg = 0xe8,
        //Other_Ind_Discard_Msg = 0xc3,
        //Other_Ind_Store_7bit_Msg = 0xd3,
        //Other_Ind_Store_UCS2_Msg = 0xe3,

        /// <summary>
        /// Unicode 16 bit UCS2 = 8
        /// </summary>
        UCS2 = 8,
        //VoiceMail_Ind_Discard_Msg = 0xc0,
        //Voicemail_Ind_Store_7bit_Msg = 0xd0,
        //Voicemail_Ind_Store_UCS2_Msg = 0xe0
        NotUse = -1
    }
    public enum ENUM_TP_VALID_PERIOD
    {
        Maximum = 0xff,
        OneDay = 0xa7,
        OneHour = 11,
        OneWeek = 0xc4,
        SixHours = 0x47,
        ThreeHours = 0x1d,
        TwelveHours = 0x8f
    }
    public enum Message_Type
    {
        REC_UNREAD = 0,
        REC_READ = 1,
        STO_UNSENT = 2,
        STO_SENT = 3,
        ALL = 4,
        UNKNOW = 0xFF
    }
    //enum SMSType
    //{
    //    EMS_RECEIVED = 0x40,    //01000000
    //    EMS_SUBMIT = 0x41,      //01000001
    //    SMS_RECEIVED = 0,       //00000000
    //    SMS_STATUS_REPORT = 2,  //00000010
    //    SMS_SUBMIT = 1          //00000001
    //}
    public enum SMS_Source
    {
        Text_mode,
        PDU_mode
    }
    
}

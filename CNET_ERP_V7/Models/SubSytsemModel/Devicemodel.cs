using CNET_V7_Domain.Domain.ViewSchema;
using CNET_V7_Entities.DataModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net;
using static System.Net.Mime.MediaTypeNames;

namespace CNET_ERP_V7.Models.SubSytsemModel
{
    public class Devicemodel
    {
        public int DeviceType { get; set; }
        public string DeviceMaintanflage { get; set; }

        #region device maintain for branch
        public List<ViewDeviceByReferenceDTO> deviceDetail { get; set; }
        public string devicename { get; set; }
        public int deviceId { get; set; }
        public string Eamildevicename { get; set; }
        public int? connctionTYpe { get; set; }
        public int? host { get; set; }
        public string devicevalue { get; set; }
        public int fixedassetid { get; set; }
        public bool Isactive { get; set; }
        public string fixedassetidupdate { get; set; }
        public string fixedassetTempid { get; set; }
        public string? brand { get; set; }
        public string generic { get; set; }
        public int model { get; set; }
        public string size { get; set; }
        public string color { get; set; }
        public int? country { get; set; }
        public string manufacturer { get; set; }
        public string remark { get; set; }
        public int preferencecodeforsave { get; set; }
        public int branchcodeforsave { get; set; }
        public string parentcode { get; set; }
        public string branchservername { get; set; }

        public string bprfcode { get; set; }
        public string bprfparent { get; set; }
        public string savedMsage { get; set; }

        #endregion

        #region device property for domain server
        public string domain_Name { get; set; }
        public string domain_password { get; set; }
        public string user_name { get; set; }
        public string devicesettingproperty { get; set; }
        public string devicesettingpropertyInindex { get; set; }

        public string domainreferencecode { get; set; }
        public string dCamera { get; set; }
        public string preferencedescrption { get; set; }


        #endregion
        #region device property for IP TV server
        public string close_schedule_after { get; set; }
        public string iIIS_UPDATE_url { get; set; }
        public string physical_update_url { get; set; }
        public string Update_frequancy { get; set; }
        public string Alarm_music { get; set; }
        public string attachement_url { get; set; }
        public string back_ground_music { get; set; }
        public string back_ground_music_volume { get; set; }
        public string hotel_picture { get; set; }
        public string midia_root_path { get; set; }
        public string scroll_text { get; set; }
        public string tv_logo { get; set; }
        public string room_servies_pos { get; set; }
        public string vod_purshes_rule { get; set; }
        public string city_name { get; set; }
        public string refresh_time { get; set; }
        public string weather_services_url { get; set; }
        public string Iptvreferencecode { get; set; }

        public List<ViewDeviceByReferenceDTO> deviceViews { get; set; }
        public List<SelectListItem> vod_purshes_rules { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "Once", Text = "Once" },
            new SelectListItem { Value =  "ForentireStay", Text = "ForentireStay" },
            new SelectListItem { Value =  "ForSomeDay", Text = "ForSomeDay" }


        };

        #endregion

        #region client cloud setting
        public string enable_synchronization { get; set; }
        public int Max_Try_Count_Download { get; set; }
        public int Max_Try_Count_Upload { get; set; }
        public string sync_frequency { get; set; }
        public string clientdevicecodeType { get; set; }
        public string clientserverType { get; set; }

        public List<SelectListItem> enable_synchronizations { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "Automatic", Text = "Automatic" },
            new SelectListItem { Value =  "Manual", Text = "Manual" },
            new SelectListItem { Value =  "NotApplicable", Text = "NotApplicable" }


        };
        public List<SelectListItem> clientserverTypes { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "LocalServer", Text = "LocalServer" },
            new SelectListItem { Value =  "CloudServer", Text = "CloudServer" }

        };
        public List<SelectListItem> sync_frequencys { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "60", Text = "One_Min" },
            new SelectListItem { Value =  "120", Text = "Two_Min" },
            new SelectListItem { Value =  "300", Text = "Five_Min" },
            new SelectListItem { Value =  "600", Text = "Ten_Min" },
            new SelectListItem { Value =  "900", Text = "Fifteen_Min" },
            new SelectListItem { Value =  "180", Text = "Thirty_Min" },
            new SelectListItem { Value =  "3600", Text = "One_Hour" },
            new SelectListItem { Value =  "7200", Text = "Two_Hour" },
            new SelectListItem { Value =  "10800", Text = "Three_Hour" },
            new SelectListItem { Value =  "14400", Text = "Four_Hour" },
            new SelectListItem { Value =  "1800", Text = "Five_Hour" },
            new SelectListItem { Value =  "21600", Text = "Six_Hour" },
            new SelectListItem { Value =  "25200", Text = "Seven_Hour" },
            new SelectListItem { Value =  "28800", Text = "Eight_Hour" },
            new SelectListItem { Value =  "32400", Text = "Nine_Hour" },
            new SelectListItem { Value =  "36000", Text = "Ten_Hour" },
            new SelectListItem { Value =  "39600", Text = "Eleven_Hour" },
            new SelectListItem { Value =  "43200", Text = "Twelve_Hour" },
            new SelectListItem { Value =  "86400", Text = "TwentyFour_Hour" },

        };


        #endregion
        #region File server setting
        public string default_Attachment_Path { get; set; }
        public int filedevicecode { get; set; }
        #endregion

        #region FTP Server setting
        public string damp_yard { get; set; }
        public string ftp_password { get; set; }
        public string ftp_url { get; set; }
        public string ftp_host { get; set; }
        public string local_root_path { get; set; }
        public string local_root_path_for_vs { get; set; }
        public string slip_time_in_second { get; set; }
        public string user_name_for_ftp { get; set; }
        public string ftpdevicecode { get; set; }
        #endregion

        #region Email server setting
        public string from_Email_Address { get; set; }
        public string email_Password { get; set; }
        public string smtp_Server { get; set; }
        public string time_Out { get; set; }
        public string smtp_Port { get; set; }
        public string emaildevicecode { get; set; }
        #endregion

        #region Telegram Server Setting
        public string teleuser_name { get; set; }
        public string tele_password { get; set; }
        public string tele_signture { get; set; }
        public string teledevicecode { get; set; }
        #endregion
        #region IpParamtre

        [RegularExpression(@"^((25[0-5]|(2[0-4]|1\d|[1-9]|)\d)(\.(?!$)|$)){4}$")]
        public string ip_address { get; set; }
        public int? devie_Port { get; set; }
        public string device_mac { get; set; }
        public string devce_remark { get; set; }
        public string Ipdevicecode { get; set; }
        public string IpcodeForSave { get; set; }
        #endregion
        #region Serial Paramtre
        public int? baud_rate { get; set; }
        public int? serial_port { get; set; }
        public string device_parity { get; set; }
        public string serial_remark { get; set; }

        public string serialdevicecode { get; set; }
        public string serialcodeForSave { get; set; }


        #endregion
        #region RMS POS Setting
        public string rmsneed_Credentials_For_Removal { get; set; }
        public string rmsdefault_Payment { get; set; }

        public string rmscash_Customer { get; set; }
        public string rmspOS_Store { get; set; }
        public string rmspOS_theme { get; set; }
        public string rmsenable_cbe { get; set; }
        public float rmsaccuracy_Tolerance { get; set; }
        public float rmssummary_Difference_Tolerance { get; set; }
        public string rmsxML_Reconciliation { get; set; }
        public string rmscheck_Voucher_Integrity { get; set; }
        public string rmsclear_Held_Bill { get; set; }
        public string rmsdownLoad_EJ { get; set; }
        public string rmsf_and_B { get; set; }
        public string rmssummary_Voucher { get; set; }
        public string rmszeroing_Voucher { get; set; }
        public string rmsbold_Order_No { get; set; }
        public string rmsnavigatorType { get; set; }
        public string rmsthemeChangedDate { get; set; }
        public string rmsaspectRatio { get; set; }
        public string rmschangeFrequency { get; set; }
        public string rmsallowAnimation { get; set; }
        public int rmsholdAmountPerPerson { get; set; }
        public int rmshold_Document_Life { get; set; }
        public string rmstableMandatory { get; set; }
        public string rmsWaiterMandatory { get; set; }
        public string rmsenableAutoEntry { get; set; }
        public string rmsenableRoomCharge { get; set; }
        public string rmsdailyControlCount { get; set; }
        public string rmsenableTouchScreen { get; set; }
        public string rmscurrency_Conversion { get; set; }
        public string rmsenableValueFactory { get; set; }
        public string rmsinputCoverMandatory { get; set; }
        public string rmsenableOrderFunction { get; set; }
        public string rmsorder_Location { get; set; }
        public string rmscompanyTinMandatory { get; set; }
        public string rmsfiscalReconciliation { get; set; }
        public string rmsenableBillManagement { get; set; }
        public string rmsopenDrawerAfterPrint { get; set; }
        public string rmsenableVocalAssistance { get; set; }
        public string rmsLanguage { get; set; }
        public string rmsorderStationMandatory { get; set; }
        public string rmsdisableVoidOnRetrieve { get; set; }
        public string rmsclearHoldBeforeClosing { get; set; }
        public string rmsenableDocumentReference { get; set; }
        public string rmsenable_GSL_Tax { get; set; }
        public string rmscheckAmountLimitForHold { get; set; }
        public string rmsenableDeliveryManagement { get; set; }
        public string rmscaptureFiscalInformation { get; set; }
        public string rmscalculateConsumptionCost { get; set; }
        public string rmsenableDynamicVisualDisplay { get; set; }
        public string rmsselectAutomaticShiftPeriod { get; set; }
        public string rmsallowmultipleInstanceOfTable { get; set; }
        public string rmsCheckSystemTimestamP { get; set; }
        public string rmsprintDiscountAdditionalChargePerLineItem { get; set; }
        public string rmsclosing_frequency { get; set; }
        public string rmsenable_Package_esign { get; set; }
        public string rmsenable_Reservation { get; set; }
        public string rmspos_Theme_Fixed { get; set; }
        public string rmspOS_Trigger_Article_Removal { get; set; }
        public string rmsstation_Type { get; set; }
        public string rmstable_Selection_Type { get; set; }
        public string rmswaiter_selection { get; set; }
        public string rmsorder_Verification_Method { get; set; }
        public string rmsdefault_Transaction_Type { get; set; }
        public string rmsdevicecode { get; set; }
        public string rms_Ignore_zero_price_article { get; set; }
        public string rms_Enable_Menu_designer { get; set; }
        public enIgnorezeroprice_rticle rms_POSUseOnlyAvailableArticle { get; set; }
        public enIgnorezeroprice_rticle rms_EnablePOSServiceCharge { get; set; }
        public string rms_POSServiceChargeValue { get; set; }
        public enrecuIgnorezeroprice_rticle rms_EnableCurrencyConversion { get; set; }
        public enValueRule rms_POSValueRule { get; set; }
        public enNeedCredentials rms_NeedCredentialsForCustomerDiscount { get; set; }
        public enBeneficiaryUser rms_BeneficiaryUser { get; set; }

        public List<SelectListItem> accuracy_Tolerances { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "False", Text = "False" },
            new SelectListItem { Value =  "True", Text = "True" }
        };
        public List<SelectListItem> closing_frequencys { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "Shift", Text = "Shift" },
            new SelectListItem { Value =  "Day", Text = "Day" },
            new SelectListItem { Value =  "Week", Text = "Week" },
            new SelectListItem { Value =  "Month", Text = "Month" },
            new SelectListItem { Value =  "Quarter", Text = "Quarter" },
            new SelectListItem { Value =  "Year", Text = "Year" }
        };
        public List<SelectListItem> order_Locations { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "Network", Text = "Network" },
            new SelectListItem { Value =  "Local", Text = "Local" },
            new SelectListItem { Value =  "KDS", Text = "KDS" },
            new SelectListItem { Value =  "Hybrid", Text = "Hybrid" }
        };
        public List<SelectListItem> need_Credentials_For_Removals { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "Never", Text = "Never" },
            new SelectListItem { Value =  "WhenHeld", Text = "WhenHeld" },
            new SelectListItem { Value =  "Always", Text = "Always" }
        };
        public List<SelectListItem> pOS_Trigger_Article_Removals { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "No", Text = "No" },
            new SelectListItem { Value =  "WhenHeld", Text = "WhenHeld" },
            new SelectListItem { Value =  "Always", Text = "Always" }
        };
        public List<SelectListItem> station_Types { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "Sale", Text = "Sale" },
            new SelectListItem { Value =  "Order", Text = "Order" }
        };
        public List<SelectListItem> table_Selection_Types { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "Tile", Text = "Tile" },
            new SelectListItem { Value =  "LayoutDesigner", Text = "LayoutDesigner" },
            new SelectListItem { Value =  "NumericPad", Text = "NumericPad" }
        };

        public List<SelectListItem> waiter_selections { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "UseOnlyAttendedWaiter", Text = "UseOnlyAttendedWaiter" },
            new SelectListItem { Value =  "KeyboardInput", Text = "KeyboardInput" }
        };
        public List<SelectListItem> aspectrations { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "A_40X60", Text = "A_40X60" },
            new SelectListItem { Value =  "A_45x55", Text = "A_45x55" },
            new SelectListItem { Value =  "A_50x50", Text = "A_50x50" },
            new SelectListItem { Value =  "A_55x45", Text = "A_55x45" },
            new SelectListItem { Value =  "A_60x40", Text = "A_60x40" }
        };
        public List<SelectListItem> order_Verification_Methods { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "None", Text = "None" },
            new SelectListItem { Value =  "Token", Text = "Token" },
            new SelectListItem { Value =  "Finger", Text = "Finger" },
            new SelectListItem { Value =  "Card", Text = "Card" },
        };
        public List<SelectListItem> changefrequencys { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "Daily", Text = "Daily" },
            new SelectListItem { Value =  "Weekly", Text = "Weekly" },
            new SelectListItem { Value =  "Monthly", Text = "Monthly" },
            new SelectListItem { Value =  "Seasonal", Text = "Seasonal" },

        };
        public List<SelectListItem> POSThemes { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "CNETBloodyFlower_1", Text = "CNETBloodyFlower_1" },
            new SelectListItem { Value =  "CNETBlueStreak_2", Text = "CNETBlueStreak_2" },
            new SelectListItem { Value =  "CNETBlueStyle_3", Text = "CNETBlueStyle_3" },
            new SelectListItem { Value =  "CNETBluetexture_4", Text = "CNETBluetexture_4" },
            new SelectListItem { Value =  "CNETChristMas1_5", Text = "CNETChristMas1_5" },
            new SelectListItem { Value =  "CNETCocoa_6", Text = "CNETCocoa_6" },
            new SelectListItem { Value =  "CNETDaisis_7", Text = "CNETDaisis_7" },
            new SelectListItem { Value =  "CNETDinner_8", Text = "CNETDinner_8" },
            new SelectListItem { Value =  "CNETEarth_9", Text = "CNETEarth_9" },
            new SelectListItem { Value =  "CNETEarthSpace_10", Text = "CNETEarthSpace_10" },
            new SelectListItem { Value =  "CNETEflagSplashed_11", Text = "CNETEflagSplashed_11" },
            new SelectListItem { Value =  "CNETEnkutatash_12", Text = "CNETEnkutatash_12" },
            new SelectListItem { Value =  "CNETEthiopa2_13", Text = "CNETEthiopa2_13" },
            new SelectListItem { Value =  "CNETEthiopia1_14", Text = "CNETEthiopia1_14" },
            new SelectListItem { Value =  "CNETEthiopia_15", Text = "CNETEthiopia_15" },
            new SelectListItem { Value =  "CNETExtra_16", Text = "CNETExtra_16" },
            new SelectListItem { Value =  "CNETInfinity_17", Text = "CNETInfinity_17" },
            new SelectListItem { Value =  "CNETNightClub_18", Text = "CNETNightClub_18" },
            new SelectListItem { Value =  "CNETNOName1_19", Text = "CNETNOName1_19" },
            new SelectListItem { Value =  "CNETRedFlower_20", Text = "CNETRedFlower_20" },
            new SelectListItem { Value =  "CNETWallPaper_21", Text = "CNETWallPaper_21" },
            new SelectListItem { Value =  "CNETWood_22", Text = "CNETWood_22" },
            new SelectListItem { Value =  "CNETWorld_23", Text = "CNETWorld_23" },
            new SelectListItem { Value =  "NOName1_24", Text = "NOName1_24" },
            new SelectListItem { Value =  "UNNamedOct1_25", Text = "UNNamedOct1_25" },
            new SelectListItem { Value =  "UNNamedOct2_26", Text = "UNNamedOct2_26" },
            new SelectListItem { Value =  "UNNamedOct3_27", Text = "UNNamedOct3_27" },



        };

        public List<SelectListItem> default_PaymentS { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "Cash", Text = "Cash" },
            new SelectListItem { Value =  "Credit", Text = "Credit" },
            new SelectListItem { Value =  "Visa_Card", Text = "Visa_Card" },
            new SelectListItem { Value =  "Debit_Card", Text = "Debit_Card" },
            new SelectListItem { Value =  "Master_Card", Text = "Master_Card" },
            new SelectListItem { Value =  "Cheque", Text = "Cheque" },
            new SelectListItem { Value =  "Mobile_Money", Text = "Mobile_Money" },
            new SelectListItem { Value =  "Food_Allowance", Text = "Food_Allowance" },

        };
        public List<SelectListItem> DefaultTransactionTypeS { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "CashSales", Text = "CashSales" },
            new SelectListItem { Value =  "CreditSales", Text = "CreditSales" }
        };
        public List<SelectListItem> langugerms { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "English", Text = "English" },
            new SelectListItem { Value =  "Amharic", Text = "Amharic" },
            new SelectListItem { Value =  "French", Text = "French" }
        };
        #endregion

        #region PMS Pos Setting

        public en1ShowOnlineBusinessRate pms_ShowOnlineBusinessRate { get; set; }
        public string pms_RoomServicePOS { get; set; }
        public string pmsneed_Credentials_For_Removal { get; set; }
        public string pmsdefault_Payment { get; set; }
        public float pmsaccuracy_Tolerance { get; set; }
        public float pmssummary_Difference_Tolerance { get; set; }
        public string pmscheck_Voucher_Integrity { get; set; }
        public string pmsclear_Held_Bill { get; set; }
        public string pmssummary_Voucher { get; set; }
        public string pmszeroing_Voucher { get; set; }
        public string pmsnavigatorType { get; set; }
        public string pmstheme_Change_dDate { get; set; }
        public string pmsaspectRatio { get; set; }
        public string pmschangeFrequency { get; set; }
        public string pmsallowAnimation { get; set; }
        public int pmsholdAmountPerPerson { get; set; }
        public int pmshold_Document_Life { get; set; }
        public string pmstableMandatory { get; set; }
        public string pmsWaiterMandatory { get; set; }
        public string pmsenableAutoEntry { get; set; }
        public string pmsenableRoomCharge { get; set; }
        public string pmsdailyControlCount { get; set; }
        public string pmsenableTouchScreen { get; set; }
        public string pmsinputCoverMandatory { get; set; }
        public string pmsenableOrderFunction { get; set; }
        public string pmsorder_Location { get; set; }
        public string pmscompanyTinMandatory { get; set; }
        public string pmsfiscalReconciliation { get; set; }
        public string pmsenableBillManagement { get; set; }
        public string pmsopenDrawerAfterPrint { get; set; }
        public string pmsenableVocalAssistance { get; set; }
        public string pmsrmsLanguage { get; set; }
        public string pmsdisableVoidOnRetrieve { get; set; }
        public string pmsclearHoldBeforeClosing { get; set; }
        public string pmsenableDocumentReference { get; set; }
        public string pmscheckAmountLimitForHold { get; set; }
        public string pmsenableDynamicVisualDisplay { get; set; }
        public string pmsallowmultipleInstanceOfTable { get; set; }
        public string pmsCheckSystemTimestamP { get; set; }
        public string pmsclosing_frequency { get; set; }
        public string pmsTrigger_Article_Removal { get; set; }
        public string pmsstation_Type { get; set; }
        public string pmspos_theme { get; set; }
        public string pmstable_Selection_Type { get; set; }
        public string pmswaiter_selection { get; set; }
        public string pmsorder_Verification_Method { get; set; }
        public string pmsdefault_Transaction_Type { get; set; }
        public string pmswelcome_Message_Rule { get; set; }
        public string pmsenable_cbe { get; set; }
        public string pmsdevicecode { get; set; }

        public List<SelectListItem> pmsdefault_PaymentS { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "Cash", Text = "Cash" },
            new SelectListItem { Value =  "Check", Text = "Check" },
            new SelectListItem { Value =  "CreditCard", Text = "CreditCard" },
            new SelectListItem { Value =  "DebitCard", Text = "DebitCard" },
            new SelectListItem { Value =  "GiftCard", Text = "GiftCard" }

        };
        #endregion

        #region SMS POS Setting
        public int sms_accuracy_Tolerance { get; set; }
        public string sms_check_Voucher_Integrity { get; set; }
        public string sms_closing_frequency { get; set; }
        public string sms_downLoad_EJ { get; set; }
        public string sms_Enable_Scale_Encryption { get; set; }
        public string sms_fiscalReconciliation { get; set; }
        public float sms_summary_Difference_Tolerance { get; set; }
        public string sms_summary_Voucher { get; set; }
        public string sms_xML_Reconciliation { get; set; }
        public string sms_zeroing_Voucher { get; set; }
        public string sms_clearHoldBeforeClosing { get; set; }
        public string sms_enableholdFunction { get; set; }
        public string sms_capture_Fiscal_Information { get; set; }
        public string sms_cash_Customer { get; set; }
        public string sms_day_closing { get; set; }
        public string sms_enable_cbe { get; set; }
        public string sms_enableDocumentReference { get; set; }
        public string sms_enable_GSL_Tax { get; set; }
        public string sms_enableValueFactory { get; set; }
        public string sms_item_selection { get; set; }
        public string sms_need_Credentials_For_Removal { get; set; }
        public string sms_pOS_Trigger_Article_Removal { get; set; }
        public string sms_printDiscountAdditionalChargePerLineItem { get; set; }
        public string sms_select_automatic_Shift_Period { get; set; }
        public string sms_pOS_Store { get; set; }
        public string sms_default_Payment { get; set; }
        public string sms_enableAutoEntry { get; set; }
        public string sms_openDrawerAfterPrint { get; set; }
        public int sms_article_code_length { get; set; }
        public int sms_article_code_start { get; set; }
        public int sms_code_length { get; set; }
        public int sms_decimal_position { get; set; }
        public int sms_value_length { get; set; }
        public int sms_value_start { get; set; }
        public string sms_enable_vocal_Assistance { get; set; }
        public string sms_language { get; set; }
        public string sms_devicecode { get; set; }
        public string sms_ignorezeropricearticle { get; set; }
        public enrecuIgnorezeroprice_rticle sms_POSUseOnlyAvailableArticle { get; set; }
        public enrecuIgnorezeroprice_rticle sms_Currency_Conversion { get; set; }
        public enrecuIgnorezeroprice_rticle sms_EnableCustomerSelection { get; set; }
        public enValueRule sms_POSValueRule { get; set; }
        public enNeedCredentials sms_NeedCredentialsForCustomerDiscount { get; set; }
        public enBeneficiaryUser sms_BeneficiaryUser { get; set; }



        public List<SelectListItem> itemselection { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "Code", Text = "Code" },
            new SelectListItem { Value =  "All", Text = "All" }

        };

        public List<SelectListItem> smsdefault_PaymentS { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "Cash", Text = "Cash" },
            new SelectListItem { Value =  "Credit", Text = "Credit" },
            new SelectListItem { Value =  "Visa_Card", Text = "Visa_Card" },
            new SelectListItem { Value =  "Debit_Card", Text = "Debit_Card" },
            new SelectListItem { Value =  "Master_Card", Text = "Master_Card" },
            new SelectListItem { Value =  "Cheque", Text = "Cheque" },
            new SelectListItem { Value =  "Mobile_Money", Text = "Mobile_Money" }

        };

        #endregion

        #region Arcade pos
        public int arcade_accuracy_Tolerance { get; set; }
        public string arcade_DefaultArticle { get; set; }
        public string arcade_arcadeDBInstance { get; set; }
        public string arcade_arcadeDBPassword{ get; set; }
        public string arcade_arcadeDBUser { get; set; }
        public string arcade_check_Voucher_Integrity { get; set; }
        public string arcade_closing_frequency { get; set; }
        public string arcade_downLoad_EJ { get; set; }
        public string arcade_fiscalReconciliation { get; set; }
        public float arcade_summary_Difference_Tolerance { get; set; }
        public string arcade_summary_Voucher { get; set; }
        public string arcade_zeroing_Voucher { get; set; }
        public string arcade_capture_Fiscal_Information { get; set; }
        public string arcade_printDiscountAdditionalChargePerLineItem { get; set; }
        public string arcade_select_automatic_Shift_Period { get; set; }
        public string arcade_default_Payment { get; set; }
        public string arcade_openDrawerAfterPrint { get; set; }
        public int arcade_value_length { get; set; }
        public int arcade_value_start { get; set; }
        public string arcade_enable_vocal_Assistance { get; set; }
        public string arcade_language { get; set; }
        public string arcade_devicecode { get; set; }
        public enBeneficiaryUser arcade_BeneficiaryUser { get; set; }
        #endregion


        #region Access control Pos

        public float acc_display_Article { get; set; }
        public float acc_force_Open_Relay { get; set; }

        public float acc_AccuracyTolerance { get; set; }
        public float acc_SummaryDifferenceTolerance { get; set; }

        public string acc_pOS_Camera { get; set; }
        public string access_Control_Device { get; set; }
        public string acc_oCR_Data_Path { get; set; }
        public string acc_detector_Xml_Path { get; set; }



        public string acc_down_Load_EJ { get; set; }
        public string access_zeroing_Voucher { get; set; }
        public string access_summary_Voucher { get; set; }
        public string acc_schedule_Article { get; set; }
        public string access_xML_Reconciliation { get; set; }
        public string acc_fiscalReconciliation { get; set; }
        public string acc_check_Voucher_Integrity { get; set; }
        public string access_capture_Fiscal_Information { get; set; }
        public string access_selectAutomaticShiftPeriod { get; set; }
        public string access_PrintDiscountAdditionalChargePerLineItem { get; set; }


        public string acc_door_Sensor { get; set; }
        public string acc_pos_store { get; set; }
        public string acc_camera_Option { get; set; }
        public string acc_ignorezeropricearticle { get; set; }
        public string access_closing_Frequency { get; set; }
        public string aac_POSTriggerArticleRemoval { get; set; }
        public string aac_NeedCredentialsForRemoval { get; set; }
        public string access_devicecode { get; set; }
        public enValueRule access_POSValueRule { get; set; }
        public enNeedCredentials access_NeedCredentialsForCustomerDiscount { get; set; }
        public enBeneficiaryUser access_BeneficiaryUser { get; set; }

        public List<SelectListItem> cameraoption { get; set; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "SnapshootOnly", Text = "SnapshootOnly" },
            new SelectListItem { Value =  "SnapshootAndLPR", Text = "SnapshootAndLPR" },
            new SelectListItem { Value =  "NotApplicable", Text = "NotApplicable" }

        };
        public List<SelectListItem> doorSensor { get; set; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "None", Text = "None" },
            new SelectListItem { Value =  "r_0", Text = "r_0" },
            new SelectListItem { Value =  "r_1", Text = "r_1" },
            new SelectListItem { Value =  "r_2", Text = "r_2" },
            new SelectListItem { Value =  "r_3", Text = "r_3" }

        };

        public List<SelectListItem> need_Credentials_For_Removalsaccesses { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "Never", Text = "Never" },
            new SelectListItem { Value =  "Always", Text = "Always" }



        };

        public List<SelectListItem> pOS_Trigger_Article_Removalsaracceses { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "No", Text = "No" },
            new SelectListItem { Value =  "Always", Text = "Always" }


        };
        #endregion

        #region 
        public string mob_POSStore { get; set; }
        public string mob_DefaultGSLType { get; set; }
        public string mob_RemoteSyncPath { get; set; }
        public string mob_LocalXmlSyncPath { get; set; }
        public string mob_LocalXmlSilentPrintPath { get; set; }
        public string mob_LocalXmlSyncDumpyard { get; set; }
        public string mob_LocalXmlSyncSleepTimeInSecond { get; set; }
        public string mob_Launcherpassword { get; set; }
        public string mob_ReturnFsNo { get; set; }
        public string mob_TableMandatory { get; set; }
        public string mob_EnableRotation { get; set; }
        public string mob_WaiterMandatory { get; set; }
        public string mob_PopulateArticle { get; set; }
        public string mob_RealTimeDBAccess { get; set; }
        public string mob_XMLReconciliation { get; set; }
        public string mob_InputCoverMandatory { get; set; }
        public string mob_UseFlashBarcodeScan { get; set; }
        public string mob_DisableVoidOnRetrieve { get; set; }
        public string mob_NeedCredentialsForRemoval { get; set; }
        public string mob_SelectAutomaticShiftPeriod { get; set; }
        public string mob_AllowmultipleInstanceOfTable { get; set; }
        public string mob_MobileLauncherKey { get; set; }
        public string mob_DeviceStatusAPIURL { get; set; }
        public string mob_AskCustomerMaintenanceforwalkin { get; set; }
        public string mob_WebAPIType { get; set; }
        public string mob_CodeSearch { get; set; }
        public string mob_DeviceMode { get; set; }
        public string mob_StationType { get; set; }
        public string mob_RotationSetting { get; set; }
        public string mob_TableSelectionType { get; set; }
        public string mob_LocalXmlSyncType { get; set; }
        public string mob_XmlProtocolType { get; set; }
        public string mob_POSTriggerArticleRemoval { get; set; }
        public string mobliedevicecode { get; set; }
        public enrecuIgnorezeroprice_rticle mob_POSUseOnlyAvailableArticle { get; set; }
        public enValueRule mob_POSValueRule { get; set; }
        public enNeedCredentials mob_NeedCredentialsForCustomerDiscount { get; set; }
        public enNeedCredentials mob_Editonlynullfields { get; set; }
        public enNeedCredentials mob_EnableDataSync { get; set; }
        public enNeedCredentials mob_EnableOnlinPayment { get; set; }
        public enNeedCredentials mob_VoucherExtensionasCommercialMessage { get; set; }
        public enCustomerVisibility mob_CustomerVisibility { get; set; }
        public enBeneficiaryUser mob_BeneficiaryUser { get; set; }
        public enQRCodeDestination mob_QRCodeDestination { get; set; }
        
        public List<SelectListItem> codesearch { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "Numerical", Text = "Numerical" },
            new SelectListItem { Value =  "Alphabetical", Text = "Alphabetical" }


        };
        public List<SelectListItem> devicemode { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "Offline", Text = "Offline" },
            new SelectListItem { Value =  "Online", Text = "Online" }


        };
        public List<SelectListItem> mobRotationSetting { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "Portrait", Text = "Portrait" },
            new SelectListItem { Value =  "Landscape", Text = "Landscape" }


        };
        public List<SelectListItem> mobStationType { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "RMSOrder", Text = "RMSOrder" },
            new SelectListItem { Value =  "SMSOrder", Text = "SMSOrder" },
            new SelectListItem { Value =  "Sale", Text = "Sale" }


        };
        public List<SelectListItem> localsynkType { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "Automatic", Text = "Automatic" },
            new SelectListItem { Value =  "Mannual", Text = "Mannual" }



        };
        public List<SelectListItem> mobWebAPIType { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "Type_1", Text = "Type_1" },
            new SelectListItem { Value =  "Type_2", Text = "Type_2" }



        };
        public List<SelectListItem> mobXmlProtocolType { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "Option_1", Text = "Option_1" },
            new SelectListItem { Value =  "Option_2", Text = "Option_2" },
            new SelectListItem { Value =  "Option_3", Text = "Option_3" },
            new SelectListItem { Value =  "Option_4", Text = "Option_4" },
            new SelectListItem { Value =  "Option_5", Text = "Option_5" }



        };

        #endregion

        #region Cinema POS setting

        public string cinema_LogPath { get; set; }
        public string cinema_POSStore { get; set; }

        public int cinema_TimeoutInSeconds { get; set; }

        public float cinema_AccuracyTolerance { get; set; }
        public float cinema_SummaryDifferenceTolerance { get; set; }
        public string cinma_DeviceStatusAPIURL { get; set; }
        public string cinema_FandB { get; set; }
        public string cinema_DownLoadEJ { get; set; }
        public string cinema_EnableOrder { get; set; }
        public string cinema_EnableItemPOS { get; set; }
        public string cinema_ZeroingVoucher { get; set; }
        public string cinema_SummaryVoucher { get; set; }
        public string cinema_FlexibleQuantity { get; set; }
        public string cinema_EnableCashPayment { get; set; }
        public string cinema_XMLReconciliation { get; set; }
        public string cinema_EnableMobilePayment { get; set; }
        public string cinema_FiscalReconciliation { get; set; }
        public string cinema_CheckVoucherIntegrity { get; set; }
        public string cinema_OrderStationMandatory { get; set; }
        public string cinema_RemoveOutdatedSchedule { get; set; }
        public string cinema_CaptureFiscalInformation { get; set; }
        public string cinema_SelectAutomaticShiftPeriod { get; set; }
        public string cinema_PrintDiscountAdditionalChargePerLineItem { get; set; }
        public string cinema_SavingOrder { get; set; }
        public string cinema_ClosingFrequency { get; set; }
        public string cinema_POSTriggerArticleRemoval { get; set; }
        public string cinema_NeedCredentialsForRemoval { get; set; }
        public string cinemadevicecode { get; set; }
        public enNeedCredentials cinema_ignorezeropricearticle { get; set; }
        public string cinema_API_Base_UR { get; set; }
        public enrecuIgnorezeroprice_rticle cinma_POSUseOnlyAvailableArticle { get; set; }
        public enrecuIgnorezeroprice_rticle cinma_EnableOnlinePayment { get; set; }  
        public enrecuIgnorezeroprice_rticle cinma_FiscalReconciliation { get; set; }
        public enCinemaStationType cinma_CinemaStationType { get; set; }


        public enValueRule cinma_POSValueRule { get; set; }
        public enNeedCredentials cinma_NeedCredentialsForCustomerDiscount { get; set; }
        public enBeneficiaryUser cinma_BeneficiaryUser { get; set; }
        public enQRCodeDestination cinma_QRCodeDestination { get; set; }
        public List<SelectListItem> cinemaSavingOrder { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "Print_And_Save", Text = "Print_And_Save" },
            new SelectListItem { Value =  "Save_And_Print", Text = "Save_And_Print" }


        };

        #endregion


        #region Transportation POS setting

        public string trans_LogPath { get; set; }
        public string trans_POSStore { get; set; }

        public int trans_TimeoutInSeconds { get; set; }

        public float trans_AccuracyTolerance { get; set; }
        public float trans_SummaryDifferenceTolerance { get; set; }

        public string trans_FandB { get; set; }
        public string trans_DownLoadEJ { get; set; }
        public string trans_EnableOrder { get; set; }
        public string trans_EnableItemPOS { get; set; }
        public string trans_ZeroingVoucher { get; set; }
        public string trans_SummaryVoucher { get; set; }
        public string trans_FlexibleQuantity { get; set; }
        public string trans_EnableCashPayment { get; set; }
        public string trans_XMLReconciliation { get; set; }
        public string trans_EnableMobilePayment { get; set; }
        public string trans_FiscalReconciliation { get; set; }
        public string trans_CheckVoucherIntegrity { get; set; }
        public string trans_OrderStationMandatory { get; set; }
        public string trans_RemoveOutdatedSchedule { get; set; }
        public string trans_CaptureFiscalInformation { get; set; }
        public string trans_DeviceStatusAPIURL { get; set; }
        public enCinemaStationType trans_CinemaStationType { get; set; }
        public string trans_SelectAutomaticShiftPeriod { get; set; }
        public string trans_PrintDiscountAdditionalChargePerLineItem { get; set; }
        public string trans_SavingOrder { get; set; }
        public string trans_ClosingFrequency { get; set; }
        public string trans_POSTriggerArticleRemoval { get; set; }
        public string trans_NeedCredentialsForRemoval { get; set; }
        public string transadevicecode { get; set; }
        public string trans_ignorezeropricearticle { get; set; }
        public string trans_API_Base_UR { get; set; }
        public enrecuIgnorezeroprice_rticle trans_POSUseOnlyAvailableArticle { get; set; }
        public enrecuIgnorezeroprice_rticle trans_EnableOnlinePayment { get; set; }
        public enQRCodeDestination trans_QRCodeDestination { get; set; }
        public enValueRule trans_POSValueRule { get; set; }
        public enNeedCredentials trans_NeedCredentialsForCustomerDiscount { get; set; }
        public enBeneficiaryUser trans_BeneficiaryUser { get; set; }



        public List<SelectListItem> transSavingOrder { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "Print_And_Save", Text = "Print_And_Save" },
            new SelectListItem { Value =  "Save_And_Print", Text = "Save_And_Print" }
        };



        #endregion

        #region NVS POS Setting
        public string nvs_POSStore = "";
        public string nvs_CashCustomer = "";

        public int nvs_AccuracyTolerance { get; set; }

        public float nvs_SummaryDifferenceTolerance { get; set; }

        public string nvs_DownLoadEJ { get; set; }
        public string nvs_SummaryVoucher { get; set; }
        public string nvs_ZeroingVoucher { get; set; }
        public string nvs_XMLReconciliation { get; set; }
        public string nvs_FiscalReconciliation { get; set; }
        public string nvs_CheckVoucherIntegrity { get; set; }
        public string nvs_Ignorezeropricearticle { get; set; }
        public string nvs_CaptureFiscalInformation { get; set; }
        public string nvs_NeedCredentialsForRemoval { get; set; }
        public string nvs_SelectAutomaticShiftPeriod { get; set; }
        public string nvs_PrintDiscountAdditionalChargePerLineItem { get; set; }
        public string nvs_ClosingFrequency { get; set; }
        public string nvs_DefaultPayment { get; set; }
        public string nvsdevicecode { get; set; }
        public string nvs_DeviceStatusAPIURL { get; set; }
        public enrecuIgnorezeroprice_rticle nvs_POSUseOnlyAvailableArticle { get; set; }
        public enrecuIgnorezeroprice_rticle nvs_EnableOnlinePayment { get; set; }
        public enrecuIgnorezeroprice_rticle nvs_UseOnlyCurrentBranch { get; set; }
        public enrecuIgnorezeroprice_rticle nvs_EnableItemSelection { get; set; }
        public enrecuIgnorezeroprice_rticle nvs_PrintBatch { get; set; }
        public enrecuIgnorezeroprice_rticle nvs_UseAutomaticLifespan { get; set; }
        public enrecuIgnorezeroprice_rticle nvs_EnableSerialNumberLifeSpan { get; set; }
        public enSerialMovementSuggestion nvs_SerialMovementSuggestion { get; set; }
        public enrecuIgnorezeroprice_rticle nvs_PrintExpiryDate { get; set; }
        public enSerialInputType nvs_SerialInputType { get; set; }
        public enrecuIgnorezeroprice_rticle nvs_LockStoreSelection { get; set; }
        public enValueRule nvs_POSValueRule { get; set; }
        public enNeedCredentials nvs_NeedCredentialsForCustomerDiscount { get; set; }
        public enBeneficiaryUser nvs_BeneficiaryUser { get; set; }
        public enQRCodeDestination nvs_QRCodeDestination { get; set; }

        #endregion

        #region Amole Payment Setting
        //public string amole_registeredIpAddress { get; set; }


        public string amodeldevicecode { get; set; }
        public string amole_merchantId { get; set; }
        public string amole_apiKey { get; set; }
        public string amole_apiUsername { get; set; }
        public string amole_apiPassword { get; set; }
        public string amole_paymentApiEndpointUrl { get; set; }
        public string amole_serviceApiEndpointUrl { get; set; }
        public string amole_vendorAccountId { get; set; }
        public string amole_logoption { get; set; }
        #endregion

        #region tel birr
        public string telDefvalue { get; set; }
        public string tel_applicationCode { get; set; }
        public string tel_authorizationEndpointUrl { get; set; }
        public string tel_operatorId { get; set; }
        public string tel_password { get; set; }
        public string tel_transactionEndpointUrl { get; set; }
        public string tel_securityCredential { get; set; }
        public string tel_thirdPartyId { get; set; }
        public string tel_resultCallbackUrl { get; set; }
        public string tel_receiverShortCode { get; set; }
        public string telenLogoption { get; set; }
        public string telbrdevicecode { get; set; }
        public string telshortcode { get; set; }
        public string telPrefcode { get; set; }


        public List<SelectListItem> enLogoption { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "NotApplicable", Text = "NotApplicable" },
            new SelectListItem { Value =  "Information", Text = "Information" },
            new SelectListItem { Value =  "Debug", Text = "Debug" },
            new SelectListItem { Value =  "Warning", Text = "Warning" },
            new SelectListItem { Value =  "Error", Text = "Error" },
        };

        #endregion
        #region Fiscal Printer Setting
        public uint fiscal_FpCapacity { get; set; }
        public uint fiscal_NotifyBefore { get; set; }
        public int fiscal_MaximumZreport { get; set; }
        public int fiscal_BarcodeHeight { get; set; }
        public int fiscal_ArticleDescriptionLength { get; set; }

        public string fiscal_EJPath { get; set; }
        public string fiscal_LogFilePath { get; set; }


        public string fiscal_PrintBarcode { get; set; }
        public string fiscal_PrintDuplicate { get; set; }
        public string fiscal_OperatorSignature { get; set; }
        public string fiscal_PrintTINAsCommercial { get; set; }
        public string fiscal_ShowFiscalReceiptHeader { get; set; }


        public string fiscal_LogFileFormat { get; set; }
        public string fiscal_UseAutomaticAttachment { get; set; }
        public string fiscaldevicecode { get; set; }
        public IFormFile fill { get; set; }
        public enBarcodeSize fiscal_Barcodesize { get; set; }
        public enGrandTotal fiscal_GrandTotal { get; set; }
        public enGrandTotal fiscal_UnitPrice { get; set; }
        public enGrandTotal fiscal_UnitQuantity { get; set; }


        public List<SelectListItem> fiscalLogFileFormat { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "Hex", Text = "Hex" },
            new SelectListItem { Value =  "Dec", Text = "Dec" },
            new SelectListItem { Value =  "ASCII", Text = "ASCII" }
        };
        public List<SelectListItem> fiscaluseautomation { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "Always", Text = "Always" },
            new SelectListItem { Value =  "WhenCustomerSelected", Text = "WhenCustomerSelected" },
            new SelectListItem { Value =  "Never", Text = "Never" }
        };
        #endregion
        #region Station Map Setting
        public string order_DefaultStore { get; set; }
        public string orderprinterdevicecode { get; set; }
        #endregion

        #region door lock Setting

        public int door_EncoderAddress { get; set; }
        public string door_UnlockDeadbolt { get; set; }
        public string door_UnlockpublicDoors { get; set; }
        public string door_Host { get; set; }
        public string door_User { get; set; }
        public string door_SystemPassword { get; set; }
        public string door_TMENcoder { get; set; }
        public string door_AdelSystem { get; set; }
        public string door_AdelEncoderType { get; set; }
        public string door_BETECHEncoderType { get; set; }
        public string door_MOllYEncoderType { get; set; }
        public string door_DelunsEncoderType { get; set; }
        public string doordevicecode { get; set; }

        public List<SelectListItem> doorAdelEncoderType { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "Manual", Text = "Manual" },
            new SelectListItem { Value =  "Automatic", Text = "Automatic" }
        };
        public List<SelectListItem> doorAdelSystem { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "Lock3200", Text = "Lock3200" },
            new SelectListItem { Value =  "Lock3200K", Text = "Lock3200K" },
            new SelectListItem { Value =  "Lock4200", Text = "Lock4200" },
            new SelectListItem { Value =  "Lock4200D", Text = "Lock4200D" },
            new SelectListItem { Value =  "Lock5200", Text = "Lock5200" },
            new SelectListItem { Value =  "Lock6200", Text = "Lock6200" },
            new SelectListItem { Value =  "Lock7200", Text = "Lock7200" },
            new SelectListItem { Value =  "Lock7200D", Text = "Lock7200D" },
            new SelectListItem { Value =  "Lock9200", Text = "Lock9200" },
            new SelectListItem { Value =  "Lock9200T", Text = "Lock9200T" },
            new SelectListItem { Value =  "A30", Text = "A30" },
            new SelectListItem { Value =  "A50", Text = "A50" },
            new SelectListItem { Value =  "A90", Text = "A90" },
            new SelectListItem { Value =  "A92", Text = "A92" },
            new SelectListItem { Value =  "A93", Text = "A93" }
        };
        public List<SelectListItem> doorTmencoder { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "DS9097U", Text = "DS9097U" },
            new SelectListItem { Value =  "DS9097E", Text = "DS9097E" }
        };

        public List<SelectListItem> doorBETECHEncoderType { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "RW_21", Text = "RW_21" },
            new SelectListItem { Value =  "RW_33", Text = "RW_33" },
            new SelectListItem { Value =  "RW_26B", Text = "RW_26B" },
            new SelectListItem { Value =  "RW_41", Text = "RW_41" }
        };
        public List<SelectListItem> doorDelunsEncoderType { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "USB", Text = "USB" },
            new SelectListItem { Value =  "proUSB", Text = "proUSB" }
        };
        public List<SelectListItem> doorMOllYEncoderType { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "RF_57", Text = "RF_57" },
            new SelectListItem { Value =  "RF_50", Text = "RF_50" }
        };
        #endregion

        #region access controller Setting
        public int contro_ExitTreshhold { get; set; }
        public int contro_NotifyBeforeDate { get; set; }
        public string contro_card_despensorversion { get; set; }
        public string contro_CameraOption { get; set; }
        public string contro_AccesscontrolType { get; set; }
        public string contro_GateType { get; set; }
        public string contro_SubscriptionTransaction { get; set; }
        public string contro_AuthenticationType { get; set; }
        public string contro_PackageCategory { get; set; }
        public string controdevicecode { get; set; }
        public enNeedCredentials contro_AllowMobileAppAccess { get; set; }


        public List<SelectListItem> controAccesscontrolType { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "Parking", Text = "Parking" },
            new SelectListItem { Value =  "Membership", Text = "Membership" }
        };
        public List<SelectListItem> controAuthencontrolType { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "Single_card_recognition", Text = "Single_card_recognition" },
            new SelectListItem { Value =  "Card_and_password", Text = "Card_and_password" },
            new SelectListItem { Value =  "Timing_open_closed", Text = "Timing_open_closed" },
            new SelectListItem { Value =  "Double_card_recognition", Text = "Double_card_recognition" },
            new SelectListItem { Value =  "Timing_alarm", Text = "Timing_alarm" },
            new SelectListItem { Value =  "Password", Text = "Password" },
            new SelectListItem { Value =  "PasswCard_or_passwordord", Text = "Card_or_password" },
            new SelectListItem { Value =  "First_card_open_the_door", Text = "First_card_open_the_door" },
            new SelectListItem { Value =  "Enable_the_anti_pass_function", Text = "Enable_the_anti_pass_function" }
        };
        public List<SelectListItem> controCameraOption { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "SnapshootOnly", Text = "SnapshootOnly" },
            new SelectListItem { Value =  "SnapshootAndLPR", Text = "SnapshootAndLPR" },
            new SelectListItem { Value =  "NotApplicable", Text = "NotApplicable" }
        };
        public List<SelectListItem> controGateType { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "Entry", Text = "Entry" },
            new SelectListItem { Value =  "Exit", Text = "Exit" },
        };
        public List<SelectListItem> controcard_despensorversion { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "V.2.13", Text = "V.2.13" },
            new SelectListItem { Value =  "V.3.19", Text = "V.3.19" },
        };
        public List<SelectListItem> controcardsubscription { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "Sales", Text = "Sales" },
            new SelectListItem { Value =  "SalesOrder", Text = "SalesOrder" },
        };
        #endregion

        #region order station mapping
        public string articleType { get; set; }
        public int posDevice { get; set; }
        public int navType { get; set; }
        public int stationDevice { get; set; }
        public int articleTypecode { get; set; }
        public string articlegslTypecode { get; set; }
        public string preferencefcode { get; set; }
        public string preferenceparent { get; set; }
        public string preferencedescription { get; set; }
        public int[] Orderstationchecklist { get; set; }

        public string[] Orderstationallchecklist { get; set; }
        public string[] Orderstationunchecklistcode { get; set; }

        public string posdevicecodeTYpe { get; set; }
        public string orderstationdevicecodeTYpe { get; set; }

        public string gsltypeForArticle { get; set; }
        public int prefererenceList { get; set; }
        public List<PreferenceoDTO> _prfOrderstn { get; set; }

        #endregion

        #region IP TV Media
        public int media_index { get; set; }
        public int media_indexTemp { get; set; }
        public string media_name { get; set; }
        public string media_description { get; set; }
        public string media_category { get; set; }
        public string media_stream_url { get; set; }
        public string media_trailer { get; set; }
        public string media_logo_poster { get; set; }
        public string media_article { get; set; }
        public bool media_active { get; set; }
        public string media_remark { get; set; }
        public double? media_location_lat { get; set; }
        public double? media_locaiton_long { get; set; }
        public string mediaupdatecode { get; set; }
        public string mediaType { get; set; }
        #endregion
        #region IP TV Movie
        public string movie_medial { get; set; }
        public DateTime movie_release_date { get; set; }
        public string movie_duration { get; set; }
        public string movie_director { get; set; }
        public string movie_actors { get; set; }
        public string movie_trailer { get; set; }
        public string movie_plot { get; set; }
        public string movie_remark { get; set; }
        public string movieupdatecode { get; set; }
        #endregion
        #region Menu Desiner 
        public string menu_package_name { get; set; }
        public string menudesignerUpdatecode { get; set; }
        public string menu_category { get; set; }
        public List<string> menu_code { get; set; }
        public List<int> menu_count { get; set; }
        public int menu_count_Item { get; set; }
        public List<int> sncount { get; set; }
        public List<string> menutype { get; set; }
        public string menu_remark { get; set; }
        #endregion
        #region E-birr
        public string e_apiKey { get; set; }
        public string e_apiUserId { get; set; }
        public string e_merchantId { get; set; }
        public string e_transactionUrl { get; set; }
        public string e_devicecode { get; set; }


        #endregion 
        #region Sahay
        public string sa_consumerKey { get; set; }
        public string sa_consumerSecret { get; set; }
        public string sa_baseUrl { get; set; }
        public string sa_devicecode { get; set; }


        #endregion

        #region Reference POS Setting
        public string ref_CodeLength { get; set; }
        public string ref_ArticleCodeStart { get; set; }
        public string ref_ArticleCodeLength { get; set; }
        public string ref_ValueStart { get; set; }
        public string ref_ValueLength { get; set; }
        public string ref_DecimalPosition { get; set; }
        public string ref_Defvalue { get; set; }
        public string ref_POSStore { get; set; }
        public string ref_DayClosing { get; set; }
        public string ref_XmlDampYard { get; set; }
        public string ref_CashCustomer { get; set; }
        public string ref_ErrorXmlPath { get; set; }
        public string ref_SilentPrintXmlPath { get; set; }
        public float ref_AccuracyTolerance { get; set; }
        public float ref_SummaryDifferenceTolerance { get; set; }
        public string ref_EnableCBE { get; set; }
        public string ref_DownLoadEJ { get; set; }
        public string ref_EnableGSLTax { get; set; }
        public string ref_ReturnFSNumber { get; set; }
        public bool ref_ZeroingVoucher { get; set; }
        public string ref_SummaryVoucher { get; set; }
        public string ref_EnableAutoEntry { get; set; }
        public string ref_XMLReconciliation { get; set; }
        public string ref_EnableValueFactory { get; set; }
        public string ref_UseTotalValuefactor { get; set; }
        public string ref_EnableOnlinePayment { get; set; }
        public string ref_EnableXmlSilentPrint { get; set; }
        public string ref_OpenDrawerAfterPrint { get; set; }
        public string ref_FiscalReconciliation { get; set; }
        public string ref_CheckVoucherIntegrity { get; set; }
        public string ref_EnableScaleEncryption { get; set; }
        public string ref_EnableVocalAssistance { get; set; }
        public string ref_ClearHoldBeforeClosing { get; set; }
        public string ref_EnableDocumentReference { get; set; }
        public string ref_EnableHoldFunctionality { get; set; }
        public string ref_CaptureFiscalInformation { get; set; }
        public string ref_SelectAutomaticShiftPeriod { get; set; }
        public string ref_Identicaluserauthentication { get; set; }
        public string ref_PrintDiscountAdditionalChargePerLineItem { get; set; }
        public string refdevicecode { get; set; }
        public enLanguage ref_Language { get; set; }
        public enDefaultPayment ref_DefaultPayment { get; set; }
        public enClosingFrequency ref_ClosingFrequency { get; set; }
        public enItemSelection ref_ItemSelection = enItemSelection.All;
        public enPOSTriggerArticleRemoval ref_POSTriggerArticleRemoval { get; set; }
        public enNeedCredentialsForRemoval ref_NeedCredentialsForRemoval { get; set; }
        public enQRCodeDestination ref_QRCodeDestination { get; set; }
        public enIgnorezeroprice_rticle ref_Ignore_zero_price_article { get; set; }
        public enIgnorezeroprice_rticle ref_POSUseOnlyAvailableArticle { get; set; }
        public enValueRule ref_POSValueRule { get; set; }
        public enNeedCredentials ref_NeedCredentialsForCustomerDiscount { get; set; }
        public enBeneficiaryUser ref_BeneficiaryUser { get; set; }


        #endregion

        #region Recurring POS Setting
        public string recu_ValueStart { get; set; }
        public string recu_ValueLength { get; set; }
        public string recu_CodeLength { get; set; }
        public string recu_DecimalPosition { get; set; }
        public string recu_ArticleCodeStart { get; set; }
        public string recu_ArticleCodeLength { get; set; }
        public string recu_Defvalue { get; set; }
        public string recu_POSStore { get; set; }
        public string recu_DayClosing { get; set; }
        public string recu_CashCustomer { get; set; }
        public string recu_EnableCBE { get; set; }
        public string recu_DownLoadEJ { get; set; }
        public string recu_EnableGSLTax { get; set; }
        public string recu_ZeroingVoucher { get; set; }
        public string recu_SummaryVoucher { get; set; }
        public string recu_EnableAutoEntry { get; set; }
        public string recu_XMLReconciliation { get; set; }
        public string recu_EnableValueFactory { get; set; }
        public string recu_EnableOnlinePayment { get; set; }
        public string recu_OpenDrawerAfterPrint { get; set; }
        public string recu_FiscalReconciliation { get; set; }
        public string recu_EnableVocalAssistance { get; set; }
        public string recu_EnableScaleEncryption { get; set; }
        public string recu_CheckVoucherIntegrity { get; set; }
        public string recu_ClearHoldBeforeClosing { get; set; }
        public string recu_EnableHoldFunctionality { get; set; }
        public string recu_EnableDocumentReference { get; set; }
        public string recu_CaptureFiscalInformation { get; set; }
        public string recu_SelectAutomaticShiftPeriod { get; set; }
        public float recu_AccuracyTolerance { get; set; }
        public float recu_SummaryDifferenceTolerance { get; set; }
        public string recu_PrintDiscountAdditionalChargePerLineItem { get; set; }
        public string recu_devicecode { get; set; }
        public string recu_DeviceStatusAPIURL { get; set; }
        public enrecuLanguage recu_Language { get; set; }
        public enrecuDefaultPayment recu_DefaultPayment { get; set; }
        public enrecuClosingFrequency recu_ClosingFrequency { get; set; }
        public enrecuItemSelection recu_ItemSelection = enrecuItemSelection.All;
        public enrecuPOSTriggerArticleRemoval recu_POSTriggerArticleRemoval { get; set; }
        public enrecuNeedCredentialsForRemoval recu_NeedCredentialsForRemoval { get; set; }
        public enrecuQRCodeDestination recu_QRCodeDestination { get; set; }
        public enrecuIgnorezeroprice_rticle recu_Ignore_zero_price_article { get; set; }
        public enrecuIgnorezeroprice_rticle recu_POSUseOnlyAvailableArticle { get; set; }
        public enValueRule recu_POSValueRule { get; set; }
        public enNeedCredentials recu_NeedCredentialsForCustomerDiscount { get; set; }
        public enBeneficiaryUser recu_BeneficiaryUser { get; set; }


        #endregion
        #region Premium RMS POS Setting

        public string prem_EnableOnlinePayment { get; set; }
        public string prem_EnableTable { get; set; }
        public string prem_EnableTab { get; set; }
        public string prem_EnableFastTransaction { get; set; }
        public string prem_EnableTakeaway { get; set; }
        public string prem_EnableDelivery { get; set; }
        public string prem_EnableRoomService { get; set; }
        public string prem_EnablePackageDesign { get; set; }
        #region Begin Table
        public string prem_TableSelectwaiterbeforeopen { get; set; }
        public string prem_TableSelectcoverbeforeopen { get; set; }
        public string prem_TableApplicableservicecharge { get; set; }
        public string prem_TableApplicableDiscount { get; set; }
        public string prem_TableAutoadditem { get; set; }
        public string prem_TableDefaultcategory { get; set; }

        #endregion

        #region Begin Tab
        public string prm_TabSelectwaiterbeforeopen { get; set; }
        public string prem_TabSelectcoverbeforeopen { get; set; }
        public string prem_TabApplicableservicecharge { get; set; }
        public string prem_TabApplicableDiscount { get; set; }
        public string prem_TabAutoadditem { get; set; }
        public string prem_TabDefaultcategory { get; set; }

        #endregion

        #region BeginFastTransaction
        public string prme_FastTransactionSelectwaiterbeforeopen { get; set; }
        public string prem_FastTransactionSelectcoverbeforeopen { get; set; }
        public string prme_FastTransactionApplicableservicecharge { get; set; }
        public string prem_FastTransactionApplicableDiscount { get; set; }
        public string prem_FastTransactionAutoadditem { get; set; }
        public string prem_FastTransactionDefaultcategory { get; set; }

        #endregion

        #region BeginTakeaway
        public string prem_TakeawaySelectwaiterbeforeopen { get; set; }
        public string prem_TakeawaySelectcoverbeforeopen { get; set; }
        public string prem_TakeawayApplicableservicecharge { get; set; }
        public string prem_TakeawayApplicableDiscount { get; set; }
        public string prem_TakeawayAutoadditem { get; set; }
        public string prem_TakeawayDefaultcategory { get; set; }

        #endregion

        #region BeginDelivery
        public string prem_DeliverySelectwaiterbeforeopen { get; set; }
        public string prem_DeliverySelectcoverbeforeopen { get; set; }
        public string prem_DeliveryApplicableservicecharge { get; set; }
        public string prem_DeliveryApplicableDiscount { get; set; }
        public string prem_DeliveryAutoadditem { get; set; }
        public string prem_DeliveryDefaultcategory { get; set; }

        #endregion

        #region BeginRoomService
        public string prem_RoomServiceSelectwaiterbeforeopen { get; set; }
        public string prem_RoomServiceSelectcoverbeforeopen { get; set; }
        public string prem_RoomServiceApplicableservicecharge { get; set; }
        public string prem_RoomServiceApplicableDiscount { get; set; }
        public string prem_RoomServiceAutoadditem { get; set; }
        public string prem_RoomServiceDefaultcategory { get; set; }


        #endregion
        public string prem_POSStore { get; set; }
        public string prem_PriceType { get; set; }
        public string prem_CashCustomer { get; set; }
        public string prem_HoldDocumentLife { get; set; }
        public string prem_HoldAmountPerPerson { get; set; }
        public string prem_EnableOrderFunction { get; set; }
        public string prem_EnableBillManagement { get; set; }
        public string prem_OpenDrawerAfterPrint { get; set; }
        public string prem_OrderStationMandatory { get; set; }
        public string prem_DisableVoidOnRetrieve { get; set; }
        public string prem_EnableDeliveryManagement { get; set; }
        public string prem_CaptureFiscalInformation { get; set; }
        public string prem_SelectAutomaticShiftPeriod { get; set; }
        public string prem_AllowmultipleInstanceOfTable { get; set; }
        public string prem_PrintDiscountAdditionalChargePerLineItem { get; set; }
        public string prem_ClearHeldBill { get; set; }
        public string prem_DownLoadEJ { get; set; }
        public string prem_SummaryVoucher { get; set; }
        public string prem_ZeroingVoucher { get; set; }
        public string prem_FiscalReconciliation { get; set; }
        public string prem_CheckVoucherIntegrity { get; set; }
        public float prem_SummaryDifferenceTolerance { get; set; }
        public float prem_AccuracyTolerance { get; set; }
        public string prem_devicecode { get; set; }

        public enrecuIgnorezeroprice_rticle prem_Ignore_zero_price_article { get; set; }
        public enrecuIgnorezeroprice_rticle prem_POSUseOnlyAvailableArticle { get; set; }

        public enpremDefaultPayment prem_DefaultPayment { get; set; }
        public enpremClosingFrequency prem_ClosingFrequency { get; set; }
        public enTableSelectionType prem_deliveryTableSelectionType { get; set; }
        public enTableSelectionType prem_FastTransactionTableselectiontype { get; set; }
        public enpremDefaultTransactionType prem_DefaultTransactionType { get; set; }
        public enOrderVerificationMethod prem_OrderVerificationMethod { get; set; }
        public enpremPOSTriggerArticleRemoval prem_POSTriggerArticleRemoval { get; set; }
        public enpremQRCodeDestination prem_QRCodeDestination { get; set; }
        public enSelectseatpositiontype prme_FastTransactionSelectseatpostionType { get; set; }
        public enClosingDocumentType _ClosingDocumentType = enClosingDocumentType.CashSales;
        public enStationType prem_StationType = enStationType.Sale;
        public enSelectseatpositiontype prem_DeliverySelectseatpositiontype { get; set; }
        public enTableSelectionType prem_RoomServiceTableSelectionType { get; set; }
        public enTableSelectionType prem_TabTableselectiontype { get; set; }
        public enTableSelectionType prem_TableTableselectiontype { get; set; }
        public enTableSelectionType prem_TakeawayTableselectiontype { get; set; }
        public enSelectseatpositiontype prem_RoomServiceSelectseatpositiontype { get; set; }
        public enSelectseatpositiontype prem_TabSelectseatpositiontype { get; set; }
        public enSelectseatpositiontype prem_TableSelectseatpositiontype { get; set; }
        public enSelectseatpositiontype prem_TakeawaySelectseatposition { get; set; }
        public enpremNeedCredentialsForRemoval prem_NeedCredentialsForRemoval { get; set; }
        public enValueRule prem_POSValueRule { get; set; }
        public enNeedCredentials prem_NeedCredentialsForCustomerDiscount { get; set; }
        public enBeneficiaryUser prem_BeneficiaryUser { get; set; }

        #endregion

        #region Reservation POS Setting
        public string resv_POSStore { get; set; }
        public string resv_CashCustomer { get; set; }
        public float resv_AccuracyTolerance { get; set; }
        public float resv_SummaryDifferenceTolerance { get; set; }
        public string resv_DownLoadEJ { get; set; }
        public string resv_SummaryVoucher { get; set; }
        public string resv_ZeroingVoucher { get; set; }
        public string resv_XMLReconciliation { get; set; }
        public string resv_FiscalReconciliation { get; set; }
        public string resv_CheckVoucherIntegrity { get; set; }
        public string resv_CaptureFiscalInformation { get; set; }
        public string resv_NeedCredentialsForRemoval { get; set; }
        public string resv_SelectAutomaticShiftPeriod { get; set; }
        public string resv_devicecode { get; set; }
        public enreserClosingFrequency resv_ClosingFrequency { get; set; }
        public enreserDefaultPayment resv_DefaultPayment { get; set; }
        public enreserPOSTriggerArticleRemoval resv_POSTriggerArticleRemoval { get; set; }


        #endregion
        #region Camare Setting
        public enType Camera_type { get; set; }
        public string Camera_Username { get; set; }
        public string Camera_password { get; set; }
        public string Camera_Image_Archieves { get; set; }
        public string Camera_Source_URl { get; set; }

        #endregion
    }
    #region Reference POS Setting
    public class DeviceView
    {
        public string name { get; set; }
        public int code { get; set; }
        public int? article { get; set; }
        public int? connectionType { get; set; }
        public int? host { get; set; }
        public string preference { get; set; }
        public bool isActive { get; set; }
        public string remark { get; set; }
    } 
    
    public enum enQRCodeDestination
    {
        CustomerDisplay,
        Receipt,
        Both,
        notapplicable
    }
    public enum enNeedCredentialsForRemoval
    {
        Never,
        WhenHeld,
        Always
    }
    public enum enItemSelection
    {
        Code,
        All
    }
    public enum enPOSTriggerArticleRemoval
    {
        No,
        WhenHeld,
        Always
    }
    public enum enClosingFrequency
    {
        Shift,
        Day,
        Week,
        Month,
        Quarter,
        Year
    }
    public enum enLanguage
    {
        English,
        Amharic,
        French
    }
    public enum enDefaultPayment
    {
        Cash,
        Credit,
        Visa_Card,
        Debit_Card,
        Master_Card,
        Cheque,
        Mobile_Money
    }
    public enum enDefaultTransactionType
    {
        CashSales,
        CreditSales
    }
    public enum enIgnorezeroprice_rticle
    {
        False,
        True,
    }
    #endregion
    #region Recurring POS Setting
    public enum enrecuIgnorezeroprice_rticle
    {
        False ,
        True 
    }
    public enum enSerialMovementSuggestion
    {
        FIFO,
        LIFO
    }
    public enum enSerialInputType
    {
        NewInsert,
        Selection
    }
    public enum enrecuQRCodeDestination
    {
        CustomerDisplay,
        Receipt,
        Both,
        notapplicable
    }
    public enum enrecuNeedCredentialsForRemoval
    {
        Never,
        WhenHeld,
        Always
    }
    public enum enrecuItemSelection
    {
        Code,
        All
    }
    public enum enrecuPOSTriggerArticleRemoval
    {
        No,
        WhenHeld,
        Always
    }
    public enum enrecuClosingFrequency
    {
        Shift,
        Day,
        Week,
        Month,
        Quarter,
        Year
    }
    public enum enrecuLanguage
    {
        English,
        Amharic,
        French
    }
    public enum enrecuDefaultPayment
    {
        Cash,
        Credit,
        Visa_Card,
        Debit_Card,
        Master_Card,
        Cheque,
        Mobile_Money
    }
    public enum enrecuDefaultTransactionType
    {
        CashSales,
        CreditSales
    }
    #endregion

    #region Premium RMS POS Setting
    public enum enClosingDocumentType
    {
        CashSales,
        CashSalesSummary
    }
    public enum enpremQRCodeDestination
    {
        CustomerDisplay,
        Receipt,
        Both,
        notapplicable
    }
    public enum enStationType
    {
        Sale,
        Order
    }
    public enum enpremNeedCredentialsForRemoval
    {
        Never,
        WhenHeld,
        Always
    }
    public enum enpremPOSTriggerArticleRemoval
    {

        No,
        WhenHeld,
        Always
    }
    public enum enpremClosingFrequency
    {
        Shift,
        Day,
        Week,
        Month,
        Quarter,
        Year
    }
    public enum enpremDefaultPayment
    {
        Cash,
        Credit,
        Visa_Card,
        Debit_Card,
        Master_Card,
        Cheque,
        Mobile_Money,
        Food_Allowance

    }
    public enum enpremDefaultTransactionType
    {
        CashSales,
        CreditSales
    }
    public enum enOrderVerificationMethod
    {
        None,
        Token,
        Finger,
        Card
    }
    public enum enTableSelectionType
    {
        List,
        Keypad,
        notapplicable
    }

    public enum enSelectseatpositiontype
    {
        Popup,
        usenextseat,
        notapplicable
    }
    #endregion   #region Premium RMS POS Setting
    #region Reservation POS Setting
    public enum enreserPOSTriggerArticleRemoval
    {
        No,
        WhenHeld,
        Always
    }
    public enum enreserClosingFrequency
    {
        Shift,
        Day,
        Week,
        Month,
        Quarter,
        Year
    }
    public enum enreserDefaultPayment
    {
        Cash,
        Credit,
        Visa_Card,
        Debit_Card,
        Master_Card,
        Cheque,
        Mobile_Money

    }
    #endregion
    #region PMS POS Setting
    public enum en1ShowOnlineBusinessRate
    {
        False,
        True,
    }
    #endregion

    #region Fiscal Printer Setting
    public enum enBarcodeSize
    {
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10
    } 
    public enum enGrandTotal
    {
        r_9999_99,
        r_999999_99,
        r_9999999_99,
        r_99999999_99,
        r_999999999_99,
        r_9999999999_99,
        r_99999999999_99
           
    }
    #endregion
  
    public enum enValueRule
    {
        Fixed,
        Flexible,
        Alternative,
        NotApplicable
    }
    public enum enNeedCredentials
    {
        False,
        True
    }
    public enum enBeneficiaryUser
    {
        Cashier,
        Waiter,
        notapplicable
    }
    public enum enType
    {
        JPEG,
        MJPEG,
        FFMPEG
    }
    public enum enCustomerVisibility
    {
        Notapplicable,
        NearMe,
        DefinedRoute,
        DefinedRouteWithGPS
    }
    public enum enCinemaStationType
    {
        Cinema_POS,
        Redeeming_Station
    }
    public class PreferenceoDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int? ParentId { get; set; }
        public bool IsActive { get; set; }
    }
}

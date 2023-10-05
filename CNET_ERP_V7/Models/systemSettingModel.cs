using CNET_V7_Entities.DataModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CNET_ERP_V7.Models
{
    public class systemSettingModel
    {
        public int sysType { get; set; }
        public string  name { get; set; }
        public string  category { get; set; }
        public int  chlidcategory { get; set; }
        public int?  consineeCode { get; set; }
        #region ObjectDefnMOdal
        public int object_code { get; set; }
        public string object_description { get; set; }
        public int? object_type { get; set; }
        public string object_color { get; set; }
        public int object_font { get; set; }
        public string object_remark { get; set; }
        #endregion
        #region Device liST
        public int dev_code { get; set; }
        public int dev_parent { get; set; }
        public string dev_description { get; set; }
        public string dev_reference { get; set; }
        public bool dev_isactive { get; set; }
        public string dev_remark { get; set; }
        public List<Preference> deviceList { get; set; }
        public string prefcode { get; set; }
        #endregion
        #region voucher list 
        public int vochr_code { get; set; }
        public int vochr_type { get; set; }
        public string conponent_type { get; set; }
        public string vochr_default { get; set; }
        public string vochr_name { get; set; }
        public string vochr_abbriviation { get; set; }
        public string vochr_isodocunum { get; set; }
        public bool vochr_isactive { get; set; }
        public string vochr_jornaltype { get; set; }
        public bool vochr_isllineItem { get; set; }
        public string vochr_remark { get; set; }
        public string vochrdefncode { get; set; }
        public int parentId { get; set; }
        public List<SystemConstant> voucherDefinition { get; set; }
        #endregion
        #region gsl type list 
        public string gsl_code { get; set; }
        public string gsl_description { get; set; }
        public int gsl_category { get; set; }
        public bool gsl_isactive { get; set; }
        public string gsl_remark { get; set; }
        public List<SystemConstant> GslListType { get; set; }
        #endregion
        #region system parameteres property
        public string _AlertBefore { get; set; }
        public bool _AllowMultipleInstance { get; set; }
        public string _ClockServer { get; set; }
        public string _CustomReportLibraryUrl { get; set; }
        public string _DatabaseBackupPath { get; set; }
        public string _DefaultDiscountAccount { get; set; }
        public bool _DuplicateCardIssue { get; set; }
        public bool _EnableCloudService { get; set; }
        public bool _EnableMAP { get; set; }
        public string _ErrorLogPath { get; set; }
        public string _ExpiryDateNotificationInDate { get; set; }
        public bool _ExportCashAsCredit { get; set; }
        public string _InventoryRoundQuantity { get; set; }
        public string _MAPURL { get; set; }
        public string _NoOfInstances { get; set; }
        public string _NoOfNestedWindows { get; set; }
        public bool _PhoneNumberMandatory { get; set; }
        public bool _PhotoMandatory { get; set; }
        public string _SaveAttachment { get; set; }
        public string _XMLDampYardPath { get; set; }
        public string _XMLRequanselationPath { get; set; }
        public bool _EnableThirdPartyInterfacing { get; set; }
        public string _LocalRootPath3rdPartyInterfacing { get; set; }
        public string _SyncType { get; set; }
        public string _ThirdPartySyncMethod { get; set; }

        public string systemParameterProperty { get; set; }


        #endregion
        #region company working hours
        public int workinghourorgunitDefn { get; set; }
        public string schedule_description { get; set; }
        public string schedule_type { get; set; }
        public int schedule_typecode { get; set; }
        public int scheduleheadercode { get; set; }
        public DateTime schedule_from_date { get; set; }
        public DateTime schedule_to_date { get; set; }

        public string[] daymonth { get; set; }
        public string[] headercodee { get; set; }
        public int[] detailcodee { get; set; }
        public string[] schduledetailcode { get; set; }
        public DateTime[] startdaet { get; set; }
        [DisplayFormat(DataFormatString = "{HH:MM:SS}")]
        public DateTime[] enddate { get; set; }
        #endregion
    }
    public class CompanySchedule
    {
        public string days { get; set; }
        public int headercode { get; set; }
        public int detailcode { get; set; }
        public DateTime fromtime { get; set; }
        public DateTime totime { get; set; }
        public decimal hours { get; set; }


    }
    public class ObjectDefnition
    {
        public string code { get; set; }
        public string description { get; set; }
        public string parent { get; set; }
        public string defaultcode { get; set; }

    }
    public class ObjectDefnitionchid
    {
        public string category { get; set; }
        public string chlddescription { get; set; }
        public string chldcode { get; set; }
    }
}


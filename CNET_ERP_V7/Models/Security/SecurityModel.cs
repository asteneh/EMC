
using CNET_V7_Domain.Domain.SecuritySchema;
using CNET_V7_Domain.Domain.SettingSchema;
using CNET_V7_Domain.Domain.ViewSchema;
using CNET_V7_Domain.Misc;
using CNET_V7_Entities.CustomReturnTypes;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CNET_ERP_V7.Models.Security
{
    public class SecurityModel
    {
        public int securityType { get; set; }
        public string OrgUnitDefn { get; set; }

        #region Security Property Setting


        public string secu_UseScreenLock { get; set; }
        public string secu_PopulateUserName { get; set; }
        public string secu_LogOnlyAttendedUsers { get; set; }
        public string secu_EnforceComplexPassword { get; set; }
        public string secu_EnforcePasswordHistory { get; set; }
        public string secu_ChangePasswordAtFirstLogin { get; set; }

        public string secu_MaximumPasswordAge { get; set; }

        public string secu_LockTime { get; set; }
        public string secu_TrialBeforeLock { get; set; }
        public string secu_PasswordExpiryNotice { get; set; }
        public string secu_MinimumPasswordLength { get; set; }
        public string secu_TimingBeforeScreenlock { get; set; }

        public string secu_LockShortKey { get; set; }

        public string secu_UserNameDisplayStyle { get; set; }

        public string securityPropertySelector { get; set; }

        public List<SelectListItem> enable_Carts { get; } = new List<SelectListItem>
        {

             new SelectListItem { Value = "False", Text = "False"},
             new SelectListItem { Value =  "True", Text = "True" }
        };
        public List<SelectListItem> displayStyle { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "List", Text = "List" }
        };

        #endregion
        #region Create Account 
        public int acc_Employee { get; set; }
        public string acc_loggInstatus { get; set; }
        public int acc_Employeeenable { get; set; }
        public string acc_username { get; set; }
        public int acc_usernamecode { get; set; }
        public string acc_usernamevalue { get; set; }
        public string acc_password { get; set; }
        public string acc_updatepassword { get; set; }
        public string acc_comfirmPassword { get; set; }
        public bool acc_Isactive { get; set; }
        public string acc_remark { get; set; }
        public int acc_loggedstatus { get; set; }

        public string passwordcomplexity { get; set; }
        public bool enableusername { get; set; }
        public string useraccountupdatecode { get; set; }
        public bool rolehaveuser { get; set; }
        public bool accessselectall { get; set; }
        public bool documentselectall { get; set; }
        public bool reportselectall { get; set; }

        public bool userRole { get; set; }
        public List<Member> mMembers { get; set; }
        public List<ViewFunctWithAccessMDTO> functWithAccessMs { get; set; }
        public List<ViewFunctWithAccessMDTO> functWithAccesdoc { get; set; }
        public List<ViewFunctWithAccessMDTO> funcMatrixaccessList { get; set; }

        public List<FunctWithAccessMDTO> _subsystemDashbord { get; set; }
        public bool countcheck { get; set; }
        #endregion

        #region Change Password
        public int cha_username { get; set; }
        public string cha_passwordtype { get; set; }
        public string cha_oldpasword { get; set; }
        public string cha_newpassword { get; set; }
        public string cha_confirmpassord { get; set; }
        public bool cha_Isactive { get; set; }
        public string cha_remark { get; set; }
        #endregion

        #region User Memebership
        public int mem_username { get; set; }
        public string mem_employeename { get; set; }
        public string mem_Address { get; set; }
        public int mem_orgunitcode { get; set; }
        public int access_orgunitcode { get; set; }
        public int activityType { get; set; }

        #endregion

        #region activity privilage
        public int activity_definition { get; set; }
        public int? activity_range { get; set; }
        public bool activity_passkey { get; set; }
        public string activity_remark { get; set; }

        public int Voucher_gslcode { get; set; }
        public int act_rolecode { get; set; }
        public int roleac_code { get; set; }
        //public List<CNETComponent> activitycNETComponents { get; set; }
        //public List<CNETComponent> activitychildcNETComponents { get; set; }
        #endregion

        #region voucher dashboard
        public string voucher_definition { get; set; }
        public string voucher_remark { get; set; }
        #endregion

        #region access privilege
        public List<SystemConstantDTO> subHeader { get; set; }
        public List<SystemConstantDTO> mainHeader { get; set; }
        public List<FunctionalityDTO> functionalities { get; set; }
        public List<SystemConstantDTO> activitycNETComponents { get; set; }
        public List<SystemConstantDTO> activitychildcNETComponents { get; set; }
        public List<FunctWithAccessMDTO> accessprivilage { get; set; }
        public List<VwRoleActivityViewDTO> roleacttivity {get; set; }
        public List<VwRoleActivityViewDTO> rolegsl { get; set; }
        public List<ActivitydTO> _changePassword { get; set; }


        #endregion
        public string Idcode { get; set; }
        public string Compcode { get; set; }
        public string Compparent { get; set; }
        public string Compdescription { get; set; }

        public string Doccode { get; set; }
        public string Docparent { get; set; }
        public string Docdescription { get; set; }

        public List<string> subsysDashpriviligefuncCode { get; set; }
        public List<string> acesspriviligefuncCode { get; set; }
        public List<string> allacesspriviligefuncCode { get; set; }
        public List<string> documentpriviligefuncCode { get; set; }
        public string Orgunitcod { get; set; }
        public int funcCategory { get; set; }
        public int subsystemcompo { get; set; }

        public string wokfcode { get; set; }
        public string wokfdescriptin { get; set; }
        public string wokfParent { get; set; }

        public string rolecode { get; set; }
        public string selectedCode { get; set; }
        public string selectedname { get; set; }

        public int Docategory { get; set; }
        public int docRolecode { get; set; }

        public string rangedescripion { get; set; }
        public decimal? rangemin { get; set; }
        public decimal? rangemax { get; set; }

        public string Repocode { get; set; }
        public string Repoparent { get; set; }
        public string Repodescription { get; set; }
        public string Repocomponent { get; set; }

        public string subsystemdashboardfunccode { get; set; }
        public int subsysrolecode { get; set; }

        public List<string> voucherCode { get; set; }
        public List<string> subsystendescCode { get; set; }
        public List<string> voucherremark { get; set; }
        public string voucherdashrole { get; set; }

        #region Report privilege 
        public List<string> reportpriviligeCode { get; set; }
        public int reportId { get; set; }
        public int reportrolecode { get; set; }
        public List<NewAccessMatrix> ReportType { get; set; }
        public List<SystemConstantDTO> mainReportType { get; set; }
        public List<SystemConstantDTO> SubReportType { get; set; }
        public List<ReportAccess> report { get; set; }
        #endregion
        #region document privilege
        public List<SystemConstantDTO> documentcNETComponents { get; set; }
        public List<SystemConstantDTO> documentchildcNETComponents { get; set; }
        public List<FunctWithAccessMDTO> documentpriv { get; set; }
        #endregion

        public string generalrolecode { get; set; }
    }
    public class WorkFlowObject
    {
        public string code { get; set; }
        public string description { get; set; }
        public string parent { get; set; }

    } 
    public class ChangePasswordDTO
    {
        public int id { get; set; }
        public string date { get; set; }
        public string? device { get; set; }
        public string consineeUnit { get; set; }
        public string userName { get; set; }
    }
    public class rolevoucher
    {
        public int id { get; set; }
        public int updatecode { get; set; }
        public int? minid { get; set; }
        public decimal? minrange { get; set; }
        public int? maxid { get; set; }
        public decimal? maxrange { get; set; }
        public int? rangecode { get; set; }
        public string? raneDesc { get; set; }
        public string DescriptionName { get; set; }
        public bool NeedsPassCode { get; set; }
        public string remark { get; set; }
    }

    public class VoucherObject
    {
        public string code { get; set; }
        public string description { get; set; }

    }
    public class SubWorkFlowObject
    {
        public string chldcode { get; set; }
        public string chlddescription { get; set; }
        public string chldparent { get; set; }

    }
    public class DocumentAccess
    {
        public string value { get; set; }
        public string parent { get; set; }
    }
    public class ReportAccess
    {
        public int? reptCode { get; set; }
        public int reptParent { get; set; }
        public string reptDescription { get; set; }
        public string reptcomponent { get; set; }
        public string reptcategory { get; set; }
        public string reptreference { get; set; }
    }
    public class ReportAccesspre
    {
        public string reprtCode { get; set; }
        public string reprtParent { get; set; }
        public string reprtDescription { get; set; }
        public string reprtcomponent { get; set; }
        public string reprtcategory { get; set; }
        public string reprtreference { get; set; }
    }
    public class Member
    {
        public string EmployeeName { get; set; }
        public string empCode { get; set; }
        public string userName { get; set; }
        public string Address { get; set; }
        public int userRoleMaperId { get; set; }
        public bool? isActive { get; set; }
    }

    public class ActivitydTO
    {
        public string code { get; set; }
        public string reference { get; set; }
        public string date { get; set; }
        public string orgdef { get; set; }
        public string username { get; set; }
        public string devicename { get; set; }

    }

    public class NewAccessMatrix
    {
        public bool accessLevel { get; set; }
        public int id { get; set; }
        public int pointer { get; set; }
        public string reference { get; set; }
        public string remark { get; set; }
        public string role { get; set; }
    }
}

using CNET_V7_Domain.Domain.CommonSchema;
using CNET_V7_Domain.Domain.ConsigneeSchema;
using CNET_V7_Domain.Domain.HrmsSchema;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace CNET_ERP_V7.Models
{
    public class Companymodel
    {
        public int comType { get; set; }
        #region Additional Information
        public int[]? persontax { get; set; }
        public int? communicationSource { get; set; }
        public int? valueFactorDefinition { get; set; }
        public int? personOrganizationUnit { get; set; }
        public decimal? transactionLimitType { get; set; }
        public decimal? transactionLimitFactor { get; set; }
        public int category { get; set; }
        public int? assigned { get; set; }

        #endregion

        #region Address

        public string phone { get; set; }
        public string companyNote { get; set; }
        public string email { get; set; }
        public int orgunitaddress { get; set; }
        public string[] orgunitadderessvalue { get; set; }
        public string[] orgunitadderessvalueInsert { get; set; }
        public string[] orgunitadderesspreferencecode { get; set; }
        public string[] orgunitadderesspreferenceupdatecode { get; set; }
        public string[] orgunitadderessaddresscode { get; set; }
        public int referenceaddress { get; set; }
        public string compCode { get; set; }
        #endregion

        #region Attachment
        public int attachmentcode { get; set; }
        public string attachmentcodee { get; set; }
        public string attachmentreference { get; set; }
        public int? attachmentcatagory { get; set; }
        public string attachmentcatedescription { get; set; }


        [Required(ErrorMessage = "please enter description")]
        public string attachmentdescription { get; set; }

        public string assessmentCode { get; set; }

        public string url { get; set; }
        public string type { get; set; }
        public int[] index { get; set; }
        public string attachmentRemark { get; set; }
        public string[] Attachmentcatagoryy { get; set; }

        public IFormFile[] Filee { get; set; }
        public IFormFile file { get; set; }

        #endregion

        #region Organiztion Basic Information
        public string q { get; set; }
        public string organizationCode { get; set; }
        public string editeOrganizationCode { get; set; }

        public int organizationType { get; set; }
        public string consigneeUnitName { get; set; }
        public string orgCode { get; set; }
        public string tradeName { get; set; }
        public string compTin { get; set; }
        public string brandName { get; set; }
        public int? businessType { get; set; }
        public int NationContactPerson { get; set; }
        public int? tax{ get; set; }
        public int? language { get; set; }
        public int? Currency { get; set; }
        public bool isActive { get; set; }
        public string code { get; set; }
        public string tin { get; set; }
        public string preference { get; set; }
        public string attachmentcategorytw { get; set; }
        public bool headoffice { get; set; }

        public DateTime? DOB { get; set; }
        public int? nationality { get; set; }
        public string? lastName { get; set; }
        public string? middleName { get; set; }
        public string firstName { get; set; }
        public string Iimage { get; set; }
        public IFormFile defimage { get; set; }
        public string Baseurl { get; set; }
        public int? cParentId { get; set; }
        public string cremark { get; set; }
        public List<ConsigneeUnitDTO> parentcode { get; set; }

        #endregion
        #region GSL Note
        public string contentnote { get; set; }
        public string notecode { get; set; }
        public string gslReference { get; set; }

        #endregion
        #region owners 
        public string owncodeforupdate { get; set; }
        public List<int> owncodeforupdateList { get; set; }
        public string ownersname { get; set; }
        public string ownerstatetype { get; set; }
        public List<string> ownerstate { get; set; }
        public string ownerstatetYPE { get; set; }
        public string ownerrelationtype { get; set; }
        public List<int> person_owners_code { get; set; }
        public List<int> person_relationtype_code { get; set; }
        public string ownerscode { get; set; }
        #endregion

        #region Bank Detail
        public int banktype { get; set; }
        public int bankAccountDetailCodeforEdit { get; set; }
        public string bankAccountDetailReference { get; set; }
        public string bankAccountDetailDesc { get; set; }
        public int bankAccountDetailType { get; set; }
        public int? bankAccountDetailCatagory { get; set; }
        public int bankAccountDetailBank { get; set; }
        public string bankAccountDetailACCnum { get; set; }
        public string bankAccountDetailRemark { get; set; }
        public List<BankAccountDetailDTO> BankAccountDetai  { get; set; }
        #endregion

        #region Organization Address
        public string compaddressCode { get; set; }
        public string[] compaddressDescription { get; set; }
        public string[] compaddressValue { get; set; }
        public string[] comporgaddressDescription { get; set; }
        public string[] comporgaddressValue { get; set; }
        public string comporganizationunitbranchcode { get; set; }
        public string comporgtbranchcode { get; set; }
        public decimal? complatitude { get; set; }
        public decimal? complongitude { get; set; }
        public int consiunitCode { get; set; }

        #endregion

        #region Industry Involved
        public string industryInvolvedCode { get; set; }
        public string industry { get; set; }
        public List<string> industrycode { get; set; }
        public string industryInvolvment { get; set; }
        public List<string> industryInvolvmentcode { get; set; }
        public string industryinvolvedreference { get; set; }
        public string industrycodeUpdate { get; set; }
        public string industrycodeupdatelist { get; set; }
        #endregion

        #region Organization Unit Definations
        public int branchType { get; set; }

        public int organizationunitcode { get; set; }
        public string organizationunittypeedit { get; set; }
        public string orgdescrpition { get; set; }
        public string orgUnitDefCode { get; set; }
        public string orgUnitDefType { get; set; }
        public string orgUnitDefDesc { get; set; }
        public int? orgUnitDefParent { get; set; }
        public int? orgUnitDefSpec { get; set; }
        public int orgUnitDefResperson { get; set; }
        public string orgUnitDefAbbrivation { get; set; }
        public string orgUnitDefRemark { get; set; }
        public int orgunitcode { get; set; }
        public int? orgunitparent { get; set; }
        public bool is_headoffice { get; set; }
        public string? orgdescription { get; set; }
        public string? orgname { get; set; }
        public string orglookupcode { get; set; }
        public string consigneCode { get; set; }
        public string consigneName { get; set; }

        public string orgunittmodal { get; set; }
        public int branchcode { get; set; }
        public bool isActiveb { get; set; }
        public bool ecommerce { get; set; }
        public bool isonline { get; set; }
        public List<ConsigneeUnitDTO> organization_unit_type { get; set; }
        public List<ConsigneeUnitDTO> organization_unit_typechild { get; set; }
        //public List<cons> organization_unit_typechild { get; set; }
        //public List<vw_VoucherConsignee> vw_Vouchers { get; set; }
        public List<LookupDTO> lookups { get; set; }


        #endregion


        #region Job Descrrrrrription
        public int job_index { get; set; }
        public int job_indexTemp { get; set; }
        public int job_status { get; set; }
        public int job_category { get; set; }
        public string job_description { get; set; }
        public string job_remark { get; set; }
        public int job_orgUnitDefn { get; set; }
        public int job_orgUnitDefncode { get; set; }
        public List<JobDescriptionDTO> _jobDescriptions { get; set; }

        #endregion

        #region Identification
        public string idencode { get; set; }
        public string expiryDate { get; set; }
        public int[] identificationType { get; set; }
        public string[] idNumber { get; set; }
        public string[] identificationDescription { get; set; }
        public int[] identificationcode { get; set; }
        public string identificationreference { get; set; }
        public string issueDate { get; set; }
        public List<IdentificationDTO> identificationDTOs {get; set;}
        public bool tinmun { get; set; }
        #endregion

        #region font
        public string font_desc { get; set; }
        public string font_face { get; set; }
        public string font_color { get; set; }
        public string font_style { get; set; }
        public byte font_size { get; set; }
        public bool font_isdefault { get; set; }
        public string font_remark { get; set; }
        public string fontcodeforupdate { get; set; }

        public List<SelectListItem> fontsize { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "6", Text = "6" },
            new SelectListItem { Value =  "7", Text = "7" },
            new SelectListItem { Value =  "8", Text = "8" },
            new SelectListItem { Value =  "9", Text = "9" },
            new SelectListItem { Value =  "10", Text = "10" },
            new SelectListItem { Value =  "11", Text = "11" },
            new SelectListItem { Value =  "12", Text = "12" },
            new SelectListItem { Value =  "13", Text = "13" },
            new SelectListItem { Value =  "14", Text = "14" },
            new SelectListItem { Value =  "15", Text = "15" },
            new SelectListItem { Value =  "16", Text = "16" },
            new SelectListItem { Value =  "17", Text = "17" },
            new SelectListItem { Value =  "18", Text = "18" },
            new SelectListItem { Value =  "19", Text = "19" },
            new SelectListItem { Value =  "20", Text = "20" },
            new SelectListItem { Value =  "21", Text = "21" },
            new SelectListItem { Value =  "22", Text = "22" },
        };
        #endregion

        #region Company Document
        public string com_description { get; set; }
        public string com_Idnumber { get; set; }
        public string com_Type { get; set; }
        public DateTime com_Issued_Date { get; set; }
        public DateTime com_Expiry_Date { get; set; }
        public string com_Issuer { get; set; }
        public string com_remark { get; set; }
        public bool com_Issued_Datedisable { get; set; }
        public bool com_Expiry_Datedisable { get; set; }
        public bool com_Issuerdisable { get; set; }
        public int idenTypecode { get; set; }
        public int idenTypeEditcode { get; set; }


        public string com_attachmentcode { get; set; }
        public string com_attachmentreference { get; set; }
        public string com_attachmentcatagory { get; set; }


        [Required(ErrorMessage = "please enter description")]
        public string com_attachmentdescription { get; set; }

        public string com_assessmentCode { get; set; }

        public string com_url { get; set; }
        public int[] com_index { get; set; }
        public string com_attachmentRemark { get; set; }
        public string[] com_Attachmentcatagoryy { get; set; }

        public IFormFile[] com_Filee { get; set; }
        public IFormFile com_file { get; set; }
        #endregion

        #region address

        public string? Contact { get; set; }

        public string? Phone1 { get; set; }

        public string? Phone2 { get; set; }

        public string? Email { get; set; }

        public string? Website { get; set; }

        public string? SpecificAddress { get; set; }

        public string? Street { get; set; }

        public string? HouseNumber { get; set; }

        public string? PoBox { get; set; }

        public string? Kebele { get; set; }

        public string? Wereda { get; set; }

        public int? Subcity { get; set; }

        public int? City { get; set; }

        public int? Region { get; set; }

        public int? Country { get; set; }

        public string? AddressLine1 { get; set; }

        public string? AddressLine2 { get; set; }

        public string? AddressLine3 { get; set; }
        #endregion

    }
}

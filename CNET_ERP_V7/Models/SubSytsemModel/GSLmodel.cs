using CNET_ERP_V7.Models.SelectorModel;
using CNET_V7_Domain.Domain.AccountingSchema;
using CNET_V7_Domain.Domain.SecuritySchema;
using CNET_V7_Domain.Domain.SettingSchema;
using CNET_V7_Entities.DataModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CNET_ERP_V7.Models.SubSytsemModel
{
    public class GSLmodel
    {
      
        public int cosigneeCode { get; set; }
        public bool prefCount { get; set; }
        public string distridutioName { get; set; }
        public string GslTypeList { get; set; }
        public string? GslTypevalue { get; set; }
        public int gslType { get; set; }
        public string gslartpropertycode { get; set; }
        public string result { get; set; }
        public string GslTypeListforperoperty { get; set; }
        public int TreeCount { get; set; }


        public string gslidseOrgcode { get; set; }
        public string gslidseOrgparent { get; set; }
        public string gslidseOrgdescription { get; set; }

        #region Article Property
        public envoucherFlag gsl_EnableGrouppanel { get; set; }
        public envoucherFlag gsl_EnableSearchbar { get; set; }
        public envoucherFlag gsl_EnableFilter { get; set; }
        public envoucherFlag gsl_EnableFieldsearch { get; set; }
        public string default_document { get; set; }
        public string document_Browser_Library_URL { get; set; }
        public string show_Find_Panel { get; set; }
        public string show_Group_Panel { get; set; }
        public string allow_Duplicate_Name { get; set; }
        public string category_Depth { get; set; }
        public string default_ObjectStategsl { get; set; }
        public string default_Tax { get; set; }
        public string id_Generation_Style { get; set; }
        public string maintain_Case { get; set; }
        public string maintain_Library_URL { get; set; }
        public string enable_Label_Printing { get; set; }
        public string label_Design_File { get; set; }
        public string label_Printer { get; set; }
        public string label_Type { get; set; }
        public string preview_Label_Before_Print { get; set; }
        public string near_Me_in_Meter { get; set; }
        public string advanced_Combo_Search_As_Type { get; set; }
        public string clear_Log_After_Days { get; set; }
        public string maintain_Modification_History { get; set; }
        public string maximum_Records { get; set; }
        public string enable_Mobile_Transaction { get; set; }
        public string consumer_Price_Formula { get; set; }
        public string Maximum_Dates_Without_Transaction { get; set; }
        public string discounted_Price_Formula { get; set; }
        public string on_holiday_Formula { get; set; }
        public string qty_Price_Formula { get; set; }
        public string retail_Price_Formula { get; set; }
        public string whole_Sale_Formula { get; set; }
        public string price_oprationconsum { get; set; }
        public string price_oprationdiscou { get; set; }
        public string price_oprationholi { get; set; }
        public string price_oprationqty { get; set; }
        public string price_oprationretai { get; set; }
        public string price_oprationwhole { get; set; }
        public string valueItemcon { get; set; }
        public string valueItemdis { get; set; }
        public string valueItemhol { get; set; }
        public string valueItemqty { get; set; }
        public string valueItemretai { get; set; }
        public string valueItemwhol { get; set; }
        public string tIN_is_Mandatory { get; set; }
        public string allow_Duplicate_TIN { get; set; }
        public string message { get; set; }

        #endregion
        #region  Article Id seting
        public int id_definitionart { get; set; }
        public int VoucherSettigForIdsettingart { get; set; }
        public int code_idsettingart { get; set; }
        public int organizationundefart { get; set; }
        public int? deviceart { get; set; }
        public int start_Fromart { get; set; }
        public int current_Valuepart { get; set; }
        public bool iS_flexibleart { get; set; }
        public string remarkart { get; set; }
        public string idseorganaizationunitart { get; set; }
        public string idsettingdelcodeart { get; set; }
        public string idsettingdelVouchercodeart { get; set; }


        public int? gslcategory { get; set; }
        #endregion
        #region Article work flow
        public int codefordeleteworkflowart { get; set; }
        public int snwlart { get; set; }
        public string componenetwrflart { get; set; }
        public string referencewrflart { get; set; }
        public int descriptionwrflart { get; set; }
        public bool hasIssuingEffectwflart { get; set; }
        public bool isManualwrflart { get; set; }
        public int? objectStateDefinitionwrflart { get; set; }
        public int requiredTimewrflart { get; set; }
        public string timeFactorwrflart { get; set; }
        public byte indexwrflart { get; set; }
        public int maxRepeatwrflart { get; set; }
        public bool sequencewrflart { get; set; }
        public bool isPrintwrflart { get; set; }
        public string remarkwrflart { get; set; }
        public int codewrflart { get; set; }
        public int Vouchercodeforworkflowart { get; set; }
        public string Vouchercodeforworkflowgetart { get; set; }
        public int Vouchercodeforworkflowdeleteart { get; set; }

        public List<CNET_V7_Domain.Domain.ViewSchema.VwWorkFlowByReferenceViewDTO> vw_WorkFlows { get; set; }
        #endregion #region work flow
        #region Article Account Requirement
        public int acccontrol { get; set; }
        public int accode { get; set; }
        public int gsltypecodefordelete { get; set; }
        public int gsltypecodeforInsert { get; set; }
        public bool accismandatory { get; set; }
        public bool accresirictionselection { get; set; }
        public string accremark { get; set; }
        public List<GslacctRequirementDTO> acctRequirements { get; set; }



        #endregion
        #region Preference account map
        public string ImagefileList { get; set; }
        public string accdescription { get; set; }
        public int accountmapdescription { get; set; }
        public string accountdescriptionorgunit { get; set; }
        public string branch { get; set; }
        public string remark { get; set; }
        public int prefParent { get; set; }
        public string prefDescription { get; set; }
        public string prefFont { get; set; }
        public int prefValue { get; set; }
        public string prefMargin { get; set; }
        public int prefshoppingcate { get; set; }
        public bool prefIsactive { get; set; }
        public string referencecode { get; set; }
        public int? prefparent { get; set; }
        public string prefvalue { get; set; }
        public string prefremark { get; set; }
        public string preffont { get; set; }
        public string prefdescription { get; set; }
        public int codeforgslTYpe { get; set; }
        public int prefcode { get; set; }
        public int preferencecodeforupdateaccmap { get; set; }
        public string preferencecodeforupdateorgunit { get; set; }
        public string preferencecodefordeleteorgunit { get; set; }
        public string preferencecodefordeleteaccmap { get; set; }
        public int getCategoryTypeForcomp { get; set; }
        public int getreferenceTypeForpref { get; set; }
        public bool prefisActive { get; set; }
        public int prefrencecodeforupdate { get; set; }

        public string Prefcode { get; set; }

        public List<AccountMapDTO> accountMaps { get; set; }

        #endregion
        #region firld format

        public List<FieldFormatDTO> FieldFormats { get; set; }
        public int fieldId { get; set; }
        public int code_feldformatart { get; set; }
        public string gslcodeforfiledformat { get; set; }
        public string filedformatcodeforheaderselectionart { get; set; }
        public int filedformatcodefordeleationart { get; set; }
        public int filedformatcodeforheadersaveart { get; set; }
        public int indexfrmatart { get; set; }
        public string fieldfrmatart { get; set; }
        public string captionfrmatart { get; set; }
        public int widthfrmatart { get; set; }
        public string? fontfrmatart { get; set; }
        public string colorfrmatart { get; set; }
        public bool isrequiredfrmatart { get; set; }
        public string remarkfrmatart { get; set; }
        public int gslTypeCodeforfieldformat { get; set; }
        public string categoryTypeCodeforfieldformat { get; set; }
        public string getgslheader { get; set; }


        #endregion
        #region fielld format model
        public string Attribute { get; set; }
        public string Caption { get; set; }
        public int Width { get; set; }
        #endregion
        #region Distribution Article
        public bool isusesymcart { get; set; }
        public string codeforsynchart { get; set; }
        public string organaizationsynchart { get; set; }
        public int snsynchart { get; set; }
        public int? copydisart { get; set; }
        public int codefordeletedistributionart { get; set; }
        public string codeforselectedistributionart { get; set; }
        public int codeforupdatedistributionart { get; set; }
        public int codeforupdatemachinedistributionart { get; set; }
        public string codeforselectedistributionmachart { get; set; }
        public int? destinatindisart { get; set; }
        public string printerdisart { get; set; }
        public string descriptionmachart { get; set; }
        public int? devicemachart { get; set; }
        public string remaarkmachart { get; set; }
        public int getselecteddistributioncodeart { get; set; }
        public string VoucherSettingCodeforsynchart { get; set; }
        public int gslTypecodeCodeforMachineart { get; set; }
        public int VoucherSettingCodeforcopydistributionart { get; set; }

        public string gslTypecode { get; set; }
        public int? gslTypecodesync { get; set; }


        public string Vouchercode { get; set; }

        public List<string> branchcode { get; set; }

        public List<DistributionDTO>  distributions{ get; set; }
        #endregion
        #region Value Factor Defn
        public string valuDescription { get; set; }
        public int valueDefnvalue { get; set; }
        public bool valueIspercent { get; set; }
        public string valueremark { get; set; }
        public int codefordeletevalueDefn { get; set; }
        public int codeforupdatevalueDefn { get; set; }
        public int gslTypevalueDefn { get; set; }
        public int valueDefncodeforsave { get; set; }
        public List<ValueFactorDefinitionDTO> ValueFactors { get; set; }

        #endregion
        #region for Attachment
        public int gsl_attachmentcode { get; set; }
        public string gsl_attachmentreference { get; set; }
        public int gsl_attachmentcatagory { get; set; }
        [Required(ErrorMessage = "please enter description")]
        public string gsl_attachmentdescription { get; set; }
        public string gsl_url { get; set; }
        public IFormFile gsl_file { get; set; }
        public string gsl_type { get; set; }
        public int[] gsl_index { get; set; }
        public string gsl_attachmentRemark { get; set; }
        public string gsl_organizationCode { get; set; }
        public string[] gsl_Attachmentcatagoryy { get; set; }
        public IFormFile[] gsl_Filee { get; set; }
        public int? gsl_preferentialcode { get; set; }
        #endregion

        public List<SelectListItem> copydistributionprinnterarts { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "Microsoft XPS Document Writer", Text = "Microsoft XPS Document Writer" },
            new SelectListItem { Value = "Microsoft Print to PDF", Text = "Microsoft Print to PDF" },
            new SelectListItem { Value = "Fax", Text = "Fax"  },
            new SelectListItem { Value = "Microsoft XPS Document Writer", Text = "Microsoft XPS Document Writer" },
            new SelectListItem { Value = "Microsoft Print to PDF", Text = "Microsoft Print to PDF" },
            new SelectListItem { Value = "Fax", Text = "Fax"  },
            new SelectListItem { Value = "Microsoft XPS Document Writer", Text = "Microsoft XPS Document Writer" },
            new SelectListItem { Value = "Microsoft Print to PDF", Text = "Microsoft Print to PDF" },
            new SelectListItem { Value = "Fax", Text = "Fax"  },
            new SelectListItem { Value = "Microsoft XPS Document Writer", Text = "Microsoft XPS Document Writer" },
            new SelectListItem { Value = "Microsoft Print to PDF", Text = "Microsoft Print to PDF" },
            new SelectListItem { Value = "Fax", Text = "Fax"  },

        };
        public List<SelectListItem> price_oprations { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "+", Text = "+" },
            new SelectListItem { Value =  "-", Text = "-" },
            new SelectListItem { Value =  "/", Text = "/" },
            new SelectListItem { Value =  "*", Text = "*" },
           };
        public List<SelectListItem> default_documents { get; } = new List<SelectListItem>
        {

            new SelectListItem { Value = "Daily", Text = "Daily" },
            new SelectListItem { Value =  "ShowAll", Text = "ShowAll" },
            new SelectListItem { Value =  "Weekly", Text = "Weekly" },
            new SelectListItem { Value =  "Monthly", Text = "Monthly" },
            new SelectListItem { Value =  "Annually", Text = "Annually" },
           };
        public List<SelectListItem> show_Find_Panels { get; } = new List<SelectListItem>
        {

            new SelectListItem { Value = "False", Text = "False" },
            new SelectListItem { Value =  "True", Text = "True" },

           };
        public List<SelectListItem> id_Generation_Styles { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "FromCurrentValue", Text = "FromCurrentValue" },
            new SelectListItem { Value = "FromMaxId", Text = "FromMaxId" },

           };
        public List<SelectListItem> maintain_Cases { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "UpperCase", Text = "UpperCase" },
            new SelectListItem { Value = "LowerCase", Text = "LowerCase" },
            new SelectListItem { Value = "ProperCase", Text = "ProperCase" },
            new SelectListItem { Value = "AsEntered", Text = "AsEntered" },

           };
        public List<SelectListItem> label_Types { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "None", Text = "None" },
            new SelectListItem { Value = "ShelfLabel", Text = "ShelfLabel" },
            new SelectListItem { Value = "PackingLabel", Text = "PackingLabel" },
            new SelectListItem { Value = "WarrantyLabel", Text = "WarrantyLabel" },

           };

        #region gsladdressmap 
        public List<string> Preferecode { get; set; }
        public string gsltypeaddcode { get; set; }
        #endregion

    }
 
  
}

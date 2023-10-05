using CNET_ERP_V7.Common.Helpers;
using CNET_ERP_V7.Models.SelectorModel;
using CNET_V7_Domain.Domain.CommonSchema;
using CNET_V7_Domain.Domain.ConsigneeSchema;
using CNET_V7_Domain.Domain.SecuritySchema;
using CNET_V7_Domain.Domain.SettingSchema;
using CNET_V7_Domain.Domain.ViewSchema;
using CNET_V7_Entities.DataModels;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.Linq;

namespace CNET_ERP_V7.Models.SubSytsemModel
{

    public class VoucherSearchModel 
    {
        public VoucherSearchModel()
        {
            consignee = new List<Select2Result>();
            VoucherDefn = new List<SystemConstantDTO>();
            orgUnit = new List<Select2Result>();
            device = new List<DeviceDTO>();
            user = new List<UserDTO>();
        }
        public string q { get; set; }
        public List<PreferenceDTO> perfernceDto { get; set; }
        public List<string> Branchcode { get; set; }
        public string? articleCode { get; set; }
        public string? VoucherDefnValue { get; set; }
        public int fieldType { get; set; }

        [DisplayName("Consignee")]
        public List<Select2Result> consignee { get; set; }

        [DisplayName("Voucher Defnation")]
        public List<SystemConstantDTO> VoucherDefn { get; set; }

        [DisplayName("Org. Unit")]
        public List<Select2Result> orgUnit { get; set; }

        [DisplayName("Device")]
        public List<DeviceDTO> device { get; set; }

        [DisplayName("User")]
        public List<UserDTO> user { get; set; }

        [DisplayName("Issued Date")]
        public string issuedDate { get; set; }

        public int documentType { get; set; }
        public int voucherType { get; set; }
        public int cosigneeCode { get; set; }
        public string distrutionType { get; set; }
        public string refreceType { get; set; }

        public bool enablevoucherpage { get; set; }


        public string docBrowToken { get; set; }
        public bool isBoomi { get; set; }

        public int id { get; set; }
        public string? code { get; set; }
        public int? type { get; set; }
        public int definitionId { get; set; }
        public int? period { get; set; }
        public int shift { get; set; }
        public int consignee1 { get; set; }
        public int? consignee2 { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public bool? isIssued { get; set; }
        public bool? isVoid { get; set; }
        public int? cart { get; set; }
        public string? fsNo { get; set; }
        public string? mrc { get; set; }
        public int? sourceStore { get; set; }
        public int? lastUser { get; set; }
        public int? destinationStore { get; set; }
        public int? lastDevice { get; set; }
        public int? lastState { get; set; }
        public List<FieldFormatDTO> FieldFormats { get; set; }

        #region Configuration Setting
        public string proprtyVoucherCode { get; set; }
        public string propreference { get; set; }
        public string VoucherSettingCodelist { get; set; }
        public string proppreference { get; set; }
        public string applicable_Additional_Charge { get; set; }
        public string document_Browser_Type { get; set; }
        public string applicable_Discount { get; set; }
        public string serial_Movement_Suggestion { get; set; }
        public string enable_Cart { get; set; }
        public string enable_Currency { get; set; }
        public string enable_Denomination { get; set; }
        public string enable_Payment_Option { get; set; }
        public string enable_LineItem_Note { get; set; }
        public string enable_new_consinemaintenance { get; set; }
        public string enable_Voucher_note { get; set; }
        public string enable_Term { get; set; }
        public string use_Mapped_Store { get; set; }
        public string use_Only_Available_Article { get; set; }
        public string is_Store_Moving_Voucher { get; set; }
        public string mandatory_Source_Store { get; set; }
        public string interface_Operation { get; set; }
        public string interface_Type { get; set; }
        public string tax_Priority { get; set; }
        public string Third_party_System { get; set; }
        public string enable_Interface { get; set; }

        public string file_Type { get; set; }
        public string pull_URL { get; set; }
        public string push_URL { get; set; }
        public string value_Type { get; set; }
        public string capture_Value_Tag { get; set; }
        public string calculate_Cost_At_Real_Time { get; set; }
        public string value_Is_Mandatory { get; set; }
        public string value_Rule { get; set; }
        public string enable_Label_Printing { get; set; }
        public string label_Type { get; set; }
        public string label_Event { get; set; }
        public string label_Design_File { get; set; }
        public string label_Articles { get; set; }
        public string preview_Label_Before_Print { get; set; }
        public string label_Printer { get; set; }
        public string default_Printer { get; set; }
        public string date_Format { get; set; }
        public string max_Line_Item { get; set; }
        public string print_Seasonal_message { get; set; }
        public string max_No_Of_Printing { get; set; }
        public string no_Of_Line_Item_Per_Page { get; set; }
        public string printing_Method { get; set; }
        public string voucher_Template { get; set; }
        public string print_Remote_Distribution { get; set; }
        public string print_Copy_Distribution { get; set; }
        public string print_Catalogue_Automatically { get; set; }
        public string print_Article_Code { get; set; }
        public string print_Article_Picture { get; set; }
        public string print_Article_Volume { get; set; }
        public string print_Journal { get; set; }
        public string print_Sum_Art_Phy_dim { get; set; }
        public string print_Values { get; set; }
        public string paper_Size { get; set; }
        public string print_Without_Preview { get; set; }
        public string print_Amount_In_Word { get; set; }
        public string sort_Line_Item { get; set; }
        public string print_Image_Code_On_Voucher { get; set; }
        public string Voucher_User_Orientation { get; set; }
        public string paper_Type { get; set; }
        public string print_Formulation_As_Spec { get; set; }
        public string print_Immediate_Reference { get; set; }
        public string Print_Ancestor_Reference { get; set; }
        public string print_Barcode_on_Document { get; set; }
        public string print_Water_mark { get; set; }
        public string use_Darker_Lines { get; set; }
        public string voucher_Tax_Type { get; set; }
        public string template_Document { get; set; }
        public string isetuRL { get; set; }
        public string print_Specification { get; set; }
        public string voucher_Orientation { get; set; }
        public string use_Period { get; set; }
        public string voucher_Type_Lookup { get; set; }
        public string idgeneration_Style { get; set; }
        public string merge_Item_code_and_Description { get; set; }
        public string no_Of_Copies { get; set; }
        public string default_Document { get; set; }
        public string document_Browser_Library_url { get; set; }
        public string use_Only_Assigned_Article { get; set; }
        public string show_Group_Panel { get; set; }
        public string enable_Weight_Bridge { get; set; }
        public string enable_Qty_Conversion { get; set; }
        public string show_Find_Panel { get; set; }
        public string print_Bank_nfo { get; set; }
        public string check_Price_Validation { get; set; }
        public string use_Flexible_Period { get; set; }
        public string use_Flexible_Date { get; set; }
        public string enable_Destination_Store { get; set; }
        public string enable_Source_Store { get; set; }
        public string value_is_Tax_Inclusive { get; set; }
        public string use_Flexible_Additional_Charge { get; set; }
        public string use_Flexible_Discount { get; set; }
        public string use_Preferential_Additional_Charge { get; set; }
        public string use_Preferential_Discount { get; set; }
        public string use_Automatic_Life_span { get; set; }
        public string enable_Serial_Number_Or_LifeSpan { get; set; }
        public string stock_Balance_View_Option { get; set; }
        public string round_Digit_Quantity { get; set; }
        public string round_Digit_Unit_Price { get; set; }
        public string round_Digit_Total { get; set; }
        public string mandatory_Destination_Store { get; set; }
        public string trigger_Article_Removal { get; set; }
        public string print_LineItem_Conversion { get; set; }
        public string print_LineItem_Count { get; set; }
        public string enable_Credit_Account { get; set; }
        public string enable_Debit_Account { get; set; }
        public string sync_Void_Document { get; set; }
        public string document_Show_User { get; set; }
        public string activity_Sync_Type { get; set; }
        public string serial_Input_Type { get; set; }
        public string card_List_Type { get; set; }
        public string organization_Unit { get; set; }
        public string enable_LineItem_Extension { get; set; }
        public string enable_Barcode_Scanning { get; set; }
        public string mandatory_Weight_Bridge { get; set; }
        public string fixed_Direct_Article_Selection { get; set; }
        public string print_Ancestor_Extension { get; set; }
        public string default_Discount_Check { get; set; }
        public string default_ObjectState { get; set; }
        public string default_Additional_Charge_Check { get; set; }
        public string enforce_Credit_Limit_Notification { get; set; }
        public string enable_Credit_Limit { get; set; }
        public string save_Ancestor_Reference { get; set; }
        public string transaction_Library_URL { get; set; }
        public string enable_Smart_Synchronization { get; set; }
        public string enable_Attachment { get; set; }
        public string enable_Consignee_Extension_Mapping { get; set; }
        public string flexible_Referred_Consignee { get; set; }
        public string enable_Flexible_Tax { get; set; }
        public string use_only_UnExpired_Article { get; set; }
        public string print_Production_Date { get; set; }
        public string print_Expiry_Date { get; set; }
        public string print_Batch { get; set; }
        public string enforce_Workflow_Sequence { get; set; }
        public string print_Quantity_Sum { get; set; }
        public string prolanguage { get; set; }
        public string referred_Quantity_Rule { get; set; }
        public string referred_Price_Rule { get; set; }
        public string With_holding_Type { get; set; }
        public string enable_Consignee_OrganizationUnit { get; set; }
        public string Sync_in_as_Not_Issued { get; set; }
        public string enable_Transaction_Type { get; set; }
        public string allow_LineItem_Duplication { get; set; }
        public string print_Consignee_Code { get; set; }
        public string apply_Discount_on_unit_price { get; set; }
        public string enable_Fiscal_Reference { get; set; }
        public string payment_Direction { get; set; }
        public string sync_LineItem_Reference_Mandatory { get; set; }
        public string article_Selection_Type { get; set; }
        public string enable_Mobile_Transaction { get; set; }
        public string customers_Near_Me_In_Meter { get; set; }
        public string customer_Visibility { get; set; }
        public envoucherFlag _allowAccountMerge { get; set; }
        public enorganationunit originorganization_Unit { get; set; }
        public enorganationunit destinationorganization_Unit { get; set; }
        public envoucherFlag EnablePurpose { get; set; }
        public envoucherFlag EnableSourcebankAccount { get; set; }
        public envoucherFlag EnabledstinitionbankAccount { get; set; }
        public envoucherFlag EnableDeliveryMethod { get; set; }
        public envoucherFlag EnableGrouppanel { get; set; }
        public envoucherFlag EnableSearchbar { get; set; }
        public envoucherFlag EnableFilter { get; set; }
        public envoucherFlag EnableFieldsearch { get; set; }
        public envoucherFlag _enableNewArticleMaintenance { get; set; }
        public envoucherFlag _setDestinationstoreascurrentstore { get; set; }
        public envoucherFlag _setSourcestoreascurrentstore { get; set; }
        public envoucherFlag _enableSynchronization { get; set; }
        public envoucherFlag _ProtectBottomMargin { get; set; }
        public enVoucherNoteSize _VoucherNoteSize { get; set; }
        public enPrintActivityReference Print_Activity_Reference { get; set; }
        public string _DefaultpaymentOption { get; set; }
        public string ISODocumentNumber { get; set; }
        public string abbriviation { get; set; }
        public string journalType { get; set; }

        public List<SelectListItem> copydistributionprinnters { get; } = new List<SelectListItem>
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
      
        public List<SelectListItem> applicable_Additional_Charges { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "article", Text = "Article" },
            new SelectListItem { Value = " Consignee", Text = "Consignee" },
            new SelectListItem { Value = " term", Text = "Term"  },
            new SelectListItem { Value = "both", Text = "Both"  },
            new SelectListItem { Value = " notApplicable", Text = " NotApplicable"  },

        };
        public List<SelectListItem> applicable_Discounts { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "article", Text = "Article" },
            new SelectListItem { Value = " consignee", Text = " Consignee" },
            new SelectListItem { Value = "discountFactor", Text = "DiscountFactor"  },
            new SelectListItem { Value = " term", Text = " Term"  },
            new SelectListItem { Value = "both", Text = "Both"  },
            new SelectListItem { Value = " notApplicable", Text = " NotApplicable"  },


        };
        public List<SelectListItem> serial_Movement_Suggestions { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "fIFO", Text = " FIFO" },
            new SelectListItem { Value = "lIFO", Text = "LIFO" },
        };
        public List<SelectListItem> enable_Carts { get; } = new List<SelectListItem>
        {

             new SelectListItem { Value = "False", Text = "False", },
             new SelectListItem { Value =  "True", Text = "True" },
        };
        public List<SelectListItem> enable_Currencys { get; } = new List<SelectListItem>
        {

            new SelectListItem { Value = "false", Text = "False" },
              new SelectListItem { Value =  "true", Text = "True" },
        };
        public List<SelectListItem> enable_Denominations { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "true", Text = "True" },
            new SelectListItem { Value = "false", Text = "False" },
        };
        public List<SelectListItem> enable_Payment_Options { get; } = new List<SelectListItem>
        {

            new SelectListItem { Value = "false", Text = "False" },
             new SelectListItem { Value =  "true", Text = "True" },
        };
        public List<SelectListItem> enable_LineItem_Notes { get; } = new List<SelectListItem>
        {

            new SelectListItem { Value = "false", Text = "False" },
            new SelectListItem { Value =  "true", Text = "True" },
        };
        public List<SelectListItem> enable_Terms { get; } = new List<SelectListItem>
        {

            new SelectListItem { Value = "false", Text = "False" },
            new SelectListItem { Value =  "true", Text = "True" },
        };
        public List<SelectListItem> enable_Voucher_Notes { get; } = new List<SelectListItem>
        {

            new SelectListItem { Value = "false", Text = "False" },
            new SelectListItem { Value =  "true", Text = "True" },
        };
        public List<SelectListItem> use_Only_Available_Articles { get; } = new List<SelectListItem>
        {

            new SelectListItem { Value = "false", Text = "False" },
            new SelectListItem { Value =  "True", Text = "True" },
        };
        public List<SelectListItem> is_Store_Moving_Vouchers { get; } = new List<SelectListItem>
        {

            new SelectListItem { Value = "False", Text = "False" },
             new SelectListItem { Value =  "True", Text = "True" },
        };
        public List<SelectListItem> use_Preferential_Discounts { get; } = new List<SelectListItem>
        {

            new SelectListItem { Value = "False", Text = "False" },
             new SelectListItem { Value =  "True", Text = "True" },
        };
        public List<SelectListItem> mandatory_Source_Stores { get; } = new List<SelectListItem>
        {

            new SelectListItem { Value = "False", Text = "False" },
            new SelectListItem { Value =  "True", Text = "True" },
        };
        public List<SelectListItem> interface_Operations { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "automatic", Text = "Automatic" },
            new SelectListItem { Value = "manual", Text = "Manual" },
        };
        public List<SelectListItem> tax_Prioritys { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "article", Text = "Article" },
            new SelectListItem { Value = "Consignee", Text = "Consignee" },
            new SelectListItem { Value = "Voucher", Text = "Voucher" },
            new SelectListItem { Value = " NotApplicable", Text = "NotApplicable" },
        };
        public List<SelectListItem> Third_party_Systems { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "peachTree", Text = "PeachTree" },
            new SelectListItem { Value = "aCCPAC", Text = "ACCPAC" },

        };
        public List<SelectListItem> interface_Types { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "pushbased", Text = "Pushbased" },
            new SelectListItem { Value = " pullbased", Text = " pullbased" },
            new SelectListItem { Value = "both", Text = "Both" },

        };
        public List<SelectListItem> file_Types { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "none", Text = " None" },
            new SelectListItem { Value = "xML", Text = "XML" },
            new SelectListItem { Value = "text", Text = "Text" },
            new SelectListItem { Value = "xls", Text = "Xls" },
            new SelectListItem { Value = "cSV", Text = "CSV" },
            new SelectListItem { Value = "dLL", Text = "DLL" },

        };
        public List<SelectListItem> value_Types { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "price", Text = "Price" },
            new SelectListItem { Value = "fIFOCost", Text = "FIFOCost" },
            new SelectListItem { Value = "periodCost", Text = "periodCost" },
            new SelectListItem { Value = "formulationCost", Text = "FormulationCost" },
            new SelectListItem { Value = "jIT", Text = "JIT" },
            new SelectListItem { Value = "lIFOCost", Text = "LIFOCost" },
            new SelectListItem { Value = "wAC", Text = "WAC" },
        };
        public List<SelectListItem> value_Rules { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "fixed", Text = "Fixed" },
            new SelectListItem { Value = "flexible", Text = "Flexible" },
            new SelectListItem { Value = "alternative", Text = "Alternative" },
            new SelectListItem { Value = "protectBottomMargin", Text = "ProtectBottomMargin" },
            new SelectListItem { Value = "notApplicable", Text = " NotApplicable" },

        };
        public List<SelectListItem> label_Types { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "none", Text = "None" },
            new SelectListItem { Value = "shelfLabel", Text = "ShelfLabel" },
            new SelectListItem { Value = "packingLabel", Text = "PackingLabel" },
            new SelectListItem { Value = "warrantyLabel", Text = "WarrantyLabel" },
            new SelectListItem { Value = "notApplicable", Text = " NotApplicable" },


        };
        public List<SelectListItem> label_Articless { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "localArticle", Text = "LocalArticle" },
            new SelectListItem { Value = "barcodedArticle", Text = "BarcodedArticle" },
            new SelectListItem { Value = "both", Text = "Both" },



        };
        public List<SelectListItem> date_Formats { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "longdate", Text = "Longdate" },
            new SelectListItem { Value = "mediumdate", Text = "Mediumdate" },
            new SelectListItem { Value = "Shortdate", Text = "Shortdate" },



        };
        public List<SelectListItem> printing_Methods { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "ESC", Text = "ESC" },
            new SelectListItem { Value = "Template", Text = "Template" },




        };
        public List<SelectListItem> print_Valuess { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "None", Text = "None" },
            new SelectListItem { Value = "UnitAmountOnly", Text = "UnitAmountOnly" },
            new SelectListItem { Value = "SubtotalOnly", Text = "SubtotalOnly" },
            new SelectListItem { Value = "All", Text = "All" },




        };
        public List<SelectListItem> paper_Sizes { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "A4", Text = "A4" },
            new SelectListItem { Value =  "A5", Text = "A5" },
            new SelectListItem { Value =  "A6", Text = "A6" },
            new SelectListItem { Value =  "A15", Text = "A15" },
            new SelectListItem { Value = "Letter", Text = "Letter" },
            new SelectListItem { Value = "Rollover", Text = "Rollover" },
            new SelectListItem { Value = "Rollover58", Text = "Rollover58" },
            new SelectListItem { Value = "Nonetherewillbenoprintout", Text = "Nonetherewillbenoprintout" },

        };
        public List<SelectListItem> sort_Line_Items { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "Bycodealphabetically", Text = "Bycodealphabetically" },
            new SelectListItem { Value =  "ByNamealphabetically", Text = "ByNamealphabetically" },
            new SelectListItem { Value =  "Asentered", Text = "Asentered" },

        };
        public List<SelectListItem> print_Image_Code_On_Vouchers { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "QRcode", Text = "QRcode" },
            new SelectListItem { Value =  "Barcode", Text = "Barcode" },
            new SelectListItem { Value =  "None", Text = "None" },

        };
        public List<SelectListItem> Voucher_User_Orientations { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "Horizontal", Text = "Horizontal" },
            new SelectListItem { Value =  "Vertical", Text = "Vertical" },


        };
        public List<SelectListItem> paper_Types { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "Continuous", Text = "Continuous" },
            new SelectListItem { Value =  "ISO", Text = "ISO" },
            new SelectListItem { Value =  "Normal", Text = "Normal" },


        };
        public List<SelectListItem> print_Water_marks { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "Standard", Text = "Standard" },
            new SelectListItem { Value =  "Attachment", Text = "Attachment" },
            new SelectListItem { Value =  "Normal", Text = " Custom" },


        };
        public List<SelectListItem> print_Specifications { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "None", Text = "None" },
            new SelectListItem { Value =  "Allspecificationtype", Text = "Allspecificationtype" },
            new SelectListItem { Value =  "Keyspecificationtype", Text = "Keyspecificationtype" },
            new SelectListItem { Value =  "NonKeyspecificationtype", Text = "NonKeyspecificationtype" },
            new SelectListItem { Value =  "Formulation", Text = "Formulation" },


        };
        public List<SelectListItem> voucher_Orientations { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "Portrait", Text = "Portrait" },
            new SelectListItem { Value =  "Landscape", Text = "Landscape" },


        };
        public List<SelectListItem> idgenerationStyles { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "FromCurrentValue", Text = "FromCurrentValue" },
            new SelectListItem { Value =  "FromMaxId", Text = "FromMaxId" },


        };
        public List<SelectListItem> default_Documents { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "Daily", Text = "Daily" },
            new SelectListItem { Value =  "ShowAll", Text = "ShowAll" },
            new SelectListItem { Value =  "Weekly", Text = "Weekly" },
            new SelectListItem { Value =  "Monthly", Text = "Monthly" },
            new SelectListItem { Value =  "Annually", Text = "Annually" },
            new SelectListItem { Value =  "None", Text = "None" },


        };
        public List<SelectListItem> stock_Balance_View_Options { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "NotApplicable", Text = "NotApplicable" },
            new SelectListItem { Value =  "AllStoreBalance", Text = "AllStoreBalance" },
            new SelectListItem { Value =  "EnabledStore", Text = "EnabledStore" },

        };
        public List<SelectListItem> trigger_Article_Removals { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "No", Text = "No" },
            new SelectListItem { Value =  "WhenHeld", Text = "WhenHeld" },
            new SelectListItem { Value =  "Always", Text = "Always" },

        };
        public List<SelectListItem> document_Show_Users { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "AllUser", Text = "AllUser" },
            new SelectListItem { Value =  "CurrentUser", Text = "CurrentUser" },


        };
        public List<SelectListItem> activity_Sync_Types { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "NotApplicable", Text = "NotApplicable" },
            new SelectListItem { Value =  "Issued", Text = "Issued" },
            new SelectListItem { Value =  "NotIssued", Text = "NotIssued" },
            new SelectListItem { Value =  "Both", Text = "Both" },


        };
        public List<SelectListItem> serial_Input_Types { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "NewInsert", Text = "NewInsert" },
            new SelectListItem { Value =  "Selection", Text = "Selection" },

        };
        public List<SelectListItem> card_List_Types { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "NotApplicable", Text = "NotApplicable" },
            new SelectListItem { Value =  "AllCard", Text = "AllCard" },
            new SelectListItem { Value =  "IssuedCard", Text = "IssuedCard" },
            new SelectListItem { Value =  "NonIssuedCard", Text = "NonIssuedCard" },

        };
        public List<SelectListItem> organization_Units { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "NotApplicable", Text = "NotApplicable" },
            new SelectListItem { Value =  "DeviceOrganizationUnit", Text = " DeviceOrganizationUnit" },
            new SelectListItem { Value =  "Flexible", Text = "Flexible" },


        };
        public List<SelectListItem> languages { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "English", Text = "English" },
            new SelectListItem { Value =  "Amharic", Text = "Amharic" },



        };
        public List<SelectListItem> referred_Quantity_Rules { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "NotApplicable", Text = "NotApplicable" },
            new SelectListItem { Value =  "Flexible", Text = "Flexible" },
            new SelectListItem { Value =  "Fixed", Text = "Fixed" },


        };
        public List<SelectListItem> With_holding_Types { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "NotApplicable", Text = "NotApplicable" },
            new SelectListItem { Value =  "Payable", Text = "Payable" },
            new SelectListItem { Value =  "Receivable", Text = "Receivable" },


        };
        public List<SelectListItem> document_Browser_Types { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "LightDocumentBrowser", Text = "LightDocumentBrowser" },
            new SelectListItem { Value =  "MediumDocumentBrowser", Text = "MediumDocumentBrowser" },
            new SelectListItem { Value =  "HeavyDocumentBrowser", Text = "HeavyDocumentBrowser" },
            new SelectListItem { Value =  "NonLineItemHeavyDocumentBrowser", Text = "NonLineItemHeavyDocumentBrowser" },


        };
        public List<SelectListItem> article_Selection_Types { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "Tile_Type_Navigation", Text = "Tile_Type_Navigation" },
            new SelectListItem { Value =  "List_Type_Navigation", Text = "List_Type_Navigation" },
            new SelectListItem { Value =  "Category_List_Navigation", Text = "Category_List_Navigation" },
        };
        public List<SelectListItem> customer_Visibilitys { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "Notapplicable", Text = "Notapplicable" },
            new SelectListItem { Value =  "NearMe", Text = "NearMe"},
            new SelectListItem { Value =  "DefinedRoute", Text = "DefinedRoute" },


        };
        public List<SelectListItem> payment_Directions { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "InBound", Text = "InBound" },
            new SelectListItem { Value =  "OutBound", Text = "OutBound"},

        };
        #endregion

        #region Id seting
        public int id_definitionper { get; set; }
        public int VoucherSettigForIdsetting { get; set; }
        public int code_idsetting { get; set; }
        public int organizationundefper { get; set; }
        public string deviceper { get; set; }
        public int start_Fromper { get; set; }
        public int current_Valueper { get; set; }
        public bool iS_flexibleper { get; set; }
        public string remarkper { get; set; }
        public string idseorganaizationunit { get; set; }
        public string idsettingdelcode { get; set; }
        public string idsettingdelVouchercode { get; set; }
        public string branch { get; set; }

        //public List<IdDefinition> idDefenation { get; set; }
        //public List<ViewDeviceByReference> deviceView { get; set; }
        //public List<IdSetting> idsetting { get; set; }
        #endregion
        #region work flow
        public string codefordeleteworkflow { get; set; }
        public int snwl { get; set; }
        public string componenetwrfl { get; set; }
        public string referencewrfl { get; set; }
        public int descriptionwrfl { get; set; }
        public bool hasIssuingEffectwfl { get; set; }
        public bool isManualwrfl { get; set; }
        public int? objectStateDefinitionwrfl { get; set; }
        public int requiredTimewrfl { get; set; }
        public string timeFactorwrfl { get; set; }
        public byte indexwrfl { get; set; }
        public byte indexwrflTemp { get; set; }
        public int maxRepeatwrfl { get; set; }
        public bool sequencewrfl { get; set; }
        public bool isPrintwrfl { get; set; }
        public string remarkwrfl { get; set; }
        public int codewrfl { get; set; }
        public int Vouchercodeforworkflow { get; set; }
        public string Vouchercodeforworkflowget { get; set; }
        public string Vouchercodeforworkflowdelete { get; set; }

        #endregion

        #region RequiredGSLDetail
        public string requiredGSLDetailcode { get; set; }
        public int? requiredGSLdeletcode { get; set; }
        public string referenceselector { get; set; }
        public string VoucherSettingCodeForgetreference { get; set; }
        public int VoucherSettingCodeForgetreferencevouchandline { get; set; }
        public string VoucherSettingCodeForCreatereference { get; set; }
        public int firstchildcount { get; set; }
        public int requiredGSLeditcode { get; set; }
        public int requiredGSLDetailcodeforedit { get; set; }
        public int requiredGSLDetailcodefordelete { get; set; }
        public int requiredGSLDetailcodefrochid { get; set; }
        public int gslreqconsi { get; set; }
        public int gslreqarti { get; set; }
        public int objectsatereq { get; set; }
        public string remarkreq { get; set; }
        public string remarpare { get; set; }
        public int getparentselectedListforconsandart { get; set; }
        public string descriptionforconandArt { get; set; }
        public byte indexforconandArt { get; set; }
        public bool isMandatoryforconandArt { get; set; }
        public bool NeedConsigneeUnit { get; set; }
        public string remark { get; set; }
        public string requriedGSLType { get; set; }
        public int requriedGSLVouchercode { get; set; }
        public List<RequiredGSLDetailDTO3> gSLDetailDTO3s { get; set; }
        #endregion



        #region Trems and Conditions
        public int TermCategory { get; set; }
        public string Termdescription { get; set; }
        public string Termdefaultvalue { get; set; }
        public bool TermisMandatory { get; set; }
        public string Termremark { get; set; }
        public int codefordeleteterms { get; set; }
        public int codeforUpdatetermandcondition { get; set; }
        public string VoucherSettingCode { get; set; }
        public int VoucherSettingCodeforTermsandConditions { get; set; }
        public int VoucherSettingCodeforTerms { get; set; }
        public List<TermDefinitionDTO2> _termDefinitionDTOs { get; set; }

        #endregion
        #region relation Detail
        public string rela_vouchDefn { get; set; }
        public string rela_code { get; set; }
        public string rela_oprator { get; set; }
        public string rela_relation { get; set; }
        public string rela_remark { get; set; }
        public string rela_voucherdef { get; set; }
        #endregion
        #region reference voucher
        public int coderefeforupdateforlineandvoucher { get; set; }
        public int Vouchercoderefeforupdateforlineandvoucher { get; set; }
        public int coderefeforupdateforinretandnested { get; set; }
        public int VouchercoderefeforCreateforinretandnested { get; set; }
        public int deletecoderefe { get; set; }
        public int deletecoderefefronested { get; set; }
        public string getselectedvoucherheader { get; set; }
        public string getselectedvoucherheadertow { get; set; }
        public string typerefe { get; set; }
        public string systemnamerefe { get; set; }
        public int namerefe { get; set; }
        public string abbrrefe { get; set; }
        public string refe { get; set; }
        public bool ismandatoryrefe { get; set; }
        public List<Select2Result> checkboxobjectstate { get; set; }
        public List<VwVoucherRelationViewDTO> relationViews { get; set; }
        public List<VoucherExtensionDefinitionDTO> vExtension { get; set; }
        public string checkboxobjectstateradio { get; set; }
        public string remarkrefe { get; set; }
        public string descforlineandvoucher { get; set; }
        public bool ismandaforlineandvoucher { get; set; }
        public int Vindex { get; set; }
        public string datetimeforlineandvoucher { get; set; }
        public string headerType { get; set; }
        public string remarkforlineandvoucher { get; set; }

        #endregion
        #region Preferential Access
        public int preferentialaccessVouchercode { get; set; }
        public int preferentialaccessVouchercodesetting { get; set; }
        public int preferentialaccess { get; set; }
        public string preferedescription { get; set; }
        public string prefereremark { get; set; }
        public int preferereheaderselecetor { get; set; }
        public int prefererecodeforupdate { get; set; }
        public int prefererecodefordeleted { get; set; }
        public int devicecode { get; set; }

        public List<PreferenceAccessDTO> accessDTOs { get; set; }
        #endregion
        #region Store Map
        public string selectsoureanddestination { get; set; }
        public string storemapcode { get; set; }
        public string storeType { get; set; }
        public bool storemapisdefault { get; set; }
        public string storemapStore { get; set; }
        public string storemapremark { get; set; }
        public int Disdefault { get; set; }
        public List<string> storemapdestination { get; set; }
        public List<string> storemapsource { get; set; }
        public int isDefault { get; set; }
        public List<bool> isdefaulesource { get; set; }
        public List<bool> isdefauledestination { get; set; }
        public int vouchercodeforsource { get; set; }
        public int vouchercodeforsourcanddestinationeget { get; set; }
        public string storemapSourceType { get; set; }
        public List<string> storemapSourcecode { get; set; }
        public List<string> storemapdestinationcode { get; set; }
        public string storemapdestinationType { get; set; }
        public int vouchercodefordestination { get; set; }
        #endregion
        #region firld format
        public int code_feldformat { get; set; }
        public int vouchercodeforfiledformat { get; set; }
        public string filedformatcodeforheaderselection { get; set; }
        public int filedformatcodefordeleation { get; set; }
        public int filedformatcodeforheadersave { get; set; }
        public int indexfrmat { get; set; }
        public string fieldfrmat { get; set; }
        public string captionfrmat { get; set; }
        public int widthfrmat { get; set; }
        public string? fontfrmat { get; set; }
        public string colorfrmat { get; set; }
        public bool isrequiredfrmat { get; set; }
        public string remarkfrmat { get; set; }
        public string VouchersettingCodeforfieldformat { get; set; }
        public string filedformatType { get; set; }
        #endregion

        #region Distribution
        public List<distribution> distribution { get; set; }
        public bool isusesymc { get; set; }
        public string codeforsynch { get; set; }
        public string organaizationsynch { get; set; }
        public int snsynch { get; set; }
        public int copydis { get; set; }
        public string codefordeletedistribution { get; set; }
        public string codeforselectedistribution { get; set; }
        public int codeforupdatedistribution { get; set; }
        public int codeforupdatemachinedistribution { get; set; }
        public string codeforselectedistributionmach { get; set; }
        public int? destinatindis { get; set; }
        public string printerdis { get; set; }
        public string descriptionmach { get; set; }
        public string devicemach { get; set; }
        public string remaarkmach { get; set; }
        public int getselecteddistributioncode { get; set; }
        public string VoucherSettingCodeforsynch { get; set; }
        public int VoucherSettingCodeforMachine { get; set; }
        public int VoucherSettingCodeforcopydistribution { get; set; }
        #endregion
        #region fielld format model
        public string Attribute { get; set; }
        public string Caption { get; set; }
        public int Width { get; set; }
        #endregion
        public List<ConsigneeUnitDTO> _destination = new List<ConsigneeUnitDTO>();
        public List<ConsigneeUnitDTO> _source = new List<ConsigneeUnitDTO>();
        public List<VoucherStoreMappingDTO> _vsource = new List<VoucherStoreMappingDTO>();
        public List<VoucherStoreMappingDTO> _vdestination = new List<VoucherStoreMappingDTO>();

    }
    public class IddensettingDTO
    {
        public int? Id { get; set; }

        public int? Reference { get; set; }

        public int? IdDefinition { get; set; }

        public int? StartFrom { get; set; }

        public bool? IsFlexible { get; set; }

        public int? Device { get; set; }

        public int? OrganizationUnit { get; set; }

        public int? CurrentValue { get; set; }

        public string? Remark { get; set; }
        public string? Description { get; set; }

    }
    public class distribution
    {
        public int idd { get; set; }
        public int ido { get; set; }
        public int idl { get; set; }
        public int sn { get; set; }
        public string descrption { get; set; }
        public string ldescrption { get; set; }
        public bool index { get; set; }
        public string addresse { get; set; }
        public string remark { get; set; }
    }

    public class TermDefinitionDTO2
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public int? Categorycode { get; set; }

        public string? Description { get; set; }

        public string? DefaultValue { get; set; }

        public bool? IsMandatory { get; set; }

        public string? Remark { get; set; }
    }
 
    public class RequiredGSLDetailDTO3
    {
        public int id { get; set; }
        public string requiredGSL { get; set; }
        public string gslType { get; set; }
        public string objectState { get; set; }
        public int? index { get; set; }
        public string remark { get; set; }
    }

    public enum envoucherFlag
    {
        False,
        True,
    }
    public enum enVoucherNoteSize
    {
        Small,
        Medium,
        Large
    }
    public enum enPrintActivityReference
    {
        NotApplicable,
        FirstActivity,
        LastActivity
    } 
    public enum enorganationunit
    {   NotApplicable,
        DeviceOrganizationUnit,
        Flexible
   }
}
  


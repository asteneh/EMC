using CNET_ERP_V7.Common.AuthNavigation;
using CNET_V7_Domain;
using Microsoft.AspNetCore.Mvc;
using CNET_ERP_V7.Models.FramworkModels;

using CNET_V7_Domain.Domain.ViewSchema;
using CNET_ERP_V7.Common.Company;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using CNET_ERP_V7.Models.SubSytsemModel;
using CNET_V7_Domain.Domain.SettingSchema;
using System.Xml.Linq;
using CNET_V7_Domain.Domain.CommonSchema;
using CNET_ERP_V7.Models.SelectorModel;
using CNET_V7_Entities.DataModels;
using CNET_ERP_V7.Common.Helpers;
using CNET_V7_Domain.Domain.ConsigneeSchema;
using Microsoft.AspNetCore.Server.IISIntegration;
using Cnetv7BufferHolder;
using System.Xml.Schema;
using Microsoft.Extensions.Configuration;

namespace CNET_ERP_V7.Controllers
{
    public class SubsystemsController : Controller
    {
        private AuthenticationManager _authenticationManager;
        private readonly HttpClient _httpClient;
        private readonly SharedHelpers _sharedHelpers;
        private NavigatorManager _navigationManager;
        public List<ConfigurationDTO> Config = new List<ConfigurationDTO>();
        public List<ConfigurationDTO> _configuration = new List<ConfigurationDTO>();

        private MapSstore _MapSstore;
        int loggedUser = 0;
        public SubsystemsController(AuthenticationManager authenticationManager,
           IHttpClientFactory httpClientFactory,
           SharedHelpers sharedHelpers, NavigatorManager navigationManager, MapSstore MapSstore)
        {
            _authenticationManager = authenticationManager;
            _httpClient = httpClientFactory.CreateClient("mainclient");
            _sharedHelpers = sharedHelpers;
            _navigationManager = navigationManager;
            _MapSstore = MapSstore;
        }

        public async Task<IActionResult> List(int id)
        {
            var authUser = await _authenticationManager.GetAuthenticatedUser();

            if (authUser == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return View(new VoucherSearchModel
            {
                documentType = id,
            });
        }
        public async Task<ActionResult> SaveConfig(VoucherSearchModel _Config)
        {
            List<ConfigurationDTO> _configuration = new List<ConfigurationDTO>();
            Config = await _sharedHelpers.GetConfigurationByRefandPref(_Config?.documentType.ToString(), CNETConstants.VOUCHER_COMPONENET);
            var delete = _sharedHelpers.DeleteConfigurationByReferenceAndPreference(_Config.documentType.ToString(), CNETConstants.VOUCHER_COMPONENET);
            ConfigurationDTO config = new ConfigurationDTO();

             config = await SaveproppertyData("Allow LineItem Duplication", _Config.allow_LineItem_Duplication, _Config.documentType.ToString());
               _configuration.Add(config);
             config =  await SaveproppertyData("Card List Type", _Config.card_List_Type, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Default Additional Charge Check", _Config.default_Additional_Charge_Check, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Default Discount Check", _Config.default_Discount_Check, _Config.documentType.ToString());
                _configuration.Add(config);
            config = await SaveproppertyData("Default ObjectState", _Config.default_ObjectState, _Config.documentType.ToString());
             _configuration.Add(config);
            config = await SaveproppertyData("Enable Attachment", _Config.enable_Attachment, _Config.documentType.ToString());
             _configuration.Add(config);
            config = await SaveproppertyData("Enable Barcode Scanning", _Config.enable_Barcode_Scanning, _Config.documentType.ToString());
             _configuration.Add(config);
            config = await SaveproppertyData("Enable Cart", _Config.enable_Cart, _Config.documentType.ToString());
              _configuration.Add(config);
            config = await SaveproppertyData("Enable Consignee Extension Mapping", _Config.enable_Consignee_Extension_Mapping, _Config.documentType.ToString());
              _configuration.Add(config);
            config = await SaveproppertyData("Enable Credit Account", _Config.enable_Credit_Account, _Config.documentType.ToString());
              _configuration.Add(config);
            config = await SaveproppertyData("Enable Credit Limit", _Config.enable_Credit_Limit, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Enable Currency", _Config.enable_Currency, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Enable Debit Account", _Config.enable_Debit_Account, _Config.documentType.ToString());
              _configuration.Add(config);
            config = await SaveproppertyData("Enable Denomination", _Config.enable_Denomination, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Enable Fiscal Reference", _Config.enable_Fiscal_Reference, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Enable LineItem Extension", _Config.enable_LineItem_Extension, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Enable LineItem Note", _Config.enable_LineItem_Note, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Enable New Consignee Maintenance", _Config.enable_new_consinemaintenance, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Enable Payment Option", _Config.enable_Payment_Option, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Enable Term", _Config.enable_Term, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Enable Transaction Type", _Config.enable_Transaction_Type, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Enable Voucher Note", _Config.enable_Voucher_note, _Config.documentType.ToString());
              _configuration.Add(config);
            config = await SaveproppertyData("Sync Void Document", _Config.sync_Void_Document, _Config.documentType.ToString());
              _configuration.Add(config);
            config = await SaveproppertyData("Protect Bottom Margin", _Config._ProtectBottomMargin.ToString(), _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Print Activity Reference", _Config.Print_Activity_Reference.ToString(), _Config.documentType.ToString());
              _configuration.Add(config);
            config = await SaveproppertyData("Enable Weight Bridge", _Config.enable_Weight_Bridge, _Config.documentType.ToString());
            _configuration.Add(config);
            config = await SaveproppertyData("Enforce Credit Limit Notification", _Config.enforce_Credit_Limit_Notification, _Config.documentType.ToString());
              _configuration.Add(config);
            config = await SaveproppertyData("Fixed Direct Article Selection", _Config.fixed_Direct_Article_Selection, _Config.documentType.ToString());
              _configuration.Add(config);
            config = await SaveproppertyData("Flexible Referred Consignee", _Config.flexible_Referred_Consignee, _Config.documentType.ToString());
              _configuration.Add(config);
            config = await SaveproppertyData("IdgenerationStyle", _Config.idgeneration_Style, _Config.documentType.ToString());
              _configuration.Add(config);
            config = await SaveproppertyData("Mandatory Weight Bridge", _Config.mandatory_Weight_Bridge, _Config.documentType.ToString());
              _configuration.Add(config);
            config = await SaveproppertyData("Max Line Item", _Config.max_Line_Item, _Config.documentType.ToString());
              _configuration.Add(config);
            config = await SaveproppertyData("Organization Unit", _Config.organization_Unit, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Payment Direction", _Config.payment_Direction, _Config.documentType.ToString());
              _configuration.Add(config);
            config = await SaveproppertyData("Save Ancestor Reference", _Config.save_Ancestor_Reference, _Config.documentType.ToString());
               _configuration.Add(config);
           config = await SaveproppertyData("Transaction Library URL", _Config.transaction_Library_URL, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Trigger Article Removal", _Config.trigger_Article_Removal, _Config.documentType.ToString());
                _configuration.Add(config);
            config = await SaveproppertyData("Use Flexible Date", _Config.use_Flexible_Date, _Config.documentType.ToString());
                _configuration.Add(config);
            config = await SaveproppertyData("Use Flexible Period", _Config.use_Flexible_Period, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Use Only Assigned Article", _Config.use_Only_Assigned_Article, _Config.documentType.ToString());
                _configuration.Add(config);
            config = await SaveproppertyData("Use Period", _Config.use_Period, _Config.documentType.ToString());
                _configuration.Add(config);
            config = await SaveproppertyData("Voucher Type Lookup", _Config.voucher_Type_Lookup, _Config.documentType.ToString());
                _configuration.Add(config);
            config = await SaveproppertyData("Default Document", _Config.default_Document, _Config.documentType.ToString());
                _configuration.Add(config);
            config = await SaveproppertyData("Document Browser Library URL", _Config.document_Browser_Library_url, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Document Browser Type", _Config.document_Browser_Type, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Document Show User", _Config.document_Show_User, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Enforce Workflow Sequence", _Config.enforce_Workflow_Sequence, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Show Find Panel", _Config.show_Find_Panel, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Show Group Panel", _Config.show_Group_Panel, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Enable Qty Conversion", _Config.enable_Qty_Conversion, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Language", _Config.prolanguage, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Enable Label Printing", _Config.enable_Label_Printing, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Label Articles", _Config.label_Articles, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Label Design File", _Config.label_Design_File, _Config.documentType.ToString());
               _configuration.Add(config);
           config = await SaveproppertyData("Label Event", _Config.label_Event, _Config.documentType.ToString());
               _configuration.Add(config);
            config = config = await SaveproppertyData("Label Printer", _Config.label_Printer, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Label Type", _Config.label_Type, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Preview Label Before Print", _Config.preview_Label_Before_Print, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Article Selection Type", _Config.article_Selection_Type, _Config.documentType.ToString());
              _configuration.Add(config);
            config = await SaveproppertyData("Customer Visibility", _Config.customer_Visibility, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Customer's Near Me In Meter", _Config.customers_Near_Me_In_Meter, _Config.documentType.ToString());
              _configuration.Add(config);
            config = await SaveproppertyData("Enable Mobile Transaction", _Config.enable_Mobile_Transaction, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Date Format", _Config.date_Format, _Config.documentType.ToString());
                _configuration.Add(config);
            config = await SaveproppertyData("Default Printer", _Config.default_Printer, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Max No Of Printing", _Config.max_No_Of_Printing, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Merge Item code and Description", _Config.merge_Item_code_and_Description, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("No Of Copies", _Config.no_Of_Copies, _Config.documentType.ToString());
                _configuration.Add(config);
            config = await SaveproppertyData("No Of Line Item Per Page", _Config.no_Of_Line_Item_Per_Page, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Paper Size", _Config.paper_Size, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Paper Type", _Config.paper_Type, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Print Amount In Word", _Config.print_Amount_In_Word, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Print Ancestor Extension", _Config.print_Ancestor_Extension, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Print Ancestor Reference", _Config.Print_Ancestor_Reference, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Print Article Code", _Config.print_Article_Code, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Print Article Picture", _Config.print_Article_Picture, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Print Article Volume", _Config.print_Article_Volume, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Print Bank Info", _Config.print_Bank_nfo, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Print Barcode on Document", _Config.print_Barcode_on_Document, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Print Batch", _Config.print_Batch, _Config.documentType.ToString());
                _configuration.Add(config);
            config = await SaveproppertyData("Print Catalogue Automatically", _Config.print_Catalogue_Automatically, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Print Consignee Code", _Config.print_Consignee_Code, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Print Copy Distribution", _Config.print_Copy_Distribution, _Config.documentType.ToString());
                _configuration.Add(config);
            config = await SaveproppertyData("Print Expiry Date", _Config.print_Expiry_Date, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Print Formulation As Spec", _Config.print_Formulation_As_Spec, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData(" Print Image Code On Voucher", _Config.print_Image_Code_On_Voucher, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Print Immediate Reference", _Config.print_Immediate_Reference, _Config.documentType.ToString());
              _configuration.Add(config);
            config = await SaveproppertyData("Print Journal", _Config.print_Journal, _Config.documentType.ToString());
              _configuration.Add(config);
            config = await SaveproppertyData("Print LineItem Conversion", _Config.print_LineItem_Conversion, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Print Lineitem Count", _Config.print_LineItem_Count, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Print Production Date", _Config.print_Production_Date, _Config.documentType.ToString());
              _configuration.Add(config);
            config = await SaveproppertyData("Print Quantity Sum", _Config.print_Quantity_Sum, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Print Remote Distribution", _Config.print_Remote_Distribution, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Print Seasonal message", _Config.print_Seasonal_message, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Print Specification", _Config.print_Specification, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Print Sum. Art. Phy. dim.", _Config.print_Sum_Art_Phy_dim, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Print Values", _Config.print_Values, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Print Water mark", _Config.print_Water_mark, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Print Without Preview", _Config.print_Without_Preview, _Config.documentType.ToString());
                _configuration.Add(config);
            config = await SaveproppertyData("Printing Method", _Config.printing_Method, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Sort Line Item", _Config.sort_Line_Item, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Template Document", _Config.template_Document, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("URl", _Config.isetuRL, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Use Darker Lines", _Config.use_Darker_Lines, _Config.documentType.ToString());
               _configuration.Add(config);
             config = await SaveproppertyData("Use only UnExpired Article", _Config.use_only_UnExpired_Article, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Voucher Orientation", _Config.voucher_Orientation, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Voucher Template", _Config.voucher_Template, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Voucher User Orientation", _Config.Voucher_User_Orientation, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Use Only Available Article", _Config.use_Only_Available_Article, _Config.documentType.ToString());
              _configuration.Add(config);
            config = await SaveproppertyData("Round Digit Quantity", _Config.round_Digit_Quantity, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Round Digit Total", _Config.round_Digit_Total, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Round Digit Unit Price", _Config.round_Digit_Unit_Price, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Enable Serial Number Or LifeSpan", _Config.enable_Serial_Number_Or_LifeSpan, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Serial Input Type", _Config.serial_Input_Type, _Config.documentType.ToString());
                _configuration.Add(config);
            config = await SaveproppertyData("Serial Movement Suggestion", _Config.serial_Movement_Suggestion, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Use Automatic Life span", _Config.use_Automatic_Life_span, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Enable Destination Store", _Config.enable_Destination_Store, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Enable Source Store", _Config.enable_Source_Store, _Config.documentType.ToString());
              _configuration.Add(config);
            config = await SaveproppertyData("Is Store Moving Voucher", _Config.is_Store_Moving_Voucher, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Mandatory Destination Store", _Config.mandatory_Destination_Store, _Config.documentType.ToString());
                _configuration.Add(config);
            config = await SaveproppertyData("Mandatory Source Store", _Config.mandatory_Source_Store, _Config.documentType.ToString());
                _configuration.Add(config);
            config = await SaveproppertyData("Stock Balance View Option", _Config.stock_Balance_View_Option, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Use Mapped Store", _Config.use_Mapped_Store, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Activity Sync Type", _Config.activity_Sync_Type, _Config.documentType.ToString());
              _configuration.Add(config);
            config = await SaveproppertyData("Enable Smart Synchronization", _Config.enable_Smart_Synchronization, _Config.documentType.ToString());
              _configuration.Add(config);
            config = await SaveproppertyData("Sync in as Not-Issued", _Config.Sync_in_as_Not_Issued, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Sync LineItem Reference Mandatory", _Config.sync_LineItem_Reference_Mandatory, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Enable Flexible Tax", _Config.enable_Flexible_Tax, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Tax Priority", _Config.tax_Priority, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Value is Tax Inclusive", _Config.value_is_Tax_Inclusive, _Config.documentType.ToString());
                _configuration.Add(config);
            config = await SaveproppertyData("Voucher Tax Type", _Config.voucher_Tax_Type, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("With holding Type", _Config.With_holding_Type, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Enable Interface", _Config.enable_Interface, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("File Type", _Config.file_Type, _Config.documentType.ToString());
                _configuration.Add(config);
            config = await SaveproppertyData("Interface Operation", _Config.interface_Operation, _Config.documentType.ToString());
                _configuration.Add(config);
            config = await SaveproppertyData("Interface Type", _Config.interface_Type, _Config.documentType.ToString());
                 _configuration.Add(config);
            config = await SaveproppertyData("Pull URL", _Config.pull_URL, _Config.documentType.ToString());
                _configuration.Add(config);
            config = await SaveproppertyData("Push URL", _Config.push_URL, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Third party System", _Config.Third_party_System, _Config.documentType.ToString());
                _configuration.Add(config);
            config = await SaveproppertyData("Applicable Additional Charge", _Config.applicable_Additional_Charge, _Config.documentType.ToString());
                _configuration.Add(config);
            config = await SaveproppertyData("Applicable Discount", _Config.applicable_Discount, _Config.documentType.ToString());
                _configuration.Add(config);
            config = await SaveproppertyData("Apply Discount on unit price", _Config.apply_Discount_on_unit_price, _Config.documentType.ToString());
                _configuration.Add(config);
            config = await SaveproppertyData("Use Flexible Additional Charge", _Config.use_Flexible_Additional_Charge, _Config.documentType.ToString());
                _configuration.Add(config);
            config = await SaveproppertyData("Use Flexible Discount", _Config.use_Flexible_Discount, _Config.documentType.ToString());
                _configuration.Add(config);
            config = await SaveproppertyData("Use Preferential Additional Charge", _Config.use_Preferential_Additional_Charge, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Use Preferential Discount", _Config.use_Preferential_Discount, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Calculate Cost At Real Time", _Config.calculate_Cost_At_Real_Time, _Config.documentType.ToString());
                _configuration.Add(config);
            config = await SaveproppertyData("Capture Value Tag", _Config.capture_Value_Tag, _Config.documentType.ToString());
                _configuration.Add(config);
            config = await SaveproppertyData("Check Price Validation", _Config.check_Price_Validation, _Config.documentType.ToString());
                _configuration.Add(config);
            config = await SaveproppertyData("Referred Price Rule", _Config.referred_Price_Rule, _Config.documentType.ToString());
                _configuration.Add(config);
            config = await SaveproppertyData("Referred Quantity Rule", _Config.referred_Quantity_Rule, _Config.documentType.ToString());
               _configuration.Add(config);
            config = await SaveproppertyData("Value Is Mandatory", _Config.value_Is_Mandatory, _Config.documentType.ToString());
            _configuration.Add(config);
            config = await SaveproppertyData("Value Rule", _Config.value_Rule, _Config.documentType.ToString());
            _configuration.Add(config);
            config = await SaveproppertyData("Value Type", _Config.value_Type, _Config.documentType.ToString());
            _configuration.Add(config);
            config = await SaveproppertyData("Allow Account Merge", _Config._allowAccountMerge.ToString(), _Config.documentType.ToString());
            _configuration.Add(config);
            config = await SaveproppertyData("Enable New Article Maintenance", _Config._enableNewArticleMaintenance.ToString(), _Config.documentType.ToString());
            _configuration.Add(config);
            config = await SaveproppertyData("Voucher Note Size", _Config._VoucherNoteSize.ToString(), _Config.documentType.ToString());
            _configuration.Add(config);
            config = await SaveproppertyData("Set Destination store as current store", _Config._setDestinationstoreascurrentstore.ToString(), _Config.documentType.ToString());
            _configuration.Add(config);
            config = await SaveproppertyData("Set Source store as current store", _Config._setSourcestoreascurrentstore.ToString(), _Config.documentType.ToString());
            _configuration.Add(config);
            config = await SaveproppertyData("Enable Synchronization", _Config._enableSynchronization.ToString(), _Config.documentType.ToString());
            _configuration.Add(config);
            config = await SaveproppertyData("Origin organization Unit", _Config.originorganization_Unit.ToString(), _Config.documentType.ToString());
            _configuration.Add(config);
            config = await SaveproppertyData("Destination organization Unit", _Config.destinationorganization_Unit.ToString(), _Config.documentType.ToString());
            _configuration.Add(config);
            config = await SaveproppertyData("Enable Purpose", _Config.EnablePurpose.ToString(), _Config.documentType.ToString());
            _configuration.Add(config);
            config = await SaveproppertyData("Enable Source Bank Account", _Config.EnableSourcebankAccount.ToString(), _Config.documentType.ToString());
            _configuration.Add(config);
            config = await SaveproppertyData("Enable destination Bank Account", _Config.EnabledstinitionbankAccount.ToString(), _Config.documentType.ToString());
            _configuration.Add(config);
            config = await SaveproppertyData("Enable Delivery Method", _Config.EnableDeliveryMethod.ToString(), _Config.documentType.ToString());
            _configuration.Add(config);
            config = await SaveproppertyData("Enable Group panel", _Config.EnableGrouppanel.ToString(), _Config.documentType.ToString());
            _configuration.Add(config);
            config = await SaveproppertyData("Enable Search bar", _Config.EnableSearchbar.ToString(), _Config.documentType.ToString());
            _configuration.Add(config);
            config = await SaveproppertyData("Enable Filter", _Config.EnableFilter.ToString(), _Config.documentType.ToString());
            _configuration.Add(config);
            config = await SaveproppertyData("Enable Field search", _Config.EnableFieldsearch.ToString(), _Config.documentType.ToString());
            _configuration.Add(config);
            config = await SaveproppertyData("ISODocumentNumber", _Config.ISODocumentNumber, _Config.documentType.ToString());
            _configuration.Add(config);
            config = await SaveproppertyData("abbriviation", _Config.abbriviation, _Config.documentType.ToString());
            _configuration.Add(config);
            config = await SaveproppertyData("journalType", _Config.journalType, _Config.documentType.ToString());
            _configuration.Add(config);


            await _sharedHelpers.saveOrUpdateRange(_configuration);
            return Json("Saved Successfully");

        }
        public async Task<ConfigurationDTO> SaveproppertyData(string attr, string curr, string vouchercode)

        {
            ConfigurationDTO configuration = new ConfigurationDTO();
            List<ConfigurationDTO> listcConfigurations = new List<ConfigurationDTO>();
            ConfigurationDTO Prev = Config?.Where(x => x.Attribute?.Trim() == attr?.Trim())?.FirstOrDefault();
            configuration.Pointer = CNETConstants.VOUCHER_COMPONENET;
            configuration.Reference = vouchercode;
            configuration.Attribute = attr;
            configuration.CurrentValue = curr == null ? "" : curr;
            if (Prev != null)
            {
                configuration.PreviousValue = Prev.CurrentValue;
            }
            else
            {
                configuration.PreviousValue = curr == null ? "" : curr;
            }
            return configuration;
        }
        public async Task<object> GetidDefinition(string code, string vouchercode, DataSourceLoadOptions loadOptions)
        {
            VoucherSearchModel searchModel = new VoucherSearchModel();
            searchModel.documentType = Convert.ToInt32(vouchercode);
            searchModel.cosigneeCode = Convert.ToInt32(code);
            return PartialView("_IdSettingMap", searchModel);
        }
        public async Task<IActionResult> createIdsetting([FromForm] VoucherSearchModel VoucherSearchModel)
        {
            IdsettingDTO idSetting = new IdsettingDTO();
            if (VoucherSearchModel.id_definitionper == 0)
            {
                return Json("Please enter all fields");
            }
            else
            {
                await currentUserAndUnit();
                if (VoucherSearchModel.code_idsetting != 0)
                {
                    idSetting.Id = VoucherSearchModel.code_idsetting;
                    idSetting.Reference = VoucherSearchModel.VoucherSettigForIdsetting;
                    idSetting.IdDefinition = VoucherSearchModel.id_definitionper;
                    idSetting.Device = VoucherSearchModel.deviceper != null ? Convert.ToInt32(VoucherSearchModel.deviceper) : null;
                    idSetting.ConsigneeUnit = VoucherSearchModel.organizationundefper;
                    idSetting.StartFrom = (int)VoucherSearchModel.start_Fromper;
                    idSetting.CurrentValue = (int)VoucherSearchModel.start_Fromper;
                    idSetting.User = loggedUser;
                    idSetting.IsFlexible = VoucherSearchModel.iS_flexibleper;
                    idSetting.Remark = VoucherSearchModel.remarkper;
                    var updateidse = await _sharedHelpers.UpdateIdsetting(idSetting);
                    return Json("Saved Successfully");
                }
                else
                {
                    idSetting.Id = 0;
                    idSetting.Reference = VoucherSearchModel.VoucherSettigForIdsetting;
                    idSetting.IdDefinition = VoucherSearchModel.id_definitionper;
                    idSetting.Device = VoucherSearchModel.deviceper != null ? Convert.ToInt32(VoucherSearchModel.deviceper) : null; 
                    idSetting.ConsigneeUnit = VoucherSearchModel.organizationundefper;
                    idSetting.StartFrom = (int)VoucherSearchModel.start_Fromper;
                    idSetting.CurrentValue = (int)VoucherSearchModel.start_Fromper;
                    idSetting.User = loggedUser;
                    idSetting.IsFlexible = VoucherSearchModel.iS_flexibleper;
                    idSetting.Remark = VoucherSearchModel.remarkper;


                    var createmodel = await _sharedHelpers.CreateIdsetting(idSetting);
                }


            }
            return Json("Saved Successfully");
        }

        public async Task<IActionResult> GetWorkFlowsByreference(int code)
        {
            VoucherSearchModel searchModel = new VoucherSearchModel();
            searchModel.documentType = code;
            return PartialView("_VoucherWorkflow", searchModel);
        }
        public async Task<IActionResult> createworkflow([FromForm] VoucherSearchModel worflo)
        {
            ActivityDefinitionDTO actdef = new ActivityDefinitionDTO();
            bool CanSaved = false;
            var resultTYpe = "";
            try
            {
                if (worflo.indexwrfl.ToString() != "0" || worflo.sequencewrfl)
                {
                    if (worflo.codewrfl == 0)
                    {
                        var ActiveworkFlows = await _sharedHelpers.GetWorkFlowsByreference(5, worflo.Vouchercodeforworkflow);
                        if (ActiveworkFlows != null && ActiveworkFlows.Count() > 0)
                        {
                            foreach (var act in ActiveworkFlows)
                            {
                                if (ActiveworkFlows.Any(x => x.Index == Convert.ToByte(worflo.indexwrfl.ToString())))
                                {
                                    resultTYpe = "Duplicate Index Used!";
                                    CanSaved = false;
                                    return Json(new { saved = CanSaved, result = resultTYpe });
                                }
                            }
                        }

                    }
                    else
                    {
                        var ActiveworkFlows = await _sharedHelpers.GetWorkFlowsByreference(5, worflo.Vouchercodeforworkflow);
                        if (ActiveworkFlows != null && ActiveworkFlows.Count() > 0)
                        {
                            foreach (var act in ActiveworkFlows)
                            {
                                if (ActiveworkFlows.Any(x => x.Index == Convert.ToByte(worflo.indexwrfl.ToString()) && x.Id != (worflo.codewrfl)))
                                {
                                    resultTYpe = "Duplicate Index Used!";
                                    CanSaved = false;
                                    return Json(new { saved = CanSaved, result = resultTYpe });
                                }
                            }
                        }
                    }
                }

                ActivityDefinitionDTO activityDefinition = new ActivityDefinitionDTO
                {
                  
                    Reference = worflo.Vouchercodeforworkflow,
                    Description = worflo.descriptionwrfl,
                    Index = worflo.indexwrfl,
                    IsManual = worflo.isManualwrfl,
                    IssuingEffect = worflo.hasIssuingEffectwfl,
                    RequiredTime = worflo.requiredTimewrfl,
                    MaxRepeat = worflo.maxRepeatwrfl,
                    Sequence = worflo.sequencewrfl,
                    Remark = worflo.remarkwrfl,
                    State = worflo.objectStateDefinitionwrfl
                };
                if (worflo.hasIssuingEffectwfl)
                {
                    var workFlows = await _sharedHelpers.GetWorkFlowsByreference(5, worflo.Vouchercodeforworkflow);
                    ActivityDefinitionDTO activity;
                    if (workFlows != null && workFlows.Count > 0)
                    {
                        workFlows = workFlows.Where(x => x.IssuingEffect == true).ToList();
                        if (workFlows != null)
                        {
                            foreach (var value in workFlows)
                            {
                              
                                activity = new ActivityDefinitionDTO();
                                activity.Id = value.Id;
                                activity.Reference = value.Reference;
                                activity.Description = value.Description;
                                activity.Index = value.Index;
                                activity.IsManual = value.IsManual;
                                activity.IssuingEffect = false;
                                activity.RequiredTime = value.RequiredTime;
                                activity.State = value.ObjectStateDefinition;
                                activity.MaxRepeat = value.MaxRepeat;
                                activity.Sequence = value.Sequence;
                                activity.Remark = value.Remark;
                                await _sharedHelpers.UpdateActivityDefinition(activity);
                            }
                        }
                    }
                }
               

                if (worflo.codewrfl != 0 && worflo.codewrfl > 0)
                {
                    if (!String.IsNullOrWhiteSpace(worflo.descriptionwrfl.ToString()) && !String.IsNullOrWhiteSpace(worflo.objectStateDefinitionwrfl.ToString()))
                    {
                        var workFlows = _sharedHelpers.GetWorkFlowsByreference(5, worflo.Vouchercodeforworkflow).Result.Where(w => w.Description == worflo.descriptionwrfl && w.ObjectStateDefinition == worflo.objectStateDefinitionwrfl).ToList();
                        if (workFlows.Count() == 0)
                        {
                            activityDefinition.Id = worflo.codewrfl;
                            await _sharedHelpers.UpdateActivityDefinition(activityDefinition);
                            resultTYpe = "Saved Successfully";
                            CanSaved = true;
                            return Json(new { saved = CanSaved, result = resultTYpe });
                        }
                        else
                        {
                            if (worflo.codewrfl == workFlows.FirstOrDefault().Id)
                            {
                                activityDefinition.Id = worflo.codewrfl;
                                await _sharedHelpers.UpdateActivityDefinition(activityDefinition);
                            
                                resultTYpe = "Saved Successfully";
                                CanSaved = true;
                                return Json(new { saved = CanSaved, result = resultTYpe });

                            }
                            else
                            {
                                resultTYpe = "This Work Flow Is Exist";
                                CanSaved = false;
                                return Json(new { saved = CanSaved, result = resultTYpe });

                            }
                        }
                    }
                    else
                    {
                        resultTYpe = "Enter All Fields";
                        CanSaved = false;
                        return Json(new { saved = CanSaved, result = resultTYpe });
                    }
                }
                else
                {
                    if (!String.IsNullOrWhiteSpace(worflo.descriptionwrfl.ToString()) && !String.IsNullOrWhiteSpace(worflo.objectStateDefinitionwrfl.ToString()))
                    {
                        var workFlow = await _sharedHelpers.GetWorkFlowsByreference(5, worflo.Vouchercodeforworkflow);

                        var workFlows = workFlow?.Where(w => w.Description == worflo.descriptionwrfl && w.ObjectStateDefinition == worflo.objectStateDefinitionwrfl).ToList();
                        if (workFlows == null || workFlows?.Count() == 0)
                        {
                            var arctcode = await _sharedHelpers.CreateActivityDefinition(activityDefinition);
                            resultTYpe = "Saved Successfully";
                            CanSaved = true;
                            return Json(new { saved = CanSaved, result = resultTYpe });
                        }
                        else
                        {
                            resultTYpe = "This Work Flow Is Exist";
                            CanSaved = true;
                            return Json(new { saved = CanSaved, result = resultTYpe });

                        }
                    }
                    else
                    {
                        resultTYpe = "Enter All Fields";
                        CanSaved = true;
                        return Json(new { saved = CanSaved, result = resultTYpe });
                    }
                }
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                throw;
            }

        }
        public async Task<IActionResult> GetIdsettingBycode(int code)
        {
            var idsettinglist = await _sharedHelpers.GetIdsettingById(code);
            var idsetting = idsettinglist?.FirstOrDefault();

            var _iddn = await _sharedHelpers.GetIdDefinitionById(idsetting.IdDefinition);

            return Json(new { code = idsetting.Id, iddefn = idsetting.IdDefinition, iddeDes = _iddn?.FirstOrDefault()?.Description, starfrom = idsetting.StartFrom, isflixiable = idsetting.IsFlexible, device = idsetting.Device, remark = idsetting.Remark });
        } 
        public async Task<IActionResult> GetNestedVoucherById(int code)
        {
            var voucherRelationList = await _sharedHelpers.GetVoucherRelationView();

          var voucherRelation = voucherRelationList.Where(x => x.Id == code).FirstOrDefault();
            return Json(new { code = voucherRelation?.Id, referen = voucherRelation?.ReferencedObject, type = voucherRelation?.Type, state = voucherRelation?.State});
        }
        public async Task<IActionResult> GetDistributionBycode(int code)
        {
            var disList = await _sharedHelpers.GetDistrubtionById(code);
            var dis = disList?.FirstOrDefault();

            return Json(new { code = dis.Id, index = dis.Index, desc = dis.Description, destin = dis.Destination, remark = dis.Remark });
        }
     
        public async Task<IActionResult> DeleteIdsetting(int CODE)
        {
            var delete = await _sharedHelpers.DeleteIdsettingByID(CODE);

            if (delete.ToString() != null)
            {
                return Json("Saved Successfully");
            }
            else
            {
                return Json("Unable to Delete Id Setting");
            }

        }
        public async Task<IActionResult> deleteDistribution(int code)
        {
            var deldis = await _sharedHelpers.DeleteDistributionById(code);
            return Json("Deleted Successfully");

        }
        public async Task<IActionResult> GetWorkFlowById(int code)
        {
            var workflowList = await _sharedHelpers.GetWorkFlowsByid(code);
            var workflow = workflowList?.FirstOrDefault();
            return Json(new { id = workflow.Id, descrption = workflow.Description, index = workflow.Index, issuued = workflow.IssuingEffect, ismanu = workflow.IsManual, remark = workflow.Remark, objesta = workflow.ObjectStateDefinition, maxrep = workflow.MaxRepeat, requ = workflow.RequiredTime, squence = workflow.Sequence });

        } 
        public async Task<IActionResult> GetVoucherExtensionId(int? code)
        {
            var voucherExLi = await _sharedHelpers.getvoucherextensionByid(code);
            var vvoucher = voucherExLi?.FirstOrDefault();
            return Json(new { id = vvoucher.Id, descrption = vvoucher.Descritpion, index = vvoucher.Index, ismanu = vvoucher.IsMandatory,exdate = vvoucher.ExDataType, remark = vvoucher?.Remark});

        } 
        public async Task<IActionResult> GetPreferentailAccessById(int code)
        {
            var prefaccList = await _sharedHelpers.GetPreferentialByid(code);
            var prefacc = prefaccList?.FirstOrDefault();
            return Json(new { id = prefacc.Id, descrption = prefacc.Description, pref = prefacc.Preference, rema = prefacc.Preference});

        }
        public async Task<IActionResult> DeletePreferentilaAccess(int code)
        {
            var deletpref = await _sharedHelpers.DeletepreferentilaaccsessBycode(code);

            if (deletpref)
            {

                return Json("Delete Successfully");
            }
            else
            {
                return Json("Used by other Transaction You Cant Not Deleted");
            }

        }
        public async Task<IActionResult> DeleteVoucherDefinition(int code)
        {
            var delvou = await _sharedHelpers.DeleteVoucherExtensionById(code);
            return Json("Deleted Successfully");
        }
        public async Task<IActionResult> DeleteWorkflow(int code, int vouchDef)
        {

            List<ObjectStateDTO> objList = new List<ObjectStateDTO>();
            var vw_WorkFlowBy = await _sharedHelpers.GetWorkFlowsByid(code);


            if (vw_WorkFlowBy != null)
            {
                var selctobjesate = await _sharedHelpers.GetObjeectStateByrefe(vw_WorkFlowBy?.FirstOrDefault()?.Id);
                var act = await _sharedHelpers.GetActivityByrefandAcDefn(vouchDef, vw_WorkFlowBy?.FirstOrDefault()?.Id);

                if (act == null || act.Count() == 0)
                {

                    var actDefn = await _sharedHelpers.DeleteActivityDefinitionById(code);
                    if (actDefn.ToString() != null || vw_WorkFlowBy?.FirstOrDefault()?.Id == null)
                    {
                        if (selctobjesate != null)
                        {
                            var delwoinobj = await _sharedHelpers.DeleteObjectStateByReference(selctobjesate?.FirstOrDefault()?.Id);

                        }

                        return Json("Delete Successfully ");
                    }
                    else
                    {
                        return Json("Unable to delete workflow. Please check if it is used in security");
                    }
                }
                else
                {
                    return Json("Unable to delete workflow.Please check if it is used by other Transaction");
                }
            }
            return null;
        }
        public async Task<IActionResult> GetrequiredGSLDetail(int code)
        {
            List<RequiredGSLDetailDTO3> RequiredGSLDetailDTOList = new List<RequiredGSLDetailDTO3>();
            if (code != 0)
            {
                List<RequiredGsldetailDTO> gslRequirementList = await _sharedHelpers.SelectRequiredGSLDetailByRequgsl(code);
                var gsltype = await _sharedHelpers.GetAllSytemConstants();
                if (gslRequirementList != null && gslRequirementList.Count > 0)
                {
                    foreach (RequiredGsldetailDTO reqde in gslRequirementList)
                    {
                        RequiredGSLDetailDTO3 RequiredGSLDetailD = new RequiredGSLDetailDTO3
                        {
                            id = reqde.Id,
                            gslType = gsltype?.FirstOrDefault(x => x.Id == reqde?.GslType)?.Description,
                            requiredGSL = gsltype?.FirstOrDefault(x => x.Id == reqde?.GslType)?.ParentId.ToString(),
                            index = reqde.Index,
                            objectState = gsltype?.FirstOrDefault(x => x.Id == reqde?.ObjectState)?.Description,
                            remark = reqde?.Remark,
                        };
                        RequiredGSLDetailDTOList.Add(RequiredGSLDetailD);
                    }
                }
            }
            var gslmodel = new VoucherSearchModel() { gSLDetailDTO3s = RequiredGSLDetailDTOList };
            return PartialView("Gsl_requiremenctDetail",gslmodel);
        }
        public async Task<IActionResult> Getgslrequirment(int vouchercode)
        {
         
            var lookupType = new VoucherSearchModel() { documentType = vouchercode };
            return PartialView("Gsl_requiremenct", lookupType);
        }
        public async Task<IActionResult> Creategslrequirementforconandart(VoucherSearchModel VoucherSearchModelgsl)
        {
            RequiredGslDTO requgsl = new RequiredGslDTO();

            List<RequiredGslDTO> gslforarticle = new List<RequiredGslDTO>();
            var returnresult = "";
            var Iscreate = false;
            if (VoucherSearchModelgsl.descriptionforconandArt == null)
            {
                returnresult = "Enter Description";
                Iscreate = false;
                return Json(new { create = Iscreate, result = returnresult });
            }
            else
            {
                if (VoucherSearchModelgsl.requiredGSLeditcode != 0)
                {
                    requgsl.Id = VoucherSearchModelgsl.requiredGSLeditcode;
                    requgsl.VoucherDefn = VoucherSearchModelgsl.requriedGSLVouchercode;
                    requgsl.Type = VoucherSearchModelgsl.getparentselectedListforconsandart;
                    requgsl.Index = VoucherSearchModelgsl.indexforconandArt;
                    requgsl.Description = VoucherSearchModelgsl.descriptionforconandArt;
                    requgsl.NeedConsigneeUnit = VoucherSearchModelgsl.NeedConsigneeUnit;
                    requgsl.IsMandatory = VoucherSearchModelgsl.isMandatoryforconandArt;
                    requgsl.Remark = VoucherSearchModelgsl.remark;

                    await _sharedHelpers.UpdateRequriedGSlDetail(requgsl);
                    returnresult = "Save Successfully";
                    Iscreate = true;
                    return Json(new { create = Iscreate, result = returnresult });

                }
                else
                {
                    requgsl.Id = 0;
                    requgsl.VoucherDefn = VoucherSearchModelgsl.requriedGSLVouchercode;
                    requgsl.Type = VoucherSearchModelgsl.getparentselectedListforconsandart;
                    requgsl.Index = VoucherSearchModelgsl.indexforconandArt;
                    requgsl.Description = VoucherSearchModelgsl.descriptionforconandArt;
                    requgsl.NeedConsigneeUnit = VoucherSearchModelgsl.NeedConsigneeUnit;
                    requgsl.IsMandatory = VoucherSearchModelgsl.isMandatoryforconandArt;
                    requgsl.Remark = VoucherSearchModelgsl.remark;

                    if (requgsl.Type  == 1835)
                    {
                        gslforarticle = await _sharedHelpers.GetRequriedGslbyVoucherdefandtype(requgsl.VoucherDefn, 1835);

                    }
                    else
                    {
                        gslforarticle = await _sharedHelpers.GetRequriedGslbyVoucherdefandtype(requgsl.VoucherDefn, 1834);

                    }
                    if (requgsl.Type == 1835 && gslforarticle.Count() >= 4)
                    {
                        returnresult = "Consignee reach maximum Size";
                        Iscreate = false;
                        return Json(new { create = Iscreate, result = returnresult });

                    }
                    else if (requgsl.Type == 1834 && gslforarticle.Count() >= 1)
                    {
                        returnresult = "Article reach maximum Size";
                        Iscreate = false;
                        return Json(new { create = Iscreate, result = returnresult });

                    }

                    else
                    {
                        var createrequrd = await _sharedHelpers.CreatRequriedGSL(requgsl);

                        returnresult = "Save Successfully";
                        Iscreate = true;
                        return Json(new { create = Iscreate, result = returnresult });

                    }
                }
            }
        
        }
        public async Task<IActionResult> Deletegslreqchild(int codee)
         {
            List<string> matchList = new List<string>();
            string resultTYpe = null;
            var checkrequlist = false;
            var requlist = await _sharedHelpers.GetRequiredGSLDetailByRequiredGSL(codee);
            if (requlist.Count() >= 1)
            {
                foreach (var item in requlist)
                {
                    var getprefdesc = await _sharedHelpers.GetsystemConstantById(Convert.ToInt32(item.GslType));

                    matchList.Add("\n" + getprefdesc.FirstOrDefault().Description);

                }
                checkrequlist = true;
                return Json(new { increament = matchList, result = resultTYpe, checklist = checkrequlist });
            }
            else
            {
                var dell = await _sharedHelpers.deleteRequriedGSl(codee);
                if (!dell)
                {
                    resultTYpe = " Not Deleted Successfully";
                    checkrequlist = false;
                    return Json(new { increament = matchList, result = resultTYpe, checklist = checkrequlist });
                }
                else
                {
                    resultTYpe = "deleted Successfully";
                    checkrequlist = false;
                    return Json(new { increament = matchList, result = resultTYpe, checklist = checkrequlist });
                }
            }
        }
        public async Task<IActionResult> Deletegslrequdetail(int codee)
        {

            var resultTYpe = "";
            var checkrequlist = false;
            var requlist = await _sharedHelpers.GetRequiredGSLDetailByRequiredGSL(codee);
            if (requlist.Count() >= 1)
            {
                foreach (var item in requlist)
                {
                    var deletegslrquudetail = await _sharedHelpers.deleteRequriedGSldetail(item.Id);
                    checkrequlist = true;
                }
            }
            if (checkrequlist == true)
            {
                var dell = await _sharedHelpers.deleteRequriedGSl(codee);
                resultTYpe = "deleted Successfully";
                checkrequlist = true;
            }
            return Json(new { result = resultTYpe, checklist = checkrequlist });
        }
        [HttpGet]
        public async Task<IActionResult> getGslrequerdList(Select2Model select2Model)
        {
            var obgesatedetableden = await _sharedHelpers.GetSytemConstantBytype("ObjectState Definition");
            var obgesatedetable = obgesatedetableden.Where(x => x.Category == "Article" || x.Category == "Consignee").ToList();
            var resultSet = new List<Select2Model>();

            if (!string.IsNullOrWhiteSpace(select2Model?.q))
            {
                var cartList = obgesatedetable?.Where(x => x.Description.ToLower().Contains(select2Model?.q.ToLower())).ToList();
                resultSet = cartList?.Select(r => new Select2Model()
                {
                    id = r.Id,
                    text = r.Description,

                }).ToList();
            }
            else
            {
                resultSet = obgesatedetable?.Select(r => new Select2Model()
                {
                    id = r.Id,
                    text = r.Description,
                }).ToList();
            }
            return Json(new
            {
                results = resultSet,
                pagination = new
                {
                    more = false //model.Result.Count > 10
                }
            });
        }
        [HttpGet]
        public async Task<IActionResult> getGslrequerdListforarticle(Select2Model select2Model)
        {
            var obgesatedetableden = await _sharedHelpers.GetSytemConstantBytype("ObjectState Definition");
            var obgesatedetable = obgesatedetableden.Where(x => x.Category == "Article" || x.Category == "Consignee").ToList();
            var resultSet = new List<Select2Model>();

            if (!string.IsNullOrWhiteSpace(select2Model?.q))
            {
                var cartList = obgesatedetable?.Where(x => x.Description.ToLower().Contains(select2Model?.q.ToLower())).ToList();
                resultSet = cartList?.Select(r => new Select2Model()
                {
                    id = r.Id,
                    text = r.Description,

                }).ToList();
            }
            else
            {
                resultSet = obgesatedetable?.Select(r => new Select2Model()
                {
                    id = r.Id,
                    text = r.Description,
                }).ToList();
            }
            return Json(new
            {
                results = resultSet,
                pagination = new
                {
                    more = false //model.Result.Count > 10
                }
            });
        }
        public async Task<IActionResult> creategslrequirementDetail( VoucherSearchModel VoucherSearchModelgslch)
        {
            RequiredGsldetailDTO requiredgsldetail = new RequiredGsldetailDTO();
            if (VoucherSearchModelgslch.requiredGSLDetailcodefrochid == null || VoucherSearchModelgslch.objectsatereq == null)
            {
                return Json("Enter all Fields");
            }
            else
            {
                if (VoucherSearchModelgslch.requiredGSLDetailcodeforedit != 0)
                {
                    if (VoucherSearchModelgslch.gslreqconsi != null && VoucherSearchModelgslch.gslreqconsi != 0)
                    {
                        requiredgsldetail.Id = VoucherSearchModelgslch.requiredGSLDetailcodeforedit;
                        requiredgsldetail.RequiredGsl = VoucherSearchModelgslch.requiredGSLDetailcodefrochid;
                        requiredgsldetail.GslType = VoucherSearchModelgslch.gslreqconsi;
                        requiredgsldetail.ObjectState = VoucherSearchModelgslch.objectsatereq == 0 ? null : VoucherSearchModelgslch.objectsatereq;
                        requiredgsldetail.Index = 0;
                        requiredgsldetail.Remark = VoucherSearchModelgslch.remarkreq;
                        await _sharedHelpers.UpdateRequriedGSldetail(requiredgsldetail);
                    }
                    else
                    {
                        requiredgsldetail.Id = VoucherSearchModelgslch.requiredGSLDetailcodeforedit;
                        requiredgsldetail.RequiredGsl = VoucherSearchModelgslch.requiredGSLDetailcodefrochid;
                        requiredgsldetail.GslType = VoucherSearchModelgslch.gslreqarti;
                        requiredgsldetail.ObjectState = VoucherSearchModelgslch.objectsatereq == 0 ? null : VoucherSearchModelgslch.objectsatereq;
                        requiredgsldetail.Index = 0;
                        requiredgsldetail.Remark = VoucherSearchModelgslch.remarkreq;
                        await _sharedHelpers.UpdateRequriedGSldetail(requiredgsldetail);
                    }
                }
                else
                {
                    if (VoucherSearchModelgslch?.gslreqconsi != null && VoucherSearchModelgslch.gslreqconsi != 0)
                    {
                        requiredgsldetail.Id = 0;
                        requiredgsldetail.RequiredGsl = VoucherSearchModelgslch.requiredGSLDetailcodefrochid;
                        requiredgsldetail.GslType = VoucherSearchModelgslch.gslreqconsi;
                        requiredgsldetail.ObjectState = VoucherSearchModelgslch.objectsatereq == 0 ? null : VoucherSearchModelgslch.objectsatereq;
                        requiredgsldetail.Index = 0;
                        requiredgsldetail.Remark = VoucherSearchModelgslch.remarkreq;
                        await _sharedHelpers.CreatRequriedGSldetail(requiredgsldetail);
                    }
                    else
                    {
                        requiredgsldetail.Id = 0;
                        requiredgsldetail.RequiredGsl = VoucherSearchModelgslch.requiredGSLDetailcodefrochid;
                        requiredgsldetail.GslType = VoucherSearchModelgslch.gslreqarti;
                        requiredgsldetail.ObjectState = VoucherSearchModelgslch.objectsatereq == 0 ? null : VoucherSearchModelgslch.objectsatereq;
                        requiredgsldetail.Index = 0;
                        requiredgsldetail.Remark = VoucherSearchModelgslch.remarkreq;
                        await _sharedHelpers.CreatRequriedGSldetail(requiredgsldetail);
                    }
                }
            }

            return Json("Save Successfully");
        }
        public async Task<IActionResult> GetrequiredGslBycode(int code)
        {
            var reugslLsit = await _sharedHelpers.GetRequiredGSLDetailById(code);
            var reugsl = reugslLsit?.FirstOrDefault();
            return Json(new { code = reugsl.Id, consin = reugsl.GslType, objectsta = reugsl.ObjectState,  remark = reugsl.Remark });
        }
        public async Task<IActionResult> deletegslrequrmentItem(int code)
        {
           
            var del = await _sharedHelpers.deleteRequriedGSldetail(code);

            return Json("Deleted Successfully");
        }

        public async Task<IActionResult> gettermsandcoditionsResult(int code)
        {
            var termlist = await _sharedHelpers.GetTermDefinitionByVoucerDefn(code);
            var lookuplist = await _sharedHelpers.GetLookUpByType("Term Category");

           List<TermDefinitionDTO2>  termlt = new List<TermDefinitionDTO2>();
            var termsandlookup = (from ter in termlist
                                  join loo in lookuplist
                                  on ter.Category equals loo.Id

                                  select new
                                  {
                                      ter.Id,
                                      ter.Description,
                                      ter.DefaultValue,
                                      ter.IsMandatory,
                                      ter.Remark,
                                      termcode = loo.Id,
                                      termdesc = loo.Description
                                  }).ToList();

            if (termsandlookup != null && termsandlookup.Count() >0)
            {
                foreach (var item in termsandlookup)
                {
                    termlt.Add(new TermDefinitionDTO2
                    {
                        Id = item.Id,
                        Description = item.Description,
                        DefaultValue = item.DefaultValue,
                        IsMandatory = item.IsMandatory,
                        Remark = item.Remark,
                        Category = item.termdesc,
                        Categorycode = item.termcode
                             
                    });
                }
            }
            var termDefn = new VoucherSearchModel() { _termDefinitionDTOs = termlt };
            return PartialView("TermandCondition", termDefn);
        }
        public async Task<IActionResult> CreateTermsandConditions(VoucherSearchModel createterms)
        {
            TermDefinitionDTO termlt = new TermDefinitionDTO();
            if (createterms.TermCategory == null || createterms.Termdescription == null)
            {
                return Json("Enter all Fields");
            }
            else
            {
                if (createterms.codeforUpdatetermandcondition != 0)
                {
                    var TermList = await _sharedHelpers.GetTermDefinitionByVoucerDefn(createterms.VoucherSettingCodeforTermsandConditions);

                    var Term = TermList?.Where(y => y.Description.ToString().ToLower() == createterms.Termdescription.ToString().ToLower() && y.Category == createterms?.TermCategory);

                    if (Term.Count() > 1)
                    {
                        return Json("This Terms and Condition Is Exist");
                    }
                    else
                    {
                        termlt.Id = createterms.codeforUpdatetermandcondition;
                        termlt.VoucherDefn = createterms.VoucherSettingCodeforTermsandConditions;
                        termlt.Category = createterms.TermCategory;
                        termlt.Description = createterms.Termdescription;
                        termlt.DefaultValue = createterms.Termdefaultvalue;
                        termlt.IsMandatory = createterms.TermisMandatory;
                        termlt.Remark = createterms.Termremark;
                        var updatetermandcond = await _sharedHelpers.UpdateTermsandconditions(termlt);
                    }
                }
                else
                {

                    var TermList = await _sharedHelpers.GetTermDefinitionByVoucerDefn(createterms.VoucherSettingCodeforTermsandConditions);

                    var Term = TermList?.Where(y => y.Description?.ToString().ToLower() == createterms.Termdescription.ToString().ToLower() && y.Category == createterms.TermCategory);

                    if (Term.Count() > 0)
                    {
                        return Json("This Terms and Condition Is Exist");
                    }
                    else
                    {
                        termlt.Id = 0;
                        termlt.VoucherDefn = createterms.VoucherSettingCodeforTermsandConditions;
                        termlt.Category = createterms.TermCategory;
                        termlt.Description = createterms.Termdescription;
                        termlt.DefaultValue = createterms.Termdefaultvalue;
                        termlt.IsMandatory = createterms.TermisMandatory;
                        termlt.Remark = createterms.Termremark;

                        var createtermandcond = await _sharedHelpers.CreateTermsandconditions(termlt);

                    }



                }
            }


            return Json("Saved Successfully");
            //return RedirectToAction("VoucherSetting", "Voucher", createterms.VoucherSettingCodeforTermsandConditions);
        }
        public async Task<IActionResult> deletetermandcondition(int code )
        {
            var dettrems = await _sharedHelpers.deletetermDefnById(code);
            return Json("Deleted Successfully");
        }
        //public async Task<IActionResult> Createrelationdetail(VoucherSearchModel createrelation)
        //{

        //    RelationDetail relation = new RelationDetail();

        //    if (createrelation.rela_vouchDefn == null)
        //    {
        //        return Json("Selecet Voucher");
        //    }
        //    else
        //    {
        //        if (createrelation.rela_code != null)
        //        {


        //            relation.code = createrelation.rela_code;
        //            relation.relation = createrelation.rela_relation;
        //            relation.voucherDefn = int.Parse(createrelation.rela_vouchDefn);
        //            relation.@operator = createrelation.rela_oprator == "+" ? 1 : 0;
        //            relation.remark = createrelation.rela_remark == null ? "" : createrelation.rela_remark;

        //            var updaterelatiodel = await _uIProcessManager.Updaterelationdetail(relation);
        //        }
        //        else
        //        {
        //            var referingobject = await _uIProcessManager.getreferencedetailByrefrring(createrelation.rela_voucherdef);

        //            var GlobalRelation = referingobject?.code;
        //            if (GlobalRelation != null)
        //            {

        //                var relationlist = await _uIProcessManager.getRelationDetailByrelationandvouchde(GlobalRelation, int.Parse(createrelation.rela_vouchDefn));

        //                if (relationlist.Count() > 0)
        //                {

        //                    foreach (var vfd in relationlist)
        //                    {
        //                        await _uIProcessManager.DeleteRelationDetailBycode(vfd.code.ToString());

        //                    }
        //                }

        //                relation.code = "";
        //                relation.relation = GlobalRelation;
        //                relation.voucherDefn = int.Parse(createrelation.rela_vouchDefn);
        //                relation.@operator = createrelation.rela_oprator == "+" ? 1 : 0;
        //                relation.remark = createrelation.rela_remark == null ? "" : createrelation.rela_remark;
        //                var createrelationde = await _uIProcessManager.Createrelationdetail(relation);

        //            }

        //        }


        //    }

        //    return Json("Saved Successfully");
        //}
        public async Task<IActionResult> getReferenceResult(string code,int vouherCode)
        {
            var voucherref = code;

            List<VwVoucherRelationViewDTO> relationViews = new List<VwVoucherRelationViewDTO>();
            var relation = await _sharedHelpers.SelectRelation();

            if (voucherref == "Nested")
            {

                var voucherRelationView = await _sharedHelpers.GetVoucherRelationView();
                relationViews = voucherRelationView.Where(x =>x.ReferringObject == vouherCode && x.RelationType == 1964).ToList();
            }

            var nested = new VoucherSearchModel() { relationViews = relationViews };
            return PartialView("_referenceMap", nested);
           
        }

        public async Task<IActionResult> getInternalReferenceResult(string code, int vouherCode)
        {

            var voucherref = code;

            List<VwVoucherRelationViewDTO> relationViews = new List<VwVoucherRelationViewDTO>();
            var relation = await _sharedHelpers.SelectRelation();


            if (voucherref == "Internal Reference")
            {

                var voucherRelationView = await _sharedHelpers.GetVoucherRelationView();
                relationViews = voucherRelationView.Where(x => x.ReferringObject == vouherCode &&  x.RelationType == 1962).ToList();
            }

            var nested = new VoucherSearchModel() { relationViews = relationViews };
            return PartialView("_Innternalreference", nested);  
          
        }
        public async Task<IActionResult> getlineandvoucherReferenceResult(string code, int vouherCode)
        {
            List<VoucherExtensionDefinitionDTO> refresult = new List<VoucherExtensionDefinitionDTO>();

            switch (code)
            {

                case "LineItem Extension":
                    refresult = await _sharedHelpers.getvoucherextension(vouherCode, 1963);

                    break;
                case "Voucher Extension":
                    refresult = await _sharedHelpers.getvoucherextension(vouherCode, 1965);

                    break;
            }
            var nested = new VoucherSearchModel() { vExtension = refresult };
            return PartialView("_linetemAndvoucherextension", nested);

        }

        public async Task<IActionResult> creategslnestedandinternalreference(VoucherSearchModel modelforreferen, string[] checkboxobjectstate)
        {
            RelationDTO nesrelationList = new RelationDTO();
            RelationalStateDTO relationalState = new RelationalStateDTO();
            List<string> resultstate = new List<string>();
            var ckeckList = checkboxobjectstate;
            var getrelbycode = await _sharedHelpers.GetRElatioSateByrel(modelforreferen.coderefeforupdateforinretandnested);

            if (modelforreferen.namerefe == null || ckeckList == null)
            {
                return Json("Enter all fields");
            }
            else
            {
                if (modelforreferen.coderefeforupdateforinretandnested != 0)
                {
                    switch (modelforreferen.getselectedvoucherheader)
                    {
                        case "Nested":
                            nesrelationList.Id = modelforreferen.coderefeforupdateforinretandnested;
                            nesrelationList.RelationType = 1964;
                            nesrelationList.ReferencedObject = modelforreferen.namerefe;
                            nesrelationList.ReferringObject = modelforreferen.VouchercoderefeforCreateforinretandnested;
                            nesrelationList.RelationLevel = 1829;
                            nesrelationList.Remark = modelforreferen.remarkrefe;

                            var retaltionforstateup = await _sharedHelpers.Updaterelation(nesrelationList);
                            var delrelat = await _sharedHelpers.DeleteRelationSateById(modelforreferen.coderefeforupdateforinretandnested);

                            foreach (var item in checkboxobjectstate)
                            {
                                relationalState.Id = 0;
                                relationalState.Relation = modelforreferen.coderefeforupdateforinretandnested;
                                relationalState.State = Convert.ToInt32(item);
                                relationalState.Criteria = modelforreferen.checkboxobjectstateradio;
                                relationalState.Remark = modelforreferen.remarkrefe;
                                var retaltionforstateobject = await _sharedHelpers.createrelationstate(relationalState);

                            }
                            break;
                        case "Internal Reference":
                            nesrelationList.Id = modelforreferen.coderefeforupdateforinretandnested;
                            nesrelationList.RelationType = 1962;
                            nesrelationList.ReferencedObject = modelforreferen.namerefe;
                            nesrelationList.ReferringObject = modelforreferen.VouchercoderefeforCreateforinretandnested;
                            nesrelationList.RelationLevel = 1829;
                            nesrelationList.Remark = modelforreferen.remarkrefe;

                            var retaltionforstatee = await _sharedHelpers.Updaterelation(nesrelationList);
                            var delrelatt = await _sharedHelpers.DeleteRelationSateById(modelforreferen.coderefeforupdateforinretandnested);


                            foreach (var item in checkboxobjectstate)
                            {
                                relationalState.Id = 0;
                                relationalState.Relation = modelforreferen.coderefeforupdateforinretandnested;
                                relationalState.State = Convert.ToInt32(item);
                                if (modelforreferen.ismandatoryrefe == true)
                                {
                                    relationalState.Criteria = "Is Mandatory";
                                }
                                else
                                {
                                    relationalState.Criteria = "IsNot Mandatory";
                                }
                                relationalState.Remark = modelforreferen.remarkrefe;

                                var retaltionforstateobject = await _sharedHelpers.createrelationstate(relationalState);

                            }
                            break;
                    }


                }
                else
                {
                    switch (modelforreferen.getselectedvoucherheader)
                    {
                        case "Nested":
                            var getreferrenceobject = await _sharedHelpers.getRelationByrefereingobject(modelforreferen.namerefe, modelforreferen?.VouchercoderefeforCreateforinretandnested, 1964);
                            if (getreferrenceobject.Count() >= 1)
                            {

                                return Content("This Voucher Is already Exist ");
                            }
                            else
                            {
                                nesrelationList.Id = 0;
                                nesrelationList.RelationType = 1964;
                                nesrelationList.ReferencedObject = modelforreferen.namerefe;
                                nesrelationList.ReferringObject = modelforreferen.VouchercoderefeforCreateforinretandnested;
                                nesrelationList.RelationLevel = 1829;
                                nesrelationList.Remark = modelforreferen.remarkrefe;
                                var retaltionforstate = await _sharedHelpers.Createrelation(nesrelationList);

                                foreach (var item in checkboxobjectstate)
                                {
                                    relationalState.Id = 0;
                                    relationalState.Relation = retaltionforstate.Id;
                                    relationalState.State = Convert.ToInt32(item);
                                    relationalState.Criteria = modelforreferen.checkboxobjectstateradio;
                                    relationalState.Remark = modelforreferen.remarkrefe;

                                    var retaltionforstateobject = await _sharedHelpers.createrelationstate(relationalState);
                                }
                            }
                            break;

                        case "Internal Reference":
                            var getreferrenceobjecttwo = await _sharedHelpers.getRelationByrefereingobject(modelforreferen.namerefe, modelforreferen.VouchercoderefeforCreateforinretandnested, 1962);
                            var getreferrenceInternal = await _sharedHelpers.getRelationforInternal(modelforreferen.VouchercoderefeforCreateforinretandnested, 1962);
                            if (getreferrenceobjecttwo.Count >= 1)
                            {
                                return Content("This Voucher Is already Exist ");
                            }
                            else if (getreferrenceInternal.Count() >= 2)
                            {
                                return Content("Internal Reference reach the maximum size");
                            }
                            else
                            {
                                nesrelationList.Id = 0;
                                nesrelationList.RelationType = 1962;
                                nesrelationList.ReferencedObject = modelforreferen.namerefe;
                                nesrelationList.ReferringObject = modelforreferen.VouchercoderefeforCreateforinretandnested;
                                nesrelationList.RelationLevel = 1829;
                                nesrelationList.Remark = modelforreferen.remarkrefe;

                                var retaltionforstatee = await _sharedHelpers.Createrelation(nesrelationList);

                                foreach (var item in checkboxobjectstate)
                                {
                                    relationalState.Id = 0;
                                    relationalState.Relation = retaltionforstatee.Id;
                                    relationalState.State = Convert.ToInt32(item);
                                    if (modelforreferen.ismandatoryrefe == true)
                                    {
                                        relationalState.Criteria = "Is Mandatory";
                                    }
                                    else
                                    {
                                        relationalState.Criteria = "IsNot Mandatory";
                                    }
                                    relationalState.Remark = modelforreferen.remarkrefe;

                                    var retaltionforstateobject = await _sharedHelpers.createrelationstate(relationalState);
                                }
                            }
                            break;
                    }
                }
            }
            //return RedirectToAction("VoucherSetting", "Voucher", modelforreferen.VouchercoderefeforCreateforinretandnested);
            return Json("Save Successfully");
        }

        public async Task<IActionResult> createLineandVoucherextension(VoucherSearchModel lineandvoucherex)
        {
            int? index = 0;
            VoucherExtensionDefinitionDTO voucher = new VoucherExtensionDefinitionDTO();
            if (lineandvoucherex.descforlineandvoucher == null)
            {
                return Json("Enter all fields");
            }
            else
            {
                if (lineandvoucherex.coderefeforupdateforlineandvoucher != 0)
                {
                    switch (lineandvoucherex.getselectedvoucherheadertow)
                    {
                        case "LineItem Extension":
                            voucher.Id = lineandvoucherex.coderefeforupdateforlineandvoucher;
                            voucher.VoucherDefinition = lineandvoucherex.Vouchercoderefeforupdateforlineandvoucher;
                            voucher.Descritpion = lineandvoucherex.descforlineandvoucher;
                            voucher.Index = lineandvoucherex.Vindex;
                            voucher.IsMandatory = lineandvoucherex.ismandaforlineandvoucher;
                            voucher.ExDataType = lineandvoucherex?.datetimeforlineandvoucher;
                            voucher.Type = 1963;
                            voucher.Remark = lineandvoucherex.remarkforlineandvoucher;

                            var vouEx = await _sharedHelpers.UpdateVoucherExtension(voucher);
                            break;
                        case "Voucher Extension":
                           
                            voucher.Id = lineandvoucherex.coderefeforupdateforlineandvoucher;
                            voucher.VoucherDefinition = lineandvoucherex.Vouchercoderefeforupdateforlineandvoucher;
                            voucher.Descritpion = lineandvoucherex.descforlineandvoucher;
                            voucher.Index = lineandvoucherex.Vindex;
                            voucher.ExDataType = lineandvoucherex?.datetimeforlineandvoucher;
                            voucher.IsMandatory = lineandvoucherex.ismandaforlineandvoucher;
                            voucher.Type = 1965;
                            voucher.Remark = lineandvoucherex.remarkforlineandvoucher;
                            var vouExupp = await _sharedHelpers.UpdateVoucherExtension(voucher);


                            break;
                    }

                }
                else
                {
                    switch (lineandvoucherex.getselectedvoucherheadertow)
                    {
                        case "LineItem Extension":
                            var indexx = await _sharedHelpers.getvoucherextension(lineandvoucherex.Vouchercoderefeforupdateforlineandvoucher, 1963);
                            if (indexx != null && indexx.Count() > 0)
                            {
                                index = indexx?.LastOrDefault()?.Index;
                                index = ++index;
                            }
                            else
                            {
                                index = 0;
                            }
                            voucher.Id = 0;
                            voucher.VoucherDefinition = lineandvoucherex.Vouchercoderefeforupdateforlineandvoucher;
                            voucher.Descritpion = lineandvoucherex.descforlineandvoucher;
                            voucher.Index = index;
                            voucher.IsMandatory = lineandvoucherex.ismandaforlineandvoucher;
                            voucher.ExDataType = lineandvoucherex?.datetimeforlineandvoucher;
                            voucher.Type = 1963;
                            voucher.Remark = lineandvoucherex.remarkforlineandvoucher;
                            var vouEx = await _sharedHelpers.CreateVoucherExtension(voucher);
                            break;
                        case "Voucher Extension":
                            var Eindex = await _sharedHelpers.getvoucherextension(lineandvoucherex.Vouchercoderefeforupdateforlineandvoucher, 1965);
                            if (Eindex != null && Eindex.Count() > 0)
                            {
                                index = Eindex?.LastOrDefault()?.Index;
                                index = ++index;
                            }
                            else
                            {
                                index = 0;
                            }
                            voucher.Id = 0;
                            voucher.VoucherDefinition = lineandvoucherex.Vouchercoderefeforupdateforlineandvoucher;
                            voucher.Descritpion = lineandvoucherex.descforlineandvoucher;
                            voucher.Index = index;
                            voucher.IsMandatory = lineandvoucherex.ismandaforlineandvoucher;
                            voucher.ExDataType = lineandvoucherex?.datetimeforlineandvoucher;
                            voucher.Type = 1965;
                            voucher.Remark = lineandvoucherex.remarkforlineandvoucher;
                            var vouExx = await _sharedHelpers.CreateVoucherExtension(voucher);

                            break;
                    }
                }
            }
            return Json("Save Successfully");
        }
        public async Task<IActionResult> deletevoucherExBycode(VoucherSearchModel delvoucherEx)
        {
            var delvou = await _sharedHelpers.DeleteVoucherExtensionById(delvoucherEx.deletecoderefe);
            return Json(new EmptyResult());
        }
        [HttpGet]
        public async Task<IActionResult> getvoucherdefstate(Select2Model select2Modelstate)
        {

            var obgesatedetable = await _sharedHelpers.GetWorkFlowsByreference(5,select2Modelstate.other);
            var resultSet = new List<Select2Model>();

            if (!string.IsNullOrWhiteSpace(select2Modelstate?.q))
            {
                var cartList = obgesatedetable?.Where(x => x.Description == Convert.ToInt32(select2Modelstate?.q)).ToList();
                resultSet = cartList?.Select(r => new Select2Model()
                {
                    id = Convert.ToInt32(r.ObjectStateDefinition),
                    text = r.Osddescription,

                }).ToList();
            }
            else
            {
                resultSet = obgesatedetable?.Select(r => new Select2Model()
                {
                    id = Convert.ToInt32(r.ObjectStateDefinition),
                    text = r.Osddescription,
                }).ToList();


            }
            return Json(new
            {
                results = resultSet,
                pagination = new
                {
                    more = false //model.Result.Count > 10
                }
            });

        }
        public async Task<IActionResult> deleterefrencefornested(VoucherSearchModel delrefnested)
        {

            var getrel = await _sharedHelpers.GetRElatioSateByrel(delrefnested.deletecoderefefronested);
            var delrelatsta = await _sharedHelpers.DeleteRelationById(delrefnested.deletecoderefefronested);
            foreach (var item in getrel)
            {

                var delrelat = await _sharedHelpers.DeleteRelationSateById(item.Id);

            }


            return Json(new EmptyResult());
        }
        public async Task<IActionResult> getPreferentialAccess(int vouherCode, int code)
        {
            var getpreacc = await _sharedHelpers.GetPreferentilaAccessByVouDefnandDevice(vouherCode, code);

            var fieldformat = new VoucherSearchModel() { accessDTOs = getpreacc };
            return PartialView("preferentilaaccess", fieldformat);

        }

        public async Task<IActionResult> CreatePreferentialaccess(VoucherSearchModel createpreference)
        {
            PreferenceAccessDTO preference = new PreferenceAccessDTO();
            if (createpreference.preferedescription == null || createpreference.preferentialaccess == 0 || createpreference.devicecode == 0)
            {
                return Json("Enter all fieldes");
            }
            else
            {
                if (createpreference.prefererecodeforupdate != 0)
                {
                    preference.Id = createpreference.prefererecodeforupdate;
                    preference.Description = createpreference.preferedescription;
                    preference.VoucherDfn = createpreference.preferentialaccessVouchercodesetting;
                    preference.Preference = createpreference.preferentialaccess;
                    preference.Device = createpreference.devicecode;
                    preference.Remark = createpreference.prefereremark;
                    var Updatepref = await _sharedHelpers.Updatepreferentilaaccsess(preference);

                }

                else
                {
                    preference.Id = 0;
                    preference.Description = createpreference.preferedescription;
                    preference.VoucherDfn = createpreference.preferentialaccessVouchercodesetting;
                    preference.Preference = createpreference.preferentialaccess;
                    preference.Device = createpreference.devicecode;
                    preference.Remark = createpreference.prefereremark;
                    var createpref = await _sharedHelpers.Createpreferentilaaccsess(preference);

                }
            }

            return Json("Saved Successfully");
            //    return RedirectToAction("VoucherSetting", "Voucher", createpreference.preferentialaccessVouchercodesetting);
        }
        public async Task<IActionResult> getdestinationandsourceResult(VoucherSearchModel stormapmodell)
        {


            var getsouranddest = stormapmodell.selectsoureanddestination;
            var vouchercodesourceanddes = stormapmodell.vouchercodeforsourcanddestinationeget;
            List<VoucherStoreMappingDTO> storemapresult = new List<VoucherStoreMappingDTO>();

            switch (getsouranddest)
            {

                case "Destination":
                    storemapresult = await _sharedHelpers.VoucherStoreMappingVoucher(vouchercodesourceanddes, false);

                    break;
                case "Source":
                    storemapresult = await _sharedHelpers.VoucherStoreMappingVoucher(vouchercodesourceanddes, true);


                    break;
            }
            var orgunitdef = await _sharedHelpers.GetConsigneeunit();

            var soureanddetination = (from str in storemapresult
                                      join org in orgunitdef
                                      on str.Store equals org.Id
                                      select new
                                      {
                                          str.Id,
                                          str.IsDefault,
                                          str.IsSource,
                                          str.Remark,
                                          orgcode = org.Id,
                                          org.Name
                                      }).ToList();

          

            return Json(soureanddetination);
        }

        public async Task<IActionResult> Deletestoremap(int code)
        {
            var deltmapstore = await _sharedHelpers.DleteStoreMap(code);
            var deletesotrmap = code;
            return Json(new EmptyResult());
        }
        public async Task<IActionResult> CreateStoremapdestination(VoucherSearchModel createdestination)
        {
            VoucherStoreMappingDTO voucherStoreMapping = new VoucherStoreMappingDTO();

            if (createdestination.storemapdestinationcode == null)
            {
                return Json("Please Add at least one Destination");
            }
            else
            {
                var storemapcount = createdestination.storemapdestinationcode.Distinct().ToList();
                var storemapremark = createdestination.storemapdestination.Distinct().ToList();
                var storemapListsource = await _sharedHelpers.VoucherStoreMappingVoucher(createdestination.vouchercodeforsource, false);
                if (storemapListsource != null && storemapListsource.Count() > 0)
                {
                    foreach (var item in storemapListsource)
                    {
                        var delete = await _sharedHelpers.DeleteStoreMapSource(item.Id);
                    }
                }
                for (var item = 0; item < storemapcount.Count(); item++)
                {
                    voucherStoreMapping.Id = 0;
                    voucherStoreMapping.VoucherDefinition = createdestination.vouchercodefordestination;
                    voucherStoreMapping.Store = Convert.ToInt32(storemapcount[item]);
                    voucherStoreMapping.IsSource = false;
                    if (createdestination.Disdefault == voucherStoreMapping.Store)
                    {
                        voucherStoreMapping.IsDefault = true;
                    }
                    else
                    {
                        voucherStoreMapping.IsDefault = false;
                    }
                    voucherStoreMapping.Remark = storemapremark[item];
                    var createsourcestore = await _sharedHelpers.createStoreMapSource(voucherStoreMapping);
                }

            }
            return Json("Saved Sucessfully");

        }
        public async Task<IActionResult> CreateStoremapSource(VoucherSearchModel createsource)
        {
            VoucherStoreMappingDTO voucherStoreMapping = new VoucherStoreMappingDTO();

            if (createsource.storemapSourcecode == null)
            {
                return Json("Please Add at least one source");
               
            }
            else
            {
                var storemapcount = createsource.storemapSourcecode.Distinct().ToList();
                var storemapremark = createsource.storemapsource.Distinct().ToList();
              var  storemapListsource = await _sharedHelpers.VoucherStoreMappingVoucher(createsource.vouchercodeforsource, true);
                if (storemapListsource != null && storemapListsource.Count() > 0)
                {
                    foreach (var item in storemapListsource)
                    {
                        var delete = await _sharedHelpers.DeleteStoreMapSource(item.Id);
                    }
                }
                for (var item = 0; item < storemapcount.Count(); item++)
                {
                    voucherStoreMapping.Id = 0;
                    voucherStoreMapping.VoucherDefinition = createsource.vouchercodeforsource;
                    voucherStoreMapping.Store = Convert.ToInt32(storemapcount[item]);
                    voucherStoreMapping.IsSource = true;
                    if (createsource.isDefault  == voucherStoreMapping.Store)
                    {
                        voucherStoreMapping.IsDefault = true;
                    }
                    else
                    {
                        voucherStoreMapping.IsDefault = false;
                    }
                    voucherStoreMapping.Remark = storemapremark[item];
                    var createsourcestore = await _sharedHelpers.createStoreMapSource(voucherStoreMapping);
                }
            }

            return Json("Saved Sucessfully sou");

        }

        public async Task<IActionResult> getFieldformatResult(int formattype, int vouchercode)
        {


            List<FieldFormatDTO> fieldFormats = new List<FieldFormatDTO>();
            List<FieldFormatDTO> fieldFormatsList = new List<FieldFormatDTO>();

            var getfiledformat = await _sharedHelpers.GetfieldformatBytypeandreference(formattype, vouchercode);
            var fieldformat = new VoucherSearchModel() { FieldFormats = getfiledformat, fieldType = formattype };
            return PartialView("fieldformattableMap", fieldformat);
        }
        public async Task<IActionResult> DeleteFiledformatarticle(int code)
        {
           
            var deletefrmat = await _sharedHelpers.DeletefieldformatById(code);
            if (deletefrmat)
            {
                return Json("Delete Successfully  ");
            }
            else
            {
                return Json("Unable to delete Records.Some records are Used by other Transactions.");
            }

        }
        public async Task<IActionResult> createfieldformat(VoucherSearchModel createfieldformat)
        {
            FieldFormatDTO fieldFormat = new FieldFormatDTO();
            if (createfieldformat.fieldfrmat == null ||  createfieldformat.colorfrmat == null || createfieldformat.captionfrmat == null)
            {
                return Json("Enter all fields");
            }
            else
            {
                var fildcode = createfieldformat.code_feldformat;
                if (createfieldformat.code_feldformat != 0)
                {
                    fieldFormat.Id = createfieldformat.code_feldformat;
                    fieldFormat.Type = createfieldformat.filedformatcodeforheadersave;
                    fieldFormat.ObjectComponent = createfieldformat.fieldfrmat;
                    fieldFormat.Reference = createfieldformat.vouchercodeforfiledformat;
                    fieldFormat.Index = createfieldformat.indexfrmat;
                    fieldFormat.Width = createfieldformat.widthfrmat;
                    fieldFormat.Font = createfieldformat.fontfrmat;
                    fieldFormat.Color = createfieldformat.colorfrmat;
                    fieldFormat.Caption = createfieldformat.captionfrmat;
                    fieldFormat.IsRequired = createfieldformat.isrequiredfrmat;
                    fieldFormat.Remark = createfieldformat.remarkfrmat;
                    var updateforma = await _sharedHelpers.Updatefieldformat(fieldFormat);

                }
                else
                {
                    fieldFormat.Id = 0;
                    fieldFormat.Type = createfieldformat.filedformatcodeforheadersave;
                    fieldFormat.ObjectComponent = createfieldformat.fieldfrmat;
                    fieldFormat.Reference = createfieldformat.vouchercodeforfiledformat;
                    fieldFormat.Index = createfieldformat.indexfrmat;
                    fieldFormat.Width = createfieldformat.widthfrmat;
                    fieldFormat.Font = createfieldformat.fontfrmat;
                    fieldFormat.Color = createfieldformat.colorfrmat;
                    fieldFormat.Caption = createfieldformat.captionfrmat;
                    fieldFormat.IsRequired = createfieldformat.isrequiredfrmat;
                    fieldFormat.Remark = createfieldformat.remarkfrmat;
                    var getfildformatbyIndex = await _sharedHelpers.GetfieldformatbyreferenceandIndex(fieldFormat.Reference, fieldFormat.Index);
                    if (getfildformatbyIndex?.Count() >= 1)
                    {
                        return Json("Not Duplicate Index allowed");
                    }
                    else
                    {
                        var cretaeforma = await _sharedHelpers.createfieldformat(fieldFormat);

                    }

                }


            }

            return Json("Saved Successfully");

            //return RedirectToAction("VoucherSetting", "Voucher", createfieldformat.vouchercodeforfiledformat);
        }

        public async Task<IActionResult> GetGSLfiledformatBycode(int code)
        {
            var fldformatList = await _sharedHelpers.GetfieldformatbyId(code);
            var fldformat = fldformatList?.FirstOrDefault();

            return Json(new { code=  fldformat.Id, index=  fldformat.Index, fieldComponent=  fldformat.ObjectComponent, caption = fldformat.Caption, width =  fldformat.Width, color =  fldformat.Color, isRequired=  fldformat.IsRequired, font =  fldformat.Font, remar =  fldformat.Remark });
        }
     
        public async Task<IActionResult> getCopydistribution(int id, int vouchercode)
        {
            List<distribution> dis = new List<distribution>();
            var copydis = await _sharedHelpers.GetDistrubtionByRefeandTye(vouchercode, id);

            var orguntdef = await _sharedHelpers.GetConsigneeByType1(1721);
            var lokup = await _sharedHelpers.GetLookUpByType("Copy Description");

            var copyDis = (from cop in copydis
                           join org in orguntdef
                           on cop.Destination equals org.Id
                           join lok in lokup
                           on cop.Index equals lok.Id
                           select new
                           {
                               cop.Id,
                               cop.Remark,
                               cop.Destination,
                               orgcode = org.Id,
                               org.Description,
                               lokcode = lok.Id,
                               lokdesc = lok.Description
                           }).ToList();


            foreach (var item in copyDis)
            {
                dis.Add(new distribution
                {
                    idd = item.Id,
                    ido = item.orgcode,
                    idl = item.lokcode,
                    descrption = item.Destination.ToString(),
                    ldescrption = item.lokdesc,
                    remark = item?.Remark

                });
            }
            var copyydis = new VoucherSearchModel() { distribution = dis };
            return PartialView("_copydistribution", copyydis);
        }
        public async Task<IActionResult> getsynchronizarion(int id, int vouchercode)
        {
            List<distribution> dis = new List<distribution>();
            int count = 1;
            var comp = _sharedHelpers.GetCompany();
            var comptin = comp?.Result?.Tin;
            var selectsynchronization = await _sharedHelpers.GetCompanyBranchsByTin(comptin);
            var distri = await _sharedHelpers.GetDistrubtionByRefeandTye(vouchercode, 1580);


            var syncro = selectsynchronization?.Select(y => new
            {
                y.Id,
                y.Name,
                sn = count++,
                index = distri.Where(x => x.Destination == y.Id).Any() ? true : false,
                addresssynch = ""


            }).ToList();



            foreach (var item in syncro)
            {
                dis.Add(new distribution
                {
                    idd = item.Id,
                    sn = item.sn,
                    descrption = item.Name,
                    index = item.index,
                    addresse = item.addresssynch

                });
            }
            var copydis = new VoucherSearchModel() { distribution = dis };
            return PartialView("_synchronization", copydis);

        }

        public async Task<IActionResult> getmachinedistribution(int id, int vouchercode)
        {
            List<distribution> dis = new List<distribution>();
            var machdist = await _sharedHelpers.GetDistrubtionByRefeandTye(vouchercode, id);
            var deviceview = await _sharedHelpers.SelectDevice();
            var mach= (from mac in machdist
                                     join dev in deviceview
                                     on mac.Destination equals dev.Id
                                     select new
                                     {
                                         mac.Id,
                                         mac.Description,
                                         mac.Remark,
                                         devcode = dev.Id,
                                         dev.MachineName
                                     }).ToList();


            foreach (var item in mach)
            {
                dis.Add(new distribution
                {
                    idd = item.Id,
                    ido = item.devcode,
                    descrption = item.Description,
                    ldescrption = item.MachineName,
                    remark = item.Remark

                });
            }
            var copydis = new VoucherSearchModel() { distribution = dis };
            return PartialView("_machinedistribution", copydis);
        }
        public async Task<IActionResult> CreatemachineDistribution(VoucherSearchModel createdistribution)
        {

            DistributionDTO distribution = new DistributionDTO();
            if (createdistribution.descriptionmach == null || createdistribution.devicemach == null)
            {
                return Json("Enter all fieldes");
            }
            else
            {
                if (createdistribution.codeforupdatemachinedistribution != 0)
                {
                    distribution.Id = createdistribution.codeforupdatemachinedistribution;
                    distribution.SystemConstant = createdistribution.VoucherSettingCodeforMachine;
                    distribution.Index = 0;
                    distribution.Type = 1579;
                    distribution.Description = createdistribution.descriptionmach;
                    distribution.DestinationPointer = 5;
                    distribution.Destination = Convert.ToInt32(createdistribution.devicemach);
                    distribution.Remark = createdistribution.remaarkmach;
                    var updiss = await _sharedHelpers.UpdateDstribution(distribution);

                }
                else
                {
                    distribution.Id = 0;
                    distribution.SystemConstant = createdistribution.VoucherSettingCodeforMachine;
                    distribution.Index = 0;
                    distribution.Type = 1579;
                    distribution.Description = createdistribution.descriptionmach;
                    distribution.DestinationPointer = 5;
                    distribution.Destination = Convert.ToInt32(createdistribution.devicemach);
                    distribution.Remark = createdistribution.remaarkmach;
                    var creadiss = await _sharedHelpers.CreateDstribution(distribution);

                }
            }


            return Json("Saved Successfully");
        }
     public async Task<IActionResult> SaveSynchronization(VoucherSearchModel Createsych)
        {
                DistributionDTO distribution = new DistributionDTO();

                List<int> distributionunchecklist = new List<int>();
                List<int> distributionlist = new List<int>();
                var getdiss = await _sharedHelpers.GetDistrubtionByRefeandTye(Createsych.VoucherSettingCodeforMachine, 1580);

                if (Createsych.Branchcode != null)
                {


                    if (getdiss.Count() > 1)
                    {
                        for (int i = 0; i < getdiss.Count(); i++)
                        {

                            if (!(Createsych.Branchcode.Contains(getdiss[i].SystemConstant.ToString())))
                            {
                                distributionunchecklist.Add(getdiss.ElementAtOrDefault(i).Id);

                            }
                        }
                        for (int i = 0; i < getdiss.Count(); i++)
                        {

                            if (Createsych.Branchcode.Contains(getdiss[i].SystemConstant.ToString()))
                            {
                                distributionlist.Add(getdiss.ElementAtOrDefault(i).Id);

                            }
                        }

                    }
                    for (var item = 0; item < Createsych.Branchcode.Count(); item++)
                    {
                        var distributionList = Createsych.Branchcode[item].Split("/");

                        distribution.Id = 0;
                        distribution.SystemConstant = Createsych.documentType;
                        distribution.Index =  Convert.ToInt32(distributionList[2]);
                        distribution.Type = 1580;
                        distribution.Description = distributionList[1];
                        distribution.DestinationPointer = 5;
                        distribution.Destination = Convert.ToInt32(distributionList[0]);
                        distribution.Remark = "";
                        var creadiss = await _sharedHelpers.CreateDstribution(distribution);
                    }

                }

                else
                {
                    if (getdiss.Count() > 0)
                    {
                        foreach (var itemdel in getdiss)
                        {

                            var deldissy = await _sharedHelpers.DeleteDistributionById(itemdel.Id);


                        }

                    }

                }
                if (distributionlist.Count() > 0)
                {
                    foreach (var itemdel in distributionlist)
                    {

                        var deldissy = await _sharedHelpers.DeleteDistributionById(itemdel);

                    }

                }
                if (distributionunchecklist.Count() > 0)
                {
                    foreach (var itemdel in distributionunchecklist)
                    {

                        var deldissy = await _sharedHelpers.DeleteDistributionById(itemdel);

                    }

                }

                return Json("Saved Successfully");
}

        public async Task<IActionResult> CreatecopyDistribution(VoucherSearchModel createdistribution)
        {
            DistributionDTO distribution = new DistributionDTO();
            if (createdistribution.copydis == 0 || createdistribution.destinatindis == null)
            {
                return Json("Enter all fieldes");
            }
            else
            {

                if (createdistribution.codeforupdatedistribution != 0)
                {

                    distribution.Id = createdistribution.codeforupdatedistribution;
                    distribution.SystemConstant = createdistribution.VoucherSettingCodeforcopydistribution;
                    distribution.Index = createdistribution.copydis;
                    distribution.Type = 1578;
                    distribution.Description = null;
                    distribution.DestinationPointer = 5;
                    distribution.Destination = createdistribution.destinatindis;
                    distribution.Remark = createdistribution.printerdis;
                    var updiss = await _sharedHelpers.UpdateDstribution(distribution);
                }
                else
                {
                    distribution.Id = 0;
                    distribution.SystemConstant = createdistribution.VoucherSettingCodeforcopydistribution;
                    distribution.Index = createdistribution.copydis;
                    distribution.Type = 1578;
                    distribution.Description = null;
                    distribution.DestinationPointer = 5;
                    distribution.Destination = createdistribution.destinatindis;
                    distribution.Remark = createdistribution.printerdis;
                    var creadis = await _sharedHelpers.CreateDstribution(distribution);

                }
            }
            return Json("Saved Successfully");
        }
        public IActionResult GetPropertyByVoucherDefinition(int id)
        {
            var persontype = new VoucherSearchModel() { documentType = id };
            return PartialView("_VoucherProperty", persontype);
        }
        public static List<VoucherSearchModel> fieldformatmodel { get; set; }
        public IActionResult CreateDefaultFieldFormat(string attribute)
        {
            var attr = attribute;

            fieldformatmodel = new List<VoucherSearchModel>
            {
                new VoucherSearchModel (){Attribute ="id",Caption="id",Width =110},
                new VoucherSearchModel (){Attribute ="consignee1Id",Caption="consignee1Id",Width =110},
                new VoucherSearchModel (){Attribute ="issuedDate",Caption="Issued Date",Width =110},
                new VoucherSearchModel (){Attribute ="isIssued",Caption="Is Issued",Width =50},
                new VoucherSearchModel (){Attribute ="isVoid",Caption="Is Void",Width =50},
                new VoucherSearchModel (){Attribute ="day",Caption="day",Width =50},
                new VoucherSearchModel (){Attribute ="month",Caption="month",Width =50},
                new VoucherSearchModel (){Attribute ="year",Caption="year",Width =50},
                new VoucherSearchModel (){Attribute ="consignee1FullName",Caption="consignee1 Full Name",Width =150},
                new VoucherSearchModel (){Attribute ="consignee1PreferenceId",Caption="consignee1 PreferenceId",Width =150},
                new VoucherSearchModel (){Attribute ="consignee1PreferenceId",Caption="consignee1 PreferenceId",Width =150},


               new VoucherSearchModel (){Attribute ="voucherDefinitionName",Caption="Voucher Definition Name",Width =150},
               new VoucherSearchModel (){Attribute ="consigneeCode",Caption="Consignee Code",Width =100},
             
               new VoucherSearchModel (){Attribute ="consigneeTIN",Caption="Consignee TIN",Width =110},
               new VoucherSearchModel (){Attribute ="consigneeTelephone",Caption="Consignee Telephone",Width =110},
               new VoucherSearchModel (){Attribute ="childPreferenceCode",Caption="Child Preference Code",Width =110},
               new VoucherSearchModel (){Attribute ="childPreferenceDesc",Caption="Child Preference Description",Width =150},
               new VoucherSearchModel (){Attribute ="parentPreferenceCode",Caption="Parent Preference Code",Width =100},
               new VoucherSearchModel (){Attribute ="parentPreferenceDesc",Caption="Parent Preference Description",Width =150},
              
              
               new VoucherSearchModel (){Attribute ="periodCode",Caption="Period Code",Width =50},
               new VoucherSearchModel (){Attribute ="periodName",Caption="Period Name",Width =100},
               new VoucherSearchModel (){Attribute ="periodTypeCode",Caption="Period Type Code",Width =50},
               new VoucherSearchModel (){Attribute ="periodTypeDescription",Caption="Period Type Description",Width =100},
               new VoucherSearchModel (){Attribute ="periodStartDateTime",Caption="Period Start DateTime",Width =100},
               new VoucherSearchModel (){Attribute ="periodEndDateTime",Caption="Period End DateTime",Width =100},
               new VoucherSearchModel (){Attribute ="deviceCode",Caption="Device Code",Width =50},
               new VoucherSearchModel (){Attribute ="deviceName",Caption="Device Name",Width =110},
               new VoucherSearchModel (){Attribute ="userCode",Caption="User Code",Width =50},
               new VoucherSearchModel (){Attribute ="userName",Caption="User Name",Width =110},
               new VoucherSearchModel (){Attribute ="orgUnitDefCode",Caption="Organization Unit Definition Code",Width =110},
               new VoucherSearchModel (){Attribute ="orgUnitDefDescription",Caption="Organization Unit Definition Description",Width =150},
               new VoucherSearchModel (){Attribute ="subTotal",Caption="SubTotal",Width =105},
               new VoucherSearchModel (){Attribute ="additionalCharge",Caption="Additional Charge",Width =105},
               new VoucherSearchModel (){Attribute ="discount",Caption="Discount",Width =105},
               new VoucherSearchModel (){Attribute ="grandTotal",Caption="Grand Total",Width =105},
               new VoucherSearchModel (){Attribute ="lastObjectStateCode",Caption="Last Object State Code",Width =110},
               new VoucherSearchModel (){Attribute ="lastObjectStateDescription",Caption="Last Object State Description",Width =150},
               new VoucherSearchModel (){Attribute ="color",Caption="Color",Width =80},
               new VoucherSearchModel (){Attribute ="voucherNote",Caption="Voucher Note",Width =200},
               new VoucherSearchModel (){Attribute ="sourceStoreCode",Caption="Source Store Code",Width =110},
               new VoucherSearchModel (){Attribute ="sourceStoreName",Caption="Source Store Name",Width =150},
               new VoucherSearchModel (){Attribute ="destinationStoreCode",Caption="Destination Store Code",Width =110},
               new VoucherSearchModel (){Attribute ="destinationStoreName",Caption="Destination Store Name",Width =150},
               new VoucherSearchModel (){Attribute ="transactionType",Caption="Transaction Type",Width =110},
               new VoucherSearchModel (){Attribute ="transactionTypeDescription",Caption="Transaction Type Description",Width =150},
               new VoucherSearchModel (){Attribute ="remark",Caption="Voucher Remark",Width =200},
               new VoucherSearchModel (){Attribute ="firstVouchExtCode",Caption="First Voucher Extension Code",Width =110},
               new VoucherSearchModel (){Attribute ="firstVouchExtDescription",Caption="First Voucher Extension Description",Width =150},
               new VoucherSearchModel (){Attribute ="firstVouchExtValue",Caption="First Voucher Extension Value",Width =110},
               new VoucherSearchModel (){Attribute ="secondVouchExtCode",Caption="Second Voucher Extension Code",Width =110},
               new VoucherSearchModel (){Attribute ="secondVouchExtDescription",Caption="Second Voucher Extension Description",Width =150},
               new VoucherSearchModel (){Attribute ="secondVouchExtValue",Caption="Second Voucher Extension Value",Width =110},
               new VoucherSearchModel (){Attribute ="thirdVouchExtCode",Caption="Third Voucher Extension Code",Width =110},
               new VoucherSearchModel (){Attribute ="thirdVouchExtDescription",Caption="Third Voucher Extension Description",Width =150},
               new VoucherSearchModel (){Attribute ="thirdVouchExtValue",Caption="Third Voucher Extension Value",Width =110},
               new VoucherSearchModel (){Attribute ="fourthVouchExtCode",Caption="Fourth Voucher Extension Code",Width =110},
               new VoucherSearchModel (){Attribute ="fourthVouchExtDescription",Caption="Fourth Voucher Extension Description",Width =150},
               new VoucherSearchModel (){Attribute ="fourthVouchExtValue",Caption="Fourth Voucher Extension Value",Width =110},
               new VoucherSearchModel (){Attribute ="VATtaxableAmount",Caption="VAT Taxable Amount",Width =105},
               new VoucherSearchModel (){Attribute ="VATtaxAmount",Caption="VAT Tax Amount",Width =105},
               new VoucherSearchModel (){Attribute ="TOT1taxableAmount",Caption="TOT1 Taxable Amount",Width =105},
               new VoucherSearchModel (){Attribute ="TOT1taxAmount",Caption="TOT1 Tax Amount",Width =105},
               new VoucherSearchModel (){Attribute ="TOT2taxableAmount",Caption="TOT2 Taxable Amount",Width =105},
               new VoucherSearchModel (){Attribute ="TOT2taxAmount",Caption="TOT2 Tax Amount",Width =105},
               new VoucherSearchModel (){Attribute ="NonTaxableAmount",Caption="Non Taxable Amount",Width =105},
               new VoucherSearchModel (){Attribute ="WithholdingTaxableAmount",Caption="Withholding Taxable Amount",Width =105},
               new VoucherSearchModel (){Attribute ="IncomeTaxtaxableAmount",Caption="Income Taxable Amount",Width =105},
               new VoucherSearchModel (){Attribute ="IncomeTaxAmount",Caption="Income Tax Amount",Width =105},
               new VoucherSearchModel (){Attribute ="EmployeePensionTaxableAmount",Caption="Employee Pension Taxable Amount",Width =105},
               new VoucherSearchModel (){Attribute ="EmployeePensionTaxAmount",Caption="Employee Pension Tax Amount",Width =105},
               new VoucherSearchModel (){Attribute ="CompanyPensionTaxableAmount",Caption="Company Pension Taxable Amount",Width =105},
               new VoucherSearchModel (){Attribute ="CompanyPensionTaxAmount",Caption="Company Pension Tax Amount",Width =105},
               new VoucherSearchModel (){Attribute ="WithholdingTaxAmount",Caption="Withholding Tax Amount",Width =105},
               new VoucherSearchModel (){Attribute ="backwardReferences",Caption="Backward References",Width =110},
               new VoucherSearchModel (){Attribute ="forwardReferences",Caption="Forward References",Width =110},
               new VoucherSearchModel (){Attribute ="InternalReference",Caption="Internal Reference",Width =110},
               new VoucherSearchModel (){Attribute ="firstOtherConsReqGSLCode",Caption="First Other Consignee ReqGSL Code",Width =110},
               new VoucherSearchModel (){Attribute ="firstOtherConsigneeCode",Caption="First Other Consignee Code",Width =110},
               new VoucherSearchModel (){Attribute ="firstOtherConsigneeName",Caption="First Other Consignee Name",Width =150},
               new VoucherSearchModel (){Attribute ="secondOtherConsReqGSLCode",Caption="Second Other Consignee ReqGSL Code",Width =110},
               new VoucherSearchModel (){Attribute ="secondOtherConsigneeCode",Caption="Second Other Consignee Code",Width =110},
               new VoucherSearchModel (){Attribute ="secondOtherConsigneeName",Caption="Second Other Consignee Name",Width =150},
               new VoucherSearchModel (){Attribute ="thirdOtherConsReqGSLCode",Caption="Third Other Consignee ReqGSL Code",Width =110},
               new VoucherSearchModel (){Attribute ="thirdOtherConsigneeCode",Caption="Third Other Consignee Code",Width =110},
               new VoucherSearchModel (){Attribute ="thirdOtherConsigneeName",Caption="Third Other Consignee Name",Width =150},
               new VoucherSearchModel (){Attribute ="fsNo",Caption="FS No.",Width =110},
               new VoucherSearchModel (){Attribute ="mrsNo",Caption="MRC No.",Width =110},
               new VoucherSearchModel (){Attribute ="waiterCode",Caption="Waiter Code",Width =110},
               new VoucherSearchModel (){Attribute ="waiterName",Caption="Waiter Name",Width =150},
               new VoucherSearchModel (){Attribute ="table",Caption="Table",Width =110},
               new VoucherSearchModel (){Attribute ="cover",Caption="Cover",Width =110},
               new VoucherSearchModel (){Attribute ="paymentMethodCode",Caption="Payment Method Code",Width =110},
               new VoucherSearchModel (){Attribute ="paymentMethodDescription",Caption="Payment Method Description",Width =150},
               new VoucherSearchModel (){Attribute ="paymentProcesserCode",Caption="Payment Processer Code",Width =110},
               new VoucherSearchModel (){Attribute ="paymentProcesserDescription",Caption="Payment Processer Description",Width =150},
               new VoucherSearchModel (){Attribute ="bankCode",Caption="Bank Code",Width =110},
               new VoucherSearchModel (){Attribute ="bankName",Caption="Bank Name",Width =150},
               new VoucherSearchModel (){Attribute ="nonCashPaymentIssuedDate",Caption="Non-Cash Payment Issued Date",Width =110},
               new VoucherSearchModel (){Attribute ="nonCashPaymentMaturityDate",Caption="Non-Cash Payment Maturity Date",Width =110},
               new VoucherSearchModel (){Attribute ="nonCashPaymentNumber",Caption="Non-Cash Payment Number",Width =110},
               new VoucherSearchModel (){Attribute ="nonCashPaymentRate",Caption="Non-Cash Payment Rate",Width =110},
               new VoucherSearchModel (){Attribute ="nonCashPaymentAmount",Caption="Non-Cash Payment Amount",Width =110},
               new VoucherSearchModel (){Attribute ="nonCashPaymentCurrencyCode",Caption="Non-Cash Payment Currency Code",Width =110},
               new VoucherSearchModel (){Attribute ="nonCashPaymentCurrencyDescription",Caption="Non-Cash Payment Currency Description",Width =150},
               new VoucherSearchModel (){Attribute ="nonCashPaymentCurrencyAbbreviation",Caption="Non-Cash Payment Currency Abbreviation",Width =110},
               new VoucherSearchModel (){Attribute ="lineItemCount",Caption="Line Item Count",Width =80},
               new VoucherSearchModel (){Attribute ="lineItemQtySum",Caption="Line Item Quantity Sum",Width =110},
               new VoucherSearchModel (){Attribute ="AllLineItemArticleCode",Caption="All Line Item Article Code",Width =200},
               new VoucherSearchModel (){Attribute ="AllLineItemArticleName",Caption="All Line Item Article Name",Width =220},
               new VoucherSearchModel (){Attribute ="voucherOrgUnitCode",Caption="Voucher Organization Unit Code",Width =110},
               new VoucherSearchModel (){Attribute ="voucherOrgUnitDescription",Caption="Voucher Organization Unit Description",Width =150},
               new VoucherSearchModel (){Attribute ="prodDate",Caption="Production Date",Width =110},
               new VoucherSearchModel (){Attribute ="expiryDate",Caption="Expiry Date",Width =110},
               new VoucherSearchModel (){Attribute ="voucherLifeSpan",Caption="Voucher LifeSpan",Width =110},
               new VoucherSearchModel (){Attribute ="firstArticleCode",Caption="First Article Code",Width =110},
               new VoucherSearchModel (){Attribute ="firstArticleName",Caption="First Article Name",Width =150},
               new VoucherSearchModel (){Attribute ="lineItemProdDate",Caption="Line Item Production Date",Width =110},
               new VoucherSearchModel (){Attribute ="lineitemExpiryDate",Caption="Line Item Expiry Date",Width =110},
               new VoucherSearchModel (){Attribute ="lineItemLifeSpan",Caption="Line Item Life Span",Width =110},
               new VoucherSearchModel (){Attribute ="subSystem",Caption="Sub System",Width =150},
               new VoucherSearchModel (){Attribute ="lastActivity",Caption="lastActivity",Width =110},
               new VoucherSearchModel (){Attribute ="quantitySum",Caption="Quantity Sum",Width =110},
               new VoucherSearchModel (){Attribute ="PersonCode",Caption="Person Code",Width =110},
               new VoucherSearchModel (){Attribute ="title",Caption="Title",Width =110},
               new VoucherSearchModel (){Attribute ="TitleDescription",Caption="Title Description",Width =110},
               new VoucherSearchModel (){Attribute ="firstName",Caption="First Name",Width =110},
               new VoucherSearchModel (){Attribute ="middleName",Caption="Middle Name",Width =110},
               new VoucherSearchModel (){Attribute ="lastName",Caption="Last Name",Width =110},
               new VoucherSearchModel (){Attribute ="name",Caption="Name",Width =200},
               new VoucherSearchModel (){Attribute ="GslType",Caption="GSL Type",Width =50},
               new VoucherSearchModel (){Attribute ="genderCode",Caption="Gender Code",Width =110},
               new VoucherSearchModel (){Attribute ="genderDesc",Caption="Gender Description",Width =110},
               new VoucherSearchModel (){Attribute ="nationality",Caption="Nationality",Width =110},
               new VoucherSearchModel (){Attribute ="nationalityName",Caption="Nationality Name",Width =110},
               new VoucherSearchModel (){Attribute ="DOB",Caption="Date of Birth",Width =110},
               new VoucherSearchModel (){Attribute ="age",Caption="Age",Width =50},
               new VoucherSearchModel (){Attribute ="isActive",Caption="Is Active",Width =50},
               new VoucherSearchModel (){Attribute ="TIN",Caption="TIN",Width =110},
               new VoucherSearchModel (){Attribute ="Telephone",Caption="Telephone",Width =110},
               new VoucherSearchModel (){Attribute ="taxTypeCode",Caption="Tax Type Code",Width =110},
               new VoucherSearchModel (){Attribute ="taxTypeDesc",Caption="Tax Type Description",Width =110},
               new VoucherSearchModel (){Attribute ="taxTypeAmount",Caption="Tax Type Amount",Width =110},
               new VoucherSearchModel (){Attribute ="preferenceCode",Caption="Preference Code",Width =110},
               new VoucherSearchModel (){Attribute ="preferenceDesc",Caption="Preference Description",Width =150},
               new VoucherSearchModel (){Attribute ="prefParentCode",Caption="Preference Parent Code",Width =110},
               new VoucherSearchModel (){Attribute ="prefParentDesc",Caption="Preference Parent Description",Width =150},
               new VoucherSearchModel (){Attribute ="objectStateDefnCode",Caption="Object State Definition Code",Width =110},
               new VoucherSearchModel (){Attribute ="objectStateDefnDesc",Caption="Object State Definition Description",Width =150},
               new VoucherSearchModel (){Attribute ="ObjectStateColor",Caption="Object State Color",Width =110},
               new VoucherSearchModel (){Attribute ="objectStateFont",Caption="Object State Font",Width =110},
               new VoucherSearchModel (){Attribute ="cityAddress",Caption="City Address",Width =110},
               new VoucherSearchModel (){Attribute ="countryAddress",Caption="Country Address",Width =110},
               new VoucherSearchModel (){Attribute ="departmentCode",Caption="Department Code",Width =110},
               new VoucherSearchModel (){Attribute ="departmentDesc",Caption="Department Description",Width =150},
               new VoucherSearchModel (){Attribute ="activityDef",Caption="Activity Definition",Width =110},
               new VoucherSearchModel (){Attribute ="startTimStamp",Caption="Start Time Date",Width =110},
               new VoucherSearchModel (){Attribute ="endTimStamp",Caption="End Time Date",Width =110},
               new VoucherSearchModel (){Attribute ="OrganizationCode",Caption="Organization Code",Width =110},
               new VoucherSearchModel (){Attribute ="tradeName",Caption="Trade Name",Width =150},
               new VoucherSearchModel (){Attribute ="brandName",Caption="Brand Name",Width =150},
               new VoucherSearchModel (){Attribute ="name",Caption="Name",Width =200},
               new VoucherSearchModel (){Attribute ="businessType",Caption="Business Type",Width =110},
               new VoucherSearchModel (){Attribute ="businessTypeName",Caption="Business Type Name",Width =150},
               new VoucherSearchModel (){Attribute ="taxType",Caption="Tax Type Code",Width =110},
               new VoucherSearchModel (){Attribute ="PhoneNumber",Caption="Phone Number",Width =110},
               new VoucherSearchModel (){Attribute ="contactPerson",Caption="Contact Person",Width =150},
               new VoucherSearchModel (){Attribute ="assignedPerson",Caption="Assigned Person",Width =150},
               new VoucherSearchModel (){Attribute ="representative",Caption="Representative",Width =150},
               new VoucherSearchModel (){Attribute ="owners",Caption="Owners",Width =150},
               new VoucherSearchModel (){Attribute ="OrgUnitCount",Caption="Organization Unit Count",Width =110},
               new VoucherSearchModel (){Attribute ="NoOfEmployee",Caption="No Of Employee",Width =110},
               new VoucherSearchModel (){Attribute ="NoOfRepresentative",Caption="No Of Representative",Width =110},
               new VoucherSearchModel (){Attribute ="NoOfAssignedPrsn",Caption="No Of Assigned Person",Width =110},
               new VoucherSearchModel (){Attribute ="NoOfOwner",Caption="No Of Owner",Width =110},
               new VoucherSearchModel (){Attribute ="ArticleCode",Caption="Article Code",Width =110},
               new VoucherSearchModel (){Attribute ="ArticleName",Caption="Article Name",Width =150},
               new VoucherSearchModel (){Attribute ="preferenceDesc",Caption="Preference Description",Width =110},
               new VoucherSearchModel (){Attribute ="prefParentCode",Caption="Preference Parent Code",Width =110},
               new VoucherSearchModel (){Attribute ="prefParentDesc",Caption="Preference Parent Description",Width =150},
               new VoucherSearchModel (){Attribute ="uomCode",Caption="Unit of Measurement Code",Width =110},
               new VoucherSearchModel (){Attribute ="uomDesc",Caption="Unit of Measurement Description",Width =150},
               new VoucherSearchModel (){Attribute ="length",Caption="Length",Width =110},
               new VoucherSearchModel (){Attribute ="width",Caption="Width",Width =110},
               new VoucherSearchModel (){Attribute ="height",Caption="Height",Width =110},
               new VoucherSearchModel (){Attribute ="weight",Caption="Weight",Width =110},
               new VoucherSearchModel (){Attribute ="brand",Caption="Brand",Width =110},
               new VoucherSearchModel (){Attribute ="generic",Caption="Generic",Width =110},
               new VoucherSearchModel (){Attribute ="model",Caption="Model",Width =110},
               new VoucherSearchModel (){Attribute ="size",Caption="Size",Width =110},
               new VoucherSearchModel (){Attribute ="productColor",Caption="Product Color",Width =110},
               new VoucherSearchModel (){Attribute ="country",Caption="Country",Width =110},
               new VoucherSearchModel (){Attribute ="countryName",Caption="Country Name",Width =150},
               new VoucherSearchModel (){Attribute ="manufacturer",Caption="Manufacturer",Width =150},
               new VoucherSearchModel (){Attribute ="defaultPriceDescription",Caption="Default Price Description",Width =150},
               new VoucherSearchModel (){Attribute ="defaultPriceValue",Caption="Default Price Value",Width =110},
               new VoucherSearchModel (){Attribute ="taxTypeCode",Caption="Tax Type Code",Width =110},
               new VoucherSearchModel (){Attribute ="taxTypeDesc",Caption="Tax Type Description",Width =110},
               new VoucherSearchModel (){Attribute ="taxTypeValue",Caption="Tax Type Value",Width =110},
               new VoucherSearchModel (){Attribute ="lifeTimeValue",Caption="Life Time Value",Width =110},
               new VoucherSearchModel (){Attribute ="lifetimeFactorCode",Caption="Life Time Factor Code",Width =110},
               new VoucherSearchModel (){Attribute ="lifetimeFactorDesc",Caption="Life Time Factor Description",Width =150},
               new VoucherSearchModel (){Attribute ="optionCodesValue",Caption="Option Codes Value",Width =110},
            };
            var filedformat = fieldformatmodel.Where(x => x.Attribute == attribute).Select(y => new { captionn = y.Caption, widthh = y.Width }).FirstOrDefault();
            return Json(filedformat);
        }
        public async Task<IActionResult> GetstoremapDestination(int code, string desc,bool add)
        {
            VoucherSearchModel searchModel = new VoucherSearchModel();
            if (add)
            {
                _MapSstore?._destination.RemoveAll(r => r.Name == desc && r.Id == code);
                _MapSstore._vdestination.Add(new VoucherStoreMap
                {
                    Id = 0,
                    IsDefault = false,
                    Store = code,
                    Remark = desc,
                });
            }
            else
            {
                _MapSstore?._vdestination.RemoveAll(r => r.Store == code);
                _MapSstore._destination.Add(new ConsigneeUnitM
                {
                    Id = code,
                    Name = desc
                });
            }
            var destin = _MapSstore?._destination;
            if (destin != null && destin.Count() > 0)
            {
                foreach (var item in destin)
                {
                    searchModel._destination.Add(new ConsigneeUnitDTO
                    {
                        Id = item.Id,
                        Name = item.Name
                    });
                }
            }  
            var vdestin = _MapSstore?._vdestination;
            if (vdestin != null && vdestin.Count() > 0)
            {
                foreach (var item in vdestin)
                {
                    searchModel._vdestination.Add(new VoucherStoreMappingDTO
                    {
                        Id = item.Id,
                        Store = item.Store,
                        IsDefault = item.IsDefault,
                        Remark = item.Remark,
                    });
                }
            } 
            var source = _MapSstore?._source;
            if (source != null && source.Count() > 0)
            {
                foreach (var item in source)
                {
                    searchModel._source.Add(new ConsigneeUnitDTO
                    {
                        Id = item.Id,
                        Name = item.Name
                    });
                }
            } 
            var vsource = _MapSstore?._vsource;
            if (vsource != null && vsource.Count() > 0)
            {
                foreach (var item in vsource)
                {
                    searchModel._vsource.Add(new VoucherStoreMappingDTO
                    {
                        Id = item.Id,
                        Store = item.Store,
                        IsDefault = item.IsDefault,
                        Remark = item.Remark,
                    });
                }
            }
            return PartialView("mapstoregeneral", searchModel);
        }
        public async Task<IActionResult> GetstoremapSource(int code, string desc,bool add)
        {
            VoucherSearchModel searchModel = new VoucherSearchModel();
            if (add)
            {
                _MapSstore?._source.RemoveAll(r => r.Name == desc && r.Id == code);
                _MapSstore._vsource.Add(new VoucherStoreMap
                {
                    Id = 0,
                    IsDefault = false,
                    Store = code,
                    Remark = desc,
                });
            }
            else
            {
                _MapSstore?._vsource.RemoveAll(r => r.Store == code);
                _MapSstore._source.Add(new ConsigneeUnitM
                {
                    Id = code,
                    Name = desc
                });
            }
            var destin = _MapSstore?._source;
            if (destin != null && destin.Count() > 0)
            {
                foreach (var item in destin)
                {
                    searchModel._source.Add(new ConsigneeUnitDTO
                    {
                        Id = item.Id,
                        Name = item.Name
                    });
                }
            }  
            var vdestin = _MapSstore?._vsource;
            if (vdestin != null && vdestin.Count() > 0)
            {
                foreach (var item in vdestin)
                {
                    searchModel._vsource.Add(new VoucherStoreMappingDTO
                    {
                        Id = item.Id,
                        Store = item.Store,
                        IsDefault = item.IsDefault,
                        Remark = item.Remark,
                    });
                }
            } 
            var source = _MapSstore?._destination;
            if (source != null && source.Count() > 0)
            {
                foreach (var item in source)
                {
                    searchModel._destination.Add(new ConsigneeUnitDTO
                    {
                        Id = item.Id,
                        Name = item.Name
                    });
                }
            } 
            var vsource = _MapSstore?._vdestination;
            if (vsource != null && vsource.Count() > 0)
            {
                foreach (var item in vsource)
                {
                    searchModel._vdestination.Add(new VoucherStoreMappingDTO
                    {
                        Id = item.Id,
                        Store = item.Store,
                        IsDefault = item.IsDefault,
                        Remark = item.Remark,
                    });
                }
            }
            return PartialView("mapstoregeneral", searchModel);
        }
        public async Task<IActionResult> GetAllstoremap(int code)
        {
            _MapSstore._destination = new List<ConsigneeUnitM>();
            _MapSstore._source = new List<ConsigneeUnitM>();
            _MapSstore._vsource = new List<VoucherStoreMap>();
            _MapSstore._vdestination = new List<VoucherStoreMap>();
            List<VoucherStoreMappingDTO> storemapListdestination = new List<VoucherStoreMappingDTO>();
            List<VoucherStoreMappingDTO> storemapListsource = new List<VoucherStoreMappingDTO>();
            if (code != 0)
            {
                storemapListdestination = await _sharedHelpers.VoucherStoreMappingVoucher(code, false);

            }
            if (code != 0)
            {
                storemapListsource = await _sharedHelpers.VoucherStoreMappingVoucher(code, true);

            }
            var storemapList = await _sharedHelpers.GetConsigneeByType1(1727);

            List<ConsigneeUnitDTO> organizationUnitdestination = new List<ConsigneeUnitDTO>();
            List<ConsigneeUnitDTO> organizationUnitsource = new List<ConsigneeUnitDTO>();

           VoucherSearchModel searchModel = new VoucherSearchModel();
                if (storemapListdestination != null && storemapListdestination.Count() > 0)
               {
                        foreach (var item in storemapList)
                        {
                            if (!storemapListdestination.Select(x => x.Store).Contains(item.Id))
                            {
                                organizationUnitdestination.Add(new ConsigneeUnitDTO { Id = item.Id, Name = item.Name });

                            }
                        }
                }
                else
                {
                     organizationUnitdestination = storemapList;
                }
          
                if(storemapListsource != null && storemapListsource.Count() > 0)
                {
                        foreach (var item in storemapList)
                        {
                            if (!storemapListsource.Select(x => x.Store).Contains(item.Id))
                            {
                                organizationUnitsource.Add(new ConsigneeUnitDTO { Id = item.Id, Name = item.Name });

                            }
                        }
                }
                else
                {
                    organizationUnitsource = storemapList;
            }
                    searchModel._vsource = storemapListsource;
                    searchModel._vdestination = storemapListdestination;
                    searchModel._source = organizationUnitsource;
                    searchModel._destination = organizationUnitdestination;

                        if (organizationUnitdestination != null && organizationUnitdestination.Count() > 0)
                        {
                            foreach (var item in organizationUnitdestination)
                            {
                                _MapSstore._destination.Add(new ConsigneeUnitM
                                {
                                    Id = item.Id,
                                    Name = item.Name,
                                });
                            }
                        }  
                        if (organizationUnitsource != null && organizationUnitsource.Count() > 0)
                        {
                            foreach (var item in organizationUnitsource)
                            {
                                _MapSstore._source.Add(new ConsigneeUnitM
                                {
                                    Id = item.Id,
                                    Name = item.Name,
                                });
                            }
                        }
                           if (storemapListsource != null && storemapListsource.Count() > 0)
                            {
                                foreach (var item in storemapListsource)
                                {
                                    _MapSstore._vsource.Add(new VoucherStoreMap
                                    {
                                        Id = item.Id,
                                        IsDefault = item.IsDefault,
                                        Store = item.Store,
                                        Remark = item.Remark,
                                    });
                                }
                            }
                         if (storemapListdestination != null && storemapListdestination.Count() > 0)
                            {
                                foreach (var item in storemapListdestination)
                                {
                                    _MapSstore._vdestination.Add(new VoucherStoreMap
                                    {
                                        Id = item.Id,
                                        IsDefault = item.IsDefault,
                                        Store = item.Store,
                                        Remark = item.Remark,
                                    });
                                }
                            }
            return PartialView("mapstoregeneral", searchModel);
        }           
        public async Task<IActionResult> DeleteNestedVoucher(int code)
        {

            var getrel = await _sharedHelpers.GetRElatioSateByrel(code);
            var delrelatsta = await _sharedHelpers.DeleteRelationById(code);
            foreach (var item in getrel)
            {
                var delrelat = await _sharedHelpers.DeleteRelationSateById(item.Id);
            }
            return Json(new EmptyResult());
        }

        #region Get Current User Organization Unit
        private async Task currentUserAndUnit()
        {
            var authUser = await _authenticationManager.GetAuthenticatedUser();
            if (authUser != null && authUser.Remark != "" && authUser.Remark != null)
            {
                string[] splitRem = authUser.Remark.Split('_');
                loggedUser = authUser.Id;
            }
        }
        #endregion
    }
}



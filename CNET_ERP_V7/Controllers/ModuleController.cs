using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CNET_ERP_V7.Common.AuthNavigation;
using CNET_ERP_V7.Models.FramworkModels;
using CNET_ERP_V7.Common.Company;
using CNET_ERP_V7.Models.SubSytsemModel;
using CNET_ERP_V7.Models;
using CNET_V7_Domain;
using Microsoft.Extensions.Configuration;
using CNET_V7_Domain.Domain.SettingSchema;
using CNET_V7_Domain.Domain.ArticleSchema;
using CNET_V7_Domain.Domain.ConsigneeSchema;
using System.Collections;
namespace CNET_ERP_V7.Controllers
{
    public class ModuleController : Controller
    {
        private AuthenticationManager _authenticationManager;
        private readonly HttpClient _httpClient;
        private readonly SharedHelpers _sharedHelpers;
        public List<ConfigurationDTO> Config = new List<ConfigurationDTO>();
        #region Ctor
        public ModuleController(AuthenticationManager authenticationManager,
              IHttpClientFactory httpClientFactory,
              SharedHelpers sharedHelpers)
        {
            _authenticationManager = authenticationManager;
            _httpClient = httpClientFactory.CreateClient("mainclient");
            _sharedHelpers = sharedHelpers;
        }

        #endregion

        public async Task<IActionResult> List(int id)
        {
            var authUser = await _authenticationManager.GetAuthenticatedUser();

            if (authUser == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return View(new moduleModel
            {
                subSyType = id,

            });

        }
        public async Task<IActionResult> SavingwarehouseProperty(moduleModel wareHModel)

        {
            List<ConfigurationDTO> VoucherConfiglist = new List<ConfigurationDTO>();
          
          
            Config  = await _sharedHelpers.GetConfigurationByRefandPref("Inventory Setting", CNETConstants.PRODUCT_INVENTORY_SUB_MODULE);
           
            var closingperiod = wareHModel.ware_ClosingPeriod == null ? "" : wareHModel.ware_ClosingPeriod;

           await SaveproppertyData("Closing Frequency", wareHModel.ware_ClosingFrequency == null ? "" : wareHModel.ware_ClosingFrequency, "Inventory Setting", CNETConstants.PRODUCT_INVENTORY_SUB_MODULE);
           await SaveproppertyData("Closing Frequency", wareHModel.ware_ClosingFrequency, "Inventory Setting", CNETConstants.PRODUCT_INVENTORY_SUB_MODULE);
           await SaveproppertyData("Closing Period", closingperiod, "Inventory Setting", CNETConstants.PRODUCT_INVENTORY_SUB_MODULE);
           await SaveproppertyData("Enable Dead Stock Notification", wareHModel.ware_EnableDeadStockNotification, "Inventory Setting", CNETConstants.PRODUCT_INVENTORY_SUB_MODULE);
           await  SaveproppertyData("Enable Expiry Date Notification", wareHModel.ware_EnableExpiryDateNotification, "Inventory Setting", CNETConstants.PRODUCT_INVENTORY_SUB_MODULE);
           await SaveproppertyData("Enable Minimum Level Notification", wareHModel.ware_EnableMinimumLevelNotification, "Inventory Setting", CNETConstants.PRODUCT_INVENTORY_SUB_MODULE);
           await SaveproppertyData("Enforce Closing", wareHModel.ware_EnforceClosing, "Inventory Setting", CNETConstants.PRODUCT_INVENTORY_SUB_MODULE);
           await SaveproppertyData("ABCAnalysisValuation", wareHModel.ware_ABCAnalysisValuation, "Inventory Setting", CNETConstants.PRODUCT_INVENTORY_SUB_MODULE);
           await SaveproppertyData("Dead Stock duration in days", wareHModel.ware_DeadStockdurationindays, "Inventory Setting", CNETConstants.PRODUCT_INVENTORY_SUB_MODULE);
           await SaveproppertyData("EnableStockBalanceService", wareHModel.ware_EnableStockBalanceService, "Inventory Setting", CNETConstants.PRODUCT_INVENTORY_SUB_MODULE);
           await SaveproppertyData("Show CS Number", wareHModel.ware_ShowCSNumber, "Inventory Setting", CNETConstants.PRODUCT_INVENTORY_SUB_MODULE);
           await SaveproppertyData("StockBalanceCalculation", wareHModel.ware_StockBalanceCalculation, "Inventory Setting", CNETConstants.PRODUCT_INVENTORY_SUB_MODULE);
           await  SaveproppertyData("StockBalanceServiceStarts", wareHModel.ware_Stock_BalanceServiceStarts, "Inventory Setting", CNETConstants.PRODUCT_INVENTORY_SUB_MODULE);
           await SaveproppertyData("UpdateStockBalanceItems", wareHModel.ware_UpdateStockBalanceItems, "Inventory Setting", CNETConstants.PRODUCT_INVENTORY_SUB_MODULE);

            return Json("Saved Successfully");

        }
        public async Task<IActionResult> SavingfinancialProperty(moduleModel finanModel)

        {
            List<ConfigurationDTO > ConfigList = new List<ConfigurationDTO>();


            Config = await _sharedHelpers.GetConfigurationByRefandPref("Accounting Setting", CNETConstants.FINANCIAL_MGT_SYS);

           await SaveproppertyData("Send As Master Stock Item", finanModel.finan_SendAsMasterStockItem, "Accounting Setting", CNETConstants.FINANCIAL_MGT_SYS);
           await SaveproppertyData("Send As Master Stock Item", finanModel.finan_SendAsMasterStockItem, "Accounting Setting", CNETConstants.FINANCIAL_MGT_SYS);
           await SaveproppertyData("Real Time Cost Management", finanModel.finan_RealTimeCostManagement, "Accounting Setting", CNETConstants.FINANCIAL_MGT_SYS);
           await SaveproppertyData("Enforce Closing", finanModel.finan_EnforceClosing, "Accounting Setting", CNETConstants.FINANCIAL_MGT_SYS);
           await SaveproppertyData("Closing Period", finanModel.finan_ClosingPeriod, "Accounting Setting", CNETConstants.FINANCIAL_MGT_SYS);
           await SaveproppertyData("Closing Frequency", finanModel.finan_ClosingFrequency, "Accounting Setting", CNETConstants.FINANCIAL_MGT_SYS);
           await SaveproppertyData("Interest Income Bank Reconcile Account", finanModel.finan_InterestIncomeBankReconcileAccount, "Accounting Setting", CNETConstants.FINANCIAL_MGT_SYS);
           await SaveproppertyData("Service Charge Bank Reconcile Account", finanModel.finan_ServiceChargeBankReconcileAccount, "Accounting Setting", CNETConstants.FINANCIAL_MGT_SYS);
           await SaveproppertyData("Default Payable Account", finanModel.finan_defaultpaymentaccount, "Accounting Setting", CNETConstants.FINANCIAL_MGT_SYS);
           await SaveproppertyData("Default Receivable Account", finanModel.finan_defaultreceivableaccount, "Accounting Setting", CNETConstants.FINANCIAL_MGT_SYS);
           await SaveproppertyData("Income Summary Account", finanModel.finan_incomesummaryaccount, "Accounting Setting", CNETConstants.FINANCIAL_MGT_SYS);
           await SaveproppertyData("Closing Capital Account", finanModel.finan_Closingcapitalaccount, "Accounting Setting", CNETConstants.FINANCIAL_MGT_SYS);
           await SaveproppertyData("Production Contra Account", finanModel.finan_ProductionContraAccount, "Accounting Setting", CNETConstants.FINANCIAL_MGT_SYS);
           await SaveproppertyData("GIT Account", finanModel.finan_gitaccount, "Accounting Setting", CNETConstants.FINANCIAL_MGT_SYS);
           await SaveproppertyData("Include Stock Adjustment On StockLedger", finanModel.finan_IncludeStockAdjustmentOnStockLedger, "Accounting Setting", CNETConstants.FINANCIAL_MGT_SYS);
           await SaveproppertyData("Include Sales Return On StockLedger", finanModel.finan_IncludeSalesReturnOnStockLedger, "Accounting Setting", CNETConstants.FINANCIAL_MGT_SYS);
           await SaveproppertyData("Consignee To Peachtree Selection Order", finanModel.finan_ConsigneeToPeachtreeSelectionOrder.ToString(), "Accounting Setting", CNETConstants.FINANCIAL_MGT_SYS);
           await SaveproppertyData("Production Flow", finanModel.finan_ProductionFlow, "Accounting Setting", CNETConstants.FINANCIAL_MGT_SYS);
           await SaveproppertyData("PeachtreeVersion", finanModel.finan_PeachtreeVersion, "Accounting Setting", CNETConstants.FINANCIAL_MGT_SYS);
           await SaveproppertyData("PeachtreeServiceChargeAccount", finanModel.finan_PeachtreeServiceChargeAccount, "Accounting Setting", CNETConstants.FINANCIAL_MGT_SYS);
           await SaveproppertyData("PeachtreeTaxAccount", finanModel.finan_PeachtreeTaxAccount, "Accounting Setting", CNETConstants.FINANCIAL_MGT_SYS);
           await SaveproppertyData("Cost Posting Method", finanModel.finan_CostPostingMethod, "Accounting Setting", CNETConstants.FINANCIAL_MGT_SYS);
           await SaveproppertyData("Perpetual Cost Method", finanModel.finan_PerpetualCostMethod, "Accounting Setting", CNETConstants.FINANCIAL_MGT_SYS);
           await SaveproppertyData("Stock Movement Cost Calculation", finanModel.finan_StockMovementCostCalculation, "Accounting Setting", CNETConstants.FINANCIAL_MGT_SYS);
           await SaveproppertyData("Sales Journal Type", finanModel.finan_SalesJournalType, "Accounting Setting", CNETConstants.FINANCIAL_MGT_SYS);
           await SaveproppertyData("Closing Mandatory", finanModel.finan_ClosingMandatory, "Accounting Setting", CNETConstants.FINANCIAL_MGT_SYS);
           await SaveproppertyData("Show Balance On TrialBalance", finanModel.finan_ShowBalanceOnTrialBalance, "Accounting Setting", CNETConstants.FINANCIAL_MGT_SYS);
           await SaveproppertyData("Allow Zero Cost Posting", finanModel.finan_AllowZeroCostPosting, "Accounting Setting", CNETConstants.FINANCIAL_MGT_SYS);
           await SaveproppertyData("Payroll Journal Posting Type", finanModel.finan_PayrollJournalPostingType, "Accounting Setting", CNETConstants.FINANCIAL_MGT_SYS);
           await SaveproppertyData("Beginning Period", finanModel.finan_BeginningPeriod, "Accounting Setting", CNETConstants.FINANCIAL_MGT_SYS);
           await SaveproppertyData("Revenue Journal Type", finanModel.finan_RevenueJournalType, "Accounting Setting", CNETConstants.FINANCIAL_MGT_SYS );
           await SaveproppertyData("Expense Journal Type", finanModel.finan_ExpenseJournalType, "Accounting Setting", CNETConstants.FINANCIAL_MGT_SYS);
           await SaveproppertyData("Enable Cost Center", finanModel.finan_enablecostcenter, "Accounting Setting", CNETConstants.FINANCIAL_MGT_SYS);

            return Json("Saved Successfully");

        }

        public async Task<IActionResult> SavingproductProperty(moduleModel produModel)
        {
            List<ConfigurationDTO> ConfigList = new List<ConfigurationDTO>();

            Config = await _sharedHelpers.GetConfigurationByRefandPref("Production Settin", CNETConstants.PRODUCTION_MGT_SYS);
           

            var closing_period = produModel.produ_ClosingPeriod == null ? "None" : produModel.produ_ClosingPeriod;
           await SaveproppertyData("Enforce Closing", produModel.produ_EnforceClosing, "Production Setting", CNETConstants.PRODUCTION_MGT_SYS);
           await SaveproppertyData("Enforce Closing", produModel.produ_EnforceClosing, "Production Setting", CNETConstants.PRODUCTION_MGT_SYS);
           await SaveproppertyData("Closing Period", closing_period, "Production Setting", CNETConstants.PRODUCTION_MGT_SYS);
           await SaveproppertyData("Closing Frequency", produModel.produ_ClosingFrequency, "Production Setting", CNETConstants.PRODUCTION_MGT_SYS);

           await SaveproppertyData("Deduct Production Raw Materials", produModel.produ_DeductProductionRawMaterials, "Production Setting", CNETConstants.PRODUCTION_MGT_SYS);
           await SaveproppertyData("Formation Depth", produModel.produ_FormationDepth, "Production Setting", CNETConstants.PRODUCTION_MGT_SYS);
           await SaveproppertyData("Production Type", produModel.produ_ProductionType, "Production Setting", CNETConstants.PRODUCTION_MGT_SYS);
           await SaveproppertyData("Consider Variation At Packing", produModel.produ_Consider_Variation_At_Packing, "Production Setting", CNETConstants.PRODUCTION_MGT_SYS);

            return Json("Saved Successfully");
        }
        public async Task<IActionResult> SavingfixedassetProperty(moduleModel FixeduModel)

        {
            List<ConfigurationDTO> ConfigList = new List<ConfigurationDTO>();
            Config = await _sharedHelpers.GetConfigurationByRefandPref("Fixed Asset Setting", CNETConstants.FIXED_ASSET_MGT_SYS);

           
            var minimumfixedas = FixeduModel.fixed_MinimumFixedAssetValue == 0 ? 0 : FixeduModel.fixed_MinimumFixedAssetValue;

           await SaveproppertyData("Minimum Fixed Asset Value", minimumfixedas.ToString(), "Fixed Asset Setting", CNETConstants.FIXED_ASSET_MGT_SYS);
           await SaveproppertyData("Minimum Fixed Asset Value", minimumfixedas.ToString(), "Fixed Asset Setting", CNETConstants.FIXED_ASSET_MGT_SYS);
           await SaveproppertyData("Depreciation Date Range", FixeduModel.fixed_DepreciationDateRange, "Fixed Asset Setting", CNETConstants.FIXED_ASSET_MGT_SYS);

            return Json("Saved Successfully");
        }
        public async Task<IActionResult> SavinghrmsProperty(moduleModel hrmsModel)
        {
            List<ConfigurationDTO> ConfigList = new List<ConfigurationDTO>();
            Config = await _sharedHelpers.GetConfigurationByRefandPref("HRMS Setting", CNETConstants.HUMAN_RESOURCE_MGT_SYS);

           
           await SaveproppertyData("Attendance Payroll Application", hrmsModel.hrms_Attendancepayrollapplication, "HRMS Setting", CNETConstants.HUMAN_RESOURCE_MGT_SYS);
           await SaveproppertyData("Attendance Payroll Application", hrmsModel.hrms_Attendancepayrollapplication, "HRMS Setting",CNETConstants.HUMAN_RESOURCE_MGT_SYS);
           await SaveproppertyData("Absent", hrmsModel.hrms_Absent.ToString(), "HRMS Setting", CNETConstants.HUMAN_RESOURCE_MGT_SYS);
           await SaveproppertyData("EarlyIn", hrmsModel.hrms_EarlyIn.ToString(), "HRMS Setting", CNETConstants.HUMAN_RESOURCE_MGT_SYS);
           await SaveproppertyData("EarlyOut", hrmsModel.hrms_EarlyOut.ToString(), "HRMS Setting", CNETConstants.HUMAN_RESOURCE_MGT_SYS);
           await SaveproppertyData("LateIn", hrmsModel.hrms_LateIn.ToString(), "HRMS Setting", CNETConstants.HUMAN_RESOURCE_MGT_SYS);
           await SaveproppertyData("LateOut", hrmsModel.hrms_LateOut.ToString(), "HRMS Setting", CNETConstants.HUMAN_RESOURCE_MGT_SYS);
           await SaveproppertyData("MachineReadingTime", hrmsModel.hrms_MachineReadingTime, "HRMS Setting", CNETConstants.HUMAN_RESOURCE_MGT_SYS);
           await SaveproppertyData("Attendance Sync Path", hrmsModel.hrms_AttendanceSyncPath, "HRMS Setting", CNETConstants.HUMAN_RESOURCE_MGT_SYS);
           await SaveproppertyData("MinTimeBetweenLogs", hrmsModel.hrms_MinTimeBetweenLogs.ToString(), "HRMS Setting", CNETConstants.HUMAN_RESOURCE_MGT_SYS);
           await SaveproppertyData("MaxLates", hrmsModel.hrms_maxLates, "HRMS Setting", CNETConstants.HUMAN_RESOURCE_MGT_SYS);
           await SaveproppertyData("Payroll Period Offset", hrmsModel.hrms_PayrollPeriodOffset, "HRMS Setting", CNETConstants.HUMAN_RESOURCE_MGT_SYS);
           await SaveproppertyData("Attendance Analysis During Sync", hrmsModel.hrms_AttendanceAnalysisDuringSync, "HRMS Setting", CNETConstants.HUMAN_RESOURCE_MGT_SYS);
           await SaveproppertyData("Attendance Sync type", hrmsModel.hrms_AttendanceSynctype, "HRMS Setting", CNETConstants.HUMAN_RESOURCE_MGT_SYS);
           await SaveproppertyData("Sync In Organization", hrmsModel.hrms_SyncInOrganization, "HRMS Setting", CNETConstants.HUMAN_RESOURCE_MGT_SYS);
           await SaveproppertyData("AttendanceAbsentRules", hrmsModel.hrms_attendanceAbsentRules, "HRMS Setting", CNETConstants.HUMAN_RESOURCE_MGT_SYS);
           await SaveproppertyData("Validate Leave And Service Date", hrmsModel.hrms_ValidateLeaveAndServiceDate.ToString(), "HRMS Setting", CNETConstants.HUMAN_RESOURCE_MGT_SYS);
           await SaveproppertyData("Lates Include Early Checkout", hrmsModel.hrms_LatesIncludeEarlyCheckout.ToString(), "HRMS Setting", CNETConstants.HUMAN_RESOURCE_MGT_SYS);
           await SaveproppertyData("Payroll Dates", hrmsModel.hrms_PayrollDates.ToString(), "HRMS Setting", CNETConstants.HUMAN_RESOURCE_MGT_SYS);

            return Json("Saved Successfully");

        }
        public async Task<IActionResult> SavingPropertySetting(moduleModel PropertyModel)

        {
            List<ConfigurationDTO> ConfigList = new List<ConfigurationDTO>();
            Config = await _sharedHelpers.GetConfigurationByRefandPref("51", CNETConstants.PROPERTY_MGT_SYS);


            var labelprinter = PropertyModel.pms_LabelPrinter == null ? "Fax" : PropertyModel.pms_LabelPrinter;
            var lostArticle = PropertyModel.pms_LostFeeArticle == null ? "P-00002" : PropertyModel.pms_LostFeeArticle;

           await SaveproppertyData("Message Check Frequency", PropertyModel.pms_MessageCheckFrequency, "59", CNETConstants.PROPERTY_MGT_SYS);
           await SaveproppertyData("Message Check Frequency", PropertyModel.pms_MessageCheckFrequency, "51", CNETConstants.PROPERTY_MGT_SYS);
           await SaveproppertyData("Summary Voucher", PropertyModel.pms_SummaryVoucher, "51", CNETConstants.PROPERTY_MGT_SYS);
           await SaveproppertyData("Enforce Closing", PropertyModel.pms_EnforceClosing, "51", CNETConstants.PROPERTY_MGT_SYS);
           await SaveproppertyData("Closing Frequency", PropertyModel.pms_ClosingFrequency, "51", CNETConstants.PROPERTY_MGT_SYS);
           await SaveproppertyData("Closing Period", PropertyModel.pms_ClosingPeriod, "51", CNETConstants.PROPERTY_MGT_SYS);
           await SaveproppertyData("Generate Report For Null Data", PropertyModel.pms_GenerateReportForNullData, "51", CNETConstants.PROPERTY_MGT_SYS);
           await SaveproppertyData("Enable Cloud Reporting", PropertyModel.pms_EnableCloudReporting, "51", CNETConstants.PROPERTY_MGT_SYS);
           await SaveproppertyData("CheckInTime", PropertyModel.pms_CheckInTime, "51", CNETConstants.PROPERTY_MGT_SYS);
           await SaveproppertyData("CheckOutTime", PropertyModel.pms_CheckOutTime, "51", CNETConstants.PROPERTY_MGT_SYS);
           await SaveproppertyData("LateCheckoutTime", PropertyModel.pms_LateCheckoutime, "51", CNETConstants.PROPERTY_MGT_SYS);
           await SaveproppertyData("NightAuditTime", PropertyModel.pms_NightAuditTime, "51", CNETConstants.PROPERTY_MGT_SYS);
           await SaveproppertyData("UndoCheckinMin", PropertyModel.pms_UndoCheckinTime, "51", CNETConstants.PROPERTY_MGT_SYS);
           await SaveproppertyData("MinRateAdjustment", PropertyModel.pms_MinimumRateAdujstment, "51", CNETConstants.PROPERTY_MGT_SYS);
           await SaveproppertyData("MattressAmount", PropertyModel.pms_MattressAmount, "51", CNETConstants.PROPERTY_MGT_SYS);
           await SaveproppertyData("ArchivePath", PropertyModel.pms_repoArchivePath, "51", CNETConstants.PROPERTY_MGT_SYS);
           await SaveproppertyData("UseLateCheckout", PropertyModel.pms_UseLateCheckout, "51", CNETConstants.PROPERTY_MGT_SYS);
           await SaveproppertyData("ArchivePrint", PropertyModel.pms_autoprint, "51", CNETConstants.PROPERTY_MGT_SYS);
           await SaveproppertyData("LateCheckoutRequiredPayment", PropertyModel.pms_RequiredPayment, "51", CNETConstants.PROPERTY_MGT_SYS);
           await SaveproppertyData("LateCheckoutAdditionPayment", PropertyModel.pms_AdditionalPaymentperhour, "51", CNETConstants.PROPERTY_MGT_SYS);
           await SaveproppertyData("EnforceCheckoutCardReturn", PropertyModel.pms_EnforceCardReturn, "51", CNETConstants.PROPERTY_MGT_SYS);
           await SaveproppertyData("LostCardFeeArticle", lostArticle, "51", CNETConstants.PROPERTY_MGT_SYS);
           await SaveproppertyData("Label Design File", PropertyModel.pms_LabelDesignFile, "51", CNETConstants.PROPERTY_MGT_SYS);
           await SaveproppertyData("Label Printer", labelprinter, "51", CNETConstants.PROPERTY_MGT_SYS);
           await SaveproppertyData("EnableJournalization", PropertyModel.pms_EnableJournalize, "51", CNETConstants.PROPERTY_MGT_SYS);
           await SaveproppertyData("NightAuditPostingRoutine", PropertyModel.pms_PostineRoutine, "51", CNETConstants.PROPERTY_MGT_SYS);
           await SaveproppertyData("DefaultFiscalBillType", PropertyModel.pms_DefaultFiscalBillType, "51", CNETConstants.PROPERTY_MGT_SYS);
           await SaveproppertyData("CustHighBalance", PropertyModel.pms_CustomerHighBalance, "51", CNETConstants.PROPERTY_MGT_SYS);
           await SaveproppertyData("ValidateExternalReference", PropertyModel.pms_ValidateExternalReference, "51", CNETConstants.PROPERTY_MGT_SYS);
           await SaveproppertyData("ChargeAtCheckin", PropertyModel.pms_ChargeAtCheckin, "51", CNETConstants.PROPERTY_MGT_SYS);
           await SaveproppertyData("Early CheckIn", PropertyModel.pms_EarlyCheckIn, "51", CNETConstants.PROPERTY_MGT_SYS);
            return Json("Saved Successfully");
        }
        public async Task<IActionResult> SavingecommProperty(moduleModel ecommModel)
        {
            List<ConfigurationDTO> ConfigList = new List<ConfigurationDTO>();
            Config = await _sharedHelpers.GetConfigurationByRefandPref("ECommerce Setting", CNETConstants.ECOMMERCE_MGT_SYS);

           
           await SaveproppertyData("Ecommerce Order Print", ecommModel.ecommerceOrderPrint, "ECommerce Setting", CNETConstants.ECOMMERCE_MGT_SYS);
           await SaveproppertyData("Control Stock", ecommModel.controlStock, "ECommerce Setting", CNETConstants.ECOMMERCE_MGT_SYS);
           await SaveproppertyData("Use Service Charge", ecommModel.useServiceCharge, "ECommerce Setting", CNETConstants.ECOMMERCE_MGT_SYS);
           await SaveproppertyData("Show Stock Below", ecommModel.showStockBelow, "ECommerce Setting", CNETConstants.ECOMMERCE_MGT_SYS);
           await SaveproppertyData("Receive order during off working hours", ecommModel.receiveorderduringoffworkinghours, "ECommerce Setting", CNETConstants.ECOMMERCE_MGT_SYS);
           await SaveproppertyData("Receive Order On Closing Hours", ecommModel.receiveOrderOnClosingHours, "ECommerce Setting", CNETConstants.ECOMMERCE_MGT_SYS);
           await SaveproppertyData("Organization Unit Selection", ecommModel.organizationUnitSelection, "ECommerce Setting", CNETConstants.ECOMMERCE_MGT_SYS);
           await SaveproppertyData("Maximum Seat", ecommModel.ecommerce_Maximum_Seat, "ECommerce Setting", CNETConstants.ECOMMERCE_MGT_SYS);
           await SaveproppertyData("Service Charge Article", ecommModel.ecommerce_Charge_Article, "ECommerce Setting", CNETConstants.ECOMMERCE_MGT_SYS);
           await SaveproppertyData("Terms and Conditions URL", ecommModel.ecommerce_TermandCondition_URL, "ECommerce Setting", CNETConstants.ECOMMERCE_MGT_SYS);
           await SaveproppertyData("Year of Establishment", ecommModel.ecommerce_Yearof_Establishment, "ECommerce Setting", CNETConstants.ECOMMERCE_MGT_SYS);
           await SaveproppertyData("Founder/CEO", ecommModel.ecommerce_Founder, "ECommerce Setting", CNETConstants.ECOMMERCE_MGT_SYS);
           await SaveproppertyData("No of Employee", ecommModel.ecommerce_Noof_Employee, "ECommerce Setting", CNETConstants.ECOMMERCE_MGT_SYS);
           await SaveproppertyData("Minimum Stock Quantity", ecommModel.ecommerce_Minimstock_Quality, "ECommerce Setting", CNETConstants.ECOMMERCE_MGT_SYS );
           await SaveproppertyData("Maximum Stock Quantity", ecommModel.ecommerce_maxstock_quantity, "ECommerce Setting", CNETConstants.ECOMMERCE_MGT_SYS);
           await SaveproppertyData("2nd Instalment Percent", ecommModel.ecommerce_2nd_InvestmentMax_Percent, "ECommerce Setting", CNETConstants.ECOMMERCE_MGT_SYS);
           await SaveproppertyData("2nd Instalment Range", ecommModel.ecommerce_2nd_InvestmentMax_Range, "ECommerce Setting", CNETConstants.ECOMMERCE_MGT_SYS);
           await SaveproppertyData("2nd Instalment Max Age", ecommModel.ecommerce_2nd_InvestmentMax_Age, "ECommerce Setting", CNETConstants.ECOMMERCE_MGT_SYS);
           await SaveproppertyData("3rd Instalment Percent", ecommModel.ecommerce_3rd_InvestmentMax_Percent, "ECommerce Setting", CNETConstants.ECOMMERCE_MGT_SYS);
           await SaveproppertyData("3rd Instalment Range", ecommModel.ecommerce_3rd_InvestmentMax_Range, "ECommerce Setting", CNETConstants.ECOMMERCE_MGT_SYS);
           await SaveproppertyData("3rd Instalment Max Age", ecommModel.ecommerce_3nd_InvestmentMax_Age, "ECommerce Setting", CNETConstants.ECOMMERCE_MGT_SYS);
           await SaveproppertyData("4th Instalment Percent", ecommModel.ecommerce_4th_Investment_percent, "ECommerce Setting", CNETConstants.ECOMMERCE_MGT_SYS);
           await SaveproppertyData("4th Instalment Max Age", ecommModel.ecommerce_4thd_InvestmentMax_Age, "ECommerce Setting", CNETConstants.ECOMMERCE_MGT_SYS);
           await SaveproppertyData("4th Instalment Range", ecommModel.ecommerce_4th_Investment_Range, "ECommerce Setting", CNETConstants.ECOMMERCE_MGT_SYS);
           await SaveproppertyData("5th Instalment Percent", ecommModel.ecommerce_5th_Investment_percent, "ECommerce Setting", CNETConstants.ECOMMERCE_MGT_SYS);
           await SaveproppertyData("5th Instalment Range", ecommModel.ecommerce_5th_Investment_Range, "ECommerce Setting", CNETConstants.ECOMMERCE_MGT_SYS);
           await SaveproppertyData("5th Instalment Max Age", ecommModel.ecommerce_5th_InvestmentMax_Age, "ECommerce Setting", CNETConstants.ECOMMERCE_MGT_SYS);
           await SaveproppertyData("6th Instalment Percent", ecommModel.ecommerce_6th_InvestmentMax_percent, "ECommerce Setting", CNETConstants.ECOMMERCE_MGT_SYS);
           await SaveproppertyData("6th Instalment Range", ecommModel.ecommerce_6th_InvestmentMax_Range, "ECommerce Setting", CNETConstants.ECOMMERCE_MGT_SYS);
           await SaveproppertyData("6th Instalment Max Age", ecommModel.ecommerce_6th_InvestmentMax_Age, "ECommerce Setting", CNETConstants.ECOMMERCE_MGT_SYS);
           await SaveproppertyData("7th Instalment Range", ecommModel.ecommerce_7th_InvestmentMax_Range, "ECommerce Setting", CNETConstants.ECOMMERCE_MGT_SYS);
           await SaveproppertyData("7th Instalment Max Age", ecommModel.ecommerce_7th_InvestmentMax_Age, "ECommerce Setting", CNETConstants.ECOMMERCE_MGT_SYS);
           await SaveproppertyData("7th Instalment Percent", ecommModel.ecommerce_7th_InvestmentMax_Percent, "ECommerce Setting", CNETConstants.ECOMMERCE_MGT_SYS);
           await SaveproppertyData("8th Instalment Percent", ecommModel.ecommerce_8th_InvestmentMax_Percent, "ECommerce Setting", CNETConstants.ECOMMERCE_MGT_SYS);
           await SaveproppertyData("8th Instalment Range", ecommModel.ecommerce_8th_InvestmentMax_Range, "ECommerce Setting", CNETConstants.ECOMMERCE_MGT_SYS);
           await SaveproppertyData("8th Instalment Max Age", ecommModel.ecommerce_8th_InvestmentMax_age, "ECommerce Setting", CNETConstants.ECOMMERCE_MGT_SYS);
           await SaveproppertyData("Stock Article", ecommModel.ecommerce_StockArticle, "ECommerce Setting", CNETConstants.ECOMMERCE_MGT_SYS);



            if (!string.IsNullOrWhiteSpace(ecommModel.defaultOrganizationUnitDef))
            {
                string DefaultOrganizationUnitDefcode = ecommModel.defaultOrganizationUnitDef.ToString()?.Split('/')[0];
                if (!string.IsNullOrWhiteSpace(DefaultOrganizationUnitDefcode))
                {
                   await SaveproppertyData("Default OrganizationUnit Definition", DefaultOrganizationUnitDefcode, "ECommerce Setting", CNETConstants.ECOMMERCE_MGT_SYS);
                }
            }
            if (!string.IsNullOrWhiteSpace(ecommModel.takeAwayBoxArticle))
            {
                string Articlecode = ecommModel.takeAwayBoxArticle?.ToString()?.Split('/')[0];
                if (!string.IsNullOrWhiteSpace(Articlecode))
                {
                   await SaveproppertyData("Take Away Box Article", Articlecode, "ECommerce Setting", CNETConstants.ECOMMERCE_MGT_SYS);

                }
            }
            if (!string.IsNullOrWhiteSpace(ecommModel.usePriceType))
            {
                string UsePriceType = ecommModel.usePriceType?.ToString()?.Split('/')[0];
                if (!string.IsNullOrWhiteSpace(UsePriceType))
                {
                   await SaveproppertyData("Use Price Type", UsePriceType, "ECommerce Setting", CNETConstants.ECOMMERCE_MGT_SYS);
                }
            }

            return Json("Saved Successfully");

        }
        public async Task<ConfigurationDTO> SaveproppertyData(string attr, string curr, string refeType, int PrefType)

        {
            var arrtr = attr;
            var curent = curr;

            ConfigurationDTO configuration = new ConfigurationDTO();
            List<ConfigurationDTO> listcConfigurations = new List<ConfigurationDTO>();
            ConfigurationDTO Prev = Config.Where(x => x.Attribute.Trim() == attr.Trim()).FirstOrDefault();
            configuration.Id = Prev != null ? Prev.Id : 0;
            configuration.Pointer = PrefType;
            configuration.Reference = refeType;
            configuration.Attribute  = attr;
            configuration.CurrentValue = curr == null ? "" : curr;
            if (Prev != null)
            {
                configuration.PreviousValue = Prev.CurrentValue ;
            }
            else
            {
                configuration.PreviousValue  = curr == null ? "" : curr;
            }
            await _sharedHelpers.SaveCofiguration(configuration);


            return configuration;

        }
        public async Task<IActionResult> OrganizationunitDetail(string subSYscode)
        {
            var orgunit = await _sharedHelpers.GetConsignee();
            var orgunitdef = await _sharedHelpers.GetConsigneeunit();

            var OrgunitList = orgunit.Where(x => x.Id == Convert.ToInt32(subSYscode)).Select(y => new
            {
                y.Id,
                y.Remark,

                orgunitDef = (from unitdef in orgunitdef
                              where unitdef.Id == y.Id
                              select new { unitdef.Id, unitdef.Name }).FirstOrDefault(),


            }).ToList();

            return Json(OrgunitList);

        }

        public async Task<IActionResult> createOrgunit([FromForm] moduleModel createorgunitdef)
        {

            ConsigneeUnitDTO createorgunit = new ConsigneeUnitDTO();


            //if (createorgunitdef.Org_unitcode != null)
            //{
            //    createorgunit.Id = createorgunitdef.Org_unitcode;
            //    createorgunit.co = CNETConstantes.HRMS;
            //    createorgunit.reference = CNETConstantes.HRMS;
            //    createorgunit.organizationUnitDefinition = createorgunitdef.Org_unit;
            //    createorgunit.remark = createorgunitdef.Org_remark;

            //    var updateorg = await _uIProcessManager.UpdateOrgUnit(createorgunit);
            //}

            //else
            //{

            //    createorgunit.code = "";
            //    createorgunit.component = CNETConstantes.HRMS;
            //    createorgunit.reference = CNETConstantes.HRMS;
            //    createorgunit.organizationUnitDefinition = createorgunitdef.Org_unit;
            //    createorgunit.remark = createorgunitdef.Org_remark;

            //    var crorgunitdef = await _uIProcessManager.Creatorgunit(createorgunit);

            //}


            return Json("Saved Successfully");
        }
        //public async Task<IActionResult> Deleteorgunitdef(string CODE)
        //{

        //    var deletorgunit = await _uIProcessManager.Deleteorganizationunit(CODE);


        //    return Json("Deleted Successfully");
        //}

    }
}

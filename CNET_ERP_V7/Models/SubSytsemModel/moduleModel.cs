using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace CNET_ERP_V7.Models.SubSytsemModel
{
    public class moduleModel
    {
        public int subSyType { get; set; }
        public string subpeopertyType { get; set; }
        public string subpeoperty { get; set; }

        #region warehouse Managenetn system

        public string ware_ClosingFrequency { get; set; }
        public string ware_ClosingPeriod { get; set; }
        public string ware_EnableDeadStockNotification { get; set; }
        public string ware_EnableExpiryDateNotification { get; set; }
        public string ware_EnableMinimumLevelNotification { get; set; }
        public string ware_EnforceClosing { get; set; }
        public string ware_ABCAnalysisValuation { get; set; }
        public string ware_DeadStockdurationindays { get; set; }
        public string ware_EnableStockBalanceService { get; set; }
        public string ware_ShowCSNumber { get; set; }
        public string ware_StockBalanceCalculation { get; set; }
        public string ware_Stock_BalanceServiceStarts { get; set; }
        public string ware_UpdateStockBalanceItems { get; set; }
        public string Warehouseprooperty { get; set; }

        public List<SelectListItem> ClosingFrequency { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "Every_Day", Text = "Every_Day" },
            new SelectListItem { Value = "Every_Period", Text = "Every_Period" },
            new SelectListItem { Value = "Every_Shift", Text = "Every_Shift" },
            new SelectListItem { Value = "Every_Quarter", Text = "Every_Quarter" },
            new SelectListItem { Value = "Every_Fiscal_Year", Text = "Every_Fiscal_Year" },

        };

        public List<SelectListItem> ENABLENOTIFICATION { get; } = new List<SelectListItem>
        {

            new SelectListItem { Value = "False", Text = "False" },
            new SelectListItem { Value =  "True", Text = "True" },
        };
        public List<SelectListItem> abcvaluation { get; } = new List<SelectListItem>
        {

            new SelectListItem { Value = "PercentBased", Text = "PercentBased" },
            new SelectListItem { Value =  "ValueBased", Text = "ValueBased" },
        };
        public List<SelectListItem> stokebalance { get; } = new List<SelectListItem>
        {

            new SelectListItem { Value = "Realtime", Text = "Realtime" },
            new SelectListItem { Value =  "Runtime", Text = "Runtime" },
        };
        public List<SelectListItem> updaetstokebalance { get; } = new List<SelectListItem>
        {

            new SelectListItem { Value = "Daily", Text = "Daily" },
            new SelectListItem { Value =  "Weekly", Text = "Weekly" },
            new SelectListItem { Value =  "Monthly", Text = "Monthly" },
            new SelectListItem { Value =  "Yearly", Text = "Yearly" },
            new SelectListItem { Value =  "All", Text = "All" },
        };

        #endregion

        #region finanicial management system 
        public string finan_ClosingFrequency { get; set; }
        public string finan_ClosingPeriod { get; set; }
        public string finan_EnforceClosing { get; set; }
        public string finan_Closingcapitalaccount { get; set; }
        public string finan_defaultpaymentaccount { get; set; }
        public string finan_defaultreceivableaccount { get; set; }
        public string finan_gitaccount { get; set; }
        public string finan_incomesummaryaccount { get; set; }
        public string finan_InterestIncomeBankReconcileAccount { get; set; }
        public string finan_ProductionContraAccount { get; set; }
        public string finan_ServiceChargeBankReconcileAccount { get; set; }
        public string finan_AllowZeroCostPosting { get; set; }
        public string finan_BeginningPeriod { get; set; }
        public string finan_ClosingMandatory { get; set; }
        public string finan_ConsigneeToPeachtreeSelectionOrder { get; set; }
        public string finan_CostPostingMethod { get; set; }
        public string finan_enablecostcenter { get; set; }
        public string finan_ExpenseJournalType { get; set; }
        public string finan_IncludeSalesReturnOnStockLedger { get; set; }
        public string finan_IncludeStockAdjustmentOnStockLedger { get; set; }
        public string finan_PayrollJournalPostingType { get; set; }
        public string finan_PerpetualCostMethod { get; set; }
        public string finan_ProductionFlow { get; set; }
        public string finan_RealTimeCostManagement { get; set; }
        public string finan_RevenueJournalType { get; set; }
        public string finan_SalesJournalType { get; set; }
        public string finan_ShowBalanceOnTrialBalance { get; set; }
        public string finan_StockMovementCostCalculation { get; set; }
        public string finan_PeachtreeServiceChargeAccount { get; set; }
        public string finan_PeachtreeVersion { get; set; }
        public string finan_PeachtreeTaxAccount { get; set; }
        public string finan_SendAsMasterStockItem { get; set; }

        public string FinancialPropertySetting { get; set; }


        public List<SelectListItem> postmethod { get; } = new List<SelectListItem>
        {

            new SelectListItem { Value = "Perpetual", Text = "Perpetual" },
            new SelectListItem { Value =  "Periodic", Text = "Periodic" },
            new SelectListItem { Value =  "Calculated_Cost", Text = "Calculated_Cost" },
            new SelectListItem { Value =  "production_cost", Text = "production_cost" },
        };
        public List<SelectListItem> JornyType { get; } = new List<SelectListItem>
        {

            new SelectListItem { Value = "Cash", Text = "Cash" },
            new SelectListItem { Value =  "Accrual", Text = "Accrual" },
            };

        public List<SelectListItem> payyrollJourny { get; } = new List<SelectListItem>
        {

            new SelectListItem { Value = "branch", Text = "branch" },
            new SelectListItem { Value =  "department", Text = "department" },
            };
        public List<SelectListItem> costMethod { get; } = new List<SelectListItem>
        {

            new SelectListItem { Value = "From_WAC", Text = "From_WAC" },
            new SelectListItem { Value =  "From_Calcualted_Cost", Text = "From_Calcualted_Cost" },
            new SelectListItem { Value =  "From_Unit_Price", Text = "From_Unit_Price" },
            };
        public List<SelectListItem> productionflow { get; } = new List<SelectListItem>
        {

            new SelectListItem { Value = "Standard", Text = "Standard" },
            new SelectListItem { Value =  "Actual", Text = "Actual" },
               };
        public List<SelectListItem> salsesjoury { get; } = new List<SelectListItem>
        {

            new SelectListItem { Value = "servicesalesjournal", Text = "servicesalesjournal" },
            new SelectListItem { Value =  "productsalesjournal", Text = "productsalesjournal" },
            new SelectListItem { Value =  "usedmappedaccount", Text = "usedmappedaccount" },

               };

        public List<SelectListItem> stockmovment { get; } = new List<SelectListItem>
        {

            new SelectListItem { Value = "From_WAC", Text = "From_WAC" },
            new SelectListItem { Value =  "From_Unit_Price", Text = "From_Unit_Price" },
            new SelectListItem { Value =  "NotApplicable", Text = "NotApplicable" },
            };
        public List<SelectListItem> peachtree { get; } = new List<SelectListItem>
        {

            new SelectListItem { Value = "Peachtree_2010", Text = "Peachtree_2010" },
            new SelectListItem { Value =  "Peachtree_2010_csv", Text = "Peachtree_2010_csv" },
            new SelectListItem { Value =  "Peachtree_2013_csv", Text = "Peachtree_2013_csv" },
            new SelectListItem { Value =  "Peachtree_2014_csv", Text = "Peachtree_2014_csv" },
            };

        #endregion

        #region production management system
        public string produ_ClosingFrequency { get; set; }
        public string produ_ClosingPeriod { get; set; }
        public string produ_EnforceClosing { get; set; }
        public string produ_DeductProductionRawMaterials { get; set; }
        public string produ_FormationDepth { get; set; }
        public string produ_ProductionType { get; set; }
        public string produ_Consider_Variation_At_Packing { get; set; }
        public string ProductionSetting { get; set; }


        public List<SelectListItem> deductraw { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "AtSales", Text = "AtSales" },
            new SelectListItem { Value =  "AtFormulation", Text = "AtFormulation" },
            new SelectListItem { Value =  "AtPackinglist", Text = "AtPackinglist" },
            new SelectListItem { Value =  "AtSalesSummary", Text = "AtSalesSummary" },
            new SelectListItem { Value =  "AtProductionRelease", Text = "AtProductionRelease" },
        };
        public List<SelectListItem> formationdpth { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "SemiFinished", Text = "SemiFinished" },
            new SelectListItem { Value =  "RawMaterial", Text = "RawMaterial" }
        };
        public List<SelectListItem> productionType { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "OrderBasedProduction", Text = "OrderBasedProduction" },
            new SelectListItem { Value =  "BatchBasedProduction", Text = "BatchBasedProduction" },
            new SelectListItem { Value =  "ContinousProduction", Text = "ContinousProduction" },
            new SelectListItem { Value =  "F_And_BProduction", Text = "F_And_BProduction" }
        };
        public List<SelectListItem> ConsiderVariations { get; } = new List<SelectListItem>
        {

             new SelectListItem { Value = "False", Text = "False", },
             new SelectListItem { Value =  "True", Text = "True" },
        };
        #endregion

        #region Fixed asset
        public decimal fixed_MinimumFixedAssetValue { get; set; }
        public string fixed_DepreciationDateRange { get; set; }

        public string fixedassetType { get; set; }

        public List<SelectListItem> daterang { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "periodrange", Text = "periodrange" },
            new SelectListItem { Value = "constantmonth_30", Text = "constantmonth_30" },
            new SelectListItem { Value = "constantquarter_90", Text = "constantquarter_90" },
            new SelectListItem { Value = "constantyear_365", Text = "constantyear_365" },

        };
        #endregion

        #region HRMS Setting
        public string hrms_EarlyIn { get; set; }
        public string hrms_LateIn { get; set; }
        public string hrms_Absent { get; set; }
        public string hrms_EarlyOut { get; set; }
        public string hrms_LateOut { get; set; }
        public string hrms_MinTimeBetweenLogs { get; set; }

        public string hrms_maxLates { get; set; }

        public string hrms_PayrollPeriodOffset { get; set; }

        [BrowsableAttribute(true)]
        public string hrms_AttendanceSyncPath { get; set; }

        public string hrms_SyncInOrganization { get; set; }

        public string hrms_AttendanceAnalysisDuringSync { get; set; }

        public string hrms_MachineReadingTime { get; set; }

        public string hrms_AttendanceSynctype { get; set; }

        public string hrms_Attendancepayrollapplication { get; set; }
        public enPayrollDates hrms_PayrollDates { get; set; }
        public ValidateLeaveAndServiceDate hrms_ValidateLeaveAndServiceDate { get; set; }
        public ValidateLeaveAndServiceDate hrms_LatesIncludeEarlyCheckout { get; set; }

        public string hrmsPropertySetting { get; set; }
        public string hrms_attendanceAbsentRules { get; set; }


        public List<SelectListItem> attendanceAbsentRule { get; } = new List<SelectListItem>
        {

            new SelectListItem { Value = "OnlyInTime", Text = "OnlyInTime" },
            new SelectListItem { Value =  "OnlyOutTime", Text = "OnlyOutTime" },
            new SelectListItem { Value =  "InTimeOrOutTime", Text = "InTimeOrOutTime" },
            new SelectListItem { Value =  "InTimeAndOutTime", Text = "InTimeAndOutTime" }

        };

        public List<SelectListItem> machinreadingtype { get; } = new List<SelectListItem>
        {

            new SelectListItem { Value = "60", Text = "One_Min" },
            new SelectListItem { Value =  "120", Text = "Two_Min" },
            new SelectListItem { Value =  "300", Text = "Five_Min" },
            new SelectListItem { Value =  "600", Text = "Ten_Min" },
            new SelectListItem { Value =  "900", Text = "Fifteen_Min" },
            new SelectListItem { Value =  "1800", Text = "Thirty_Min" },
            new SelectListItem { Value =  "3600", Text = "One_Hour" },
            new SelectListItem { Value =  "7200", Text = "Two_Hour" },
            new SelectListItem { Value =  "10800", Text = "Three_Hour" },
            new SelectListItem { Value =  "14400", Text = "Four_Hour" },
            new SelectListItem { Value =  "18000", Text = "Five_Hour" },
            new SelectListItem { Value =  "21600", Text = "Six_Hour" },
            new SelectListItem { Value =  "25200", Text = "Seven_Hour" },
            new SelectListItem { Value =  "28800", Text = "Eight_Hour" },
            new SelectListItem { Value =  "32400", Text = "Nine_Hour" },
            new SelectListItem { Value =  "36000", Text = "Ten_Hour" },
            new SelectListItem { Value =  "39600", Text = "Eleven_Hour" },
            new SelectListItem { Value =  "43200", Text = "Twelve_Hour" },
            new SelectListItem { Value =  "86400", Text = "TwentyFour_Hour" }

        };

        public List<SelectListItem> attendancesycntype { get; } = new List<SelectListItem>
        {

            new SelectListItem { Value = "File", Text = "File" },
            new SelectListItem { Value =  "Cloud", Text = "Cloud" },
            new SelectListItem { Value =  "Not_Applicable", Text = "Not_Applicable" },

        };
        public List<SelectListItem> attendanceapp { get; } = new List<SelectListItem>
        {

            new SelectListItem { Value = "Maximum_Late", Text = "Maximum_Late" },
            new SelectListItem { Value =  "Actual_Hours", Text = "Actual_Hours" },


        };

        #endregion

        #region Organaization unit
        public string Org_unitcode { get; set; }
        public string Org_unit { get; set; }
        public string Org_remark { get; set; }
        #endregion

        #region  Property Management System
        public string pms_MessageCheckFrequency { get; set; }
        public string pms_SummaryVoucher { get; set; }
        public string pms_EnforceClosing { get; set; }
        public string pms_ClosingPeriod { get; set; }
        public string pms_ClosingFrequency { get; set; }
        public string pms_GenerateReportForNullData { get; set; }
        public string pms_EnableCloudReporting { get; set; }
        public string pms_CheckInTime { get; set; }
        public string pms_CheckOutTime { get; set; }
        public string pms_autoprint { get; set; }
        public string pms_UndoCheckinTime { get; set; }
        public string pms_MinimumRateAdujstment { get; set; }
        public string pms_MattressAmount { get; set; }
        public string pms_repoArchivePath { get; set; }
        public string pms_UseLateCheckout { get; set; }
        public string pms_EnforceCardReturn { get; set; }
        public string pms_LostFeeArticle { get; set; }
        public string pms_LateCheckoutime { get; set; }
        public string pms_AdditionalPaymentperhour { get; set; }
        [BrowsableAttribute(true)]
        public string pms_LabelDesignFile { get; set; }
        public string pms_LabelPrinter { get; set; }
        public string pms_EnableJournalize { get; set; }
        public string pms_PostineRoutine { get; set; }
        public string pms_DefaultFiscalBillType { get; set; }
        public string pms_CustomerHighBalance { get; set; }
        public string pms_ValidateExternalReference { get; set; }
        public string pms_ChargeAtCheckin { get; set; }
        public string pms_EarlyCheckIn { get; set; }
        public string pms_RequiredPayment { get; set; }
        public string pms_NightAuditTime { get; set; }

        public string PmsPropertysetting { get; set; }

        public List<SelectListItem> defaultfiscalbill { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "Summary", Text = "Summary" },
            new SelectListItem { Value = "Summary Edit", Text = "Summary Edit" },
            new SelectListItem { Value = "Long Detail", Text = "Long Detail" },

        };
        public List<SelectListItem> messagecheckfrequ { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value =  "1", Text = "One_Sec" },
            new SelectListItem { Value = "10", Text = "Ten_Sec" },
            new SelectListItem { Value = "15", Text = "Fifteen_Sec" },
            new SelectListItem { Value = "30", Text = "Thirty_Sec" },
            new SelectListItem { Value = "60", Text = "One_Min" },
            new SelectListItem { Value = "120", Text = "Two_Min" },
            new SelectListItem { Value = "300", Text = "Five_Min" },
            new SelectListItem { Value = "600", Text = "Ten_Min" },
            new SelectListItem { Value = "900", Text = "Fifteen_Min" },
            new SelectListItem { Value = "1800", Text = "Thirty_Min" },
            new SelectListItem { Value = "3600", Text = "One_Hour" },
            new SelectListItem { Value = "7200", Text = "Two_Hour" },
            new SelectListItem { Value = "10800", Text = "Three_Hour" },
            new SelectListItem { Value = "14400", Text = "Four_Hour" },
            new SelectListItem { Value = "18000", Text = "Five_Hour" },
            new SelectListItem { Value = "21600", Text = "Six_Hour" },
            new SelectListItem { Value = "25200", Text = "Seven_Hour" },
            new SelectListItem { Value = "28800", Text = "Eight_Hour" },
            new SelectListItem { Value = "32400", Text = "Nine_Hour" },
            new SelectListItem { Value = "36000", Text = "Ten_Hour" },
            new SelectListItem { Value = "39600", Text = "Eleven_Hour" },
            new SelectListItem { Value = "43200", Text = "Twelve_Hour" },
            new SelectListItem { Value = "46400", Text = "TwentyFour_Hour" },


        };
        #endregion

        #region ECommerce Setting
        public string defaultOrganizationUnitDef { get; set; }
        public string seat_Availability { get; set; }
        public string ecommerceOrderPrint { get; set; }
        public string controlStock { get; set; }
        public string showStockBelow { get; set; }
        public string useServiceCharge { get; set; }
        public string takeAwayBoxArticle { get; set; }
        public string usePriceType { get; set; }
        public string receiveorderduringoffworkinghours { get; set; }
        public string receiveOrderOnClosingHours { get; set; }
        public string organizationUnitSelection { get; set; }
        public string ecommerceSelection { get; set; }
        public string ecommerce_Maximum_Seat { get; set; }
        public string ecommerce_2nd_InvestmentMax_Age { get; set; }
        public string ecommerce_4thd_InvestmentMax_Age { get; set; }
        public string ecommerce_2nd_InvestmentMax_Percent { get; set; }
        public string ecommerce_3rd_InvestmentMax_Percent { get; set; }
        public string ecommerce_3rd_InvestmentMax_Range { get; set; }
        public string ecommerce_2nd_InvestmentMax_Range { get; set; }

        public string ecommerce_3nd_InvestmentMax_Age { get; set; }
        public string ecommerce_4th_Investment_percent { get; set; }
        public string ecommerce_4th_Investment_Range { get; set; }
        public string ecommerce_5th_InvestmentMax_Age { get; set; }
        public string ecommerce_5th_Investment_percent { get; set; }
        public string ecommerce_5th_Investment_Range { get; set; }
        public string ecommerce_6th_InvestmentMax_Age { get; set; }
        public string ecommerce_6th_InvestmentMax_percent { get; set; }
        public string ecommerce_6th_InvestmentMax_Range { get; set; }
        public string ecommerce_7th_InvestmentMax_Age { get; set; }
        public string ecommerce_7th_InvestmentMax_Percent { get; set; }
        public string ecommerce_7th_InvestmentMax_Range { get; set; }
        public string ecommerce_8th_InvestmentMax_age { get; set; }
        public string ecommerce_8th_InvestmentMax_Percent { get; set; }
        public string ecommerce_8th_InvestmentMax_Range { get; set; }
        public string ecommerce_Founder { get; set; }
        public string ecommerce_maxstock_quantity { get; set; }
        public string ecommerce_Minimstock_Quality { get; set; }
        public string ecommerce_Noof_Employee { get; set; }
        public string ecommerce_Charge_Article { get; set; }
        public string ecommerce_StockArticle { get; set; }
        public string ecommerce_TermandCondition_URL { get; set; }
        public string ecommerce_Yearof_Establishment { get; set; }

        public List<SelectListItem> ecommerceOrderPrinttype { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "False", Text = "False" },
            new SelectListItem { Value =  "True", Text = "True" },
        };
        public List<SelectListItem> seat_Availabilitys { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "Blocked_Seat", Text = "Blocked_Seat" },
            new SelectListItem { Value =  "Available_Seat", Text = "Available_Seat" },
            new SelectListItem { Value =  "NotApplicable", Text = "NotApplicable" },
        };
        public List<SelectListItem> organizationUnitSelectionType { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "Nearest", Text = "Nearest" },
            new SelectListItem { Value =  "Default", Text = "Default" },
        };

        #endregion
    }
    public enum ValidateLeaveAndServiceDate
    {
        False,
        True,
    }
    public enum enPayrollDates
    {
        PeriodRange,
        ThirtyDays

    }
}

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
using Microsoft.CodeAnalysis;
using System.Security.Cryptography;
using System.Text;
using CNET_ERP_V7.Models.Security;
using CNET_V7_Domain.Domain.CommonSchema;

namespace CNET_ERP_V7.Controllers
{
    public class SystemParametersController : Controller
    {

        private AuthenticationManager _authenticationManager;
        private readonly HttpClient _httpClient;
        private readonly SharedHelpers _sharedHelpers;
        public List<ConfigurationDTO> Config = new List<ConfigurationDTO>();
        #region Ctor
        public SystemParametersController(AuthenticationManager authenticationManager,
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
            return View(new systemSettingModel
            {
                sysType = id,

            });

        }
        public async Task<IActionResult> SavingsystemparaSettingProperty(systemSettingModel campnaySetting)

        {
            List<ConfigurationDTO> _configuration = new List<ConfigurationDTO>();
            Config = await  _sharedHelpers.GetConfigurationByRefandPref("Company", CNETConstants.COMPANY_COMPONENT);
            var delete = _sharedHelpers.DeleteConfigurationByReferenceAndPreference("Company", CNETConstants.COMPANY_COMPONENT);
            ConfigurationDTO config = new ConfigurationDTO();

             config = await SaveproppertyData(campnaySetting._MAPURL, "MapURL");
            _configuration.Add(config);
            config = await SaveproppertyData(campnaySetting._ClockServer, "Clock Server");
            _configuration.Add(config);
            config = await SaveproppertyData(campnaySetting._NoOfInstances, "No Of Instances");
            _configuration.Add(config);
            config = await SaveproppertyData(campnaySetting._NoOfNestedWindows, "No OfNested Windows");
            _configuration.Add(config);
            config = await SaveproppertyData(campnaySetting._SaveAttachment, "Save Attachment");
            _configuration.Add(config);
            config = await SaveproppertyData(campnaySetting._EnableMAP.ToString(), "Enable MAP");
            _configuration.Add(config);
            config = await SaveproppertyData(campnaySetting._SyncType, "Sync Type");
            _configuration.Add(config);
            config = await SaveproppertyData(campnaySetting._ThirdPartySyncMethod, "Third Party Sync Method");
            _configuration.Add(config);
            config = await SaveproppertyData(campnaySetting._LocalRootPath3rdPartyInterfacing, "Local Root Path 3rd Party Interfacing");
            _configuration.Add(config);
            config =  await SaveproppertyData(campnaySetting._EnableThirdPartyInterfacing.ToString(), "Enable ThirdParty Interfacing");
            _configuration.Add(config);
            config = await SaveproppertyData(campnaySetting._ErrorLogPath, "Error Log Path");
            _configuration.Add(config);
            config = await SaveproppertyData(campnaySetting._DatabaseBackupPath, "Database Backup Path");
            _configuration.Add(config);
            config = await SaveproppertyData(campnaySetting._InventoryRoundQuantity, "Inventory Round Quantity");
            _configuration.Add(config);
            config =  await SaveproppertyData(campnaySetting._AllowMultipleInstance.ToString(), "Allow Multiple Instance");
            _configuration.Add(config);
            config =  await SaveproppertyData(campnaySetting._PhotoMandatory.ToString(), "Photo Mandatory");
            _configuration.Add(config);
            config = await SaveproppertyData(campnaySetting._DefaultDiscountAccount, "Default Discount Account");
            _configuration.Add(config);
            config =  await SaveproppertyData(campnaySetting._DuplicateCardIssue.ToString(), "Duplicate Card Issue");
            _configuration.Add(config);
            config =  await SaveproppertyData(campnaySetting._PhoneNumberMandatory.ToString(), "Phone Number Mandatory");
            _configuration.Add(config);
            config =  await SaveproppertyData(campnaySetting._ExportCashAsCredit.ToString(), "Export Cash As Credit");
            _configuration.Add(config);
            config = await SaveproppertyData(campnaySetting._ExpiryDateNotificationInDate, "Expiry Date Notification In Date");
            _configuration.Add(config);
            config = await SaveproppertyData(campnaySetting._CustomReportLibraryUrl, "Custom Report Library URL");
            _configuration.Add(config);
            config = await SaveproppertyData(campnaySetting._XMLRequanselationPath, "XML Reconciliation Path");
            _configuration.Add(config);
            config = await SaveproppertyData(campnaySetting._XMLDampYardPath, "XML DampYard Path");
            _configuration.Add(config);
            config = await SaveproppertyData(campnaySetting._AlertBefore, "Access Control Subscription Alert Before");
            _configuration.Add(config);


            await _sharedHelpers.saveOrUpdateRange(_configuration);
            return Json("Saved Successfully");

        }
        public async Task<ConfigurationDTO> SaveproppertyData(string CurrentV, string ConfigA)

        {
            ConfigurationDTO configuration = new ConfigurationDTO();
            List<ConfigurationDTO> listcConfigurations = new List<ConfigurationDTO>();
            ConfigurationDTO Prev = Config.Where(x => x.Attribute?.Trim() == ConfigA?.Trim())?.FirstOrDefault();

            configuration.Pointer = CNETConstants.COMPANY_COMPONENT;
            configuration.Reference = "Company";
            configuration.Attribute = ConfigA;
            configuration.CurrentValue = CurrentV == null ? "" : CurrentV;
            if (Prev != null)
            {
                configuration.PreviousValue = Prev.CurrentValue;
            }
            else
            {
                configuration.PreviousValue = CurrentV == null ? "" : CurrentV;
            }
           
            return configuration;
        }

        public async Task<IActionResult> Createcompantworkinghour(systemSettingModel createworkhur)
        {
            bool flag = false;
            var created = "";
            if (createworkhur.workinghourorgunitDefn == null || createworkhur?.workinghourorgunitDefn == 0)
            {
                flag = false;
                created = "Please Selecte Branch";
            }
            else
            if (createworkhur.schedule_from_date == null || createworkhur.schedule_to_date == null)
            {
                flag = false;
                created = "Please Enter Start and End Date";
            }
            else
            {
                bool update = false;

                try
                {
                    ScheduleHeaderDTO CompanySchedule = new ScheduleHeaderDTO
                    {
                        Description = createworkhur.schedule_description == null ? "Working Hour" : createworkhur.schedule_description,
                        Cateogry = createworkhur.schedule_typecode == null ? 842 : createworkhur.schedule_typecode,
                        Type = 1839,
                        StartDate = createworkhur.schedule_from_date,
                        EndDate = createworkhur.schedule_to_date,
                        IsActive = true,
                        Remark = null
                    };

                    if (createworkhur.scheduleheadercode <= 0)
                    {
                        var id = await  _sharedHelpers.CreateScheduleheader(CompanySchedule);

                        createworkhur.scheduleheadercode = id.Id;
                    }
                    else
                    {
                        update = true;
                        CompanySchedule.Id = createworkhur.scheduleheadercode;
                        var updateschduleheader = await _sharedHelpers.UpdateScheduleheader(CompanySchedule);
                    }
                    if (createworkhur.scheduleheadercode > 0)
                    {
                        if (createworkhur.daymonth.Count() > 0)
                        {
                            for (int item = 0; item < createworkhur.daymonth.Count(); item++)
                            {
                                ScheduleDetailDTO CompanyScheduledetail = new ScheduleDetailDTO
                                {
                                    Seheduleheader = createworkhur.scheduleheadercode,
                                    DayMonth = createworkhur.daymonth[item],
                                    StartTime = createworkhur.startdaet[item].ToString("hh:mm:ss tt"),
                                    EndTime = createworkhur.enddate[item].ToString("hh:mm:ss tt"),
                                    DayOfMonth = 1,
                                    Remark = ""
                                };
                                if (createworkhur.detailcodee[item] <=0)
                                {
                                    try
                                    {
                                        var createschdule = await _sharedHelpers.CreateScheduledetail(CompanyScheduledetail);
                                    }
                                    catch (Exception)
                                    {

                                        throw;
                                    }
                                   
                                }
                                else
                                {
                                    CompanyScheduledetail.Id = Convert.ToInt32(createworkhur.detailcodee[item]);
                                    try
                                    {
                                        var updaetschdule = await _sharedHelpers.UpdateScheduledetail(CompanyScheduledetail);
                                    }
                                    catch (Exception)
                                    {

                                        throw;
                                    }
                                  
                                }
                            }
                        }

                        if (!update)
                        {
                            ScheduleDTO CompanySchedules = new ScheduleDTO
                            {
                                Description = createworkhur.schedule_description,
                                Pointer = 4,
                                Reference = createworkhur.workinghourorgunitDefn,
                                ScheduledHeader = createworkhur.scheduleheadercode,
                                Remark = null
                            };
                            try
                            {
                                await _sharedHelpers.CreateSchedule(CompanySchedules);

                            }
                            catch (Exception)
                            {

                                throw;
                            }
                          
                        }
                        flag = true;
                        created = "Saved Successfully";
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }

            }
            return Json(new { created = created, notcreated = flag });
        }

        public async Task<IActionResult> GetCompanyworkingHour(int id)
        {
            var modell = new systemSettingModel() { consineeCode = id };
            return PartialView("_workingHoursdetail", modell);
        }
    }
}

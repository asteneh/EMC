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
using CNET_V7_Entities.DataModels;
using System.Collections.Immutable;
using CNET_V7_Domain.Domain.ViewSchema;
using CNET_V7_Domain.Domain.SecuritySchema;
using CNET_V7_Domain.Domain.CommonSchema;
using CNET_ERP_V7.Models.SelectorModel;
using Cnetv7BufferHolder;
using CNET_V7_Domain.Misc;
using CNET_V7_Entities.CustomReturnTypes;
using System.Data;
using System.Globalization;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Rewrite;

namespace CNET_ERP_V7.Controllers
{
    public class SecurityController : Controller
    {
        private AuthenticationManager _authenticationManager;
        private readonly HttpClient _httpClient;
        private readonly SharedHelpers _sharedHelpers;
        public List<ConfigurationDTO> Config = new List<ConfigurationDTO>();
        public List<Configuration> Configdev = new List<Configuration>();
        private readonly ILogger<HomeController> _logger;
        private List<UserRoleMapper> listOfUserRoleMappers;

        public static List<DocumentAccess> DocumentBrowserData { get; set; }
        public static List<DocumentAccess> VoucherDocumentBrowserDataList { get; set; }
        public static List<DocumentAccess> ArticleDocumentBrowserData { get; set; }
        public static List<DocumentAccess> PersonOrganizationDocumentBrowserData { get; set; }
        public static List<string> SubsystemDashboardDataList { get; set; }
        #region Ctor
        public SecurityController(AuthenticationManager authenticationManager,
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
            return View( new SecurityModel
            {
                //sysType = id,

            });

        }
        public async Task<IActionResult> SavingSecuritySettingProperty(SecurityModel securityModel)
        {
            List<ConfigurationDTO> _configuration = new List<ConfigurationDTO>();
            Config = await _sharedHelpers.GetConfigurationByRefandPref("Security", CNETConstants.SECURITY_COMPONENT);
            var delete = _sharedHelpers.DeleteConfigurationByReferenceAndPreference("Security", CNETConstants.SECURITY_COMPONENT);
            ConfigurationDTO config = new ConfigurationDTO();

            config = await SaveData("Enforce Complex Password", securityModel.secu_EnforceComplexPassword);
            _configuration.Add(config);
            config =  await SaveData("Enforce Password History", securityModel.secu_EnforcePasswordHistory);
            _configuration.Add(config);
            config =  await SaveData("Maximum Password Age", securityModel.secu_MaximumPasswordAge);
            _configuration.Add(config);
            config =  await SaveData("Minimum Password Length", securityModel.secu_MinimumPasswordLength);
            _configuration.Add(config);
            config = await SaveData("Trial Before Lock", securityModel.secu_TrialBeforeLock);
            _configuration.Add(config);
            config =  await SaveData("Lock Time", securityModel.secu_LockTime);
            _configuration.Add(config);
            config =  await SaveData("Password Expiry Notice", securityModel.secu_PasswordExpiryNotice);
            _configuration.Add(config);
            config = await SaveData("Use Screen Lock", securityModel.secu_UseScreenLock);
            _configuration.Add(config);
            config =  await SaveData("Lock Short Key", securityModel.secu_LockShortKey);
            _configuration.Add(config);
            config =  await SaveData("User Name Display Style", securityModel.secu_UserNameDisplayStyle);
            _configuration.Add(config);
            config =  await SaveData("Populate User Name", securityModel.secu_PopulateUserName);
            _configuration.Add(config);
            config =  await SaveData("Log Only Attended Users", securityModel.secu_LogOnlyAttendedUsers);
            _configuration.Add(config);
            config =  await SaveData("Change Password At First Login", securityModel.secu_ChangePasswordAtFirstLogin);
            _configuration.Add(config);
            config =  await SaveData("Timing Before Screen lock", securityModel.secu_TimingBeforeScreenlock);
            _configuration.Add(config);

            await _sharedHelpers.saveOrUpdateRange(_configuration);
            return Json("Save Successfully");
        }

        public async Task<ConfigurationDTO> SaveData(string arttb, string currnt)
        {
            var atrributt = arttb;
            var curentval = currnt;

            ConfigurationDTO configuration = new ConfigurationDTO();
            List<ConfigurationDTO> listcConfigurations = new List<ConfigurationDTO>();
            ConfigurationDTO Prev = Config.Where(x => x.Attribute?.Trim() == arttb?.Trim())?.FirstOrDefault();
            configuration.Pointer  =CNETConstants.SECURITY_COMPONENT;
            configuration.Reference = "Security";
            configuration.Attribute = arttb;
            configuration.CurrentValue = currnt == null ? "" : currnt;
            if (Prev != null)
            {
                configuration.PreviousValue = Prev.PreviousValue;
            }
            else
            {
                configuration.PreviousValue = currnt == null ? "" : currnt;
            }
            return configuration;
        }

        public async Task<IActionResult> getUseraccount(int employee)
        {
            var cbeStatus = 0;
            int? usercode = 0;
            var lastname = "";
            string firstName = "";
            string password = "";
            var Mname = "";
            var checkenable = false;
            var userInfoFromDbase = await _sharedHelpers.GetUserByPerson(employee);
            var personviewlist = await _sharedHelpers.GetVoucherConsigneeById(employee);
         
            if (userInfoFromDbase != null && userInfoFromDbase.Count() > 0)
            {
                usercode = userInfoFromDbase?.FirstOrDefault()?.Id;
                Mname = userInfoFromDbase?.FirstOrDefault()?.UserName;
                password = userInfoFromDbase?.FirstOrDefault()?.Password;
                checkenable = true;
                cbeStatus = 9;
            }
            else if (personviewlist != null)
            {
                lastname = personviewlist?.FirstOrDefault()?.SecondName;
                firstName = personviewlist?.FirstOrDefault()?.FirstName;
                Mname = (lastname?.Length > 1) ? firstName + lastname?.Substring(0, 1) : firstName;
                checkenable = true;
                cbeStatus = 3;
                password = "";
                usercode = 0;
            }
            return Json(new { increament = checkenable, result = Mname, loggedIn = cbeStatus, ucode = usercode, password = password });
        }
        public async Task<IActionResult> Getreportprivilege()
        {
            List<SystemConstantDTO> subChild = new List<SystemConstantDTO>();
            List<ReportAccess> NestedChild = new List<ReportAccess>();
            List<ReportDTO> reportType = new List<ReportDTO>();
            var _allsys = GeneralBufferHolder.SystemConstants;
            var reportList = await _sharedHelpers.GetreportList();

            var mainreport = _allsys.Where(x => x.ParentId == 631 && x.IsActive == true).ToList();
            foreach (var item in mainreport)
            {
                var subreport = _allsys.Where(x => x.ParentId == item.Id && x.IsActive == true).ToList();
                subChild.AddRange(subreport);
            }
            
            foreach (var item in subChild)
            {
                var subreport = reportList.Where(x => x.Subsystem == item.Id).GroupBy(x => x.Reference).Select(y => y.First()).ToList();
                reportType.AddRange(subreport);
            }
            foreach (var item in reportType)
            {
                var subreport = _allsys.Where(x => x.IsActive == true && x.Id == Convert.ToInt32(item.Reference))?.FirstOrDefault();
                NestedChild.Add(new ReportAccess {reptCode = subreport?.Id,reptParent = item.Subsystem,reptDescription = subreport?.Description });
            }
            var reportprivlilegeList = new SecurityModel() { mainReportType = mainreport, SubReportType = subChild, report = NestedChild };

            return PartialView("_ReportPrivilageList", reportprivlilegeList);

        }
        public async Task<IActionResult> Getaccessprivilege()
        {
            var functionalities = await _sharedHelpers.SelectAllFunctionality();


            List<SystemConstantDTO> cNETComponents = new List<SystemConstantDTO>();
          

            List<SystemConstantDTO> cNETComponentchild = new List<SystemConstantDTO>();
          

            List<FunctionalityDTO> functiona = new List<FunctionalityDTO>();
          
            var Ccompnent = GeneralBufferHolder.SystemConstants;

             List<int> function = functionalities.Select(x => x.Function).Distinct().ToList();
            var functionalitiesList = functionalities?.GroupBy(x => x.Category).Select(x => x.First());

            var subSystemComp = functionalities?.GroupBy(x => x.SubSystemComponent).Select(x => x.First());


            var cNETComponencListtYPE = (from app in function
                                         join cnt in Ccompnent
                                     on app equals cnt.Id
                                         orderby cnt.Description

                                         select new
                                         {
                                             cnt.Description,
                                             cnt.Id,
                                             cnt.ParentId,

                                         }).ToList();

            cNETComponents = cNETComponencListtYPE.Select(x => new SystemConstantDTO
            {
                Id = x.Id,
                Description = x.Description,
                ParentId = 0

            }).ToList();
            cNETComponents = cNETComponents.Where(x => x.Description.ToLower() != "document access privilege").ToList();
            var cNETComponenchlid = (from sub in subSystemComp
                                     join cntt in Ccompnent
                                   on sub.SubSystemComponent equals cntt.Id

                                     select new
                                     {
                                         cntt.Description,
                                         cntt.Id,
                                         sub.Function
                                     }).ToList();


            cNETComponentchild = cNETComponenchlid.Select(x => new SystemConstantDTO
            {
                Id = x.Id,
                Description = x.Description,
                Type = x.Function.ToString(),


            }).ToList();

            functiona = functionalitiesList.Select(x => new FunctionalityDTO
            {
                Id = x.Id,
                Description = Ccompnent.Where(y => y.Id == x.Category)?.FirstOrDefault()?.Description,
                //VisualComponent = x.VisualComponent,
                SubSystemComponent = x.SubSystemComponent,
                Category = x.Category,
                Function = x.Function,


            }).ToList();

            var accessprivlilegeList = new SecurityModel() { subHeader = cNETComponentchild, mainHeader = cNETComponents, functionalities = functiona };

            return PartialView("_AccessPrivilageList", accessprivlilegeList);
            }

        public async Task<IActionResult> Getactivityprivilege()
        {

            List<SystemConstantDTO> cNETComponentList = new List<SystemConstantDTO>();
            List<SystemConstantDTO> cNETComponentgslList = new List<SystemConstantDTO>();
            List<SystemConstantDTO> cNETComponentvoucherList = new List<SystemConstantDTO>();

            List<SystemConstantDTO> cNETComponentchildList = new List<SystemConstantDTO>();
            List<SystemConstantDTO> cNETComponentgslchildList = new List<SystemConstantDTO>();
            List<SystemConstantDTO> cNETComponentchildvoucherList = new List<SystemConstantDTO>();


            var VoucherDefination = GeneralBufferHolder.SystemConstants.Where(x => x.Type == "Transaction" || x.Type == "Sub module").ToList();
            VoucherDefination = VoucherDefination.Where(y => y.IsActive == true).ToList();
            var sbsystem = await _sharedHelpers.GetSytemConstantBytype("Subsystem");
            sbsystem = sbsystem.Where(x => x.IsActive == true).ToList();
            var VoucherComponentList = VoucherDefination.Select(x => x.Type).Distinct().ToList();

            var cnetCompList = GeneralBufferHolder.SystemConstants;

            var GSLTypeList = cnetCompList.Where(x => x.Id == 3 || x.Id == 4).ToList();
            var GSLTypeComponentList = GSLTypeList.Select(x => x.ParentId)?.Distinct().ToList();

            var GSLTypelist = cnetCompList?.Where(x => x.IsActive == true && (x.Type == "GSL Article" || x.Type == "GSL Consignee")).ToList();

            cNETComponentList.AddRange(sbsystem);
            cNETComponentList.AddRange(GSLTypeList);
            cNETComponentchildList.AddRange(VoucherDefination);
            cNETComponentchildList.AddRange(GSLTypelist);
            var activitycNETComponents = new SecurityModel() { activitycNETComponents = cNETComponentList, activitychildcNETComponents = cNETComponentchildList };


            return PartialView("_ActivityPrivilageList", activitycNETComponents);

        }
        public async Task<IActionResult> Getdocumentaccessprivilege()
        {
            List<SystemConstantDTO> cNETComponentList = new List<SystemConstantDTO>();
            List<SystemConstantDTO> cNETComponentgslList = new List<SystemConstantDTO>();
            List<SystemConstantDTO> cNETComponentvoucherList = new List<SystemConstantDTO>();

            List<SystemConstantDTO> cNETComponentchildList = new List<SystemConstantDTO>();
            List<SystemConstantDTO> cNETComponentgslchildList = new List<SystemConstantDTO>();
            List<SystemConstantDTO> cNETComponentchildvoucherList = new List<SystemConstantDTO>();


            var VoucherDefination = GeneralBufferHolder.SystemConstants.Where(x => x.Type == "Transaction" || x.Type == "Sub module").ToList();
            VoucherDefination = VoucherDefination.Where(y => y.IsActive == true).ToList();
            var sbsystem = await _sharedHelpers.GetSytemConstantBytype("Subsystem");
             sbsystem = sbsystem.Where(x => x.IsActive == true).ToList();
            var VoucherComponentList = VoucherDefination.Select(x => x.Type).Distinct().ToList();

            var cnetCompList = GeneralBufferHolder.SystemConstants;

            var GSLTypeList = cnetCompList.Where(x => x.Id == 3 || x.Id == 4).ToList();
            var GSLTypeComponentList = GSLTypeList.Select(x => x.ParentId )?.Distinct().ToList();

            var GSLTypelist = cnetCompList?.Where(x => x.IsActive == true && (x.Type == "GSL Article" || x.Type == "GSL Consignee")).ToList();

            cNETComponentList.AddRange(sbsystem);
            cNETComponentList.AddRange(GSLTypeList);
            cNETComponentchildList.AddRange(VoucherDefination);
            cNETComponentchildList.AddRange(GSLTypelist);

            var activitycNETComponents = new SecurityModel() { documentcNETComponents = cNETComponentList, documentchildcNETComponents = cNETComponentchildList };

            return PartialView("_DocumentSubsystemList", activitycNETComponents);

        }
        public async Task<IActionResult> getAccountResult()
        {
            return PartialView("_CreateAccount");
        }
        public async Task<IActionResult> GetuserDetailBycode(int Code)
        {
            var uservalueview = await _sharedHelpers.GetUserById(Code);

            var userdetl = uservalueview?.FirstOrDefault();

            return Json(new { code = userdetl.Id, username = userdetl.UserName, status = userdetl.LoggedInStatus, iasctive = userdetl.IsActive, remark = userdetl.Remark });
        }

        public async Task<IActionResult> createaccountdetail(SecurityModel createuseacc)
        {
            var value = await _sharedHelpers.GetUserByUserName(createuseacc.acc_usernamevalue);

            //List<ModificationHistory> listofmodHistory = new List<ModificationHistory>();
            var Timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
            var passcomplexity = "";
            bool valueExists = false;

            ActivityDefinitionDTO act = new ActivityDefinitionDTO();

            List<string> results = new List<string>();
            List<string> resultcode = new List<string>();
            List<DeviceDTO> dev = new List<DeviceDTO>();

            ActivityDTO mnewUserActivity = new ActivityDTO();
            var resultset = "";
            bool iscorrect = false;
            var minlength = 0;

            var passhistory = await _sharedHelpers.GetConfigurationByRefandPref("Security", CNETConstants.SECURITY_COMPONENT);
            var passcurrnthistory = passhistory.Where(c => c.Attribute == "Enforce Password History").FirstOrDefault();
            var minimmumpasslength = passhistory.Where(c => c.Attribute == "Minimum Password Length").FirstOrDefault();
            var complexpassword = passhistory.Where(c => c.Attribute == "Enforce Complex Password").FirstOrDefault();

            bool isEnforcecomplexPassword = false;
            bool isEnforcePasswordHistory = false;
            if (passcurrnthistory != null)
                isEnforcePasswordHistory = Convert.ToBoolean(passcurrnthistory.CurrentValue);

            if (complexpassword != null)
                isEnforcecomplexPassword = Convert.ToBoolean(complexpassword.CurrentValue);

            if (minimmumpasslength != null)
                minlength = Convert.ToInt32(minimmumpasslength.CurrentValue);

            if (createuseacc.acc_Employee == 0 || createuseacc.acc_username == null)
            {
                resultset = "Enter all fields";
                iscorrect = false;
            }
            else
            {
                var actDef = await _sharedHelpers.GetActivityDefinitionByDesc("1666");

                UserDTO muser = new UserDTO();
                if (createuseacc.acc_usernamecode != 0)
                {
                    if (value != null)
                    {
                        muser.Id = value.Id;
                    }
                    else
                    {
                        muser.Id = createuseacc.acc_usernamecode;
                    }


                    if (createuseacc.acc_Isactive == true)
                    {
                        muser.IsActive = true;
                    }
                    else
                    {
                        muser.IsActive = false;
                    }
                    muser.Person = createuseacc.acc_Employeeenable = createuseacc.acc_Employeeenable == 0 ? muser.Person : createuseacc.acc_Employeeenable;
                    muser.UserName = createuseacc.acc_usernamevalue = createuseacc.acc_usernamevalue == null ? muser.UserName : createuseacc.acc_usernamevalue;
                    if (value != null)
                        muser.Password = value.Password;
                    muser.LoggedInStatus = Convert.ToInt16(createuseacc.acc_loggedstatus);
                    muser.Remark = createuseacc.acc_remark = createuseacc.acc_remark == null ? "" : createuseacc.acc_remark;
                    byte[] hashBytes = Convert.FromBase64String(value.Password);
                    var updateuse = await _sharedHelpers.UpdateUser(muser);
                    if (updateuse != null)
                    {
                      
                        if (actDef == null)
                        {
                            ActivityDefinitionDTO actt = new ActivityDefinitionDTO();
                            //actt.Pointer = 1;
                            actt.Reference = 26;
                            actt.Index = 0;
                            actt.IssuingEffect = false;
                            actt.IsManual = false;
                            var actdef = await _sharedHelpers.CreateActivityDefinition(actt);
                            mnewUserActivity.ActivityDefinition = actdef.Id;
                        }
                        else
                        {
                            mnewUserActivity.ActivityDefinition = (int)actDef?.FirstOrDefault()?.Id;
                        }

                        var currentArticlee = await _sharedHelpers.SelectArticleByName(Environment.MachineName.ToLower());
                        ArticleDTO currentArticle = currentArticlee?.FirstOrDefault();
                        if (currentArticle != null)
                        {
                            mnewUserActivity.Device = null;
                            List<int> configList = new List<int>() { 1 };
                            UserDTO currentUser = await _authenticationManager.GetAuthenticatedUser();
                            mnewUserActivity.Pointer = 1;
                            mnewUserActivity.Reference = currentUser.Id; // muser.person;
                            mnewUserActivity.TimeStamp = DateTime.UtcNow;
                            mnewUserActivity.User = 0;  //.userName;
                            mnewUserActivity.Year = DateTime.UtcNow.Year;
                            mnewUserActivity.Month = DateTime.UtcNow.Month;
                            //mnewUserActivity.Date = DateTime.UtcNow.Day;
                            var activityCode = await _sharedHelpers.CreateActivity(mnewUserActivity);


                            //resultset = "Saveed Successfully";

                        }
                    }
                    resultset = "Saved Successfully";
                    iscorrect = true;

                }
                else
                {
                    passcomplexity = createuseacc?.passwordcomplexity?.Trim();

                    if (value != null)
                    {
                        muser.Id = value.Id;
                        muser.UserName = createuseacc.acc_username;
                        muser.LoggedInStatus = Convert.ToInt16(createuseacc.acc_loggedstatus); ;
                        muser.Remark = createuseacc.acc_remark;
                        if (createuseacc.acc_Isactive == true)
                        {
                            muser.IsActive = true;
                        }
                        else
                        {
                            muser.IsActive = false;
                        }
                        muser.Password = value.Password;
                        muser.Person = value.Person;
                        muser.Id = value.Id;
                        muser.Salt = value.Salt;
                       
                        var updateuse = await _sharedHelpers.CreateUser(muser);

                        var actDefItem = actDef;

                        if (actDefItem == null)
                        {

                            //act.Pointer = 1;
                            act.Reference = 26;
                            act.Index = 0;
                            act.IssuingEffect = false;
                            act.IsManual = false;
                            var createActDen = await _sharedHelpers.CreateActivityDefinition(act);
                            act.Id = createActDen.Id;


                        }
                        var currentUser = await _authenticationManager.GetAuthenticatedUser();

                        mnewUserActivity.TimeStamp = DateTime.UtcNow;
                        mnewUserActivity.ActivityDefinition = act.Id;
                        mnewUserActivity.Device = 0;
                        mnewUserActivity.Pointer = 1;
                        mnewUserActivity.Reference = currentUser.Id; // muser.person;
                        mnewUserActivity.User = 356;  //.userName;
                        var activityCode = await _sharedHelpers.CreateActivity(mnewUserActivity);
                        resultset = "Saveed Successfully";
                        iscorrect = true;

                    }

                    else
                    {
                        if (createuseacc.acc_password != null)
                        {
                            if (createuseacc.acc_password != createuseacc.acc_comfirmPassword)
                            {
                                resultset = "Password Missmatch";
                                iscorrect = false;
                            }
                            else
                            {
                                if (createuseacc.acc_password.Length < minlength)
                                {
                                    resultset = "Please Check your Setting  Your Minimum Password Length  is  " + minlength;
                                    iscorrect = false;
                                }
                                else if (isEnforcecomplexPassword == true && passcomplexity != "Very Strong")

                                {
                                    resultset = "Please Check your Setting  Your Password Is " + passcomplexity;
                                    iscorrect = false;

                                }
                                else
                                {
                                    muser.UserName = createuseacc.acc_username;
                                    muser.Person = createuseacc.acc_Employee;
                                    muser.LoggedInStatus = createuseacc.acc_loggedstatus = createuseacc.acc_loggedstatus == 0 ? 9 : createuseacc.acc_loggedstatus;
                                    muser.Remark = createuseacc.acc_remark = createuseacc.acc_remark == null ? "" : createuseacc.acc_remark;
                                    muser.IsActive = createuseacc.acc_Isactive;
                                    muser.Password = createuseacc.acc_password;
                                    muser.Salt = "";


                                    //if (createuseacc.acc_Employee != 0)
                                    //    if (isEnforcePasswordHistory)
                                    //    {
                                    //        var listofPasswordformModificationHistory = await _sharedHelpers.GetModificationHistory_VIEW(muser.code, "USER_PASSWORD");


                                    //        foreach (var oldpass in listofPasswordformModificationHistory)
                                    //        {

                                    //            if (oldpass.newValue == muser.pas)
                                    //            {
                                    //                valueExists = true;
                                    //                break;
                                    //            }
                                    //        }

                                    //        if (valueExists)
                                    //        {
                                    //            resultset = "Previously Used Password !";
                                    //            iscorrect = false;
                                    //        }
                                    //    }

                                    var cretaeuser = await _sharedHelpers.CreateUser(muser);
                                    muser.Id = cretaeuser.Id;

                                    var currentUser = await _authenticationManager.GetAuthenticatedUser();
                                    var currentArticle = await _sharedHelpers.SelectArticleByName(Environment.MachineName.ToLower());

                                    if (currentArticle != null)
                                    {
                                        dev = await _sharedHelpers.SelectDevice();
                                    }
                                    var devicecode = dev?.Where(x => x?.Article == currentArticle?.FirstOrDefault()?.Id).FirstOrDefault()?.Id;
                                    var orgunit = await _sharedHelpers.GetConsigneeunit();
                                    var orgunitdef = orgunit?.Where(x => x.Consignee == devicecode)?.FirstOrDefault();
                                    mnewUserActivity.Id = 0;
                                    mnewUserActivity.Reference = currentUser.Id; // muser.person;
                                    mnewUserActivity.ActivityDefinition = act.Id;
                                    mnewUserActivity.TimeStamp = DateTime.UtcNow;
                                    if (orgunitdef != null)
                                        mnewUserActivity.ConsigneeUnit = orgunitdef.Consignee;
                                    mnewUserActivity.Device = devicecode;
                                    mnewUserActivity.User = muser.Id;
                                    mnewUserActivity.Pointer = 1;
                                    mnewUserActivity.Year = DateTime.UtcNow.Year;
                                    mnewUserActivity.Month = DateTime.UtcNow.Month;
                                    //mnewUserActivity.Date = DateTime.UtcNow.Day;
                                    mnewUserActivity.Remark = "";

                                    var createactivity = await _sharedHelpers.CreateActivity(mnewUserActivity);

                                    resultset = "Saved Successfullyy";
                                    iscorrect = true;
                                    //}
                                }


                            }
                        }
                        else
                        {
                            resultset = "Enter all fields";
                            iscorrect = false;
                        }
                    }
                }
            }
            return Json(new { result = resultset, correct = iscorrect });

        }
        public async Task<IActionResult> ResetPassword(int CODE)
        {
            List<string> resetpass = new List<string>();
            List<DeviceDTO> devv = new List<DeviceDTO>();

            ActivityDefinitionDTO act = new ActivityDefinitionDTO();
            var value = await _sharedHelpers.GetUserById(CODE);

            if (value != null)
            {
                ActivityDTO mnewUserActivity = new ActivityDTO();

                UserDTO muser = new UserDTO();
                muser.Person = Convert.ToInt32(value?.FirstOrDefault()?.Person);
                muser.Remark = "";
                muser.IsActive = true;
                muser.LoggedInStatus = Convert.ToInt16("1");
                muser.Password = Encrypt(Convert.ToString("123456"));


                muser.Id = Convert.ToInt32(value?.FirstOrDefault()?.Id);
                muser.UserName = value?.FirstOrDefault()?.UserName;
                var updateuser = await _sharedHelpers.UpdateUser(muser);

                var actDef = await _sharedHelpers.GetActivityDefinitionByDesc("1666");
                if (actDef == null)
                {

                    //act.Pointer = 1;
                    act.Reference = 26;
                    act.Index = 0;
                    act.IssuingEffect = false;
                    act.IsManual = false;
                    var createActDenn = await _sharedHelpers.CreateActivityDefinition(act);
                    act.Id = createActDenn.Id;

                    var currentArticle = await _sharedHelpers.SelectArticleByName(Environment.MachineName.ToLower());

                    if (currentArticle != null)
                    {
                        devv = await _sharedHelpers.SelectDevice();
                    }
                    var devicecode = devv?.Where(x => x.Article == currentArticle?.FirstOrDefault()?.Id).FirstOrDefault().Id;


                    var currentUser = await _authenticationManager.GetAuthenticatedUser();

                    mnewUserActivity.Id = 0;

                    mnewUserActivity.TimeStamp = DateTime.UtcNow;
                    var orgunit = await _sharedHelpers.GetConsigneeunit();
                    var orgunitdef = orgunit?.Where(x => x.Consignee == devicecode).FirstOrDefault();

                    if (currentArticle != null)
                    {
                        mnewUserActivity.Id = 0;
                        mnewUserActivity.Reference = currentUser.Id; // muser.person;
                        if (act.Id != 0)
                        {
                            mnewUserActivity.ActivityDefinition = act.Id;
                        }
                        else
                        {
                            mnewUserActivity.ActivityDefinition = (int)actDef?.FirstOrDefault()?.Id;
                        }
                        mnewUserActivity.TimeStamp = DateTime.UtcNow;
                        if (orgunitdef != null)
                            mnewUserActivity.ConsigneeUnit = orgunitdef.Consignee;
                        mnewUserActivity.Device = devicecode;
                        mnewUserActivity.User = muser.Id;
                        mnewUserActivity.Pointer = 1;
                        mnewUserActivity.Year = DateTime.UtcNow.Year;
                        mnewUserActivity.Month = DateTime.UtcNow.Month;
                        //mnewUserActivity.Date = DateTime.UtcNow.Day;
                        mnewUserActivity.Remark = "";
                        var createactivity = await _sharedHelpers.CreateActivity(mnewUserActivity);

                    }
                }
                return Json("Saved Succesfully");
            }
            return Json("Saved Succesfully");
        }

        public async Task<IActionResult> GetPasswordChangeActivity()
        {
            var Deviceart = await _sharedHelpers.GetArticlesByGSL(15);
            var currentUser = await _authenticationManager.GetAuthenticatedUser();
            var actDef = await _sharedHelpers.ActivityDefnBufferList();
            var Actdefchange = actDef?.Where(x => x.Description == 1663).FirstOrDefault();


            List<ActivitydTO> activitydtolist = new List<ActivitydTO>();
            ActivitydTO activitydto = new ActivitydTO();
            var activities = await _sharedHelpers.GetActivityByref(currentUser.Person);

            if (activities != null && Actdefchange != null)
            {
                activities = activities.Where(x => x.ActivityDefinition == Actdefchange.Id).ToList();
                activities = activities.OrderByDescending(x => x.Id).Take(20).ToList();
            }
            
            if (activities != null && activities.Count() > 0)
            {
                foreach (var act in activities)
                {
                    activitydto = new ActivitydTO();
                    activitydto.code = act.Id.ToString();
                    activitydto.reference = act.Reference.ToString();
                    activitydto.date = act.TimeStamp.ToString();//, act.startTimStamp.ToString("dd-mm-yyyy hh:mm:ss");

                    if (!string.IsNullOrEmpty(act.Device.ToString()))
                    {
                        List<DeviceDTO> de = await _sharedHelpers.SelectDeviceBYId(act.Device);
                        if (de != null)
                        {
                            ArticleDTO devicename = Deviceart.FirstOrDefault(x => x.Id == de?.FirstOrDefault()?.Article);
                            if (devicename != null)
                            {
                                activitydto.devicename = devicename.Name;
                            }
                        }

                    }
                    if (!string.IsNullOrEmpty(act.ConsigneeUnit.ToString()))
                    {
                        List<ConsigneeUnitDTO> orgdef = await _sharedHelpers.GetConsigneeunitById(Convert.ToInt32(act.ConsigneeUnit));
                        if (orgdef != null)
                        {
                            activitydto.orgdef = orgdef?.FirstOrDefault().Name;
                        }
                    }
                    if (!string.IsNullOrEmpty(act.User.ToString()))
                    {
                        List<UserDTO> username = await _sharedHelpers.GetUserById(act.User);
                        if (username != null)
                        {
                            activitydto.username = username?.FirstOrDefault()?.UserName;
                        }
                    }
                    activitydtolist.Add(activitydto);
                }
            }
            var changepas = new SecurityModel() { _changePassword = activitydtolist };
            return PartialView("_ChangeUserPassword", changepas);
        }
        public async Task<IActionResult> Createreportaccessmatrix(SecurityModel createrepo)
        {
            var viewFunctWithAccessChanged = await _sharedHelpers.GetreportListByreference(createrepo.reportId);

            foreach (var fam in viewFunctWithAccessChanged)
            {
                var deletedAccessMatrix = _sharedHelpers.GetAccessMatrix().Result.Where(x => x.Reference == fam.Id.ToString ()).FirstOrDefault();
                if (deletedAccessMatrix != null)
                {
                    await _sharedHelpers.DeleteAccessMatrix(deletedAccessMatrix.Id);
                }
            }
            if (createrepo.reportpriviligeCode != null && createrepo.reportpriviligeCode.Count() > 0)
            {
                for (int i = 0; i < createrepo.reportpriviligeCode.Count(); i++)
                {
                    var accessMatAdd = new AccessMatrixDTO();
                    accessMatAdd.Reference = createrepo.reportpriviligeCode[i];
                    accessMatAdd.Pointer = 631;
                    accessMatAdd.Role = createrepo.reportrolecode;
                    accessMatAdd.AccessLevel = "1";
                    var savedFlag = await _sharedHelpers.CreateAccessMatrix(accessMatAdd);
                }

            }

            return Json("Saved Sucessfully");
        }

        [HttpPost]
        public async Task<IActionResult> Deleteuseraccount(int Id)
        {
            var resultset = "";
            var checkstatus = false;
            UserDTO userobj = new UserDTO();
            userobj.Id = Id;
            var activity = await _sharedHelpers.GetActivityByUsername(userobj.Id);
            var activityList = activity.Where(c => c.Pointer == 5 || c.Pointer == 3).ToList();

            if (activityList != null && activityList.Where(a => a.Pointer != 994).ToList().Count > 0)
            {
                resultset = "Could Not Delete this user ,It is used by Transactions";
                checkstatus = false;
            }
            else
            {
                var haverole = await _sharedHelpers.GetUserRoleMap(userobj.Id);
                if (haverole == null)
                {

                    var delete = await _sharedHelpers.DeleteActivityById(Id);
                    await _sharedHelpers.DeleteUserRoleMapByCode(Id);

                    resultset = "Delete Successfully";
                    checkstatus = true;

                }
                else
                {
                    resultset = "Not Delete  ,It is used By User Role Mapper";
                    checkstatus = false;

                }
            }


            return Json(new { increment = checkstatus, result = resultset });

        }

        public async Task<IActionResult> changepassworddetail(SecurityModel changepass)
        {
            var passcomplexity = changepass?.cha_passwordtype?.Trim();

            UserDTO reuser = new UserDTO();

            List<DeviceDTO> devcode = new List<DeviceDTO>();

            //ModificationHistory listofmodHistory = new ModificationHistory();
            bool isEnforcePasswordHistory = false;
            var resultset = "";
            var minlength = 0;
            List<string> usercode = new List<string>();
            var muser = await _sharedHelpers.GetUserById(changepass.cha_username);

            var passhistory = await _sharedHelpers.GetConfigurationByRefandPref("Security", CNETConstants.SECURITY_COMPONENT);
            var passcurrnthistory = passhistory.Where(c => c.Attribute == "Enforce Password History").FirstOrDefault();
            var minimmumpasslength = passhistory.Where(c => c.Attribute == "Minimum Password Length").FirstOrDefault();
            var complexpassword = passhistory.Where(c => c.Attribute == "Enforce Complex Password").FirstOrDefault();

            bool isEnforcecomplexPassword = false;

            if (passcurrnthistory != null)
                isEnforcePasswordHistory = Convert.ToBoolean(passcurrnthistory.CurrentValue);

            if (complexpassword != null)
                isEnforcecomplexPassword = Convert.ToBoolean(complexpassword.CurrentValue);

            if (minimmumpasslength != null)
                minlength = Convert.ToInt32(minimmumpasslength.CurrentValue);


            if (changepass.cha_username == null && changepass.cha_oldpasword == null && changepass.cha_newpassword == null && changepass.cha_confirmpassord == null)
            {

                return Json("Enter all fields");
            }
            else
            {
                if (changepass.cha_newpassword != changepass.cha_confirmpassord)
                {
                    resultset = "Password Missmatch";
                }
                else
                {
                    if (changepass?.cha_newpassword?.Length < minlength)
                    {
                        resultset = "Please Check your Setting  Your Minimum Password Length  is  " + minlength;
                    }
                    else if (isEnforcecomplexPassword == true && passcomplexity != "Very Strong")

                    {
                        resultset = "Please Check your Setting  Your Password Is " + passcomplexity;

                    }
                    else
                    {
                        if (muser != null)
                        {
                            //var oldpass = Decrypt(muser?.FirstOrDefault()?.Password);

                            if (changepass.cha_oldpasword != changepass.cha_oldpasword)
                            {
                                resultset = "Old Password not correct";
                            }
                            else
                            {
                                reuser.Id = Convert.ToInt32(muser?.FirstOrDefault()?.Id);
                                reuser.UserName = muser?.FirstOrDefault()?.UserName;
                                reuser.Person = Convert.ToInt32(muser?.FirstOrDefault()?.Person);
                                reuser.LoggedInStatus = muser?.FirstOrDefault()?.LoggedInStatus;
                                reuser.Remark = "";
                                reuser.IsActive = changepass.cha_Isactive;
                                reuser.Password = changepass.cha_newpassword;
                                reuser.Salt = "";

                                var passrepeat = await _sharedHelpers.GetUserBypass(reuser.Password);
                                if (passrepeat.Count() >= 1)
                                {
                                    resultset = "Previously Used Password !";
                                }
                                else
                                {

                                    var updateuserpass = await _sharedHelpers.UpdateUser(reuser);
                                    ActivityDTO mnewUserActivity = new ActivityDTO();
                                    var currentUser = await _authenticationManager.GetAuthenticatedUser();
                                    var currentArticle = await _sharedHelpers.SelectArticleByName(Environment.MachineName.ToLower());

                                    if (currentArticle != null)
                                    {
                                        devcode = await _sharedHelpers.SelectDevice();
                                    }
                                    var devicecode = devcode?.Where(x => x.Article == currentArticle?.FirstOrDefault()?.Id)?.FirstOrDefault()?.Id;
                                    var orgunit = await _sharedHelpers.GetConsigneeunit();
                                    var orgunitdef = orgunit?.Where(x => x.Consignee == devicecode).FirstOrDefault();
                                    mnewUserActivity.Id = 0;
                                    mnewUserActivity.Reference = currentUser.Id; // muser.person;
                                    mnewUserActivity.ActivityDefinition = 0;
                                    mnewUserActivity.TimeStamp = DateTime.UtcNow;
                                    if (orgunitdef != null)
                                        mnewUserActivity.ConsigneeUnit = orgunitdef.Consignee;
                                    mnewUserActivity.Device = null;
                                    mnewUserActivity.User = (int)muser?.FirstOrDefault()?.Id;
                                    mnewUserActivity.Pointer = 1;
                                    mnewUserActivity.Year = DateTime.UtcNow.Year;
                                    mnewUserActivity.ActivityDefinition = DateTime.UtcNow.Month;
                                    //mnewUserActivity.Date = DateTime.UtcNow.Day;
                                    mnewUserActivity.Remark = "";

                                    var createactivity = await _sharedHelpers.CreateActivity(mnewUserActivity);

                                    //listofmodHistory.Code = "";
                                    //listofmodHistory.activity = mnewUserActivity.code;
                                    //listofmodHistory.attribute = "USER_PASSWORD";
                                    //listofmodHistory.oldValue = changepass.cha_oldpasword;
                                    //listofmodHistory.newValue = Encrypt(changepass.cha_newpassword);
                                    //listofmodHistory.Remark = DateTime.Now.ToString();

                                    //var createmod = await _sharedHelpers.Createmodificationhistory(listofmodHistory);
                                    resultset = "Saved Successfully !";

                                }

                            }
                        }

                    }

                }

            }
            return Json(new { result = resultset });
        }
        public IActionResult GetRolehaveuser(bool value)
        {

            var userRole = new SecurityModel() { userRole = value };

            return PartialView("_RoleType", userRole);
        }
        public async Task<IActionResult> GetDocumentListType(int voucherCode, int orgunitdefcode, string selectedname)
        {
            var funcMatrix = await _sharedHelpers.GetFunctWithAccessMatrixByfun(3091, voucherCode, orgunitdefcode);
           
            SecurityModel model =new SecurityModel();
            model.documentpriv = funcMatrix;
            model.docRolecode = orgunitdefcode;
            model.Docategory = voucherCode;
            return PartialView("_DocumentList", model);
        }
        public async Task<IActionResult> getUserRole(int CODE)
        {
            listOfUserRoleMappers = new List<UserRoleMapper>();

            var mRelations = await _sharedHelpers.GetUserRoleMapByrole(CODE);

            //address = new List<Address>();

            //listOfUserRoleMappers = new List<UserRoleMapper>();
            //if (address == null)
            //    address = await _sharedHelpers.SelectAllAddress(); 

            List<UserDTO> mUsers = await _sharedHelpers.GetUser();
            List<Member> mMembers = await UserToMember(mRelations);

            var useraccountList = new SecurityModel() { mMembers = mMembers };

            return PartialView("_userMemebership", useraccountList);
        }
        public async Task<List<Member>> UserToMember(List<UserRoleMapperDTO> userRole)
        {
            //CNETMemoryManagement.EnhanceMemo();
            List<Member> mMembers = new List<Member>();

            foreach (var ur in userRole)
            {
                if (ur != null)
                {
                    Member mmMember = new Member();
                    var Person = await _sharedHelpers.GetActiveUserPersonList();
                    var mPerson = Person.Where(u => u.Id == ur.User).FirstOrDefault();
                    if (mPerson == null) continue;
                    //var add = address.Where(a => a.reference == mPerson.person).ToList();
                    //string personAdd = "";
                    //for (int i = 0; i < add.Count; i++)
                    //{
                    //    if (i == 0) personAdd += add[i].value;

                    //    else
                    //        personAdd += "," + add[i].value;
                    //}

                    mmMember.EmployeeName = mPerson.FullName;
                    mmMember.empCode = mPerson.Person.ToString();
                    mmMember.userName = mPerson.UserName;
                    mmMember.userRoleMaperId = ur.Id;
                    UserDTO user = await _sharedHelpers.GetUserByUserName(mPerson.UserName);
                    mmMember.isActive = user.IsActive;
                    //mmMember.Address = personAdd;
                    mMembers.Add(mmMember);
                }
            }
            return mMembers.OrderByDescending(u => u.isActive).ToList();
        }
        public async Task<IActionResult> getNemeandAdd(int Id)
        {
            string FullName = "";
            string personAdd = "";
            var user = await _sharedHelpers.GetUserById(Id);
            var mpersonls = await _sharedHelpers.GetConsigneeById(Convert.ToInt32(user?.FirstOrDefault()?.Person));
            var mperson = mpersonls?.FirstOrDefault();
            //var adderess = await _sharedHelpers.SelectAddressByReference(Id);
            //var adderessvalue = adderess.FirstOrDefault();
            if (mperson != null)
                FullName = mperson.FirstName + " " + mperson.SecondName + " " + mperson.ThirdName;
            //if (adderessvalue != null)
            //    personAdd = adderessvalue.value;

            return Json(new { address = personAdd, result = FullName });
        }
        public async Task<IActionResult> Createusermemebershipdetail(SecurityModel creatrole)
        {
            UserRoleMapperDTO mRelation = new UserRoleMapperDTO();
            var resultTYpe = "";
            bool canSave = false;

            if (creatrole.mem_orgunitcode == 0)
            {
                resultTYpe = "Pleas Select role";
                canSave = false;
                return Json(new { result = resultTYpe, canSaved = canSave });
            }
            else
            {
                if (creatrole.mem_employeename == null && creatrole.mem_username == 0)   
                {
                    resultTYpe = "Enter all fields";
                    canSave = false;
                    return Json(new { result = resultTYpe, canSaved = canSave });
                }
                else
                {
                   var rolemap = await _sharedHelpers.SelectAllUserRoleMapper();

                    var roleusercount = rolemap.Where(u => u.User == creatrole.mem_username).ToList();

                    if (roleusercount.Count() >= 1)
                    {
                        resultTYpe = "User Already has a roll ";
                        canSave = false;
                        return Json(new { result = resultTYpe, canSaved = canSave });
                    }
                    else
                    {
                        mRelation.Id = 0;
                        mRelation.User = creatrole.mem_username;
                        mRelation.Role = creatrole.mem_orgunitcode;
                        mRelation.ExpiryDate = DateTime.Now;
                        mRelation.Remark = "";
                        var Creatrole = await _sharedHelpers.CreateUserRoleMapper(mRelation);
                        resultTYpe = "Saved Successfully";
                        canSave = true;
                        return Json(new { result = resultTYpe, canSaved = canSave });
                    }
                }
            }
        }
       
        public async Task<IActionResult> Deleteuserrole(int code)
        {
            var deletrle = await _sharedHelpers.DeleteUserRoleMapByCode(code);
            return Json("Delete Successfully");
        }
        public async Task<IActionResult> LoadRoleaccess(int subsys, int category, int role)
        {
            SecurityModel access = new SecurityModel();
            var countcheck = false;
            var funcMatrix = await _sharedHelpers.GetFunctWithAccessMatrix(subsys, category,role);
           access.accessprivilage = funcMatrix;
           access.access_orgunitcode = role;
           access.funcCategory = category;
           access.subsystemcompo = subsys;
            return PartialView("_AccessPrivilageDetail", access);
        }
        public async Task<IActionResult> Createaccessmatrix(SecurityModel creatematrix)
        {
            List<AccessMatrix> accessMatrices = new List<AccessMatrix>();
            List<FunctWithAccessMDTO> functs = new List<FunctWithAccessMDTO>();
            var funcMatrix = await _sharedHelpers.GetFunctWithAccessMatrix(creatematrix.subsystemcompo, creatematrix.funcCategory, creatematrix.access_orgunitcode);

            if (funcMatrix != null && funcMatrix.Count() > 0)
            {
                foreach (var item in funcMatrix)
                {
                    if (creatematrix.acesspriviligefuncCode != null && creatematrix.acesspriviligefuncCode.Count() > 0)
                    {
                        if (!(creatematrix.acesspriviligefuncCode.Contains(item?.FunctionId?.ToString())))
                        {
                            functs.Add(new FunctWithAccessMDTO { MatrixId = item.MatrixId, FunctionId = item.FunctionId, SubSystemComponent = item.SubSystemComponent, Access = false });

                        }
                        else
                        {
                            functs.Add(new FunctWithAccessMDTO { MatrixId = item.MatrixId, FunctionId = item.FunctionId, SubSystemComponent = item.SubSystemComponent, Access = true });

                        }
                    }
                    else
                    {
                        functs.Add(new FunctWithAccessMDTO { MatrixId = item.MatrixId, FunctionId = item.FunctionId, SubSystemComponent = item.SubSystemComponent, Access = false });

                    }
                }
            }
            if (functs != null && functs.Count() > 0)
            {
                foreach (var fam in functs)
                {
                    var accessMatAdd = new AccessMatrixDTO();
                    if (fam.MatrixId == null && (fam.Access == true))
                    {
                        accessMatAdd.Reference = fam?.FunctionId?.ToString();
                        accessMatAdd.Pointer = Convert.ToInt32(fam?.SubSystemComponent);
                        accessMatAdd.Role = creatematrix.access_orgunitcode; //fam.category ?? funccode;
                        accessMatAdd.AccessLevel = "1";
                        var savedFlag = await _sharedHelpers.CreateAccessMatrix(accessMatAdd);

                    }
                    if (fam?.MatrixId != 0 && (fam?.Access == false))
                    {
                        var savedFlag = await _sharedHelpers.DeleteAccessMatrix(fam.MatrixId);
                    }
                }
            }
            return Json("Saved Sucessfully");
        }
        public async Task<IActionResult> Createdocumentaccessmatrix(SecurityModel createdocu)
        {
            var funcMatrix = await _sharedHelpers.GetFunctWithAccessMatrixByfun(3091, createdocu.Docategory, createdocu.docRolecode);

            List<AccessMatrix> accessMatrices = new List<AccessMatrix>();
            List<FunctWithAccessMDTO> functs = new List<FunctWithAccessMDTO>();
            if (funcMatrix != null && funcMatrix.Count() > 0)
            {
                foreach (var item in funcMatrix)
                {
                    if (createdocu.documentpriviligefuncCode != null && createdocu.documentpriviligefuncCode.Count() > 0)
                    {
                        if (!(createdocu.documentpriviligefuncCode.Contains(item?.FunctionId?.ToString())))
                        {
                            functs.Add(new FunctWithAccessMDTO { MatrixId = item.MatrixId,Function = item.Function ,FunctionId = item.FunctionId, SubSystemComponent = item.SubSystemComponent, Access = false });

                        }
                        else
                        {
                            functs.Add(new FunctWithAccessMDTO { MatrixId = item.MatrixId, Function = item.Function, FunctionId = item.FunctionId, SubSystemComponent = item.SubSystemComponent, Access = true });

                        }
                    }
                    else
                    {
                        functs.Add(new FunctWithAccessMDTO { MatrixId = item.MatrixId, Function = item.Function, FunctionId = item.FunctionId, SubSystemComponent = item.SubSystemComponent, Access = false });

                    }
                }
            }
            if (functs != null && functs.Count() > 0)
            {
                foreach (var fam in functs)
                {
                    var accessMatAdd = new AccessMatrixDTO();
                    if (fam.MatrixId == null && (fam.Access == true))
                    {
                        accessMatAdd.Reference = fam?.FunctionId?.ToString();
                        accessMatAdd.Pointer = Convert.ToInt32(fam?.SubSystemComponent);
                        accessMatAdd.Role = createdocu.docRolecode; //fam.category ?? funccode;
                        accessMatAdd.AccessLevel = "1";
                        var savedFlag = await _sharedHelpers.CreateAccessMatrix(accessMatAdd);

                    }
                    if (fam?.MatrixId != 0 && (fam?.Access == false))
                    {
                        var savedFlag = await _sharedHelpers.DeleteAccessMatrix(fam.MatrixId);
                    }
                }
            }
            return Json("Saved Sucessfully");
        }
        private string Encrypt(string clearText)
        {

            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        private string Decrypt(string cipherText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        public async Task<bool> CreateFunction(int CODE, string selectdesc)
        {
            try
            {
                var voucerdefinition = await _sharedHelpers.GetSytemConstantBytype("Transaction");
                var cnetCompo = GeneralBufferHolder.SystemConstants;

                List<string> resultcode = new List<string>();
                string prefix = "";
                var VoucerDefList = voucerdefinition?.Where(v => v.Id == CODE).FirstOrDefault();
                if (VoucerDefList != null)
                {

                    DocumentBrowserData = new List<DocumentAccess>();


                    var doucmentBro = await LoadVoucherDocumentAccessData();
                    DocumentBrowserData.AddRange(doucmentBro);
                    if (VoucerDefList.Id != 208)
                    {
                        DocumentBrowserData.Remove(DocumentBrowserData.FirstOrDefault(x => x.value == "Case detail"));
                    }
                    if (VoucerDefList.Id != 207)
                    {
                        DocumentBrowserData.Remove(DocumentBrowserData.FirstOrDefault(x => x.value == "Escalation detail"));
                    }

                    //if (!VoucerDefList.isl.Value)
                    //{
                    //    DocumentBrowserData.Remove(DocumentBrowserData.FirstOrDefault(x => x.value == "Line item detail"));
                    //}
                    //if (VoucerDefList.journalType == null)
                    //{
                    //    DocumentBrowserData.Remove(DocumentBrowserData.FirstOrDefault(x => x.value == "Journal detail"));
                    //}

                }
                else
                {
                    var gslTypelist = await _sharedHelpers.GetsystemConstantById(CODE);

                    if (gslTypelist != null)
                    {
                        if (gslTypelist?.FirstOrDefault()?.ParentId == 3)
                        {
                            DocumentBrowserData = new List<DocumentAccess>();
                            var articledoucmentBro = await LoadArticleDocumentAccessData();

                            DocumentBrowserData.AddRange(articledoucmentBro);
                        }
                        else if (gslTypelist?.FirstOrDefault()?.ParentId == 4)
                        {
                            if (gslTypelist?.FirstOrDefault()?.ParentId == 4)
                            {
                                prefix = "Consignee ";

                                DocumentBrowserData = new List<DocumentAccess>();
                                var perOrgDocument = await LoadPerOrganizationDocumentAccessData();
                                DocumentBrowserData.AddRange(perOrgDocument);
                            }

                        }
                    }
                }

                #region
                bool evenonesaved = false;
                List<string> Parents = DocumentBrowserData.Select(x => x.parent).Distinct().ToList();
                foreach (string par in Parents)
                {

                    var COMP = cnetCompo?.FirstOrDefault(x => x.Description == par && x.Type == "Document Browser Access");
                    if (COMP == null)
                    {
                        COMP = new SystemConstantDTO
                        {
                            Description = par,
                            Type = "Document Browser Access",
                            ParentId = null,
                            Remark = "",
                        };
                        evenonesaved = true;
                        await _sharedHelpers.CreateSystemConstant(COMP);
                    }
                }


                foreach (DocumentAccess DocumentAccess in DocumentBrowserData)
                {
                    int? CNETComponentcode = 0;
                    var COMP = cnetCompo?.FirstOrDefault(x => x.Description == DocumentAccess.value && x.Type == "Document Browser Access");
                    var ParentCOMP = cnetCompo?.FirstOrDefault(x => x.Description == DocumentAccess.parent && x.Type == "Document Browser Access");


                    if (COMP == null)
                    {
                        COMP = new SystemConstantDTO
                        {
                            Description = DocumentAccess.value,
                            Type = "Document Browser Access",
                            ParentId = ParentCOMP?.Id,
                            Remark = "",
                        };
                        var createcom = await _sharedHelpers.CreateSystemConstant(COMP);
                       
                        CNETComponentcode = createcom?.Id;
                    }
                    else
                    {
                        CNETComponentcode = COMP?.Id;
                    }



                    FunctionalityDTO Fun = new FunctionalityDTO
                    {
                        //VisualComponent = Convert.ToInt32(CNETComponentcode),
                        SubSystemComponent = 1617,
                        Description = selectdesc + " " + DocumentAccess.value,
                        Category = CODE,
                        Function = 5,
                    };

                    await _sharedHelpers.CreateFunctionality(Fun);


                }
                #endregion

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public async Task<IActionResult> LoadProcessRolevoucher(int CODE, int ROLE)
        {
            SecurityModel vrole = new SecurityModel();
            var roleact = await _sharedHelpers.GetRoleActivityWithRange(ROLE, CODE);
            vrole.roleacttivity = roleact;
            return PartialView("_ActivityPrivilagevoucher", vrole);

        }
        public async Task<IActionResult> LoadProcessRolegsl(int CODE, int ROLE)
        {

            SecurityModel vrole = new SecurityModel();
            var roleact = await _sharedHelpers.GetRoleActivityWithRange(ROLE, CODE);
            vrole.rolegsl = roleact;

            return PartialView("_ActivityPrivilagegsl", vrole);
        }

        public async Task<IActionResult> getactivitydefinition(Select2Model select2Modelact)
        {
            var selectedcode = Convert.ToInt32(select2Modelact.other);
            List<VwWorkFlowByReferenceViewDTO> obgesatedetable = new List<VwWorkFlowByReferenceViewDTO>();
            if (selectedcode > 100)
            {
                obgesatedetable = await _sharedHelpers.GetWorkFlowsByreference(5,select2Modelact.other);

            }
            else
            {
                obgesatedetable = await _sharedHelpers.GetWorkFlowsByreference( 3,select2Modelact.other);

            }
            var resultSet = new List<Select2Model>();

            if (!string.IsNullOrWhiteSpace(select2Modelact?.q))
            {
                var cartList = obgesatedetable?.Where(x => x.Description.ToString().Contains(select2Modelact?.q.ToLower())).ToList();
                resultSet = cartList?.Select(r => new Select2Model()
                {
                    id = r.Id,
                    text = r.DescriptionName,

                }).ToList();
            }
            else
            {


                resultSet = obgesatedetable?.Select(r => new Select2Model()
                {
                    id = r.Id,
                    text = r.DescriptionName,
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
        public async Task<IActionResult> createActivityPrivilegedetail(SecurityModel Createact)
        {
            RoleActivityDTO roleActivities = new RoleActivityDTO();

            var compCode = "";
            var resultset = "";
            var checkcode = false;
            var checkclear = false;
            var selectedvougslcode = Createact.Voucher_gslcode;
            if (selectedvougslcode != 0)
            {
                if (selectedvougslcode > 100)
                {
                    var voucherdefcomp = GeneralBufferHolder.SystemConstants.Where(x => x.Type ==  "Transaction");
                    var voucherdefcompcode = voucherdefcomp?.Where(x => x.Id == selectedvougslcode).FirstOrDefault();
                    if (voucherdefcompcode != null)
                        compCode = voucherdefcompcode.Type;
                    checkcode = true;
                }
                else
                {
                    var voucherdefcomp = GeneralBufferHolder.SystemConstants.Where(x => x.Type == "GSL Consignee" || x.Type == "GSL Article").ToList();
                    var voucherdefcompcode = voucherdefcomp?.Where(x => x.Id == selectedvougslcode).FirstOrDefault();
                    if (voucherdefcompcode != null)
                        compCode = voucherdefcompcode.Category;
                    checkcode = false;
                }
            }

            if (Createact.activity_definition == 0)
            {
                resultset = "Enter All Fileds";
                checkclear = false;
                return Json(new { clear = checkclear, checkcode = checkcode, result = resultset });

            }
            else
            {
                if (Createact.roleac_code != 0)
                {
                    roleActivities.Id = Createact.roleac_code;
                    roleActivities.Role = Createact.act_rolecode;
                    roleActivities.ActivityDefinition = Createact.activity_definition;
                    roleActivities.Min = Createact.rangemin;
                    roleActivities.Max = Createact.rangemax;
                    roleActivities.NeedsPassCode = Createact.activity_passkey;
                    roleActivities.Remark = Createact.activity_remark;
                    var updaterole = await _sharedHelpers.UpdateRoleActivity(roleActivities);
                    checkclear = true;
                }
                else
                {
                    roleActivities.Id = 0;
                    roleActivities.Role = Createact.act_rolecode;
                    roleActivities.ActivityDefinition = Createact.activity_definition;
                    roleActivities.Min = Createact.rangemin;
                    roleActivities.Max = Createact.rangemax;
                    roleActivities.NeedsPassCode = Createact.activity_passkey;
                    roleActivities.Remark = Createact.activity_remark;

                    var AllroleActivitywithRanges = await _sharedHelpers.GetRoleActivityWithRange(Createact.act_rolecode, selectedvougslcode);
                    var AllroleActivitywithRangesList = AllroleActivitywithRanges?.Where(r => r.ActivityDefinition == Createact.activity_definition).ToList();
                    if (AllroleActivitywithRangesList?.Count() >= 1)
                    {
                        resultset = "This Activity Definition is already in list";
                        checkclear = false;
                        return Json(new { clear = checkclear, checkcode = checkcode, result = resultset });
                    }
                    else
                    {
                        var createrole = await _sharedHelpers.CreateRoleActivity(roleActivities);
                        resultset = "Saved Successfully";
                        checkclear = true;
                        return Json(new { clear = checkclear, checkcode = checkcode, result = resultset });
                    }
                }
            }
            return Json(new { clear = checkclear, checkcode = checkcode, result = resultset });
        }
         
        public async Task<IActionResult> Deleteroleactivity(int code)
        {
            var deleterole = await _sharedHelpers.DeleteRoleActivity(code);
            return Json("Deleted Successfully");
        }
        public async Task<IActionResult> LoadDashboardRoleaccess(int role)
        {
            SecurityModel _sModel = new SecurityModel();
            var funcMatrix = await _sharedHelpers.GetFunctWithAccessMatrixByfunSubsys(role, 557);

            _sModel._subsystemDashbord = funcMatrix;
            _sModel.subsysrolecode = role;
            return PartialView("_subsystemDashboardPrivilege", _sModel);
        }
        //public async Task<IActionResult> getVoucherdashboard(string CODE)
        //{
        //    var voucherDef = await _sharedHelpers.SelectVoucherdefinition();
        //    var cnetCompo = await _sharedHelpers.GetCnetcomponent();
        //    var count = 1;
        //    var voucherdashboards = await _sharedHelpers.VoucherDashBoardPrivilegeSelectByrole(CODE);

        //    var voucherPrivilege = (from priv in voucherdashboards
        //                            join def in voucherDef
        //                            on priv.voucherDefinition equals def.code.ToString()
        //                            select new
        //                            {
        //                                priv.code,
        //                                priv.remark,
        //                                def.type,
        //                                def.name
        //                            }).ToList();



        //    var vouchCount = voucherPrivilege.Count();

        //    var voucherdashPriv = voucherPrivilege?.Select(y => new
        //    {
        //        y.code,
        //        y.name,
        //        y.remark,
        //        sn = count++,
        //        description = (from cnet in cnetCompo
        //                       where cnet.code == y.type
        //                       select new { cnet.code, cnet.description }).FirstOrDefault(),
        //        count = vouchCount
        //        //cnetCompo?.Where(c => c.code == y.type ? c.description : y.type)

        //    }).ToList();


        //    return Json(voucherdashPriv);

        //}

        public async Task<IActionResult> Createsystemdashboard(SecurityModel subsys)
        {
            var funcMatrix = await _sharedHelpers.GetFunctWithAccessMatrixByfunSubsys(subsys.subsysrolecode, 557);
            var resultset = "";
            List<AccessMatrix> accessMatrices = new List<AccessMatrix>();
            List<FunctWithAccessMDTO> functs = new List<FunctWithAccessMDTO>();
            if (funcMatrix != null && funcMatrix.Count() > 0)
            {
                foreach (var item in funcMatrix)
                {
                    if (subsys.subsysDashpriviligefuncCode != null && subsys.subsysDashpriviligefuncCode.Count() > 0)
                    {
                        if (!(subsys.subsysDashpriviligefuncCode.Contains(item?.FunctionId?.ToString())))
                        {
                            functs.Add(new FunctWithAccessMDTO { MatrixId = item.MatrixId, Function = item.Function, FunctionId = item.FunctionId, SubSystemComponent = item.SubSystemComponent, Access = false });

                        }
                        else
                        {
                            functs.Add(new FunctWithAccessMDTO { MatrixId = item.MatrixId, Function = item.Function, FunctionId = item.FunctionId, SubSystemComponent = item.SubSystemComponent, Access = true });

                        }
                    }
                    else
                    {
                        functs.Add(new FunctWithAccessMDTO { MatrixId = item.MatrixId, Function = item.Function, FunctionId = item.FunctionId, SubSystemComponent = item.SubSystemComponent, Access = false });

                    }
                }
            }
            if (functs != null && functs.Count() > 0)
            {
                foreach (var fam in functs)
                {
                    var accessMatAdd = new AccessMatrixDTO();
                    if (fam.MatrixId == null && (fam.Access == true))
                    {
                        accessMatAdd.Reference = fam?.FunctionId?.ToString();
                        accessMatAdd.Pointer = Convert.ToInt32(fam?.SubSystemComponent);
                        accessMatAdd.Role = subsys.subsysrolecode; //fam.category ?? funccode;
                        accessMatAdd.AccessLevel = "1";
                        var savedFlag = await _sharedHelpers.CreateAccessMatrix(accessMatAdd);

                    }
                    if (fam?.MatrixId != 0 && (fam?.Access == false))
                    {
                        var savedFlag = await _sharedHelpers.DeleteAccessMatrix(fam.MatrixId);
                    }
                }
            }
            resultset = "Saved Sucessfullyy";

            return Json(new { result = resultset });
        }
        public async Task<IActionResult> saveVoucerDashboardprivilege(SecurityModel vouchdash)
        {
            //var resultset = "";
            //VoucherDashBoardPrivilege VoucherDashBoardPrivilegeList = new VoucherDashBoardPrivilege();

            //if (vouchdash.voucherdashrole == null)
            //{
            //    resultset = "Please Select role First";
            //}
            //else
            //{
            //    for (int item = 0; item < vouchdash.voucherCode.Count(); item++)
            //    {
            //        var vouchercode = await _sharedHelpers.GetVoucherDefinitionByname(vouchdash.voucherCode[item].Trim());

            //        if (vouchercode != null)
            //        {
            //            VoucherDashBoardPrivilegeList.code = "";
            //            VoucherDashBoardPrivilegeList.role = vouchdash.voucherdashrole;
            //            VoucherDashBoardPrivilegeList.voucherDefinition = vouchercode.code.ToString();
            //            VoucherDashBoardPrivilegeList.remark = vouchdash.voucherremark[item] == null ? "" : vouchdash.voucherremark[item];
            //            var createvouch = await _sharedHelpers.VoucherDashBoardPrivilegeInsert(VoucherDashBoardPrivilegeList);
            //            resultset = "Saved Successfullyy";
            //        }
            //    }

            //}
            return Json("");
        }

        public async Task<IActionResult> Deletevoucherdashboard(string Id)
        {
            //var deltvoucher = await _sharedHelpers.DeleteVoucherdashboard(Id);
            return Json("Saved Successfully");
        }
        public async Task<IActionResult> GetUserroleMapperById(int code)
        {
            var userMapper = await _sharedHelpers.GetUserRoleMapById(code);
            var user = await _sharedHelpers.GetUserById(userMapper?.FirstOrDefault()?.User);
            var consignee = await _sharedHelpers.GetConsigneeById(Convert.ToInt32(user?.FirstOrDefault()?.Person));
            var mperson = consignee?.FirstOrDefault();
           var fullName = mperson.FirstName + " " + mperson.SecondName + " " + mperson.ThirdName;
            return Json(new { id = userMapper?.FirstOrDefault()?.Id, user = userMapper?.FirstOrDefault()?.User, employe = fullName });
        }
        public async Task<List<DocumentAccess>> LoadVoucherDocumentAccessData()
        {
            VoucherDocumentBrowserDataList = new List<DocumentAccess>
            {
                new DocumentAccess{value ="Excel",parent ="Export"},
                new DocumentAccess{value ="CSV",parent ="Export"},
                new DocumentAccess{value ="PDF",parent ="Export"},
                new DocumentAccess{value ="Peachtree",parent ="Export"},
                new DocumentAccess{value ="XML",parent ="Export"},
                new DocumentAccess{value ="Edit consignee profile",parent ="Options"},
                new DocumentAccess{value ="Edit voucher consignee",parent ="Options"},
                new DocumentAccess{value ="Add reference",parent ="Options"},
                new DocumentAccess{value ="Closed relation",parent ="Options"},
                new DocumentAccess{value ="Voucher extension",parent ="Options"},
                new DocumentAccess{value ="Withholding",parent ="Options"},
                new DocumentAccess{value ="Income tax",parent ="Options"},
                new DocumentAccess{value ="Non cash payment",parent ="Options"},
                new DocumentAccess{value ="Attachment",parent ="Options"},
                new DocumentAccess{value ="Voucher account",parent ="Options"},
                new DocumentAccess{value ="Batch post",parent ="Options"},
                new DocumentAccess{value ="Sync all",parent ="Options"},
                new DocumentAccess{value ="Journal cost update",parent ="Options"},
                new DocumentAccess{value ="Document cost update",parent ="Options"},
                new DocumentAccess{value ="Org unit",parent ="Combo"},
                new DocumentAccess{value ="User",parent ="Combo"},
                new DocumentAccess{value ="Device",parent ="Combo"},
                new DocumentAccess{value ="Date Criteria",parent ="Combo"},
                new DocumentAccess{value ="Journal detail",parent ="Bottom Tabs"},
                new DocumentAccess{value ="Attachment",parent ="Bottom Tabs"},
                new DocumentAccess{value ="Voucher note",parent ="Bottom Tabs"},
                new DocumentAccess{value ="Line item detail",parent ="Bottom Tabs"},
                new DocumentAccess{value ="Consignee History",parent ="Bottom Tabs"},
                new DocumentAccess{value ="Map",parent ="Bottom Tabs"},
                new DocumentAccess{value ="Voucher detail",parent ="Side Tabs"},
                new DocumentAccess{value ="Consignee detail",parent ="Side Tabs"},
                new DocumentAccess{value ="Key Detail",parent ="Side Tabs"},
            };
            return VoucherDocumentBrowserDataList;
        }
        public async Task<List<DocumentAccess>> LoadArticleDocumentAccessData()
        {
            ArticleDocumentBrowserData = new List<DocumentAccess>
                {
                    new DocumentAccess{value ="Excel",parent ="Export"},
                    new DocumentAccess{value ="CSV",parent ="Export"},
                    new DocumentAccess{value ="PDF",parent ="Export"},
                    new DocumentAccess{value ="Peachtree",parent ="Export"},
                    new DocumentAccess{value ="XML",parent ="Export"},
                    new DocumentAccess{value ="Document",parent ="Print"},
                    new DocumentAccess{value ="Selected Label",parent ="Print"},
                    new DocumentAccess{value ="All Label",parent ="Print"},
                    new DocumentAccess{value ="Attachment",parent ="Bottom Tabs"},
                    new DocumentAccess{value ="Transaction History",parent ="Bottom Tabs"},
                    new DocumentAccess{value ="GSL Detail",parent ="Side Tabs"},
                    new DocumentAccess{value ="Key Detail",parent ="Side Tabs"},
                };

            return ArticleDocumentBrowserData;
        }
        public async Task<List<DocumentAccess>> LoadPerOrganizationDocumentAccessData()
        {
            PersonOrganizationDocumentBrowserData = new List<DocumentAccess>
                {
                    new DocumentAccess{value ="Excel",parent ="Export"},
                    new DocumentAccess{value ="CSV",parent ="Export"},
                    new DocumentAccess{value ="PDF",parent ="Export"},
                    new DocumentAccess{value ="Peachtree",parent ="Export"},
                    new DocumentAccess{value ="XML",parent ="Export"},
                    new DocumentAccess{value ="Attachment",parent ="Bottom Tabs"},
                    new DocumentAccess{value ="Transaction History",parent ="Bottom Tabs"},
                    new DocumentAccess{value ="GSL Detail",parent ="Side Tabs"},
                    new DocumentAccess{value ="Key Detail",parent ="Side Tabs"},
                };

            return PersonOrganizationDocumentBrowserData;
        }

        public async Task<IActionResult> GetroleActivityById(int code)
        {
            var role = await _sharedHelpers.GetRoleactivityById(code);
            return Json(new { code = role.Id, actidefn = role.ActivityDefinition, rangemin = role.Min,rangemax = role.Max, passkey = role.NeedsPassCode, remark = role.Remark });
                
        }
        public async Task<IActionResult> AccessPrivlageReport(int code, int role)
        {
            var report = await _sharedHelpers.GetreportListByreference(code);
            var AllAccessMatrixxx = await _sharedHelpers.GetAccessMatrix();
            var AllAccessMatrix = AllAccessMatrixxx.Where(x => x.Pointer == 631 && x.Role == role).ToList();
            List<NewAccessMatrix> ListAccessMatrix = new List<NewAccessMatrix>();
            foreach (var cnetreport in report)
            {
                NewAccessMatrix accmatrix = new NewAccessMatrix();
                accmatrix.id = cnetreport.Id;
                accmatrix.pointer = 631;
                accmatrix.reference = cnetreport.Id.ToString();
                accmatrix.role = role.ToString();
                var valueabeleble = AllAccessMatrix.Where(x => x.Reference == cnetreport.Id.ToString()).FirstOrDefault();
                if (valueabeleble != null)
                {
                    accmatrix.accessLevel = true;
                }
                else
                {
                    accmatrix.accessLevel = false;
                }
                accmatrix.remark = cnetreport.DefaultName;
                ListAccessMatrix.Add(accmatrix);
            }
            SecurityModel sModel =  new SecurityModel();
            sModel.ReportType = ListAccessMatrix;
            sModel.reportId = code;
            sModel.reportrolecode = role;
            return PartialView("_ReportPrivilageDetail", sModel);
        }
    }
}

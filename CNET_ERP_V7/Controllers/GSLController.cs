
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
using CNET_V7_Entities.DataModels;
using CNET_V7_Domain.Domain.CommonSchema;
using CNET_V7_Domain.Domain.AccountingSchema;
using CNET_V7_Domain.Domain.ConsigneeSchema;
using Cnetv7BufferHolder;
using System.Xml.Schema;

namespace CNET_ERP_V7.Controllers
{
    //[Authorize]
    //[AutoValidateAntiforgeryToken]
    public class GSLController : Controller
    {
        private AuthenticationManager _authenticationManager;
        private readonly HttpClient _httpClient;
        private readonly SharedHelpers _sharedHelpers;
        public List<ConfigurationDTO> Config = new List<ConfigurationDTO>();
        private IWebHostEnvironment _env;
        int loggedUser = 0;
        #region Ctor
        public GSLController(AuthenticationManager authenticationManager,
              IHttpClientFactory httpClientFactory,
              SharedHelpers sharedHelpers,
              IWebHostEnvironment env)
        {
            _authenticationManager = authenticationManager;
            _httpClient = httpClientFactory.CreateClient("mainclient");
            _sharedHelpers = sharedHelpers;
            _env = env;
        }

        #endregion

        public async Task<IActionResult> List(int id)
        {
            var authUser = await _authenticationManager.GetAuthenticatedUser();

            if (authUser == null)
            {
                return RedirectToAction("Login", "Login");
            }
           return View(new GSLmodel
            {
                gslType = id,
               
            });

        }
        public async Task<IActionResult> SavingArticleProperty(GSLmodel artModelforgsl)

        {
              List<ConfigurationDTO> _configuration = new List<ConfigurationDTO>();
                var gslTypecode = artModelforgsl.gslType.ToString();
                Config = await _sharedHelpers.GetConfigurationByRefandPref(artModelforgsl?.gslType.ToString(),CNETConstants.MAIN_TYPE_GSL);
                var deletete = _sharedHelpers.DeleteConfigurationByReferenceAndPreference(gslTypecode, CNETConstants.MAIN_TYPE_GSL);
           
                ConfigurationDTO config = new ConfigurationDTO();

            config =  await SaveData("Default Document", artModelforgsl.default_document, gslTypecode);
            _configuration.Add(config);
            config =    await SaveData("Document Browser Library URL", artModelforgsl.document_Browser_Library_URL, gslTypecode);
            _configuration.Add(config);
             config = await SaveData("Allow Duplicate Name", artModelforgsl.allow_Duplicate_Name, gslTypecode);
            _configuration.Add(config);
             config = await SaveData("Category Depth", artModelforgsl.category_Depth, gslTypecode);
            _configuration.Add(config);
             config =   await SaveData("Maximum Dates Without Transaction", artModelforgsl.Maximum_Dates_Without_Transaction, gslTypecode);
            _configuration.Add(config);

                if (!string.IsNullOrWhiteSpace(artModelforgsl.default_ObjectStategsl))
                {
                    string DefaultObjectState = artModelforgsl.default_ObjectStategsl.Split('/')[0];
                  config = await SaveData("Default ObjectState", DefaultObjectState, gslTypecode);
                    _configuration.Add(config);
                }
                else
                {
                    config =  await SaveData("Default ObjectState", "", gslTypecode);
                    _configuration.Add(config);
                }
            config =  await SaveData("Default Tax", artModelforgsl.default_Tax, gslTypecode);
            _configuration.Add(config);
             config =  await SaveData("IdGenerationStyle", artModelforgsl.id_Generation_Style, gslTypecode);
            _configuration.Add(config);
             config =  await SaveData("Maintain Case", artModelforgsl.maintain_Case, gslTypecode);
            _configuration.Add(config);
             config =  await SaveData("Maintain Library URL", artModelforgsl.maintain_Library_URL, gslTypecode);
            _configuration.Add(config);
            config =   await SaveData("Enable Label Printing", artModelforgsl.enable_Label_Printing, gslTypecode);
            _configuration.Add(config);
              config = await SaveData("Label Design File", artModelforgsl.label_Design_File, gslTypecode);
            _configuration.Add(config);
             config =   await SaveData("Label Printer", artModelforgsl.label_Printer, gslTypecode);
            _configuration.Add(config);
             config =   await SaveData("Label Type", artModelforgsl.label_Type, gslTypecode);
            _configuration.Add(config);
             config =  await SaveData("Preview Label Before Print", artModelforgsl.preview_Label_Before_Print, gslTypecode);
            _configuration.Add(config);
              config = await SaveData("Near Me in Meter", artModelforgsl.near_Me_in_Meter, gslTypecode);
            _configuration.Add(config);
             config = await SaveData("Advanced Combo Search As Type", artModelforgsl.advanced_Combo_Search_As_Type, gslTypecode);
            _configuration.Add(config);
            config = await SaveData("Clear Log After Days", artModelforgsl.clear_Log_After_Days, gslTypecode);
            _configuration.Add(config);
            config =   await SaveData("Maintain Modification History", artModelforgsl.maintain_Modification_History, gslTypecode);
            _configuration.Add(config);
             config =  await SaveData("Maximum Records", artModelforgsl.maximum_Records, gslTypecode);
            _configuration.Add(config);
             config =  await SaveData("Enable Mobile Transaction", artModelforgsl.enable_Mobile_Transaction, gslTypecode);
            _configuration.Add(config);
              config = await SaveData("Consumer Price Formula", string.IsNullOrEmpty(artModelforgsl.consumer_Price_Formula) ? "" : artModelforgsl.consumer_Price_Formula, gslTypecode);
            _configuration.Add(config);
             config =   await SaveData("Discounted Price Formula", string.IsNullOrEmpty(artModelforgsl.discounted_Price_Formula) ? "" : artModelforgsl.discounted_Price_Formula, gslTypecode);
            _configuration.Add(config);
              config = await SaveData("On holiday Formula", string.IsNullOrEmpty(artModelforgsl.on_holiday_Formula) ? "" : artModelforgsl.on_holiday_Formula, gslTypecode);
            _configuration.Add(config);
              config = await SaveData("Qty Price Formula", string.IsNullOrEmpty(artModelforgsl.qty_Price_Formula) ? "" : artModelforgsl.qty_Price_Formula, gslTypecode);
            _configuration.Add(config);
              config = await SaveData("Retail Price Formula", string.IsNullOrEmpty(artModelforgsl.retail_Price_Formula) ? "" : artModelforgsl.retail_Price_Formula, gslTypecode);
            _configuration.Add(config);
              config = await SaveData("Whole Sale Formula", string.IsNullOrEmpty(artModelforgsl.whole_Sale_Formula) ? "" : artModelforgsl.whole_Sale_Formula, gslTypecode);
            _configuration.Add(config);
              config = await SaveData("Enable Group panel", artModelforgsl.gsl_EnableGrouppanel.ToString(), gslTypecode);
            _configuration.Add(config);
              config =  await SaveData("Enable Search bar", artModelforgsl.gsl_EnableSearchbar.ToString(), gslTypecode);
            _configuration.Add(config);
             config =    await SaveData("Enable Filter", artModelforgsl.gsl_EnableFilter.ToString(), gslTypecode);
            _configuration.Add(config);
            config =   await SaveData("Enable Field search", artModelforgsl.gsl_EnableFieldsearch.ToString(), gslTypecode);
            _configuration.Add(config);
            await _sharedHelpers.saveOrUpdateRange(_configuration);
            return Json("Save Successfully");
        } 
        public async Task<ConfigurationDTO> SaveData(string attr, string curr, string vouchercode)

        {

            var gsl = await _sharedHelpers.GetsystemConstantById(Convert.ToInt32(vouchercode));
            var gslcomp = gsl?.FirstOrDefault()?.ParentId;
            ConfigurationDTO configuration = new ConfigurationDTO();
            List<ConfigurationDTO> listcConfigurations = new List<ConfigurationDTO>();
            ConfigurationDTO Prev = Config.Where(x =>x.Attribute?.Trim() == attr?.Trim())?.FirstOrDefault();

            configuration.Pointer = CNETConstants.MAIN_TYPE_GSL;
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
        public async Task<IActionResult> GetGSLidDefinition(string BranchCode, int gslCode, int category)
        {
            GSLmodel modell = new GSLmodel();
            var Gmodel = new GSLmodel() { cosigneeCode = Convert.ToInt32(BranchCode), gslcategory = category,gslType = gslCode };
            return PartialView("_IdSettingMap", Gmodel);
        }  
        public async Task<IActionResult> getGetPreferenceList(int gslCode)
        {
            GSLmodel modell = new GSLmodel();
            var Gmodel = new GSLmodel() {gslType = gslCode };
            return PartialView("_Preference", Gmodel);
        }
        public async Task<IActionResult> createIdsettingarticle(GSLmodel createarticle)
        {
            IdsettingDTO idSetting = new IdsettingDTO();
            var resultset = "";
            var checklist = false;
            if (createarticle.id_definitionart == 0)
            {
                resultset = "Enter all fields";
                checklist = false;
                return Json(new { result = resultset, check = checklist });
            }
            else
            {
               await currentUserAndUnit();
                if (createarticle.code_idsettingart != 0)
                {
                    idSetting.Id = createarticle.code_idsettingart;
                    idSetting.Reference = createarticle.VoucherSettigForIdsettingart;
                    idSetting.IdDefinition = createarticle.id_definitionart;
                    idSetting.Device = createarticle.deviceart;
                    idSetting.ConsigneeUnit = createarticle.organizationundefart;
                    idSetting.StartFrom = createarticle.start_Fromart;
                    idSetting.CurrentValue = createarticle.start_Fromart;
                    idSetting.User = loggedUser;
                    idSetting.IsFlexible = createarticle.iS_flexibleart;
                    idSetting.Remark = createarticle.remarkart;
                    var updateidse = await _sharedHelpers.UpdateIdsetting(idSetting);
                    resultset = "Update Successsfully";
                    checklist = true;
                    return Json(new { result = resultset, check = checklist });
                }
                else
                {
                    idSetting.Id = 0;
                    idSetting.Reference = createarticle.VoucherSettigForIdsettingart;
                    idSetting.IdDefinition = createarticle.id_definitionart;
                    idSetting.Device = createarticle.deviceart;
                    idSetting.ConsigneeUnit = createarticle.organizationundefart;
                    idSetting.StartFrom = createarticle.start_Fromart;
                    idSetting.User = loggedUser;
                    idSetting.CurrentValue = createarticle.start_Fromart;
                    idSetting.IsFlexible = createarticle.iS_flexibleart;
                    idSetting.Remark = createarticle.remarkart;

                    var createmodel = await _sharedHelpers.CreateIdsetting(idSetting);
                    resultset = "Saved Successfully";
                    checklist = true;
                    return Json(new { result = resultset, check = checklist });
                }

            }
        }
        public async Task<IActionResult> GetGSLIdsettingBycode(int code)
        {
            var idsettingList = await _sharedHelpers.GetIdsettingById(code);
            var idsetting = idsettingList?.FirstOrDefault();
            var _iddn = await _sharedHelpers.GetIdDefinitionById(idsetting?.IdDefinition);

            return Json(new { code = idsetting.Id, iddeDes = _iddn?.FirstOrDefault()?.Description, iddefn = idsetting.IdDefinition, starfrom = idsetting.StartFrom, isflixiable = idsetting.IsFlexible, device = idsetting.Device, remark = idsetting.Remark });
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
     
        public IActionResult GetPropertyBygSLtyPE(int id)
        {

            var ArticleType = new GSLmodel() { gslType = id };
            return PartialView("_ArtProperty", ArticleType);
        }
        public IActionResult GetPrefparent(int id)
        {
            var ArticleType = new GSLmodel() { gslType = id };
            return PartialView("preferentialMapModal", ArticleType);
        }
        public async Task<IActionResult> getWorkflowList(int gslcode)
        {
            int count = 1;
            int gsltypeList = 0;
            var gsltype = await _sharedHelpers.GetsystemConstantById(gslcode);
            if (gsltype?.FirstOrDefault().Type == "GSL Article")
            {
                gsltypeList = 3;
            }
            else
            {
                gsltypeList = 4;
            }
            var workflow = await _sharedHelpers.GetWorkFlowsByreferenceType(gslcode);
            var workFlowLi = workflow?.OrderBy(w => w.Index).ToList();
          
            var workflowList = new GSLmodel() { vw_WorkFlows = workFlowLi };

            return PartialView("_Articleworkflow", workflowList);
        }
        public async Task<IActionResult> createArticleworkflow(GSLmodel worflo)
        {
            ActivityDefinitionDTO actdef = new ActivityDefinitionDTO();
            bool CanSaved = false;
            var resultTYpe = "";
            try
            {
                if (worflo.indexwrflart.ToString() != "0" || worflo.sequencewrflart)
                {
                    if (worflo.codewrflart == 0)
                    {
                        var ActiveworkFlows = await _sharedHelpers.GetWorkFlowsByreference(5, worflo.Vouchercodeforworkflowart);
                        if (ActiveworkFlows != null && ActiveworkFlows.Count() > 0)
                        {
                            foreach (var act in ActiveworkFlows)
                            {
                                if (ActiveworkFlows.Any(x => x.Index == Convert.ToByte(worflo.indexwrflart.ToString())))
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
                        var ActiveworkFlows = await _sharedHelpers.GetWorkFlowsByreferenceType(worflo.Vouchercodeforworkflowart);
                        if (ActiveworkFlows != null && ActiveworkFlows.Count() > 0)
                        {
                            foreach (var act in ActiveworkFlows)
                            {
                                if (ActiveworkFlows.Any(x => x.Index == Convert.ToByte(worflo.indexwrflart) && x.Id != (worflo.codewrflart)))
                                {
                                    resultTYpe = "Duplicate Index Used!";
                                    CanSaved = false;
                                    return Json(new { saved = CanSaved, result = resultTYpe });
                                }
                            }
                        }
                    }
                }
                var _sys = await _sharedHelpers.GetsystemConstantById(worflo.Vouchercodeforworkflowart);

                ActivityDefinitionDTO activityDefinition = new ActivityDefinitionDTO
                {
                    //Pointer = _sys?.FirstOrDefault()?.ParentId,
                    Reference = worflo.Vouchercodeforworkflowart,
                    Description = worflo.descriptionwrflart,
                    Index = worflo.indexwrflart,
                    IsManual = worflo.isManualwrflart,
                    IssuingEffect = worflo.hasIssuingEffectwflart,
                    RequiredTime = worflo.requiredTimewrflart,
                    MaxRepeat = worflo.maxRepeatwrflart,
                    Sequence = worflo.sequencewrflart,
                    State = worflo.objectStateDefinitionwrflart,
                    Remark = worflo.remarkwrflart
                };
                if (worflo.hasIssuingEffectwflart)
                {
                    var workFlows = await _sharedHelpers.GetWorkFlowsByreference(5, worflo.Vouchercodeforworkflowart);
                    ActivityDefinitionDTO activity;
                    if (workFlows != null && workFlows.Count() > 0)
                    {
                        workFlows = workFlows.Where(x => x.IssuingEffect == true).ToList();
                        if (workFlows != null)
                        {
                            foreach (var value in workFlows)
                            {

                                activity = new ActivityDefinitionDTO();
                                activity.Id = value.Id;
                                //activity.Pointer = _sys?.FirstOrDefault()?.ParentId;
                                activity.Reference = value.Reference;
                                activity.Description = value.Description;
                                activity.Index = value.Index;
                                activity.IsManual = value.IsManual;
                                activity.IssuingEffect = false;
                                activity.RequiredTime = value.RequiredTime;
                                activity.MaxRepeat = value.MaxRepeat;
                                activity.Sequence = value.Sequence;
                                activity.State = value.State;
                                activity.Remark = value.Remark;
                                await _sharedHelpers.UpdateActivityDefinition(activity);
                            }
                        }
                    }
                }
               

                if (worflo.codewrflart != 0 && worflo.codewrflart > 0)
                {
                    if (!String.IsNullOrWhiteSpace(worflo.descriptionwrflart.ToString()) && !String.IsNullOrWhiteSpace(worflo.objectStateDefinitionwrflart.ToString()))
                    {
                        var workFlows = _sharedHelpers.GetWorkFlowsByreference(5, worflo.Vouchercodeforworkflowart).Result.Where(w => w.Description == worflo.descriptionwrflart && w.ObjectStateDefinition == worflo.objectStateDefinitionwrflart).ToList();
                        if (workFlows.Count() == 0)
                        {
                            activityDefinition.Id = worflo.codewrflart;
                            await _sharedHelpers.UpdateActivityDefinition(activityDefinition);
                            resultTYpe = "Saved Successfully";
                            CanSaved = true;
                            return Json(new { saved = CanSaved, result = resultTYpe });
                        }
                        else
                        {
                            if (worflo.codewrflart == workFlows.FirstOrDefault().Id)
                            {
                                activityDefinition.Id = worflo.codewrflart;
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
                    if (!String.IsNullOrWhiteSpace(worflo.descriptionwrflart.ToString()) && !String.IsNullOrWhiteSpace(worflo.objectStateDefinitionwrflart.ToString()))
                    {
                        var workFlow = await _sharedHelpers.GetWorkFlowsByreference(5, worflo.Vouchercodeforworkflowart);

                        var workFlows = workFlow?.Where(w => w.Description == worflo.descriptionwrflart && w.ObjectStateDefinition == worflo.objectStateDefinitionwrflart).ToList();
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
                            CanSaved = false;
                            return Json(new { saved = CanSaved, result = resultTYpe });

                        }
                    }
                    else
                    {
                        resultTYpe = "Enter All Fields";
                        CanSaved = false;
                        return Json(new { saved = CanSaved, result = resultTYpe });
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<IActionResult> DeletearticleWorkflow(int code,int gslcode)
        {
            List<ObjectState> objList = new List<ObjectState>();
            VwWorkFlowByReferenceView vw_WorkFlowBy = new VwWorkFlowByReferenceView();
            var selectworkbyref = await _sharedHelpers.GetWorkFlowsByreferenceTypeandId(gslcode, code);
           
            if (selectworkbyref != null)
            {
                var delworfl = await _sharedHelpers.DeleteActivityDefinitionById(code);

                var selctobjesate = await _sharedHelpers.GetObjeectStateByrefe(selectworkbyref?.FirstOrDefault()?.Id);
                if (selctobjesate != null)
                {
                    var delwoinobj = await _sharedHelpers.DeleteObjectStateByReference(selctobjesate?.FirstOrDefault()?.Id);

                }
            }

            return Json("Deleted Succssefully");
        }

        public async Task<IActionResult> GeTworkflowbycode(int code)
        {

            var workflowList = await _sharedHelpers.GetWorkFlowsByid(code);
            var workflow = workflowList?.FirstOrDefault();
            return Json(new { code = workflow.Id, descriptionname = workflow.Description, index = workflow.Index, ismanual = workflow.IsManual,maxrepeat = workflow.MaxRepeat, sequence = workflow.Sequence, objectstat = workflow.ObjectStateDefinition, requiredtime = workflow.RequiredTime, remark = workflow.Remark });

        }
        public async Task<IActionResult> getaccrequireList(int gslcode)
        {
            List<GslacctRequirementDTO> gslacc = new List<GslacctRequirementDTO>();
            List<GslacctRequirementDTO> gslaccList = new List<GslacctRequirementDTO>();

            var acccontrol = await _sharedHelpers.GetControlAccount();

            var acccontrolgsl = await _sharedHelpers.GetGSLAcctRequirementBygslType(Convert.ToInt32(gslcode));


            var gslaccrequr = (from gsl in acccontrolgsl
                               join acc in acccontrol
                               on gsl.ContAcct equals acc.Id

                               select new
                               {
                                   gsl.Id,
                                   gsl.IsMandatory,
                                   gsl.RestrictSelection,
                                   gsl.Remark,
                                   desccode = acc.Id,
                                   acc.Description

                               }).ToList();
            gslacc = gslaccrequr.Select(x => new GslacctRequirementDTO
            {
                Id = x.Id,
                IsMandatory = x.IsMandatory,
                RestrictSelection = x.RestrictSelection,
                Remark = x.Description,


            }).ToList();


            gslaccList.AddRange(gslacc);

            var gslaccre = new GSLmodel() { acctRequirements = gslaccList };
            return PartialView("_articleaccountrequirement", gslaccre);
        }
        public async Task<IActionResult> createaccrequirearticle(GSLmodel creategslacc)
        {
            GslacctRequirementDTO gSLAcct = new GslacctRequirementDTO();

            if (creategslacc.acccontrol == 0)
            {
                return Json("Enter all Fields");
            }
            else
            {
                if (creategslacc.accode != 0)
                {
                    gSLAcct.Id = creategslacc.accode;
                    gSLAcct.ContAcct = creategslacc.acccontrol;
                    gSLAcct.GslTypeList = creategslacc.gsltypecodeforInsert;
                    gSLAcct.IsMandatory = creategslacc.accismandatory;
                    gSLAcct.RestrictSelection = creategslacc.accresirictionselection;
                    gSLAcct.Remark = creategslacc.accremark;

                    var updateereq = await _sharedHelpers.UpdateSLAcctRequirement(gSLAcct);
                }
                else
                {
                    gSLAcct.Id = 0;
                    gSLAcct.ContAcct = creategslacc.acccontrol;
                    gSLAcct.GslTypeList = creategslacc.gsltypecodeforInsert;
                    gSLAcct.IsMandatory = creategslacc.accismandatory;
                    gSLAcct.RestrictSelection = creategslacc.accresirictionselection;
                    gSLAcct.Remark = creategslacc.accremark;

                    var createreq = await _sharedHelpers.CreateSLAcctRequirement(gSLAcct);

                }

            }
            return Json("Saved Successfully");

        }

        public async Task<IActionResult> Deletearticleaccrequ(int CODE)
        {

            var delete = await _sharedHelpers.DeletaccrequiredBycode(CODE);
            if (Convert.ToBoolean(delete) == true)
            {
                return Json("Deleted Successfully");
            }
            else
            {
                return Json("Used by other Transaction You Cant Not Deleted");

            }
        }

        public async Task<IActionResult> GeTgslaccbycode(int code)
        {
            var gslaccList = await _sharedHelpers.GetGSLAcctRequirementBycode(code);
            var gslacc = gslaccList?.FirstOrDefault();
            return Json(new { gslacc.Id, gslacc.ContAcct, gslacc.IsMandatory, gslacc.RestrictSelection, gslacc.Remark });
        }
        public async Task<IActionResult> getaccountmappreference(string prefcode)
        {
            List<AccountMapDTO> acmds = new List<AccountMapDTO>();
            var AcountMap = await _sharedHelpers.GetAccountMapByrefrence(prefcode);

            //acmds = await PopulateAccountMap(AcountMap);
            var accountMap = new GSLmodel() { accountMaps = AcountMap };
            return PartialView("_PreferenceMap", accountMap);
        }

        public async Task<List<AccountMapDTO>> PopulateAccountMap(List<AccountMapDTO> accounMaps)
        {
            try
            {
                List<AccountMapDTO> acmds = new List<AccountMapDTO>();
                int i = 1;
                foreach (var data in accounMaps)
                {
                    AccountMapDTO amd = new AccountMapDTO();
                    amd.Description = data.Description;
                    amd.Account = data.Account;
                    amd.Id = data.Id;
                   
                    amd.ConsigneeUnit = data.ConsigneeUnit;
                    amd.Reference = data.Reference;
                    amd.Remark = data.Remark;
                    acmds.Add(amd);
                }
                return acmds;
            }
            catch (Exception ex) { return new List<AccountMapDTO>(); }
        }

        public async Task<IActionResult> createaccountmap([FromForm] GSLmodel createaccountmap)
        {
            AccountMapDTO accuntMap = new AccountMapDTO();
            List<string> acccode = new List<string>();
            ConsigneeDTO orgubit = new ConsigneeDTO();

            if (createaccountmap.accdescription == null || createaccountmap.accountmapdescription == null || createaccountmap.accountdescriptionorgunit == null)
            {
                return Json("Enter all fields");
            }
            else
            {

                if (createaccountmap.preferencecodeforupdateaccmap != 0 && createaccountmap.accountdescriptionorgunit != null)
                {
                    accuntMap.Id = createaccountmap.preferencecodeforupdateaccmap;
                    accuntMap.Description = createaccountmap.accdescription;
                    accuntMap.ConsigneeUnit = Convert.ToInt32(createaccountmap.accountdescriptionorgunit);
                    accuntMap.Account = createaccountmap.accountmapdescription;
                    accuntMap.Reference = createaccountmap.referencecode;
                    accuntMap.Remark = createaccountmap.remark;

                    var updateaedefn = await _sharedHelpers.Updateaccountmap(accuntMap);

                }
                else
                {
                    accuntMap.Id = 0;
                    accuntMap.Description = createaccountmap.accdescription;
                    accuntMap.Account = createaccountmap.accountmapdescription;
                    accuntMap.ConsigneeUnit = Convert.ToInt32(createaccountmap.accountdescriptionorgunit);
                    accuntMap.Reference = createaccountmap.referencecode;
                    accuntMap.Remark = createaccountmap.remark;
                    var cretaedefn = await _sharedHelpers.createaccountmap(accuntMap);
                }

            }
            return Json("Saved Successfully");
        }

        public async Task<IActionResult> Deleteacountmap(int id)
        {
            var accountmapList = await _sharedHelpers.DeleteAccountMapById(id);

            return Json("Deleted Successfully");
        }
        public async Task<IActionResult> GetAaccountmapBycode(int code)
        {
            var accountmapList = await _sharedHelpers.GetAccountMapById(code);
            var accountmap = accountmapList?.FirstOrDefault();

            return Json(new {id = accountmap.Id, accountmap.Description, accountmap.Account, accountmap.Reference, accountmap.Remark,  orgunitcode = accountmap.ConsigneeUnit });
        }
        public async Task<IActionResult> GetPreferenceBycode(int code)
        {
            var prefList = await _sharedHelpers.GetpreferenceById(code);
            var pref = prefList?.FirstOrDefault();
            var Objdefn = await _sharedHelpers.GetObjectstaByreference(code);


            return Json(new {id = pref?.Id, pref?.SystemConstant, pref?.Description, pref?.Index, pref?.Font, pref?.Value, Parent =  pref?.ParentId, pref?.IsActive, pref?.Color, pref?.Remark, obdefcode = Objdefn?.FirstOrDefault()?.ObjectStateDefinition });

        }
        public async Task<IActionResult> getFieldformatResult(int idd, int gslcode)
        {

            var getfiledformat = await _sharedHelpers.GetfieldformatBytypeandreference(idd, gslcode);
       
            var fieldformat = new GSLmodel() { FieldFormats = getfiledformat, fieldId = idd };
            return PartialView("artfieldformattableMap", fieldformat);
        }

        public async Task<IActionResult> GetGSLfiledformatBycode(int code)
        {
            var fldformatList = await _sharedHelpers.GetfieldformatbyId(code);
            var fldformat = fldformatList?.FirstOrDefault();

            return Json(new {id = fldformat.Id, fldformat.Index, fldformat.ObjectComponent, fldformat.Caption, fldformat.Width, fldformat.Color, fldformat.IsRequired, fldformat.Font, fldformat.Remark });
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
        public async Task<IActionResult> getCopydistributionarticle(int _idd, int gslcode)
        {
          
            var copydis = await _sharedHelpers.GetDistrubtionByRefeandTye(gslcode, _idd);

            var dis = new GSLmodel() { distributions = copydis };
            return PartialView("_artcopydistribution", dis);
        }
        public async Task<IActionResult> CreatecopyDistributionarticle(GSLmodel createartcopydist)
        {
            DistributionDTO distribution = new DistributionDTO();
            if (createartcopydist.copydisart == null || createartcopydist.destinatindisart == null)
            {
                return Json("Enter all fieldes");
            }
            else
            {
                if (createartcopydist.codeforupdatedistributionart != 0)
                {

                    distribution.Id = createartcopydist.codeforupdatedistributionart;
                    distribution.DestinationPointer = createartcopydist.VoucherSettingCodeforcopydistributionart;
                    distribution.Index = createartcopydist.copydisart;
                    distribution.Type = 1578;
                    distribution.Description = null;
                    distribution.SystemConstant = createartcopydist.VoucherSettingCodeforcopydistributionart;
                    distribution.Destination = createartcopydist.destinatindisart;
                    distribution.Remark = createartcopydist.printerdisart;
                    var updiss = await _sharedHelpers.UpdateDstribution(distribution);

                }
                else
                {

                    distribution.Id = 0;
                    distribution.DestinationPointer = createartcopydist.VoucherSettingCodeforcopydistributionart;
                    distribution.Index = createartcopydist.copydisart;
                    distribution.Type = 1578;
                    distribution.Description = null;
                    distribution.SystemConstant = createartcopydist.VoucherSettingCodeforcopydistributionart;
                    distribution.Destination = createartcopydist.destinatindisart;
                    distribution.Remark = createartcopydist.printerdisart;
                    var creadis = await _sharedHelpers.CreateDstribution(distribution);

                }
            }

            return Json("Saved Successfully");


        }

        [HttpPost]
        public async Task<IActionResult> deleteDistributioncopyartilce(GSLmodel deletedistr)
        {
            int deletedistribution = deletedistr.codefordeletedistributionart;
            var deldis = await _sharedHelpers.DeleteDistributionById(deletedistribution);
            return Json(new EmptyResult());

        }
        public async Task<IActionResult> getsynchronizarion(string Vouchercode)

        {
            var vouchercode = new GSLmodel() { Vouchercode = Vouchercode };

            return PartialView("_artsynchronization", vouchercode);

        }

        public async Task<IActionResult> CreatesynchroforDiscarticle(GSLmodel Createsych)
        {
            Distribution distItem = new Distribution();
            DistributionDTO distribution = new DistributionDTO();

            List<int> distributionunchecklist = new List<int>();
            List<int> distributionlist = new List<int>();
            var getdiss = await _sharedHelpers.GetDistrubtionByRefeandTye(Createsych.gslType, 1580);

            if (Createsych.branchcode != null)
            {
                if (getdiss.Count() > 1)
                {
                    for (int i = 0; i < getdiss.Count(); i++)
                    {

                        if (!(Createsych.branchcode.Contains(getdiss[i]?.DestinationPointer.ToString())))
                        {
                            distributionunchecklist.Add(getdiss.ElementAtOrDefault(i).Id);

                        }
                    }
                    for (int i = 0; i < getdiss.Count(); i++)
                    {

                        if (Createsych.branchcode.Contains(getdiss[i]?.DestinationPointer.ToString()))
                        {
                            distributionlist.Add(getdiss.ElementAtOrDefault(i).Id);

                        }
                    }

                }
                for (var item = 0; item < Createsych.branchcode.Count(); item++)
                {
                    var distributionList = Createsych.branchcode[item].Split("/");

                    distribution.Id = 0;
                    distribution.DestinationPointer = Createsych.gslTypecodesync;
                    distribution.Index = Convert.ToInt32(distributionList[2]);
                    distribution.Type = 1580;
                    distribution.Description = distributionList[1];
                    distribution.SystemConstant = 5;
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

        public async Task<IActionResult> GetDistributionBycode(int code)
        {
            var disList = await _sharedHelpers.GetDistrubtionById(code);
            var dis = disList?.FirstOrDefault();

            return Json(new { code = dis.Id, index = dis.Index, desc = dis.Description, destin = dis.Destination, remark = dis.Remark });
        }

        public async Task<IActionResult> getmachinedistributionarticle(int _idd, int gslcode)
        {

            var copydis = await _sharedHelpers.GetDistrubtionByRefeandTye(gslcode, _idd);

            var dis = new GSLmodel() { distributions = copydis };
            return PartialView("_artmachinedistribution", dis);

        }
        public async Task<IActionResult> deleteDistribution(int code)
        {
            var deldis = await _sharedHelpers.DeleteDistributionById(code);
            return Json("Saved Successfully");

        }
        public async Task<IActionResult> CreatemachineDistributionarticle(GSLmodel createdistribution)
        {

            DistributionDTO distribution = new DistributionDTO();
            if (createdistribution.descriptionmachart == null || createdistribution.devicemachart == null)
            {
                return Json("Enter all fieldes");
            }
            else
            {
                if (createdistribution.codeforupdatemachinedistributionart != 0)
                {
                    distribution.Id = createdistribution.codeforupdatemachinedistributionart;
                    distribution.SystemConstant = createdistribution.gslTypecodeCodeforMachineart;
                    distribution.Index = 0;
                    distribution.Type = 1579;
                    distribution.Description = createdistribution.descriptionmachart;
                    distribution.Destination = createdistribution.devicemachart;
                    distribution.Remark = createdistribution.remaarkmachart;
                    var updiss = await _sharedHelpers.UpdateDstribution(distribution);

                }
                else
                {
                    distribution.Id = 0;
                    distribution.SystemConstant = createdistribution.gslTypecodeCodeforMachineart;
                    distribution.Index = 0;
                    distribution.Type = 1579;
                    distribution.Description = createdistribution.descriptionmachart;
                    distribution.Destination = createdistribution.devicemachart;
                    distribution.Remark = createdistribution.remaarkmachart;
                    var creadiss = await _sharedHelpers.CreateDstribution(distribution);

                }
            }


            return Json("Saved Successfully");
        }

        [HttpPost]
        public async Task<IActionResult> createfieldformatarticle(GSLmodel createfieldformatarticle)
        {
            FieldFormatDTO fieldFormat = new FieldFormatDTO();
            var fildcode = createfieldformatarticle.code_feldformatart;
            if (createfieldformatarticle.fieldfrmatart == null  || createfieldformatarticle.colorfrmatart == null)
            {
                return Json("Enter all fields");
            }
            else
            {

                if (createfieldformatarticle.code_feldformatart != 0)
            {
                fieldFormat.Id = createfieldformatarticle.code_feldformatart;
                fieldFormat.Type = (int)createfieldformatarticle.filedformatcodeforheadersaveart;
                fieldFormat.ObjectComponent = createfieldformatarticle.fieldfrmatart;
                fieldFormat.Reference = createfieldformatarticle.gslTypeCodeforfieldformat;
                fieldFormat.Index = createfieldformatarticle.indexfrmatart;
                fieldFormat.Width = createfieldformatarticle.widthfrmatart;
                fieldFormat.Font = createfieldformatarticle.fontfrmatart;
                fieldFormat.Color = createfieldformatarticle.colorfrmatart;
                fieldFormat.Caption = createfieldformatarticle.captionfrmatart;
                fieldFormat.IsRequired = createfieldformatarticle.isrequiredfrmatart;
                fieldFormat.Remark = createfieldformatarticle.remarkfrmatart;
                var updateforma = await _sharedHelpers.Updatefieldformat(fieldFormat);

            }
            else
            {
                fieldFormat.Id = 0;
                fieldFormat.Type = createfieldformatarticle.filedformatcodeforheadersaveart;
                fieldFormat.ObjectComponent = createfieldformatarticle.fieldfrmatart;
                fieldFormat.Reference = createfieldformatarticle.gslTypeCodeforfieldformat;
                fieldFormat.Index = createfieldformatarticle.indexfrmatart;
                fieldFormat.Width = createfieldformatarticle.widthfrmatart;
                fieldFormat.Font = createfieldformatarticle.fontfrmatart;
                fieldFormat.Color = createfieldformatarticle.colorfrmatart;
                fieldFormat.Caption = createfieldformatarticle.captionfrmatart;
                fieldFormat.IsRequired = createfieldformatarticle.isrequiredfrmatart;
                fieldFormat.Remark = createfieldformatarticle.remarkfrmatart;
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
       
        public async Task<IActionResult> GetValueDefnByTypeandGslList(int type, int gslcode)
        {

            List<ValueFactorDefinitionDTO> factorDefinitions = new List<ValueFactorDefinitionDTO>();
            var valueDEfn = await _sharedHelpers.getValueDefnByTypeandGslList(type, Convert.ToInt32(gslcode));


            var valuefaDefn = new GSLmodel() { ValueFactors = valueDEfn };

            return PartialView("ValueDefnTableMap", valuefaDefn);

        }
        public async Task<IActionResult> GetValueFactBycode(int code)
        {

            var valuefaclist = await _sharedHelpers.getValueDefnById(code);
            var valuefac = valuefaclist?.FirstOrDefault();
            return Json(new { id = valuefac.Id, valuefac.Description, valuefac.IsPercent, valuefac.Value, valuefac.Remark });
        }
        public async Task<IActionResult> Createvaluefactordefnnition(GSLmodel createvalueDefn)
        {
            ValueFactorDefinitionDTO valueFactor = new ValueFactorDefinitionDTO();
            if (createvalueDefn.valuDescription == null)
            {
                return Json("Enter all fields");
            }
            else
            {
                if (createvalueDefn.codeforupdatevalueDefn != 0)
                {
                    valueFactor.Id = createvalueDefn.codeforupdatevalueDefn;
                    valueFactor.Type = createvalueDefn.valueDefncodeforsave;
                    valueFactor.Description = createvalueDefn.valuDescription;
                    valueFactor.IsPercent = createvalueDefn.valueIspercent;
                    valueFactor.Value = createvalueDefn.valueDefnvalue;
                    valueFactor.GslType = createvalueDefn.gslTypevalueDefn;
                    valueFactor.Remark = createvalueDefn.valueremark;

                    var updatevadefn = await _sharedHelpers.UpdateValueDefn(valueFactor);
                }
                else
                {
                    valueFactor.Id = 0;
                    valueFactor.Type = createvalueDefn.valueDefncodeforsave;
                    valueFactor.Description = createvalueDefn.valuDescription;
                    valueFactor.IsPercent = createvalueDefn.valueIspercent;
                    valueFactor.Value = createvalueDefn.valueDefnvalue;
                    valueFactor.GslType = createvalueDefn.gslTypevalueDefn;
                    valueFactor.Remark = createvalueDefn.valueremark;

                    var createvaluedefn = await _sharedHelpers.createValueDefn(valueFactor);
                }


            }

            return Json("Saved Successfully");
        }


        public async Task<IActionResult> deleteValueFacDefn(int code)
        {
            var deletefrmat = await _sharedHelpers.DeletevalueDEfnById(code);
            return Json("Deleted Successfully");

        }
        public async Task<IActionResult> DeleteAccountMap(int code)
        {
            var delaccou = await _sharedHelpers.DeletegslaccountmapById(code);

            return Json("Deleted Successfully");

        }
        public async Task<IActionResult> createPreferentilachild(GSLmodel createpreference)
        {
            var resultt = "";
            bool check = false;
             PreferenceDTO Crepreference = new PreferenceDTO();
            Preference Cpreference = new Preference();
            ObjectStateDTO Objectstate = new ObjectStateDTO();
            if (createpreference.prefDescription == null)
            {
                resultt = "Enter all Fields";
                check = false;
                return Json(new { check = check, result = resultt });
            }
            else
            {
                if (createpreference.prefrencecodeforupdate != 0)
                {
                    Crepreference.Id = createpreference.prefrencecodeforupdate;
                    Crepreference.SystemConstant = createpreference.getreferenceTypeForpref;
                    Crepreference.Description = createpreference.prefDescription;
                    Crepreference.Index = 1;
                    Crepreference.Font = createpreference.prefFont;
                    Crepreference.Value = createpreference.prefValue;
                    Crepreference.ParentId = createpreference.prefParent == 0 ? null : createpreference.prefParent;
                    Crepreference.IsActive = createpreference.prefIsactive;
                    Crepreference.Color = null;
                    Crepreference.Remark = null;

                    var updateprf = await _sharedHelpers.Updatepreference(Crepreference);

                    var obsta = await _sharedHelpers.GetObjectstaByreference(Crepreference.Id);
                    //var preparent = await _sharedHelpers.GetpreferenceByparent(Crepreference.code);
                    var preparent = await _sharedHelpers.GetpreferenceByparentandref(Crepreference.Id, Crepreference.SystemConstant);

                    var obcode = obsta?.FirstOrDefault()?.Id;
                    foreach (var item in obsta)
                    {
                        var deloobjesta = await _sharedHelpers.DeleteObjectStateByReference(item.Id);
                    }

                    if (createpreference?.prefParent != 0 || preparent?.Count() == 0)
                    {
                        if (obsta.Count() == 0)
                        {
                            if (createpreference.prefshoppingcate != 0)
                            {
                                Objectstate.Reference = createpreference.prefrencecodeforupdate;
                                Objectstate.ObjectStateDefinition = createpreference.prefshoppingcate;
                                Objectstate.Remark = null;
                                var creobsta = await _sharedHelpers.CreateObjectState(Objectstate);
                            }
                        }
                        else
                        {
                            if (createpreference.prefshoppingcate != 0)
                            {
                                Objectstate.Reference = Crepreference.Id;
                                Objectstate.ObjectStateDefinition = createpreference.prefshoppingcate;
                                Objectstate.Remark = null;
                                var creobsta = await _sharedHelpers.CreateObjectState(Objectstate);
                            }
                        }
                    }
                    resultt = "Updated Successfully";
                    check = true;
                    return Json(new { check = check, result = resultt });
                }
                else
                {
                    Crepreference.Id = 0;
                    Crepreference.SystemConstant = createpreference.getreferenceTypeForpref;
                    Crepreference.Description = createpreference.prefDescription;
                    Crepreference.Index = 1;
                    Crepreference.Font = createpreference.prefFont;
                    Crepreference.Value = createpreference.prefValue;
                    Crepreference.ParentId = createpreference.prefParent == 0? null : createpreference.prefParent;
                    Crepreference.IsActive = createpreference.prefIsactive;
                    Crepreference.Color = null;
                    Crepreference.Remark = null;
                    var creatprf = await _sharedHelpers.Createpreference(Crepreference);

                    var obsta = await _sharedHelpers.GetObjectstaByreference(Crepreference.Id);


                    var preparent = await _sharedHelpers.GetpreferenceByparentandref(Crepreference.Id, Crepreference.SystemConstant);


                    var obcode = obsta?.FirstOrDefault()?.Id;
                    foreach (var item in obsta)
                    {
                        var deloobjesta = await _sharedHelpers.DeleteObjectStateByReference(item.Id);
                    }

                    if (createpreference?.prefParent != 0)
                    {
                        if (obsta?.Count() == 0)
                        {
                            if (createpreference?.prefshoppingcate != 0)
                            {
                                Objectstate.Reference = creatprf.Id;
                                Objectstate.ObjectStateDefinition = createpreference.prefshoppingcate;
                                Objectstate.Remark = null;
                                var creobsta = await _sharedHelpers.CreateObjectState(Objectstate);
                            }
                        }
                        else
                        {
                            if (createpreference?.prefshoppingcate != 0)
                            {
                                Objectstate.Reference = creatprf.Id;
                                Objectstate.ObjectStateDefinition = createpreference.prefshoppingcate;
                                Objectstate.Remark = null;
                                var creobsta = await _sharedHelpers.CreateObjectState(Objectstate);
                            }
                        }
                    }
                    resultt = "Saved Successfully";
                    check = true;
                    return Json(new { check = check, result = resultt });
                }
            }
        }
        public async Task<IActionResult> saveAttachemnt(GSLmodel attmodel, string prefcode)
        {
            var getattchList = await _sharedHelpers.AttachmentByReference(0);
            var lastindex = getattchList?.LastOrDefault()?.Index;
            var index = ++lastindex;

            if (attmodel.gsl_preferentialcode == 0)
            {
                return Json(new { success = true, message = "Please selecte Description" });

            }
            else
            {
                if (attmodel.gsl_attachmentcode == null && attmodel.gsl_file != null)
                {
                    int attType = 0;
                    string ext = Path.GetExtension(attmodel.gsl_file.FileName);
                    if (ext == ".pdf")
                    {
                        attType = 1463;
                    }
                    else if (ext == ".jpg" || ext == ".png" || ext == ".jpeg")
                    {
                        attType = 1462;
                    }
                    else if (ext == ".mp4")
                    {
                        attType = 1467;

                    }
                    else
                    {
                        attType = 1466;
                    }
                    bool retrival = false;
                    string uniqueFileName = null;

                    //Redirect path based on category here...

                    uniqueFileName = Guid.NewGuid().ToString() + "_" + attmodel.gsl_file.FileName;
                    uniqueFileName = uniqueFileName.Replace(" ", string.Empty);
                    var filepath = Path.Combine(_env.WebRootPath, "images", uniqueFileName);
                    var stream = new FileStream(filepath, FileMode.Create);
                    await attmodel.gsl_file.CopyToAsync(stream);
                    stream.Close();

                    AttachmentDTO attachement = new AttachmentDTO();

                    attachement.Reference = Convert.ToInt32(attmodel.gsl_preferentialcode);
                    attachement.Category = 1448;
                    attachement.Description = attmodel.gsl_attachmentdescription.Replace(" ", string.Empty);
                    attachement.Url = uniqueFileName.Replace(" ", string.Empty);
                    attachement.Type = attType;
                    attachement.Index = 0;
                    attachement.Remark = ext;

                    await _sharedHelpers.CreateAttachment(attachement);

                    return Json(new { success = true, message = "Attachment save sucessfully" });
                }
                else
                {

                    if (attmodel.gsl_attachmentcode != 0)
                    {
                        int attType = 0;
                        string ext = Path.GetExtension(attmodel.gsl_file.FileName);
                        if (ext == ".pdf")
                        {
                            attType = 1463;
                        }
                        else if (ext == ".jpg" || ext == ".png" || ext == ".jpeg")
                        {
                            attType = 1462;
                        }
                        else if (ext == ".mp4")
                        {
                            attType = 1467;

                        }
                        else
                        {
                            attType = 1466;
                        }

                        string uniqueFileName = null;

                        //Redirect path based on category here...

                        uniqueFileName = Guid.NewGuid().ToString() + "_" + attmodel.gsl_file.FileName;
                        uniqueFileName = uniqueFileName.Replace(" ", string.Empty);
                        var filepath = Path.Combine(_env.WebRootPath, "images", uniqueFileName);
                        var stream = new FileStream(filepath, FileMode.Create);
                        await attmodel.gsl_file.CopyToAsync(stream);
                        stream.Close();

                        AttachmentDTO attachement = new AttachmentDTO();
                        attachement.Id = attmodel.gsl_attachmentcode;
                        attachement.Reference = 0;
                        attachement.Category = attmodel.gsl_attachmentcatagory;
                        attachement.Description = attmodel.gsl_attachmentdescription.Replace(" ", string.Empty);
                        attachement.Url = uniqueFileName.Replace(" ", string.Empty);
                        attachement.Type = attType;
                        attachement.Index =(int)index;
                        attachement.Remark = ext;

                        await _sharedHelpers.UpdateAttachment(attachement);
                        return Json(new { success = true, message = "Attachment  update sucessfully" }); ;
                    }
                    else
                    {
                        var result = await _sharedHelpers.GetattachmentbyId(attmodel.gsl_attachmentcode);
                        EmptyResult _singleton = new EmptyResult();
                        if (result == null)
                        {
                            return _singleton;
                        }
                        var filename = result?.FirstOrDefault()?.Url;
                        var contentRoot = Path.Combine(_env.WebRootPath, "images", filename);
                        FileInfo file = new FileInfo(contentRoot);

                        if (file.Exists)
                        {
                            file.Delete();
                            //}

                            string ext = Path.GetExtension(attmodel.gsl_file.FileName);
                            SystemConstantDTO look = GeneralBufferHolder.SystemConstants.Where(l => l.Value.Contains(ext, comparisonType: StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

                            string uniqueFileName = null;
                            uniqueFileName = Guid.NewGuid().ToString() + "_" + attmodel.gsl_file.FileName;
                            uniqueFileName = uniqueFileName.Replace(" ", string.Empty);
                            var filepath = Path.Combine(_env.WebRootPath, "images", uniqueFileName);
                            var stream = new FileStream(filepath, FileMode.Create);
                            await attmodel.gsl_file.CopyToAsync(stream);
                            stream.Close();

                            AttachmentDTO attachement = new AttachmentDTO();

                            attachement.Id = attmodel.gsl_attachmentcode;
                            attachement.Reference = 0;
                            attachement.Category = attmodel.gsl_attachmentcatagory;
                            attachement.Description = attmodel.gsl_attachmentdescription.Replace(" ", string.Empty);
                            attachement.Url = uniqueFileName.Replace(" ", string.Empty);
                            attachement.Type = look.Id;
                            attachement.Index = (int)index;
                            attachement.Remark = ext;

                            await _sharedHelpers.UpdateAttachment(attachement);
                            return Json(new { success = true, message = "Attachment update sucessfully" });

                        }
                        //string iDate = "05/05/2005";
                        //DateTime oDate = Convert.ToDateTime(iDate);

                    }
                }
            }
            return Json(new { success = true, message = "Attachment update sucessfully" });

        }
        [HttpPost]
        public async Task<IActionResult> DeletePreferencList(int PREFCODE)
        {
            int getcodeforaccount = PREFCODE;
            var result = "";
            var checkstatus = false;
            var preflist = await _sharedHelpers.GetpreferenceByparent(PREFCODE);
            var accountlist = await _sharedHelpers.GetAccountMapByrefrence(PREFCODE.ToString());

            var Isdeleted = false;
            if (preflist?.Count() >= 1)
            {
                result = "First delete child";
                checkstatus = false;
            }
            else
            {
                if (accountlist.Count() >= 1)
                {
                    foreach (var item in accountlist)
                    {
                        var delaccou = await _sharedHelpers.DeletegsltaxByreference(item.Id);
                        //var orgunitbyref = await _sharedHelpers.GetOrgUnitByreference(item.Id);

                        //var delorgun = await _sharedHelpers.deleteorgunitBycode(orgunitbyref.code);

                    }

                }

                Isdeleted = await _sharedHelpers.DeletePreference(getcodeforaccount);
                if (Isdeleted)
                {
                    var obsta = await _sharedHelpers.GetObjectstaByreference(getcodeforaccount);
                    foreach (var item in obsta)
                    {
                        var deloobjesta = await _sharedHelpers.DeleteObjectStateByReference(item.Id);
                    }


                }

                result = "delete successfully";
                checkstatus = true;
            }
            return Json(new { rowremove = checkstatus, result = result });

        }
        public async Task<IActionResult> Retrive_Gsl_Attachement(int attchcode)
        {

            var getattchList = await _sharedHelpers.AttachmentByReference(attchcode);
            var getattchListType = getattchList.LastOrDefault();


            return Json(new { data = getattchListType });
        }

        public async Task<IActionResult> DeleteAttachment(GSLmodel MODEL)
        {

            await _sharedHelpers.DeleteAttachment(MODEL.gsl_attachmentcode);

            return Json(new { });

        }


        public static List<GSLmodel> fieldformatmodel { get; set; }
        public IActionResult CreateDefaultFieldFormat(string attribute)
        {
            var attr = attribute;

            fieldformatmodel = new List<GSLmodel>
            {
               new GSLmodel (){Attribute ="voucherCode",Caption="Voucher Code",Width =110},
               new GSLmodel (){Attribute ="voucherDefinition",Caption="Voucher Definition Code",Width =50},
               new GSLmodel (){Attribute ="voucherDefinitionName",Caption="Voucher Definition Name",Width =150},
               new GSLmodel (){Attribute ="consigneeCode",Caption="Consignee Code",Width =100},
               new GSLmodel (){Attribute ="consigneeName",Caption="Consignee Name",Width =150},
               new GSLmodel (){Attribute ="consigneeTIN",Caption="Consignee TIN",Width =110},
               new GSLmodel (){Attribute ="consigneeTelephone",Caption="Consignee Telephone",Width =110},
               new GSLmodel (){Attribute ="childPreferenceCode",Caption="Child Preference Code",Width =110},
               new GSLmodel (){Attribute ="childPreferenceDesc",Caption="Child Preference Description",Width =150},
               new GSLmodel (){Attribute ="parentPreferenceCode",Caption="Parent Preference Code",Width =100},
               new GSLmodel (){Attribute ="parentPreferenceDesc",Caption="Parent Preference Description",Width =150},
               new GSLmodel (){Attribute ="IssuedDate",Caption="Issued Date",Width =110},
               new GSLmodel (){Attribute ="IsIssued",Caption="Is Issued",Width =50},
               new GSLmodel (){Attribute ="IsVoid",Caption="Is Void",Width =50},
               new GSLmodel (){Attribute ="periodCode",Caption="Period Code",Width =50},
               new GSLmodel (){Attribute ="periodName",Caption="Period Name",Width =100},
               new GSLmodel (){Attribute ="periodTypeCode",Caption="Period Type Code",Width =50},
               new GSLmodel (){Attribute ="periodTypeDescription",Caption="Period Type Description",Width =100},
               new GSLmodel (){Attribute ="periodStartDateTime",Caption="Period Start DateTime",Width =100},
               new GSLmodel (){Attribute ="periodEndDateTime",Caption="Period End DateTime",Width =100},
               new GSLmodel (){Attribute ="deviceCode",Caption="Device Code",Width =50},
               new GSLmodel (){Attribute ="deviceName",Caption="Device Name",Width =110},
               new GSLmodel (){Attribute ="userCode",Caption="User Code",Width =50},
               new GSLmodel (){Attribute ="userName",Caption="User Name",Width =110},
               new GSLmodel (){Attribute ="orgUnitDefCode",Caption="Organization Unit Definition Code",Width =110},
               new GSLmodel (){Attribute ="orgUnitDefDescription",Caption="Organization Unit Definition Description",Width =150},
               new GSLmodel (){Attribute ="subTotal",Caption="SubTotal",Width =105},
               new GSLmodel (){Attribute ="additionalCharge",Caption="Additional Charge",Width =105},
               new GSLmodel (){Attribute ="discount",Caption="Discount",Width =105},
               new GSLmodel (){Attribute ="grandTotal",Caption="Grand Total",Width =105},
               new GSLmodel (){Attribute ="lastObjectStateCode",Caption="Last Object State Code",Width =110},
               new GSLmodel (){Attribute ="lastObjectStateDescription",Caption="Last Object State Description",Width =150},
               new GSLmodel (){Attribute ="color",Caption="Color",Width =80},
               new GSLmodel (){Attribute ="voucherNote",Caption="Voucher Note",Width =200},
               new GSLmodel (){Attribute ="sourceStoreCode",Caption="Source Store Code",Width =110},
               new GSLmodel (){Attribute ="sourceStoreName",Caption="Source Store Name",Width =150},
               new GSLmodel (){Attribute ="destinationStoreCode",Caption="Destination Store Code",Width =110},
               new GSLmodel (){Attribute ="destinationStoreName",Caption="Destination Store Name",Width =150},
               new GSLmodel (){Attribute ="transactionType",Caption="Transaction Type",Width =110},
               new GSLmodel (){Attribute ="transactionTypeDescription",Caption="Transaction Type Description",Width =150},
               new GSLmodel (){Attribute ="remark",Caption="Voucher Remark",Width =200},
               new GSLmodel (){Attribute ="firstVouchExtCode",Caption="First Voucher Extension Code",Width =110},
               new GSLmodel (){Attribute ="firstVouchExtDescription",Caption="First Voucher Extension Description",Width =150},
               new GSLmodel (){Attribute ="firstVouchExtValue",Caption="First Voucher Extension Value",Width =110},
               new GSLmodel (){Attribute ="secondVouchExtCode",Caption="Second Voucher Extension Code",Width =110},
               new GSLmodel (){Attribute ="secondVouchExtDescription",Caption="Second Voucher Extension Description",Width =150},
               new GSLmodel (){Attribute ="secondVouchExtValue",Caption="Second Voucher Extension Value",Width =110},
               new GSLmodel (){Attribute ="thirdVouchExtCode",Caption="Third Voucher Extension Code",Width =110},
               new GSLmodel (){Attribute ="thirdVouchExtDescription",Caption="Third Voucher Extension Description",Width =150},
               new GSLmodel (){Attribute ="thirdVouchExtValue",Caption="Third Voucher Extension Value",Width =110},
               new GSLmodel (){Attribute ="fourthVouchExtCode",Caption="Fourth Voucher Extension Code",Width =110},
               new GSLmodel (){Attribute ="fourthVouchExtDescription",Caption="Fourth Voucher Extension Description",Width =150},
               new GSLmodel (){Attribute ="fourthVouchExtValue",Caption="Fourth Voucher Extension Value",Width =110},
               new GSLmodel (){Attribute ="VATtaxableAmount",Caption="VAT Taxable Amount",Width =105},
               new GSLmodel (){Attribute ="VATtaxAmount",Caption="VAT Tax Amount",Width =105},
               new GSLmodel (){Attribute ="TOT1taxableAmount",Caption="TOT1 Taxable Amount",Width =105},
               new GSLmodel (){Attribute ="TOT1taxAmount",Caption="TOT1 Tax Amount",Width =105},
               new GSLmodel (){Attribute ="TOT2taxableAmount",Caption="TOT2 Taxable Amount",Width =105},
               new GSLmodel (){Attribute ="TOT2taxAmount",Caption="TOT2 Tax Amount",Width =105},
               new GSLmodel (){Attribute ="NonTaxableAmount",Caption="Non Taxable Amount",Width =105},
               new GSLmodel (){Attribute ="WithholdingTaxableAmount",Caption="Withholding Taxable Amount",Width =105},
               new GSLmodel (){Attribute ="IncomeTaxtaxableAmount",Caption="Income Taxable Amount",Width =105},
               new GSLmodel (){Attribute ="IncomeTaxAmount",Caption="Income Tax Amount",Width =105},
               new GSLmodel (){Attribute ="EmployeePensionTaxableAmount",Caption="Employee Pension Taxable Amount",Width =105},
               new GSLmodel (){Attribute ="EmployeePensionTaxAmount",Caption="Employee Pension Tax Amount",Width =105},
               new GSLmodel (){Attribute ="CompanyPensionTaxableAmount",Caption="Company Pension Taxable Amount",Width =105},
               new GSLmodel (){Attribute ="CompanyPensionTaxAmount",Caption="Company Pension Tax Amount",Width =105},
               new GSLmodel (){Attribute ="WithholdingTaxAmount",Caption="Withholding Tax Amount",Width =105},
               new GSLmodel (){Attribute ="backwardReferences",Caption="Backward References",Width =110},
               new GSLmodel (){Attribute ="forwardReferences",Caption="Forward References",Width =110},
               new GSLmodel (){Attribute ="InternalReference",Caption="Internal Reference",Width =110},
               new GSLmodel (){Attribute ="firstOtherConsReqGSLCode",Caption="First Other Consignee ReqGSL Code",Width =110},
               new GSLmodel (){Attribute ="firstOtherConsigneeCode",Caption="First Other Consignee Code",Width =110},
               new GSLmodel (){Attribute ="firstOtherConsigneeName",Caption="First Other Consignee Name",Width =150},
               new GSLmodel (){Attribute ="secondOtherConsReqGSLCode",Caption="Second Other Consignee ReqGSL Code",Width =110},
               new GSLmodel (){Attribute ="secondOtherConsigneeCode",Caption="Second Other Consignee Code",Width =110},
               new GSLmodel (){Attribute ="secondOtherConsigneeName",Caption="Second Other Consignee Name",Width =150},
               new GSLmodel (){Attribute ="thirdOtherConsReqGSLCode",Caption="Third Other Consignee ReqGSL Code",Width =110},
               new GSLmodel (){Attribute ="thirdOtherConsigneeCode",Caption="Third Other Consignee Code",Width =110},
               new GSLmodel (){Attribute ="thirdOtherConsigneeName",Caption="Third Other Consignee Name",Width =150},
               new GSLmodel (){Attribute ="fsNo",Caption="FS No.",Width =110},
               new GSLmodel (){Attribute ="mrsNo",Caption="MRC No.",Width =110},
               new GSLmodel (){Attribute ="waiterCode",Caption="Waiter Code",Width =110},
               new GSLmodel (){Attribute ="waiterName",Caption="Waiter Name",Width =150},
               new GSLmodel (){Attribute ="table",Caption="Table",Width =110},
               new GSLmodel (){Attribute ="cover",Caption="Cover",Width =110},
               new GSLmodel (){Attribute ="paymentMethodCode",Caption="Payment Method Code",Width =110},
               new GSLmodel (){Attribute ="paymentMethodDescription",Caption="Payment Method Description",Width =150},
               new GSLmodel (){Attribute ="paymentProcesserCode",Caption="Payment Processer Code",Width =110},
               new GSLmodel (){Attribute ="paymentProcesserDescription",Caption="Payment Processer Description",Width =150},
               new GSLmodel (){Attribute ="bankCode",Caption="Bank Code",Width =110},
               new GSLmodel (){Attribute ="bankName",Caption="Bank Name",Width =150},
               new GSLmodel (){Attribute ="nonCashPaymentIssuedDate",Caption="Non-Cash Payment Issued Date",Width =110},
               new GSLmodel (){Attribute ="nonCashPaymentMaturityDate",Caption="Non-Cash Payment Maturity Date",Width =110},
               new GSLmodel (){Attribute ="nonCashPaymentNumber",Caption="Non-Cash Payment Number",Width =110},
               new GSLmodel (){Attribute ="nonCashPaymentRate",Caption="Non-Cash Payment Rate",Width =110},
               new GSLmodel (){Attribute ="nonCashPaymentAmount",Caption="Non-Cash Payment Amount",Width =110},
               new GSLmodel (){Attribute ="nonCashPaymentCurrencyCode",Caption="Non-Cash Payment Currency Code",Width =110},
               new GSLmodel (){Attribute ="nonCashPaymentCurrencyDescription",Caption="Non-Cash Payment Currency Description",Width =150},
               new GSLmodel (){Attribute ="nonCashPaymentCurrencyAbbreviation",Caption="Non-Cash Payment Currency Abbreviation",Width =110},
               new GSLmodel (){Attribute ="lineItemCount",Caption="Line Item Count",Width =80},
               new GSLmodel (){Attribute ="lineItemQtySum",Caption="Line Item Quantity Sum",Width =110},
               new GSLmodel (){Attribute ="AllLineItemArticleCode",Caption="All Line Item Article Code",Width =200},
               new GSLmodel (){Attribute ="AllLineItemArticleName",Caption="All Line Item Article Name",Width =220},
               new GSLmodel (){Attribute ="voucherOrgUnitCode",Caption="Voucher Organization Unit Code",Width =110},
               new GSLmodel (){Attribute ="voucherOrgUnitDescription",Caption="Voucher Organization Unit Description",Width =150},
               new GSLmodel (){Attribute ="prodDate",Caption="Production Date",Width =110},
               new GSLmodel (){Attribute ="expiryDate",Caption="Expiry Date",Width =110},
               new GSLmodel (){Attribute ="voucherLifeSpan",Caption="Voucher LifeSpan",Width =110},
               new GSLmodel (){Attribute ="firstArticleCode",Caption="First Article Code",Width =110},
               new GSLmodel (){Attribute ="firstArticleName",Caption="First Article Name",Width =150},
               new GSLmodel (){Attribute ="lineItemProdDate",Caption="Line Item Production Date",Width =110},
               new GSLmodel (){Attribute ="lineitemExpiryDate",Caption="Line Item Expiry Date",Width =110},
               new GSLmodel (){Attribute ="lineItemLifeSpan",Caption="Line Item Life Span",Width =110},
               new GSLmodel (){Attribute ="subSystem",Caption="Sub System",Width =150},
               new GSLmodel (){Attribute ="lastActivity",Caption="lastActivity",Width =110},
               new GSLmodel (){Attribute ="quantitySum",Caption="Quantity Sum",Width =110},
               new GSLmodel (){Attribute ="PersonCode",Caption="Person Code",Width =110},
               new GSLmodel (){Attribute ="title",Caption="Title",Width =110},
               new GSLmodel (){Attribute ="TitleDescription",Caption="Title Description",Width =110},
               new GSLmodel (){Attribute ="firstName",Caption="First Name",Width =110},
               new GSLmodel (){Attribute ="middleName",Caption="Middle Name",Width =110},
               new GSLmodel (){Attribute ="lastName",Caption="Last Name",Width =110},
               new GSLmodel (){Attribute ="name",Caption="Name",Width =200},
               new GSLmodel (){Attribute ="GslType",Caption="GSL Type",Width =50},
               new GSLmodel (){Attribute ="genderCode",Caption="Gender Code",Width =110},
               new GSLmodel (){Attribute ="genderDesc",Caption="Gender Description",Width =110},
               new GSLmodel (){Attribute ="nationality",Caption="Nationality",Width =110},
               new GSLmodel (){Attribute ="nationalityName",Caption="Nationality Name",Width =110},
               new GSLmodel (){Attribute ="DOB",Caption="Date of Birth",Width =110},
               new GSLmodel (){Attribute ="age",Caption="Age",Width =50},
               new GSLmodel (){Attribute ="isActive",Caption="Is Active",Width =50},
               new GSLmodel (){Attribute ="TIN",Caption="TIN",Width =110},
               new GSLmodel (){Attribute ="Telephone",Caption="Telephone",Width =110},
               new GSLmodel (){Attribute ="taxTypeCode",Caption="Tax Type Code",Width =110},
               new GSLmodel (){Attribute ="taxTypeDesc",Caption="Tax Type Description",Width =110},
               new GSLmodel (){Attribute ="taxTypeAmount",Caption="Tax Type Amount",Width =110},
               new GSLmodel (){Attribute ="preferenceCode",Caption="Preference Code",Width =110},
               new GSLmodel (){Attribute ="preferenceDesc",Caption="Preference Description",Width =150},
               new GSLmodel (){Attribute ="prefParentCode",Caption="Preference Parent Code",Width =110},
               new GSLmodel (){Attribute ="prefParentDesc",Caption="Preference Parent Description",Width =150},
               new GSLmodel (){Attribute ="objectStateDefnCode",Caption="Object State Definition Code",Width =110},
               new GSLmodel (){Attribute ="objectStateDefnDesc",Caption="Object State Definition Description",Width =150},
               new GSLmodel (){Attribute ="ObjectStateColor",Caption="Object State Color",Width =110},
               new GSLmodel (){Attribute ="objectStateFont",Caption="Object State Font",Width =110},
               new GSLmodel (){Attribute ="cityAddress",Caption="City Address",Width =110},
               new GSLmodel (){Attribute ="countryAddress",Caption="Country Address",Width =110},
               new GSLmodel (){Attribute ="departmentCode",Caption="Department Code",Width =110},
               new GSLmodel (){Attribute ="departmentDesc",Caption="Department Description",Width =150},
               new GSLmodel (){Attribute ="activityDef",Caption="Activity Definition",Width =110},
               new GSLmodel (){Attribute ="startTimStamp",Caption="Start Time Date",Width =110},
               new GSLmodel (){Attribute ="endTimStamp",Caption="End Time Date",Width =110},
               new GSLmodel (){Attribute ="OrganizationCode",Caption="Organization Code",Width =110},
               new GSLmodel (){Attribute ="tradeName",Caption="Trade Name",Width =150},
               new GSLmodel (){Attribute ="brandName",Caption="Brand Name",Width =150},
               new GSLmodel (){Attribute ="name",Caption="Name",Width =200},
               new GSLmodel (){Attribute ="businessType",Caption="Business Type",Width =110},
               new GSLmodel (){Attribute ="businessTypeName",Caption="Business Type Name",Width =150},
               new GSLmodel (){Attribute ="taxType",Caption="Tax Type Code",Width =110},
               new GSLmodel (){Attribute ="PhoneNumber",Caption="Phone Number",Width =110},
               new GSLmodel (){Attribute ="contactPerson",Caption="Contact Person",Width =150},
               new GSLmodel (){Attribute ="assignedPerson",Caption="Assigned Person",Width =150},
               new GSLmodel (){Attribute ="representative",Caption="Representative",Width =150},
               new GSLmodel (){Attribute ="owners",Caption="Owners",Width =150},
               new GSLmodel (){Attribute ="OrgUnitCount",Caption="Organization Unit Count",Width =110},
               new GSLmodel (){Attribute ="NoOfEmployee",Caption="No Of Employee",Width =110},
               new GSLmodel (){Attribute ="NoOfRepresentative",Caption="No Of Representative",Width =110},
               new GSLmodel (){Attribute ="NoOfAssignedPrsn",Caption="No Of Assigned Person",Width =110},
               new GSLmodel (){Attribute ="NoOfOwner",Caption="No Of Owner",Width =110},
               new GSLmodel (){Attribute ="ArticleCode",Caption="Article Code",Width =110},
               new GSLmodel (){Attribute ="ArticleName",Caption="Article Name",Width =150},
               new GSLmodel (){Attribute ="preferenceDesc",Caption="Preference Description",Width =110},
               new GSLmodel (){Attribute ="prefParentCode",Caption="Preference Parent Code",Width =110},
               new GSLmodel (){Attribute ="prefParentDesc",Caption="Preference Parent Description",Width =150},
               new GSLmodel (){Attribute ="uomCode",Caption="Unit of Measurement Code",Width =110},
               new GSLmodel (){Attribute ="uomDesc",Caption="Unit of Measurement Description",Width =150},
               new GSLmodel (){Attribute ="length",Caption="Length",Width =110},
               new GSLmodel (){Attribute ="width",Caption="Width",Width =110},
               new GSLmodel (){Attribute ="height",Caption="Height",Width =110},
               new GSLmodel (){Attribute ="weight",Caption="Weight",Width =110},
               new GSLmodel (){Attribute ="brand",Caption="Brand",Width =110},
               new GSLmodel (){Attribute ="generic",Caption="Generic",Width =110},
               new GSLmodel (){Attribute ="model",Caption="Model",Width =110},
               new GSLmodel (){Attribute ="size",Caption="Size",Width =110},
               new GSLmodel (){Attribute ="productColor",Caption="Product Color",Width =110},
               new GSLmodel (){Attribute ="country",Caption="Country",Width =110},
               new GSLmodel (){Attribute ="countryName",Caption="Country Name",Width =150},
               new GSLmodel (){Attribute ="manufacturer",Caption="Manufacturer",Width =150},
               new GSLmodel (){Attribute ="defaultPriceDescription",Caption="Default Price Description",Width =150},
               new GSLmodel (){Attribute ="defaultPriceValue",Caption="Default Price Value",Width =110},
               new GSLmodel (){Attribute ="taxTypeCode",Caption="Tax Type Code",Width =110},
               new GSLmodel (){Attribute ="taxTypeDesc",Caption="Tax Type Description",Width =110},
               new GSLmodel (){Attribute ="taxTypeValue",Caption="Tax Type Value",Width =110},
               new GSLmodel (){Attribute ="lifeTimeValue",Caption="Life Time Value",Width =110},
               new GSLmodel (){Attribute ="lifetimeFactorCode",Caption="Life Time Factor Code",Width =110},
               new GSLmodel (){Attribute ="lifetimeFactorDesc",Caption="Life Time Factor Description",Width =150},
               new GSLmodel (){Attribute ="optionCodesValue",Caption="Option Codes Value",Width =110},

            };
            var filedformat = fieldformatmodel.Where(x => x.Attribute == attribute).Select(y => new { captionn = y.Caption, widthh = y.Width }).FirstOrDefault();
            return Json(filedformat);

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

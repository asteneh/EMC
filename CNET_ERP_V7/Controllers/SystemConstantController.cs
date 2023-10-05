using CNET_ERP_V7.Common.AuthNavigation;
using CNET_ERP_V7.Common.Company;
using CNET_ERP_V7.Models;
using CNET_ERP_V7.Models.SubSytsemModel;
using CNET_V7_Domain;
using CNET_V7_Domain.Domain.SettingSchema;
using CNET_V7_Entities.DataModels;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;
using System.Drawing;

namespace CNET_ERP_V7.Controllers
{
    public class SystemConstantController : Controller
    {
        private AuthenticationManager _authenticationManager;
        private readonly HttpClient _httpClient;
        private readonly SharedHelpers _sharedHelpers;
        private NavigatorManager _navigationManager;
        public List<ConfigurationDTO> Config = new List<ConfigurationDTO>();
        public SystemConstantController(AuthenticationManager authenticationManager,
           IHttpClientFactory httpClientFactory,
           SharedHelpers sharedHelpers, NavigatorManager navigationManager)
        {
            _authenticationManager = authenticationManager;
            _httpClient = httpClientFactory.CreateClient("mainclient");
            _sharedHelpers = sharedHelpers;
            _navigationManager = navigationManager;
        }
        public async Task<IActionResult>  List()
        {
            var authUser = await _authenticationManager.GetAuthenticatedUser();

            if (authUser == null)
            {
                return RedirectToAction("Login", "Login");
            }
            //GlobalParamsModel unorderedNavList = await _sharedHelpers.GetGlobalParams(authUser);
            //var rootNode = await _navigationManager.PrepareNavigationList(unorderedNavList.navigatorList);
            //var subsystem = rootNode.Select(x => x.ChildNodes).ToList();
            //var subsystem2 = subsystem.Select(y => y.ChildNodes).ToList();
            //string desc = subsystem.Where(z => z.Url?.Split('/')[3] == id.ToString())?.FirstOrDefault()?.Title;

            return View(new systemSettingModel
            {
                
            });
        }

       public async Task<IActionResult> GetSystemConstantList(string name)
        {
            systemSettingModel searchModel = new systemSettingModel();
            searchModel.name = name;
            return PartialView("_systemConstant", searchModel);
        }
        public async Task<IActionResult> GetDetailData(int code,string cate)
        {
            systemSettingModel searchModel = new systemSettingModel();
            searchModel.name = code.ToString();
            searchModel.category = cate;
            return PartialView("_systemConstantDetail", searchModel);
        }
        public async Task<IActionResult> GetGslTypeBycode(string Code)
        {
            var gslListtype = await _sharedHelpers.GetsystemConstantById(Convert.ToInt32(Code));
            var gslList = gslListtype?.FirstOrDefault();


            return Json(new { code = gslList.Id, description = gslList.Description, category = gslList.ParentId, iasctive = gslList.IsActive, remark = gslList.Remark });
        }

        public async Task<IActionResult> SavegslType(systemSettingModel gslTypemodel)
        {
            SystemConstantDTO gSLType = new SystemConstantDTO();

            if (gslTypemodel.gsl_description == null)
            {
                return Json("Enter All Fildes");
            }
            else
            {
                if (gslTypemodel.gsl_code != null)
                {
                    var trn = await _sharedHelpers.GetsystemConstantById(Convert.ToInt32(gslTypemodel.gsl_code));
                    gSLType.Id = Convert.ToInt32(gslTypemodel.gsl_code);
                    gSLType.Description = gslTypemodel.gsl_description;
                    gSLType.ParentId = gslTypemodel.gsl_category;
                    gSLType.IsActive = gslTypemodel.gsl_isactive;
                    gSLType.Type = trn.FirstOrDefault().Type;
                    gSLType.Category = trn.FirstOrDefault().Category;
                    gSLType.Remark = gslTypemodel.gsl_remark;
                    var updategsl = await _sharedHelpers.UpdateSystemConstant(gSLType);
                }
                else
                {
                    gSLType.Id = 0;
                    gSLType.Description = gslTypemodel.gsl_description;
                    gSLType.ParentId = gslTypemodel.gsl_category;
                    gSLType.IsActive = gslTypemodel.gsl_isactive;
                    gSLType.Remark = gslTypemodel.gsl_remark;
                    var creategsl = await _sharedHelpers.CreateSystemConstant(gSLType);
                }
            }
            return Json("Saved Successfully");
        }
        public async Task<IActionResult> GetvouvherdefinitionBycode(string code)
        {
            var voucherdefList = await _sharedHelpers.GetsystemConstantById(Convert.ToInt32(code));
            var voucherdef = voucherdefList?.FirstOrDefault();

            return Json(new { code = voucherdef?.Id, name = voucherdef?.Description, type = voucherdef?.ParentId, iasctive = voucherdef?.IsActive, remark = voucherdef?.Remark });

        }

        public async Task<IActionResult> SavevoucherDefn(systemSettingModel voucherDefn)
        {
            SystemConstantDTO voucherDefinition = new SystemConstantDTO();

            if (voucherDefn.vochr_name == null)
            {
                return Json("Enter All Fields ");
            }
            else
            {
                if (voucherDefn.vochr_code != null)
                {
                    var trn = await _sharedHelpers.GetsystemConstantById(voucherDefn.vochr_code);

                    voucherDefinition.Id = Convert.ToInt32(voucherDefn.vochr_code);
                    voucherDefinition.ParentId = voucherDefn.parentId;
                    voucherDefinition.Description = voucherDefn.vochr_name;
                    voucherDefinition.IsActive = voucherDefn.vochr_isactive;
                    voucherDefinition.Type = "Transaction";
                    voucherDefinition.Category = trn?.FirstOrDefault()?.Category;
                    voucherDefinition.Remark = voucherDefn.vochr_remark;
                    var updatevoucher = await _sharedHelpers.UpdateSystemConstant(voucherDefinition);


                    if (updatevoucher?.Id != 0)
                    {
                        ReportDTO reportup = new ReportDTO();
                        var report = await _sharedHelpers.GetreportList();
                        var reportList = report?.Where(r => r.Reference == voucherDefn.vochr_code).ToList();
                        if (reportList != null && reportList.Count() > 0)
                        {
                            foreach (var item in reportList)
                            {

                                reportup.Id = item.Id;
                                reportup.Index = item.Index;
                                reportup.Subsystem = item.Subsystem;
                                reportup.Reference = item.Reference;
                                reportup.DefaultName = item.DefaultName;
                                reportup.CustomName = voucherDefn.vochr_name;
                                reportup.IsActive = item.IsActive;
                                reportup.Url = item.Url;
                                reportup.Remark = item.Remark;


                                var updatereport = await _sharedHelpers.UpdateReport(reportup);
                            }
                        }

                    }

                }
                else
                {
                    voucherDefinition.Id = 0;
                    voucherDefinition.ParentId = voucherDefn.parentId;
                    voucherDefinition.Description = voucherDefn.vochr_name;
                    voucherDefinition.IsActive = voucherDefn.vochr_isactive;
                    voucherDefinition.Type = "Transaction";
                    voucherDefinition.Remark = voucherDefn.vochr_remark;
                    var creatvoucherdef = await _sharedHelpers.CreateSystemConstant(voucherDefinition);

                }
            }

            return Json("Save Successfully");
        }
        public async Task<IActionResult> GetpreferecneBycode(int code)
        {
            var mprefLsit = await _sharedHelpers.GetsystemConstantById(code);
            var mpref = mprefLsit?.FirstOrDefault();

            return Json(new { code = mpref.Id, desc = mpref.Description, parent = mpref.ParentId, isactive = mpref.IsActive, remark = mpref.Remark });
        }
        public async Task<IActionResult> SavedeviceType(systemSettingModel listmodel)
        {
            var resultset = "";
            if (listmodel.dev_description == null)
            {
                resultset = "Enter All Fileds";
            }
            else
            {
                var trn = await _sharedHelpers.GetsystemConstantById(Convert.ToInt32(listmodel.dev_code));
                SystemConstantDTO prefr = new SystemConstantDTO();
                prefr.Id = listmodel.dev_code;
                prefr.ParentId = listmodel.dev_parent;
                prefr.Description = listmodel.dev_description;
                prefr.IsActive = listmodel.dev_isactive;
                prefr.Type = trn.FirstOrDefault().Type;
                prefr.Remark = listmodel.dev_remark;
                var updatepref = await _sharedHelpers.UpdateSystemConstant(prefr);

                if (updatepref.ToString() != null)
                {
                    resultset = "Saved Successfully";
                }
                else
                {
                    resultset = "Not Saved";
                }
            }

            return Json(new { result = resultset });
        }
        //public async Task<IActionResult> GetObjectstateDefnBycode(int code)
        //{
        //    var ObjecetDefnList = await _sharedHelpers.GetObjectStateDefnByID(code);
        //    var ObjecetDefn = ObjecetDefnList?.FirstOrDefault();

        //    return Json(new { id = ObjecetDefn?.Id, ObjecetDefn?.Type, ObjecetDefn?.Description, ObjecetDefn?.Color, ObjecetDefn?.Font, ObjecetDefn?.Remark });
        //}
        //public async Task<IActionResult> SaveobjectDefn(systemSettingModel objectDefn)
        //{

        //    ObjectStateDefinitionDTO objectState = new ObjectStateDefinitionDTO();
        //    int ObjectstateCode = 0;
        //    var objectstatedefn = await _sharedHelpers.GetObjectStateDefn();

        //    if (objectDefn.object_description == null)
        //    {
        //        return Json("Enter All Fileds");
        //    }
        //    else
        //    {
        //        if (objectDefn.object_code != 0)
        //        {
        //            var value = objectstatedefn?.Where(l => l.Type == objectDefn.object_type && l.Description == objectDefn.object_description && l.Color == objectDefn.object_color && l.Font == objectDefn.object_font).ToList();

        //            if (value.Count() == 0)
        //            {
        //                objectState.Id = objectDefn.object_code;
        //                objectState.Type = objectDefn.object_type;
        //                objectState.Description = objectDefn.object_description;
        //                objectState.Color = objectDefn.object_color;
        //                objectState.Font = objectDefn.object_font;
        //                objectState.Remark = objectDefn.object_remark;
        //                //await _sharedHelpers.UpdateObjectStateDefinition(objectState);
        //                return Json("Save Successfully");
        //            }
        //            else
        //            {
        //                if (objectDefn.object_code == value.FirstOrDefault()?.Id)
        //                {
        //                    objectState.Id = ObjectstateCode;
        //                    objectState.Type = objectDefn.object_type;
        //                    objectState.Description = objectDefn.object_description;
        //                    objectState.Color = objectDefn.object_color;
        //                    objectState.Font = objectDefn.object_font;
        //                    objectState.Remark = objectDefn.object_remark;
        //                    //await _sharedHelpers.UpdateObjectStateDefinition(objectState);
        //                    return Json("Save Successfully");
        //                }
        //                else
        //                {
        //                    return Json("This Object State Definition Is Exist");
        //                }

        //            }

        //        }

        //        else
        //        {
        //            var value = objectstatedefn?.Where(l => l.Type == objectDefn.object_type && l.Description == objectDefn.object_description && l.Color == objectDefn.object_color && l.Font == objectDefn.object_font).ToList();

        //            if (value.Count() == 0)
        //            {
        //                objectState.Id = 0;
        //                objectState.Type = objectDefn.object_type;
        //                objectState.Description = objectDefn.object_description;
        //                objectState.Color = objectDefn.object_color;
        //                objectState.Font = objectDefn.object_font;
        //                objectState.Remark = objectDefn.object_remark;
        //                //await _sharedHelpers.CreateObjectStateDefinition(objectState);
        //                return Json("Save Successfully");
        //            }
        //            else
        //            {
        //                return Json("This Object State Definition Is Exist");
        //            }
        //        }

        //    }
        //}


    }
}



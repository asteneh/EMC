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
using CNET_V7_Entities.DataModels;
using Cnetv7BufferHolder;

namespace CNET_ERP_V7.Controllers
{
    public class IdDefinitionController : Controller
    {
        private AuthenticationManager _authenticationManager;
        private readonly HttpClient _httpClient;
        private readonly SharedHelpers _sharedHelpers;
        public List<ConfigurationDTO> Config = new List<ConfigurationDTO>();
        #region Ctor
        public IdDefinitionController(AuthenticationManager authenticationManager,
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
            return View(new IdDefinitionModel
            {
              

            });

        }
        public async Task<IActionResult> GetIdDefinitionList(string type)
         {
            var idewithAssigned = await _sharedHelpers.GetIdDefination(type);
            var _idDefn = new IdDefinitionModel() { dTO2s = idewithAssigned };
            return PartialView("_idDefinition", _idDefn);
        }
        public async Task<IActionResult> SaveiddefinitionType(IdDefinitionModel idDefinition)
        {

            IddefinitionDTO iddef = new IddefinitionDTO();
            var resultset = "";
            var checksetset = false;
            if (idDefinition.iden_description == null)
            {
                resultset = "Enter All Fileds";
                checksetset = false;
                return Json(new { check = checksetset, result = resultset });
            }
            else
            {
                if ((idDefinition.iden_prefixseparator != null && idDefinition.iden_prefixseparator.Length > 1) || (idDefinition.iden_suffixseparator != null && idDefinition.iden_suffixseparator.Length > 1))
                {
                    resultset = "Prefix Separater and Suffix Separater value should not be Grater than one characters";
                    checksetset = false;
                    return Json(new { check = checksetset, result = resultset });
                }
                else if (int.Parse(idDefinition.iden_length) < 2)
                {
                    resultset = "Running value should not be less than two characters";
                    checksetset = false;
                    return Json(new { check = checksetset, result = resultset });
                }
                else
                {
                    if (idDefinition.iden_code != 0)
                    {

                        iddef.Id = idDefinition.iden_code;
                        iddef.SystemConstant = idDefinition.iden_component;
                        iddef.Description = idDefinition.iden_description;
                        iddef.Prefix = idDefinition.iden_prefix == null ? "" : idDefinition.iden_prefix;
                        iddef.PrefixSeparator = idDefinition.iden_prefixseparator == null ? "" : idDefinition.iden_prefixseparator;
                        iddef.SuffixSeparator = idDefinition.iden_suffixseparator == null ? "" : idDefinition.iden_suffixseparator;
                        iddef.Length = int.Parse(idDefinition.iden_length);
                        iddef.Suffix = idDefinition.iden_suffix == null ? "" : idDefinition.iden_suffix;
                        iddef.Remark = idDefinition.iden_remark;
                        var updateiden = await _sharedHelpers.UpdateIdDefinition(iddef);

                        resultset = "Saved Successfully";
                        checksetset = true;
                        return Json(new { check = checksetset, result = resultset });
                    }
                    else
                    {
                        var idenseting = await _sharedHelpers.GetIddefinition();

                        var prfx = idDefinition.iden_prefix == null ? "" : idDefinition.iden_prefix;
                        var prfxseparator = idDefinition.iden_prefixseparator == null ? "" : idDefinition.iden_prefixseparator;
                        var sufixseparator = idDefinition.iden_suffixseparator == null ? "" : idDefinition.iden_suffixseparator;
                        var sufix = idDefinition.iden_suffix == null ? "" : idDefinition.iden_suffix;
                        var duplicated = idenseting.Where(x => x.Description.ToLower() == idDefinition.iden_description.ToLower() && x.PrefixSeparator.ToLower() == prfxseparator.ToLower() && x.Prefix.ToLower() == prfx.ToLower() && x.SuffixSeparator.ToLower() == sufixseparator.ToLower() && x.Suffix.ToLower() == sufix.ToLower()).FirstOrDefault();

                        if (duplicated != null)
                        {
                            resultset = "This Id Setting is Exist ";
                            checksetset = false;
                            return Json(new { check = checksetset, result = resultset });
                        }
                        else
                        {
                            iddef.Id = 0;
                            iddef.SystemConstant = idDefinition.iden_component;
                            iddef.Description = idDefinition.iden_description;
                            iddef.Prefix = idDefinition.iden_prefix == null ? "" : idDefinition.iden_prefix;
                            iddef.PrefixSeparator = idDefinition.iden_prefixseparator == null ? "" : idDefinition.iden_prefixseparator;
                            iddef.SuffixSeparator = idDefinition.iden_suffixseparator == null ? "" : idDefinition.iden_suffixseparator;
                            iddef.Length = int.Parse(idDefinition.iden_length);
                            iddef.Suffix = idDefinition.iden_suffix == null ? "" : idDefinition.iden_suffix;
                            iddef.Remark = idDefinition.iden_remark;

                            var createiden = await _sharedHelpers.CreateIdDefinition(iddef);
                            resultset = "Saved Successfully";
                            checksetset = true;
                            return Json(new { check = checksetset, result = resultset });
                        }
                    }
                }
            }

        }

        public async Task<IActionResult> deleteIddefinition(int code)
        {
            var deleteiden = await _sharedHelpers.DeleteIddefinitionByID(code);
            return Json("Deleted Successfully");
        }
        public async Task<IActionResult> GetIdDefinitionById(int code)
        {
            var idenn = await _sharedHelpers.GetIddefinitionById(code);
            var iden = idenn?.FirstOrDefault();
            return Json(new { code = iden.Id, descption = iden.Description, compo = iden.SystemConstant, preftype = iden.Prefix, prefseparator = iden.PrefixSeparator, lenth = iden.Length, suffxx= iden.Suffix, suffsepa= iden.SuffixSeparator , remark  = iden.Remark});
        }
    }
}

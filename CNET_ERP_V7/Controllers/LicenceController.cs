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
using CNET_V7_Domain.Domain.ViewSchema;
using CNET_V7_Entities.DataModels;
using System.Collections.Immutable;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Cnetv7BufferHolder;

namespace CNET_ERP_V7.Controllers
{
    public class LicenceController : Controller
    {
        private AuthenticationManager _authenticationManager;
        private readonly HttpClient _httpClient;
        private readonly SharedHelpers _sharedHelpers;
        public List<ConfigurationDTO> Config = new List<ConfigurationDTO>();
        #region Ctor
        public LicenceController(AuthenticationManager authenticationManager,
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
            return View(new LicenseModel
            {
            });
        }
        public async Task<IActionResult> getLicencePosDetail()
        {

            List<CnetlicenseDTO> cNETs = new List<CnetlicenseDTO> ();
            List<CnetlicenseDTO>  cNETsList = new List<CnetlicenseDTO> ();

            var mConmpLIsts = GeneralBufferHolder.SystemConstants;
            var deviceLIsts = await _sharedHelpers.SelectDevice();

            var mConmpLIstsByType = mConmpLIsts.Where(c => c?.Type?.ToLower() == "pos").ToList();
            var licence = await _sharedHelpers.LicenseBufferList();
            var licenceList = licence.Where(l => mConmpLIstsByType.Any(c => c.Id == l.Licensee)).ToList();




            var Data = licenceList?.Select(retposResult => new LicencePosResult
            {
                pos_code = retposResult.Id,
                pos_description = retposResult?.Licensee == null ? "" : _sharedHelpers.GetSytemConstantBytypeandId(retposResult.Licensee, "POS").Result.FirstOrDefault()?.Description,
                pos_descriptioncode = retposResult?.Licensee == null ? 0 : _sharedHelpers.GetSytemConstantBytypeandId(retposResult.Licensee, "POS").Result.FirstOrDefault()?.Id,
                pos_device = retposResult?.CreatorUser == null ? "" : _sharedHelpers.SelectDeviceBYId(retposResult?.CreatorUser)?.Result?.FirstOrDefault()?.MachineName,
                pos_devicecode = retposResult?.CreatorUser == null ? 0 : _sharedHelpers.SelectDeviceBYId(retposResult?.CreatorUser)?.Result?.FirstOrDefault()?.Id,
                pos_licencecode = retposResult?.LicenseCode,
                pos_remark = retposResult?.Licensee == null ? "" : _sharedHelpers.GetSytemConstantBytypeandId(retposResult.Licensee, "POS").Result.FirstOrDefault()?.Type?.ToUpper(),
                pos_remaindate = null /*resourceLicense.IsLicenseValid(getSystemCode(retposResult.reference), retposResult.licenseCode, "0001", "POS").Result.RemainingDays.ToString()*/,
                pos_remaindate1 = null /*_resourceLicense.IsLicenseValid(getSystemCode(retposResult.reference), retposResult.licenseCode, "0001", "POS").Result.isValid.ToString()*/,


            }).ToList();
            cNETs = Data.Select(x => new CnetlicenseDTO
            {
                Id = x.pos_code,
                Licensee = 0,
                LicenseCode = x.pos_licencecode,
                Remark = x.pos_remark+"/" +x.pos_device + "/" + x.pos_remaindate + "/" + x.pos_remaindate1+"/"+ x.pos_description,
                CreatorUser = 0

            }).ToList();

            cNETsList.AddRange(cNETs);

            var CNERTlicence = new LicenseModel() { NETLicenses = cNETsList };
            return PartialView("_LicenceposDetail", CNERTlicence);

        }

        public async Task<IActionResult> getLicenceSubDetail()
        {
            List<CnetlicenseDTO>  cNETs = new List<CnetlicenseDTO> ();
            List<CnetlicenseDTO>  cNETsList = new List<CnetlicenseDTO> ();

            var mConmpLIsts = GeneralBufferHolder.SystemConstants;

            var mConmpLIstsByType = mConmpLIsts.Where(c => c.Type == "Subsystem").ToList();
            var licence = await _sharedHelpers.LicenseBufferList();

            var cnetline = licence.Where(l => mConmpLIstsByType.Any(c => c.Id == l.Licensee)).ToList();


            var Data = cnetline.Select(retposResult => new LicenceSubResult
            {
                sub_code = retposResult.Id,
                sub_description = retposResult.Licensee == null ? "" : _sharedHelpers.GetSytemConstantBytypeandId(retposResult.Licensee, "SUBSYSTEM").Result?.FirstOrDefault()?.Description,
                sub_descriptioncode = retposResult.Licensee == null ? 0 : _sharedHelpers.GetSytemConstantBytypeandId(retposResult.Licensee, "SUBSYSTEM").Result?.FirstOrDefault()?.Id,
                sub_licencecode = retposResult.LicenseCode,
                sub_remark = retposResult.Licensee == null ? "" : _sharedHelpers.GetSytemConstantBytypeandId(retposResult.Licensee, "SUBSYSTEM").Result?.FirstOrDefault()?.Type.ToUpper(),
                sub_remaindate = null, /*_resourceLicense.IsLicenseValid(getSystemCode(retposResult.reference), retposResult.licenseCode, "0001", "SUBSYSTEM").Result.RemainingDays.ToString()*/
                sub_remaindate1 = null, /*_resourceLicense.IsLicenseValid(getSystemCode(retposResult.reference), retposResult.licenseCode, "0001", "SUBSYSTEM").Result.isValid.ToString()*/

            }).ToList();
            //Data.ForEach(x => x.pos_remaindate = getRemainDate(x.pos_remaindate));

            cNETs = Data.Select(x => new CnetlicenseDTO
            {
                Id = x.sub_code,
                Licensee =0,
                LicenseCode = x.sub_licencecode,
                Remark = x.sub_remark+"/"+ x.sub_remaindate + "/" + x.sub_remaindate1+"/" + x.sub_description,
                CreatorUser = 0

            }).ToList();

            cNETsList.AddRange(cNETs);

            var CNERTlicence = new LicenseModel() { cNETLicenses = cNETsList };
            return PartialView("_LicenceSubDetail", CNERTlicence);

        }

        public async Task<IActionResult> createLicencedetail([FromForm] LicenseModel licenseModel)
        {
            List<CnetlicenseDTO>  cNETLicenses = new List<CnetlicenseDTO> ();
            List<CnetlicenseDTO>  cNETLicensesList = new List<CnetlicenseDTO> ();

            var resultTYpe = "";
            if (licenseModel.licenccodeList != null && licenseModel.componentcodeList != null)
            {

                CnetlicenseDTO aobjcomp = new CnetlicenseDTO();
                for (int item = 0; item < licenseModel.licenccodeList.Count() - 1; item++)
                {
                    var getlicencecode = await _sharedHelpers.GetLicenseBylicence(licenseModel.licenccodeList[item]);

                    if (getlicencecode != null)
                    {
                        var deletorgunit = await _sharedHelpers.DeleteCnetlicence(getlicencecode?.FirstOrDefault()?.Id);

                    }
                    var comp = licenseModel.componentcodeList[item];
                    var cList = comp.ToCharArray();
                    var splitedComp = cList[0].ToString() + cList[10].ToString() + cList[11].ToString() + cList[12].ToString();

                    var CNcompnt = GeneralBufferHolder.SystemConstants;
                    var CNcomp = CNcompnt.FirstOrDefault(c => c.Id == Convert.ToInt32(comp));

                    //var liIsValid = await _resourceLicense.IsLicenseValid(splitedComp, licenseModel.licenccodeList[item], "0001", CNcomp.Type == "POS" ? "POS" : "SUBSYSTEM");

                    //if (liIsValid != null && liIsValid.isValid && liIsValid.RemainingDays > 0)
                    //{
                        aobjcomp.Id = 0;
                        aobjcomp.Licensee = Convert.ToInt32(licenseModel.componentcodeList[item]);
                        aobjcomp.LicenseCode = licenseModel.licenccodeList[item];
                        aobjcomp.Remark = CNcomp?.Type == "POS" ? "POS" : licenseModel.lince_remark;
                        if (licenseModel.lince_device != 0)
                        {
                            aobjcomp.CreatorUser = licenseModel.lince_device;
                        }
                        var createlice = await _sharedHelpers.createCnetlicence(aobjcomp);

                        if (createlice.Id >0)
                        {
                            resultTYpe = "Saved Successfully";
                        }
                    

                }

            }



            return Json(new { result = resultTYpe });

        }
        public async Task<IActionResult> DeleteLicenceItem(int ID)
        {
           
            var deletorgunit = await _sharedHelpers.DeleteCnetlicence(ID);

            return Json("Deleted Successfully");
        }

    //    public async Task<IActionResult> getLicencecodeFromcloud()
    //    {

    //        var checknull = false;
    //        var cnetcomponent = await _sharedHelpers.Getcnetcoomponenet();
    //        var LicenseServerdevice = await _sharedHelpers.GetdeviceList();
    //        var LicenseServerDevice = LicenseServerdevice.FirstOrDefault(x => x.preference == CNETConstantes.LicenseServer); ;

    //        var LicenseServerIP = "";
    //        var LicenseServerPort = "";
    //        var HasLicenseServer = false;
    //        if (LicenseServerDevice != null)
    //        {
    //            List<IPParameter> LicenseServerDeviceIPParameterList = await _sharedHelpers.getIPParameterBydevice(LicenseServerDevice.code);


    //            if (LicenseServerDeviceIPParameterList != null && LicenseServerDeviceIPParameterList.Count > 0)
    //            {
    //                IPParameter LicenseServerDeviceIPParameter = LicenseServerDeviceIPParameterList.FirstOrDefault();
    //                if (!string.IsNullOrEmpty(LicenseServerDeviceIPParameter.ip) && !string.IsNullOrEmpty(LicenseServerDeviceIPParameter.port))
    //                {
    //                    LicenseServerIP = LicenseServerDeviceIPParameter.ip;
    //                    LicenseServerPort = LicenseServerDeviceIPParameter.port;
    //                    HasLicenseServer = true;
    //                }
    //                else
    //                {
    //                    HasLicenseServer = false;
    //                }
    //            }

    //        }
    //        var CompanyTin = await _resourceLicense.GetTIN_Number();

    //        var licenseFromCloud = new List<spGetClientInfobyTin_Result>();

    //        if (HasLicenseServer == true)
    //        {

    //            using (HttpClient client = new HttpClient())
    //            {
    //                string URL = "http://" + LicenseServerIP + ":" + LicenseServerPort + "/";
    //                client.BaseAddress = new Uri(URL);

    //                client.DefaultRequestHeaders.Accept.Clear();

    //                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


    //                HttpResponseMessage response = null;

    //                try
    //                {
    //                    string urlParameters = "api/License/getLicense?tin=" + CompanyTin;
    //                    response = await client.GetAsync(urlParameters);
    //                    if (response.IsSuccessStatusCode)
    //                    {
    //                        dynamic res = await response.Content.ReadAsStringAsync();
    //                        licenseFromCloud = JsonConvert.DeserializeObject<List<spGetClientInfobyTin_Result>>(res);
    //                    }
    //                }

    //                catch (HttpRequestException e)
    //                {
    //                }
    //            }
    //        }
    //        if (licenseFromCloud.Count() > 0)
    //        {


    //            var licensefromcloud = licenseFromCloud.Select(y => new

    //            {
    //                y.Branch,
    //                y.expiryDate,
    //                component = (from cnet in cnetcomponent
    //                             where cnet.code == y.component
    //                             select new { cnet.code, cnet.description }).FirstOrDefault()
    //,
    //                y.licenseKey,
    //                y.deviceValue

    //            }).ToList();
    //            return Json(licensefromcloud);
    //        }
    //        else
    //        {
    //            checknull = false;
    //            return Json(new { checknull = checknull });
    //        }


    //    }
        private string getSystemCode(string component)
        {

            char[] splitedCurrentSystemCode = component.ToCharArray();
            string systemCodeAfterSplit = (splitedCurrentSystemCode[0].ToString() +
                                           splitedCurrentSystemCode[10].ToString() +
                                           splitedCurrentSystemCode[11].ToString() +
                                           splitedCurrentSystemCode[12].ToString());


            return systemCodeAfterSplit;
        }

        public class CLicensList
        {
            public string description { get; set; }
            public bool oldrecord { get; set; }
            public bool select { get; set; }
            public string remainingDate { get; set; }
        }
    }
}

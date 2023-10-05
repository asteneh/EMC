using CNET_ERP_V7.Common.AuthNavigation;
using CNET_ERP_V7.Common.Company;
using CNET_ERP_V7.Models.FramworkModels;
using CNET_ERP_V7.Models.SubSytsemModel;
using CNET_V7_Domain.Domain.CommonSchema;
using CNET_V7_Domain.Domain.ConsigneeSchema;
using CNET_V7_Domain.Domain.SecuritySchema;
using CNET_V7_Domain.Domain.SettingSchema;
using CNET_V7_Domain.Domain.ViewSchema;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace CNET_ERP_V7.Controllers
{
    public class FiltersController : Controller
    {
        private readonly HttpClient _httpClient;
        private Common.AuthNavigation.AuthenticationManager _authenticationManager;
        private readonly SharedHelpers _sharedHelpers;
        public FiltersController(
            IHttpClientFactory httpClientFactory,
            Common.AuthNavigation.AuthenticationManager authenticationManager,
            SharedHelpers sharedHelpers)
        {
            _httpClient = httpClientFactory.CreateClient("mainclient");
            _authenticationManager = authenticationManager;
            _sharedHelpers = sharedHelpers;
        }

        [HttpGet]
        public async Task<object> GetConsigneeList(DataSourceLoadOptions loadOptions, VoucherSearchModel searchM)
        {
            Dictionary<string, string> Dictionaryvalue = new Dictionary<string, string>();
            Dictionaryvalue.Add("voucherDefn", searchM.documentType.ToString());
            Dictionaryvalue.Add("type", "1628");

            var _requirdGslList = new List<RequiredGslDTO>();
            _requirdGslList = await _sharedHelpers.GetFilterData<List<RequiredGslDTO>>("RequiredGsl", Dictionaryvalue);
            var resquiredGSLCodes = _requirdGslList.Select(r => r.Id).ToList();
            var gslTypeDetailList = await GetRequiredGSLDetailByRequiredGSL(resquiredGSLCodes.ToList());

            //if (gslTypeDetailList.Count() > 0)
            //{
            //    var groupedDetailList = gslTypeDetailList.GroupBy(d => d.GslType).ToList();
            //    foreach (var reqGSL in groupedDetailList)
            //    {
            //        var consignee = await GetVoucherConsignee(reqGSL.Key, reqGSL.Select(g => g.ObjectState).Distinct().ToList(), searchM.q);
            //        consignee.Wait();

            //        if (consignee.Result?.Count > 0)
            //        {
            //            listOfConsignees.AddRange(consignee.Result);
            //        }
            //    }
            //    //if (listOfConsignees != null && listOfConsignees.Count > 0)
            //    //    listOfConsignees = listOfConsignees.GroupBy(p => p.code).Select(p => p.First()).ToList();
            //}

            //getRequiredgslbyrefrerence and type ==>  CNETConstantes.VOUCHER_CONSIGNEE
            //gslRequiredGslDetail by string list
            return DataSourceLoader.Load(_requirdGslList, loadOptions);
        }
        private async Task<List<RequiredGsldetailDTO>> GetRequiredGSLDetailByRequiredGSL(List<int> gsltypes)
        {
            var _requirdGslDetailList = new List<RequiredGsldetailDTO>();

            var response = await _httpClient.GetAsync("RequiredGsldetail");
            if (!response.IsSuccessStatusCode)
                return null;

            var jrgDetailDto = await response.Content.ReadAsStringAsync();
            var rgDetailDto = JsonConvert.DeserializeObject<List<RequiredGsldetailDTO>>(jrgDetailDto);
            _requirdGslDetailList = rgDetailDto != null ? rgDetailDto?.ToList() : null;
            var fillist = new List<RequiredGsldetailDTO>();
            foreach (var index in gsltypes)
            {
                var fil = _requirdGslDetailList.Where(r => r.RequiredGsl == index).FirstOrDefault();
                fillist.Add(fil);
            }

            return fillist;
        }
        //public async Task<List<vw_VoucherConsignee>> GetVoucherConsignee(int? gslType = null, List<int> objectState = null, string consigneeCode = null)
        //{
        //    //using (CNET2016Entities context = new CNET2016Entities())
        //    //{
        //    //    var result = from con in context.vw_VoucherConsignee select con;
        //    //    result = gslType.HasValue ? result.Where(c => c.gslType == gslType.Value) : result;
        //    //    result = objectState != null ? result.Where(c => objectState.Contains(c.objectStateDefinition)) : result;
        //    //    result = !string.IsNullOrWhiteSpace(consigneeCode) ? result.Where(c => c.code.Contains(consigneeCode)) : result;

        //    //    return await result.ToListAsync();

        //    //}
        //}

        [HttpGet]
        public async Task<object> GetDeviceList(DataSourceLoadOptions loadOptions, VoucherSearchModel x)
        {
            Dictionary<string, string> Dictionaryvalue = new Dictionary<string, string>();
            Dictionaryvalue.Add("ConsigneeUnit", "1");
            var _listofDevicesByunit = await _sharedHelpers.GetFilterData<List<DeviceDTO>>("Device", Dictionaryvalue);

            return DataSourceLoader.Load(_listofDevicesByunit, loadOptions);
        }

        [HttpGet]
        public async Task<object> GetUserList(DataSourceLoadOptions loadOptions, VoucherSearchModel x)
        {
            var _listofAllUsers = new List<UserDTO>();

            var response = await _httpClient.GetAsync("User");
            if (!response.IsSuccessStatusCode)
                return null;

            var juserDto = await response.Content.ReadAsStringAsync();
            var userDto = JsonConvert.DeserializeObject<List<UserDTO>>(juserDto);
            _listofAllUsers = userDto != null ? userDto?.ToList() : null;

            return DataSourceLoader.Load(_listofAllUsers, loadOptions);
        }

        [HttpGet]
        public async Task<object> GetCatagoryListt(DataSourceLoadOptions loadOptions, VoucherSearchModel pageM)
        {
            var _preferenceList = new List<PreferenceDTO>();
            try
            {
                Dictionary<string, string> Dictionaryvalue = new Dictionary<string, string>();
                Dictionaryvalue.Add("Reference", pageM.documentType.ToString());
                _preferenceList = await _sharedHelpers.GetFilterData<List<PreferenceDTO>>("Preference", Dictionaryvalue);
            }
            catch (Exception ex)
            {

            }
            return DataSourceLoader.Load(_preferenceList, loadOptions);
        }


        [HttpGet]
        public IActionResult Test(string loadOptions)
        {
            // Assuming `loadOptions` is a JSON string containing the load options

            try
            {
                var json = new[] {
                new Customer {
                    iD = 1,
                    companyName = "Premier Buy",
                    address = "7601 Penn Avenue South",
                    city = "Richfield",
                            state = "Minnesota",
                            zipcode = 55423,
                            phone = "(612) 291-1000",
                            fax = "(612) 291-2001",
                            website = "http =//www.nowebsitepremierbuy.com"
                        }


                    };
                return Ok(json);
                //// Process the load options and extract necessary data if required

                //// Make the necessary API call or fetch data from the desired source
                //// You can use HttpClient or any other library to make the HTTP GET request
                //// Replace "YOUR_API_URL" with the actual URL or endpoint you want to call
                //string apiUrl = "YOUR_API_URL" + "?" + loadOptions;
                //using (HttpClient client = new HttpClient())
                //{
                //    HttpResponseMessage response = client.GetAsync(apiUrl).Result;
                //    response.EnsureSuccessStatusCode();
                //    string json = response.Content.ReadAsStringAsync().Result;

                //    // Return the JSON response as the result
                //    return Ok(json);
                // }
            }
            catch (Exception ex)
            {
                // Handle error case
                return Content("", ex.Message);
            }
        }

    }
    public class Customer
    {
        public int iD { get; set; }
        public string companyName { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public int zipcode { get; set; }
        public string phone { get; set; }
        public string fax { get; set; }
        public string website { get; set; }
    }
}

using CNET_ERP_V7.Common.AuthNavigation;
using CNET_ERP_V7.Common.Company;
using CNET_ERP_V7.Models;
using CNET_ERP_V7.Models.FramworkModels;
using CNET_V7_Domain.Misc;
using Cnetv7BufferHolder;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;

namespace CNET_ERP_V7.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private AuthenticationManager _authenticationManager;
        private readonly SharedHelpers _sharedHelpers;
        private readonly HttpClient _httpClient;
        public HomeController(ILogger<HomeController> logger,
            AuthenticationManager authenticationManager ,
           IHttpClientFactory httpClientFactory,
           SharedHelpers sharedHelpers)
        {
            _logger = logger;
            _authenticationManager = authenticationManager;
            _httpClient = httpClientFactory.CreateClient("mainclient");
            _sharedHelpers = sharedHelpers;
            GeneralBufferHolder.SystemConstants = _sharedHelpers.GetAllSytemConstants()?.Result?.ToList();
            GeneralBufferHolder.AllTaxs = _sharedHelpers.GetAllTaxs()?.Result?.ToList();
            var comp = _sharedHelpers.GetCompany();
            var comptin = comp?.Result?.Tin;
            GeneralBufferHolder.CompanyInformations = _sharedHelpers.GetCompanyInfo(comptin)?.Result;
            GeneralBufferHolder.AllPeriods = _sharedHelpers.GetAllPeriods()?.Result?.ToList();
            GeneralBufferHolder.AllCurrencies = _sharedHelpers.GetAllCurrencies()?.Result?.ToList();
            GeneralBufferHolder.AllConsineeUnit = _sharedHelpers.GetAllConsigneeUnits()?.Result?.ToList();

            GeneralBufferHolder.AllPreferences = _sharedHelpers.GetAllPreferences()?.Result?.ToList();
        }
        public async Task<IActionResult> Index()
        {
            var authUser = await _authenticationManager.GetAuthenticatedUser();
            if (authUser == null || authUser.Remark == "" || authUser.Remark == null)
            {
                return RedirectToAction("Login", "Login");
            }
            //also check the consignee unit code exist in the buffer

            var _GvariableModel = await  _sharedHelpers.GetGlobalParams(authUser);

            return View(_GvariableModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        private string Decrypt(string encryptedText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(encryptedText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey,
                    new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    encryptedText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return encryptedText;
        }
    }
}
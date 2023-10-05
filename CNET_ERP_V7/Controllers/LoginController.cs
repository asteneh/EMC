using CNET_ERP_V7.Common.AuthNavigation;
using CNET_ERP_V7.Common.Company;
using CNET_ERP_V7.Models;
using CNET_ERP_V7.Models.FramworkModels;
using CNET_ERP_V7.WebConstants;
using CNET_V7_Domain;
using CNET_V7_Domain.Domain.ConsigneeSchema;
using CNET_V7_Domain.Domain.SecuritySchema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace CNET_ERP_V7.Controllers
{
    public class LoginController : Controller
    {
        private readonly HttpClient _httpClient;
        private AuthenticationManager _authenticationManager;
        private readonly SharedHelpers _sharedHelpers;
        public LoginController(IHttpClientFactory httpClientFactory, 
            AuthenticationManager authenticationManager,
            SharedHelpers sharedHelpers)
        {
            _httpClient = httpClientFactory.CreateClient("mainclient");
            _authenticationManager = authenticationManager;
            _sharedHelpers = sharedHelpers;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            var iResult = await _authenticationManager.identificationValid();
            if(iResult.isValid)
                return View(new LoginModel { ValidID = iResult});

            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model, string? returnUrl)
        {
            if (true || ModelState.IsValid)
            {
                var loginResult = await _authenticationManager.AuthenticateUser(model.Username?.Trim(), model.Password,model?._tin);
                if (loginResult.Success)
                {
                    if (false && model.Branch == null)
                        return RedirectToAction("Login", "Login");
                    var addBranchTologgedUser = await _sharedHelpers.GetUserByUserName(model.Username) ?? null;
                    addBranchTologgedUser.Remark = model.Branch + "_" + Encrypt(model.Password);
                    _authenticationManager.SignIn(addBranchTologgedUser, model.RememberMe);
                    if (string.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
                    {
                        if (model.Branch != null)
                        {
                            var rpx = Encrypt(model.Password);
                            return RedirectToAction("Index", "Home");
                        }
                        else
                            return RedirectToAction("Login", "Login");
                    }
                    return Redirect(returnUrl);

                }
                else
                {
                    ModelState.AddModelError("", "Incorrect Username or Password!");
                }
            }
            var _company = await _sharedHelpers.GetCompany();
            model.ValidID.BranchList = await _sharedHelpers.GetCompanyBranchsByTin(model._tin);
            model.ValidID.tin = model._tin;
            model.ValidID.CompanyTradeName = _company.FirstName;

            return View("Login", model);
        } 

        [HttpPost]
        public async Task<IActionResult> checkMyId([FromBody] VerifyIdModel model)
        {
            if (ModelState.IsValid)
            {
                string message = string.Empty;
                var myOrg = await _sharedHelpers.GetCompany();
                if(myOrg != null)
                {
                    if (myOrg?.Tin == model.myId?.Trim())
                    {
                        if (model.remember)
                        {
                            addCookie(CNET_WebConstantes.IdentificationCookie, model?.myId, CNET_WebConstantes.IdentificationCookieLifeTime);
                        }
                        else
                        {
                            addCookie(CNET_WebConstantes.IdentificationCookie, model?.myId, CNET_WebConstantes.IdentificationCookieDailyLifeTime);
                        }

                        return Json(new
                        {
                            d = true,
                            m = "Verified successfuly"
                        });
                    }
                    message = "Invalid identification no.";
                }
                else
                {
                    message = "Company definition not found";
                }
                    
                return Json(new
                {
                    d = false,
                    m = message
                });
            }

            return View("Index", model);
        }

        public async Task<IActionResult> Logout()
        {
            _authenticationManager.SignOut();
            return RedirectToAction("Login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private void addCookie(string key, string value, int? expireTime)
        {
            CookieOptions option = new CookieOptions();

            if (expireTime.HasValue)
                option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
            else
                option.Expires = DateTime.Now.AddMilliseconds(10);

            Response.Cookies.Append(key, value, option);
        }
        private string Encrypt(string clearText)
        {

            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey,
                    new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
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
    }
}

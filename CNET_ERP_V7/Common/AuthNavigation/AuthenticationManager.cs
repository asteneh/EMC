using CNET_V7_Domain.Domain.ConsigneeSchema;
using CNET_V7_Domain.Domain.SecuritySchema;
using CNET_ERP_V7.WebConstants;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using CNET_ERP_V7.Common.Company;
using System.Net.Http;
using CNET_ERP_V7.Models;
using CNET_V7_Domain.Misc;

namespace CNET_ERP_V7.Common.AuthNavigation
{
    public class AuthenticationManager
    {
        private IHttpContextAccessor _httpContextAccessor;
        private readonly HttpClient _httpClient;
        private readonly SharedHelpers _sharedHelpers;
        private UserDTO _cachedUser;
        public AuthenticationManager(
                IHttpContextAccessor httpContextAccessor,
                IHttpClientFactory httpClientFactory,
                SharedHelpers sharedHelpers
                )
        {
            _httpContextAccessor = httpContextAccessor;
            _httpClient = httpClientFactory.CreateClient("mainclient");
            _sharedHelpers = sharedHelpers;
        }

        public virtual async Task<validIdentificationReturn> identificationValid()
        {
            var validinfo = new validIdentificationReturn();
            var idCookie = _httpContextAccessor.HttpContext?.Request.Cookies[CNET_WebConstantes.IdentificationCookie];
            if (!string.IsNullOrWhiteSpace(idCookie))
            {
                var myOrg = await _sharedHelpers.GetCompany();
                if (myOrg?.Tin == idCookie)
                {
                    var _branchlist = await _sharedHelpers.GetCompanyBranchsByTin(myOrg?.Tin);
                    validinfo.CompanyTradeName = myOrg.FirstName;
                    validinfo.BranchList = _branchlist;
                    validinfo.tin = idCookie;
                    validinfo.isValid = true;
                    return validinfo;
                }
            }
            validinfo.isValid = false;
            return validinfo;
        }
       
        public async Task<ResponseModel<LoginResponse>> AuthenticateUser(string userName, string password ,string tin)
        {
            var _s = new ResponseModel<LoginResponse>();
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
            {
                //return AuthenticationErrorType.IncorrectUserNamePassword;
                return _s;
            }
            else
              {
                    var endpoint = "SysInitialize/authenticate";
                    string queryString = $"?userName={userName}&password={password}&tin={tin}";
                    string requestUrl = $"{endpoint}{queryString}";
                
                    HttpResponseMessage response = await _httpClient.GetAsync(requestUrl); 
                    string juservalidation = await response.Content.ReadAsStringAsync();
                    var userValidation = JsonConvert.DeserializeObject<ResponseModel<LoginResponse>>(juservalidation);

                    if (!response.IsSuccessStatusCode)
                        return userValidation;

                    if (userValidation.Success)
                        return userValidation;
                    else
                        return userValidation;
                
            }
        }

        public virtual async void SignIn(UserDTO user, bool isPersistent)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            //create claims for customer's username and email
            var claims = new List<Claim>();

            if (!string.IsNullOrEmpty(user.UserName))
                claims.Add(new Claim(ClaimTypes.Name, user.UserName, ClaimValueTypes.String, CNET_WebConstantes.ClaimsIssuer));

            if (!string.IsNullOrEmpty(user.Remark))
                claims.Add(new Claim(ClaimTypes.Email, user.Remark, ClaimValueTypes.String, CNET_WebConstantes.ClaimsIssuer));

            //create principal for the current authentication scheme
            var userIdentity = new ClaimsIdentity(claims, CNET_WebConstantes.CookieScheme);
            var userPrincipal = new ClaimsPrincipal(userIdentity);

            //set value indicating whether session is persisted and the time at which the authentication was issued
            var authenticationProperties = new AuthenticationProperties
            {
                IsPersistent = isPersistent,
                IssuedUtc = DateTime.UtcNow
            };

            //sign in
            await _httpContextAccessor.HttpContext.SignInAsync(CNET_WebConstantes.CookieScheme, userPrincipal, authenticationProperties);

            //cache authenticated customer
            _cachedUser = user;
        }
        public virtual async Task<UserDTO?> GetAuthenticatedUser()
        {
            if (_cachedUser != null)
                return _cachedUser;

            var authenticateResult = await _httpContextAccessor.HttpContext.AuthenticateAsync(CNET_WebConstantes.CookieScheme);
            if (!authenticateResult.Succeeded)
                return null;

            UserDTO? user = null;

            //try to get customer by username
            var usernameClaim = authenticateResult.Principal.FindFirst(claim => claim.Type == ClaimTypes.Name
                && claim.Issuer.Equals(CNET_WebConstantes.ClaimsIssuer, StringComparison.InvariantCultureIgnoreCase));
            if (usernameClaim != null)
            {
                user = await _sharedHelpers.GetUserByUserName(usernameClaim.Value) ?? null;
            }

            //get claim2
            var usernameClaim2 = authenticateResult.Principal.FindFirst(claim => claim.Type == ClaimTypes.Email
               && claim.Issuer.Equals(CNET_WebConstantes.ClaimsIssuer, StringComparison.InvariantCultureIgnoreCase));
            if (user != null && usernameClaim2 != null)
            {
                user.Remark = usernameClaim2.Value;

            }
            //whether the found user is available
            if (user == null)
                return null;

            //cache authenticated customer
            _cachedUser = user;

            return _cachedUser;
        }
        public virtual async void SignOut()
        {
            //reset cached customer
            _cachedUser = null;
            //and sign out from the current authentication scheme
            await _httpContextAccessor.HttpContext.SignOutAsync(CNET_WebConstantes.CookieScheme);
        }
    }

    #region Enum
    public enum AuthenticationErrorType : int
    {
        NoError = 0,
        IncorrectUserNamePassword = 1,
        ServerDateBreached = 2,
        UserHasNoRightToCommitPendingTask = 3,
        AccountIsExpired = 4,
        AccountIsDisabled = 5,
        OneOfTheAttachedDeviceIsNotLicensed = 6,
        MandatoryPeriodicalProcedureHasNotBeenPerformed = 7,
        UserAlreadyLoggedInWithThisAccount = 8,
        YourAccountWillExpire = 9,
        NoAccountLogInformation = 10,
        SecurityConfigurationsAreNotSettled = 11,
        UnKnownSecurityError = 12,
        NoLicenseEntryForRequestedResource = 13,
        NoLicenseEntryForTheAttachedDevice = 14,
        InvalidLicense = 15,
        PasswordResetRequired = 16,
        ChangePasswordFail = 17,
        NoRoleFound = 18,
        VersionIncompatibility = 19,
        NoActivityForUserNamePasswordIncorrect = 20,
        NoActivtyforusernamecreated = 21,
        NoActivityForSuccessfullyLoggedIn = 22,
    }
    #endregion

}


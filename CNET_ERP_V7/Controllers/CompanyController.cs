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
using CNET_V7_Domain.Domain.CommonSchema;
using System.Collections.Immutable;
using CNET_V7_Domain.Domain.HrmsSchema;
using CNET_ERP_V7.Models.SelectorModel;
using CNET_V7_Domain.Domain.TransactionSchema;
using CNET_V7_Domain.Domain.ViewSchema;
using System.Net;
using AuthenticationManager = CNET_ERP_V7.Common.AuthNavigation.AuthenticationManager;
using DevExtreme.AspNet.Mvc;
using CNET_V7_Entities.DataModels;
using Cnetv7BufferHolder;
using CNET_ERP_V7.Common.Helpers;
using System.Diagnostics;
using System.Security.Principal;

namespace CNET_ERP_V7.Controllers
{
    public class CompanyController : Controller
    {

        #region Dclaration
        private readonly ILogger<HomeController> _logger;
        public string orgTin;
        private IWebHostEnvironment _env;

        public string idencode;
        bool industryinvolvedflagg = false;

        public List<string> currentCompanyOrgUnits = new List<string>();
        string FtpFilePath_IP = "ftp://196.191.244.133";
        string userName = "CHM_USER";
        string passWord = "AttACHeMenT5&@BBMF@TIIvsDNR";
        string uniqueFileName = null;
        byte[] fileBytes = null;
        string ftpfilepath = null;
        String ftpFilePath2 = null;
        int gsltype;
        ConsigneeDTO organization = new ConsigneeDTO();
        int? organizationCode = 0;

        bool isForUpdate = false;
        #endregion

        private AuthenticationManager _authenticationManager;
        private readonly HttpClient _httpClient;
        private readonly SharedHelpers _sharedHelpers;
        public List<ConfigurationDTO> Config = new List<ConfigurationDTO>();
        #region Ctor
        public CompanyController(AuthenticationManager authenticationManager,
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
            return View(new Companymodel()
            {
                comType = id,

            });

        }
     
        public IActionResult GetOrgunitdefvalue(int BRanchCOde)
        {

            var orgunitaddress = new Companymodel() { orgunitaddress = BRanchCOde };

            return PartialView("organizationAddressTable", orgunitaddress);
        }
        public async Task<IActionResult> Getparent(int orgunit, bool headoffice)
        {
            var currentCompanyOrgUnits = new List<int>();// GetIorgUnitDefins();

            List<ConsigneeUnitDTO> parentid = new List<ConsigneeUnitDTO>();
            var organization = await _sharedHelpers.GetConsigneeByType(1);

            var orgunitdefList = await _sharedHelpers.GetConsigneeunit();
            orgunitdefList = orgunitdefList.Where(x => x.Consignee == organization?.FirstOrDefault()?.Id).ToList();
            if (organization != null)
            {
                var getorganizationunit = orgunitdefList.Where(x => x.Consignee ==  organization?.FirstOrDefault()?.Id);
                currentCompanyOrgUnits = getorganizationunit.Select(o => o.Consignee).ToList();

            }
            if (orgunit == 1719 || orgunit == 1718 || orgunit == 1727 )
            {
                parentid = orgunitdefList.Where(x => x.Type == 1719).DistinctBy(y => y.Name).ToList();
            }
            else if (orgunit == 1720 || orgunit == 1721 || orgunit == 1722)
            {
                parentid = orgunitdefList.Where(x => x.Type == 1718 || x.Type == 1720).DistinctBy(y => y.Name).ToList();
            }
            else
            {
                parentid = orgunitdefList.Where(o =>
                       (o.Type == orgunit ||
                       (orgunit != 1725 && o.Type == 1719))
                        && (organization != null && currentCompanyOrgUnits.Contains(o.Consignee))).OrderBy(x => x.Type).ToList();

            }

            var parentcode = new Companymodel() { branchType = orgunit, parentcode = parentid, headoffice = headoffice };

            return PartialView("_oudModal", parentcode);
        }
        public IActionResult GetbasicInformation()
        {
            return PartialView("_basicInformation");
        }
        public async Task<IActionResult> Getorgunitcode(int orgunit)
        {
            
            List<ConsigneeUnitDTO> getorganizationunit = new List<ConsigneeUnitDTO>();

            List<VoucherConsigneeListDTO> vw_VoucherConsignees = new List<VoucherConsigneeListDTO>();
            List<VwConsigneeViewDTO> vw_VoucherConsigneesList = new List<VwConsigneeViewDTO>();
            List<int> currentCompanyOrgUnits = new List<int>();
            var getorganization = await _sharedHelpers.GetConsigneeByType(1);

            var spacficOrg = await _sharedHelpers.GetConsigneeByType1(orgunit);
            var orgunitdef = await _sharedHelpers.GetConsigneeunit();
            if (getorganization != null)
            {
                getorganizationunit = await _sharedHelpers.GetConsigneeByconsignee(getorganization?.FirstOrDefault()?.Id);
                currentCompanyOrgUnits = getorganizationunit.Select(o => o.Consignee).ToList();

            }

            if (orgunitdef != null)
            {
                //newcode
                if (getorganization != null)
                {

                    spacficOrg = orgunitdef.Where(x => x.Type == orgunit && currentCompanyOrgUnits.Contains(x.Consignee)).ToList();
                }
                else
                {
                    spacficOrg = orgunitdef.Where(x => x.Type == orgunit).ToList();
                }
            }
            var organization_unit_type = new Companymodel() { organization_unit_type = spacficOrg };
           
            return PartialView("_Organization_UnitDefination_List", organization_unit_type);

        }
        public async Task<IActionResult> saveOrganization(Companymodel model)
        {
            gsltype = 1;
            var company = await _sharedHelpers.GetConsigneeByType(gsltype);
            if (company != null)
                organization = company?.FirstOrDefault();
            List<ConsigneeDTO> consignee = new List<ConsigneeDTO>();
            Dictionary<string, string> Dictionaryvalue1 = new Dictionary<string, string>() { { "gsltype", gsltype.ToString() }, { "requiredfields", "id,code,isperson,MainConsigneeUnit" } };
            consignee = await _sharedHelpers.GetDynamicData<List<ConsigneeDTO>>("Consignee", Dictionaryvalue1);

            int? consign = consignee?.FirstOrDefault()?.MainConsigneeUnit == 0 ? 0 : consignee?.FirstOrDefault()?.MainConsigneeUnit;

            var _Createconsigne = new ConsigneeDTO
            {
                Id = organization.Id,
                Code = model.code,
                Tin = model.tin,
                FirstName = model.tradeName,
                SecondName = model.brandName,
                BusinessType = model.businessType,
                MainConsigneeUnit = consign,
                Preference = model.category,
                ComminicationSource = model.communicationSource,
                CreditLimit = model.transactionLimitFactor,
                TransactionLimit = model.transactionLimitType,
                ParentId = model.cParentId == 0 ? null : model.cParentId,
                AssignedConsigneeUnit = model.assigned,
                BaseUrl = model.Baseurl,
                DefaultImageUrl = model.Iimage,
                DefaultCurrency = model.Currency,
                DefaultLanguage = model.language,
                Note = model.companyNote,
                Nationality = model.nationality,
                StartDate = model.DOB,
                CreatedOn = DateTime.Now,
                LastModified = DateTime.Now,
                GslType = gsltype,
                IsActive = model.isActive,
                Locked = false,
                Remark = null
           };
            var _ConsigneeUnit = new List<ConsigneeUnitDTO>();
                if (consign == 0 )
                {
                        var ConsigneeUnit = new ConsigneeUnitDTO
                        {
                            // required
                            Id = 0,
                            Consignee = 0,
                            Name = "Main office",
                            Description = "head qurter",
                            Type = 1719,
                            Specialization = 1706,
                            CreatedOn = DateTime.Now,
                            LastModified = DateTime.Now,
                            IsActive = true,
                            Subcity = model.Subcity == 0 ? null : model.Subcity,
                            City = model.City == 0 ? null : model.City,
                            Region = model.Region == 0 ? null : model.Region,
                            Country = model.Country == 0 ? null : model.Country,
                            ParentId = null,
                            Longitude = model.complatitude,
                            Latitude = model.complatitude,
                            Contact = model.Contact,
                            Phone1 = model.Phone1,
                            Phone2 = model.Phone2,
                            Email = model.Email,
                            Website = model.Website,
                            SpecificAddress = model.SpecificAddress,
                            Street = model.Street,
                            HouseNumber = model.HouseNumber,
                            PoBox = model.PoBox,
                            Kebele = model.Kebele,
                            Wereda = model.Wereda,
                            AddressLine1 = model.AddressLine1,
                            AddressLine2 = model.AddressLine2,
                            AddressLine3 = model.AddressLine3,

                        };
                        _ConsigneeUnit.Add(ConsigneeUnit);
                
             
                }
               List<GsltaxDTO> _GslTax = new List<GsltaxDTO>();
                if (model.tax != null)
                {
                    GsltaxDTO _tax = new GsltaxDTO
                    {
                        Reference = organization.Id,
                        Tax = Int32.Parse(model.tax.ToString())

                    };
                    _GslTax.Add(_tax);
                }
            #region Identification
            List<IdentificationDTO> _Identification = new List<IdentificationDTO>();

            if (model.identificationDescription != null)
            {
                for (int i = 0; i < model.identificationDescription.Count(); i++)
                {
                    if (model.idNumber[i] != null)
                    {
                        IdentificationDTO Identify = new IdentificationDTO
                        {

                            Description = model.identificationDescription[i].ToString(),
                            IdNumber = model.idNumber[i].ToString(),
                            IssueDate = DateTime.Now,
                            ExpiryDate = DateTime.Now,
                            Consignee = (int)organization?.Id,
                            Type = model.identificationType[i],
                        };
                        _Identification.Add(Identify);
                    }
                }
            }
            var authUser = await _authenticationManager.GetAuthenticatedUser();
            // Activity
            var _Activity = new ActivityDTO();
            _Activity.TimeStamp = DateTime.Now;
            _Activity.Year = DateTime.Now.Year;
            _Activity.Month = DateTime.Now.Month;
            _Activity.User = authUser.Id;
            _Activity.ActivityDefinition = 3307;
            _Activity.Platform = "web";
            #endregion
            var ConsigneeBuffer = new ConsigneeBuffer
            {
                consignee = _Createconsigne,
                consigneeUnits = _ConsigneeUnit,
                Gsltaxs = _GslTax,
                Identifications = _Identification,
                Activity = _Activity,
            };
            var isSuccess = await _sharedHelpers.SaveConsigneeBuffer(ConsigneeBuffer);
            if (isSuccess != null)
            {
                return Json("Consignee Saved Successfully");
            }
            else
            {
                return Json("Consignee Saving faild,Please try again.");
            }
        }

        #region Save Selected Tab General
        public async Task<IActionResult> SaveInSelectedTabGeneral(Companymodel model)
        {
            ConsigneeDTO _organization = new ConsigneeDTO();
        

            organizationCode = organization?.Id;
            ArticleDTO item = new ArticleDTO();
            DeviceDTO device = new DeviceDTO();

            if (organization == null)
            {
                isForUpdate = false;
                organization = new ConsigneeDTO();

                //var val = await _sharedHelpers.IdGenerater("Organization", null, CNETConstantes.FIXEDASSET.ToString(), CNETConstantes.ARTICLE, 1);// UIProcessManager.("GSL", device, gsltype.ToString(), CNETConstantes.ORGANIZATION);

                //organization.code = val.GeneratedNewId;
            }
            else
            {
                isForUpdate = true;

            }
            bool isSaved = false;

            #region Organization Insertion

            string imageUrl = null;
            if (model.defimage != null)
            {
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.defimage.FileName;
                uniqueFileName = uniqueFileName.Replace(" ", string.Empty);
                uniqueFileName = uniqueFileName.Replace("+", string.Empty);
                uniqueFileName = uniqueFileName.Replace("%", string.Empty);
                uniqueFileName = uniqueFileName.Replace(",", string.Empty);
                uniqueFileName = uniqueFileName.Replace("&", string.Empty);
                uniqueFileName = uniqueFileName.Replace("*", string.Empty);
                uniqueFileName = uniqueFileName.Replace("@", string.Empty);
                //model.attachmentreference = model.editedGslCode;
                //getDynamicFilePath(model);

                //fileBytes = streamToByteArray(model.defimage.OpenReadStream());

                //if (UploadFileToFTP())
                //{
                //    imageUrl = String.Format("{0}/{1}", FtpFilePath_IP, ftpFilePath2);
                //}
            }
            List<ConsigneeDTO> consignee = new List<ConsigneeDTO>();
            Dictionary<string, string> Dictionaryvalue1 = new Dictionary<string, string>() { { "gsltype","1" }, { "code", model.code }, { "requiredfields", "id,code,isperson,MainConsigneeUnit" } };
            consignee = await _sharedHelpers.GetDynamicData<List<ConsigneeDTO>>("Consignee", Dictionaryvalue1);

            int? consign = consignee?.FirstOrDefault()?.MainConsigneeUnit == 0 ? 0 : consignee?.FirstOrDefault()?.MainConsigneeUnit;

            if (!isForUpdate)
            {
                _organization.Id =  _sharedHelpers.CreateOrganization(_organization).Id;
            }
            else
            {
                _organization.Id = organization.Id;
                _organization.Code = model.code;
                _organization.FirstName = model.tradeName;
                _organization.Tin = model.compTin;
                _organization.SecondName = model.brandName;
                _organization.BusinessType = model.businessType;
                _organization.MainConsigneeUnit = 1;
                _organization.Preference = model.category;
                _organization.ComminicationSource = model.communicationSource;
                _organization.CreditLimit = model.transactionLimitFactor;
                _organization.TransactionLimit = model.transactionLimitType;
                _organization.ParentId = model.cParentId == 0 ? null : model.cParentId;
                _organization.AssignedConsigneeUnit = model.assigned;
                _organization.BaseUrl = model.Baseurl;
                _organization.DefaultImageUrl = model.Iimage;
                _organization.DefaultCurrency = model.Currency;
                _organization.DefaultLanguage = model.language;
                _organization.Nationality = model.nationality;
                _organization.StartDate = model.DOB;
                _organization.CreatedOn = DateTime.Now;
                _organization.LastModified = DateTime.Now;
                _organization.GslType = gsltype;
                _organization.Note = model.companyNote;
                _organization.IsActive = model.isActive;
                _organization.Locked = false;
                _organization.Remark = null;
                var updateOrg = await _sharedHelpers.UpdateOrganization(_organization);
            }
            isSaved = true;
            #endregion

            if (isSaved)
            {

                var gsltax = await _sharedHelpers.GetGslTaxByrefernce(_organization.Id);
                var deletegsltax = await _sharedHelpers.DeletegsltaxByreference(gsltax?.FirstOrDefault()?.Id);
                GsltaxDTO _tax = new GsltaxDTO
                {
                    Reference = _organization.Id,
                    Tax = (int)model.tax,
                    //IsArticle = true,

                };
                await _sharedHelpers.CreateGSLTax(_tax);

                #region Organization Tax center on Relation
               

                #endregion

                #region Identification
                await saveIdentification(model);
                #endregion

                #region Ownerrelation
                await CreateOwnerrelation(model);
                #endregion

                TempData["successData"] = "Data Saved successfully";
            }
            return View();
        }

        #endregion

        #region Create
        public async Task<bool> CreateOwnerrelation(Companymodel ownrel)
        {


            bool relationflag = false;
            List<RelationDTO> relationlist = new List<RelationDTO>();
            List<RelationDTO> relationupdate = new List<RelationDTO>();

            if (ownrel.person_owners_code == null)
            {
                return relationflag;
            }

            for (int i = 1; i < ownrel.person_owners_code.Count(); i++)
            {
                if (ownrel.owncodeforupdateList[i] == null)
                {

                    RelationDTO relation = new RelationDTO
                    {
                        RelationType = ownrel.person_relationtype_code[i],
                        ReferencedObject = ownrel.person_owners_code[i],
                        ReferringObject = 0,
                        RelationLevel = 1
                    };

                    if (relation != null)
                    {
                        await _sharedHelpers.Createrelation(relation);
                        relationflag = true;
                    }
                    relationlist.Add(relation);

                }
                else
                {

                    RelationDTO relation = new RelationDTO
                    {
                        Id = ownrel.owncodeforupdateList[i],
                        RelationType = ownrel.person_relationtype_code[i],
                        ReferencedObject = ownrel.person_owners_code[i],
                        ReferringObject = 0,
                        RelationLevel = 1
                    };
                    if (relation != null)
                    {
                        await _sharedHelpers.Updaterelation(relation);
                        relationflag = true;
                    }
                    relationupdate.Add(relation);


                }

            }

            //if (relationlist.Count() >= 1)
            //{
            //    relationflag = await _sharedHelpers.CreateRelationList(relationlist);

            //}
            //if (relationupdate.Count() >= 1)
            //{
            //    relationflag = await _sharedHelpers.UpdateRelationList(relationupdate);

            //}
            return relationflag;
        }

        [HttpPost]

        public async Task<bool> saveIdentification(Companymodel identify)
        {
            bool identificaionflag = false;


            List<IdentificationDTO> orgIdentify = new List<IdentificationDTO>();

           
            for (int i = 0; i < identify.identificationDescription.Count(); i++)
            {
                IdentificationDTO idAdd = new IdentificationDTO();
                IdentificationDTO idEdit = new IdentificationDTO();
                IdentificationDTO idDelete = new IdentificationDTO();


                if ((identify.identificationcode == null || identify.identificationcode?.Count() == 0) && identify.idNumber != null)
                {
                    idAdd.Consignee = (int)organization?.Id;
                    idAdd.Description = identify.identificationDescription[i].ToString();
                    idAdd.IdNumber = identify.idNumber[i];
                    idAdd.Type = identify.identificationType[i];
                    await _sharedHelpers.CreateIdentification1(idAdd);
                   
                }
                else
                {
                    if (identify.idNumber != null)
                    {
                        idAdd.Id = Convert.ToInt32(identify?.identificationcode[i]);
                        idAdd.Consignee = (int)organization?.Id;
                        idAdd.Description = identify.identificationDescription[i].ToString();
                        idAdd.IdNumber = identify?.idNumber[i]?.ToString();
                        idAdd.Type = identify?.identificationType[i];
                         await _sharedHelpers.UpdateIdentification2(idAdd);
                    }
                }
            }
            //if (identificationAdded.Count > 0)
            //     await _sharedHelpers.CreateIdentification(identificationAdded);
            //if (identificationChanged.Count > 0)
            //     await _sharedHelpers.UpdateIdentification(identificationChanged);

            return identificaionflag = true;
        }


        public async Task<IActionResult> SaveOrganizationUnit(Companymodel OrgudeFn)
        {
            var organization = await _sharedHelpers.GetConsigneeByType(1);
            var organizationCode = organization?.FirstOrDefault();
            var resultTYpe = "";
            var checkstatus = false;

            ConsigneeUnitDTO organizationUnitDefinitions = new ConsigneeUnitDTO();
            ConsigneeUnitDTO CRorganizationUnitDefinitions = new ConsigneeUnitDTO();
            ConsigneeDTO CRorganizationUnit = new ConsigneeDTO();

            RelationDTO Orgrelation = new RelationDTO();
            var orgunitdefn = await _sharedHelpers.GetConsigneeunit();

            if (OrgudeFn.orgUnitDefDesc != null && OrgudeFn.orgUnitDefSpec != null)
            {
                if ((OrgudeFn.organizationType != 1719 && OrgudeFn.organizationType != 1725) && OrgudeFn.orgUnitDefParent == 0)
                {
                    resultTYpe = "Parent is Mandatory";
                    checkstatus = false;
                }
                else
                {
                     if (OrgudeFn.organizationunitcode != 0)
                {
                    var orgunitdefnremark = orgunitdefn.Where(o => o.Remark == "Head Office").ToList();

                    if (orgunitdefnremark.Count() >= 1 && OrgudeFn.is_headoffice == true)
                    {
                        foreach (var item in orgunitdefnremark)
                        {
                            organizationUnitDefinitions.Id = item.Id;
                            organizationUnitDefinitions.Code = item.Code;
                            organizationUnitDefinitions.Type = item.Type;
                            organizationUnitDefinitions.Consignee = organizationCode.Id;
                            organizationUnitDefinitions.ParentId = item.ParentId == 0 ? null : item.ParentId;
                            organizationUnitDefinitions.Description = item.Description;
                            organizationUnitDefinitions.Name = item.Description;
                            organizationUnitDefinitions.Specialization = item.Specialization;
                            organizationUnitDefinitions.Abrivation = item.Abrivation;
                            organizationUnitDefinitions.IsOnline = item.IsOnline;
                            organizationUnitDefinitions.IsActive = item.IsActive;
                            organizationUnitDefinitions.Ecommerce = item.Ecommerce;
                            organizationUnitDefinitions.CreatedOn = DateTime.Now;
                            organizationUnitDefinitions.LastModified = DateTime.Now;
                            organizationUnitDefinitions.Remark = item.Remark;

                            var updateorgunitdefrem = await _sharedHelpers.UpdateOrganizationunit(organizationUnitDefinitions);

                        }
                    }
                    organizationUnitDefinitions.Id = OrgudeFn.organizationunitcode;
                    organizationUnitDefinitions.Code = OrgudeFn.consigneCode;
                    organizationUnitDefinitions.Type = OrgudeFn.organizationType;
                    organizationUnitDefinitions.Consignee = organizationCode.Id;
                    organizationUnitDefinitions.ParentId = OrgudeFn.orgUnitDefParent == 0 ? null : OrgudeFn.orgUnitDefParent;
                    organizationUnitDefinitions.Description = OrgudeFn.orgUnitDefDesc;
                    organizationUnitDefinitions.Name = OrgudeFn.consigneName;
                    organizationUnitDefinitions.Specialization = OrgudeFn.orgUnitDefSpec;
                    organizationUnitDefinitions.Abrivation = OrgudeFn.orgUnitDefAbbrivation;
                    organizationUnitDefinitions.IsActive = OrgudeFn.isActiveb;
                    organizationUnitDefinitions.IsOnline = OrgudeFn.isonline;
                    organizationUnitDefinitions.Ecommerce = OrgudeFn.ecommerce;
                    organizationUnitDefinitions.CreatedOn = DateTime.Now;
                    organizationUnitDefinitions.LastModified = DateTime.Now;
                    if (OrgudeFn.is_headoffice == true)
                    {
                        organizationUnitDefinitions.Remark = "Head Office";



                    }
                    else
                    {
                        organizationUnitDefinitions.Remark = OrgudeFn.orgUnitDefRemark;
                    }
                    var updateorgunitdef = await _sharedHelpers.UpdateOrganizationunit(organizationUnitDefinitions);


                    CRorganizationUnit.Preference = 0;
                    CRorganizationUnit.Code = OrgudeFn.organizationunitcode.ToString();
                    var updateorgunit = await _sharedHelpers.UpdateOrganization(CRorganizationUnit);

                    var getrelation = await _sharedHelpers.getRelationByreference(OrgudeFn.organizationunitcode);

                    if (getrelation != null && getrelation.Count () > 0)
                    {
                        if (OrgudeFn.orgUnitDefResperson != 0 && !string.IsNullOrWhiteSpace(OrgudeFn.orgUnitDefResperson.ToString()))
                        {
                            Orgrelation.Id = Convert.ToInt32(getrelation?.FirstOrDefault()?.Id);
                            Orgrelation.RelationType = 0;
                            Orgrelation.ReferringObject = OrgudeFn.orgUnitDefResperson;
                            Orgrelation.ReferencedObject = OrgudeFn.organizationunitcode;
                            Orgrelation.RelationLevel = 0;
                            var updatereela = await _sharedHelpers.Updaterelation(Orgrelation);
                        }

                        resultTYpe = "save sucessfully";
                        checkstatus = true;

                    }
                    else
                    {
                        if (OrgudeFn.orgUnitDefResperson != 0 && !string.IsNullOrWhiteSpace(OrgudeFn.orgUnitDefResperson.ToString()))
                        {
                            Orgrelation.RelationType = 0;
                            Orgrelation.ReferringObject = OrgudeFn.orgUnitDefResperson;
                            Orgrelation.ReferencedObject = OrgudeFn.organizationunitcode;
                            Orgrelation.RelationLevel = 0;

                            var createrela = await _sharedHelpers.Createrelation(Orgrelation);
                        }

                    }
                    resultTYpe = "save sucessfully";
                    checkstatus = true;

                }
                else
                {
                    var orgunitdefnvalue = orgunitdefn.Where(o => o.Type == OrgudeFn.organizationType && o.Description == OrgudeFn.orgUnitDefDesc && o.ParentId == OrgudeFn.orgUnitDefParent && o.Specialization == OrgudeFn.orgUnitDefSpec).ToList();

                    if (orgunitdefnvalue.Count() >= 1)
                    {
                        resultTYpe = "Enter Other Description";
                        checkstatus = false;
                    }
                    else
                    {
                        organizationUnitDefinitions.Id = 0;
                        organizationUnitDefinitions.Type = OrgudeFn.organizationType;
                        organizationUnitDefinitions.Code = OrgudeFn.consigneCode;
                        organizationUnitDefinitions.Description = OrgudeFn.orgUnitDefDesc;
                        organizationUnitDefinitions.Name = OrgudeFn.consigneName;
                        organizationUnitDefinitions.Consignee = organizationCode.Id;
                        organizationUnitDefinitions.Specialization = OrgudeFn.orgUnitDefSpec;
                        organizationUnitDefinitions.Abrivation = OrgudeFn.orgUnitDefAbbrivation;
                        organizationUnitDefinitions.IsActive = OrgudeFn.is_headoffice;
                        organizationUnitDefinitions.Ecommerce = OrgudeFn.ecommerce;
                        organizationUnitDefinitions.IsOnline = OrgudeFn.isonline;
                        organizationUnitDefinitions.ParentId = OrgudeFn.orgUnitDefParent == 0 ? null : OrgudeFn.orgUnitDefParent;
                        organizationUnitDefinitions.CreatedOn = DateTime.Now;
                        organizationUnitDefinitions.LastModified = DateTime.Now;
                        if (OrgudeFn.is_headoffice == true)
                        {
                            organizationUnitDefinitions.Remark = "Head Office";

                            var orgunitdefnremark = orgunitdefn.Where(o => o.Remark == "Head Office").ToList();
                            if (orgunitdefnremark != null || orgunitdefnremark.Count() > 0)
                            {
                                foreach (ConsigneeUnitDTO OD in orgunitdefnremark)
                                {
                                    OD.Remark = "";
                                    var updateorgunitdefrem = await _sharedHelpers.UpdateOrganizationunit(OD);
                                }
                            }
                        }
                        else
                        {
                            organizationUnitDefinitions.Remark = OrgudeFn.orgUnitDefRemark;
                        }
                        organizationUnitDefinitions.Remark = OrgudeFn.orgUnitDefRemark;

                        organizationUnitDefinitions.Id =  _sharedHelpers.CreateOrganizationunit(organizationUnitDefinitions).Id;


                        CRorganizationUnit.Preference = 0;
                        CRorganizationUnit.Code = organizationUnitDefinitions.Id.ToString();

                        if (organizationUnitDefinitions.Id > 0)
                        {
                            var createorgunit = await _sharedHelpers.CreateOrganization(CRorganizationUnit);
                        }

                        if (OrgudeFn.orgUnitDefResperson != 0 && !string.IsNullOrWhiteSpace(OrgudeFn.orgUnitDefResperson.ToString()))
                        {

                            Orgrelation.RelationType = 0;
                            Orgrelation.ReferringObject = OrgudeFn.orgUnitDefResperson;
                            Orgrelation.ReferencedObject = organizationUnitDefinitions.Id;
                            Orgrelation.RelationType = 0;

                            var createrela = await _sharedHelpers.Createrelation(Orgrelation);

                        }
                        resultTYpe = "save sucessfully";
                        checkstatus = true;

                    }
                }
                }
            }
            else
            {
                resultTYpe = "Enter All Fields";
                checkstatus = false;

            }
            return Json(new { reloaddata = checkstatus, result = resultTYpe });

            //return PartialView("_Organization_UnitDefination_List" ,new Companymodel { organization_unit_type  = OrgudeFn.organizationType });

        }
        public async Task<IActionResult> CreateOrganizationUnitAddress(Companymodel orgaddress)
        {
            ConsigneeUnitDTO unitDTO = new ConsigneeUnitDTO();
            if (orgaddress.referenceaddress == 0)
            {
                return Json("Please Selecte Branch");
            }
            else
            {
                if (orgaddress.referenceaddress != 0)
                {
                    var conunit = await _sharedHelpers.GetConsigneeunitById(orgaddress.referenceaddress);
                    var consgnUnit = conunit?.FirstOrDefault();
                    unitDTO.Id = orgaddress.referenceaddress;
                    unitDTO.Type = consgnUnit.Type;
                    unitDTO.Consignee = consgnUnit.Consignee;
                    unitDTO.Name = consgnUnit.Name;
                    unitDTO.Description = consgnUnit.Description;
                    unitDTO.Specialization = consgnUnit.Specialization;
                    unitDTO.Purpose = consgnUnit.Purpose;
                    unitDTO.Preference = consgnUnit.Preference;
                    unitDTO.Abrivation = consgnUnit.Abrivation;
                    unitDTO.Latitude = orgaddress.complatitude;
                    unitDTO.Longitude = orgaddress.complongitude;
                    unitDTO.Contact = orgaddress.Contact;
                    unitDTO.Phone1 = orgaddress.Phone1;
                    unitDTO.Phone2 = orgaddress.Phone2;
                    unitDTO.Email = orgaddress.email;
                    unitDTO.Website = orgaddress.Website;
                    unitDTO.SpecificAddress = orgaddress.SpecificAddress;
                    unitDTO.HouseNumber = orgaddress.HouseNumber;
                    unitDTO.PoBox = orgaddress.PoBox;
                    unitDTO.Kebele = orgaddress.Kebele;
                    unitDTO.Wereda = orgaddress.Wereda;
                    unitDTO.Subcity = orgaddress.Subcity;
                    unitDTO.City = orgaddress.City;
                    unitDTO.Region = orgaddress.Region;
                    unitDTO.Street = orgaddress.Street;
                    unitDTO.Country = orgaddress.Country;
                    unitDTO.AddressLine1 = orgaddress.AddressLine1;
                    unitDTO.AddressLine2 = orgaddress.AddressLine2;
                    unitDTO.AddressLine3 = orgaddress.AddressLine3;
                    unitDTO.IsActive = consgnUnit.IsActive;
                    unitDTO.ParentId = consgnUnit.ParentId;
                    unitDTO.DefaultObjectState = consgnUnit.DefaultObjectState;
                    unitDTO.CreatedOn = consgnUnit.CreatedOn;
                    unitDTO.LastModified = consgnUnit.LastModified;
                    unitDTO.Locked = consgnUnit.Locked;
                    unitDTO.IsOnline = consgnUnit.IsOnline;
                    unitDTO.ImageUrl = consgnUnit.ImageUrl;
                    unitDTO.Remark = consgnUnit.Remark;
                    await _sharedHelpers.UpdateOrganizationunit(unitDTO);
                }
               
            }
            return Json("Saved Successfully");
        }
        public async Task<IActionResult> createJobdescription(Companymodel createjob)
        {
            JobDescriptionDTO job = new JobDescriptionDTO();
            var checkcodeforUpdateorsave = false;

            var resultTYpe = "";
            if (createjob.job_category == 0 || createjob.job_description == null || createjob.job_status == 0)
            {
                resultTYpe = "Enter all fields";
            }
            else
            {
                if (createjob.job_orgUnitDefncode != 0)
                {
                    job.Id = createjob.job_orgUnitDefncode;
                    job.ConsigneeUnit = createjob.job_orgUnitDefn;
                    job.Index = createjob.job_index;
                    job.Category = createjob.job_category;
                    job.Description = createjob.job_description;
                    job.Status = createjob.job_status;
                    job.Remark = createjob.job_remark;
                    var updatejob = await _sharedHelpers.UpdateJobdescription(job);
                    checkcodeforUpdateorsave = true;
                    resultTYpe = "Update Successfully";
                }
                else
                {
                    job.Id = 0;
                    job.ConsigneeUnit = createjob.job_orgUnitDefn;
                    job.Index = createjob.job_index;
                    job.Category = createjob.job_category;
                    job.Description = createjob.job_description;
                    job.Status = createjob.job_status;
                    job.Remark = createjob.job_remark;
                    var creatjob = await _sharedHelpers.CreateJobdescription(job);
                    checkcodeforUpdateorsave = true;
                    resultTYpe = "Created Successfully";
                }
            }

            return Json(new { increament = checkcodeforUpdateorsave, result = resultTYpe });
        }
        public async Task<IActionResult> GetJobDescriptionById(int code)
        {
            var _JodDesn = await _sharedHelpers.GetjobdescriptionByorgId(code);

            return Json(new { code = _JodDesn.Id, desc = _JodDesn.Description, index = _JodDesn.Index, ctegory = _JodDesn.Category, status = _JodDesn.Status, remark = _JodDesn.Remark });
        }
        public async Task<IActionResult> createBankAccountdetail(Companymodel model)
        {
            BankAccountDetailDTO bankAccountDetail = new BankAccountDetailDTO();

            var checkstatus = false;
            var resultset = "";

            if (model.bankAccountDetailCatagory == null)
            {
                resultset = "Please Selecte Category";
                checkstatus = false;
                return Json(new { check = checkstatus, result = resultset });
            }
            else
            {
                if (model.bankAccountDetailDesc == null || model.bankAccountDetailType == 0 || model.bankAccountDetailACCnum == null)
                {
                    resultset = "Please Enter all fields";
                    checkstatus = false;
                    return Json(new { check = checkstatus, result = resultset });
                }
                else
                {
                    var company = await _sharedHelpers.GetConsigneeByType(1);
                    if (model.bankAccountDetailCodeforEdit != 0)
                    {
                        bankAccountDetail.Id = model.bankAccountDetailCodeforEdit;
                        bankAccountDetail.Consignee = (int)company?.FirstOrDefault()?.Id;
                        bankAccountDetail.Description = model.bankAccountDetailDesc;
                        bankAccountDetail.Category = model.bankAccountDetailCatagory;
                        bankAccountDetail.Type = model.bankAccountDetailType;
                        bankAccountDetail.ConsigneeUnit = model.bankAccountDetailBank;
                        bankAccountDetail.AccountNo = model.bankAccountDetailACCnum;
                        bankAccountDetail.Remark = model.bankAccountDetailRemark;

                        var updatebank = await _sharedHelpers.UpdateBankAccountDetail(bankAccountDetail);
                        checkstatus = true;
                        resultset = "Saved Successfully";
                        return Json(new { check = checkstatus, result = resultset });


                    }
                    else
                    {
                        bankAccountDetail.Id = 0;
                        bankAccountDetail.Consignee = (int)company?.FirstOrDefault()?.Id;
                        bankAccountDetail.Description = model.bankAccountDetailDesc;
                        bankAccountDetail.Category = model.bankAccountDetailCatagory;
                        bankAccountDetail.Type = model.bankAccountDetailType;
                        bankAccountDetail.ConsigneeUnit = model.bankAccountDetailBank;
                        bankAccountDetail.AccountNo = model.bankAccountDetailACCnum;
                        bankAccountDetail.Remark = model.bankAccountDetailRemark;

                        var modell = await _sharedHelpers.GetBankAccountDetailByAcNumandRefandCat(bankAccountDetail.AccountNo, bankAccountDetail?.Consignee);
                        if (modell.Count() >= 1)
                        {
                            checkstatus = false;
                            resultset = "Bank Account Detail already exist";
                            return Json(new { check = checkstatus, result = resultset });
                        }
                        else
                        {
                            await _sharedHelpers.CreateBankAccountDetail(bankAccountDetail);
                            checkstatus = true;
                            resultset = "Saved Successfully";
                            return Json(new { check = checkstatus, result = resultset });
                        }
                    }
                }
            }
        }
        public async Task<IActionResult> GetBankAccountDetailById(int code)
        {
            var  banklit = await _sharedHelpers.GetBankAccountDetailById(code);
            var bank = banklit?.FirstOrDefault();
            return Json(new {id = bank.Id,description = bank.Description, type = bank.Type,bankk = bank.ConsigneeUnit,accnom = bank.AccountNo,remark = bank.Remark });
        }
        public async Task<IActionResult> saveAttachemnt(Companymodel attmodel)
        {
            byte index = 0;

            bool retrival = false;

            uniqueFileName = Guid.NewGuid().ToString() + "_" + attmodel?.file?.FileName;
            uniqueFileName = uniqueFileName.Replace(" ", string.Empty);
            getDynamicFilePath(attmodel);

            fileBytes = streamToByteArray(attmodel.file.OpenReadStream());

            AttachmentDTO attachement = new AttachmentDTO();
            if (UploadFileToFTP(attmodel))
            {
                var company = await _sharedHelpers.GetConsigneeByType(1);

                attachement.Reference = Convert.ToInt32(company?.FirstOrDefault()?.Id);
                attachement.Category = attmodel.attachmentcatagory;
                attachement.Description = attmodel.attachmentdescription.Replace(" ", string.Empty);
                attachement.Url = String.Format("{0}/{1}", FtpFilePath_IP, ftpFilePath2);
                attachement.Type = 1463;
                attachement.Index = index;
                attachement.Remark = null;

                 await _sharedHelpers.CreateAttachment(attachement);

                return Json(new { success = true, message = "Attachment save sucessfully" });
            }
            else
            {
                return Json(new { success = true, message = "Sorry Attachment Noted saved!" });
            }

           
        }
        public bool UploadFileToFTP(Companymodel attmode)
        {
            var upload = true;

            try
            {
                if (attmode.attachmentcatagory == 1438 || attmode.attachmentcatagory == 1441 || attmode.attachmentcatagory == 1448)
                {
                    ftpFilePath2 = String.Format(ftpfilepath + "{0}", uniqueFileName);
                    FtpWebRequest ftpRequest;

                    ftpRequest = (FtpWebRequest)WebRequest.Create(String.Format("{0}/{1}", FtpFilePath_IP, ftpFilePath2));
                    ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
                    ftpRequest.Credentials = new NetworkCredential(userName, passWord);
                    ftpRequest.ContentLength = fileBytes.Length;

                    if (DirectoryExists())
                    {
                        Stream requestStream = ftpRequest.GetRequestStream();
                        requestStream.Write(fileBytes, 0, fileBytes.Length);
                        requestStream.Close();

                    }
                    else
                    {
                        ftpRequest = (FtpWebRequest)FtpWebRequest.Create(String.Format("{0}/{1}", FtpFilePath_IP, ftpFilePath2));
                        ftpRequest.Method = WebRequestMethods.Ftp.MakeDirectory;
                        ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
                        ftpRequest.Credentials = new NetworkCredential(userName, passWord);
                        ftpRequest.ContentLength = fileBytes.Length;

                        using (Stream requestStream = ftpRequest.GetRequestStream())
                        {
                            requestStream.Write(fileBytes, 0, fileBytes.Length);
                            requestStream.Close();
                        }
                    }
                    upload = true;
                }
            }
            catch (Exception Ex)
            {
                return false;
            }
            return upload;

        }

        public void getDynamicFilePath(Companymodel gslmodel)
        {
            string tinNumber = getCompanyTin()?.Result?.ToString();
            string profileType = "";

            if (gslmodel.attachmentcatagory != null)
            {
                if (gslmodel.attachmentcatagory == 1441)
                {
                    profileType = "logo";
                }
                else if (gslmodel.attachmentcatagory == 1448)
                {
                    profileType = "Picture";
                }
                else if (gslmodel.attachmentcatagory == 1438)
                {
                    profileType = "banner";
                }

                ftpfilepath = String.Format("/{0}/{1}/{2}/", tinNumber, "CompanyProfile", profileType);
            }

        }
        private async Task<string> getCompanyTin()
        {
            string tinNumber = null;

            var myOrg = await _sharedHelpers.GetCompany();
                tinNumber = myOrg?.Tin;
            return tinNumber;
        }

        public static byte[] streamToByteArray(Stream input)
        {
            MemoryStream ms = new MemoryStream();
            input.CopyTo(ms);
            return ms.ToArray();
        }

        public async Task<IActionResult> savecompAttachemnt(Companymodel attmodel)
        {
            int Identificationlastcode = 0;
            List<AttachmentDTO> Updateattachment = new List<AttachmentDTO>();

            AttachmentDTO attachement = new AttachmentDTO();

            int  attType = 0;
            string ext = Path.GetExtension(attmodel.com_file.FileName);
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

            uniqueFileName = Guid.NewGuid().ToString() + "_" + attmodel.com_file.FileName;
            uniqueFileName = uniqueFileName.Replace(" ", string.Empty);
            var filepath = Path.Combine(_env.WebRootPath, "images", uniqueFileName);
            var stream = new FileStream(filepath, FileMode.Create);
            await attmodel.com_file.CopyToAsync(stream);
            stream.Close();

            var identificationList = await _sharedHelpers.GetIddefinition();
            var IdentificationType = identificationList.Where(i => i.SystemConstant == 0);
            if (IdentificationType.Count() >= 1)
            {
                Identificationlastcode = IdentificationType.LastOrDefault().Id;
                Updateattachment = await _sharedHelpers.AttachmentByReference(Identificationlastcode);

            }

            if (Updateattachment != null)
            {
                attachement.Id = Updateattachment.FirstOrDefault().Id;
                attachement.Reference = Updateattachment.FirstOrDefault().Reference;
                attachement.Category = 1451;
                attachement.Description = attmodel.com_attachmentdescription.Replace(" ", string.Empty);
                attachement.Url = uniqueFileName.Replace(" ", string.Empty);
                attachement.Type = attType;
                attachement.Index = 0;
                attachement.Remark = ext;

                await _sharedHelpers.UpdateAttachment(attachement);

                return Json(new { success = true, message = "Attachment  update successfully" }); ;

            }
            else
            {

                attachement.Reference = Identificationlastcode;
                attachement.Category = 1451;
                attachement.Description = attmodel.com_attachmentdescription.Replace(" ", string.Empty);
                attachement.Url = uniqueFileName.Replace(" ", string.Empty);
                attachement.Type = attType;
                attachement.Index = 0;
                attachement.Remark = ext;

                 await _sharedHelpers.CreateAttachment(attachement);


                return Json(new { success = true, message = "Attachment save successfully" });

            }
        }

        public async Task<IActionResult> createCompanyDocument(Companymodel criden)
        {


            IdentificationDTO identification = new IdentificationDTO();
            List<string> idenn = new List<string>();
            var indexInc = 0;
            var checkcodeforUpdateorsave = false;
            var resultTYpe = "";


            if (criden.idenTypecode == 0)
            {
                resultTYpe = "Please Select Company Document Type";
            }
            else
            {
                if (criden.com_description == null || criden.com_Idnumber == null)
                {
                    resultTYpe = "Enter all Fields";
                }
                else
                {

                    if (criden.idenTypeEditcode != 0)
                    {
                        identification.Id = criden.idenTypeEditcode;
                        identification.Consignee = 1;
                        identification.Description = criden.com_description;
                        identification.IdNumber = criden.com_Idnumber;
                        identification.Type = criden.idenTypecode;
                        if (criden.com_Issued_Datedisable == true)
                        {
                            identification.IssueDate = DateTime.Now;
                        }
                        else
                        {
                            identification.IssueDate = criden.com_Issued_Date;
                        }
                        if (criden.com_Expiry_Datedisable == true)
                        {
                            identification.ExpiryDate = null;
                        }
                        else
                        {
                            identification.ExpiryDate = criden.com_Expiry_Date;
                        }
                        if (criden.com_Issuerdisable == true)
                        {
                            identification.Issuer = null;
                        }
                        else
                        {
                            identification.Issuer = criden.com_Issuer;
                        }
                        identification.Remark = criden.com_remark;

                        var updateiden = await _sharedHelpers.UpdateIdentification2(identification);
                        resultTYpe = "Updated Successfully";
                        checkcodeforUpdateorsave = true;
                    }
                    else
                    {
                        identification.Id = 0;
                        identification.Consignee = 1;
                        identification.Description = criden.com_description;
                        identification.IdNumber = criden.com_Idnumber;
                        identification.Type = criden.idenTypecode;
                        if (criden.com_Issued_Datedisable == true)
                        {
                            identification.IssueDate = DateTime.Now;
                        }
                        else
                        {
                            identification.IssueDate = criden.com_Issued_Date;

                        }
                        if (criden.com_Expiry_Datedisable == true)
                        {
                            identification.ExpiryDate = null;
                        }
                        else
                        {
                            identification.ExpiryDate = Convert.ToDateTime(criden.com_Expiry_Date);
                        }
                        if (criden.com_Issuerdisable == true)
                        {
                            identification.Issuer = null;
                        }
                        else
                        {
                            identification.Issuer = criden.com_Issuer;
                        }

                        identification.Remark = criden.com_remark;

                        var createiden = await _sharedHelpers.CreateIdentification1(identification);
                        resultTYpe = "Saved Successfully";
                        checkcodeforUpdateorsave = true;


                    }
                }
            }
            return Json(new { increament = checkcodeforUpdateorsave, result = resultTYpe });

        }

        #endregion
        public bool DirectoryExists()
        {
            try
            {
                FtpWebRequest request = GetRequest();
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                return request.GetResponse() != null;
            }
            catch
            {
                return false;
            }
        }
        protected FtpWebRequest GetRequest()
        {
            FtpWebRequest request = WebRequest.Create(String.Format("{0}/{1}", FtpFilePath_IP, ftpfilepath)) as FtpWebRequest;
            request.Credentials = new NetworkCredential(userName, passWord);
            request.Proxy = null;
            request.KeepAlive = false;
            return request;
        }

        #region Get
        public virtual async Task<IActionResult> organizationUnitDefinationList(Companymodel Model)
        {

            ////    var OrgUnitDes = Model.orgdescrpition;
            //var OrgUnitDefType = Model.orgUnitDefType;
            //List<ConsigneeUnitDTO> spacficOrg = new List<OrganizationUnitDefinition>();

            //var getorganization = await _sharedHelpers.SelectOrganizationByType(0);

            //var orgunitdef = await _sharedHelpers.GetorgUnitDefinition();

            //var getorganizationunit = await _sharedHelpers.GetOrgUnitByReference(getorganization.code);
            //currentCompanyOrgUnits = getorganizationunit.Select(o => o.organizationUnitDefinition).ToList();


            //var relationList = await _sharedHelpers.getRelationByrelationTYpenadreference(CNETConstantes.COMPANYORGANIZATIONSUPERVISOR);
            //var getrelationdesc = await _sharedHelpers.GetVoucherConsigneeList();
            //var getlookdesc = await _sharedHelpers.SelectLookUp();

            //if (orgunitdef != null)
            //{
            //    //newcode
            //    if (getorganization != null)
            //    {
            //        spacficOrg = orgunitdef.Where(x => x.type == OrgUnitDefType && currentCompanyOrgUnits.Contains(x.code)).ToList();
            //    }
            //    else
            //    {
            //        spacficOrg = orgunitdef.Where(x => x.type == OrgUnitDefType).ToList();
            //    }
            //}

            //var relationanndVouch = (from rel in relationList
            //                         join vou in getrelationdesc
            //                     on rel.referringObject equals vou.code

            //                         select new
            //                         {
            //                             vou.code,
            //                             vou.name,
            //                             rel.referenceObject

            //                         }).ToList();

            //var spacficOrglistt = spacficOrg.Select(y => new
            //{
            //    y.code,
            //    y.parentId,
            //    y.type,
            //    y.description,
            //    y.abbriviation,
            //    y.remark,

            //    specialization = (from loo in getlookdesc
            //                      where loo.code == y.specialization
            //                      select new { loo.code, loo.description }).FirstOrDefault(),


            //    responsivleperson = (from rels in relationanndVouch
            //                         where rels.referenceObject == y.code
            //                         select new { rels.code, rels.name }).FirstOrDefault()


            //}).ToList();

            return Json(null);

        }
        public virtual async Task<IActionResult> getOUDParrent(Select2Model searchModel)
        {
            //    var resultSet = new List<Select2Result>();

            //    List<OrganizationUnitDefinition> ORGUnitDef = new List<OrganizationUnitDefinition>();
            //    List<OrganizationUnitDefinition> OrgUnitDef = new List<OrganizationUnitDefinition>();

            //    OrganizationUnitDefinition organizationUnitDefinition = new OrganizationUnitDefinition();

            //    if (!string.IsNullOrWhiteSpace(searchModel?.q))
            //    {
            //        var modelOUnit = await _sharedHelpers.GetOrgUnitByReference(searchModel.other);


            //        for (var i = 0; i < modelOUnit.Count; i++)
            //        {
            //            string OrgUnitDeff = modelOUnit[i].organizationUnitDefinition;
            //            OrgUnitDef = await _sharedHelpers.GetOrganizationUnitDefinitionByOrgCode(OrgUnitDeff);
            //            organizationUnitDefinition.code = OrgUnitDef[0].code;
            //            organizationUnitDefinition.description = OrgUnitDef[0].description;
            //            ORGUnitDef.Add(organizationUnitDefinition);
            //        }

            //        resultSet = ORGUnitDef.Select(r => new Select2Result()
            //        {
            //            id = r.code,
            //            text = r.description
            //        }).ToList();
            //    }
            //    return Json(new
            //    {
            //        results = resultSet,
            //        pagination = new
            //        {
            //            more = false //model.Result.Count > 10
            //        }
            //    });

            return null;
        }
        public virtual async Task<IActionResult> OrganizationBankDetail(int code)
        {
            var company = await _sharedHelpers.GetConsigneeByType(1);
            var bankdetail = await _sharedHelpers.GetBankAccountDetailByRefandCatagory(company?.FirstOrDefault()?.Id, code);

            var bank = new Companymodel() { BankAccountDetai = bankdetail };
            return PartialView("_BankDetailList", bank);
        }

        [HttpGet]
        public virtual async Task<IActionResult> getRelationList(Select2Model searchModel)
        {
            //var resultSet = new List<Select2Result>();
            //if (!string.IsNullOrWhiteSpace(searchModel?.q) && searchModel?.other != 0)
            //{
            //    int typeList = searchModel.other;

            //    if (typeList == 22 || typeList == 7|| typeList == 14 || typeList == 21 || typeList == 24)
            //    {
            //        var relationTypeAgent = await _sharedHelpers.GetAllSuppliers(typeList, searchModel.q);
            //        resultSet = relationTypeAgent.Select(r => new Select2Result()
            //        {
            //            id = r.code,
            //            text = r.description
            //        }).ToList();
            //    }
            //    else if (typeList == 11)
            //    {
            //        var relationTypeAgent = await _sharedHelpers.GetArticlesByTypeAndnames(typeList, searchModel.q);
            //        resultSet = relationTypeAgent.Select(r => new Select2Result()
            //        {
            //            id = r.code,
            //            text = r.name
            //        }).ToList();
            //    }
            //    else if (typeList == 13)
            //    {
            //        var relationTypeAgent = await _sharedHelpers.GetOrganizationByTypeandName(typeList, searchModel.q);
            //        resultSet = relationTypeAgent.Select(r => new Select2Result()
            //        {
            //            id = r.code,
            //            text = r.brandName
            //        }).ToList();
            //    }
            //}
            //return Json(new
            //{
            //    results = resultSet,
            //    pagination = new
            //    {
            //        more = false //model.Result.Count > 10
            //    }
            //});
            return null;
        }



        public async Task<IActionResult> Retrive_Attachement()
        {
            var company = await _sharedHelpers.GetConsigneeByType(1);
            var resultSetattachment = await _sharedHelpers.AttachmentByReference(company?.FirstOrDefault()?.Id);
            var resultSetattachmentList = resultSetattachment.Where(x => x.Category == 1441 || x.Category == 1448 || x.Category == 1438).ToList();

            var resultset = resultSetattachmentList.Select(r => new AttachementviewModel()
            {
                attcode = r.Id,
                reference = r.Reference,
                url = r.Url,
                catagory = r.Category,
                description = r.Description

            }).ToList();

            return Json(new { data = resultset });
        }

        public async Task<Stream> readAttachmentFile(string id)
        {
            
            var url = id?.Split("*")[0];
            var code = id?.Split("*")[1];
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                // Provide credentials to access the FTP server 
                request.Credentials = new NetworkCredential(userName, passWord);
                FtpWebResponse response = await request.GetResponseAsync() as FtpWebResponse;

                // Downloaded PDF Stream
                Stream stream = response.GetResponseStream();

                //Convert FTP Data Stream to Memory Stream
                MemoryStream mStream = new MemoryStream();
                stream.CopyTo(mStream);

                // Create file stream from the byte array.
                Stream newStream = new MemoryStream(mStream.ToArray());

                // return RedirectToAction("Index", "Company");
                return newStream;
            }

            catch (Exception e)
            {
                await _sharedHelpers.DeleteAttachment(Convert.ToInt32(code));
                return null;

            }

        }
        public async Task<IActionResult> Retrive_Attachement_docu(int attchcode, int comcategory)
        {
            int Identificationlastcode = 0;
            var identificationList = await _sharedHelpers.GetIddefinition();
            var IdentificationType = identificationList.Where(i => i.Id == 0 && i.SystemConstant == attchcode);
            Identificationlastcode = IdentificationType.LastOrDefault().Id;


            var resultSetattachment = await _sharedHelpers.AttachmentByReferencenadCat(Identificationlastcode, comcategory);
            var Comresultset = resultSetattachment.Select(r => new DocumentAttachementviewModel()
            {
                com_attcode = r.Id,
                com_reference = r.Reference,
                com_url = r.Url,
                com_catagory = r.Category,
                com_description = r.Description

            }).ToList();

            return Json(new { data = Comresultset });
        }

        public async Task<IActionResult> Retrive_Attachement_CompanyTYpe(int DOCU)
        {

            int? Identificationlastcode = 0;
            var identificationList = await _sharedHelpers.GetIddefinition();
            var IdentificationType = identificationList.Where(i => i.Id == 0 && i.SystemConstant == DOCU);
            Identificationlastcode = IdentificationType?.LastOrDefault()?.Id;
            if (Identificationlastcode >= 1)
            {
                var resultSetattachment = await _sharedHelpers.AttachmentByReferencenadCat(Identificationlastcode, 1451);
                var Comresultset = resultSetattachment.Select(r => new CompanyDocumentAttachementviewModel()
                {
                    company_attcode = r.Id,
                    company_reference = r.Reference,
                    company_url = r.Url,
                    company_catagory = r.Category,
                    company_description = r.Description

                }).ToList();

                return Json(new { data = Comresultset });
            }
            else
            {
                return Json(new EmptyResult());
            }
        }
        public async Task<IActionResult> Retrive_Attachement_CompanyeachTYpe(int Attach)
        {

            var resultSetattachment = await _sharedHelpers.AttachmentByReferencenadCat(Attach, 1451);
            var Comresulteachset = resultSetattachment.Select(r => new CompanyDocumentAttachementEachviewModel()
            {
                company_eachattcode = r.Id,
                company_eachreference = r.Reference,
                company_eachurl = r.Url,
                company_eachcatagory = r.Category,
                company_eachdescription = r.Description

            }).ToList();

            return Json(new { data = Comresulteachset });
        }


        public async Task<IActionResult> getcompanyDoumentdetail(int id)
        {
           var iden = await _sharedHelpers.GetIdentificationsByTypeAndReference(id, 1);
            var orgunitaddress = new Companymodel() { identificationDTOs = iden };

            return PartialView("companydoumentDetail", orgunitaddress);
        }

        public async Task<IActionResult> JobdescriptionList(int idd)
        {
            var lookupList = GeneralBufferHolder.SystemConstants;
            var orgunitlist = await _sharedHelpers.GetConsigneeunit();
            List<JobDescriptionDTO> getjobdesc = new List<JobDescriptionDTO>();
            if (idd != 0)
            {
                getjobdesc = await _sharedHelpers.getjobdescriptionByorgUnitDefn(idd);

            }
            var orgunitaddress = new Companymodel() { _jobDescriptions = getjobdesc };

            return PartialView("_roledetail", orgunitaddress);
        }
        #endregion

        #region Delete

        public async Task<IActionResult> Deleterelation(int ID)
        {
            await _sharedHelpers.DeleteRelationById(ID);

            return Json("");
        }

        //public async Task<IActionResult> DeleteIndustryInvolved(string ID)
        //{

        //    await _sharedHelpers.DeleteIndustryInvolvedByCode(ID);

        //    return Json("");
        //}

        public async Task<IActionResult> DeleteIdentification(int ID)
        {
            var getiden = await _sharedHelpers.GetIIdentificationById(ID);
            var getattach = await _sharedHelpers.AttachmentByReference(getiden.Consignee);
            var deltiden = await _sharedHelpers.DeleteIdentificationById(ID);
            var deltattach = await _sharedHelpers.DeleteAttachment(getattach.FirstOrDefault()?.Id);

            return Json("Deleted Succssfully");
        }
        public async Task<IActionResult> DeleteOrganizationUnit(int code, string desc)
        {

            var resultset = "";
            var check = false;
            var orgunitdefn = await _sharedHelpers.GetConsigneeunit();
            if (desc == "Branch")
            {
                var branch = await _sharedHelpers.GetdeviceByConsigneeUnit(code);
                var devicevalue = branch?.Where(o => o.Id == code).ToList();


                if (devicevalue?.Count() >= 1)
                {
                    resultset = "First Delete Device and Other Child Organization Units ";
                    check = false;
                }
                else
                {
                    var OrgUnitDefparentt = orgunitdefn?.Where(x => x.ParentId == code).ToList();
                    if (OrgUnitDefparentt.Count() > 0)
                    {
                        resultset = "First Delete child";
                        check = false;
                    }
                    else
                    {

                        var value = orgunitdefn?.Where(o => o.Id == code);

                        foreach (var valu in value)
                        {
                            var delteorgunit = await _sharedHelpers.DeleteCosnigneeunitByid(valu.Id);


                        }
                        var delteorgunitdeg = await _sharedHelpers.DeleteCosnigneeunitByid(code);
                        resultset = "Deleted Successfully";
                        check = true;
                    }
                }

            }
            else if (desc == "Role")
            {

                var valuesall = await _sharedHelpers.SelectAllUserRoleMapper();
                var valuesAll = valuesall?.Where(x => x.Role == code).ToList();
                if (valuesAll.Count() > 0)
                {
                    resultset = "Used by User Role Mapper ";
                    check = false;

                }
                else
                {
                  
                    var OrgUnitDefparentt = orgunitdefn?.Where(x => x.ParentId == code).ToList();
                    if (OrgUnitDefparentt.Count() > 0)
                    {
                        resultset = "First Delete child";
                        check = false;
                    }
                    else
                    {

                        var value = orgunitdefn?.Where(o => o.Id == code);

                        foreach (var valu in value)
                        {
                            var delteorgunit = await _sharedHelpers.DeleteCosnigneeunitByid(valu.Id);


                        }
                        var delteorgunitdeg = await _sharedHelpers.DeleteCosnigneeunitByid(code);
                        resultset = "Deleted Successfully";
                        check = true;
                    }
                }
            }
            else
            {
                var OrgUnitDefparentt = orgunitdefn?.Where(x => x.ParentId == code).ToList();
                if (OrgUnitDefparentt.Count() > 0)
                {
                    resultset = "First Delete child";
                    check = false;

                }
                else
                {
                    var value = orgunitdefn?.Where(o => o.Id == code);

                    foreach (var valu in value)
                    {
                        var delteorgunit = await _sharedHelpers.DeleteCosnigneeunitByid(valu.Id);
                    }
                    var delteorgunitdeg = await _sharedHelpers.DeleteCosnigneeunitByid(code);

                    if (delteorgunitdeg.ToString() != null)
                    {
                        resultset = "Deleted Successfully";
                        check = true;
                    }
                    else
                    {
                        resultset = "Not Deleted Used by  Other  Transaction";
                        check = false;
                    }
                }
            }
            return Json(new { result = resultset, checkstatus = check });
        }
        public async Task<IActionResult> DeleteJobDescriptionById(int code)
        {
            await _sharedHelpers.DeletejobDesc(code);

            return Json("Deleted Successfully");
        }
        public async Task<IActionResult> DeleteBankAccountDetail(int code)
        {
            await _sharedHelpers.DeleteBankDetailById(code);

            return Json(new { });
        }
        public async Task<IActionResult> DeleteAttachment2(int code)
        {

            await _sharedHelpers.DeleteAttachment(code);

            return Json(new { });

        }
        public async Task<IActionResult> DeleteAttachmentItem(int code)
        {
            string _deltedfile = _sharedHelpers.GetattachmentbyId(code)?.Result?.FirstOrDefault()?.Url?.ToString();

            if (OnDelete(_deltedfile))
            {
                await _sharedHelpers.DeleteAttachment(code);
            }
            return Json(new { });
        }

        public bool OnDelete(string path)
        {
            try
            {
                string FtpserverIP = path;

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(FtpserverIP);
                request.Method = WebRequestMethods.Ftp.DeleteFile;
                request.Credentials = new NetworkCredential(userName, passWord);

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Console.WriteLine("Delete status: {0}", response.StatusDescription);
                response.Close();

                return true;

            }
            catch (Exception)
            {

                return false;
            }

            //using (FtpWebResponse response = (FtpWebResponse) request.GetResponse())
            //{
            //    return response.StatusDescription;
            //}
            //response.Close();
        }

        [HttpPost]
        public async Task<IActionResult> DisplayNote()
        {
            var modelll = await _sharedHelpers.GetConsigneeByType(1);
            var modell = modelll.Where(x => x.Note != null)?.FirstOrDefault();
            return Json(new { note = modell?.Note,id = modell?.Id });
        }
        public async Task<IActionResult> DeleteAttachment(int code)
        {
            //string _deltedfile = _sharedHelpers.GetattachmentbyCode(code)?.Result?.FirstOrDefault()?.url?.ToString();


            //if (OnDelete(_deltedfile))
            //{
                await _sharedHelpers.DeleteAttachment(code);
            

            return Json(new { });

        } 
        public async Task<IActionResult> GetIndustryInvolved()
        {

            Companymodel modell = new Companymodel();
            return PartialView("IndustryInvolvedList", modell);
        } 
        public async Task<IActionResult> GetIndustryBycode(int code)
        {
            var induList = await _sharedHelpers.GetConsigneeunitById(code);
            var indu = induList?.FirstOrDefault();
            return Json(new {code = indu.Id,involve = indu.Name,indus = indu.Specialization });
        }
        public async Task<IActionResult> Deleteconsignneunit(int code)
        {
            var induList = await _sharedHelpers.DeleteCosnigneeunitByid(code);
            return Json("Deleted Sucessfully");
        }
        //public bool OnDelete(string path)
        //{
        //    try
        //    {
        //        string FtpserverIP = path;

        //        FtpWebRequest request = (FtpWebRequest)WebRequest.Create(FtpserverIP);
        //        request.Method = WebRequestMethods.Ftp.DeleteFile;
        //        request.Credentials = new NetworkCredential(userName, passWord);

        //        FtpWebResponse response = (FtpWebResponse)request.GetResponse();
        //        Console.WriteLine("Delete status: {0}", response.StatusDescription);
        //        response.Close();

        //        return true;

        //    }
        //    catch (Exception)
        //    {

        //        return false;
        //    }

        //    //using (FtpWebResponse response = (FtpWebResponse) request.GetResponse())
        //    //{
        //    //    return response.StatusDescription;
        //    //}
        //    //response.Close();
        //}


        [HttpPost]
        public async Task<IActionResult> DisplayCnetGps(int compCode)
        {
            var maplocationl = await _sharedHelpers.GetConsigneeunitById(compCode);
            var maplocation = maplocationl?.FirstOrDefault();
            return Json(new { lat = maplocation?.Latitude,longg = maplocation?.Longitude });
        }
        #endregion
        public IActionResult fontsixes()
        {
            var model = new Companymodel();
            model.fontsize.ToList();
            return PartialView(@"~/Views/Company/fontModel.cshtml", model);
        }
    }
 }

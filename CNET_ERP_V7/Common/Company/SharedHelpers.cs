using CNET_V7_Domain.Domain.ConsigneeSchema;
using CNET_V7_Domain.Domain.SecuritySchema;
using CNET_ERP_V7.WebConstants;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using CNET_V7_Domain;
using CNET_V7_Domain.Misc;
using CNET_ERP_V7.Models.FramworkModels;
using System.Security.Cryptography;
using System.Text;
using CNET_V7_Domain.Domain.CommonSchema;
using CNET_V7_Domain.Domain.SettingSchema;
using CNET_ERP_V7.Models.SubSytsemModel;
using CNET_V7_Domain.Domain.AccountingSchema;
using CNET_V7_Domain.Domain.ArticleSchema;
using CNET_V7_Domain.Domain.TransactionSchema;
using DevExtreme.AspNet.Mvc.Builders;
using CNET_V7_Domain.Domain.HrmsSchema;
using CNET_V7_Entities.DataModels;
using CNET_V7_Domain.Domain.ViewSchema;
using CNET_V7_Domain.Domain.PmsSchema;
using CNET_V7_Domain.Misc.CommonTypes;
using CNET_V7_Entities.CustomReturnTypes;

namespace CNET_ERP_V7.Common.Company
{
    public class SharedHelpers
    {
        private readonly HttpClient _httpClient;
        private ConsigneeDTO? _organization;
        private UserDTO? _loggedInUser;
        public SharedHelpers(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("mainclient");
        }

        public virtual async Task<List<SystemConstantDTO>> GetAllSytemConstants()
        {
            var response = await _httpClient.GetAsync("SystemConstant/dynamic");
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var sysconstDto_ = JsonConvert.DeserializeObject<List<SystemConstantDTO>>(jsysconstDto_);

            return sysconstDto_;
        }
        public virtual async Task<List<TaxDTO>> GetAllTaxs()
        {
            var response = await _httpClient.GetAsync("Tax/dynamic");
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var taxconstDto_ = JsonConvert.DeserializeObject<List<TaxDTO>>(jsysconstDto_);

            return taxconstDto_;
        }
        public virtual async Task<List<ViewActivepersonRoleDTO>> GetActiveUserPersonList()
        {
            var response = await _httpClient.GetAsync("ViewActivepersonRole/dynamic");
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var taxconstDto_ = JsonConvert.DeserializeObject<List<ViewActivepersonRoleDTO>>(jsysconstDto_);

            return taxconstDto_;
        }
        public virtual async Task<List<PeriodDTO>> GetAllPeriods()
        {
            var response = await _httpClient.GetAsync("Period/dynamic");
            if (!response.IsSuccessStatusCode)
                return null;

            var jperiodDto = await response.Content.ReadAsStringAsync();
            var periodDto_ = JsonConvert.DeserializeObject<List<PeriodDTO>>(jperiodDto);

            return periodDto_;
        }
        public async Task<List<CurrencyDTO>> GetAllCurrencies()
        {
            var currency = await GetDynamicData<List<CurrencyDTO>>("Currency");
            return currency ?? new List<CurrencyDTO>();
        }
        public async Task<List<ConsigneeUnitDTO>> GetAllConsigneeUnits()
        {
            var consigneeunits = await GetDynamicData<List<ConsigneeUnitDTO>>("ConsigneeUnit");
            return consigneeunits ?? new List<ConsigneeUnitDTO>();
        }
        public async Task<List<PreferenceDTO>> GetAllPreferences()
        {
            var relationstates = await GetDynamicData<List<PreferenceDTO>>("Preference");
            return relationstates ?? new List<PreferenceDTO>();
        }
        public async Task<T> GetDynamicData<T>(string Database)
        {
            string uri = string.Format(Database + "/dynamic");

            return await GetReqAsync<T>(uri);
        }
        public virtual async Task<SystemInitDTO> GetCompanyInfo(string? _tin)
        {
            var response = await _httpClient.GetAsync("SysInitialize?deviceName=web&tin=" + _tin + "&platform=0&isWeb=true");
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysDto = await response.Content.ReadAsStringAsync();
            var sysDto_ = JsonConvert.DeserializeObject<SystemInitDTO>(jsysDto);
            return sysDto_;
        }
       
        public virtual async Task<List<RoleActivityDTO>> GetRoleactivityByrole(int role)
        {

            var response = await _httpClient.GetAsync("RoleActivity/filter?role=" + role);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var ObjectStedenDto_ = JsonConvert.DeserializeObject<List<RoleActivityDTO>>(jsysconstDto_);

            return ObjectStedenDto_;
        }
        public virtual async Task<RoleActivityDTO> GetRoleactivityById(int _idd)
        {

            var response = await _httpClient.GetAsync("RoleActivity/filter?id=" + _idd);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var ObjectStedenDto_ = JsonConvert.DeserializeObject<List<RoleActivityDTO>>(jsysconstDto_);

            return ObjectStedenDto_?.FirstOrDefault();
        }
        public virtual async Task<List<LookupDTO>> GetLookUpByType(string type)
        {

            var response = await _httpClient.GetAsync("Lookup/filter?type=" + type);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var lookupdto_ = JsonConvert.DeserializeObject<List<LookupDTO>>(jsysconstDto_);

            return lookupdto_;
        } 
        public virtual async Task<List<string>> GetViewColoumnsByViewName(string objectTYpe)
        {

            var response = await _httpClient.GetAsync("CommonLibrary/list-column?objectType=" + objectTYpe);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var lookupdto_ = JsonConvert.DeserializeObject<ResponseModel<List<string>>>(jsysconstDto_);

            return lookupdto_.Data;
        }
        public virtual async Task<List<IdDefinations>> GetIdDefination(string component)
        {
            var response = await _httpClient.GetAsync("CommonLibrary/GetIdDefination?pointer=" + component);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var lookupdto_ = JsonConvert.DeserializeObject<List<IdDefinations>>(jsysconstDto_);

            return lookupdto_;
        } 

        public virtual async Task<List<LookupDTO>> GetLookUpById(int? id)
        {

            var response = await _httpClient.GetAsync("Lookup/filter?id=" + id);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var lookupdto_ = JsonConvert.DeserializeObject<List<LookupDTO>>(jsysconstDto_);

            return lookupdto_;
        } 
        public virtual async Task<List<TermDefinitionDTO>> GetTermDefinitionByVoucerDefn(int voucher)
        {

            var response = await _httpClient.GetAsync("TermDefinition/filter?voucherDefn=" + voucher);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var _tremDefndto_ = JsonConvert.DeserializeObject<List<TermDefinitionDTO>>(jsysconstDto_);

            return _tremDefndto_;
        }
      
        public virtual async Task<List<ViewFunctWithAccessMDTO>> GetFuncwithAccessMatView()
        {

            var response = await _httpClient.GetAsync("ViewFunctWithAccessM/dynamic");
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var Taxto = JsonConvert.DeserializeObject<List<ViewFunctWithAccessMDTO>>(jsysconstDto_);
            return Taxto;
        } 
        public virtual async Task<List<FunctWithAccessMDTO>> GetFunctWithAccessMatrix(int subsys,int category,int role)
        {
            string endpoint = "CommonLibrary/get-func-with-access-m";
            string queryString = $"?role={role}&category={category}&subSys={subsys}";
            string requestUrl = $"{endpoint}{queryString}";
            var response = await _httpClient.GetAsync(requestUrl);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var Taxto = JsonConvert.DeserializeObject<ResponseModel<List<FunctWithAccessMDTO>>>(jsysconstDto_);
            return Taxto.Data;

        }
        public virtual async Task<List<FunctWithAccessMDTO>> GetFunctWithAccessMatrixByfun(int func,int? category,int role)
        {
            string endpoint = "CommonLibrary/get-func-with-access-m";
            string queryString = $"?role={role}&category={category}&function={func}";
            string requestUrl = $"{endpoint}{queryString}";
            var response = await _httpClient.GetAsync(requestUrl);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var Taxto = JsonConvert.DeserializeObject<ResponseModel<List<FunctWithAccessMDTO>>>(jsysconstDto_);
            return Taxto.Data;

        } 
        public virtual async Task<List<FunctWithAccessMDTO>> GetFunctWithAccessMatrixByfunSubsys(int role, int func)
        {
            string endpoint = "CommonLibrary/get-func-with-access-m";
            string queryString = $"?role={role}&function={func}";
            string requestUrl = $"{endpoint}{queryString}";
            var response = await _httpClient.GetAsync(requestUrl);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var Taxto = JsonConvert.DeserializeObject<ResponseModel<List<FunctWithAccessMDTO>>>(jsysconstDto_);
            return Taxto.Data;

        } 
        public virtual async Task<List<ViewFunctWithAccessMDTO>> GetFuncwithAccessMatViewByCatandRole(int cate,int type)
        {
            string endpoint = "ViewFunctWithAccessM/filter";
            string queryString = $"?category={cate}&role={type}";
            string requestUrl = $"{endpoint}{queryString}";
            var response = await _httpClient.GetAsync(requestUrl);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var Taxto = JsonConvert.DeserializeObject<List<ViewFunctWithAccessMDTO>>(jsysconstDto_);
            return Taxto;

        } 
        public virtual async Task<List<TaxDTO>> GetTaxByCategory(int cat)
        {

            var response = await _httpClient.GetAsync("Tax/filter?category="+ cat);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var Taxto = JsonConvert.DeserializeObject<List<TaxDTO>>(jsysconstDto_);
            return Taxto;

        }  
        public virtual async Task<List<TaxDTO>> GetTaxByCode(int? id)
        {

            var response = await _httpClient.GetAsync("Tax/filter?id=" + id);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var Taxto = JsonConvert.DeserializeObject<List<TaxDTO>>(jsysconstDto_);
            return Taxto;

        }
        public virtual async Task<List<ConsigneeUnitDTO>> GetConsigneeunit()
        {
            var response = await _httpClient.GetAsync("ConsigneeUnit/dynamic?requiredFields=id,consignee,name,type,specialization,parentId,abrivation,remark");
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var Taxto = JsonConvert.DeserializeObject<List<ConsigneeUnitDTO>>(jsysconstDto_);
            return Taxto;
        } 
        public virtual async Task<List<Lookup>> SelectLookUp() 
        {
            var response = await _httpClient.GetAsync("ConsigneeUnit/dynamic?requiredFields=id,consignee,name,Type");
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var Taxto = JsonConvert.DeserializeObject<List<Lookup>>(jsysconstDto_);
            return Taxto;
        }  
        public virtual async Task<List<UserRoleMapperDTO>> SelectAllUserRoleMapper()
        {
            var response = await _httpClient.GetAsync("UserRoleMapper/dynamic");
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var Taxto = JsonConvert.DeserializeObject<List<UserRoleMapperDTO>>(jsysconstDto_);
            return Taxto;
        }
        public virtual async Task<List<RequiredGslDTO>> GetRequrGslByvoucherDefn(int voucher)
        {
            var response = await _httpClient.GetAsync("RequiredGsl/filter?voucherDefn=" + voucher);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var rquDto = JsonConvert.DeserializeObject<List<RequiredGslDTO>>(jsysconstDto_);
            return rquDto;
        } 
        public virtual async Task<List<JobDescriptionDTO>> getjobdescriptionByorgUnitDefn(int orgunit)
        {
            var response = await _httpClient.GetAsync("JobDescription/filter?consigneeUnit=" + orgunit);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var rquDto = JsonConvert.DeserializeObject<List<JobDescriptionDTO>>(jsysconstDto_);
            return rquDto;
        }
        public virtual async Task<List<RequiredGsldetailDTO>> SelectRequiredGSLDetailByRequgsl(int requgsl)
        {
            var response = await _httpClient.GetAsync("RequiredGsldetail/filter?requiredGSL=" + requgsl);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var rqudlDto = JsonConvert.DeserializeObject<List<RequiredGsldetailDTO>>(jsysconstDto_);
            return rqudlDto;
        }
        public virtual async Task<List<ConsigneeDTO>> GetConsignee()
        {
            var response = await _httpClient.GetAsync("Consignee/dynamic");
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var Taxto = JsonConvert.DeserializeObject<List<ConsigneeDTO>>(jsysconstDto_);
            return Taxto;
        } 
        public virtual async Task<List<ConsigneeDTO>> GetConsigneeByType(int type)
        {
            var response = await _httpClient.GetAsync("Consignee/filter?gslType=" + type);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var Taxto = JsonConvert.DeserializeObject<List<ConsigneeDTO>>(jsysconstDto_);
            return Taxto;
        }
       
        public virtual async Task<List<ConsigneeUnitDTO>> GetConsigneeByType1(int type)
        {
            var response = await _httpClient.GetAsync("ConsigneeUnit/filter?type=" + type);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var Taxto = JsonConvert.DeserializeObject<List<ConsigneeUnitDTO>>(jsysconstDto_);
            return Taxto;

        }
        public virtual async Task<List<ConsigneeUnitDTO>> GetConsigneeByremark(string rema)
        {
            var response = await _httpClient.GetAsync("ConsigneeUnit/filter?remark=" + rema);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var Taxto = JsonConvert.DeserializeObject<List<ConsigneeUnitDTO>>(jsysconstDto_);
            return Taxto;

        }
        public virtual async Task<List<VoucherStoreMappingDTO>> VoucherStoreMappingVoucher(int vouchDefn, bool source)
        {
            string endpoint = "VoucherStoreMapping/filter";
            string queryString = $"?voucherDefinition={vouchDefn}&isSource={source}";
            string requestUrl = $"{endpoint}{queryString}";
            var response = await _httpClient.GetAsync(requestUrl);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var vstoto = JsonConvert.DeserializeObject<List<VoucherStoreMappingDTO>>(jsysconstDto_);
            return vstoto;

        }
        public virtual async Task<List<IdentificationDTO>> GetIdentificationsByTypeAndReference(int type, int consignee)
        {
            string endpoint = "Identification/filter";
            string queryString = $"?type={type}&consignee={consignee}";
            string requestUrl = $"{endpoint}{queryString}";
            var response = await _httpClient.GetAsync(requestUrl);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var vstoto = JsonConvert.DeserializeObject<List<IdentificationDTO>>(jsysconstDto_);
            return vstoto;

        }
        public virtual async Task<List<ConsigneeUnitDTO>> GetConsigneeunitById(int _id)
        {
            var response = await _httpClient.GetAsync("ConsigneeUnit/filter?id=" + _id);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var consDto = JsonConvert.DeserializeObject<List<ConsigneeUnitDTO>>(jsysconstDto_);
            return consDto;

        } 
        public virtual async Task<JobDescriptionDTO> GetjobdescriptionByorgId(int _id)
        {
            var response = await _httpClient.GetAsync("JobDescription/filter?id=" + _id);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var _JobDesn = JsonConvert.DeserializeObject<List<JobDescriptionDTO>>(jsysconstDto_);
            return _JobDesn?.FirstOrDefault();

        } 
        public virtual async Task<List<ConsigneeDTO>> GetConsigneeById(int _id)
        {
            var response = await _httpClient.GetAsync("Consignee/filter?id=" + _id);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var consDto = JsonConvert.DeserializeObject<List<ConsigneeDTO>>(jsysconstDto_);
            return consDto;

        } 
        public virtual async Task<List<ConsigneeUnitDTO>> GetConsigneeByconsignee(int? _id)
        {
            var response = await _httpClient.GetAsync("ConsigneeUnit/filter?consignee=" + _id);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var consDto = JsonConvert.DeserializeObject<List<ConsigneeUnitDTO>>(jsysconstDto_);
            return consDto;

        }
        public virtual async Task<List<IddefinitionDTO>> GetIddefinition()
        {
            var response = await _httpClient.GetAsync("Iddefinition/dynamic");
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var idDendto = JsonConvert.DeserializeObject<List<IddefinitionDTO>>(jsysconstDto_);
            return idDendto;

        } 
        public virtual async Task<List<AttachmentDTO>> AttachmentByReference(int? refe)
        {
            var response = await _httpClient.GetAsync("Attachment/filter?reference=" + refe);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var idDendto = JsonConvert.DeserializeObject<List<AttachmentDTO>>(jsysconstDto_);
            return idDendto;

        } 
        public virtual async Task<List<AttachmentDTO>> AttachmentByReferencenadCat(int? refe,int cat)
        {
            string endpoint = "Attachment/filter";
            string queryString = $"?reference={refe}&category={cat}";
            string requestUrl = $"{endpoint}{queryString}";
            var response = await _httpClient.GetAsync(requestUrl);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var idDendto = JsonConvert.DeserializeObject<List<AttachmentDTO>>(jsysconstDto_);
            return idDendto;
        }
        public virtual async Task<Identification> GetIIdentificationById(int id)
        {
            var response = await _httpClient.GetAsync("Identification/filter?id=" + id);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var idDendto = JsonConvert.DeserializeObject<List<Identification>>(jsysconstDto_);
            return idDendto?.FirstOrDefault();

        } 
        public virtual async Task<List<IdentificationDTO>> GetIIdentification()
        {
            var response = await _httpClient.GetAsync("Identification/dynamic");
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var idDendto = JsonConvert.DeserializeObject<List<IdentificationDTO>>(jsysconstDto_);
            return idDendto;

        }
        public virtual async Task<List<IddefinitionDTO>> GetIddefinitionByPointer(int _pntr)
        {
            var response = await _httpClient.GetAsync("Iddefinition/filter?pointer=" + _pntr);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var idDendto = JsonConvert.DeserializeObject<List<IddefinitionDTO>>(jsysconstDto_);
            return idDendto;

        } 
        public virtual async Task<List<IddefinitionDTO>> GetIddefinitionById(int _id)
        {
            var response = await _httpClient.GetAsync("Iddefinition/filter?id=" + _id);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var idDendto = JsonConvert.DeserializeObject<List<IddefinitionDTO>>(jsysconstDto_);
            return idDendto;

        }
        public virtual async Task<List<ViewDeviceByReferenceDTO>> SelectAllDeviceByOrganizationalUnitDefinition(int consiUnit)
        {
            var response = await _httpClient.GetAsync("ViewDeviceByReference/filter?consigneeUnit=" + consiUnit);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var devicevdto = JsonConvert.DeserializeObject<List<ViewDeviceByReferenceDTO>>(jsysconstDto_);
            return devicevdto;

        } 
        public virtual async Task<List<ViewDeviceByReferenceDTO>> GetViewDeviceByReferenceBypreference(int preference)
        {
            var response = await _httpClient.GetAsync("ViewDeviceByReference/filter?preference=" + preference);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var devicevdto = JsonConvert.DeserializeObject<List<ViewDeviceByReferenceDTO>>(jsysconstDto_);
            return devicevdto;

        } 
        public virtual async Task<List<ViewDeviceByReferenceDTO>> GetViewDeviceByReferenceByartilce(int art)
        {
            var response = await _httpClient.GetAsync("ViewDeviceByReference/filter?article=" + art);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var devicevdto = JsonConvert.DeserializeObject<List<ViewDeviceByReferenceDTO>>(jsysconstDto_);
            return devicevdto;

        }
        public virtual async Task<List<IdsettingDTO>> SelectIdsettingByConsUnitandRefe(int? orgunit, int? refe)
        {
            string endpoint = "Idsetting/filter";
            string queryString = $"?consigneeUnit={orgunit}&reference={refe}";
            string requestUrl = $"{endpoint}{queryString}";
            var response = await _httpClient.GetAsync(requestUrl);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var idstdto = JsonConvert.DeserializeObject<List<IdsettingDTO>>(jsysconstDto_);
            return idstdto;

        }
        public virtual async Task<List<VwWorkFlowByReferenceViewDTO>> GetWorkFlowsByreference(int _compo, int reference)
        {
            string endpoint = "VwWorkFlowByReferenceView/filter";
            string queryString = $"?reference={reference}";
            string requestUrl = $"{endpoint}{queryString}";

            var response = await _httpClient.GetAsync(requestUrl);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var Cworkflowdto = JsonConvert.DeserializeObject<List<VwWorkFlowByReferenceViewDTO>>(jsysconstDto_);

            return Cworkflowdto;
        } 
        public virtual async Task<List<VwRoleActivityViewDTO>> GetRoleActivityWithRange(int _role, int reference)
        {
            string endpoint = "VwRoleActivityView/filter";
            string queryString = $"?role={_role}&reference={reference}";
            string requestUrl = $"{endpoint}{queryString}";

            var response = await _httpClient.GetAsync(requestUrl);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var Cworkflowdto = JsonConvert.DeserializeObject<List<VwRoleActivityViewDTO>>(jsysconstDto_);

            return Cworkflowdto;
        }
        public virtual async Task<List<VwWorkFlowByReferenceViewDTO>> GetWorkFlowsByreferenceType( int reference)
        {
            string endpoint = "VwWorkFlowByReferenceView/filter";
            string queryString = $"?reference={reference}";
            string requestUrl = $"{endpoint}{queryString}";

            var response = await _httpClient.GetAsync(requestUrl);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var Cworkflowdto = JsonConvert.DeserializeObject<List<VwWorkFlowByReferenceViewDTO>>(jsysconstDto_);

            return Cworkflowdto;
        } 
        public virtual async Task<List<VwWorkFlowByReferenceViewDTO>> GetWorkFlowsByreferenceTypeandId( int reference,int _idd)
        {
            string endpoint = "VwWorkFlowByReferenceView/filter";
            string queryString = $"?reference={reference}&id={_idd}";
            string requestUrl = $"{endpoint}{queryString}";

            var response = await _httpClient.GetAsync(requestUrl);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var Cworkflowdto = JsonConvert.DeserializeObject<List<VwWorkFlowByReferenceViewDTO>>(jsysconstDto_);

            return Cworkflowdto;
        }
        public virtual async Task<List<VwWorkFlowByReferenceViewDTO>> GetWorkFlowsByid(int _id)
        {

            var response = await _httpClient.GetAsync("VwWorkFlowByReferenceView/filter?id=" + _id);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var Cworkflowdto = JsonConvert.DeserializeObject<List<VwWorkFlowByReferenceViewDTO>>(jsysconstDto_);

            return Cworkflowdto;
        }
        public virtual async Task<List<PreferenceAccessDTO>> GetPreferentialByid(int _id)
        {

            var response = await _httpClient.GetAsync("PreferenceAccess/filter?id=" + _id);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var Cworkflowdto = JsonConvert.DeserializeObject<List<PreferenceAccessDTO>>(jsysconstDto_);

            return Cworkflowdto;
        }
        public virtual async Task<List<IdsettingDTO>> SelectIdSetting()
        {
            var response = await _httpClient.GetAsync("Idsetting/dynamic");
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var idstdto = JsonConvert.DeserializeObject<List<IdsettingDTO>>(jsysconstDto_);
            return idstdto;

        }
        public virtual async Task<List<DeviceDTO>> SelectDeviceBYId(int? id)
        {
            var response = await _httpClient.GetAsync("Device/filter?id=" + id);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var devdto = JsonConvert.DeserializeObject<List<DeviceDTO>>(jsysconstDto_);
            return devdto;

        }  
        public virtual async Task<List<DeviceDTO>> GetdeviceByConnecType(int conntype)
        {
            var response = await _httpClient.GetAsync("Device/filter?connectionType=" + conntype);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var devdto = JsonConvert.DeserializeObject<List<DeviceDTO>>(jsysconstDto_);
            return devdto;

        }
        public virtual async Task<List<DeviceDTO>> GetdeviceByConsigneeUnit(int cUnit)
        {
            var response = await _httpClient.GetAsync("Device/filter?consigneeUnit=" + cUnit);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var devdto = JsonConvert.DeserializeObject<List<DeviceDTO>>(jsysconstDto_);
            return devdto;

        }


        public virtual async Task<List<UserDTO>> GetUser()
        {
            var response = await _httpClient.GetAsync("User/dynamic");
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var Userdto = JsonConvert.DeserializeObject<List<UserDTO>>(jsysconstDto_);
            return Userdto;

        }
        public virtual async Task<List<UserDTO>> GetUserByPerson(int person)
        {
            var response = await _httpClient.GetAsync("User/filter?person=" + person);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var Userdto = JsonConvert.DeserializeObject<List<UserDTO>>(jsysconstDto_);
            return Userdto;

        }
        public virtual async Task<List<AccountDTO>> GetAccount()
        {
            var response = await _httpClient.GetAsync("Account/dynamic");
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var accDTO = JsonConvert.DeserializeObject<List<AccountDTO>>(jsysconstDto_);
            return accDTO;

        } 
        public virtual async Task<List<AccountMapDTO>> GetAccountMapByrefrence(string prefcode)
        {
            var response = await _httpClient.GetAsync("AccountMap/filter?reference=" + prefcode);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var accDTO = JsonConvert.DeserializeObject<List<AccountMapDTO>>(jsysconstDto_);
            return accDTO;

        } 
        public virtual async Task<List<AccountMapDTO>> GetAccountMapById(int id)
        {
            var response = await _httpClient.GetAsync("AccountMap/filter?id=" + id);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var accDTO = JsonConvert.DeserializeObject<List<AccountMapDTO>>(jsysconstDto_);
            return accDTO;

        }
        public virtual async Task<List<AccountDTO>> GetAccountById(int id)
        {
            var response = await _httpClient.GetAsync("Account/filter?id=" + id);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var accDTO = JsonConvert.DeserializeObject<List<AccountDTO>>(jsysconstDto_);
            return accDTO;

        }  
        public virtual async Task<List<ControlAccountDTO>> GetControlAccount()
        {
            var response = await _httpClient.GetAsync("ControlAccount/dynamic");
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var accDTO = JsonConvert.DeserializeObject<List<ControlAccountDTO>>(jsysconstDto_);
            return accDTO;

        } 
        public virtual async Task<List<GslacctRequirementDTO>> GetGSLAcctRequirementBygslType(int gsltype)
        {
            var response = await _httpClient.GetAsync("GslacctRequirement/filter?gslTypeList=" + gsltype);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var accDTO = JsonConvert.DeserializeObject<List<GslacctRequirementDTO>>(jsysconstDto_);
            return accDTO;

        }
        public virtual async Task<List<GslacctRequirementDTO>> GetGSLAcctRequirementBycode(int id)
        {
            var response = await _httpClient.GetAsync("GslacctRequirement/filter?id=" + id);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var accDTO = JsonConvert.DeserializeObject<List<GslacctRequirementDTO>>(jsysconstDto_);
            return accDTO;

        }
        public virtual async Task<List<PreferenceDTO>> GetPreferenceByDescription(string orgn)
        {
            var response = await _httpClient.GetAsync("Preference/filter?description=" + orgn);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var accDTO = JsonConvert.DeserializeObject<List<PreferenceDTO>>(jsysconstDto_);
            return accDTO;

        }
        public virtual async Task<string> CNETIdGenerater()
        {
            var response = await _httpClient.GetAsync("IDGenerate?objectType=consignee&reference=26&generationType=0&consigneeUnit=18965&isWeb=true");
            if (!response.IsSuccessStatusCode)
                return null;
            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var iddto = JsonConvert.DeserializeObject<ResponseModel<string>>(jsysconstDto_);
            return iddto?.Data;


        }
        public virtual async Task<List<ConfigurationDTO>> GetConfigurationByRefandPref(string reference, int preference)
        {
            string endpoint = "Configuration/filter";
            string queryString = $"?reference={reference}&Pointer={preference}";
            string requestUrl = $"{endpoint}{queryString}";

            var response = await _httpClient.GetAsync(requestUrl);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var Confgdto = JsonConvert.DeserializeObject<List<ConfigurationDTO>>(jsysconstDto_);

            return Confgdto;
        }
        public virtual async Task<List<ConfigurationDTO>> DeleteConfigurationByReferenceAndPreference(string reference, int preference)
        {
            string endpoint = "Configuration/deletebycondition";
            string queryString = $"?reference={reference}&Pointer={preference}";
            string requestUrl = $"{endpoint}{queryString}";

            var response = await _httpClient.DeleteAsync(requestUrl);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var Confgdto = JsonConvert.DeserializeObject<List<ConfigurationDTO>>(jsysconstDto_);

            return Confgdto;
        }

        public virtual async Task<List<SystemConstantDTO>> SelectsysConstantByTypeAndCategory(string type, string _category)
        {
            string endpoint = "SystemConstant/filter";
            string queryString = $"?type={type}&category={_category}";
            string requestUrl = $"{endpoint}{queryString}";

            var response = await _httpClient.GetAsync(requestUrl);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var sysdto = JsonConvert.DeserializeObject<List<SystemConstantDTO>>(jsysconstDto_);

            return sysdto;
        }

        public virtual async Task<List<SystemConstantDTO>> GetdeviceById(int? id)
        {

            var response = await _httpClient.GetAsync("SystemConstant/filter?id=" + id);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var SysCdto = JsonConvert.DeserializeObject<List<SystemConstantDTO>>(jsysconstDto_);

            return SysCdto;
        }
        public virtual async Task<List<ActivityDefinitionDTO>> GetActivityDefinitionByrefrence(int? refe)
        {

            var response = await _httpClient.GetAsync("ActivityDefinition/filter?reference=" + refe);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var _actCdto = JsonConvert.DeserializeObject<List<ActivityDefinitionDTO>>(jsysconstDto_);

            return _actCdto;
        } 
        public virtual async Task<List<ActivityDefinitionDTO>> GetActivityDefinitionByDesc(string desc)
        {

            var response = await _httpClient.GetAsync("ActivityDefinition/filter?description=" + desc);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var _actCdto = JsonConvert.DeserializeObject<List<ActivityDefinitionDTO>>(jsysconstDto_);

            return _actCdto;
        }
        public virtual async Task<List<SystemConstantDTO>> GetsystemConstantById(int? id)
        {
            var response = await _httpClient.GetAsync("SystemConstant/filter?id=" + id);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var SysCdto = JsonConvert.DeserializeObject<List<SystemConstantDTO>>(jsysconstDto_);

            return SysCdto;
        } 
        public virtual async Task<List<SystemConstantDTO>> GetsystemConstantByParntId(int? id)
        {
            var response = await _httpClient.GetAsync("SystemConstant/filter?parentId=" + id);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var SysCdto = JsonConvert.DeserializeObject<List<SystemConstantDTO>>(jsysconstDto_);

            return SysCdto;
        } 
        public virtual async Task<List<GsltaxDTO>> GetGslTaxByrefernce(int? refe)
        {
            var response = await _httpClient.GetAsync("Gsltax/filter?reference=" + refe);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var SysCdto = JsonConvert.DeserializeObject<List<GsltaxDTO>?>(jsysconstDto_);

            return SysCdto;
        }
        public virtual async Task<List<ArticleDTO>> SelectArticleByName(string name)
        {

            var response = await _httpClient.GetAsync("Article/filter?name=" + name);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var artCdto = JsonConvert.DeserializeObject<List<ArticleDTO>>(jsysconstDto_);

            return artCdto;
        }
        public virtual async Task<List<ArticleDTO>> GetArticlesByGSL(int gslType)
        {

            var response = await _httpClient.GetAsync("Article/filter?gslType=" + gslType);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var artCdto = JsonConvert.DeserializeObject<List<ArticleDTO>>(jsysconstDto_);

            return artCdto;
        } 
        public virtual async Task<List<ArticleDTO>> ArticleBufferList()
        {

            var response = await _httpClient.GetAsync("Article/dynamic");
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var artCdto = JsonConvert.DeserializeObject<List<ArticleDTO>>(jsysconstDto_);

            return artCdto;
        }
        public virtual async Task<List<ReportDTO>> GetreportList()
        {

            var response = await _httpClient.GetAsync("Report/dynamic");
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var artCdto = JsonConvert.DeserializeObject<List<ReportDTO>>(jsysconstDto_);

            return artCdto;
        } 
        public virtual async Task<List<ReportDTO>> GetreportListByreference(int reference)
        {

            var response = await _httpClient.GetAsync("Report/filter?reference="+ reference);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var artCdto = JsonConvert.DeserializeObject<List<ReportDTO>>(jsysconstDto_);

            return artCdto;
        } 
        public virtual async Task<List<AccessMatrixDTO>> GetAccessMatrix()
        {

            var response = await _httpClient.GetAsync("AccessMatrix/dynamic");
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var artCdto = JsonConvert.DeserializeObject<List<AccessMatrixDTO>>(jsysconstDto_);

            return artCdto;
        }
        public virtual async Task<List<ArticleDTO>> SelectArticleByGSLtype(int gsltype)
        {

            var response = await _httpClient.GetAsync("Article/filter?gslType=" + gsltype);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var artCdto = JsonConvert.DeserializeObject<List<ArticleDTO>>(jsysconstDto_);

            return artCdto;
        }
      
        public virtual async Task<ArticleDTO> SelectArticleById(int _id)
        {

            var response = await _httpClient.GetAsync("Article/filter?id=" + _id);
            if (!response.IsSuccessStatusCode)
                return null;

            var artconstDto_ = await response.Content.ReadAsStringAsync();
            var artCdto = JsonConvert.DeserializeObject<List<ArticleDTO>>(artconstDto_);

            return artCdto?.FirstOrDefault();
        }
        public virtual async Task<List<DeviceDTO>> SelectDevice()
        {

            var response = await _httpClient.GetAsync("Device/dynamic");
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var deviconstDto_ = JsonConvert.DeserializeObject<List<DeviceDTO>>(jsysconstDto_);

            return deviconstDto_;
        }  
        public virtual async Task<List<DeviceDTO>> SelectDeviceByarticle(int art)
        {

            var response = await _httpClient.GetAsync("Device/filter?article="+art);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var deviconstDto_ = JsonConvert.DeserializeObject<List<DeviceDTO>>(jsysconstDto_);

            return deviconstDto_;
        }
        public virtual async Task<List<DeviceDTO>> SelectDeviceByhost(int? art)
        {

            var response = await _httpClient.GetAsync("Device/filter?host="+art);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var deviconstDto_ = JsonConvert.DeserializeObject<List<DeviceDTO>>(jsysconstDto_);

            return deviconstDto_;
        }  
        public virtual async Task<List<DeviceDTO>> SelectDeviceById(int? id)
        {

            var response = await _httpClient.GetAsync("Device/filter?id="+id);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var deviconstDto_ = JsonConvert.DeserializeObject<List<DeviceDTO>>(jsysconstDto_);

            return deviconstDto_;
        } 
        public virtual async Task<List<PreferenceDTO>> SelectPreference()
        {

            var response = await _httpClient.GetAsync("Preference/dynamic");
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var prefDto_ = JsonConvert.DeserializeObject<List<PreferenceDTO>>(jsysconstDto_);

            return prefDto_;
        } 
        public virtual async Task<List<PreferenceAccessDTO>> GetPreferentilaAccessByVouDefnandDevice(int vourDfn,int dev)
        {
            string endpoint = "PreferenceAccess/filter";
            string queryString = $"?voucherDfn={vourDfn}&device={dev}";
            string requestUrl = $"{endpoint}{queryString}";
            var response = await _httpClient.GetAsync(requestUrl);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var prefDto_ = JsonConvert.DeserializeObject<List<PreferenceAccessDTO>>(jsysconstDto_);

            return prefDto_;
        }  
        public virtual async Task<PreferenceDTO> SelectPreferenceByID(int? _id)
        {

            var response = await _httpClient.GetAsync("Preference/filter?id=" +_id);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var prefDto_ = JsonConvert.DeserializeObject<List<PreferenceDTO>>(jsysconstDto_);

            return prefDto_?.FirstOrDefault();
        }
        public virtual async Task<List<SystemConstantDTO>> GetSystemConstantByCat(string category)
        {

            var response = await _httpClient.GetAsync("SystemConstant/filter?category=" + category);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var SysCdto = JsonConvert.DeserializeObject<List<SystemConstantDTO>>(jsysconstDto_);

            return SysCdto;
        }
        public async Task<T> GetDynamicData<T>(string Database, Dictionary<string, string> keyValuePairs)
        {
            string Filtervalues = "";
            int totalcount = keyValuePairs.Count;
            int count = 0;
            foreach (var values in keyValuePairs)
            {
                if (count > 0)
                    Filtervalues += "&";

                Filtervalues += values.Key + "=" + values.Value;

                count++;
            }
            string uri = string.Format(Database + "/dynamic/?{0}", Filtervalues);

            return await GetReqAsync<T>(uri);
        }
        public virtual async Task<ConsigneeBuffer> SaveConsigneeBuffer(ConsigneeBuffer confnDTO)
        {
            try
            {


                HttpResponseMessage response = null;
                if (confnDTO.consignee.Id != 0)
                {
                    string testJson = JsonConvert.SerializeObject(confnDTO);
                    File.WriteAllText("testdata.json", testJson);
                    response = await _httpClient.PutAsJsonAsync(requestUri: "ConsigneeBuffer", value: confnDTO);
                }
                else
                {
                    string testJson = JsonConvert.SerializeObject(confnDTO);
                    File.WriteAllText("testdata.json", testJson);
                    response = await _httpClient.PostAsJsonAsync("ConsigneeBuffer", confnDTO);
                }
                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    var responseData = JsonConvert.DeserializeObject<ConsigneeBuffer>(responseJson);
                    var test = responseData;
                    return responseData;
                }

            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
            }
            return default;
        }
        public virtual async Task<List<VwConsigneeViewDTO>> GetVoucherConsigneeList(int gsltype)
        {
            var response = await _httpClient.GetAsync("VwConsigneeView/filter?gslType=" + gsltype);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var SysCdto = JsonConvert.DeserializeObject<List<VwConsigneeViewDTO>>(jsysconstDto_);

            return SysCdto;
        } 
        public virtual async Task<List<VwConsigneeViewDTO>> GetVoucherConsignee()
        {
            var response = await _httpClient.GetAsync("VwConsigneeView/dynamic");
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var SysCdto = JsonConvert.DeserializeObject<List<VwConsigneeViewDTO>>(jsysconstDto_);

            return SysCdto;
        } 
        public virtual async Task<List<VwConsigneeViewDTO>> GetVoucherConsigneeById(int id)
        {
            var response = await _httpClient.GetAsync("VwConsigneeView/filter?id=" + id);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var SysCdto = JsonConvert.DeserializeObject<List<VwConsigneeViewDTO>>(jsysconstDto_);

            return SysCdto;
        }
        public virtual async Task<List<PeriodDTO>> GetperiodBytype(int type)
        {

            var response = await _httpClient.GetAsync("Period/filter?type=" + type);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var SysCdto = JsonConvert.DeserializeObject<List<PeriodDTO>>(jsysconstDto_);

            return SysCdto;
        }
        public virtual async Task<List<CountryDTO>> GetAllCountries()
        {

            var response = await _httpClient.GetAsync("Country/dynamic");
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var CountryDTO = JsonConvert.DeserializeObject<List<CountryDTO>>(jsysconstDto_);

            return CountryDTO;
        }
        public virtual async Task<List<ObjectStateDTO>> GetObjeectStateByrefe(int? refrence)
        {

            var response = await _httpClient.GetAsync("ObjectState/filter?reference=" + refrence);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var objDTO = JsonConvert.DeserializeObject<List<ObjectStateDTO>>(jsysconstDto_);

            return objDTO;
        }  
        public virtual async Task<List<ObjectStateDTO>> GetObjectstaByreference(int? refrence)
        {

            var response = await _httpClient.GetAsync("ObjectState/filter?reference=" + refrence);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var objDTO = JsonConvert.DeserializeObject<List<ObjectStateDTO>>(jsysconstDto_);

            return objDTO;
        }
        public virtual async Task<List<ActivityDTO>> GetActivityByrefandAcDefn(int refrence, int? actDefn)
        {
            string endpoint = "Activity/filter";
            string queryString = $"?reference={refrence}&activityDefinition={actDefn}";
            string requestUrl = $"{endpoint}{queryString}";

            var response = await _httpClient.GetAsync(requestUrl);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var actDefnaadto = JsonConvert.DeserializeObject<List<ActivityDTO>>(jsysconstDto_);

            return actDefnaadto;
        } 
        public virtual async Task<List<ActivityDTO>> GetActivityByref(int refrence)
        {
            string endpoint = "Activity/filter";
            string queryString = $"?reference={refrence}";
            string requestUrl = $"{endpoint}{queryString}";

            var response = await _httpClient.GetAsync(requestUrl);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var actDefnaadto = JsonConvert.DeserializeObject<List<ActivityDTO>>(jsysconstDto_);

            return actDefnaadto;
        } 
        public virtual async Task<List<ActivityDTO>> GetActivityByUsername(int refrence)
        {
            string endpoint = "Activity/filter";
            string queryString = $"?user={refrence}";
            string requestUrl = $"{endpoint}{queryString}";

            var response = await _httpClient.GetAsync(requestUrl);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var actDefnaadto = JsonConvert.DeserializeObject<List<ActivityDTO>>(jsysconstDto_);

            return actDefnaadto;
        } 
        public virtual async Task<List<UserRoleMapperDTO>> GetUserRoleMap(int refrence)
        {
            string endpoint = "UserRoleMapper/filter";
            string queryString = $"?user={refrence}";
            string requestUrl = $"{endpoint}{queryString}";

            var response = await _httpClient.GetAsync(requestUrl);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var actDefnaadto = JsonConvert.DeserializeObject<List<UserRoleMapperDTO>>(jsysconstDto_);

            return actDefnaadto;
        } 
        public virtual async Task<List<UserRoleMapperDTO>> GetUserRoleMapById(int _idd)
        {
            string endpoint = "UserRoleMapper/filter";
            string queryString = $"?id={_idd}";
            string requestUrl = $"{endpoint}{queryString}";

            var response = await _httpClient.GetAsync(requestUrl);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var actDefnaadto = JsonConvert.DeserializeObject<List<UserRoleMapperDTO>>(jsysconstDto_);

            return actDefnaadto;
        }
        public virtual async Task<List<UserRoleMapperDTO>> GetUserRoleMapByrole(int refrence)
        {
            string endpoint = "UserRoleMapper/filter";
            string queryString = $"?role={refrence}";
            string requestUrl = $"{endpoint}{queryString}";

            var response = await _httpClient.GetAsync(requestUrl);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var actDefnaadto = JsonConvert.DeserializeObject<List<UserRoleMapperDTO>>(jsysconstDto_);

            return actDefnaadto;
        }
        public virtual async Task<List<ActivityDefinitionDTO>> ActivityDefnBufferList()
        {

            var response = await _httpClient.GetAsync("ActivityDefinition/dynamic");
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var actDefnaadto = JsonConvert.DeserializeObject<List<ActivityDefinitionDTO>>(jsysconstDto_);

            return actDefnaadto;
        } 
        public virtual async Task<List<PreferenceDTO>> GetpreferenceByparentandref(int parent ,int refrence)
        {
            string endpoint = "SystemConstant/filter";
            string queryString = $"?parentId={parent}&reference={refrence}";
            string requestUrl = $"{endpoint}{queryString}";

            var response = await _httpClient.GetAsync(requestUrl);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var actDefnaadto = JsonConvert.DeserializeObject<List<PreferenceDTO>>(jsysconstDto_);

            return actDefnaadto;
        } 
        public virtual async Task<List<RequiredGslDTO>> GetRequriedGslbyVoucherdefandtype(int? voucherdef, int? type)
        {
            string endpoint = "RequiredGsl/filter";
            string queryString = $"?VoucherDefn={voucherdef}&Type={type}";
            string requestUrl = $"{endpoint}{queryString}";

            var response = await _httpClient.GetAsync(requestUrl);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var redto = JsonConvert.DeserializeObject<List<RequiredGslDTO>>(jsysconstDto_);

            return redto;
        } 
        
        public virtual async Task<List<RequiredGsldetailDTO>> GetRequiredGSLDetailByRequiredGSL(int? requed)
        {
          
            var response = await _httpClient.GetAsync("RequiredGsldetail/filter?requiredGSL=" + requed);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var redto = JsonConvert.DeserializeObject<List<RequiredGsldetailDTO>>(jsysconstDto_);

            return redto;
        } 
        public virtual async Task<List<RequiredGsldetailDTO>> GetRequiredGSLDetailById(int? _idd)
        {
          
            var response = await _httpClient.GetAsync("RequiredGsldetail/filter?id=" + _idd);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var redto = JsonConvert.DeserializeObject<List<RequiredGsldetailDTO>>(jsysconstDto_);

            return redto;
        }
        public virtual async Task<List<VoucherExtensionDefinitionDTO>> getvoucherextension(int? voucherdef, int type)
        {
            string endpoint = "VoucherExtensionDefinition/filter";
            string queryString = $"?VoucherDefinition={voucherdef}&type={type}";
            string requestUrl = $"{endpoint}{queryString}";

            var response = await _httpClient.GetAsync(requestUrl);

            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var redto = JsonConvert.DeserializeObject<List<VoucherExtensionDefinitionDTO>>(jsysconstDto_);

            return redto;
        }
        public virtual async Task<List<VoucherExtensionDefinitionDTO>> getvoucherextensionByid(int? id)
        {
            string endpoint = "VoucherExtensionDefinition/filter";
            string queryString = $"?id={id}";
            string requestUrl = $"{endpoint}{queryString}";

            var response = await _httpClient.GetAsync(requestUrl);

            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var redto = JsonConvert.DeserializeObject<List<VoucherExtensionDefinitionDTO>>(jsysconstDto_);

            return redto;
        } 
        public virtual async Task<List<RelationDTO>> getRelationByrefereingobject(int? refedObject, int? refrringObject,int reltype)
        {
            string endpoint = "Relation/filter";
            string queryString = $"?referencedObject={refedObject}&referringObject={refrringObject}&relationType={reltype}";
            string requestUrl = $"{endpoint}{queryString}";

            var response = await _httpClient.GetAsync(requestUrl);

            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var redto = JsonConvert.DeserializeObject<List<RelationDTO>>(jsysconstDto_);

            return redto;
        } 
        public virtual async Task<List<RelationDTO>> getRelationByrelationType(int reltype)
        {
            string endpoint = "Relation/filter";
            string queryString = $"?relationType={reltype}";
            string requestUrl = $"{endpoint}{queryString}";

            var response = await _httpClient.GetAsync(requestUrl);

            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var redto = JsonConvert.DeserializeObject<List<RelationDTO>>(jsysconstDto_);

            return redto;
        }
        public virtual async Task<List<RelationDTO>> getRelationforInternal(int? refrringObject,int reltype)
        {
            string endpoint = "Relation/filter";
            string queryString = $"?referringObject={refrringObject}&relationType={reltype}";
            string requestUrl = $"{endpoint}{queryString}";

            var response = await _httpClient.GetAsync(requestUrl);

            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var redto = JsonConvert.DeserializeObject<List<RelationDTO>>(jsysconstDto_);

            return redto;
        } 
        public virtual async Task<List<RelationDTO>> getRelationByreference(int? refereceObject)
        {
            string endpoint = "Relation/filter";
            string queryString = $"?referencedObject={refereceObject}";
            string requestUrl = $"{endpoint}{queryString}";

            var response = await _httpClient.GetAsync(requestUrl);

            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var redto = JsonConvert.DeserializeObject<List<RelationDTO>>(jsysconstDto_);

            return redto;
        } 
        public virtual async Task<List<ValueFactorDefinitionDTO>> getValueDefnByTypeandGslList(int? type,int? gsltype)
        {
            string endpoint = "ValueFactorDefinition/filter";
            string queryString = $"?type={type}&gslType={gsltype}";
            string requestUrl = $"{endpoint}{queryString}";

            var response = await _httpClient.GetAsync(requestUrl);

            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var redto = JsonConvert.DeserializeObject<List<ValueFactorDefinitionDTO>>(jsysconstDto_);

            return redto;
        }  
        public virtual async Task<List<ValueFactorDefinitionDTO>> getValueDefnById(int id)
        {
            string endpoint = "ValueFactorDefinition/filter";
            string queryString = $"?id={id}";
            string requestUrl = $"{endpoint}{queryString}";

            var response = await _httpClient.GetAsync(requestUrl);

            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var redto = JsonConvert.DeserializeObject<List<ValueFactorDefinitionDTO>>(jsysconstDto_);

            return redto;
        }
        public virtual async Task<List<RelationalStateDTO>> GetRElatioSateByrel(int relation)
        {
          
            var response = await _httpClient.GetAsync("RelationalState/filter?relation=" + relation);

            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var redto = JsonConvert.DeserializeObject<List<RelationalStateDTO>>(jsysconstDto_);

            return redto;
        } 
        public virtual async Task<List<FieldFormatDTO>> GetfieldformatBytypeandreference(int type,int refe)
        {
            string endpoint = "FieldFormat/filter";
            string queryString = $"?type={type}&reference={refe}";
            string requestUrl = $"{endpoint}{queryString}";

            var response = await _httpClient.GetAsync(requestUrl);

            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var redto = JsonConvert.DeserializeObject<List<FieldFormatDTO>>(jsysconstDto_);

            return redto;
        } 
        public virtual async Task<List<FieldFormatDTO>> GetfieldformatbyreferenceandIndex(int refe, int index)
        {

            string endpoint = "FieldFormat/filter";
            string queryString = $"?reference={refe}&index={index}";
            string requestUrl = $"{endpoint}{queryString}";

            var response = await _httpClient.GetAsync(requestUrl);

            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var redto = JsonConvert.DeserializeObject<List<FieldFormatDTO>>(jsysconstDto_);

            return redto;
        }
        public virtual async Task<List<DistributionDTO>> GetDistrubtionByRefeandTye(int refe, int? type)
        {
            string endpoint = "Distribution/filter";
            string queryString = $"?SystemConstant={refe}&type={type}";
            string requestUrl = $"{endpoint}{queryString}";

            var response = await _httpClient.GetAsync(requestUrl);

            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var redto = JsonConvert.DeserializeObject<List<DistributionDTO>>(jsysconstDto_);

            return redto;
        } 
        public virtual async Task<List<DistributionDTO>> GetDistrubtionById(int? id)
        {
            string endpoint = "Distribution/filter";
            string queryString = $"?id={id}";
            string requestUrl = $"{endpoint}{queryString}";

            var response = await _httpClient.GetAsync(requestUrl);

            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var redto = JsonConvert.DeserializeObject<List<DistributionDTO>>(jsysconstDto_);

            return redto;
        } 
        public virtual async Task<List<FieldFormatDTO>> GetfieldformatByreference(int? refe)
        {

            var response = await _httpClient.GetAsync("FieldFormat/filter?reference=" + refe);

            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var redto = JsonConvert.DeserializeObject<List<FieldFormatDTO>>(jsysconstDto_);

            return redto;
        } 
        public virtual async Task<List<FieldFormatDTO>> GetfieldformatbyId(int id)
        {
            var response = await _httpClient.GetAsync("FieldFormat/filter?id=" + id);

            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var redto = JsonConvert.DeserializeObject<List<FieldFormatDTO>>(jsysconstDto_);

            return redto;
        }   
        public virtual async Task<List<PreferenceDTO>> GetpreferenceById(int id)
        {
            var response = await _httpClient.GetAsync("Preference/filter?id=" + id);

            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var redto = JsonConvert.DeserializeObject<List<PreferenceDTO>>(jsysconstDto_);

            return redto;
        } 
        public virtual async Task<List<BankAccountDetailDTO>> GetBankAccountDetailById(int id)
        {
            var response = await _httpClient.GetAsync("BankAccountDetail/filter?id=" + id);

            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var redto = JsonConvert.DeserializeObject<List<BankAccountDetailDTO>>(jsysconstDto_);

            return redto;
        } 
        public virtual async Task<List<BankAccountDetailDTO>> GetBankAccountDetailByAcNumandRefandCat(string accoNo,int? consn)
        {
            string endpoint = "BankAccountDetail/filter";
            string queryString = $"?accountNo={accoNo}&consignee={consn}";
            string requestUrl = $"{endpoint}{queryString}";

            var response = await _httpClient.GetAsync(requestUrl);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var redto = JsonConvert.DeserializeObject<List<BankAccountDetailDTO>>(jsysconstDto_);

            return redto;
        } 
        public virtual async Task<List<BankAccountDetailDTO>> GetBankAccountDetailByRefandCatagory(int? consn,int category)
        {
            string endpoint = "BankAccountDetail/filter";
            string queryString = $"?&consignee={consn}&category={category}";
            string requestUrl = $"{endpoint}{queryString}";

            var response = await _httpClient.GetAsync(requestUrl);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var redto = JsonConvert.DeserializeObject<List<BankAccountDetailDTO>>(jsysconstDto_);

            return redto;
        }
        #region Post
        public virtual async Task<ConfigurationDTO?> saveOrUpdateRange(List<ConfigurationDTO> confnDTO)
        {
            var response = await _httpClient.PostAsJsonAsync("Configuration/saveOrUpdateRange", confnDTO);
            return null;
        }
         public virtual async Task<ConfigurationDTO?> SaveCofiguration(ConfigurationDTO confnDTO)
        {
            try
            {
                if (!string.IsNullOrEmpty(confnDTO.Id.ToString()) && confnDTO.Id > 0)
                {
                    var response = await _httpClient.PutAsJsonAsync("Configuration", confnDTO);
                    if (response.IsSuccessStatusCode)
                    {
                        return null;
                    }
                    else
                    {
                        Console.WriteLine("Error Calling Web Api");
                    }
                }
                else
                {
                    var response = await _httpClient.PostAsJsonAsync("Configuration", confnDTO);
                    if (response.IsSuccessStatusCode)
                    {
                        return null;
                    }
                    else
                    {
                        Console.WriteLine("Error Calling Web Api");
                    }
                }

            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
            }
            return null;

        }
        public virtual async Task<SystemConstantDTO> CreateSystemConstant(SystemConstantDTO _sysconst)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("SystemConstant", _sysconst);
                if (response.IsSuccessStatusCode)
                {
                    var jsysconstDto_ = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<SystemConstantDTO>(jsysconstDto_);
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
            }
            return null;

        } 
        public virtual async Task<OrderStationMapDTO> CreateOrderStationMap(OrderStationMapDTO _orderstation)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("OrderStationMap", _orderstation);
                if (response.IsSuccessStatusCode)
                {
                    var jsysconstDto_ = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<OrderStationMapDTO>(jsysconstDto_);
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
            }
            return null;

        }

        public virtual async Task<UserDTO> CreateUser(UserDTO _userDTO)
        {
            try
            {
                //JavaScriptSerializer serializer = new JavaScriptSerializer();
                //string json = serializer.Serialize(_userDTO);
                //Response.WriteAsync(json);
                var response = await _httpClient.PostAsJsonAsync("CommonLibrary/create_user", _userDTO);

                if (response.IsSuccessStatusCode)
                {
                    var jsysconstDto_ = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<UserDTO>(jsysconstDto_);
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
            }
            return null;

        } 
        public virtual async Task<UserDTO> UpdateUser(UserDTO _userDTO)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("User", _userDTO);
                if (response.IsSuccessStatusCode)
                {
                    var jsysconstDto_ = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<UserDTO>(jsysconstDto_);
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
            }
            return null;

        }   
        public virtual async Task<RoleActivityDTO> CreateRoleActivity(RoleActivityDTO _userDTO)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("RoleActivity", _userDTO);
                if (response.IsSuccessStatusCode)
                {
                    var jsysconstDto_ = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<RoleActivityDTO>(jsysconstDto_);
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
            }
            return null;

        } 
        public virtual async Task<RoleActivityDTO> UpdateRoleActivity(RoleActivityDTO _userDTO)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("RoleActivity", _userDTO);
                if (response.IsSuccessStatusCode)
                {
                    var jsysconstDto_ = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<RoleActivityDTO>(jsysconstDto_);
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
            }
            return null;

        } 
        public virtual async Task<FunctionalityDTO> CreateFunctionality(FunctionalityDTO _userDTO)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("Functionality", _userDTO);
                if (response.IsSuccessStatusCode)
                {
                    var jsysconstDto_ = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<FunctionalityDTO>(jsysconstDto_);
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
            }
            return null;

        } 
       public virtual async Task<AccessMatrixDTO> CreateAccessMatrix(AccessMatrixDTO _accma)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("AccessMatrix", _accma);
                if (response.IsSuccessStatusCode)
                {
                    var jsysconstDto_ = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<AccessMatrixDTO>(jsysconstDto_);
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
            }
            return null;

        } 
       public virtual async Task<UserRoleMapperDTO> CreateUserRoleMapper(UserRoleMapperDTO _mapper)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("UserRoleMapper", _mapper);
                if (response.IsSuccessStatusCode)
                {
                    var jsysconstDto_ = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<UserRoleMapperDTO>(jsysconstDto_);
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
            }
            return null;

        } 
        
       public virtual async Task<CnetlicenseDTO> createCnetlicence(CnetlicenseDTO clince)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("Cnetlicense", clince);
                if (response.IsSuccessStatusCode)
                {
                    var jsysconstDto_ = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<CnetlicenseDTO>(jsysconstDto_);
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
            }
            return null;

        } 
        public virtual async Task<SystemConstantDTO> UpdateSystemConstant(SystemConstantDTO _sysconst)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("SystemConstant", _sysconst);
                if (response.IsSuccessStatusCode)
                { 
                    var jsysconstDto_ = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<SystemConstantDTO>(jsysconstDto_);
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
            }
            return null;

        } 
        public virtual async Task<ReportDTO> UpdateReport(ReportDTO _report)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("Report", _report);
                if (response.IsSuccessStatusCode)
                { 
                    var jsysconstDto_ = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ReportDTO>(jsysconstDto_);
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
            }
            return null;

        }
        
        public virtual async Task<ArticleDTO> CreateArticle(ArticleDTO artdTO)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("Article", artdTO);
                if (response.IsSuccessStatusCode)
                {
                    var jsysconstDto_ = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ArticleDTO>(jsysconstDto_);
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
            }
            return null;

        } 
        public virtual async Task<DeviceDTO> CreateDevice(DeviceDTO artdTO)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("Device", artdTO);
                if (response.IsSuccessStatusCode)
                {
                    var jsysconstDto_ = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<DeviceDTO>(jsysconstDto_);
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
            }
            return null;

        } 
        public virtual async Task<DeviceDTO> UpdateDevice(DeviceDTO artdTO)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("Device", artdTO);
                if (response.IsSuccessStatusCode)
                {
                    var jsysconstDto_ = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<DeviceDTO>(jsysconstDto_);
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
            }
            return null;

        }
        public virtual async Task<ScheduleDTO> CreateSchedule(ScheduleDTO schldTO)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("Schedule", schldTO);
                if (response.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
            }
            return null;

        }
        public virtual async Task<IdsettingDTO> UpdateIdsetting(IdsettingDTO idsetting)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("Idsetting", idsetting);
                if (response.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
            }
            return null;

        }
        public virtual async Task<IdsettingDTO> CreateIdsetting(IdsettingDTO idsetting)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("Idsetting", idsetting);
                if (response.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
            }
            return null;

        }
        public virtual async Task<ScheduleDetailDTO> CreateScheduledetail(ScheduleDetailDTO schlDetaildTO)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("ScheduleDetail", schlDetaildTO);
                if (response.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
            }
            return null;

        }
        public virtual async Task<ScheduleHeaderDTO> CreateScheduleheader(ScheduleHeaderDTO schheaderdTO)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("ScheduleHeader", schheaderdTO);
                if (response.IsSuccessStatusCode)
                {
                    var jsysconstDto_ = await response.Content.ReadAsStringAsync();
                    var header =  JsonConvert.DeserializeObject<ScheduleHeaderDTO>(jsysconstDto_);
                    return header;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
            }
            return null;

        }
        public virtual async Task<ScheduleHeaderDTO> UpdateScheduleheader(ScheduleHeaderDTO schlDetaildTO)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("ScheduleHeader", schlDetaildTO);
                if (response.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
            }
            return null;

        }
        public virtual async Task<ScheduleDetailDTO> UpdateScheduledetail(ScheduleDetailDTO schlDetaildTO)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("ScheduleDetail", schlDetaildTO);
                if (response.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
            }
            return null;

        }
        public virtual async Task<ArticleDTO?> UpdateArticle(ArticleDTO artdTO)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("Article", artdTO);
                if (response.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }


            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
            }
            return null;

        }
        public async Task<ActivityDefinitionDTO> CreateActivityDefinition(ActivityDefinitionDTO actdTO)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("ActivityDefinition", actdTO);
                if (response.IsSuccessStatusCode)
                {
                    var jsysconstDto_ = await response.Content.ReadAsStringAsync();
                    var objDTO = JsonConvert.DeserializeObject<ActivityDefinitionDTO>(jsysconstDto_);

                    return objDTO;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return null;
        }
        public virtual async Task<ActivityDefinitionDTO?> UpdateActivityDefinition(ActivityDefinitionDTO actdTO)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("ActivityDefinition", actdTO);
                if (response.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return null;
        }

        public async Task<ActivityDTO> CreateActivity(ActivityDTO actdTO)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("Activity", actdTO);
                if (response.IsSuccessStatusCode)
                {
                    var jsysconstDto_ = await response.Content.ReadAsStringAsync();
                    var objDTO = JsonConvert.DeserializeObject<ActivityDTO>(jsysconstDto_);

                    return objDTO;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return null;
        }

        public async Task<RequiredGslDTO> CreatRequriedGSL(RequiredGslDTO requrd)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("RequiredGsl", requrd);
                if (response.IsSuccessStatusCode)
                {
                    var jsysconstDto_ = await response.Content.ReadAsStringAsync();
                    var requgslDTO = JsonConvert.DeserializeObject<RequiredGslDTO>(jsysconstDto_);

                    return requgslDTO;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return null;
        } 
        public async Task<RequiredGslDTO> UpdateRequriedGSlDetail(RequiredGslDTO requrd)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("RequiredGsl", requrd);
                if (response.IsSuccessStatusCode)
                {
                    var jsysconstDto_ = await response.Content.ReadAsStringAsync();
                    var requgslDTO = JsonConvert.DeserializeObject<RequiredGslDTO>(jsysconstDto_);

                    return requgslDTO;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return null;
        } 
        public async Task<RequiredGsldetailDTO> CreatRequriedGSldetail(RequiredGsldetailDTO requrd)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("RequiredGsldetail", requrd);
                if (response.IsSuccessStatusCode)
                {
                    var jsysconstDto_ = await response.Content.ReadAsStringAsync();
                    var requgslDetialDTO = JsonConvert.DeserializeObject<RequiredGsldetailDTO>(jsysconstDto_);

                    return requgslDetialDTO;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return null;
        }
        public async Task<RequiredGsldetailDTO> UpdateRequriedGSldetail(RequiredGsldetailDTO requrd)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("RequiredGsldetail", requrd);
                if (response.IsSuccessStatusCode)
                {
                    var jsysconstDto_ = await response.Content.ReadAsStringAsync();
                    var requgslDetialDTO = JsonConvert.DeserializeObject<RequiredGsldetailDTO>(jsysconstDto_);

                    return requgslDetialDTO;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return null;
        }
    
        public virtual async Task<ObjectStateDTO> CreateObjectState(ObjectStateDTO objdTO)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("ObjectState", objdTO);
                if (response.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return null;
        }
        public virtual async Task<PreferenceDTO> Createpreference(PreferenceDTO _pref)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("Preference", _pref);
                if (response.IsSuccessStatusCode)
                {
                    var jsysconstDto_ = await response.Content.ReadAsStringAsync();
                    var prefDTO = JsonConvert.DeserializeObject<PreferenceDTO>(jsysconstDto_);

                    return prefDTO;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return null;
        }  
        public virtual async Task<PreferenceDTO> Updatepreference(PreferenceDTO _pref)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("Preference", _pref);
                if (response.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return null;
        }
        public virtual async Task<ObjectStateDTO?> UpdateObjectState(ObjectStateDTO objdTO)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("ObjectState", objdTO);
                if (response.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return null;
        }

        public virtual async Task<TermDefinitionDTO> CreateTermsandconditions(TermDefinitionDTO termDefn)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("TermDefinition", termDefn);
                if (response.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return null;
        }
        public virtual async Task<TermDefinitionDTO?> UpdateTermsandconditions(TermDefinitionDTO termDefn)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("TermDefinition", termDefn);
                if (response.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return null;
        }
        public virtual async Task<RelationalStateDTO?> createrelationstate(RelationalStateDTO relaSate)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("RelationalState", relaSate);
                if (response.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return null;
        } 
        public virtual async Task<RelationalStateDTO?> Updaterelationstate(RelationalStateDTO relaSate)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("RelationalState", relaSate);
                if (response.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return null;
        }
       
        public virtual async Task<RelationDTO?> Createrelation(RelationDTO relaSate)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("Relation", relaSate);
                if (response.IsSuccessStatusCode)
                {
                     var jsysconstDto_ = await response.Content.ReadAsStringAsync();
                    var relatio = JsonConvert.DeserializeObject<RelationDTO>(jsysconstDto_);

                    return relatio;

                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return null;
        }
        public virtual async Task<RelationDTO?> Updaterelation(RelationDTO relaSate)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("Relation", relaSate);
                if (response.IsSuccessStatusCode)
                {
                    var jsysconstDto_ = await response.Content.ReadAsStringAsync();
                    var relatio = JsonConvert.DeserializeObject<RelationDTO>(jsysconstDto_);

                    return relatio;

                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return null;
        } 
        public virtual async Task<VoucherExtensionDefinitionDTO?> CreateVoucherExtension(VoucherExtensionDefinitionDTO relaSate)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("VoucherExtensionDefinition", relaSate);
                if (response.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return null;
        } 
        public virtual async Task<VoucherExtensionDefinitionDTO?> UpdateVoucherExtension(VoucherExtensionDefinitionDTO relaSate)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("VoucherExtensionDefinition", relaSate);
                if (response.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return null;
        } 
        public virtual async Task<PreferenceAccessDTO?> Createpreferentilaaccsess(PreferenceAccessDTO accessDTO)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("PreferenceAccess", accessDTO);
                if (response.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return null;
        } 
        public virtual async Task<PreferenceAccessDTO?> Updatepreferentilaaccsess(PreferenceAccessDTO accessDTO)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("PreferenceAccess", accessDTO);
                if (response.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return null;
        }
       public virtual async Task<VoucherStoreMappingDTO?> createStoreMapSource(VoucherStoreMappingDTO mapstore)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("VoucherStoreMapping", mapstore);
                if (response.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return null;
        }
         public virtual async Task<FieldFormatDTO?> createfieldformat(FieldFormatDTO fieldFormatDTO)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("FieldFormat", fieldFormatDTO);
                if (response.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return null;
        }
        public virtual async Task<FieldFormatDTO?> Updatefieldformat(FieldFormatDTO fieldFormatDTO)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("FieldFormat", fieldFormatDTO);
                if (response.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return null;
        } 
        public virtual async Task<GslacctRequirementDTO?> CreateSLAcctRequirement(GslacctRequirementDTO fieldFormatDTO)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("GslacctRequirement", fieldFormatDTO);
                if (response.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return null;
        }
        public virtual async Task<GslacctRequirementDTO?> UpdateSLAcctRequirement(GslacctRequirementDTO fieldFormatDTO)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("GslacctRequirement", fieldFormatDTO);
                if (response.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return null;
        }
      public virtual async Task<ConsigneeDTO?> CreateOrganization(ConsigneeDTO consignee)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("Consignee", consignee);
                if (response.IsSuccessStatusCode)
                {
                    var jsysconstDto_ = await response.Content.ReadAsStringAsync();
                    var consiDTO = JsonConvert.DeserializeObject<ConsigneeDTO>(jsysconstDto_);

                    return  consiDTO;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return null;
        }  
        public virtual async Task<ConsigneeDTO?> UpdateOrganization(ConsigneeDTO consignee)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("Consignee", consignee);
                if (response.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return null;
        }

        public virtual async Task<ConsigneeUnitDTO?> CreateOrganizationunit(ConsigneeUnitDTO consignee)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("ConsigneeUnit", consignee);
                if (response.IsSuccessStatusCode)
                {
                    var jsysconstDto_ = await response.Content.ReadAsStringAsync();
                    var consiDTO = JsonConvert.DeserializeObject<ConsigneeUnitDTO>(jsysconstDto_);

                    return consiDTO;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return null;
        }
        public virtual async Task<ConsigneeUnitDTO?> UpdateOrganizationunit(ConsigneeUnitDTO consignee)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("ConsigneeUnit", consignee);
                if (response.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return null;
        }


        public virtual async Task<GsltaxDTO?> CreateGSLTax(GsltaxDTO gltaxdTO)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("Gsltax", gltaxdTO);
                if (response.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return null;
        } 
        public virtual async Task<IdentificationDTO?> CreateIdentification(List<IdentificationDTO> iden)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("Identification", iden);
                if (response.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return null;
        }
          
        public virtual async Task<IdentificationDTO?> UpdateIdentification(List<IdentificationDTO> iden)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("Identification", iden);
                if (response.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return null;
        }    
        public virtual async Task<IdentificationDTO?> CreateIdentification1(IdentificationDTO iden)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("Identification", iden);
                if (response.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return null;
        }
          
        public virtual async Task<IdentificationDTO?> UpdateIdentification2(IdentificationDTO iden)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("Identification", iden);
                if (response.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return null;
        }
        public virtual async Task<JobDescriptionDTO?> UpdateJobdescription(JobDescriptionDTO jobDto)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("JobDescription", jobDto);
                if (response.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return null;
        }
        public virtual async Task<JobDescriptionDTO?> CreateJobdescription(JobDescriptionDTO jobDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("JobDescription", jobDto);
                if (response.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return null;
        }
        public virtual async Task<BankAccountDetailDTO?> CreateBankAccountDetail(BankAccountDetailDTO backdetail)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("BankAccountDetail", backdetail);
                if (response.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return null;
        } 
        public virtual async Task<BankAccountDetailDTO?> UpdateBankAccountDetail(BankAccountDetailDTO backdetail)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("BankAccountDetail", backdetail);
                if (response.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return null;
        }
          public virtual async Task<AttachmentDTO?> CreateAttachment(AttachmentDTO attxh)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("Attachment", attxh);
                if (response.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return null;
        } 
        public virtual async Task<AttachmentDTO?> UpdateAttachment(AttachmentDTO attxh)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("Attachment", attxh);
                if (response.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return null;
        }
        public virtual async Task<AccountMapDTO> createaccountmap(AccountMapDTO accountMap)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("AccountMap", accountMap);
                if (response.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return null;
        } 
        public virtual async Task<AccountMapDTO> Updateaccountmap(AccountMapDTO accountMap)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("AccountMap", accountMap);
                if (response.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return null;
        }
        public virtual async Task<DistributionDTO> CreateDstribution(DistributionDTO distr)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("Distribution", distr);
                if (response.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return null;
        }
       public virtual async Task<DistributionDTO> UpdateDstribution(DistributionDTO distr)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("Distribution", distr);
                if (response.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return null;
        }
         public virtual async Task<ValueFactorDefinitionDTO> createValueDefn(ValueFactorDefinitionDTO valueDefn)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("ValueFactorDefinition", valueDefn);
                if (response.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return null;
        } 
        public virtual async Task<ValueFactorDefinitionDTO> UpdateValueDefn(ValueFactorDefinitionDTO valueDefn)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("ValueFactorDefinition", valueDefn);
                if (response.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return null;
        } 
        public virtual async Task<IddefinitionDTO> CreateIdDefinition(IddefinitionDTO valueDefn)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("Iddefinition", valueDefn);
                if (response.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return null;
        } 
        public virtual async Task<IddefinitionDTO> UpdateIdDefinition(IddefinitionDTO valueDefn)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("Iddefinition", valueDefn);
                if (response.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return null;
        }  
        public virtual async Task<IddefinitionDTO> CreateCnetlicense(CnetlicenseDTO clinc)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("Cnetlicense",clinc );
                if (response.IsSuccessStatusCode)
                {
                    return null;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
            return null;
        }

        #endregion

        #region Delete
        public virtual async Task<int> DeleteAccessMatrix(int? _id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync("AccessMatrix/" + _id);
                if (response.IsSuccessStatusCode)
                {
                    return 0;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
            }


            return 0;
        }
        public virtual async Task<int> DeleteOrderStationMap(int? _id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync("OrderStationMap/" + _id);
                if (response.IsSuccessStatusCode)
                {
                    return 0;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
            }


            return 0;
        }      
        public virtual async Task<int> DeleteRoleActivity(int? _id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync("RoleActivity/" + _id);
                if (response.IsSuccessStatusCode)
                {
                    return 0;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
            }


            return 0;
        }     
        public virtual async Task<int> DeleteObjectStateByReference(int? _id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync("ObjectState/" + _id);
                if (response.IsSuccessStatusCode)
                {
                    return 0;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
            }


            return 0;
        }   
        public virtual async Task<int> DeleteStoreMapSource(int? _id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync("VoucherStoreMapping/" + _id);
                if (response.IsSuccessStatusCode)
                {
                    return 0;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
            }


            return 0;
        }  
        public virtual async Task<int> DeleteCnetlicence(int? _id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync("Cnetlicense/" + _id);
                if (response.IsSuccessStatusCode)
                {
                    return 0;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
            }


            return 0;
        }
        public virtual async Task<bool> DeleteDevice(int? _id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync("Device/" + _id);
                if (response.IsSuccessStatusCode)
                {
                    return response.IsSuccessStatusCode;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
            }


            return false;
        } 
        public virtual async Task<int> Deletearticle(int? _id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync("Article/" + _id);
                if (response.IsSuccessStatusCode)
                {
                    return 0;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
            }


            return 0;
        }   
        public virtual async Task<int> DeleteConfigurationById(int? _id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync("Configuration/" + _id);
                if (response.IsSuccessStatusCode)
                {
                    return 0;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
            }


            return 0;
        }  
        public virtual async Task<bool> DeletePreference(int? _id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync("Preference/" + _id);
                if (response.IsSuccessStatusCode)
                {
                    return response.IsSuccessStatusCode;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
            }


            return false;
        } 
        public virtual async Task<bool> deleteRequriedGSl(int? _id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync("RequiredGsl/" + _id);
                if (response.IsSuccessStatusCode)
                {
                    return response.IsSuccessStatusCode;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
            }


            return false;
        } 
        public virtual async Task<int> deleteRequriedGSldetail(int? _id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync("RequiredGsldetail/" + _id);
                if (response.IsSuccessStatusCode)
                {
                    return 0;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
            }


            return 0;
        }
        public virtual async Task<int> DeleteActivityDefinitionById(int _id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync("ActivityDefinition/" + _id);
                if (response.IsSuccessStatusCode)
                {
                    return 0;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
            }
            return 0;
        }
        public virtual async Task<int> DeleteActivityById(int _id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync("Activity/" + _id);
                if (response.IsSuccessStatusCode)
                {
                    return 0;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
            }


            return 0;
        } 
        public virtual async Task<int> DeleteUserRoleMapByCode(int _id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync("UserRoleMapper/" + _id);
                if (response.IsSuccessStatusCode)
                {
                    return 0;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
            }


            return 0;
        } 
        public virtual async Task<int> DeleteCosnigneeById(int _id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync("Consignee/" + _id);
                if (response.IsSuccessStatusCode)
                {
                    return 0;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
            }


            return 0;
        } 
        public virtual async Task<int> DeleteCosnigneeunitByid(int _id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync("ConsigneeUnit/" + _id);
                if (response.IsSuccessStatusCode)
                {
                    return 0;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
            }


            return 0;
        } 
        public virtual async Task<int> DeleteAccountMapById(int _id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync("AccountMap/" + _id);
                if (response.IsSuccessStatusCode)
                {
                    return 0;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
            }


            return 0;
        }
        public virtual async Task<int> DeleteIdsettingByID(int _id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync("Idsetting/" + _id);
                if (response.IsSuccessStatusCode)
                {
                    return 0;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
            }


            return 0;
        }
        public virtual async Task<int> DeleteIddefinitionByID(int _id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync("Iddefinition/" + _id);
                if (response.IsSuccessStatusCode)
                {
                    return 0;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
            }


            return 0;
        }
       public virtual async Task<int> deletetermDefnById(int _id)
            {
                try
                {
                    var response = await _httpClient.DeleteAsync("TermDefinition/" + _id);
                    if (response.IsSuccessStatusCode)
                    {
                        return 0;
                    }
                    else
                    {
                        Console.WriteLine("Error Calling Web Api");
                    }
                }
                catch (Exception e)
                {

                    Debug.WriteLine(e.Message);
                }


                return 0;
            }
       public virtual async Task<int> DeleteRelationSateById(int _id)
            {
                try
                {
                    var response = await _httpClient.DeleteAsync("RelationalState/" + _id);
                    if (response.IsSuccessStatusCode)
                    {
                        return 0;
                    }
                    else
                    {
                        Console.WriteLine("Error Calling Web Api");
                    }
                }
                catch (Exception e)
                {

                    Debug.WriteLine(e.Message);
                }


                return 0;
            } 
        public virtual async Task<int> DeleteRelationById(int? _id)
            {
                try
                {
                    var response = await _httpClient.DeleteAsync("Relation/" + _id);
                    if (response.IsSuccessStatusCode)
                    {
                        return 0;
                    }
                    else
                    {
                        Console.WriteLine("Error Calling Web Api");
                    }
                }
                catch (Exception e)
                {

                    Debug.WriteLine(e.Message);
                }


                return 0;
            }
       public virtual async Task<int> DeleteVoucherExtensionById(int _id)
            {
                try
                {
                    var response = await _httpClient.DeleteAsync("VoucherExtensionDefinition/" + _id);
                    if (response.IsSuccessStatusCode)
                    {
                        return 0;
                    }
                    else
                    {
                        Console.WriteLine("Error Calling Web Api");
                    }
                }
                catch (Exception e)
                {

                    Debug.WriteLine(e.Message);
                }


                return 0;
            }
        public virtual async Task<bool> DeletepreferentilaaccsessBycode(int _id)
            {
                try
                {
                    var response = await _httpClient.DeleteAsync("PreferenceAccess/" + _id);
                    if (response.IsSuccessStatusCode)
                    {
                        return response.IsSuccessStatusCode;
                    }
                    else
                    {
                        Console.WriteLine("Error Calling Web Api");
                    }
                }
                catch (Exception e)
                {

                    Debug.WriteLine(e.Message);
                }


                return false;
            }
        public virtual async Task<int> DleteStoreMap(int _id)
            {
                try
                {
                    var response = await _httpClient.DeleteAsync("VoucherStoreMapping/" + _id);
                    if (response.IsSuccessStatusCode)
                    {
                        return 0;
                    }
                    else
                    {
                        Console.WriteLine("Error Calling Web Api");
                    }
                }
                catch (Exception e)
                {

                    Debug.WriteLine(e.Message);
                }


                return 0;
            }
  
        public virtual async Task<bool> DeletefieldformatById(int _id)
            {
                try
                {
                    var response = await _httpClient.DeleteAsync("FieldFormat/" + _id);
                    if (response.IsSuccessStatusCode)
                    {
                        return response.IsSuccessStatusCode;
                    }
                    else
                    {
                        Console.WriteLine("Error Calling Web Api");
                    }
                }
                catch (Exception e)
                {

                    Debug.WriteLine(e.Message);
                }


                return false;
            }
       public virtual async Task<int> DeletegsltaxByreference(int? _id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync("Gsltax/" + _id);
                if (response.IsSuccessStatusCode)
                {
                    return 0;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
            }


            return 0;
        }
           public virtual async Task<int> DeleteIdentificationById(int _id)
            {
                try
                {
                    var response = await _httpClient.DeleteAsync("Identification/" + _id);
                    if (response.IsSuccessStatusCode)
                    {
                        return 0;
                    }
                    else
                    {
                        Console.WriteLine("Error Calling Web Api");
                    }
                }
                catch (Exception e)
                {

                    Debug.WriteLine(e.Message);
                }


                return 0;
            } 
        public virtual async Task<int> DeleteAttachment(int? _id)
            {
                try
                {
                    var response = await _httpClient.DeleteAsync("Attachment/" + _id);
                    if (response.IsSuccessStatusCode)
                    {
                        return 0;
                    }
                    else
                    {
                        Console.WriteLine("Error Calling Web Api");
                    }
                }
                catch (Exception e)
                {

                    Debug.WriteLine(e.Message);
                }


                return 0;
            }
        public virtual async Task<int> DeleteBankDetailById(int? _id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync("BankAccountDetail/" + _id);
                if (response.IsSuccessStatusCode)
                {
                    return 0;
                }
                else
                {
                    Console.WriteLine("Error Calling Web Api");
                }
            }
            catch (Exception e)
            {

                Debug.WriteLine(e.Message);
            }
            return 0;

        }
             public virtual async Task<int> DeletejobDesc(int? _id)
            {
                try
                {
                    var response = await _httpClient.DeleteAsync("JobDescription/" + _id);
                    if (response.IsSuccessStatusCode)
                    {
                        return 0;
                    }
                    else
                    {
                        Console.WriteLine("Error Calling Web Api");
                    }
                }
                catch (Exception e)
                {

                    Debug.WriteLine(e.Message);
                }


                return 0;
            } 
        public virtual async Task<bool> DeletaccrequiredBycode(int? _id)
            {
                try
                {
                    var response = await _httpClient.DeleteAsync("GslacctRequirement/" + _id);
                    if (response.IsSuccessStatusCode)
                    {
                        return response.IsSuccessStatusCode;
                    }
                    else
                    {
                        Console.WriteLine("Error Calling Web Api");
                    }
                }
                catch (Exception e)
                {

                    Debug.WriteLine(e.Message);
                }


                return false;
            } 
        public virtual async Task<bool> DeleteDistributionById(int? _id)
            {
                try
                {
                    var response = await _httpClient.DeleteAsync("Distribution/" + _id);
                    if (response.IsSuccessStatusCode)
                    {
                        return response.IsSuccessStatusCode;
                    }
                    else
                    {
                        Console.WriteLine("Error Calling Web Api");
                    }
                }
                catch (Exception e)
                {

                    Debug.WriteLine(e.Message);
                }


                return false;
            }
             public virtual async Task<int> DeletevalueDEfnById(int? _id)
            {
                try
                {
                    var response = await _httpClient.DeleteAsync("ValueFactorDefinition/" + _id);
                    if (response.IsSuccessStatusCode)
                    {
                        return 0;
                    }
                    else
                    {
                        Console.WriteLine("Error Calling Web Api");
                    }
                }
                catch (Exception e)
                {

                    Debug.WriteLine(e.Message);
                }


                return 0;
            }
        public virtual async Task<int> DeletegslaccountmapById(int? _id)
            {
                try
                {
                    var response = await _httpClient.DeleteAsync("AccountMap/" + _id);
                    if (response.IsSuccessStatusCode)
                    {
                        return 0;
                    }
                    else
                    {
                        Console.WriteLine("Error Calling Web Api");
                    }
                }
                catch (Exception e)
                {

                    Debug.WriteLine(e.Message);
                }


                return 0;
            }

        #endregion

        public virtual async Task<ConsigneeDTO?> GetCompany()
        {
            var response = await _httpClient.GetAsync("Consignee/filter?gslType=1");
            if (!response.IsSuccessStatusCode)
                return null;

            var jconsignee = await response.Content.ReadAsStringAsync();
            var _consigneeDTO = JsonConvert.DeserializeObject<List<ConsigneeDTO>>(jconsignee);

            _organization = _consigneeDTO != null && _consigneeDTO.Count() > 0 ? _consigneeDTO?.FirstOrDefault() : null;

            return _organization;
        }
        public virtual async Task<ScheduleDTO?> GetScheduleByReference(int? refe)
        {

            var response = await _httpClient.GetAsync("Schedule/filter?reference=" + refe);
            if (!response.IsSuccessStatusCode)
                return null;

            var jconsignee = await response.Content.ReadAsStringAsync();
            var _scheDTO = JsonConvert.DeserializeObject<List<ScheduleDTO>>(jconsignee);

            return _scheDTO?.FirstOrDefault();
        }
        public virtual async Task<List<ScheduleDetailDTO>> GetScheduleDetailsByRefAndType(int? refe, string headerType)
        {
            string endpoint = "VwScheduleDetail/filter";
            string queryString = $"?referenc={refe}&headerType={headerType}";
            string requestUrl = $"{endpoint}{queryString}";
            var response = await _httpClient.GetAsync(requestUrl);
            if (!response.IsSuccessStatusCode)
                return null;

            var jconsignee = await response.Content.ReadAsStringAsync();
            var _scheDTO = JsonConvert.DeserializeObject<List<ScheduleDetailDTO>>(jconsignee);

            return _scheDTO;
        }
        public virtual async Task<List<ScheduleHeaderDTO>> SelectScheduleHeaderById(int? id)
        {

            var response = await _httpClient.GetAsync("ScheduleHeader/filter?id=" + id);
            if (!response.IsSuccessStatusCode)
                return null;

            var jconsignee = await response.Content.ReadAsStringAsync();
            var _schehdrDTO = JsonConvert.DeserializeObject<List<ScheduleHeaderDTO>>(jconsignee);

            return _schehdrDTO;
        }
        public virtual async Task<List<IdsettingDTO>> GetIdsettingById(int? id)
        {

            var response = await _httpClient.GetAsync("Idsetting/filter?id=" + id);
            if (!response.IsSuccessStatusCode)
                return null;

            var jconsignee = await response.Content.ReadAsStringAsync();
            var _idStDTO = JsonConvert.DeserializeObject<List<IdsettingDTO>>(jconsignee);

            return _idStDTO;
        }
        public virtual async Task<List<IddefinitionDTO>> GetIdDefinitionById(int? id)
        {

            var response = await _httpClient.GetAsync("Iddefinition/filter?id=" + id);
            if (!response.IsSuccessStatusCode)
                return null;

            var jconsignee = await response.Content.ReadAsStringAsync();
            var _idStDTO = JsonConvert.DeserializeObject<List<IddefinitionDTO>>(jconsignee);

            return _idStDTO;
        }

        public virtual async Task<UserDTO?> GetUserByUserName(string _userName)
        {
            if (_loggedInUser != null)
                return _loggedInUser;

            var response = await _httpClient.GetAsync("User/filter?userName=" + _userName);
            if (!response.IsSuccessStatusCode)
                return null;

            var juser = await response.Content.ReadAsStringAsync();
            var usernameUser = JsonConvert.DeserializeObject<List<UserDTO>>(juser);

            _loggedInUser = usernameUser != null && usernameUser.Count > 0 ? usernameUser.FirstOrDefault() : null;

            return _loggedInUser;
        }
        public virtual async Task<List<UserDTO>?> GetUserById(int? id)
        {
           

            var response = await _httpClient.GetAsync("User/filter?id=" + id);
            if (!response.IsSuccessStatusCode)
                return null;

            var juser = await response.Content.ReadAsStringAsync();
            var usernameUser = JsonConvert.DeserializeObject<List<UserDTO>>(juser);

          
            return usernameUser;
        }  
        public virtual async Task<List<UserDTO>?> GetUserBypass(string pass)
        {
           

            var response = await _httpClient.GetAsync("User/filter?password=" + pass);
            if (!response.IsSuccessStatusCode)
                return null;

            var juser = await response.Content.ReadAsStringAsync();
            var usernameUser = JsonConvert.DeserializeObject<List<UserDTO>>(juser);

          
            return usernameUser;
        }


        public virtual async Task<List<ConsigneeUnitDTO>?> GetCompanyBranchsByTin(string? _tin)
        {
            var _CompanyBranchs = new List<ConsigneeUnitDTO>();

            var response = await _httpClient.GetAsync("SysInitialize?deviceName=web&tin=" + _tin + "&platform=0&isWeb=true");
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysDto = await response.Content.ReadAsStringAsync();
            var sysDto_ = JsonConvert.DeserializeObject<ResponseModel<SystemInitDTO>>(jsysDto);

            _CompanyBranchs = sysDto_ != null ? sysDto_?.Data.branches?.ToList() : null;

            return _CompanyBranchs;
        }
        //public virtual async Task<List<ConsigneeUnitDTO>> GetConsigneeByType()
        //{
        //    var response = await _httpClient.GetAsync("ConsigneeUnit/filter?type=1719&consignee=39763");
        //    if (!response.IsSuccessStatusCode)
        //        return null;

        //    var jsysconstDto_ = await response.Content.ReadAsStringAsync();
        //    var Taxto = JsonConvert.DeserializeObject<List<ConsigneeUnitDTO>>(jsysconstDto_);
        //    return Taxto;

        //}
        public virtual async Task<List<SystemConstantDTO>> GetSytemConstantBytype(string _type)
        {

            var response = await _httpClient.GetAsync("SystemConstant/filter?type=" + _type);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var sysconstDto_ = JsonConvert.DeserializeObject<List<SystemConstantDTO>>(jsysconstDto_);

            return sysconstDto_;
        } 
        public virtual async Task<List<SystemConstantDTO>> GetSytemConstantBytypeandId(int id,string _type)
        {
            string endpoint = "SystemConstant/filter";
            string queryString = $"?id={id}&type={_type}";
            string requestUrl = $"{endpoint}{queryString}";
            var response = await _httpClient.GetAsync(requestUrl);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var sysconstDto_ = JsonConvert.DeserializeObject<List<SystemConstantDTO>>(jsysconstDto_);

            return sysconstDto_;
        } 
        public virtual async Task<List<VwVoucherRelationViewDTO>> GetVoucherRelationView()
        {

            var response = await _httpClient.GetAsync("VwVoucherRelationView/dynamic");
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var sysconstDto_ = JsonConvert.DeserializeObject<List<VwVoucherRelationViewDTO>>(jsysconstDto_);

            return sysconstDto_;
        }
        public virtual async Task<List<RelationDTO>> SelectRelation()
        {

            var response = await _httpClient.GetAsync("Relation/dynamic");
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var resysconstDto_ = JsonConvert.DeserializeObject<List<RelationDTO>>(jsysconstDto_);

            return resysconstDto_;
        }

        public virtual async Task<List<OrderStationMapDTO>> GetOrderStationMapByPosdevandstadev(string posDevice, string stationDevice)
        {

            string endpoint = "OrderStationMap/filter";
            string queryString = $"?posDevice={posDevice}&stationDevice={stationDevice}";
            string requestUrl = $"{endpoint}{queryString}";
            var response = await _httpClient.GetAsync(requestUrl);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var derStationDto_ = JsonConvert.DeserializeObject<List<OrderStationMapDTO>>(jsysconstDto_);

            return derStationDto_;
        }
        public virtual async Task<List<PreferenceDTO>> GetpreferenceByreference(int? refnce)
        {

            var response = await _httpClient.GetAsync("Preference/filter?SystemConstant=" + refnce);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var preferenceDto_ = JsonConvert.DeserializeObject<List<PreferenceDTO>>(jsysconstDto_);

            return preferenceDto_;
        }
        public virtual async Task<List<OrderStationMapDTO>> GetOrderStationByPosDevice(int? posdevice)
        {

            var response = await _httpClient.GetAsync("OrderStationMap/filter?posDevice=" + posdevice);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var preferenceDto_ = JsonConvert.DeserializeObject<List<OrderStationMapDTO>>(jsysconstDto_);

            return preferenceDto_;
        }
        public virtual async Task<List<PreferenceDTO>> GetpreferenceByreference()
        {

            var response = await _httpClient.GetAsync("Preference/dynamic");
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var preferenceDto_ = JsonConvert.DeserializeObject<List<PreferenceDTO>>(jsysconstDto_);

            return preferenceDto_;
        } 
        public virtual async Task<List<PreferenceDTO>> GetpreferenceByparent(int parent)
        {

            var response = await _httpClient.GetAsync("Preference/filter?parentId=" + parent);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var preferenceDto_ = JsonConvert.DeserializeObject<List<PreferenceDTO>>(jsysconstDto_);

            return preferenceDto_;
        }
       
        public virtual async Task<List<FunctionalityDTO>> SelectAllFunctionality()
        {

            var response = await _httpClient.GetAsync("Functionality/dynamic");
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var sysconstDto_ = JsonConvert.DeserializeObject<List<FunctionalityDTO>>(jsysconstDto_);

            return sysconstDto_;
        }  
        public virtual async Task<List<ViewFunctWithAccessMDTO>> GetFuncwithAccessMatViewBy(int role,int cate,int subsys)
        {
            string endpoint = "ViewFunctWithAccessM/filter";
            string queryString = $"?role={role}&category={cate}&subSystemComponent={subsys}";
            string requestUrl = $"{endpoint}{queryString}";
            var response = await _httpClient.GetAsync(requestUrl);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var sysconstDto_ = JsonConvert.DeserializeObject<List<ViewFunctWithAccessMDTO>>(jsysconstDto_);

            return sysconstDto_;
        } 
      
        public virtual async Task<List<CnetlicenseDTO>> LicenseBufferList()
        {

            var response = await _httpClient.GetAsync("Cnetlicense/dynamic");
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var sysconstDto_ = JsonConvert.DeserializeObject<List<CnetlicenseDTO>>(jsysconstDto_);

            return sysconstDto_;
        } 
        public virtual async Task<List<AttachmentDTO>> GetattachmentbyId(int _idd)
        {

            var response = await _httpClient.GetAsync("Attachment/filter?id=" +_idd);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var sysconstDto_ = JsonConvert.DeserializeObject<List<AttachmentDTO>>(jsysconstDto_);

            return sysconstDto_;
        }
        public virtual async Task<List<CnetlicenseDTO>> GetLicenseBylicence(string _licenceCoe)
        {

            var response = await _httpClient.GetAsync("Cnetlicense/filter?licenseCode=" + _licenceCoe);
            if (!response.IsSuccessStatusCode)
                return null;

            var jsysconstDto_ = await response.Content.ReadAsStringAsync();
            var sysconstDto_ = JsonConvert.DeserializeObject<List<CnetlicenseDTO>>(jsysconstDto_);

            return sysconstDto_;
        }
        public virtual async Task<List<FieldFormatDTO>?> DocumetntFieldFormat(int? _ref)
        {
            var _listofFieldFormat = new List<FieldFormatDTO>();

            var response = await _httpClient.GetAsync("FieldFormat/filter?reference=" + _ref);
            if (!response.IsSuccessStatusCode)
                return null;

            var jfiDto = await response.Content.ReadAsStringAsync();
            var fiDto = JsonConvert.DeserializeObject<List<FieldFormatDTO>>(jfiDto);

            _listofFieldFormat = fiDto != null ? fiDto?.ToList() : null;

            return _listofFieldFormat;
        }

        public async Task<GlobalParamsModel> GetGlobalParams(UserDTO? userdto)
        {
            if (userdto == null || string.IsNullOrEmpty(userdto.Remark))
            {
                return null;
            }

            string[] splitRem = userdto.Remark.Split('_');
            string _rpx = splitRem.Length >= 2 ? splitRem[1]?.Trim() : null;
            string _rpB = splitRem[0];
            var xtin = await GetCompany();
            string endpoint = "SysInitialize/authenticate";
            string queryString = $"?userName={userdto.UserName}&password={Decrypt(_rpx)}&app_type=1&tin={xtin.Tin}";
            string requestUrl = $"{endpoint}{queryString}";

            HttpResponseMessage response = await _httpClient.GetAsync(requestUrl);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            string jsonResponse = await response.Content.ReadAsStringAsync();
            var userValidation = JsonConvert.DeserializeObject<ResponseModel<LoginResponse>>(jsonResponse);


            var globalParams = new GlobalParamsModel
            {
                navigatorList = userValidation.Data?.navigatorList,
                personInfo = userValidation.Data?.personInfo
            };

            return globalParams;
        }

        public async Task<T> GetFilterDynamicData<T>(string Database, Dictionary<string, string> keyValuePairs)
        {
            string Filtervalues = "";
            int totalcount = keyValuePairs.Count;
            int count = 0;
            foreach (var values in keyValuePairs)
            {
                if (count > 0)
                    Filtervalues += "&";

                Filtervalues += values.Key + "=" + values.Value;

                count++;
            }
            string uri = string.Format(Database + "/?{0}", Filtervalues);

            return await GetReqAsync<T>(uri);
        }
        public async Task<T> GetFilterData<T>(string Database, Dictionary<string, string> keyValuePairs)
        {
            string Filtervalues = "";
            int totalcount = keyValuePairs.Count;
            int count = 0;
            foreach (var values in keyValuePairs)
            {
                if (count > 0)
                    Filtervalues += "&";

                Filtervalues += values.Key + "=" + values.Value;

                count++;
            }
            string uri = string.Format(Database + "/filter?{0}", Filtervalues);

            return await GetReqAsync<T>(uri);
        }

        public async Task<TResponse> GetReqAsync<TResponse>(string path)
        {

            HttpResponseMessage response = await _httpClient.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var responseJson = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<TResponse>(responseJson);
                return responseData;
            }
            else
            {
                var responseJson = await response.Content.ReadAsStringAsync();
                throw new Exception(responseJson);
            }
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

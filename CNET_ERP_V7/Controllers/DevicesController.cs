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
using System.Security.Cryptography.Xml;
using Cnetv7BufferHolder;
using DevExtreme.AspNet.Mvc.FileManagement;

namespace CNET_ERP_V7.Controllers
{
    public class DevicesController : Controller
    {

        private AuthenticationManager _authenticationManager;
        private readonly HttpClient _httpClient;
        private readonly SharedHelpers _sharedHelpers;
        public List<ConfigurationDTO> Config = new List<ConfigurationDTO>();
       
        #region Ctor
        public DevicesController(AuthenticationManager authenticationManager,
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
            return View(new Devicemodel
            {
                DeviceType = id,

            });
        }
        public async Task<IActionResult> createDevices(Devicemodel Dmodel)
        {

            string DeviceName = "";
            string type = "";
            string result = "";
            bool isSaved = false;
            DeviceDTO createdevic = new DeviceDTO();

            if (Dmodel.devicename != "Email Server" && Dmodel.devicename != null)
            {
                DeviceName = Dmodel.devicename;
                type = Dmodel.branchservername;
            }
            else
            {
                DeviceName = Dmodel.Eamildevicename;
                type = Dmodel.branchservername;
            }
            if (!Convert.ToBoolean(Dmodel.DeviceMaintanflage))
            {

                if (type == "Database Server" || type == "E-Commerce Server" || type == "IP TV Server" || type == "Application Server" || type == "VOD Server" ||
                    type == "File Server" || type == "CNET Cloud Server" || type == "Client Cloud Server" || type == "Local Web Server" || type == "License Server"
                    || type == "FTP Server" || type == "Telegram Bot Server")
                {
                    var data = await _sharedHelpers.GetViewDeviceByReferenceBypreference(Dmodel.preferencecodeforsave);
                    if (data.Count() >= 1)
                    {
                        result = "Can't Maintain more than one " + type + " !!";
                        isSaved = false;
                        return Json(new { data = result, isSaved = isSaved });
                    }

                }
                List<PreferenceDTO> _pref = new List<PreferenceDTO>();
                 _pref = await _sharedHelpers.GetPreferenceByDescription("Default Device Preference");
                if (_pref ==null || _pref.Count() == 0)
                {
                    PreferenceDTO Crepreference = new PreferenceDTO();
                    Crepreference.Id = 0;
                    Crepreference.SystemConstant = 15;
                    Crepreference.Description = "Default Device Preference";
                    Crepreference.Index = 1;
                    var creatprf = await _sharedHelpers.Createpreference(Crepreference);
                }
                 _pref = await _sharedHelpers.GetPreferenceByDescription("Default Device Preference");

                ArrayList list = new ArrayList();
                var CVA = await _sharedHelpers.SelectArticleByName(DeviceName);
                if (CVA == null || CVA?.Count() <= 0)
                {

                    if (Dmodel.connctionTYpe == 0 || string.IsNullOrWhiteSpace(DeviceName) || String.IsNullOrWhiteSpace(Dmodel?.model.ToString()))
                    {

                        result = "Pleas select connection type Or Enter device name Or select Model";
                        isSaved = false;
                        return Json(new { data = result, isSaved = isSaved });
                    }

                    ArticleDTO art = new ArticleDTO();
                    art.Id = 0;
                    art.GslType = 15;
                    art.LocalCode = "FA-00028-";
                    art.Name = DeviceName;
                    art.Description = DeviceName;
                    art.Preference = Convert.ToInt32(_pref?.FirstOrDefault()?.Id);
                    art.Uom = 1419;
                    art.IsActive = true;
                    art.Brand = Dmodel.brand;
                    art.Generic = Dmodel.generic;
                    art.Model = GeneralBufferHolder.SystemConstants.Where(x => x.Id == Dmodel.model)?.FirstOrDefault()?.Description;
                    art.Size = Dmodel.size;
                    art.Color = Dmodel.color;
                    art.CountryOrigin = Dmodel.country;
                    art.Manufacturer = Dmodel.manufacturer;
                    art.CreatedOn = DateTime.Now;
                    art.LastModified = DateTime.Now;
                    var artcode = await _sharedHelpers.CreateArticle(art);
                    var CVD = await _sharedHelpers.SelectArticleByName(DeviceName);
                    if (CVD != null || CVD?.Count() > 0)
                    {
                        DeviceDTO dev = new DeviceDTO();
                        dev.Preference = Dmodel.preferencecodeforsave;
                        dev.Article = (int)artcode?.Id;
                        dev.ConsigneeUnit = Dmodel.branchcodeforsave;
                        dev.MachineName = DeviceName;
                        dev.Description = DeviceName;
                        dev.IpAddress = Dmodel.ip_address;
                        dev.IpPort = Dmodel.devie_Port;
                        dev.MacAddress = Dmodel.device_mac;
                        dev.SerialPort = Dmodel.serial_port;
                        dev.BaudRate = Dmodel.baud_rate == null ? Dmodel.baud_rate : Convert.ToInt32(GeneralBufferHolder.SystemConstants.FirstOrDefault(x => x.Id == Dmodel.baud_rate)?.Description);
                        if (Dmodel.host > 0)
                        {
                            dev.Host = Dmodel.host;
                        }

                        dev.ConnectionType = Dmodel.connctionTYpe;
                        dev.DeviceValue = Dmodel.devicevalue;
                        if (Dmodel.Isactive == true)
                        {
                            dev.IsActive = true;
                        }
                        else
                        {
                            dev.IsActive = false;
                        }
                        dev.Remark = Dmodel.remark;
                        int code = _sharedHelpers.CreateDevice(dev).Id;

                        result = "Save Successfully";
                        isSaved = true;
                        return Json(new { data = result, isSaved = isSaved });

                    }
                    else
                    {
                        result = "Please check current Fixed Asset Id Setting values";
                        isSaved = true;
                        return Json(new { data = result, isSaved = isSaved });
                    }
                }
                else
                {
                    list.Add(DeviceName + " is already exist!");
                }
                if (list.Count > 0)
                {
                    string temporary = string.Empty;
                    foreach (var i in list)
                    {
                        temporary = temporary + i.ToString();

                    }
                    result = temporary;
                    isSaved = false;
                    return Json(new { data = result, isSaved = isSaved });

                }

            }
            else
            {
                if (Dmodel.connctionTYpe == 0 || string.IsNullOrWhiteSpace(DeviceName) || String.IsNullOrWhiteSpace(Dmodel?.model.ToString()))
                {

                    result = "Pleas select connection type Or Enter device name Or select Model";
                    isSaved = false;
                    return Json(new { data = result, isSaved = isSaved });
                }
                else
                {
                    List<PreferenceDTO> _pref = new List<PreferenceDTO>();
                    _pref = await _sharedHelpers.GetPreferenceByDescription("Default Device Preference");
                    if (_pref == null || _pref.Count() == 0)
                    {
                        PreferenceDTO Crepreference = new PreferenceDTO();
                        Crepreference.Id = 0;
                        Crepreference.SystemConstant = 15;
                        Crepreference.Description = "Default Device Preference";
                        Crepreference.Index = 1;
                        var creatprf = await _sharedHelpers.Createpreference(Crepreference);
                    }
                    _pref = await _sharedHelpers.GetPreferenceByDescription("Default Device Preference");


                    var devicees = await _sharedHelpers.SelectDeviceByarticle(Dmodel.fixedassetid);
                    ArticleDTO art = new ArticleDTO();
                    art.Id = Dmodel.fixedassetid;
                    art.LocalCode = "FA-00028-";
                    art.GslType = 15;
                    art.Name = DeviceName;
                    art.Preference = _pref.FirstOrDefault().Id;
                    art.Uom = 1419;
                    art.IsActive = true;
                    art.Brand = Dmodel.brand;
                    art.Generic = Dmodel.generic;
                    art.Model = GeneralBufferHolder.SystemConstants.Where(x => x.Id == Dmodel.model)?.FirstOrDefault()?.Description;
                    art.Size = Dmodel.size;
                    art.Color = Dmodel.color;
                    art.CountryOrigin = Dmodel.country;
                    art.Manufacturer = Dmodel.manufacturer;
                    art.CreatedOn = DateTime.Now;
                    art.LastModified = DateTime.Now;
                    await _sharedHelpers.UpdateArticle(art);
                    DeviceDTO devices = new DeviceDTO();
                    devices.Id = devicees.FirstOrDefault().Id;
                    devices.Article = Dmodel.fixedassetid;
                    devices.MachineName = DeviceName;
                    devices.Description = DeviceName;
                    devices.ConnectionType = Dmodel.connctionTYpe;
                    devices.ConsigneeUnit = Dmodel.branchcodeforsave;
                    devices.IpAddress = Dmodel.ip_address;
                    devices.IpPort = Dmodel.devie_Port;
                    devices.MacAddress = Dmodel.device_mac;
                    devices.SerialPort = Dmodel.serial_port;
                    devices.BaudRate = Dmodel.baud_rate == null ? Dmodel.baud_rate : Convert.ToInt32(GeneralBufferHolder.SystemConstants.FirstOrDefault(x => x.Id == Dmodel.baud_rate)?.Description);
                    if (Dmodel.host != 0)
                    {
                        devices.Host = Dmodel.host; ;
                    }

                    devices.Preference = Dmodel.preferencecodeforsave;
                    devices.DeviceValue = Dmodel.devicevalue;
                    if (Dmodel.Isactive)
                    {
                        devices.IsActive = true;
                    }
                    else
                    {
                        devices.IsActive = false;
                    }

                    devices.Remark = Dmodel.remark;
                    await _sharedHelpers.UpdateDevice(devices);

                    result = "Save Successfully";
                    isSaved = true;
                    return Json(new { data = result, isSaved = isSaved });
                }
            }
            return Json(new { data = result, isSaved = isSaved });
        }

        public async Task<IActionResult> getPropertycurrentvalue(string id)
        {
            string code = id;
            var deviceConfig = await _sharedHelpers.GetConfigurationByRefandPref(code, CNETConstants.DEVICE);

            var devicsettingpropertycurvallue = deviceConfig.Select(y => new

            {
                id = y.Id,
                currentvalue = y.CurrentValue,
                attrinute = y.Attribute,
            }).ToList();

            return Json(new { data = devicsettingpropertycurvallue });
        }
        public async Task<IActionResult> currentPropertyvalue(int id, int devcode)
        {
            Devicemodel _deviceId = new Devicemodel();
            _deviceId.deviceId = id;
            _deviceId.DeviceType = devcode;
            return PartialView("deviceSetting", _deviceId);
        }
        public async Task<IActionResult> GetmaintainedDevice(int Prefcode)
        {
            var deviceTypes = await _sharedHelpers.GetViewDeviceByReferenceBypreference(Prefcode);

            var deviceList = deviceTypes.Where(X => X.IsActive == true).ToList();
            var deviceView = new Devicemodel() { deviceViews = deviceList };


            return PartialView("_serverdevicename", deviceView);

        }
        public async Task<IActionResult> getdevicedetailResult(int code, int deviceCode)
        {
            var deviceList = await _sharedHelpers.SelectAllDeviceByOrganizationalUnitDefinition(code);
            var device = deviceList.Where(x => x.Preference == deviceCode).ToList();

            var DeviceList = new Devicemodel() { deviceDetail = device };

            return PartialView("devicedetail", DeviceList);

        }
        public async Task<IActionResult> GetdevicemINBycode(int code)
        {
            var ViewDeviceList = await _sharedHelpers.GetViewDeviceByReferenceByartilce(code);
            var ViewDevice = ViewDeviceList?.FirstOrDefault();
           var modell =   GeneralBufferHolder.SystemConstants.Where(x => x.Description == ViewDevice.Model)?.FirstOrDefault()?.Id;
           var buaderate =   GeneralBufferHolder.SystemConstants.Where(x => x.Description == ViewDevice.BaudRate.ToString())?.FirstOrDefault()?.Id;
            return Json(new { artcode = ViewDevice.Article, artupdate = ViewDevice.Article, deviname = ViewDevice.Name, conntype = ViewDevice.ConnectionType, hostname = ViewDevice.Host, devicevalue = ViewDevice.DeviceValue, isactive = ViewDevice.IsActive, brand = ViewDevice.Brand, model = modell, ipport = ViewDevice.IpPort,ipadd = ViewDevice.IpAddress,macadd  = ViewDevice.MacAddress, serialport = ViewDevice.SerialPort, baud  = buaderate, remark = ViewDevice.Remark });
        }


        public async Task<IActionResult> DeletedevicedetailItem(int CODE)
        {
            int artcode = CODE;

            var ViewDevice = await _sharedHelpers.GetViewDeviceByReferenceByartilce(artcode);
            if (ViewDevice != null)
            {
                var hostValues = await _sharedHelpers.SelectDeviceByhost(ViewDevice?.FirstOrDefault()?.Id);
                if (hostValues.Count() == 0)
                {


                    var device = await _sharedHelpers.SelectDeviceById(ViewDevice?.FirstOrDefault()?.Id);

                    Article art = new Article();
                    art.Id = Convert.ToInt32(device?.FirstOrDefault()?.Article);

                    var artccode = art.Id;

                    var deletedevice = await _sharedHelpers.DeleteDevice(ViewDevice?.FirstOrDefault()?.Id);

                    if (Convert.ToBoolean(deletedevice) == true)
                    {


                        var deleteart = await _sharedHelpers.Deletearticle(artccode);

                        var config = await _sharedHelpers.GetConfigurationByRefandPref(ViewDevice?.FirstOrDefault()?.Id.ToString(), CNETConstants.DEVICE);
                        foreach (var item in config)
                        {
                            var deletete = _sharedHelpers.DeleteConfigurationById(item.Id);

                        }
                        return Json("Deleted Successfully");
                    }
                    else
                    {
                        return Json("This Device Is Set on RMS POS  Order Station Maping ");
                    }


                }
                else
                {
                    string valus = "";
                    string valu = "";
                    int _idd = Convert.ToInt32(hostValues?.FirstOrDefault()?.Article);
                    var valuDevice = await _sharedHelpers.SelectArticleById(_idd);
                    if (valuDevice != null)
                    {
                        valu = valuDevice.Name;
                    }
                    if (valu != "")
                    {
                        var pref = await _sharedHelpers.SelectPreferenceByID(hostValues?.FirstOrDefault()?.Preference);

                        valus = pref.Description;
                    }

                    return Json("This Device Is Host For " + valu + "\n" + "In " + valus);
                }
            }

            return null;
        }


        public async Task<IActionResult> SavingdomainProperty(Devicemodel modeldev)
        {
            var devicereferencecode = modeldev.domainreferencecode;

            Config = await _sharedHelpers.GetConfigurationByRefandPref(devicereferencecode, CNETConstants.DEVICE);
            await _sharedHelpers.DeleteConfigurationByReferenceAndPreference(devicereferencecode, CNETConstants.DEVICE);
            List<ConfigurationDTO> _configuration = new List<ConfigurationDTO>();
            ConfigurationDTO config = new ConfigurationDTO();
            string msg = null;
            bool cansave = false;

            if (devicereferencecode == "0" || devicereferencecode == null)
            {
                msg = "Please Select Device";
                cansave = false;
                return Json(new { msg = msg, cansave = cansave });
            }
            else
            {
                string password = "";
                password = Encrypt(modeldev.domain_password);
                config =  await SaveData("Domain Name", modeldev.domain_Name, devicereferencecode);
                _configuration.Add(config);
                config =  await SaveData("Password", password, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("User Name", modeldev.user_name, devicereferencecode);
                _configuration.Add(config);

                await _sharedHelpers.saveOrUpdateRange(_configuration);
                msg = "Save Successfully";
                cansave = true;
                return Json(new { msg = msg, cansave = cansave });
            }

        }
        public async Task<IActionResult> SavingIpTVProperty(Devicemodel modeldev)
        {

            var devicereferencecode = modeldev.Iptvreferencecode;

            Config = await _sharedHelpers.GetConfigurationByRefandPref(devicereferencecode, CNETConstants.DEVICE);
            await _sharedHelpers.DeleteConfigurationByReferenceAndPreference(devicereferencecode, CNETConstants.DEVICE);
            List<ConfigurationDTO> _configuration = new List<ConfigurationDTO>();
            ConfigurationDTO config = new ConfigurationDTO();
            string msg = null;
            bool cansave = false;

            if (devicereferencecode == "0" || devicereferencecode == null)
            {
                msg = "Please Select Device";
                cansave = false;
                return Json(new { msg = msg, cansave = cansave });
            }
            else
            {
                var deviceType = modeldev.preferencedescrption;

                if (modeldev.close_schedule_after == null)
                {
                    config = await SaveData("CloseScheduleAfter", "", devicereferencecode);
                    _configuration.Add(config);
                }
                else
                {
                    config = await SaveData("", modeldev.close_schedule_after, devicereferencecode);
                    _configuration.Add(config);
                }
                config = await SaveData("IISUpdateURl", modeldev.iIIS_UPDATE_url, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("PhysicalUpdateURl", modeldev.physical_update_url, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("UpdateFrequancy", modeldev.Update_frequancy, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Alarm Music", modeldev.Alarm_music, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("AttachmentURl", modeldev.attachement_url, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Back Ground Music", modeldev.back_ground_music, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Back Ground Music Volume", modeldev.back_ground_music_volume, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Hotel Picture", modeldev.hotel_picture, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("MediaRootPath", modeldev.midia_root_path, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("ScrollText", modeldev.scroll_text, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("TV Logo", modeldev.tv_logo, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Room Service POS", modeldev.room_servies_pos, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("VOD Purchase Rule", modeldev.vod_purshes_rule, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("City", modeldev.city_name, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Refresh", modeldev.refresh_time, devicereferencecode);
                  _configuration.Add(config);
                config = await SaveData("Weather Service URL", modeldev.weather_services_url, devicereferencecode);
                _configuration.Add(config);


                await _sharedHelpers.saveOrUpdateRange(_configuration);
                msg = "Save Successfully";
                cansave = true;
                return Json(new { msg = msg, cansave = cansave });
            }
        }

        public async Task<IActionResult> SavingClientSettingProperty(Devicemodel modelclient)

        {
            var devicereferencecode = modelclient.clientdevicecodeType;
            string msg = null;
            bool cansave = false;
            if (devicereferencecode == "0" || devicereferencecode == null)
            {
                msg = "Please Select Device";
                cansave = false;
                return Json(new { msg = msg, cansave = cansave });
            }
            else
            {
                Config = await _sharedHelpers.GetConfigurationByRefandPref(devicereferencecode, CNETConstants.DEVICE);
                await _sharedHelpers.DeleteConfigurationByReferenceAndPreference(devicereferencecode, CNETConstants.DEVICE);
                List<ConfigurationDTO> _configuration = new List<ConfigurationDTO>();
                ConfigurationDTO config = new ConfigurationDTO();

                config = await SaveData("Server Type", modelclient.clientserverType, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Enable Synchronization", modelclient.enable_synchronization, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Max Try Count Download", modelclient.Max_Try_Count_Download.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Max Try Count Upload", modelclient.Max_Try_Count_Upload.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Sync Frequency", modelclient.sync_frequency, devicereferencecode);
                _configuration.Add(config);


                await _sharedHelpers.saveOrUpdateRange(_configuration);
                msg = "Save Successfully";
                cansave = true;
                return Json(new { msg = msg, cansave = cansave });
            }
        }
        public async Task<IActionResult> SavingFileSettingProperty(Devicemodel modelfile)
        {
            string devicereferencecode = modelfile?.filedevicecode.ToString();
            string msg = null;
            bool cansave = false;
            if (devicereferencecode == "0" ||  devicereferencecode == null)
            {
                msg = "Please Select Device";
                cansave = false;
                return Json(new { msg = msg, cansave = cansave });
            }
            else
            {
                Config = await _sharedHelpers.GetConfigurationByRefandPref(devicereferencecode, CNETConstants.DEVICE);
                await _sharedHelpers.DeleteConfigurationByReferenceAndPreference(devicereferencecode, CNETConstants.DEVICE);
                List<ConfigurationDTO> _configuration = new List<ConfigurationDTO>();
                ConfigurationDTO config = new ConfigurationDTO();

                config =  await SaveData("Default Attachment Path", modelfile.default_Attachment_Path, devicereferencecode);
                _configuration.Add(config);

                await _sharedHelpers.saveOrUpdateRange(_configuration);
                msg = "Save Successfully";
                cansave = true;
                return Json(new { msg = msg, cansave = cansave });
            }

        }
        public async Task<IActionResult> SavingFtpSettingProperty(Devicemodel modelftp)

        {
            var devicereferencecode = modelftp.ftpdevicecode;
            string msg = null;
            bool cansave = false;
            if (devicereferencecode == "0" || devicereferencecode == null)
            {
                msg = "Please Select Device";
                cansave = false;
                return Json(new { msg = msg, cansave = cansave });
            }
            else
            {
                Config = await _sharedHelpers.GetConfigurationByRefandPref(devicereferencecode, CNETConstants.DEVICE);
                await _sharedHelpers.DeleteConfigurationByReferenceAndPreference(devicereferencecode, CNETConstants.DEVICE);
                List<ConfigurationDTO> _configuration = new List<ConfigurationDTO>();
                ConfigurationDTO config = new ConfigurationDTO();

                if (modelftp.damp_yard == null)
                {

                    config = await SaveData("Damp Yard", "", devicereferencecode);
                    _configuration.Add(config);
                }
                else
                {
                    config = await SaveData("Damp Yard", modelftp.damp_yard, devicereferencecode);
                    _configuration.Add(config);
                }



                string Host = "";
                string password = "";
                if (!string.IsNullOrWhiteSpace(modelftp.ftp_host))
                {
                    string hostName = modelftp.ftp_host;
                    if (hostName.Contains("ftp://"))
                    {
                        Host = hostName;
                    }
                    else
                    {
                        Host = "ftp://" + hostName;
                    }
                }
                if (!string.IsNullOrWhiteSpace(modelftp.ftp_password))
                {
                    password = Encrypt(modelftp.ftp_password);
                }
                config = await SaveData("FTP Password", password, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("FTP URL", modelftp.ftp_url, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Host", Host, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Local Root Path", modelftp.local_root_path, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Local Root Path For V5", modelftp.local_root_path_for_vs, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Slip Time", modelftp.slip_time_in_second, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("User Name", modelftp.user_name, devicereferencecode);
                _configuration.Add(config);

                await _sharedHelpers.saveOrUpdateRange(_configuration);
                msg = "Save Successfully";
                cansave = true;
                return Json(new { msg = msg, cansave = cansave });
            }

        }

        public async Task<IActionResult> SavingEmailSettingProperty(Devicemodel emailmodel)
        {

            var devicereferencecode = emailmodel.emaildevicecode;
            string msg = null;
            bool cansave = false;
            if (devicereferencecode == "0" || devicereferencecode == null )
            {
                msg = "Please Select Device";
                cansave = false;
                return Json(new { msg = msg, cansave = cansave });
            }
            else
            {
                Config = await _sharedHelpers.GetConfigurationByRefandPref(devicereferencecode, CNETConstants.DEVICE);
                await _sharedHelpers.DeleteConfigurationByReferenceAndPreference(devicereferencecode, CNETConstants.DEVICE);
                List<ConfigurationDTO> _configuration = new List<ConfigurationDTO>();
                ConfigurationDTO config = new ConfigurationDTO();


                string passwordenc = "";
                config = await SaveData("From Email Address", emailmodel.from_Email_Address, devicereferencecode);
                _configuration.Add(config);
                if (!string.IsNullOrEmpty(emailmodel.email_Password))
                {
                    passwordenc = Encrypt(emailmodel.email_Password);
                }
                config = await SaveData("Email Password", passwordenc, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Smtp Server", emailmodel.smtp_Server, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Time Out", emailmodel.time_Out, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Smtp Port", emailmodel.smtp_Port, devicereferencecode);
                _configuration.Add(config);


                await _sharedHelpers.saveOrUpdateRange(_configuration);
                msg = "Save Successfully";
                cansave = true;
                return Json(new { msg = msg, cansave = cansave });
            }
        }
        public async Task<IActionResult> SavingTelegSettingProperty(Devicemodel Telegmodel)
        {

            var devicereferencecode = Telegmodel.teledevicecode;


            Config = await _sharedHelpers.GetConfigurationByRefandPref(devicereferencecode, CNETConstants.DEVICE);
            await _sharedHelpers.DeleteConfigurationByReferenceAndPreference(devicereferencecode, CNETConstants.DEVICE);
            List<ConfigurationDTO> _configuration = new List<ConfigurationDTO>();
            ConfigurationDTO config = new ConfigurationDTO();


            string msg = null;
            bool cansave = false;
            if (devicereferencecode == "0" || devicereferencecode == null)
            {
                msg = "Please Select Device";
                cansave = false;
                return Json(new { msg = msg, cansave = cansave });
                
            }
            else
            {

                string passwordenc = "";
                if (!string.IsNullOrEmpty(Telegmodel.tele_password))
                {
                    passwordenc = Encrypt(Telegmodel.tele_password);
                }
                config= await SaveData("User Name", Telegmodel.teleuser_name, devicereferencecode);
                _configuration.Add(config);

                config =  await SaveData("Password", passwordenc, devicereferencecode);
                _configuration.Add(config);

                config = await SaveData("Signature", Telegmodel.tele_signture, devicereferencecode);
                _configuration.Add(config);


                await _sharedHelpers.saveOrUpdateRange(_configuration);
                msg = "Save Successfully";
                cansave = true;
                return Json(new { msg = msg, cansave = cansave });
            }
        }

        public async Task<IActionResult> SavingrmsSettingProperty(Devicemodel rmsmodel)
        {
            string msg = null;
            bool cansave = false;
            var currentdate = DateTime.Now;
            var devicereferencecode = rmsmodel.rmsdevicecode;
            if (devicereferencecode == "0" || devicereferencecode == null)
            {
                msg = "Please Select Device";
                cansave = false;
                return Json(new { msg = msg, cansave = cansave });
            }
            else
            {
                Config = await _sharedHelpers.GetConfigurationByRefandPref(devicereferencecode, CNETConstants.DEVICE);
                await _sharedHelpers.DeleteConfigurationByReferenceAndPreference(devicereferencecode, CNETConstants.DEVICE);
                List<ConfigurationDTO> _configuration = new List<ConfigurationDTO>();
                ConfigurationDTO config = new ConfigurationDTO();

                config = await SaveData("Enable Dynamic Visual Display", rmsmodel.rmsenableDynamicVisualDisplay, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("POS Theme", rmsmodel.rmspOS_theme, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Navigator Type", rmsmodel.rmsnavigatorType, devicereferencecode);
                _configuration.Add(config);
                var datechange = rmsmodel.rmsthemeChangedDate == null ? currentdate.ToString() : rmsmodel.rmsthemeChangedDate;
                config = await SaveData("Theme Changed Date", datechange, devicereferencecode);
                _configuration.Add(config);
                string AspectRatio = rmsmodel.rmsaspectRatio.Split('_')[1];
                config = await SaveData("Aspect Ratio", AspectRatio, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Change Frequency", rmsmodel.rmschangeFrequency, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Allow Animation", rmsmodel.rmsallowAnimation, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Enable Touch Screen", rmsmodel.rmsenableTouchScreen, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Check Amount Limit For Hold", rmsmodel.rmscheckAmountLimitForHold, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Hold Amount Per Person", rmsmodel.rmsholdAmountPerPerson.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Hold Document Life", rmsmodel.rmshold_Document_Life.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Enable Bill Management", rmsmodel.rmsenableBillManagement, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Company Tin Mandatory", rmsmodel.rmscompanyTinMandatory, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Enable Delivery Management", rmsmodel.rmsenableDeliveryManagement, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Enable Document Reference", rmsmodel.rmsenableDocumentReference, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Enable Room Charge", rmsmodel.rmsenableRoomCharge, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Enable Reservation", rmsmodel.rmsenable_Reservation, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Enable Package Design", rmsmodel.rmsenable_Package_esign, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Station Type", rmsmodel.rmsstation_Type, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Table Selection Type", rmsmodel.rmstable_Selection_Type, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Waiter Selection", rmsmodel.rmswaiter_selection, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("POS Trigger Article Removal", rmsmodel.rmspOS_Trigger_Article_Removal, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Need Credentials For Removal", rmsmodel.rmsneed_Credentials_For_Removal, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Enable Order Function", rmsmodel.rmsenableOrderFunction, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Order Station Mandatory", rmsmodel.rmsorderStationMandatory, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Order Location", rmsmodel.rmsorder_Location, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Order Verification Method", rmsmodel.rmsorder_Verification_Method, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Enable Auto Entry", rmsmodel.rmsenableAutoEntry, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Open Drawer After Print", rmsmodel.rmsopenDrawerAfterPrint, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Check System Timestamp", rmsmodel.rmsCheckSystemTimestamP, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Default Transaction Type", rmsmodel.rmsdefault_Transaction_Type, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Default Payment", rmsmodel.rmsdefault_Payment, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Table Mandatory", rmsmodel.rmstableMandatory, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Waiter Mandatory", rmsmodel.rmsWaiterMandatory, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Input Cover Mandatory", rmsmodel.rmsinputCoverMandatory, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Clear Hold Before Closing", rmsmodel.rmsclearHoldBeforeClosing, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Allow multiple Instance Of Table", rmsmodel.rmsallowmultipleInstanceOfTable, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Enable Vocal Assistance", rmsmodel.rmsenableVocalAssistance, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Language", rmsmodel.rmsLanguage, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Closing Frequency", rmsmodel.rmsclosing_frequency, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("DownLoad EJ", rmsmodel.rmsdownLoad_EJ, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Accuracy Tolerance", rmsmodel.rmsaccuracy_Tolerance.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Summary Difference Tolerance", rmsmodel.rmssummary_Difference_Tolerance.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Check Voucher Integrity", rmsmodel.rmscheck_Voucher_Integrity, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Fiscal Reconciliation", rmsmodel.rmsfiscalReconciliation, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Zeroing Voucher", rmsmodel.rmszeroing_Voucher, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Calculate Consumption Cost", rmsmodel.rmscalculateConsumptionCost, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Summary Voucher", rmsmodel.rmssummary_Voucher, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("F and B", rmsmodel.rmsf_and_B, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Clear Held Bill", rmsmodel.rmsclear_Held_Bill, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Daily Control Count", rmsmodel.rmsdailyControlCount, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Disable Void On Retrieve", rmsmodel.rmsdisableVoidOnRetrieve, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Enable CBE", rmsmodel.rmsenable_cbe, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Capture Fiscal Information", rmsmodel.rmscaptureFiscalInformation, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Enable Value Factory", rmsmodel.rmsenableValueFactory, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Enable GSL Tax", rmsmodel.rmsenable_GSL_Tax, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Pos Theme Fixed", rmsmodel.rmspos_Theme_Fixed, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Select Automatic Shift Period", rmsmodel.rmsselectAutomaticShiftPeriod, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Print Discount & Additional Charge Per LineItem", rmsmodel.rmsprintDiscountAdditionalChargePerLineItem, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("XML Reconciliation", rmsmodel.rmsxML_Reconciliation, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Bold Order No", rmsmodel.rmsbold_Order_No, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Currency Conversion", rmsmodel.rmscurrency_Conversion, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Ignore zero price article", rmsmodel.rms_Ignore_zero_price_article, devicereferencecode);
                      _configuration.Add(config);
                config = await SaveData("Enable Menu designer", rmsmodel.rms_Enable_Menu_designer, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("POS Use Only Available Article", rmsmodel.rms_POSUseOnlyAvailableArticle.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Enable Currency Conversion", rmsmodel.rms_EnableCurrencyConversion.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("POS Value Rule", rmsmodel.rms_POSValueRule.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Need Credentials For Customer Discount", rmsmodel.rms_NeedCredentialsForCustomerDiscount.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Beneficiary User", rmsmodel.rms_BeneficiaryUser.ToString(), devicereferencecode);
                _configuration.Add(config);
                if (!string.IsNullOrWhiteSpace(rmsmodel.rmspOS_Store))
                {
                    string posStore = rmsmodel.rmspOS_Store.Split('/')[0];
                    config = await SaveData("posStore", (posStore), devicereferencecode);
                    _configuration.Add(config);
                }
                else
                {
                    config = await SaveData("posStore", "", devicereferencecode);
                    _configuration.Add(config);
                }
                if (!string.IsNullOrWhiteSpace(rmsmodel.rmscash_Customer))
                {
                    string DefaultObjectState = rmsmodel.rmscash_Customer.Split('/')[0];
                    config = await SaveData("Cash Customer", (DefaultObjectState), devicereferencecode);
                    _configuration.Add(config);
                }
                else
                {
                    config = await SaveData("Cash Customer", "", devicereferencecode);
                    _configuration.Add(config);
                }

                await _sharedHelpers.saveOrUpdateRange(_configuration);
                msg = "Save Successfully";
                cansave = true;
                return Json(new { msg = msg, cansave = cansave });
            }
        }
        public async Task<IActionResult> SavingpmsSettingProperty(Devicemodel devicemodelpms)
        {
            var devicereferencecode = devicemodelpms.pmsdevicecode;
            DateTime currentdate = DateTime.Now;

            Config = await _sharedHelpers.GetConfigurationByRefandPref(devicereferencecode, CNETConstants.DEVICE);
            await _sharedHelpers.DeleteConfigurationByReferenceAndPreference(devicereferencecode, CNETConstants.DEVICE);
            List<ConfigurationDTO> _configuration = new List<ConfigurationDTO>();
            ConfigurationDTO config = new ConfigurationDTO();



            string msg = null;
            bool cansave = false;
            if (devicereferencecode == "0" || devicereferencecode == null)
            {
                msg = "Please Select Device";
                cansave = false;
                return Json(new { msg = msg, cansave = cansave });
            }
            else
            {
                string AspectRatio = null;
                if (string.IsNullOrWhiteSpace(devicemodelpms.pmsaspectRatio))
                {
                    AspectRatio = "";
                }
                else
                {
                  AspectRatio = devicemodelpms.pmsaspectRatio?.Split('_')[1];
                }
                config =  await SaveData("Accuracy Tolerance", devicemodelpms.pmsaccuracy_Tolerance.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Show Online Business Rate", devicemodelpms.pms_ShowOnlineBusinessRate.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Check Voucher Integrity", devicemodelpms.pmscheck_Voucher_Integrity, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Clear Held Bill", devicemodelpms.pmsclear_Held_Bill, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Closing Frequency", devicemodelpms.pmsclosing_frequency, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Daily Control Count", devicemodelpms.pmsdailyControlCount, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Fiscal Reconciliation", devicemodelpms.pmsfiscalReconciliation, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Summary Difference Tolerance", devicemodelpms.pmssummary_Difference_Tolerance.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Summary Voucher", devicemodelpms.pmssummary_Voucher, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Zeroing Voucher", devicemodelpms.pmszeroing_Voucher, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Check Amount Limit For Hold", devicemodelpms.pmscheckAmountLimitForHold, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Clear Hold Before Closing", devicemodelpms.pmsclearHoldBeforeClosing, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Hold Amount Per Person", devicemodelpms.pmsholdAmountPerPerson.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Hold Document Life", devicemodelpms.pmshold_Document_Life.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Enable Order Function", devicemodelpms.pmsenableOrderFunction, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Order Location", devicemodelpms.pmsorder_Location, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Order Verification Method", devicemodelpms.pmsorder_Verification_Method, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Allow multiple Instance Of Table", devicemodelpms.pmsallowmultipleInstanceOfTable, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Company Tin Mandatory", devicemodelpms.pmscompanyTinMandatory, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Disable Void On Retrieve", devicemodelpms.pmsdisableVoidOnRetrieve, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Enable Bill Management", devicemodelpms.pmsenableBillManagement, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Enable CBE", devicemodelpms.pmsenable_cbe, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Enable Document Reference", devicemodelpms.pmsenableDocumentReference, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Enable Room Charge", devicemodelpms.pmsenableRoomCharge, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Need Credentials For Removal", devicemodelpms.pmsneed_Credentials_For_Removal, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Station Type", devicemodelpms.pmsstation_Type, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Table Selection Type", devicemodelpms.pmstable_Selection_Type, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Trigger Article Removal", devicemodelpms.pmsTrigger_Article_Removal, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Waiter Selection", devicemodelpms.pmswaiter_selection, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("POS Theme", devicemodelpms.pmspos_theme, devicereferencecode);
                _configuration.Add(config);
                if (string.IsNullOrWhiteSpace(devicemodelpms.pmswelcome_Message_Rule))
                {
                    config = await SaveData("Welcome Message Rule", "", devicereferencecode);
                    _configuration.Add(config);

                }
                else
                {
                    string RoleCode = devicemodelpms.pmswelcome_Message_Rule.Split('/')[0];
                    config = await SaveData("Welcome Message Rule", devicemodelpms.pmswelcome_Message_Rule, devicereferencecode);
                    _configuration.Add(config);

                }
                config = await SaveData("Check System Timestamp", devicemodelpms.pmsCheckSystemTimestamP, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Default Payment", devicemodelpms.pmsdefault_Payment, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Default Transaction Type", devicemodelpms.pmsdefault_Transaction_Type, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Enable Auto Entry", devicemodelpms.pmsenableAutoEntry, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Input Cover Mandatory", devicemodelpms.pmsinputCoverMandatory, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Open Drawer After Print", devicemodelpms.pmsopenDrawerAfterPrint, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Table Mandatory", devicemodelpms.pmstableMandatory, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Waiter Mandatory", devicemodelpms.pmsWaiterMandatory, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Enable Vocal Assistance", devicemodelpms.pmsenableVocalAssistance, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Language", devicemodelpms.pmsrmsLanguage, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Allow Animation", devicemodelpms.pmsallowAnimation, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Aspect Ratio", AspectRatio, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Change Frequency", devicemodelpms.pmschangeFrequency, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Enable Dynamic Visual Display", devicemodelpms.pmsenableDynamicVisualDisplay, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Enable Touch Screen", devicemodelpms.pmsenableTouchScreen, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Navigator Type", devicemodelpms.pmsnavigatorType, devicereferencecode);
                _configuration.Add(config);

                if (string.IsNullOrWhiteSpace(devicemodelpms.pmstheme_Change_dDate))
                {
                    config = await SaveData("Theme Changed Date", currentdate.ToString("dd/MM/yyyy"), devicereferencecode);
                    _configuration.Add(config);
                }
                else
                {
                    config = await SaveData("Theme Changed Date", devicemodelpms.pmstheme_Change_dDate, devicereferencecode);
                    _configuration.Add(config);
                }

                await _sharedHelpers.saveOrUpdateRange(_configuration);
                msg = "Save Successfully";
                cansave = true;
                return Json(new { msg = msg, cansave = cansave });
            }

        }

        public async Task<IActionResult> SavingrArcaseSettingProperty(Devicemodel devicemodelsms)
        {
            string msg = null;
            bool cansave = false;
            var devicereferencecode = devicemodelsms.arcade_devicecode;
            if (devicereferencecode == "0" || devicereferencecode == null)
            {
                msg = "Please Select Device";
                cansave = false;
                return Json(new {msg = msg,cansave = cansave});
            }
            else
            {

                List<ConfigurationDTO> _configuration = new List<ConfigurationDTO>();
                Config = await _sharedHelpers.GetConfigurationByRefandPref(devicereferencecode, CNETConstants.DEVICE);
                var delete = _sharedHelpers.DeleteConfigurationByReferenceAndPreference(devicereferencecode, CNETConstants.DEVICE);
                ConfigurationDTO config = new ConfigurationDTO();


                config = await SaveData("Accuracy Tolerance", devicemodelsms.arcade_accuracy_Tolerance.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Check Voucher Integrity", devicemodelsms.arcade_check_Voucher_Integrity, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Closing Frequency", devicemodelsms.arcade_closing_frequency, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("DownLoad EJ", devicemodelsms.arcade_downLoad_EJ, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Fiscal Reconciliation", devicemodelsms.arcade_fiscalReconciliation, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Summary Difference Tolerance", devicemodelsms.arcade_summary_Difference_Tolerance.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Summary Voucher", devicemodelsms.arcade_summary_Voucher, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Zeroing Voucher", devicemodelsms.arcade_zeroing_Voucher, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Capture Fiscal Information", devicemodelsms.arcade_capture_Fiscal_Information, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Beneficiary User", devicemodelsms.arcade_BeneficiaryUser.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Print Discount & Additional Charge Per LineItem", devicemodelsms.arcade_printDiscountAdditionalChargePerLineItem, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Select Automatic Shift Period", devicemodelsms.arcade_select_automatic_Shift_Period, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Default Payment", devicemodelsms.arcade_default_Payment, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Open Drawer After Print", devicemodelsms.arcade_openDrawerAfterPrint, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Value Length", devicemodelsms.arcade_value_length.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Value Start", devicemodelsms.arcade_value_start.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Enable Vocal Assistance", devicemodelsms.arcade_enable_vocal_Assistance, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Language", devicemodelsms.arcade_language, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Arcade DB User", devicemodelsms.arcade_arcadeDBUser, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Arcade DB Instance", devicemodelsms.arcade_arcadeDBInstance, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Arcade DB Password", devicemodelsms.arcade_arcadeDBPassword, devicereferencecode);
                _configuration.Add(config);
                if (string.IsNullOrWhiteSpace(devicemodelsms.arcade_DefaultArticle))
                {
                    config = await SaveData("Default Article", "", devicereferencecode);
                    _configuration.Add(config);
                }
                else
                {
                    string artCode = devicemodelsms.arcade_DefaultArticle.Split('/')[0];
                    config = await SaveData("Default Article", artCode, devicereferencecode);
                    _configuration.Add(config);
                }

                await _sharedHelpers.saveOrUpdateRange(_configuration);
                msg = "Save Successfully";
                cansave = true;
                return Json(new { msg = msg, cansave = cansave });
            }

        }
        public async Task<IActionResult> SavingrSMSSettingProperty(Devicemodel devicemodelsms)
        {
            string msg = null;
            bool cansave = false;
            var devicereferencecode = devicemodelsms.sms_devicecode;
            if (devicereferencecode == "0" || devicereferencecode == null)
            {
                msg = "Please Select Device";
                cansave = false;
                return Json(new { msg = msg, cansave = cansave });
            }
            else
            {
                Config = await _sharedHelpers.GetConfigurationByRefandPref(devicereferencecode, CNETConstants.DEVICE);
                await _sharedHelpers.DeleteConfigurationByReferenceAndPreference(devicereferencecode, CNETConstants.DEVICE);
                List<ConfigurationDTO> _configuration = new List<ConfigurationDTO>();
                ConfigurationDTO config = new ConfigurationDTO();


                config = await SaveData("Accuracy Tolerance", devicemodelsms.sms_accuracy_Tolerance.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Check Voucher Integrity", devicemodelsms.sms_check_Voucher_Integrity, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Closing Frequency", devicemodelsms.sms_closing_frequency, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("DownLoad EJ", devicemodelsms.sms_downLoad_EJ, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Fiscal Reconciliation", devicemodelsms.sms_fiscalReconciliation, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Summary Difference Tolerance", devicemodelsms.sms_summary_Difference_Tolerance.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Summary Voucher", devicemodelsms.sms_summary_Voucher, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("XML Reconciliation", devicemodelsms.sms_xML_Reconciliation, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Zeroing Voucher", devicemodelsms.sms_zeroing_Voucher, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Clear Hold Before Closing", devicemodelsms.sms_clearHoldBeforeClosing, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Enable Hold Functionality", devicemodelsms.sms_enableholdFunction, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Capture Fiscal Information", devicemodelsms.sms_capture_Fiscal_Information, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Ignore zero price article", devicemodelsms.sms_ignorezeropricearticle, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("POS Value Rule", devicemodelsms.sms_POSValueRule.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Need Credentials For Customer Discount", devicemodelsms.sms_NeedCredentialsForCustomerDiscount.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Beneficiary User", devicemodelsms.sms_BeneficiaryUser.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Enable Customer Selection", devicemodelsms.sms_EnableCustomerSelection.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Currency Conversion", devicemodelsms.sms_Currency_Conversion.ToString(), devicereferencecode);
                _configuration.Add(config);

                if (!string.IsNullOrWhiteSpace(devicemodelsms.sms_cash_Customer))
                {
                    string cashcustomer = devicemodelsms.sms_cash_Customer.Split('/')[0];
                    config = await SaveData("Cash Customer", (cashcustomer), devicereferencecode);
                    _configuration.Add(config);
                }
                else
                {
                    config = await SaveData("Cash Customer", "", devicereferencecode);
                    _configuration.Add(config);
                }

                if (!string.IsNullOrWhiteSpace(devicemodelsms.sms_day_closing))
                {
                    string dayclosing = devicemodelsms.sms_day_closing.Split('/')[0];
                    config = await SaveData("Day Closing", (dayclosing), devicereferencecode);
                    _configuration.Add(config);
                }
                else
                {
                    config = await SaveData("Day Closing", "", devicereferencecode);
                    _configuration.Add(config);
                }

                config = await SaveData("Enable CBE", devicemodelsms.sms_enable_cbe, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Enable Document Reference", devicemodelsms.sms_enableDocumentReference, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Enable GSL Tax", devicemodelsms.sms_enable_GSL_Tax, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Enable Value Factory", devicemodelsms.sms_enableValueFactory, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Item Selection", devicemodelsms.sms_item_selection, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Need Credentials For Removal", devicemodelsms.sms_need_Credentials_For_Removal, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("POS Trigger Article Removal", devicemodelsms.sms_pOS_Trigger_Article_Removal, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Print Discount & Additional Charge Per LineItem", devicemodelsms.sms_printDiscountAdditionalChargePerLineItem, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Select Automatic Shift Period", devicemodelsms.sms_select_automatic_Shift_Period, devicereferencecode);
                _configuration.Add(config);

                if (!string.IsNullOrWhiteSpace(devicemodelsms.sms_pOS_Store))
                {
                    string posstore = devicemodelsms.sms_pOS_Store.Split('/')[0];
                    config = await SaveData("POS Store", (posstore), devicereferencecode);
                    _configuration.Add(config);

                }
                else
                {
                    config = await SaveData("POS Store", "", devicereferencecode);
                    _configuration.Add(config);
                }
                config = await SaveData("Default Payment", devicemodelsms.sms_default_Payment, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Enable Auto Entry", devicemodelsms.sms_enableAutoEntry, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Open Drawer After Print", devicemodelsms.sms_openDrawerAfterPrint, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Enable Scale Encryption", devicemodelsms.sms_Enable_Scale_Encryption, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Code Length", devicemodelsms.sms_code_length.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Article Code Start", devicemodelsms.sms_article_code_start.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Article Code Length", devicemodelsms.sms_article_code_length.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Value Length", devicemodelsms.sms_value_length.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Value Start", devicemodelsms.sms_value_start.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Decimal Position", devicemodelsms.sms_decimal_position.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Enable Vocal Assistance", devicemodelsms.sms_enable_vocal_Assistance, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Language", devicemodelsms.sms_language, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("POS Use Only Available Article", devicemodelsms.sms_POSUseOnlyAvailableArticle.ToString(), devicereferencecode);
                _configuration.Add(config);


                await _sharedHelpers.saveOrUpdateRange(_configuration);
                msg = "Save Successfully";
                cansave = true;
                return Json(new { msg = msg, cansave = cansave });
            }
        }

        public async Task<IActionResult> SavingAccessSettingProperty(Devicemodel devicemodelaccess)
        {
            var devicereferencecode = devicemodelaccess.access_devicecode;
            string msg = null;
            bool cansave = false;
            if (devicereferencecode == "0" || devicereferencecode == null)
            {
                msg = "Please Select Device";
                cansave = false;
                return Json(new { msg = msg, cansave = cansave });
            }
            else
            {

                Config = await _sharedHelpers.GetConfigurationByRefandPref(devicereferencecode, CNETConstants.DEVICE);
                await _sharedHelpers.DeleteConfigurationByReferenceAndPreference(devicereferencecode, CNETConstants.DEVICE);
                List<ConfigurationDTO> _configuration = new List<ConfigurationDTO>();
                ConfigurationDTO config = new ConfigurationDTO();


                if (!string.IsNullOrWhiteSpace(devicemodelaccess.access_Control_Device))
                {
                    string accesscontrolcode = devicemodelaccess.access_Control_Device.Split('/')[0];
                    config =  await SaveData("Access Control Device", accesscontrolcode, devicereferencecode);
                    _configuration.Add(config);

                }
                else
                {
                    config = await SaveData("Access Control Device", "", devicereferencecode);
                    _configuration.Add(config);

                }
                config = await SaveData("OCR Data Path", devicemodelaccess.acc_oCR_Data_Path, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Detector Xml Path", devicemodelaccess.acc_detector_Xml_Path, devicereferencecode);
                _configuration.Add(config);

                if (!string.IsNullOrWhiteSpace(devicemodelaccess.acc_pOS_Camera))
                {
                    string cameraposcode = devicemodelaccess.acc_pOS_Camera.Split('/')[0];
                    config = await SaveData("POS Camera", cameraposcode, devicereferencecode);
                    _configuration.Add(config);

                }
                else
                {
                    config = await SaveData("POS Camera", "", devicereferencecode);
                    _configuration.Add(config);

                }

                config = await SaveData("Camera Option", devicemodelaccess.acc_camera_Option, devicereferencecode);
                _configuration.Add(config);
                string DoorSensor = "";
                if (devicemodelaccess.acc_door_Sensor == "None")
                {
                    DoorSensor = "None";
                }
                else
                {
                    DoorSensor = (devicemodelaccess.acc_door_Sensor.Split('_')[1]);
                }

                config = await SaveData("Door Sensor", DoorSensor, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Display Article", devicemodelaccess.acc_display_Article.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("ForceOpenRelay", devicemodelaccess.acc_force_Open_Relay.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Need Credentials For Removal", devicemodelaccess.aac_NeedCredentialsForRemoval, devicereferencecode);
                _configuration.Add(config);
                if (!string.IsNullOrWhiteSpace(devicemodelaccess.acc_pos_store))
                {
                    string storecode = devicemodelaccess.acc_pos_store.Split('/')[0];
                    config = await SaveData("POS Store", storecode, devicereferencecode);
                    _configuration.Add(config);
                }
                else
                {
                    config = await SaveData("POS Store", "", devicereferencecode);
                    _configuration.Add(config);
                }
                config = await SaveData("Check Voucher Integrity", devicemodelaccess.acc_check_Voucher_Integrity, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("POS Trigger Article Removal", devicemodelaccess.aac_POSTriggerArticleRemoval, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("ScheduleArticle", devicemodelaccess.acc_schedule_Article, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Select Automatic Shift Period", devicemodelaccess.access_selectAutomaticShiftPeriod, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Accuracy Tolerance", devicemodelaccess.acc_AccuracyTolerance.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Closing Frequency", devicemodelaccess.access_closing_Frequency, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("DownLoad EJ", devicemodelaccess.acc_down_Load_EJ, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Fiscal Reconciliation", devicemodelaccess.acc_fiscalReconciliation, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Summary Difference Tolerance", devicemodelaccess.acc_SummaryDifferenceTolerance.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Summary Voucher", devicemodelaccess.access_summary_Voucher, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("XML Reconciliation", devicemodelaccess.access_xML_Reconciliation, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Zeroing Voucher", devicemodelaccess.access_zeroing_Voucher, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Capture Fiscal Information", devicemodelaccess.access_capture_Fiscal_Information, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Print Discount & Additional Charge Per LineItem", devicemodelaccess.access_PrintDiscountAdditionalChargePerLineItem, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Ignore zero price article", devicemodelaccess.acc_ignorezeropricearticle, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("POS Value Rule", devicemodelaccess.access_POSValueRule.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Need Credentials For Customer Discount", devicemodelaccess.access_NeedCredentialsForCustomerDiscount.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Beneficiary User", devicemodelaccess.access_BeneficiaryUser.ToString(), devicereferencecode);
                _configuration.Add(config);


                await _sharedHelpers.saveOrUpdateRange(_configuration);
                msg = "Save Successfully";
                cansave = true;
                return Json(new { msg = msg, cansave = cansave });

            }
        }

        public async Task<IActionResult> SavingmobilSettingProperty(Devicemodel devicemodelmobli)
        {
            var devicereferencecode = devicemodelmobli.mobliedevicecode;
            string msg = null;
            bool cansave = false;
            if (devicereferencecode == "0" || devicereferencecode == null)
            {
                msg = "Please Select Device";
                cansave = false;
                return Json(new { msg = msg, cansave = cansave });
            }
            else
            {

                Config = await _sharedHelpers.GetConfigurationByRefandPref(devicereferencecode, CNETConstants.DEVICE);
                await _sharedHelpers.DeleteConfigurationByReferenceAndPreference(devicereferencecode, CNETConstants.DEVICE);
                List<ConfigurationDTO> _configuration = new List<ConfigurationDTO>();
                ConfigurationDTO config = new ConfigurationDTO();


                string password = null;
                if (!string.IsNullOrEmpty(devicemodelmobli.mob_MobileLauncherKey))
                {
                     password = Encrypt(devicemodelmobli.mob_MobileLauncherKey);
                }
                config = await SaveData("Ask Customer Maintenance for walk in", devicemodelmobli.mob_AskCustomerMaintenanceforwalkin, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Ask Customer Maintenance for walk in", devicemodelmobli.mob_AskCustomerMaintenanceforwalkin, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Allow multiple Instance Of Table", devicemodelmobli.mob_AllowmultipleInstanceOfTable, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Need Credentials For Removal", devicemodelmobli.mob_NeedCredentialsForRemoval, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Station Type", devicemodelmobli.mob_StationType, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Table Selection Type", devicemodelmobli.mob_TableSelectionType, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("POS Trigger Article Removal", devicemodelmobli.mob_POSTriggerArticleRemoval, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Input Cover Mandatory", devicemodelmobli.mob_InputCoverMandatory, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Table Mandatory", devicemodelmobli.mob_TableMandatory, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Waiter Mandatory", devicemodelmobli.mob_WaiterMandatory, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Code Search", devicemodelmobli.mob_CodeSearch, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Device Mode", devicemodelmobli.mob_DeviceMode, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("EnableRotation", devicemodelmobli.mob_EnableRotation, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("RotationSetting", devicemodelmobli.mob_RotationSetting, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Use Flash Bar Code Scan", devicemodelmobli.mob_UseFlashBarcodeScan, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Populate Article", devicemodelmobli.mob_PopulateArticle, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Select Automatic Shift Period", devicemodelmobli.mob_SelectAutomaticShiftPeriod, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("XML Reconciliation", devicemodelmobli.mob_XMLReconciliation, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Web API Type", devicemodelmobli.mob_WebAPIType, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Mobile Launcher Key", password, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("POS Value Rule", devicemodelmobli.mob_POSValueRule.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Need Credentials For Customer Discount", devicemodelmobli.mob_NeedCredentialsForCustomerDiscount.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Beneficiary User", devicemodelmobli.mob_BeneficiaryUser.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Edit only null fields", devicemodelmobli.mob_Editonlynullfields.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Customer Visibility", devicemodelmobli.mob_CustomerVisibility.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Voucher Extension as Commercial Message", devicemodelmobli.mob_VoucherExtensionasCommercialMessage.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Device Status API URL", devicemodelmobli.mob_DeviceStatusAPIURL, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Launcher password", devicemodelmobli.mob_Launcherpassword, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("QR Code Destination", devicemodelmobli.mob_QRCodeDestination.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Enable Data Sync", devicemodelmobli.mob_EnableDataSync.ToString(), devicereferencecode);
                _configuration.Add(config);
                if (devicemodelmobli.mob_POSStore != null)
                {
                    string storecode = devicemodelmobli.mob_POSStore.Split('/')[0];

                    config = await SaveData("POS Store", storecode, devicereferencecode);
                    _configuration.Add(config);
                }
                else

                {
                    config = await SaveData("POS Store", "", devicereferencecode);
                    _configuration.Add(config);
                }
                if (devicemodelmobli.mob_DefaultGSLType != null)
                {
                    string gslTypecode = devicemodelmobli.mob_DefaultGSLType;

                    config = await SaveData("Default GSL Type", gslTypecode, devicereferencecode);
                    _configuration.Add(config);
                }
                else
                {
                    config = await SaveData("Default GSL Type", "", devicereferencecode);
                    _configuration.Add(config);
                }
                config = await SaveData("Disable Void On Retrieve", devicemodelmobli.mob_DisableVoidOnRetrieve, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("LocalXmlSyncPath", devicemodelmobli.mob_LocalXmlSyncPath, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("LocalXmlSyncDumpyard", devicemodelmobli.mob_LocalXmlSyncDumpyard, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("LocalXmlSilentPrintPath", devicemodelmobli.mob_LocalXmlSilentPrintPath, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Remote Sync Path", devicemodelmobli.mob_RemoteSyncPath, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Return Fs No", devicemodelmobli.mob_ReturnFsNo, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("LocalXmlSyncSleepTimeInSecond", devicemodelmobli.mob_LocalXmlSyncSleepTimeInSecond, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Xml Protocol Type", devicemodelmobli.mob_XmlProtocolType, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("LocalXmlSyncType", devicemodelmobli.mob_LocalXmlSyncType, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("RealTime DB Access", devicemodelmobli.mob_RealTimeDBAccess, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("POS Use Only Available Article", devicemodelmobli.mob_POSUseOnlyAvailableArticle.ToString(), devicereferencecode);
                _configuration.Add(config);


                await _sharedHelpers.saveOrUpdateRange(_configuration);
                msg = "Save Successfully";
                cansave = true;
                return Json(new { msg = msg, cansave = cansave });
            }
           
        }
        public async Task<IActionResult> SavingcinemaSettingProperty(Devicemodel devicemodelcinema)
        {
            var devicereferencecode = devicemodelcinema.cinemadevicecode;
            string msg = null;
            bool cansave = false;
            if (devicereferencecode == "0"|| devicereferencecode == null)
            {
                msg = "Please Select Device";
                cansave = false;
                return Json(new { msg = msg, cansave = cansave });
            }
            else
            {
                Config = await _sharedHelpers.GetConfigurationByRefandPref(devicereferencecode, CNETConstants.DEVICE);
                await _sharedHelpers.DeleteConfigurationByReferenceAndPreference(devicereferencecode, CNETConstants.DEVICE);
                List<ConfigurationDTO> _configuration = new List<ConfigurationDTO>();
                ConfigurationDTO config = new ConfigurationDTO();

                config = await SaveData("Timeout In Seconds", devicemodelcinema.cinema_TimeoutInSeconds.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Remove Outdated Schedule", devicemodelcinema.cinema_RemoveOutdatedSchedule, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Enable Order", devicemodelcinema.cinema_EnableOrder, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Enable Item POS", devicemodelcinema.cinema_EnableItemPOS, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Order Station Mandatory", devicemodelcinema.cinema_OrderStationMandatory, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Enable Mobile Payment", devicemodelcinema.cinema_EnableMobilePayment, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Enable Cash Payment", devicemodelcinema.cinema_EnableCashPayment, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Log Path", devicemodelcinema.cinema_LogPath, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Check Voucher Integrity", devicemodelcinema.cinema_CheckVoucherIntegrity, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("POS Trigger Article Removal", devicemodelcinema.cinema_POSTriggerArticleRemoval, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Select Automatic Shift Period", devicemodelcinema.cinema_SelectAutomaticShiftPeriod, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Accuracy Tolerance", devicemodelcinema.cinema_AccuracyTolerance.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Closing Frequency", devicemodelcinema.cinema_ClosingFrequency, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("DownLoad EJ", devicemodelcinema.cinema_DownLoadEJ, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Fiscal Reconciliation", devicemodelcinema.cinema_FiscalReconciliation, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Summary Difference Tolerance", devicemodelcinema.cinema_SummaryDifferenceTolerance.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Summary Voucher", devicemodelcinema.cinema_SummaryVoucher, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("XML Reconciliation", devicemodelcinema.cinema_XMLReconciliation, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Zeroing Voucher", devicemodelcinema.cinema_ZeroingVoucher, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Capture Fiscal Information", devicemodelcinema.cinema_CaptureFiscalInformation, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Print Discount & Additional Charge Per LineItem", devicemodelcinema.cinema_PrintDiscountAdditionalChargePerLineItem, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Saving Order", devicemodelcinema.cinema_SavingOrder, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Need Credentials For Removal", devicemodelcinema.cinema_NeedCredentialsForRemoval, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Flexible Quantity", devicemodelcinema.cinema_FlexibleQuantity, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Ignore zero price article", devicemodelcinema.cinema_ignorezeropricearticle.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("API Base URL", devicemodelcinema.cinema_API_Base_UR, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("POS Use Only Available Article", devicemodelcinema.cinma_POSUseOnlyAvailableArticle.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("POS Value Rule", devicemodelcinema.cinma_POSValueRule.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Need Credentials For Customer Discount", devicemodelcinema.cinma_NeedCredentialsForCustomerDiscount.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("eneficiary User", devicemodelcinema.cinma_BeneficiaryUser.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Device Status API UR", devicemodelcinema.cinma_DeviceStatusAPIURL, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Cinema Station Type", devicemodelcinema.cinma_CinemaStationType.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Enable Online Payment", devicemodelcinema.cinma_EnableOnlinePayment.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("QR Code Destination", devicemodelcinema.cinma_QRCodeDestination.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Fiscal Reconciliation", devicemodelcinema.cinma_FiscalReconciliation.ToString(), devicereferencecode);
                _configuration.Add(config);

                if (!string.IsNullOrWhiteSpace(devicemodelcinema.cinema_POSStore))
                {
                    string storecode = devicemodelcinema.cinema_POSStore.Split('/')[0];

                    config = await SaveData("POS Store", storecode, devicereferencecode);
                    _configuration.Add(config);
                }
                else
                {

                    config = await SaveData("POS Store", "", devicereferencecode);
                    _configuration.Add(config);
                }

              


                await _sharedHelpers.saveOrUpdateRange(_configuration);
                msg = "Save Successfully";
                cansave = true;
                return Json(new { msg = msg, cansave = cansave });
            }
        }
        public async Task<IActionResult> SavingTransportSettingProperty(Devicemodel devicemodeltrans)
        {
            var devicereferencecode = devicemodeltrans.transadevicecode;
            string msg = null;
            bool cansave = false;
            if (devicereferencecode == "0" || devicereferencecode == null)
            {
                msg = "Please Select Device";
                cansave = false;
                return Json(new { msg = msg, cansave = cansave });
            }
            else
            {
                Config = await _sharedHelpers.GetConfigurationByRefandPref(devicereferencecode, CNETConstants.DEVICE);
                await _sharedHelpers.DeleteConfigurationByReferenceAndPreference(devicereferencecode, CNETConstants.DEVICE);
                List<ConfigurationDTO> _configuration = new List<ConfigurationDTO>();
                ConfigurationDTO config = new ConfigurationDTO();

                config = await SaveData("Timeout In Seconds", devicemodeltrans.trans_TimeoutInSeconds.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Remove Outdated Schedule", devicemodeltrans.trans_RemoveOutdatedSchedule, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Enable Order", devicemodeltrans.trans_EnableOrder, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Enable Item POS", devicemodeltrans.trans_EnableItemPOS, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Order Station Mandatory", devicemodeltrans.trans_OrderStationMandatory, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Enable Mobile Payment", devicemodeltrans.trans_EnableMobilePayment, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Enable Cash Payment", devicemodeltrans.trans_EnableCashPayment, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Log Path", devicemodeltrans.trans_LogPath, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Check Voucher Integrity", devicemodeltrans.trans_CheckVoucherIntegrity, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("POS Trigger Article Removal", devicemodeltrans.trans_POSTriggerArticleRemoval, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Select Automatic Shift Period", devicemodeltrans.trans_SelectAutomaticShiftPeriod, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Accuracy Tolerance", devicemodeltrans.trans_AccuracyTolerance.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Closing Frequency", devicemodeltrans.trans_ClosingFrequency, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("DownLoad EJ", devicemodeltrans.trans_DownLoadEJ, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Fiscal Reconciliation", devicemodeltrans.trans_FiscalReconciliation, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Summary Difference Tolerance", devicemodeltrans.trans_SummaryDifferenceTolerance.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Summary Voucher", devicemodeltrans.trans_SummaryVoucher, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("XML Reconciliation", devicemodeltrans.trans_XMLReconciliation, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Zeroing Voucher", devicemodeltrans.trans_ZeroingVoucher, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Capture Fiscal Information", devicemodeltrans.trans_CaptureFiscalInformation, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Print Discount & Additional Charge Per LineItem", devicemodeltrans.trans_PrintDiscountAdditionalChargePerLineItem, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Saving Order", devicemodeltrans.trans_SavingOrder, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Need Credentials For Removal", devicemodeltrans.trans_NeedCredentialsForRemoval, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Flexible Quantity", devicemodeltrans.trans_FlexibleQuantity, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Ignore zero price article", devicemodeltrans.trans_ignorezeropricearticle, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("API Base URL", devicemodeltrans.trans_API_Base_UR, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("POS Use Only Available Article", devicemodeltrans.trans_POSUseOnlyAvailableArticle.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("POS Value Rule", devicemodeltrans.trans_POSValueRule.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Need Credentials For Customer Discount", devicemodeltrans.trans_NeedCredentialsForCustomerDiscount.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Beneficiary User", devicemodeltrans.trans_BeneficiaryUser.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Device Status API URL", devicemodeltrans.trans_DeviceStatusAPIURL, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Cinema Station Type", devicemodeltrans.trans_CinemaStationType.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("QR Code Destination", devicemodeltrans.trans_QRCodeDestination.ToString(), devicereferencecode);
                _configuration.Add(config);


                if (!string.IsNullOrWhiteSpace(devicemodeltrans.trans_POSStore))
                {
                    string storecode = devicemodeltrans.trans_POSStore.Split('/')[0];

                    config = await SaveData("POS Store", storecode, devicereferencecode);
                    _configuration.Add(config);
                }
                else
                {

                    config = await SaveData("POS Store", "", devicereferencecode);
                    _configuration.Add(config);
                }

                await _sharedHelpers.saveOrUpdateRange(_configuration);
                msg = "Save Successfully";
                cansave = true;
                return Json(new { msg = msg, cansave = cansave });
            }
        }
        public async Task<IActionResult> SavingreservationposSettingProperty(Devicemodel resv_devicemodel)
        {
            string msg = null;
            bool cansave = false;
            var devicereferencecode = resv_devicemodel.resv_devicecode;
            if (devicereferencecode == "0" || devicereferencecode == null)
            {
                msg = "Please Select Device";
                cansave = false;
                return Json(new { msg = msg, cansave = cansave });
            }
            else
            {
                Config = await _sharedHelpers.GetConfigurationByRefandPref(devicereferencecode, CNETConstants.DEVICE);
                await _sharedHelpers.DeleteConfigurationByReferenceAndPreference(devicereferencecode, CNETConstants.DEVICE);
                List<ConfigurationDTO> _configuration = new List<ConfigurationDTO>();
                ConfigurationDTO config = new ConfigurationDTO();

                config = await SaveData("Accuracy Tolerance", resv_devicemodel.resv_AccuracyTolerance.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Summary Difference Tolerance", resv_devicemodel.resv_SummaryDifferenceTolerance.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("DownLoad EJ", resv_devicemodel.resv_DownLoadEJ, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Check Voucher Integrity", resv_devicemodel.resv_CheckVoucherIntegrity, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Closing Frequency", resv_devicemodel.resv_ClosingFrequency.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Fiscal Reconciliation", resv_devicemodel.resv_FiscalReconciliation, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Summary Voucher", resv_devicemodel.resv_SummaryVoucher, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Zeroing Voucher", resv_devicemodel.resv_ZeroingVoucher, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Need Credentials For Removal", resv_devicemodel.resv_NeedCredentialsForRemoval, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Default Payment", resv_devicemodel.resv_DefaultPayment.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Capture Fiscal Information", resv_devicemodel.resv_CaptureFiscalInformation, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Select Automatic Shift Period", resv_devicemodel.resv_SelectAutomaticShiftPeriod, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("XML Reconciliation", resv_devicemodel.resv_XMLReconciliation, devicereferencecode);
                _configuration.Add(config);
                if (!string.IsNullOrWhiteSpace(resv_devicemodel.resv_CashCustomer))
                {
                    string empcode = resv_devicemodel.resv_CashCustomer.Split('/')[0];
                    if (!string.IsNullOrWhiteSpace(empcode))
                    {
                        //var value = await _sharedHelpers.GetVw_AllPersonViewByCode(empcode);
                        if (empcode != null)
                        {
                            config = await SaveData("Cash Customer", empcode, devicereferencecode);
                            _configuration.Add(config);
                        }
                        else
                        {
                            config = await SaveData("Cash Customer", "Not Applicable", devicereferencecode);
                            _configuration.Add(config);
                        }
                    }
                }
                if (!string.IsNullOrWhiteSpace(resv_devicemodel.resv_POSStore))
                {
                    string storecode = resv_devicemodel.resv_POSStore.Split('/')[0];
                    if (!string.IsNullOrWhiteSpace(storecode))
                    {
                        var value = await _sharedHelpers.GetConsigneeunitById(Convert.ToInt32(storecode));
                        if (value != null)
                        {
                            config = await SaveData("POS Store", value?.FirstOrDefault()?.Id.ToString(), devicereferencecode);
                            _configuration.Add(config);
                        }
                        else
                        {
                            config = await SaveData("POS Store", "", devicereferencecode);
                            _configuration.Add(config);
                        }
                    }
                    else
                    {
                        config = await SaveData("POS Store", "", devicereferencecode);
                        _configuration.Add(config);
                    }
                }

                await _sharedHelpers.saveOrUpdateRange(_configuration);
                msg = "Save Successfully";
                cansave = true;
                return Json(new { msg = msg, cansave = cansave });
            }
        }
        public async Task<IActionResult> SavingNvsSettingProperty(Devicemodel devicemodelNvs)
        {
            var devicereferencecode = devicemodelNvs.nvsdevicecode;
            string msg = null;
            bool cansave = false;
            if (devicereferencecode == "0" || devicereferencecode == null)
            {
                msg = "Please Select Device";
                cansave = false;
                return Json(new { msg = msg, cansave = cansave });
            }
            else
            {
                Config = await _sharedHelpers.GetConfigurationByRefandPref(devicereferencecode, CNETConstants.DEVICE);
                await _sharedHelpers.DeleteConfigurationByReferenceAndPreference(devicereferencecode, CNETConstants.DEVICE);
                List<ConfigurationDTO> _configuration = new List<ConfigurationDTO>();
                ConfigurationDTO config = new ConfigurationDTO();

                config = await SaveData("Check Voucher Integrity", devicemodelNvs.nvs_CheckVoucherIntegrity, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Select Automatic Shift Period", devicemodelNvs.nvs_SelectAutomaticShiftPeriod, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Accuracy Tolerance", devicemodelNvs.nvs_AccuracyTolerance.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Closing Frequency", devicemodelNvs.nvs_ClosingFrequency, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("DownLoad EJ", devicemodelNvs.nvs_DownLoadEJ, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Fiscal Reconciliation", devicemodelNvs.nvs_FiscalReconciliation, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Summary Difference Tolerance", devicemodelNvs.nvs_SummaryDifferenceTolerance.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Summary Voucher", devicemodelNvs.nvs_SummaryVoucher, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("XML Reconciliation", devicemodelNvs.nvs_XMLReconciliation, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Zeroing Voucher", devicemodelNvs.nvs_ZeroingVoucher, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Capture Fiscal Information", devicemodelNvs.nvs_CaptureFiscalInformation, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Print Discount & Additional Charge Per LineItem", devicemodelNvs.nvs_PrintDiscountAdditionalChargePerLineItem, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Need Credentials For Removal", devicemodelNvs.nvs_NeedCredentialsForRemoval, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Default Payment", devicemodelNvs.nvs_DefaultPayment, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Ignore zero price article", devicemodelNvs.nvs_Ignorezeropricearticle, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("POS Use Only Available Article", devicemodelNvs.nvs_POSUseOnlyAvailableArticle.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Use Only Current Branch", devicemodelNvs.nvs_UseOnlyCurrentBranch.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Lock Store Selection", devicemodelNvs.nvs_LockStoreSelection.ToString(), devicereferencecode);
                config = await SaveData("POS Value Rule", devicemodelNvs.nvs_POSValueRule.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Need Credentials For Customer Discount", devicemodelNvs.nvs_NeedCredentialsForCustomerDiscount.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Beneficiary User", devicemodelNvs.nvs_BeneficiaryUser.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("POS Print Batch", devicemodelNvs.nvs_PrintBatch.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("POS Print Expiry Date", devicemodelNvs.nvs_PrintExpiryDate.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("POS Serial Input Type", devicemodelNvs.nvs_SerialInputType.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("POS Use Automatic Life span", devicemodelNvs.nvs_UseAutomaticLifespan.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("POS Enable Serial Number Or LifeSpan", devicemodelNvs.nvs_EnableSerialNumberLifeSpan.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("POS Serial Movement Suggestion", devicemodelNvs.nvs_SerialMovementSuggestion.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Device Status API URL", devicemodelNvs.nvs_DeviceStatusAPIURL, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Enable Item Selection", devicemodelNvs.nvs_EnableItemSelection.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Enable Online Payment", devicemodelNvs.nvs_EnableOnlinePayment.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("QR Code Destination", devicemodelNvs.nvs_QRCodeDestination.ToString(), devicereferencecode);
                _configuration.Add(config);

                if (!string.IsNullOrWhiteSpace(devicemodelNvs.nvs_CashCustomer))
                {
                    string cashcustomer = devicemodelNvs.nvs_CashCustomer.Split('/')[0];

                    config = await SaveData("Cash Customer", cashcustomer, devicereferencecode);
                    _configuration.Add(config);
                }
                else
                {

                    config = await SaveData("Cash Customer", "Not Applicable", devicereferencecode);
                    _configuration.Add(config);
                }

                if (!string.IsNullOrWhiteSpace(devicemodelNvs.nvs_POSStore))
                {
                    string posstorecode = devicemodelNvs.nvs_POSStore.Split('/')[0];

                    config = await SaveData("POS Store", posstorecode, devicereferencecode);
                    _configuration.Add(config);
                }
                else
                {

                    config = await SaveData("POS Store", "", devicereferencecode);
                    _configuration.Add(config);
                }

                await _sharedHelpers.saveOrUpdateRange(_configuration);
                msg = "Save Successfully";
                cansave = true;
                return Json(new { msg = msg, cansave = cansave });
            }
        }
        public async Task<IActionResult> SavingrDoorSettingProperty(Devicemodel devicemodeldoor)
        {
            var devicereferencecode = devicemodeldoor.doordevicecode;

            string msg = null;
            bool cansave = false;
            if (devicereferencecode == "0" || devicereferencecode == null)
            {
                msg = "Please Select Device";
                cansave = false;
                return Json(new { msg = msg, cansave = cansave });
            }
            else
            {
                Config = await _sharedHelpers.GetConfigurationByRefandPref(devicereferencecode, CNETConstants.DEVICE);
                await _sharedHelpers.DeleteConfigurationByReferenceAndPreference(devicereferencecode, CNETConstants.DEVICE);
                List<ConfigurationDTO> _configuration = new List<ConfigurationDTO>();
                ConfigurationDTO config = new ConfigurationDTO();

                config = await SaveData("Deluns Encoder Type", devicemodeldoor.door_DelunsEncoderType, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Deluns Encoder Type", devicemodeldoor.door_DelunsEncoderType, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Unlock Deadbolt", devicemodeldoor.door_UnlockDeadbolt, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Unlock public Doors", devicemodeldoor.door_UnlockpublicDoors, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Adel System", devicemodeldoor.door_AdelSystem, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Host", devicemodeldoor.door_Host, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("User", devicemodeldoor.door_User, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Adel Encoder", devicemodeldoor.door_AdelEncoderType, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("TM ENcode", devicemodeldoor.door_TMENcoder, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("BETECH Encoder Type", devicemodeldoor.door_BETECHEncoderType, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("System Password", devicemodeldoor.door_SystemPassword, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("MOllY Encoder Type", devicemodeldoor.door_MOllYEncoderType, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Encoder Address", devicemodeldoor.door_EncoderAddress.ToString(), devicereferencecode);
                _configuration.Add(config);



                await _sharedHelpers.saveOrUpdateRange(_configuration);
                msg = "Save Successfully";
                cansave = true;
                return Json(new { msg = msg, cansave = cansave });
            }
        }
        public async Task<IActionResult> SavingaccesscontroSettingProperty(Devicemodel devicemodelcontro)
        {
            string msg = null;
            bool cansave = false;
            var devicereferencecode = devicemodelcontro.controdevicecode;
            if (devicereferencecode == "0" || devicereferencecode == null)
            {
                msg = "Please Select Device";
                cansave = false;
                return Json(new { msg = msg, cansave = cansave });
            }
            else
            {
                Config = await _sharedHelpers.GetConfigurationByRefandPref(devicereferencecode, CNETConstants.DEVICE);
                await _sharedHelpers.DeleteConfigurationByReferenceAndPreference(devicereferencecode, CNETConstants.DEVICE);
                List<ConfigurationDTO> _configuration = new List<ConfigurationDTO>();
                ConfigurationDTO config = new ConfigurationDTO();

                config = await SaveData("Authentication Type", devicemodelcontro.contro_AuthenticationType, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Subscription Transaction", devicemodelcontro.contro_SubscriptionTransaction, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Access control Type", devicemodelcontro.contro_AccesscontrolType, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("CameraOption", devicemodelcontro.contro_CameraOption, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Gate Type", devicemodelcontro.contro_GateType, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Exit Treshhold", devicemodelcontro.contro_ExitTreshhold.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Notify Before Date", devicemodelcontro.contro_NotifyBeforeDate.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("CardDespensorVersion", devicemodelcontro.contro_card_despensorversion, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Package Category", devicemodelcontro.contro_card_despensorversion, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Allow Mobile App Access", devicemodelcontro.contro_AllowMobileAppAccess.ToString(), devicereferencecode);
                _configuration.Add(config);

                await _sharedHelpers.saveOrUpdateRange(_configuration);
                msg = "Save Successfully";
                cansave = true;
                return Json(new { msg = msg, cansave = cansave });
            }

        } 
        public async Task<IActionResult> SavingCameraSettiing(Devicemodel devicemodelcontro)
        {
            string msg = null;
            bool cansave = false;
            var devicereferencecode = devicemodelcontro.dCamera;
            if (devicereferencecode == "0" || devicereferencecode == null )
            {
                msg = "Please Select Device";
                cansave = false;
                return Json(new { msg = msg, cansave = cansave });
            }
            else
            {
                Config = await _sharedHelpers.GetConfigurationByRefandPref(devicereferencecode, CNETConstants.DEVICE);
                await _sharedHelpers.DeleteConfigurationByReferenceAndPreference(devicereferencecode, CNETConstants.DEVICE);
                List<ConfigurationDTO> _configuration = new List<ConfigurationDTO>();
                ConfigurationDTO config = new ConfigurationDTO();


                config = await SaveData("User Name", devicemodelcontro.Camera_Username, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Type", devicemodelcontro.Camera_type.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Password", devicemodelcontro.Camera_password, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("SourceURl", devicemodelcontro.Camera_Source_URl, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Image Archieves", devicemodelcontro.Camera_Image_Archieves, devicereferencecode);
                _configuration.Add(config);

                await _sharedHelpers.saveOrUpdateRange(_configuration);
                msg = "Save Successfully";
                cansave = true;
                return Json(new { msg = msg, cansave = cansave });
            }
           
        }

        public async Task<IActionResult> SavingtelbirrSettingProperty(Devicemodel devicemodelcontro)
        {
            var devicereferencecode = devicemodelcontro.telbrdevicecode;
            string msg = null;
            bool cansave = false;
            if (devicereferencecode == "0" || devicereferencecode == null)
            {
                msg = "Please Select Device";
                cansave = false;
                return Json(new { msg = msg, cansave = cansave });
            }
            else
            {

                Config = await _sharedHelpers.GetConfigurationByRefandPref(devicereferencecode, CNETConstants.DEVICE);
                await _sharedHelpers.DeleteConfigurationByReferenceAndPreference(devicereferencecode, CNETConstants.DEVICE);
                List<ConfigurationDTO> _configuration = new List<ConfigurationDTO>();
                ConfigurationDTO config = new ConfigurationDTO();


                if (devicemodelcontro.telPrefcode == "PREFE000000109")
                {
                    config = await SaveData("ApplicationCode", devicemodelcontro.tel_applicationCode, devicereferencecode);
                    _configuration.Add(config);
                    config = await SaveData("AuthorizationEndpointUrl", devicemodelcontro.tel_authorizationEndpointUrl, devicereferencecode);
                    config = await SaveData("Password", devicemodelcontro.tel_password, devicereferencecode);
                    _configuration.Add(config);
                    config = await SaveData("SecurityCredential", devicemodelcontro.tel_securityCredential, devicereferencecode);
                    config = await SaveData("OperatorId", devicemodelcontro.tel_operatorId, devicereferencecode);
                    _configuration.Add(config);
                    config = await SaveData("TransactionEndpointUrl", devicemodelcontro.tel_transactionEndpointUrl, devicereferencecode);
                    _configuration.Add(config);
                    config = await SaveData("ThirdPartyId", devicemodelcontro.tel_thirdPartyId, devicereferencecode);
                    _configuration.Add(config);
                    config = await SaveData("ReceiverShortCode", devicemodelcontro.tel_receiverShortCode, devicereferencecode);
                    _configuration.Add(config);
                }
                else if (devicemodelcontro.telPrefcode == "PREFE000000125")
                {
                    config = await SaveData("Password", devicemodelcontro.tel_password, devicereferencecode);
                    _configuration.Add(config);
                    config = await SaveData("SecurityCredential", devicemodelcontro.tel_securityCredential, devicereferencecode);
                    config = await SaveData("OperatorId", devicemodelcontro.tel_operatorId, devicereferencecode);
                    _configuration.Add(config);
                    config = await SaveData("TransactionEndpointUrl", devicemodelcontro.tel_transactionEndpointUrl, devicereferencecode);
                    config = await SaveData("ThirdPartyId", devicemodelcontro.tel_thirdPartyId, devicereferencecode);
                    _configuration.Add(config);
                    config = await SaveData("ReceiverShortCode", devicemodelcontro.tel_receiverShortCode, devicereferencecode);
                    _configuration.Add(config);
                    config = await SaveData("ResultCallbackUrl", devicemodelcontro.tel_resultCallbackUrl, devicereferencecode);
                    _configuration.Add(config);
                }

             

                await _sharedHelpers.saveOrUpdateRange(_configuration);
                msg = "Save Successfully";
                cansave = true;
                return Json(new { msg = msg, cansave = cansave });
            }

        }


        public async Task<IActionResult> SavingebirrSettingProperty(Devicemodel devicemodelcontro)
        {
            var devicereferencecode = devicemodelcontro.e_devicecode;
            string msg = null;
            bool cansave = false;
            if (devicereferencecode == "0" || devicereferencecode == null)
            {
                msg = "Please Select Device";
                cansave = false;
                return Json(new { msg = msg, cansave = cansave });
            }
            else
            {

                Config = await _sharedHelpers.GetConfigurationByRefandPref(devicereferencecode, CNETConstants.DEVICE);
                await _sharedHelpers.DeleteConfigurationByReferenceAndPreference(devicereferencecode, CNETConstants.DEVICE);
                List<ConfigurationDTO> _configuration = new List<ConfigurationDTO>();
                ConfigurationDTO config = new ConfigurationDTO();

                config = await SaveData("ApiKey", devicemodelcontro.e_apiKey, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("ApiUserId", devicemodelcontro.e_apiUserId, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("MerchantId", devicemodelcontro.e_merchantId, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("TransactionUrl", devicemodelcontro.e_transactionUrl, devicereferencecode);
                _configuration.Add(config);


                await _sharedHelpers.saveOrUpdateRange(_configuration);
                msg = "Save Successfully";
                cansave = true;
                return Json(new { msg = msg, cansave = cansave });
            }

        }


        public async Task<IActionResult> SavingsahaySettingProperty(Devicemodel devicemodelcontro)
        {
            var devicereferencecode = devicemodelcontro.sa_devicecode;

            string msg = null;
            bool cansave = false;
            if (devicereferencecode == "0" || devicereferencecode == null)
            {
                msg = "Please Select Device";
                cansave = false;
                return Json(new { msg = msg, cansave = cansave });
            }
            else
            {

                Config = await _sharedHelpers.GetConfigurationByRefandPref(devicereferencecode, CNETConstants.DEVICE);
                await _sharedHelpers.DeleteConfigurationByReferenceAndPreference(devicereferencecode, CNETConstants.DEVICE);
                List<ConfigurationDTO> _configuration = new List<ConfigurationDTO>();
                ConfigurationDTO config = new ConfigurationDTO();

                config = await SaveData("ConsumerKey", devicemodelcontro.sa_consumerKey, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("ConsumerSecret", devicemodelcontro.sa_consumerSecret, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("BaseUrl", devicemodelcontro.sa_baseUrl, devicereferencecode);
                _configuration.Add(config);

                await _sharedHelpers.saveOrUpdateRange(_configuration);
                msg = "Save Successfully";
                cansave = true;
                return Json(new { msg = msg, cansave = cansave });
            }

        }

        public async Task<IActionResult> SavingamoleSettingProperty(Devicemodel devicemodelamole)
        {
            string msg= null;
            bool cansave = false;
            var devicereferencecode = devicemodelamole.amodeldevicecode;
            if (devicereferencecode == "0" || devicereferencecode == null)
            {
                msg = "Please Select Device";
                cansave = false;
                return Json(new { msg = msg, cansave = cansave });
            }
            else
            {
                Config = await _sharedHelpers.GetConfigurationByRefandPref(devicereferencecode, CNETConstants.DEVICE);
                await _sharedHelpers.DeleteConfigurationByReferenceAndPreference(devicereferencecode, CNETConstants.DEVICE);
                List<ConfigurationDTO> _configuration = new List<ConfigurationDTO>();
                ConfigurationDTO config = new ConfigurationDTO();

                config = await SaveData("MerchantId", devicemodelamole.amole_merchantId, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("ApiKey", devicemodelamole.amole_apiKey = devicemodelamole.amole_apiKey == null ? "" : devicemodelamole.amole_apiKey, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("ApiUsername", devicemodelamole.amole_apiUsername = devicemodelamole.amole_apiUsername == null ? "" : devicemodelamole.amole_apiUsername, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("ApiPassword", devicemodelamole.amole_apiPassword = devicemodelamole.amole_apiPassword == null ? "" : devicemodelamole.amole_apiPassword, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("PaymentApiEndpointUrl", devicemodelamole?.amole_paymentApiEndpointUrl, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("ServiceApiEndpointUrl", devicemodelamole.amole_serviceApiEndpointUrl = devicemodelamole.amole_serviceApiEndpointUrl == null ? "" : devicemodelamole.amole_serviceApiEndpointUrl, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("VendorAccountId", devicemodelamole.amole_vendorAccountId = devicemodelamole.amole_vendorAccountId == null ? "" : devicemodelamole.amole_vendorAccountId, devicereferencecode);
                _configuration.Add(config);

                await _sharedHelpers.saveOrUpdateRange(_configuration);
                msg = "Save Successfully";
                cansave = true;
                return Json(new { msg = msg, cansave = cansave });

            }

        }

        public async Task<IActionResult> SavingfiscalSettingProperty(Devicemodel devicemodelaFiscal)
        {
            string msg = null;
            bool cansave = false;
            var devicereferencecode = devicemodelaFiscal.fiscaldevicecode;
            if (devicereferencecode == "0" || devicereferencecode == null)
            {

                msg = "Please Select Device";
                cansave = false;
                return Json(new { msg = msg, cansave = cansave });
            }
            else
            {
                Config = await _sharedHelpers.GetConfigurationByRefandPref(devicereferencecode, CNETConstants.DEVICE);
                await _sharedHelpers.DeleteConfigurationByReferenceAndPreference(devicereferencecode, CNETConstants.DEVICE);
                List<ConfigurationDTO> _configuration = new List<ConfigurationDTO>();
                ConfigurationDTO config = new ConfigurationDTO();


                string UnitPrice = devicemodelaFiscal.fiscal_UnitPrice.ToString().Split('_')[1] + ".99";
                string UnitQuantity = devicemodelaFiscal.fiscal_UnitQuantity.ToString().Split('_')[1] + ".99";
                string GrandTotal = devicemodelaFiscal.fiscal_GrandTotal.ToString().Split('_')[1] + ".99";
                int bacrcode = 0;
                if (devicemodelaFiscal.fiscal_Barcodesize.ToString() == "Three")
                {
                    bacrcode = 3;
                }
                else if (devicemodelaFiscal.fiscal_Barcodesize.ToString() == "Four")
                {
                    bacrcode = 4;
                }
                else if (devicemodelaFiscal.fiscal_Barcodesize.ToString() == "Five")
                {
                    bacrcode = 5;
                }
                else if (devicemodelaFiscal.fiscal_Barcodesize.ToString() == "Six")
                {
                    bacrcode = 6;
                }
                else if (devicemodelaFiscal.fiscal_Barcodesize.ToString() == "Seven")
                {
                    bacrcode = 7;
                }
                else if (devicemodelaFiscal.fiscal_Barcodesize.ToString() == "Eight")
                {
                    bacrcode = 8;

                }
                else if (devicemodelaFiscal.fiscal_Barcodesize.ToString() == "Nine")
                {
                    bacrcode = 9;
                }
                else if (devicemodelaFiscal.fiscal_Barcodesize.ToString() == "Ten")
                {
                    bacrcode = 10;
                }
                config = await SaveData("Log File Format", devicemodelaFiscal.fiscal_LogFileFormat, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Log File Format", devicemodelaFiscal.fiscal_LogFileFormat, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Log File Path", devicemodelaFiscal.fiscal_LogFilePath, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Maximum Z report", devicemodelaFiscal.fiscal_MaximumZreport.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("EJPath", devicemodelaFiscal.fiscal_EJPath, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Use Automatic Attachment", devicemodelaFiscal.fiscal_UseAutomaticAttachment, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Fp Capacity", devicemodelaFiscal.fiscal_FpCapacity.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Notify Before", devicemodelaFiscal.fiscal_NotifyBefore.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Unit Price", UnitPrice, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Unit Quantity", UnitQuantity, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Grand Total", GrandTotal, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Barcode Height", devicemodelaFiscal.fiscal_BarcodeHeight.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Print Barcode", devicemodelaFiscal.fiscal_PrintBarcode, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("OperatorSignature", devicemodelaFiscal.fiscal_OperatorSignature, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Show Fiscal Receipt Header", devicemodelaFiscal.fiscal_ShowFiscalReceiptHeader, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Print TIN As Commercial", devicemodelaFiscal.fiscal_PrintTINAsCommercial, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Print Duplicate", devicemodelaFiscal.fiscal_PrintDuplicate, devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Article Description Length", devicemodelaFiscal.fiscal_ArticleDescriptionLength.ToString(), devicereferencecode);
                _configuration.Add(config);
                config = await SaveData("Barcode size", bacrcode.ToString(), devicereferencecode);
                _configuration.Add(config);

                await _sharedHelpers.saveOrUpdateRange(_configuration);
                msg = "Save Successfully";
                cansave = true;
                return Json(new { msg = msg, cansave = cansave });
            }
        }


        public async Task<IActionResult> SavingOrderprinterSettingProperty(Devicemodel devicemodelOrder)
        {
            var devicereferencecode = devicemodelOrder.orderprinterdevicecode;

            string msg = null;
            bool cansave = false;
            if (devicereferencecode == "0" || devicereferencecode == null)
            {
                msg = "Please Select Device";
                cansave = false;
                return Json(new { msg = msg, cansave = cansave });
            }
            else
            {
                Config = await _sharedHelpers.GetConfigurationByRefandPref(devicereferencecode, CNETConstants.DEVICE);
                await _sharedHelpers.DeleteConfigurationByReferenceAndPreference(devicereferencecode, CNETConstants.DEVICE);
                List<ConfigurationDTO> _configuration = new List<ConfigurationDTO>();
                ConfigurationDTO config = new ConfigurationDTO();


                if (string.IsNullOrWhiteSpace(devicemodelOrder.order_DefaultStore))
                {
                    config = await SaveData("Default Store", "", devicereferencecode);
                    _configuration.Add(config);
                }
                else
                {
                    string defaultstorecode = devicemodelOrder.order_DefaultStore.Split('/')[0];

                    config = await SaveData("Default Store", defaultstorecode, devicereferencecode);
                    _configuration.Add(config);

                }

            

                await _sharedHelpers.saveOrUpdateRange(_configuration);
                msg = "Save Successfully";
                cansave = true;
                return Json(new { msg = msg, cansave = cansave });
            }

        }

        public async Task<IActionResult> SavingreferenceposSettingProperty(Devicemodel referencepos)
        {
            string msg = null;
            bool cansave = false;
            if (referencepos.refdevicecode == "0" || referencepos.refdevicecode == null)
            {
                msg = "Please Select Device";
                cansave = false;
                return Json(new { msg = msg, cansave = cansave });
            }
            else
            {
                var deviceCode = referencepos.refdevicecode;
                Config = await _sharedHelpers.GetConfigurationByRefandPref(deviceCode, CNETConstants.DEVICE);
                await _sharedHelpers.DeleteConfigurationByReferenceAndPreference(deviceCode , CNETConstants.DEVICE);
                List<ConfigurationDTO> _configuration = new List<ConfigurationDTO>();
                ConfigurationDTO config = new ConfigurationDTO();


                config = await SaveData("Accuracy Tolerance", referencepos.ref_AccuracyTolerance.ToString(), deviceCode);
                _configuration.Add(config);
                config = await SaveData("Summary Difference Tolerance", referencepos.ref_SummaryDifferenceTolerance.ToString(), deviceCode);
                _configuration.Add(config);
                config = await SaveData("DownLoad EJ", referencepos.ref_DownLoadEJ, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Check Voucher Integrity", referencepos.ref_CheckVoucherIntegrity, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Closing Frequency", referencepos.ref_ClosingFrequency.ToString(), deviceCode);
                _configuration.Add(config);
                config = await SaveData("Fiscal Reconciliation", referencepos.ref_FiscalReconciliation, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Summary Voucher", referencepos.ref_SummaryVoucher, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Zeroing Voucher", referencepos.ref_ZeroingVoucher.ToString(), deviceCode);
                _configuration.Add(config);
                config = await SaveData("Need Credentials For Removal", referencepos.ref_NeedCredentialsForRemoval.ToString(), deviceCode);
                _configuration.Add(config);
                config = await SaveData("Default Payment", referencepos.ref_DefaultPayment.ToString(), deviceCode);
                _configuration.Add(config);
                config = await SaveData("Capture Fiscal Information", referencepos.ref_CaptureFiscalInformation, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Select Automatic Shift Period", referencepos.ref_SelectAutomaticShiftPeriod, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Print Discount & Additional Charge Per LineItem", referencepos.ref_PrintDiscountAdditionalChargePerLineItem, deviceCode);
                _configuration.Add(config);
                config = await SaveData("XML Reconciliation", referencepos.ref_XMLReconciliation, deviceCode);
                _configuration.Add(config);
                config = await SaveData("QR Code Destination", referencepos.ref_QRCodeDestination.ToString(), deviceCode);
                _configuration.Add(config);
                config = await SaveData("Enable Online Payment", referencepos.ref_EnableOnlinePayment, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Cash Customer", referencepos.ref_CashCustomer, deviceCode);
                _configuration.Add(config);
                config = await SaveData("POS Store", referencepos.ref_POSStore, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Ignore zero price article", referencepos.ref_Ignore_zero_price_article.ToString(), deviceCode);
                _configuration.Add(config);
                config = await SaveData("Clear Hold Before Closing", referencepos.ref_ClearHoldBeforeClosing, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Enable Hold Functionality", referencepos.ref_EnableHoldFunctionality, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Day Closing", referencepos.ref_DayClosing, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Enable CBE", referencepos.ref_EnableCBE, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Enable Document Reference", referencepos.ref_EnableDocumentReference, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Enable GSL Tax", referencepos.ref_EnableGSLTax, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Enable Value Factory", referencepos.ref_EnableValueFactory, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Item Selection", referencepos.ref_ItemSelection.ToString(), deviceCode);
                _configuration.Add(config);
                config = await SaveData("POS Trigger Article Removal", referencepos.ref_POSTriggerArticleRemoval.ToString(), deviceCode);
                _configuration.Add(config);
                config = await SaveData("Enable Auto Entry", referencepos.ref_EnableAutoEntry, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Open Drawer After Print", referencepos.ref_OpenDrawerAfterPrint, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Article Code Length", referencepos.ref_ArticleCodeLength, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Article Code Start", referencepos.ref_ArticleCodeStart, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Code Length", referencepos.ref_CodeLength, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Decimal Position", referencepos.ref_DecimalPosition, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Enable Scale Encryption", referencepos.ref_EnableScaleEncryption, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Value Start", referencepos.ref_ValueStart, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Value Length", referencepos.ref_ValueLength, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Enable Vocal Assistanc", referencepos.ref_EnableVocalAssistance, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Language", referencepos.ref_Language.ToString(), deviceCode);
                _configuration.Add(config);
                config = await SaveData("Enable Xml Silent Print", referencepos.ref_EnableXmlSilentPrint, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Silent Print Xml Path", referencepos.ref_SilentPrintXmlPath, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Error Xml Path", referencepos.ref_ErrorXmlPath, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Identical User Authentication", referencepos.ref_Identicaluserauthentication, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Return FS Number", referencepos.ref_ReturnFSNumber, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Use Total Valuefactor", referencepos.ref_UseTotalValuefactor, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Xml Damp Yard", referencepos.ref_XmlDampYard, deviceCode);
                _configuration.Add(config);
                config = await SaveData("POS Use Only Available Article", referencepos.ref_POSUseOnlyAvailableArticle.ToString(), deviceCode);
                _configuration.Add(config);
                config = await SaveData("POS Value Rule", referencepos.ref_POSValueRule.ToString(), deviceCode);
                _configuration.Add(config);
                config = await SaveData("Need Credentials For Customer Discount", referencepos.ref_NeedCredentialsForCustomerDiscount.ToString(), deviceCode);
                _configuration.Add(config);
                config = await SaveData("Beneficiary User", referencepos.ref_BeneficiaryUser.ToString(), deviceCode);
                _configuration.Add(config);

                await _sharedHelpers.saveOrUpdateRange(_configuration);
                msg = "Save Successfully";
                cansave = true;
                return Json(new { msg = msg, cansave = cansave });
            }
        }
        public async Task<IActionResult> SavingrecurringeposSettingProperty(Devicemodel referencepos)  
        {
            string msg = null;
            bool cansave = false;
            List<ConfigurationDTO> devicecodelist = new List<ConfigurationDTO>();
            if (referencepos.recu_devicecode == "0" || referencepos.recu_devicecode == null)
            {
                msg = "Please Select Device";
                cansave = false;
                return Json(new { msg = msg, cansave = cansave });
            }
            else
            {
                var deviceCode = referencepos.recu_devicecode;
                Config = await _sharedHelpers.GetConfigurationByRefandPref(deviceCode, CNETConstants.DEVICE);
                await _sharedHelpers.DeleteConfigurationByReferenceAndPreference(deviceCode, CNETConstants.DEVICE);
                List<ConfigurationDTO> _configuration = new List<ConfigurationDTO>();
                ConfigurationDTO config = new ConfigurationDTO();


                config = await SaveData("Accuracy Tolerance", referencepos.recu_AccuracyTolerance.ToString(), deviceCode);
                _configuration.Add(config);
                config = await SaveData("Summary Difference Tolerance", referencepos.recu_SummaryDifferenceTolerance.ToString(), deviceCode);
                _configuration.Add(config);
                config = await SaveData("DownLoad EJ", referencepos.recu_DownLoadEJ, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Check Voucher Integrity", referencepos.recu_CheckVoucherIntegrity, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Closing Frequency", referencepos.recu_ClosingFrequency.ToString(), deviceCode);
                _configuration.Add(config);
                config = await SaveData("Fiscal Reconciliation", referencepos.recu_FiscalReconciliation, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Summary Voucher", referencepos.recu_SummaryVoucher, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Zeroing Voucher", referencepos.recu_ZeroingVoucher, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Need Credentials For Removal", referencepos.recu_NeedCredentialsForRemoval.ToString(), deviceCode);
                _configuration.Add(config);
                config = await SaveData("Default Payment", referencepos.recu_DefaultPayment.ToString(), deviceCode);
                _configuration.Add(config);
                config = await SaveData("Capture Fiscal Information", referencepos.recu_CaptureFiscalInformation, deviceCode);
                _configuration.Add(config);
                config = await SaveData("XML Reconciliation", referencepos.recu_XMLReconciliation, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Select Automatic Shift Period", referencepos.recu_SelectAutomaticShiftPeriod, deviceCode);
                _configuration.Add(config);
                config = await SaveData("XML Reconciliation", referencepos.recu_XMLReconciliation, deviceCode);
                _configuration.Add(config);
                config = await SaveData("XML Reconciliation", referencepos.recu_XMLReconciliation, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Print Discount & Additional Charge Per LineItem", referencepos.recu_PrintDiscountAdditionalChargePerLineItem, deviceCode);
                _configuration.Add(config);
                config = await SaveData("QR Code Destination", referencepos.recu_QRCodeDestination.ToString(), deviceCode);
                _configuration.Add(config);
                config = await SaveData("Enable Online Payment", referencepos.recu_EnableOnlinePayment, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Cash Customer", referencepos.recu_CashCustomer, deviceCode);
                _configuration.Add(config);
                config = await SaveData("POS Store", referencepos.recu_POSStore, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Ignore zero price article", referencepos.recu_Ignore_zero_price_article.ToString(), deviceCode);
                _configuration.Add(config);
                config = await SaveData("Clear Hold Before Closing", referencepos.recu_ClearHoldBeforeClosing, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Enable Hold Functionality", referencepos.recu_EnableHoldFunctionality, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Day Closing", referencepos.recu_DayClosing, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Enable CBE", referencepos.recu_EnableCBE, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Enable Document Reference", referencepos.recu_EnableDocumentReference, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Enable GSL Tax", referencepos.recu_EnableGSLTax, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Enable Value Factory", referencepos.recu_EnableValueFactory, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Item Selection", referencepos.recu_ItemSelection.ToString(), deviceCode);
                _configuration.Add(config);
                config = await SaveData("POS Trigger Article Removal", referencepos.recu_POSTriggerArticleRemoval.ToString(), deviceCode);
                _configuration.Add(config);
                config = await SaveData("Enable Auto Entry", referencepos.recu_EnableAutoEntry, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Open Drawer After Print", referencepos.recu_OpenDrawerAfterPrint, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Article Code Length", referencepos.recu_ArticleCodeLength, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Article Code Start", referencepos.recu_ArticleCodeStart, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Code Length", referencepos.recu_CodeLength, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Decimal Position", referencepos.recu_DecimalPosition, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Enable Scale Encryption", referencepos.recu_EnableScaleEncryption, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Value Start", referencepos.recu_ValueStart, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Value Length", referencepos.recu_ValueLength, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Enable Vocal Assistanc", referencepos.recu_EnableVocalAssistance, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Language", referencepos.recu_Language.ToString(), deviceCode);
                _configuration.Add(config);
                config = await SaveData("POS Use Only Available Article", referencepos.recu_POSUseOnlyAvailableArticle.ToString(), deviceCode);
                _configuration.Add(config);
                config = await SaveData("POS Value Rule", referencepos.recu_POSValueRule.ToString(), deviceCode);
                _configuration.Add(config);
                config = await SaveData("Need Credentials For Customer Discount", referencepos.recu_NeedCredentialsForCustomerDiscount.ToString(), deviceCode);
                _configuration.Add(config);
                config = await SaveData("Beneficiary User", referencepos.recu_BeneficiaryUser.ToString(), deviceCode);
                _configuration.Add(config);
                config = await SaveData(" Device Status API URL", referencepos.recu_DeviceStatusAPIURL, deviceCode);
                _configuration.Add(config);

                await _sharedHelpers.saveOrUpdateRange(_configuration);
                msg = "Save Successfully";
                cansave = true;
                return Json(new { msg = msg, cansave = cansave });
            }
        }

        public async Task<IActionResult> SavingpremiumposSettingProperty(Devicemodel premiumpos)
        {
            string msg = null;
            bool cansave = false;
            List<ConfigurationDTO> devicecodelist = new List<ConfigurationDTO>();
            if (premiumpos.prem_devicecode == "0"|| premiumpos.prem_devicecode == null)
            {
                msg = "Please Select Device";
                cansave = false;
                return Json(new { msg = msg, cansave = cansave });
            }
            else
            {
                var deviceCode = premiumpos.prem_devicecode;
                Config = await _sharedHelpers.GetConfigurationByRefandPref(deviceCode, CNETConstants.DEVICE);
                await _sharedHelpers.DeleteConfigurationByReferenceAndPreference(deviceCode, CNETConstants.DEVICE);
                List<ConfigurationDTO> _configuration = new List<ConfigurationDTO>();
                ConfigurationDTO config = new ConfigurationDTO();

                config = await SaveData("Closing Frequency", premiumpos.prem_ClosingFrequency.ToString(), deviceCode);
                _configuration.Add(config);
                config = await SaveData("Accuracy Tolerance", premiumpos.prem_AccuracyTolerance.ToString(), deviceCode);
                _configuration.Add(config);
                config = await SaveData("Summary Difference Tolerance", premiumpos.prem_SummaryDifferenceTolerance.ToString(), deviceCode);
                _configuration.Add(config);
                config = await SaveData("Check Voucher Integrity", premiumpos.prem_CheckVoucherIntegrity, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Fiscal Reconciliation", premiumpos.prem_FiscalReconciliation, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Zeroing Voucher", premiumpos.prem_ZeroingVoucher, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Summary Voucher", premiumpos.prem_SummaryVoucher, deviceCode);
                _configuration.Add(config);
                config = await SaveData("DownLoad EJ", premiumpos.prem_DownLoadEJ, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Clear Held Bill", premiumpos.prem_ClearHeldBill, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Enable Bill Management", premiumpos.prem_EnableBillManagement, deviceCode);
                _configuration.Add(config);
                config = await SaveData("POS Trigger Article Removal", premiumpos.prem_POSTriggerArticleRemoval.ToString(), deviceCode);
                _configuration.Add(config);
                config = await SaveData("Need Credentials For Removal", premiumpos.prem_NeedCredentialsForRemoval.ToString(), deviceCode);
                _configuration.Add(config);
                config = await SaveData("Enable Order Function", premiumpos.prem_EnableOrderFunction, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Order Station Mandatory", premiumpos.prem_OrderStationMandatory, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Order Verification Method", premiumpos.prem_OrderVerificationMethod.ToString(), deviceCode);
                _configuration.Add(config);
                config = await SaveData("Open Drawer After Print", premiumpos.prem_OpenDrawerAfterPrint, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Default Transaction Type", premiumpos.prem_DefaultTransactionType.ToString(), deviceCode);
                _configuration.Add(config);
                config = await SaveData("Default Payment", premiumpos.prem_DefaultPayment.ToString(), deviceCode);
                _configuration.Add(config);
                config = await SaveData("Allow multiple Instance Of Table", premiumpos.prem_AllowmultipleInstanceOfTable, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Disable Void On Retrieve", premiumpos.prem_DisableVoidOnRetrieve, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Capture Fiscal Information", premiumpos.prem_CaptureFiscalInformation, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Print Discount & Additional Charge Per LineItem", premiumpos.prem_PrintDiscountAdditionalChargePerLineItem, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Enable Package Design", premiumpos.prem_EnablePackageDesign, deviceCode);
                _configuration.Add(config);
                config = await SaveData("QR Code Destination", premiumpos.prem_QRCodeDestination.ToString(), deviceCode);
                _configuration.Add(config);
                config = await SaveData("Closing Document Type", premiumpos.prem_ClosingFrequency.ToString(), deviceCode);
                _configuration.Add(config);
                config = await SaveData("Enable Online Payment", premiumpos.prem_EnableOnlinePayment, deviceCode);
                _configuration.Add(config);
                config = await SaveData("POS Value Rule", premiumpos.prem_POSValueRule.ToString(), deviceCode);
                _configuration.Add(config);
                config = await SaveData("Need Credentials For Customer Discount", premiumpos.prem_NeedCredentialsForCustomerDiscount.ToString(), deviceCode);
                _configuration.Add(config);
                config = await SaveData(" Beneficiary User", premiumpos.prem_BeneficiaryUser.ToString(), deviceCode);
                _configuration.Add(config);

                if (!string.IsNullOrWhiteSpace(premiumpos.prem_POSStore))
                {
                    string storecode = premiumpos.prem_POSStore.Split('/')[0];
                    if (!string.IsNullOrWhiteSpace(storecode))
                    {
                        var value = await _sharedHelpers.GetsystemConstantById(Convert.ToInt32(storecode));
                        if (value != null)
                        {
                            config = await SaveData(value?.FirstOrDefault().Id.ToString(), "POS Store", deviceCode);
                            _configuration.Add(config);
                        }
                        else
                        {
                            config = await SaveData("", "POS Store", deviceCode);
                            _configuration.Add(config);
                        }
                    }
                    else
                    {
                        config = await SaveData("", "POS Store", deviceCode);
                        _configuration.Add(config);
                    }

                }
                if (!string.IsNullOrWhiteSpace(premiumpos.prem_PriceType))
                {
                    string pricetype = premiumpos.prem_PriceType.Split('/')[0];
                    if (!string.IsNullOrWhiteSpace(pricetype))
                    {
                        var value = await _sharedHelpers.GetsystemConstantById(Convert.ToInt32(pricetype));
                        if (value != null)
                        {
                            config = await SaveData(value?.FirstOrDefault().Id.ToString(), "Price Type", deviceCode);
                            _configuration.Add(config);
                        }
                        else
                        {
                            config = await SaveData("", "Price Type", deviceCode);
                            _configuration.Add(config);
                        }
                    }
                    else
                    {
                        config = await SaveData("", "Price Type", deviceCode);
                        _configuration.Add(config);
                    }
                }
                config = await SaveData(premiumpos.prem_StationType.ToString(), "Station Type", deviceCode);
                _configuration.Add(config);
                config = await SaveData("Enable Table", premiumpos.prem_EnableTable, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Table Select waiter before open", premiumpos.prem_TableSelectwaiterbeforeopen, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Table Select cover before open", premiumpos.prem_TableSelectcoverbeforeopen, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Table Applicable servicecharge", string.IsNullOrEmpty(premiumpos.prem_TableApplicableservicecharge) ? "" : premiumpos.prem_TableApplicableservicecharge.Split('/')[0], deviceCode);
                _configuration.Add(config);
                config = await SaveData("Table Applicable Discount", string.IsNullOrEmpty(premiumpos.prem_TableApplicableDiscount) ? "" : premiumpos.prem_TableApplicableDiscount.Split('/')[0], deviceCode);
                _configuration.Add(config);
                config = await SaveData("Table Auto add item", string.IsNullOrEmpty(premiumpos.prem_TableAutoadditem) ? "" : premiumpos.prem_TableAutoadditem.Split('/')[0], deviceCode);
                _configuration.Add(config);
                config = await SaveData("Table Default category", string.IsNullOrEmpty(premiumpos.prem_TableDefaultcategory) ? "" : premiumpos.prem_TableDefaultcategory.Split('/')[0], deviceCode);
                _configuration.Add(config);
                config = await SaveData("Table Table selection type", premiumpos.prem_TableTableselectiontype.ToString(), deviceCode);
                _configuration.Add(config);
                config = await SaveData("Table Select seat position type", premiumpos.prem_TableSelectseatpositiontype.ToString(), deviceCode);
                _configuration.Add(config);
                config = await SaveData("Enable Tab", premiumpos.prem_EnableTab, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Tab Select waiter before open", premiumpos.prm_TabSelectwaiterbeforeopen, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Tab Select cover before open", premiumpos.prem_TabSelectcoverbeforeopen, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Tab Applicable servicecharge", string.IsNullOrEmpty(premiumpos.prem_TabApplicableservicecharge) ? "" : premiumpos.prem_TabApplicableservicecharge.Split('/')[0], deviceCode);
                _configuration.Add(config);
                config = await SaveData("Tab Applicable Discount", string.IsNullOrEmpty(premiumpos.prem_TabApplicableDiscount) ? "" : premiumpos.prem_TabApplicableDiscount.Split('/')[0], deviceCode);
                _configuration.Add(config);
                config = await SaveData("Tab Auto add item", string.IsNullOrEmpty(premiumpos.prem_TabAutoadditem) ? "" : premiumpos.prem_TabAutoadditem.Split('/')[0], deviceCode);
                _configuration.Add(config);
                config = await SaveData("Tab Default category", string.IsNullOrEmpty(premiumpos.prem_TabDefaultcategory) ? "" : premiumpos.prem_TabDefaultcategory.Split('/')[0], deviceCode);
                _configuration.Add(config);
                config = await SaveData("Tab Table selection type", premiumpos.prem_TabTableselectiontype.ToString(), deviceCode);
                _configuration.Add(config);
                config = await SaveData("Tab Select seat position type", premiumpos.prem_TabSelectseatpositiontype.ToString(), deviceCode);
                _configuration.Add(config);
                config = await SaveData("Enable Fast Transaction", premiumpos.prem_EnableFastTransaction, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Fast Transaction Select waiter before open", premiumpos.prme_FastTransactionSelectwaiterbeforeopen, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Fast Transaction Select cover before open", premiumpos.prem_FastTransactionSelectcoverbeforeopen, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Fast Transaction Applicable servicecharge", string.IsNullOrEmpty(premiumpos.prme_FastTransactionApplicableservicecharge) ? "" : premiumpos.prme_FastTransactionApplicableservicecharge.Split('/')[0], deviceCode);
                _configuration.Add(config);
                config = await SaveData("Fast Transaction Applicable Discount", string.IsNullOrEmpty(premiumpos.prem_FastTransactionApplicableDiscount) ? "" : premiumpos.prem_FastTransactionApplicableDiscount.Split('/')[0], deviceCode);
                _configuration.Add(config);
                config = await SaveData("Fast Transaction Auto add item", string.IsNullOrEmpty(premiumpos.prem_FastTransactionAutoadditem) ? "" : premiumpos.prem_FastTransactionAutoadditem.Split('/')[0], deviceCode);
                _configuration.Add(config);
                config = await SaveData("Fast Transaction Default category", string.IsNullOrEmpty(premiumpos.prem_FastTransactionDefaultcategory) ? "" : premiumpos.prem_FastTransactionDefaultcategory.Split('/')[0], deviceCode);
                _configuration.Add(config);
                config = await SaveData("Fast Transaction Table selection type", premiumpos.prem_FastTransactionTableselectiontype.ToString(), deviceCode);
                _configuration.Add(config);
                config = await SaveData("Fast Transaction Select seat position type", premiumpos.prme_FastTransactionSelectseatpostionType.ToString(), deviceCode);
                _configuration.Add(config);
                config = await SaveData("Enable Takeaway", premiumpos.prem_EnableTakeaway, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Takeaway Select waiter before open", premiumpos.prem_TakeawaySelectwaiterbeforeopen, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Takeaway Select cover before open", premiumpos.prem_TakeawaySelectcoverbeforeopen, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Takeaway Applicable servicecharge", string.IsNullOrEmpty(premiumpos.prem_TakeawayApplicableservicecharge) ? "" : premiumpos.prem_TakeawayApplicableservicecharge.Split('/')[0], deviceCode);
                _configuration.Add(config);
                config = await SaveData("Takeaway Applicable Discount", string.IsNullOrEmpty(premiumpos.prem_TakeawayApplicableDiscount) ? "" : premiumpos.prem_TakeawayApplicableDiscount.Split('/')[0], deviceCode);
                _configuration.Add(config);
                config = await SaveData("Takeaway Auto add item", string.IsNullOrEmpty(premiumpos.prem_TakeawayAutoadditem) ? "" : premiumpos.prem_TakeawayAutoadditem.Split('/')[0], deviceCode);
                _configuration.Add(config);
                config = await SaveData("Takeaway Default category", string.IsNullOrEmpty(premiumpos.prem_TakeawayDefaultcategory) ? "" : premiumpos.prem_TakeawayDefaultcategory.Split('/')[0], deviceCode);
                _configuration.Add(config);
                config = await SaveData("Takeaway Table selection type", premiumpos.prem_TakeawayTableselectiontype.ToString(), deviceCode);
                _configuration.Add(config);
                config = await SaveData("Takeaway Select seat position type", premiumpos.prem_TakeawaySelectseatposition.ToString(), deviceCode);
                _configuration.Add(config);
                config = await SaveData("Enable Delivery", premiumpos.prem_EnableDelivery, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Delivery Select waiter before open", premiumpos.prem_DeliverySelectwaiterbeforeopen, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Delivery Select cover before open", premiumpos.prem_DeliverySelectcoverbeforeopen, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Delivery Applicable servicecharge", string.IsNullOrEmpty(premiumpos.prem_DeliveryApplicableservicecharge) ? "" : premiumpos.prem_DeliveryApplicableservicecharge.Split('/')[0], deviceCode);
                _configuration.Add(config);
                config = await SaveData("Delivery Applicable Discount", string.IsNullOrEmpty(premiumpos.prem_DeliveryApplicableDiscount) ? "" : premiumpos.prem_DeliveryApplicableDiscount.Split('/')[0], deviceCode);
                _configuration.Add(config);
                config = await SaveData("Delivery Auto add item", string.IsNullOrEmpty(premiumpos.prem_DeliveryAutoadditem) ? "" : premiumpos.prem_DeliveryAutoadditem.Split('/')[0], deviceCode);
                _configuration.Add(config);
                config = await SaveData("Delivery Default category", string.IsNullOrEmpty(premiumpos.prem_DeliveryDefaultcategory) ? "" : premiumpos.prem_DeliveryDefaultcategory.Split('/')[0], deviceCode);
                _configuration.Add(config);
                config = await SaveData("Delivery Table selection type", premiumpos.prem_deliveryTableSelectionType.ToString(), deviceCode);
                _configuration.Add(config);
                config = await SaveData("Delivery Select seat position type", premiumpos.prem_DeliverySelectseatpositiontype.ToString(), deviceCode);
                _configuration.Add(config);
                config = await SaveData("Enable Room Service", premiumpos.prem_EnableRoomService, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Room Service Select waiter before open", premiumpos.prem_RoomServiceSelectwaiterbeforeopen, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Room Service Select cover before open", premiumpos.prem_RoomServiceSelectcoverbeforeopen, deviceCode);
                _configuration.Add(config);
                config = await SaveData("Room Service Applicable servicecharge", string.IsNullOrEmpty(premiumpos.prem_RoomServiceApplicableservicecharge) ? "" : premiumpos.prem_RoomServiceApplicableservicecharge.Split('/')[0], deviceCode);
                _configuration.Add(config);
                config = await SaveData("Room Service Applicable Discount", string.IsNullOrEmpty(premiumpos.prem_RoomServiceApplicableDiscount) ? "" : premiumpos.prem_RoomServiceApplicableDiscount.Split('/')[0], deviceCode);
                _configuration.Add(config);
                config = await SaveData("Room Service Auto add item", string.IsNullOrEmpty(premiumpos.prem_RoomServiceAutoadditem) ? "" : premiumpos.prem_RoomServiceAutoadditem.Split('/')[0], deviceCode);
                _configuration.Add(config);
                config = await SaveData("Room Service Default category", string.IsNullOrEmpty(premiumpos.prem_RoomServiceDefaultcategory) ? "" : premiumpos.prem_RoomServiceDefaultcategory.Split('/')[0], deviceCode);
                _configuration.Add(config);
                config = await SaveData("Room Service Table selection type", premiumpos.prem_RoomServiceTableSelectionType.ToString(), deviceCode);
                _configuration.Add(config);
                config = await SaveData("Room Service Select seat position type", premiumpos.prem_RoomServiceSelectseatpositiontype.ToString(), deviceCode);
                _configuration.Add(config);
                config = await SaveData("Ignore zero price article", premiumpos.prem_Ignore_zero_price_article.ToString(), deviceCode);
                _configuration.Add(config);
                config = await SaveData("POS Use Only Available Article", premiumpos.prem_POSUseOnlyAvailableArticle.ToString(), deviceCode);
                _configuration.Add(config);

                await _sharedHelpers.saveOrUpdateRange(_configuration);
                msg = "Save Successfully";
                cansave = true;
                return Json(new { msg = msg, cansave = cansave });
            }
        }
        public async Task<ConfigurationDTO> SaveData(string arttb, string currnt, string devicecodeType)
        {
            var atrributt = arttb;
            var curentval = currnt;
            ConfigurationDTO configuration = new ConfigurationDTO();
            List<ConfigurationDTO> listcConfigurations = new List<ConfigurationDTO>();
            ConfigurationDTO Prev = Config.Where(x => x.Attribute?.Trim() == arttb?.Trim())?.FirstOrDefault();
            configuration.Pointer = CNETConstants.DEVICE;
            configuration.Reference = devicecodeType;
            configuration.Attribute = arttb;
            configuration.CurrentValue = currnt == null ? "" : currnt;
            if (Prev != null)
            {
                configuration.PreviousValue = Prev.CurrentValue;
            }
            else
            {
                configuration.PreviousValue = currnt == null ? "" : currnt;
            }
            return configuration;
        }
        private string Encrypt(string clearText)
        {

            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
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
        public async Task<IActionResult> GetOrderStationMap(int stationdevice, int preference, int posDevice)
        {
            if (preference == 0)
            {
                preference = 13;
            }
            var _pref = await _sharedHelpers.GetpreferenceByreference(preference);
            var orderStationMaplt = await _sharedHelpers.GetOrderStationByPosDevice(posDevice);
            var orderStationMap = orderStationMaplt.Where(x => x.StationDevice == stationdevice).ToList();

            List<PreferenceoDTO> preferenceoDTOs = new List<PreferenceoDTO>();
            if (_pref != null && _pref.Count() > 0)
            {
                foreach (var item in _pref)
                {
                    preferenceoDTOs.Add(new PreferenceoDTO
                    {
                        Id = item.Id,
                        Description = item.Description,
                        IsActive = false,
                        ParentId = item.ParentId
                    });
                }
            }
            if (preferenceoDTOs != null && preferenceoDTOs.Count() > 0)
            {

                foreach (var item in preferenceoDTOs)
                {
                    item.IsActive = orderStationMap.Select(y => y.Preference.ToString()).Contains(item.Id.ToString()) ? true : false;
                    item.ParentId = preferenceoDTOs.Where(y => y.Id == item.ParentId).Count() > 0 ? item.ParentId : null;
                }
            }

            Devicemodel _oModel = new Devicemodel();
            _oModel._prfOrderstn = preferenceoDTOs;
            _oModel.stationDevice = stationdevice;
            _oModel.posDevice = posDevice;
            _oModel.prefererenceList = preference;
            return PartialView("OrderstationCategory", _oModel);


        }
        public async Task<IActionResult> SaveOrderstationdevice(Devicemodel _oModel)
        {
            List<string> matchList = new List<string>();
            string resultTYpe = null;
            bool Notesaved = false;
            List<int> PrevOrder = new List<int>();
            if (_oModel.posDevice != 0)
            {
                var _pref = await _sharedHelpers.GetpreferenceByreference(_oModel.prefererenceList);
                List<PreferenceDTO> pre = new List<PreferenceDTO>();
                if (_oModel.Orderstationchecklist != null && _oModel.Orderstationchecklist.Count() > 0)
                {
                    for (int item = 0; item < _oModel.Orderstationchecklist.Count(); item++)
                    {
                      var pref  =  _pref.Where(x => x.Id == _oModel.Orderstationchecklist[item]).ToList();
                        pre.AddRange(pref);
                    }
                }
                var orderStationMaplt = await _sharedHelpers.GetOrderStationByPosDevice(_oModel.posDevice);
                var orderStationMap = orderStationMaplt.Where(x => x.StationDevice == _oModel.stationDevice).ToList();
                if (orderStationMap != null && orderStationMap.Count() > 0)
                {
                    foreach (var item in orderStationMap)
                    {
                        await _sharedHelpers.DeleteOrderStationMap(item?.Id);
                    }
                }
                List<OrderStationMapDTO> posdevice = await _sharedHelpers.GetOrderStationByPosDevice(_oModel.posDevice);
                PrevOrder = posdevice.Select(x => x.Preference).ToList();



                var _idd = _oModel.navType;
                string? type = _sharedHelpers.GetsystemConstantById(_idd)?.Result.FirstOrDefault()?.Description;
                foreach (var pref in pre)
                {
                    var currentList = posdevice.Where(p => p.PosDevice == _oModel.posDevice && p.Preference == pref.Id && p.StationDevice == _oModel.stationDevice).ToList();
                    if (type == "KDS" && !(currentList.Count > 0))
                    {

                        OrderStationMapDTO steation = new OrderStationMapDTO();
                        steation.PosDevice = _oModel.posDevice;
                        steation.Preference = pref.Id;
                        steation.StationDevice = _oModel.stationDevice;
                        await _sharedHelpers.CreateOrderStationMap(steation);
                       
                    }
                    else
                    {
                        if (!PrevOrder.Contains(pref.Id))
                        {

                            OrderStationMapDTO steation = new OrderStationMapDTO();
                            steation.PosDevice = _oModel.posDevice;
                            steation.Preference = pref.Id;
                            steation.StationDevice = _oModel.stationDevice;
                            await _sharedHelpers.CreateOrderStationMap(steation);
                          
                        }
                        else
                        {

                            matchList.Add(pref.Description);
                        }
                        if (matchList != null && matchList.Count() > 0)
                        {
                            Notesaved = true;
                        }
                        else
                        {
                            Notesaved = false;
                        }
                    }
                    resultTYpe = "Saved Successfully";
                }
                return Json(new { increament = matchList, result = resultTYpe, notesaved = Notesaved });
            }
            else
            {
                resultTYpe = "Please select  device";
                return Json(new { increament = matchList, result = resultTYpe, notesaved = Notesaved });
            }

        }
        public async Task<IActionResult> getDeviceConnectionType(int devCode)
        {
            bool isPara = false;
            bool isserial = false;
           

            var deviceviewList = await _sharedHelpers.SelectDeviceById(devCode);
            var connectionType = deviceviewList.FirstOrDefault()?.ConnectionType;
            switch (connectionType)
            {
                case 1550:
                case 1547:
                case 1548:
                case 1546:
                    isPara = false;
                    isserial = true;
                    break;
                case 1549:
                case 1552:
                    isPara = true;
                    isserial = false;
                    break;
                case 1545:
                case 1551:
                    isPara = false;
                    isserial = false;
                    break;
            }
            return Json(new { isPara  = isPara , isserial  = isserial });
        }

        public async Task<IActionResult> getIPparamtreResult(string PARALIST)
        {
            //string devicecde = PARALIST;
            //var getIPParameter = await _uIProcessManager.SelectIpParameter();

            //var getIPParameterList = getIPParameter.Where(x => x.device == devicecde).Select(y => new

            //{
            //    y.code,
            //    y.device,
            //    y.ip,
            //    y.port,
            //    y.mac,
            //    y.remark
            //}).ToList();

            return Json(new EmptyResult());
        }
    }
}

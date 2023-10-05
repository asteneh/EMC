using CNET_V7_Domain.Domain.SettingSchema;
using CNET_V7_Entities.DataModels;

namespace CNET_ERP_V7.Models
{
    public class LicenseModel
    {

        public string lince_description { get; set; }
        public int lince_device { get; set; }
        public string lince_code { get; set; }
        public string lince_remaiiningDay { get; set; }
        public string lince_remark { get; set; }
        public string lince_updateposcode { get; set; }
        public string lince_updateSubcode { get; set; }

        public List<string> licenccodeList { get; set; }
        public List<string> componentcodeList { get; set; }

        public List<CnetlicenseDTO> cNETLicenses { get; set; }
        public List<CnetlicenseDTO> NETLicenses { get; set; }
    }
}

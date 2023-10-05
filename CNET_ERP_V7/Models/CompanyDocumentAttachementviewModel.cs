using System.ComponentModel.DataAnnotations;

namespace CNET_ERP_V7.Models
{
    public class CompanyDocumentAttachementviewModel
    {
        public int company_attcode { get; set; }
        public int? company_reference { get; set; }
        public int? company_catagory { get; set; }
        [Required(ErrorMessage = "please enter description")]
        public string company_description { get; set; }
        public string company_url { get; set; }
        public IFormFile company_file { get; set; }
        public string company_type { get; set; }
        public byte company_index { get; set; }
        public string company_remark { get; set; }
    }
}

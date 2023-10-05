using System.ComponentModel.DataAnnotations;

namespace CNET_ERP_V7.Models
{
    public class CompanyDocumentAttachementEachviewModel
    {
        public int company_eachattcode { get; set; }
        public int? company_eachreference { get; set; }
        public int? company_eachcatagory { get; set; }
        [Required(ErrorMessage = "please enter description")]
        public string company_eachdescription { get; set; }
        public string company_eachurl { get; set; }
        public IFormFile company_eachfile { get; set; }
        public string company_eachtype { get; set; }
        public byte company_eachindex { get; set; }
        public string company_eachremark { get; set; }
    }
}

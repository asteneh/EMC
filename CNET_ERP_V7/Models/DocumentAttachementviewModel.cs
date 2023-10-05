using System.ComponentModel.DataAnnotations;

namespace CNET_ERP_V7.Models
{
    public class DocumentAttachementviewModel
    {

        public int com_attcode { get; set; }
        public int? com_reference { get; set; }
        public int? com_catagory { get; set; }
        [Required(ErrorMessage = "please enter description")]
        public string com_description { get; set; }
        public string? com_url { get; set; }
        public IFormFile com_file { get; set; }
        public string com_type { get; set; }
        public byte com_index { get; set; }
        public string com_remark { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace CNET_ERP_V7.Models
{
    public class AttachementviewModel
    {

        public int attcode { get; set; }
        public int? reference { get; set; }
        public int?  catagory { get; set; }
        [Required(ErrorMessage = "please enter description")]
        public string description { get; set; }
        public string url { get; set; }
        public IFormFile file { get; set; }
        public string type { get; set; }
        public byte index { get; set; }
        public string remark { get; set; }
    }
}

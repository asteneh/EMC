using System.ComponentModel;

namespace CNET_ERP_V7.Models
{
    public class VerifyIdModel
    {
        [DisplayName("Identification No.")]
        public string? myId { get; set; }
        [DisplayName("Remember")]
        public bool remember { get; set; }
    }
}

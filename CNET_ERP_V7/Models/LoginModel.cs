using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using CNET_V7_Domain.Domain.ConsigneeSchema;

namespace CNET_ERP_V7.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Username is required!")]
        [DataType(DataType.Text)]
        [DisplayName("Username")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [DataType(DataType.Password)]
        //[NoTrim]
        [DisplayName("Password")]
        public string? Password { get; set; }

        [DisplayName("Remember Me")]
        public bool RememberMe { get; set; }

        [Required(ErrorMessage = "Branch is required!")]
        public string? Branch { get; set; }
        public string? _tin { get; set; }
        public validIdentificationReturn? ValidID { get; set; } = new validIdentificationReturn();
    }
    public class validIdentificationReturn {
        public string? tin { get; set; }
        public bool isValid { get; set; }
        public string? CompanyTradeName { get; set; }
        public List<ConsigneeUnitDTO>? BranchList { get; set; }
    }
    public class Authentication
    {
        public bool data { get; set; }
        public object ex { get; set; }
        public object message { get; set; }
        public bool success { get; set; }
    }
}

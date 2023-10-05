
using CNET_V7_Domain.Domain.ConsigneeSchema;
using CNET_V7_Domain.Misc.CommonTypes;

namespace CNET_ERP_V7.Models.FramworkModels
{
    public class GlobalParamsModel
    {
        public int? gslType { get; set; }
        public List<NavigatorDTO>? navigatorList { get; set; }
        public ConsigneeDTO? personInfo { get; set; }
        public string? currentBranch { get; set; }
    }
}

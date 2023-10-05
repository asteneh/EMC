using CNET_V7_Domain.Domain.ConsigneeSchema;
using CNET_V7_Domain.Domain.HrmsSchema;
using CNET_V7_Domain.Domain.SettingSchema;
        
namespace CNET_ERP_V7.Common.Helpers
{
    public class MapSstore
    {
        public List<ConsigneeUnitM> _destination = new List<ConsigneeUnitM>();
        public List<ConsigneeUnitM> _source = new List<ConsigneeUnitM>();
        public List<VoucherStoreMap> _vsource = new List<VoucherStoreMap>();
        public List<VoucherStoreMap> _vdestination = new List<VoucherStoreMap>();
    }
    public class ConsigneeUnitM
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
    public class VoucherStoreMap
    {
        public int Id { get; set; }
        public int Store { get; set; }
        public bool IsDefault { get; set; }

        public string? Remark { get; set; }
    }
}

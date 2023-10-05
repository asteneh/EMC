namespace CNET_ERP_V7.Models.SelectorModel
{
    public class Select2Model
    {
        public int id { get; set; }
        public string idd { get; set; }
        public string text { get; set; }
        public string qq { get; set; }
        public string q { get; set; }
        public int page { get; set; }
        public int other { get; set; }
        public string brandName { get; set; }
        public string businessName { get; set; }
        public bool isActive { get; set; }

        public List<Select2Result> children { get; set; }
    }
}

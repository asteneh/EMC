namespace CNET_ERP_V7.Models.SelectorModel
{
    public class Select2Result
    {   
        public int? id { get; set; }  
        public string idd { get; set; }  
        public string? text { get; set; }
        public string name { get; set; }
        public List<Select2Result> children { get; set; }
    }
}

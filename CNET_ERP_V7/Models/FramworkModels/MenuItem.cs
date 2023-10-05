namespace CNET_ERP_V7.Models.FramworkModels
{
    public class MenuItem
    {
        public string SystemName { get; set; }

        public string Title { get; set; }

        public string ControllerName { get; set; }

        public string ActionName { get; set; }

        public RouteValueDictionary RouteValues { get; set; }

        public string Url { get; set; }

        public IList<MenuItem> ChildNodes { get; set; }

        public string IconClass { get; set; }

        public bool Visible { get; set; }

        public bool OpenUrlInNewTab { get; set; }
    }
}

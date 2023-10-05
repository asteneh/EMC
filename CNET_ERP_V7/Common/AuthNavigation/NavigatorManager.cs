using CNET_ERP_V7.Models.FramworkModels;
using CNET_V7_Domain;
using CNET_V7_Domain.Misc.CommonTypes;
using Microsoft.AspNetCore.Mvc;

namespace CNET_ERP_V7.Common.AuthNavigation
{
    public class NavigatorManager
    {
        static AuthenticationManager _authenticationManager;
        public NavigatorManager(AuthenticationManager authenticationManager)
        {
            _authenticationManager = authenticationManager;
        }
        public async Task<List<MenuItem>> PrepareNavigationList(List<NavigatorDTO>? module)
        {
            if (module == null)
            {
                return null;
            }
            var navigationsList = new List<MenuItem>();
            foreach ( var item in module)
            {
                var gslParent = new MenuItem()
                {
                    Title = item.name,
                    IconClass = "fa-briefcase",
                    ChildNodes = await geNodes(item.children,item.name)
                };
                navigationsList.Add(gslParent);
            }

            return navigationsList;
        }
        
        private async Task<List<MenuItem>> geNodes(List<NavigatorDTO> sub_module, string Controllername)
        {
            if (sub_module == null)
            {
                return null;
            }
            string ControllerName = Controllername;

            var nodes = new List<MenuItem>();
            MenuItem menuItem;

            foreach (var item in sub_module)
            {
                var childs = item.children.ToList();
                if (Controllername == "Subsystems")
                {
                    childs.Add(new NavigatorDTO { id = item.id, name = item.name, children = item.children });
                }
                if (Controllername == "System Setting")
                {
                    if (item.name == "Company Setting")
                    {
                        ControllerName = "Company";

                    }
                    else if (item.name == "System Parameters")
                    {
                        ControllerName = "SystemParameters";
                    }
                    else if (item.name == "ID Definition")
                    {
                        ControllerName = "IdDefinition";
                    }
                    else if (item.name == "License")
                    {
                        ControllerName = "Licence";
                    }
                    else if (item.name == "Security")
                    {
                        ControllerName = "Security";
                    }
                    else if (item.name == "System Constants")
                    {
                        ControllerName = "SystemConstant";
                    }
                }

                var subsystemName = item.name;
                menuItem = new MenuItem()
                {
                    Title = item.name,
                    IconClass = "fa-dot-circle",
                    Url = "/" + ControllerName + "/List/" + item.id,
                    ChildNodes = new List<MenuItem>()
                };

                foreach (var child in childs)
                {
                    var grandchilds = child.children.ToList();

                    var childNode = new MenuItem()
                    {
                        Title = child.name,
                        IconClass = "fa-dot-circle",
                        Url = (child.children.Count() > 0) ? "/Module/List/" + child.id : "/" + ControllerName + "/List/" + child.id,
                        ChildNodes = new List<MenuItem>()
                    };
                    if (subsystemName == "Human Resource Management System")
                    {
                        foreach (var grandchild in grandchilds)
                        {
                            childNode.ChildNodes.Add(new MenuItem()
                            {
                                Title = grandchild.name,
                                IconClass = "fa-dot-circle-o",
                                Url = (grandchild.children.Count() > 0) ? "/Module/List/" + child.id : "/" + ControllerName + "/List/" + child.id,
                            });
                        }
                    }
                    menuItem.ChildNodes.Add(childNode);
                }

                nodes.Add(menuItem);
            }

            return nodes;
        }

    }
}

using NMShop.Shared.Scaffold;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMShop.Shared.Models
{
    public class NavigationUnit
    {
        public NavigationItem Category { get; set; }
        public List<NavigationItem> Items { get; set; }
    }
}

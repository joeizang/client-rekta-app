using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RektaRetailApp.Web.Models
{
    public class ButtonModel
    {
        public string ButtonText { get; set; } = null!;

        public string AspActionName { get; set; } = null!;

        public List<string> CssClasses { get; set; } = new List<string>();

        public string? BootstrapIconClass { get; set; }
    }
}

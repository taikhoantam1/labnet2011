using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using DomainModel.Properties;

namespace DomainModel
{
    public class VMPanelSearch
    {
        [Required(ErrorMessageResourceName = "VMTestSearch_NameRequired", ErrorMessageResourceType = typeof(Resources))]
        public string Name { get; set; }

        public List<PanelSearchObject> ListSearchResult { get; set; }
    }

    public class PanelSearchObject
    {
        public int Id { get; set; }

        public string PanelName { get; set; }
    }
}

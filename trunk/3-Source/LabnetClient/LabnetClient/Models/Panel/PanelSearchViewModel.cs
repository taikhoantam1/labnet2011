using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainModel;

namespace LabnetClient.Models
{
    public class PanelSearchViewModel : BaseModel
    {
        public PanelSearchViewModel()
        {
        }

        public string PanelName { get; set; }

        public JQGridModel JQGrid { get; set; }

        public AutocompleteModel Autocomplete { get; set; }

        public PanelSearchViewModel(List<VMPanel> panels)
        {
            Autocomplete = new AutocompleteModel("Panel.Name");
            JQGrid = new JQGridModel(typeof(VMPanel), false, panels, "");
        }
    }
}
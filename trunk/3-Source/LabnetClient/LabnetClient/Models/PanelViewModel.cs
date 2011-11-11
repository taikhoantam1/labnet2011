using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainModel;

namespace LabnetClient.Models
{
    public class PanelViewModel : BaseModel
    {
        public PanelViewModel()
        {
            Panel = new VMPanel();
            Autocomplete = new AutocompleteModel("Panel.TestName");
        }

        public VMPanel Panel { get; set; }

        public AutocompleteModel Autocomplete { get; set; }

        public List<VMTestListItem> PanelTestList { get; set; }
    }
}
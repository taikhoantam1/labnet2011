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
            JQGrid = new JQGridModel(typeof(VMTestListItem), "", "");
            PanelTestList = new List<VMTestListItem>();
        }

        public PanelViewModel(VMPanel panel,List<VMTestListItem> panelTestList)
        {
            Panel = panel;
            Autocomplete = new AutocompleteModel("Panel.TestName");
            PanelTestList = panelTestList;
            JQGrid = new JQGridModel(typeof(VMTestListItem), panelTestList, "");
        }

        public VMPanel Panel { get; set; }

        public AutocompleteModel Autocomplete { get; set; }

        public List<VMTestListItem> PanelTestList { get; set; }

        public JQGridModel JQGrid { get; set; }
    }
}
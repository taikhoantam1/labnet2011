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
        
        }

        public PanelViewModel(VMPanel panel,List<VMTestListItem> panelTestList)
        {
            Panel = panel;
            Autocomplete = new AutocompleteModel("Panel.TestName");
            PanelTestList = panelTestList;
            JQGrid = new JQGridModel(typeof(VMTestListItem), true,panelTestList,"/Panel/SavePanelTest");
        }

        public VMPanel Panel { get; set; }

        public AutocompleteModel Autocomplete { get; set; }

        public List<VMTestListItem> PanelTestList { get; set; }

        public JQGridModel JQGrid { get; set; }
    }
}
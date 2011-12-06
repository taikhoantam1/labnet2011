using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DomainModel;

namespace LabnetClient.Models
{
    public class PanelSearchViewModel : BaseModel
    {
        public VMPanelSearch PanelSearch { get; set; }

        public PanelSearchViewModel()
        {
            PanelSearch = new VMPanelSearch();
        }
    }
}
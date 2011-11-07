using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainModel
{
    public class VMTestListItem
    {
        public VMTestListItem()
        {
            IsDelete = false;
        }
        public string TestName { get; set; }
        public int TesstId { get; set; }
        public decimal Cost { get; set; }
        public bool IsDelete { get; set; }
    }
}

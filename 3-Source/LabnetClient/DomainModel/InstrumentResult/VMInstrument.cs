using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainModel
{
    public class VMInstrument
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int BaudRate { get; set; }

        public int Databits { get; set; }

        public string Parity { get; set; }

        public string Stopbit { get; set; }

        public string COMName { get; set; }

        public bool IsActive { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Models
{
    public class PhoenixReport
    {
        public Guid GUID { get; set; }
        public string Name { get; set; }
        public DateTime Timestamp { get; set; }
        public string ProjectName { get; set; }
        public string SuiteName { get; set; }
        public Queue<PhoenixTest> Tests { get; set; }
    }
}

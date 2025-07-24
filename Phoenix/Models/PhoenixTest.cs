using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Models
{
    public class PhoenixTest
    {
        public string Author { get; set; }
        public string Description { get; set; }
        public Uri DefectLink { get; set; }
        public Guid GUID { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public DateTime Timestamp { get; set; }
        public List<string> Tags { get; set; }
        public Queue<PhoenixEvent> Events { get; set; }
    }
}

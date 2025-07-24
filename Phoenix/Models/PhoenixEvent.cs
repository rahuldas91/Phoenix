using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Models
{
    public class PhoenixEvent
    {
        public int Type { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
        public object ActualData { get; set; }
        public object ExpectedData { get; set; }
        public bool Status { get; set; }
        public string Screenshot { get; set; }
    }
}

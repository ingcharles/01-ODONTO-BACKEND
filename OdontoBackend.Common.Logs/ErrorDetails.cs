using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OdontoBackend.Common.Logs
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = default!;
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}

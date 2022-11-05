using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChanelEngine.Service.Common.Models
{
    public class ExtraData
    {
        public string VAT_CALCULATION_METHOD_KEY { get; set; }

        [JsonProperty("Extra Data")]
        public string Data { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Orders.Common.Interfaces
{
    public interface IApplicationConfiguration
    {
        string Url { get; set; }
        string ApiKey { get; set; }
    }
}

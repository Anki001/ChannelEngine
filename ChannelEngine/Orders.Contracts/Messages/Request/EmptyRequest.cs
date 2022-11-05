using System;
using System.Collections.Generic;
using System.Text;

namespace Orders.Contracts.Messages.Request
{
    public class EmptyRequest
    {
        public static EmptyRequest Instance { get; set; }
        public EmptyRequest()
        {
            Instance = new EmptyRequest();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shopper.Models
{
    class SToken
    {
        public string Token { get; set; }

        public DateTime Expiry { get; set; }

        public DateTime Issued { get; set; }
    }
}

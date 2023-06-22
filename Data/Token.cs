using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FnolApiVersion2.O.Data
{
    //Class that is used for displaying details of token and its expiry time
    public class Token
    {
        public string? TokenGenerated {get;set;}

        public DateTime TokenExpiry {get;set;}
    }
}
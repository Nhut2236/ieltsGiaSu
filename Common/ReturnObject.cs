using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HooksLearning.Common
{
    public class ReturnObject
    {
        public string message;
        public int status;
        public dynamic data;
    }
}

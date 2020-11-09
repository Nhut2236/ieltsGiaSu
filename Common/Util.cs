using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HooksLearning.Common
{
    public class Util
    {
        public static string vnNowStr()
        {
            return DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        public static ReturnObject returnError(Exception ex)
        {
            ReturnObject obj = new ReturnObject();
            obj.status = 404;
            obj.message = ex.StackTrace;
            return obj;
        }
    }
}

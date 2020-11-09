using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BookingServices.Models;
using Microsoft.AspNetCore.Mvc;


namespace BookingServices.Controllers
{
    public class BaseAccountController : Controller
    {
        protected readonly BookingServicesContext bookingServicesContext;

        public BaseAccountController(BookingServicesContext bookingServicesContext)
        {
            this.bookingServicesContext = bookingServicesContext;
        }

        public String getUserIdByToken()
        {
            var header = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var credentials = header.Parameter;
            var tokenString = new JwtSecurityToken(jwtEncodedString: credentials);
            string claims = tokenString.Claims.First(c => c.Type == "Id").Value;
            return claims;
        }
    }
}

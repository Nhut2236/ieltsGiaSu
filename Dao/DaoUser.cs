using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingServices.Models;
using TWG_SSM.Dao;

namespace BookingServices.Dao
{
    public class DaoUser : DaoBase
    {
        public DaoUser(BookingServicesContext _context) : base(_context) { }

        public Users getById(string Id)
        {
            return context.Users.Where(p => p.Id.Equals(Id)).FirstOrDefault();
        }

        public Users SignIn(string password)
        {
            return context.Users.Where(p => p.Password.Equals(password)).FirstOrDefault();
        }

        public object Logout(string password, string userId)
        {
            Boolean success = false;
            string message = "Something is error. Try again!";
            var exist = context.Users.Where(p => p.Id.Equals(userId) && p.Password.Equals(password)).FirstOrDefault();
            if (exist != null)
            {
                success = true;
                message = "Successfully. Hope to see you soon!";
            }
            return new
            {
                success = success,
                message = message
            };
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookingServices.Models;
using TWG_SSM.Dao;
using HooksLearning.Common;

namespace BookingServices.Dao
{
    public class DaoComment : DaoBase
    {
        public DaoComment(BookingServicesContext _context) : base(_context) { }

        public Comment GetById(string Id)
        {
            return context.Comment.Where(p => p.Id.Equals(Id)).FirstOrDefault();
        }

        public Comment Create(Comment comment)
        {
            comment.Id = Guid.NewGuid().ToString();
            comment.CreatedAt = Util.vnNowStr();
            context.Comment.Add(comment);
            context.SaveChanges();
            return comment;
        }

        public List<Comment> GetAll()
        {
            return context.Comment.ToList();
        }
    }
}

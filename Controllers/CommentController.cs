using Microsoft.AspNetCore.Mvc;
using BookingServices.Models;
using BookingServices.Dao;
using Microsoft.AspNetCore.Authorization;


namespace BookingServices.Controllers
{
    [Route("api/[controller]/[action]")]
    public class CommentController : BaseAccountController
    {
        public CommentController(BookingServicesContext bookingServicesContext) : base(bookingServicesContext) { }

        [HttpGet("{id}")]
        [Authorize]
        public object Get(string id)
        {
            DaoUser daoUser = new DaoUser(bookingServicesContext);
            var data = daoUser.getById(id);
            return data;
        }

        [HttpPost]
        [Authorize]
        public Comment Create([FromBody] Comment entity)
        {
            DaoComment daoComment = new DaoComment(bookingServicesContext);
            Comment comment = daoComment.Create(entity);
            return comment;
        }

        [HttpGet]
        public object GetAll()
        {
            DaoComment daoComment = new DaoComment(bookingServicesContext);
            var data = daoComment.GetAll();
            return data;
        }
    }
}

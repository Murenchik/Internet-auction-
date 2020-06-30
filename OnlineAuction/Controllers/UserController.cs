using OnlineAuction.Mapper;
using OnlineAuction.Model;
using Services;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OnlineAuction.Controllers
{
    public class UserController : ApiController
    {

        private readonly IUserService Service;

        public UserController(IUserService service)
        {
            Service = service;
        }

        // POST api/user/register/
        [HttpPost]
        public HttpResponseMessage Register([FromBody]string userCredentials)
        {
            var loginPass = userCredentials.Split();
            if (loginPass.Length != 2)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad user credentials");
            }
            try
            {
                return Request.CreateResponse(HttpStatusCode.OK,
                    MapperDTO.Mapper.Map<User>(Service.Register(loginPass[0], loginPass[1])));
            }
            catch (InvalidOperationException exc)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exc.Message);
            }
        }

        // POST api/user/login/
        [HttpPost]
        public HttpResponseMessage Login([FromBody]string userCredentials)
        {
            var loginPass = userCredentials.Split();
            if (loginPass.Length != 2)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad user credentials");
            }
            try
            {
                var user = MapperDTO.Mapper.Map<User>(Service.Authorize(loginPass[0], loginPass[1]));
                if (user == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User not found");
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, user);
                }
            }
            catch (InvalidOperationException exc)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exc.Message);
            }
           
        }
    }
}

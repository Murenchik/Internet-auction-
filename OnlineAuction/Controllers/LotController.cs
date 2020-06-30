using OnlineAuction.Mapper;
using OnlineAuction.Model;
using Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OnlineAuction.Controllers
{
    public class LotController : ApiController
    {
        private readonly ILotService Service;

        public LotController(ILotService service)
        {
            Service = service;
        }

        [HttpGet]
        public HttpResponseMessage All()
        {
            try
            {
                var result = Service.All() as List<Services.Model.Lot>;
                result.ForEach(l =>
                {
                    if (l.Admin != null)
                    {
                        l.Admin.Password = null;
                        l.Admin.Lots = null;
                    }
                    if (l.Subcategory != null)
                    {
                        l.Subcategory.Lots = null;
                    }
                    l.User.Password = null;
                    l.User.Lots = null;
                    l.Category.Lots = null;
                });
                var rest = MapperDTO.Mapper.Map<ICollection<Lot>>(result);
                return Request.CreateResponse(HttpStatusCode.OK, rest);
            } catch (Exception exc)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad user credentials.");
            }
           

        }

        // PUT api/lot/add/
        [HttpPut]
        public HttpResponseMessage Add(
                                    string description,
                                    int categoryId,
                                    int subcategoryId,
                                    DateTime expiryDate,
                                    [FromBody]string userCredentials)
        {
            if (userCredentials == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "No user credentials.");
            }
            var loginPass = userCredentials.Split();
            if (loginPass.Length != 2)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad user credentials.");
            }
            try
            {
                var mapper = MapperDTO.Mapper;

                var category = mapper.Map<Category, Services.Model.Category>
                    (new Category { CategoryId = categoryId });
                var subcategory = mapper.Map<Subcategory, Services.Model.Subcategory>
                    (new Subcategory { SubcategoryId = subcategoryId });
                var user = mapper.Map<User, Services.Model.User>
                    (new User { Login = loginPass[0], Password = loginPass[1] });

                Service.Add(description, expiryDate, user, category, subcategory);
                return Request.CreateResponse(HttpStatusCode.Created);
            }
            catch (InvalidOperationException exc)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, exc.Message);
            } 
            catch (ArgumentOutOfRangeException exc)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, exc.Message);
            }
        }

        // Post api/lot/delete/
        [HttpPost]
        public HttpResponseMessage Delete(int lotId, [FromBody]string userCredentials)
        {
            var loginPass = userCredentials.Split();
            if (loginPass.Length != 2)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Bad user credentials.");
            }
            try
            {
                var mapper = MapperDTO.Mapper;

                var lot = mapper.Map<Lot, Services.Model.Lot>
                    (new Lot { LotId = lotId });
                var user = mapper.Map<User, Services.Model.User>
                    (new User { Login = loginPass[0], Password = loginPass[1] });
                Service.Delete(lot, user);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (InvalidOperationException exc)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, exc.Message);
            }
        }
    }
}
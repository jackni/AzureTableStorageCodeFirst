using Example.Infrastructure;
using Example.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Example.Controllers
{
    public class ToDoController : ApiController
    {
        [HttpPost]
        [ValidationActionFilter]
        public async Task<IHttpActionResult> AddToDoItem(ToDoItem todoItem)
        {
            try
            {
                using (var context = new ToDoContext())
                {
                    await context.ToDoItems.AddAsync(todoItem);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                throw new ApiException(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public IHttpActionResult GetToDoItems()
        {
            try
            {
                using (var context = new ToDoContext())
                {
                    var result = context.ToDoItems.Find(i=>i.PartitionKey== "<my test partionKey>");
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                throw new ApiException(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}

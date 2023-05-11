using Crud_Operation.CommonLayer.Model;
using Crud_Operation.ServiceLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crud_Operation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    //Here a class is already created
    public class CrudOperationController : ControllerBase
    {

        public readonly ICrudOperationSL _crudOperationSL;

        public CrudOperationController(ICrudOperationSL crudOperationSL)
        {
            _crudOperationSL = crudOperationSL;
        }

        //now we will write crud operations api
        [HttpPost]
        [Route(template: "CreateRecord")]
       public async Task<IActionResult> CreateRecord(CreateRecordRequest request)
        //IActionResult is basically used to OK or bad request etc.

        //ASYNC and AWAIT - Async functions return a promise. This promise state can be either resolved or rejected.
        //                  Await suspends the called function execution until the promise returns a result for that execution.
        {
            CreateRecordResponse response = null;
            try
            {
                response = await _crudOperationSL.CreateRecord(request);
            }

            catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return Ok(response);
        }


        [HttpGet]
        public async Task<IActionResult> ReadResult()
        {
            ReadRecord response = null;
            try
            {
                
            }
            catch(Exception ex)
            {

            }
            return Ok(response);
        }


        //Controller can be connected with service layer with the help of constructor dependency injection. 
        //Service layer can be connected with repository layer with the help of constructor dependency injection.
    }
}

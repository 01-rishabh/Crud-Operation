using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crud_Operation.CommonLayer.Model
{
    public class CreateRecordRequest
    {
        public string Username { get; set; }
        public int Age { get; set; }
    }

   public class CreateRecordResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}

using Crud_Operation.CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crud_Operation.ServiceLayer
{
    //Service layer is used for business logic. Its second name is business layer.
    public interface ICrudOperationSL
    {
        public Task<CreateRecordResponse> CreateRecord(CreateRecordRequest request);
        public Task<ReadRecord> ReadRecord();
    }
}

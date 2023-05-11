using Crud_Operation.CommonLayer.Model;
using Crud_Operation.RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crud_Operation.ServiceLayer
{
    //Service layer is used for business logic. Its second name is business layer.

    //Also we have to call service layer from the repository layer so we have to define dependency injection in service layer.
    public class CrudOperationSL : ICrudOperationSL
    {
        public readonly ICrudOperationRL _crudOperationRL;

        public CrudOperationSL(ICrudOperationRL crudOperationRL)
        {
            _crudOperationRL = crudOperationRL;
        }
        public async Task<CreateRecordResponse> CreateRecord(CreateRecordRequest request)
        {
            return await _crudOperationRL.CreateRecord(request);
        }

        public async Task<ReadRecord> ReadRecord()
        {
            return await _crudOperationRL.ReadRecord();
        }
    }
}

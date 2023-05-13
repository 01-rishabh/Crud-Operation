using Crud_Operation.CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Crud_Operation.RepositoryLayer
{
    public class CrudOperationRL : ICrudOperationRL
    {
        /// <summary>
        /// Connecting database with the app. It is done in the repository layer with the help of construction dependency injection.
        /// Firslty we have to import data of appsetting.json to Repository layer because the connection string is defined in 
        /// appsettings.json
        /// </summary>
        public readonly IConfiguration _configuration;
        public readonly SqlConnection _sqlConnection;

        public CrudOperationRL(IConfiguration configuration)
        {
            _configuration = configuration;
            _sqlConnection = new SqlConnection(_configuration[key: "ConnectionStrings:DBSettingsConnection"]);
        }


        /// <summary>
        /// Post Method is implemented here
        /// </summary>
        public async Task<CreateRecordResponse> CreateRecord(CreateRecordRequest request)
        {
            CreateRecordResponse response = new CreateRecordResponse();
            response.IsSuccess = true;
            response.Message = "Successful";

            try
            {
                string SqlQuery = "Insert Into CrudOperationTable (Username, Age) values(@Username, @Age)";
                using (SqlCommand sqlCommand = new SqlCommand(SqlQuery, _sqlConnection))
                
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    //if i have to use stored procedures instead of sqlquery then in the above statement i would 
                    // have just wrote stored procedure there instead of text.
                    sqlCommand.CommandTimeout = 180;
                    sqlCommand.Parameters.AddWithValue(parameterName: "@Username", request.Username);
                    sqlCommand.Parameters.AddWithValue(parameterName: "@Age", request.Age);
                    _sqlConnection.Open();
                    ///so as we are using async and await then the controller must not go on without first executing this query.
                    int Status = await sqlCommand.ExecuteNonQueryAsync();
                    //if the value of status is more than 0 then it would run successfully.
                    if(Status <= 0)
                    {
                        response.IsSuccess = false;
                        response.Message = "Something went wrong.";
                    }

                }
            }
            catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            finally
            {
                _sqlConnection.Close();
            }

            return response;
        }

        public async Task<ReadRecord> ReadRecord()
        {
            ReadRecord response = new ReadRecord();
            response.IsSuccess = true;
            response.Message = "Successful";
            try
            {
                string SqlQuery = "Select UserName, Age from CrudOperationTable;";
                using(SqlCommand sqlCommand = new SqlCommand(SqlQuery, _sqlConnection))
                {
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandTimeout = 180;
                    _sqlConnection.Open();

                    using(SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync())
                    {
                        if (sqlDataReader.HasRows)
                        {
                            response.readRecordData = new List<ReadRecordData>();
                            while (await sqlDataReader.ReadAsync())
                            {

                                ReadRecordData dbData = new ReadRecordData();
                                dbData.Username = sqlDataReader[name: "Username"] != DBNull.Value ? sqlDataReader[name: "Username"].ToString() : string.Empty;
                                dbData.Age = sqlDataReader[name: "Age"] != DBNull.Value ? Convert.ToInt32 (sqlDataReader[name: "Age"]) : 0;
                                response.readRecordData.Add(dbData);
                            }
                        }
                    }
                }


            }
            catch(Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;

            }
            finally
            {
                _sqlConnection.Close();
            }
            return response;
        }
    }
}

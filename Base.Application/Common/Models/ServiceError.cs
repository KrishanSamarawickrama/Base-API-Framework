using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Application
{
    [Serializable]
    public class ServiceError
    {
        public ServiceError(string message, int code)
        {
            Message = message;
            Code = code;
        }

        public ServiceError(){}

        public string Message { get; }
        public int Code { get; }

        public static ServiceError DefaultError => new ServiceError("An exception occured.", 999);

        public static ServiceError CustomMessage(string errorMessage)
        {
            return new ServiceError(errorMessage, 997);
        }

        public static ServiceError Canceled => new ServiceError("The request canceled successfully!", 994);

        public static ServiceError NotFound => new ServiceError("The specified resource was not found.", 990);

        public static ServiceError ValidationFormat => new ServiceError("Request object format is not true.", 901);

        public static ServiceError Validation => new ServiceError("One or more validation errors occurred.", 900);

    }
}

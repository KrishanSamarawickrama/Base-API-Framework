using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Application
{
    public class ServiceResult<T> : ServiceResult
    {
        public T Data { get; set; }

        public ServiceResult(T data)
        {
            Data = data;
        }

        public ServiceResult(T data, ServiceError error) : base(error)
        {
            Data = data;
        }

        public ServiceResult(ServiceError error) : base(error)
        {

        }

    }

    public class ServiceResult
    {
        bool IsSuccess => this.Error == null;

        public ServiceError Error { get; set; }

        public ServiceResult(ServiceError error)
        {
            if (error == null)
            {
                error = ServiceError.DefaultError;
            }

            Error = error;
        }

        public ServiceResult() { }

        public static ServiceResult Failed(ServiceError error) => new (error);

        public static ServiceResult<T> Failed<T>(ServiceError error) => new (error);

        public static ServiceResult<T> Failed<T>(T data, ServiceError error) => new (data, error);

        public static ServiceResult<T> Success<T>(T data) => new(data);

    }
}

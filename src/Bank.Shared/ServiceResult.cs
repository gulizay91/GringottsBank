using NServiceBus;
using System.Net;

namespace Bank.Shared
{
    public class ServiceResult<T> : IMessage
    {
        public T? Payload { get; protected set; }

        public string ResponseMessage { get; protected set; }

        public HttpStatusCode StatusCode { get; protected set; }

        public bool Success { get; protected set; }

        public ServiceResult(bool success, string responseMessage, T? payload, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            Success = success;
            ResponseMessage = responseMessage;
            Payload = payload;
            StatusCode = statusCode;
        }

        public static ServiceResult<T> ErrorResult(string errorMessage, T? payload, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        {
            return new ServiceResult<T>(false, errorMessage, payload, statusCode);
        }

        public static ServiceResult<T> SuccessResult(T? payload, string? message = null, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new ServiceResult<T>(true, message?? "Transaction completed successfully.", payload, statusCode);
        }
    }
}

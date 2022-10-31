using Core.Entities;

namespace Core.Utilities.Result
{

    public class ApiResponse<T> : IApiResponse<T>
    {
        public Statuses Status { get;}

        public string ResultMessage { get; }

        public int ErrorCode { get; }

        public T Data { get; }

        public ApiResponse(Statuses status, string resultMessage, int errorCode, T data)
        {
            Status = status;
            ResultMessage = resultMessage;
            ErrorCode = errorCode;
            Data = data;
        }
    }
}

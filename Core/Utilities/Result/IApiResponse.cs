using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Result
{
    public enum Statuses
        {
            Success = 1,
            Failed = 2
        }

    public interface IApiResponse<T>
    {
        
        Statuses Status { get; }
        string ResultMessage { get; }
        int ErrorCode { get; }
        T Data { get; }
    }
}

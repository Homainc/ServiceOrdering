using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OrderingService.Domain.Logic.Interfaces;

namespace OrderingService.Web
{
    public static class ResponseExtension
    {
        public static IActionResult ToHttpResponse<T>(this IResponse<T> response)
        {
            switch (response.ResponseResult)
            {
                case ResponseResult.ValidationError:
                    return new ObjectResult(response)
                    {
                        StatusCode = (int) HttpStatusCode.BadRequest
                    };
                case ResponseResult.NotFound:
                    return new ObjectResult(response)
                    {
                        StatusCode = (int) HttpStatusCode.NotFound
                    };
                case ResponseResult.InternalError:
                    return new ObjectResult(response)
                    {
                        StatusCode = (int) HttpStatusCode.InternalServerError
                    };
                default:
                    return new ObjectResult(response)
                    {
                        StatusCode = (int)HttpStatusCode.OK
                    };
            }
        }
    }
}

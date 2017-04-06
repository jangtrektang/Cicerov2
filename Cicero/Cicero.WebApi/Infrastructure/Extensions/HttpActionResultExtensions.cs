using Cicero.WebApi.Infrastructure.Api.ActionResults;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Cicero.WebApi.Infrastructure.Extensions
{
    public static class HttpActionResultExtensions
    {
        public static IHttpActionResult WithPaginationHeader<T>(this IHttpActionResult result, IPagedList<T> pagedList)
        {
            var header = ToJson(new
            {
                Page = pagedList.PageNumber,
                PageCount = pagedList.PageCount,
                PageSize = pagedList.PageSize,
                Count = pagedList.Count,
                TotalCount = pagedList.TotalItemCount
            });

            return new ResponseDecoratingResult(result, response => 
                response.Headers.Add("X-Pagination", header));
        }

        public static IHttpActionResult WithMetaHeader(this IHttpActionResult result, object value)
        {
            return new ResponseDecoratingResult(result, response =>
                response.Headers.Add("X-Meta", ToJson(value)));
        }

        private static string ToJson(object obj)
        {
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.None
            };

            return JsonConvert.SerializeObject(obj, settings);
        }
    }
}
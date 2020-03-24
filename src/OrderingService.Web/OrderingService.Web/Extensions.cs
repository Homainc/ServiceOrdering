using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace OrderingService.Web
{
    public static class ModelStateDictionaryExtension
    {
        public static string GetErrorsString(this ModelStateDictionary model)
        {
            var errors = model.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
            return String.Join(";\n", errors);
        }
    }
}

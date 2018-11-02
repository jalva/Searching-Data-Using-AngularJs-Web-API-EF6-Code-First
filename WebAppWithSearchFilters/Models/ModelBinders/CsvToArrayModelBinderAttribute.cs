using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;

namespace WebAppWithSearchFilters.Models.ModelBinders
{
    public class CsvToArrayModelBinderAttribute : ModelBinderAttribute, IModelBinder
    {
        public CsvToArrayModelBinderAttribute() : base(typeof(CsvToArrayModelBinderAttribute)) { }

        public bool BindModel(HttpActionContext actionContext, ModelBindingContext bindingContext)
        {
            var key = bindingContext.ModelName;
            var val = bindingContext.ValueProvider.GetValue(key);
            if (val == null)
            {
                return false;
            }
            var s = val.AttemptedValue;
            if (s != null)
            {
                var elementType = bindingContext.ModelType.GetElementType();
                var converter = TypeDescriptor.GetConverter(elementType);
                var values = Array.ConvertAll(s.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries),
                    x => converter.ConvertFromString(!Equals(null, x) ? x.Trim() : null));

                var typedValues = Array.CreateInstance(elementType, values.Length);

                values.CopyTo(typedValues, 0);

                bindingContext.Model = typedValues;
            }
            else
            {
                // change this line to null if you prefer nulls to emtpy arrays
                bindingContext.Model = Array.CreateInstance(bindingContext.ModelType.GetElementType(), 0);
            }

            return true;
        }
    }
}
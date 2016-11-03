using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ML.Infrastructure.Repository.Extensions
{
    public static class AttributeExtensions
    {
        public static TValue GetAttributeValue<TAttribute, TValue>(this Type type,
            Func<TAttribute, TValue> valueSelector) where TAttribute : System.Attribute
        {
            var attr = type.GetCustomAttributes(typeof (TAttribute), true).FirstOrDefault() as TAttribute;
            if (attr != null)
            {
                return valueSelector(attr);
            }
            return default(TValue);
        }
    }
}

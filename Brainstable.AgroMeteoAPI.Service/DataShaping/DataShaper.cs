using System.Dynamic;
using System.Reflection;
using Brainstable.AgroMeteoAPI.Contracts;

namespace Brainstable.AgroMeteoAPI.Service.DataShaping
{
    public class DataShaper<T> : IDataShaper<T> where T : class
    {
        public PropertyInfo[] Properties { get; set; }

        public DataShaper()
        {
            Properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }
        
        public IEnumerable<ExpandoObject> ShapeData(IEnumerable<T> entities, string fieldsString)
        {
            var requireProperties = GetRequireProperties(fieldsString);

            return FetchData(entities, requireProperties);
        }

        public ExpandoObject ShapeData(T entity, string fieldsString)
        {
            var requireProperties = GetRequireProperties(fieldsString);

            return FetchDataForEntity(entity, requireProperties);
        }

        private IEnumerable<PropertyInfo> GetRequireProperties(string fieledsString)
        {
            var requriedProperties = new List<PropertyInfo>();

            if (!string.IsNullOrEmpty(fieledsString))
            {
                var fields = fieledsString.Split(',', StringSplitOptions.RemoveEmptyEntries);

                foreach (var field in fields)
                {
                    var property = Properties.FirstOrDefault(pi =>
                        pi.Name.Equals(field.Trim(), StringComparison.InvariantCultureIgnoreCase));

                    if (property == null)
                        continue;

                    requriedProperties.Add(property);
                }
            }
            else
            {
                requriedProperties = Properties.ToList();
            }

            return requriedProperties;  
        }

        private IEnumerable<ExpandoObject> FetchData(IEnumerable<T> entities, IEnumerable<PropertyInfo> requireProperties)
        {
            var shapeData = new List<ExpandoObject>();

            foreach (var entity in entities)
            {
                var shapeObject = FetchDataForEntity(entity, requireProperties);
                shapeData.Add(shapeObject);
            }

            return shapeData;
        }

        private ExpandoObject FetchDataForEntity(T entity, IEnumerable<PropertyInfo> requireProperties)
        {
            var shapedObject = new ExpandoObject();

            foreach (var property in requireProperties)
            {
                var objectPropertyValue = property.GetValue(entity);
                shapedObject.TryAdd(property.Name, objectPropertyValue);
            }

            return shapedObject;
        }
    }
}

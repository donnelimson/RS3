using Codebiz.Domain.Common.Model.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Utilities
{
    public class LookUpHelper
    {
        public static List<LookUpDTO> GenerateLookUp<T>(object model, List<T> data)
        {
            var lookUpDTO = new List<LookUpDTO>();
            var properties = model.GetType().GetProperties()
               .Where(p => ((DisplayNameAttribute)Attribute.GetCustomAttribute(p, typeof(DisplayNameAttribute))) != null);
            var keyProperties = model.GetType().GetProperties()
               .Where(p => ((KeyAttribute)Attribute.GetCustomAttribute(p, typeof(KeyAttribute))) != null);
            var propertyDisplayNames = properties.Select(p => ((DisplayNameAttribute)Attribute.GetCustomAttribute(p, typeof(DisplayNameAttribute))).DisplayName).ToArray();

            var propertyIds = keyProperties.Select(p => p.Name).ToArray();
            var propertyNames = properties.Select(p => p.Name).ToArray();

            for (int j = 0; j < propertyDisplayNames.Length; j++)
            {
                if (propertyDisplayNames[j] == "NAME")
                {
                    for (var i = 0; i <= data.Count - 1; i++)
                    {
                        lookUpDTO.Add(new LookUpDTO
                        {
                            Id = Convert.ToInt32(data[i].GetType().GetProperty(propertyIds[0]).GetValue(data[i], null)),
                            Description = data[i].GetType().GetProperty(propertyNames[j]).GetValue(data[i], null).ToString(),
                            Name = data[i].GetType().GetProperty(propertyNames[j]).GetValue(data[i], null).ToString()
                        });
                    }
                    break;
                }
            }
            return lookUpDTO;

        }
        public static object CreateObjectBy(Type classDTO)
        {
            object theObject = Activator.CreateInstance(classDTO);
            return theObject;
        }
    }
}

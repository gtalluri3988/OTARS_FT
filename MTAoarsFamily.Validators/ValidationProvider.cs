using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Microsoft.Practices.Unity;
using MTAoarsFamily.Utilities.IoC;

namespace MTAoarsFamily.Validators {

    public class ValidationProvider : IValidationProvider {

        IDictionary<Type, IList<dynamic>> validators;

        public ValidationProvider() {
            validators = new Dictionary<Type, IList<dynamic>>();
            Populate();
        }

        public IEnumerable<IValidator<T>> GetValidators<T>(T item) {
            var result = new List<IValidator<T>>();
            var type = typeof(T);
            while (type != null && type.Name != "Object") {
                result.AddRange(SelectValidators<T>(type));
                foreach (var intr in type.GetInterfaces()) {
                    result.AddRange(SelectValidators<T>(intr));
                }
                type = type.BaseType;
            }
            //return result;
            foreach (var validator in result) {
                yield return ObjectContainer.Container.Resolve(validator.GetType()) as IValidator<T>;
            }
        }

        IEnumerable<IValidator<T>> SelectValidators<T>(Type type) {
            IList<dynamic> list;
            if (validators.TryGetValue(type, out list)) {
               
                return list.Cast<IValidator<T>>().AsEnumerable();
            }
            return new List<IValidator<T>>();
        }

        void Populate() {
            var list = Assembly.GetCallingAssembly().GetTypes()
                 .Where(
                         type => type.GetInterfaces().Any(
                                     iType => iType.IsGenericType && iType.GetGenericTypeDefinition() == typeof(IValidator<>)
                                     )
                     );
            foreach (Type type in list) {
                Add(type);
            }
        }

        void Add(Type type) {
            var interfaceType = type.GetInterfaces().Single(
                  iType => iType.IsGenericType
                      && iType.GetGenericTypeDefinition() == typeof(IValidator<>)
                      );
            Type genericType = interfaceType.GetGenericArguments().Single();
            IList<dynamic> list = null;
            if (!validators.TryGetValue(genericType, out list)) {
                list = new List<dynamic>();
                validators.Add(genericType, list);
            }
            (list as List<dynamic>).Add(ObjectContainer.Container.Resolve(type));
        }

    }// class

}// namespace

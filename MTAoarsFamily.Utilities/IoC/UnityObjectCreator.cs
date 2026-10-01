using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;

namespace MTAoarsFamily.Utilities.IoC {

    public class UnityObjectCreator : IObjectCreator {

        public T Create<T>() {
            return ObjectContainer.Container.Resolve<T>();
        }

    }

}

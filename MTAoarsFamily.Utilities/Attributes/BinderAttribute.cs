using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.Utilities.Interfaces;


namespace MTAoarsFamily.Utilities.Attributes {
    public class BinderAttribute : Attribute {
        private IModelBinder binder;
        public BinderAttribute(Type type) {
            try {
                binder = (IModelBinder)Activator.CreateInstance(type);
            } catch {
                throw new Exception("Invalid Binder");
            }
        }

        public IModelBinder GetBinder() {
            return binder;
        }

    }
}

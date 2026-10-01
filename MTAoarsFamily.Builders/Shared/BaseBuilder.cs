using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MTAoarsFamily.Services.Shared;
using MTAoarsFamily.Builders.Interfaces;

namespace MTAoarsFamily.Builders.Shared {
    public class BaseBuilder : IBaseBuilder
    {
        protected static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        BuilderContext context;
        public BaseBuilder()
        {
            context = new BuilderContext();
        }

        public BuilderContext CurrentContext
        {
            get { return context; }
            set { context = value; }
        }

    }// class
}// namespace

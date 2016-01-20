using System;
using System.Web.Mvc;
using StructureMap;
using StructureMap.Configuration.DSL;
using StructureMap.TypeRules;

namespace Yogam.AMC.Infrastructure.Registries
{
    public class ActionFilterRegistry : Registry
    {
        public ActionFilterRegistry(Func<IContainer> containerFactory)
        {
            // Register the StructureMap filter provider
            For<IFilterProvider>().Use(new StructureMapFilterProvider(containerFactory));

            
            //Convention for how StructureMap should perform setter injection into ActionFilters.
            SetAllProperties(x =>
                x.Matching(p =>
                    p.DeclaringType.CanBeCastTo(typeof(ActionFilterAttribute)) &&
                    p.DeclaringType.Namespace.StartsWith("Yogam") &&
                    !p.DeclaringType.IsPrimitive && p.PropertyType != typeof(string)));
        }
    }
}
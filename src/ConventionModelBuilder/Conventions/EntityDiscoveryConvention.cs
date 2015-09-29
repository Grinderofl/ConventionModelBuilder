using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ConventionModelBuilder.Conventions.Options;
using ConventionModelBuilder.Extensions;
using ConventionModelBuilder.Options;
using Microsoft.Data.Entity;

namespace ConventionModelBuilder.Conventions
{
    public class EntityDiscoveryConvention : IModelBuilderConvention
    {
        public EntityDiscoveryConventionOptions Options { get; } = new EntityDiscoveryConventionOptions();
        
        public void Apply(ModelBuilder builder)
        {
            var entities = FindEntities();
            foreach (var entity in entities)
                builder.Entity(entity);
        }

        protected virtual IEnumerable<Type> FindEntities()
        {
            var types = Options.Assemblies.SelectMany(x => x.GetExportedTypes());

            foreach (var criteria in Options.Criterias)
                types = types.Where(x => criteria.IsSatisfiedBy(x.GetTypeInfo()));
            
            return types;
        }
    }
}
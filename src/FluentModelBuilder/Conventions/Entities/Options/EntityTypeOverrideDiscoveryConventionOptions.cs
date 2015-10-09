using System.Collections.Generic;
using FluentModelBuilder.Options;
using FluentModelBuilder.Sources.Assemblies;

namespace FluentModelBuilder.Conventions.Entities.Options
{
    public class EntityTypeOverrideDiscoveryConventionOptions : IAssemblyOptions
    {
        public IList<IAssemblySource> AssemblySources { get; set; } = new List<IAssemblySource>();
    }
}
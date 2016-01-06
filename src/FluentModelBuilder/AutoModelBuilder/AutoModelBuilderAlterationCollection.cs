using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Data.Entity.Metadata.Conventions;

namespace FluentModelBuilder
{
    public class AlterationCollectionBase<T, TAlteration> : IEnumerable<TAlteration> where T : AlterationCollectionBase<T, TAlteration>
    {
        protected readonly List<TAlteration> Alterations = new List<TAlteration>();

        private void Add(Type type)
        {
            Add((TAlteration)Activator.CreateInstance(type));
        }

        public T Add<TAlterationType>() where TAlterationType : TAlteration
        {
            Add(typeof (TAlterationType));
            return (T) this;
        }

        public T Add(TAlteration alteration)
        {
            if(!Alterations.Exists(a => a.GetType() == alteration.GetType() && alteration.GetType() != typeof(EntityTypeOverrideAlteration)))
                Alterations.Add(alteration);
            return (T) this;
        }

        public T AddFromAssembly(Assembly assembly)
        {
            foreach(var type in assembly.GetExportedTypes())
                if (typeof (TAlteration).IsAssignableFrom(type))
                    Add(type);
            return (T) this;
        }

        public T AddFromAssemblyOf<TAssemblyType>()
        {
            return AddFromAssembly(typeof(TAssemblyType).GetTypeInfo().Assembly);
        }

        public IEnumerator<TAlteration> GetEnumerator()
        {
            return Alterations.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        
    }

    public class AutoModelBuilderAlterationCollection : AlterationCollectionBase<AutoModelBuilderAlterationCollection, IAutoModelBuilderAlteration>
    {
        protected internal void Apply(AutoModelBuilder builder)
        {
            foreach (var alteration in Alterations)
                alteration.Alter(builder);
        }
    }

    public class ConventionSetAlterationCollection :
        AlterationCollectionBase<ConventionSetAlterationCollection, IConventionSetAlteration>
    {
        protected internal void Apply(ConventionSet set)
        {
            foreach (var alteration in Alterations)
                alteration.Alter(set);
        }
    }

    
}
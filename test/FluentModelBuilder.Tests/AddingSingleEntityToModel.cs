//using FluentModelBuilder.InMemory;

using System.Linq;
using FluentModelBuilder;
using FluentModelBuilder.InMemory;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Framework.DependencyInjection;
using Xunit;

namespace FluentModelBuilder.Tests
{
    public class AddingSingleEntityToModel : TestBase
    {
        public AddingSingleEntityToModel(ModelFixture fixture) : base(fixture)
        {
        }

        protected override void ConfigureOptions(DbContextOptionsBuilder options)
        {
            options.UseFluentBuilder().Entities(c => c.Add<SingleEntity>()).WithInMemoryDatabase();
            //((IDbContextOptionsBuilderInfrastructure)options).AddOrUpdateExtension(new MyExtension());
            //options.UseInMemoryDatabase();
            //options.UseFluentBuilder().UseModelSource(new InMemoryModelSourceBuilder()); //.AddEntity<SingleEntity>(); //.UsingInMemory();
        }

        [Fact]
        public void AddsSingleEntity()
        {
            Assert.Equal(1, Model.EntityTypes.Count);
        }

        [Fact]
        public void AddsProperties()
        {
            var properties = Model.EntityTypes[0].GetProperties().ToArray();
            Assert.Equal("Id", properties[0].Name);
            Assert.Equal("DateProperty", properties[1].Name);
            Assert.Equal("StringProperty", properties[2].Name);
        }
    }

}
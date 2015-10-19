﻿using System;
using System.Reflection;
using FluentModelBuilder.Core.Contributors;
using FluentModelBuilder.Core.Criteria;

namespace FluentModelBuilder.Extensions
{
    public static class DiscoveryContributorExtensions
    {
        public static T BaseType<T>(this T contributor, Type type) where T : DiscoveryContributorBase<T>
        {
            return
                contributor.NotAbstract()
                    .WithCriterion<BaseTypeCriterion>(c => c.AddType(type.GetTypeInfo()));
        }

        public static T NotAbstract<T>(this T contributor) where T : DiscoveryContributorBase<T>
        {
            return contributor.WithCriterion<NonAbstractCriterion>();
        }

        public static T Namespace<T>(this T contributor,
            Func<string, bool> namespaceAction) where T : DiscoveryContributorBase<T>
        {
            return contributor.AddCriterion(new ExpressionCriterion(t => namespaceAction(t.Namespace)));
        }

        public static T When<T>(this T contributor,
            Func<TypeInfo, bool> typeExpression) where T : DiscoveryContributorBase<T>
        {
            return contributor.AddCriterion(new ExpressionCriterion(typeExpression));
        }
    }
}
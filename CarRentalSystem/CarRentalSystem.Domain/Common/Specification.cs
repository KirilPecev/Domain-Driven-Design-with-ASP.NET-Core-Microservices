﻿namespace CarRentalSystem.Domain.Common
{
    using System.Collections.Concurrent;
    using System.Linq.Expressions;

    public abstract class Specification<T>
    {
        private static readonly ConcurrentDictionary<string, Func<T, bool>> DelegateCache
            = new ConcurrentDictionary<string, Func<T, bool>>();

        private readonly List<string> cacheKey;

        protected Specification()
            => cacheKey = new List<string> { GetType().Name };

        protected virtual bool Include => true;

        public virtual bool IsSatisfiedBy(T value)
        {
            if (!Include)
            {
                return true;
            }

            Func<T, bool> func = DelegateCache.GetOrAdd(
                string.Join(string.Empty, cacheKey),
                _ => ToExpression().Compile());

            return func(value);
        }

        public Specification<T> And(Specification<T> specification)
        {
            if (!specification.Include)
            {
                return this;
            }

            cacheKey.Add($"{nameof(this.And)}{specification.GetType()}");

            return new BinarySpecification(this, specification, true);
        }

        public Specification<T> Or(Specification<T> specification)
        {
            if (!specification.Include)
            {
                return this;
            }

            cacheKey.Add($"{nameof(this.Or)}{specification.GetType()}");

            return new BinarySpecification(this, specification, false);
        }

        public abstract Expression<Func<T, bool>> ToExpression();

        public static implicit operator Expression<Func<T, bool>>(Specification<T> specification)
            => specification.Include
                ? specification.ToExpression()
                : value => true;

        private class BinarySpecification : Specification<T>
        {
            private readonly Specification<T> left;
            private readonly Specification<T> right;
            private readonly bool andOperator;

            public BinarySpecification(Specification<T> left, Specification<T> right, bool andOperator)
            {
                this.right = right;
                this.left = left;
                this.andOperator = andOperator;
            }

            public override Expression<Func<T, bool>> ToExpression()
            {
                Expression<Func<T, bool>> leftExpression = left;
                Expression<Func<T, bool>> rightExpression = right;

                Expression body = andOperator
                    ? Expression.AndAlso(leftExpression.Body, rightExpression.Body)
                    : Expression.OrElse(leftExpression.Body, rightExpression.Body);

                ParameterExpression parameter = Expression.Parameter(typeof(T));
                body = (BinaryExpression)new ParameterReplacer(parameter).Visit(body);

                body = body ?? throw new InvalidOperationException("Binary expression cannot be null.");

                return Expression.Lambda<Func<T, bool>>(body, parameter);
            }
        }

        private class ParameterReplacer : ExpressionVisitor
        {
            private readonly ParameterExpression parameter;

            protected override Expression VisitParameter(ParameterExpression node)
                => base.VisitParameter(parameter);

            internal ParameterReplacer(ParameterExpression parameter)
                => this.parameter = parameter;
        }
    }
}

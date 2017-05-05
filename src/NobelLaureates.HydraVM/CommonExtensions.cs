using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace NobelLaureates.HydraVM
{
    public static class CommonExtensions
    {
        public static string PropertyName<T>(this T vm, Expression<Func<T, object>> expression) where T : INotifyPropertyChanged
        {
            if (vm == null) throw new ArgumentNullException(nameof(vm));
            if (expression == null) throw new ArgumentNullException(nameof(expression));

            var body = expression.Body as MemberExpression ??
                ((UnaryExpression)expression.Body).Operand as MemberExpression;

            return body != null ? body.Member.Name : string.Empty;
        }
    }
}

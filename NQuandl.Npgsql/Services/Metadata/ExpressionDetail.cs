using System;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;

namespace NQuandl.Npgsql.Services.Metadata
{
    //from http://codereview.stackexchange.com/questions/33152/function-that-builds-dictionary-based-on-lambda-params
    public class ExpressionDetail
    {
        private ExpressionDetail()
        {

        }

        public MemberInfo MemberInfo { get; private set; }

        public LambdaExpression Expression { get; private set; }

        //By figuring out MemberInfo from the Expression, 
        //we can now have all these read-only properties to get expression detail.
        public string Name { get { return MemberInfo.Name; } }

        public Type DeclaringType { get { return MemberInfo.DeclaringType; } }

        public string FullName { get { return DeclaringType.FullName + "." + Name; } }

        //Depending on performance requirement, you may want to use Lazy<T> to calculate this value
        //only once. 
        public Delegate Delegate { get { return Expression.Compile(); } }

        //We are expecting a lambda expression which should either point to a method or a property access.
        //To get body, we have to handle the case of expression being UnaryExpression
        //To learn more: http://stackoverflow.com/questions/3567857/why-are-some-object-properties-unaryexpression-and-others-memberexpression
        private static Expression GetBody(LambdaExpression expression)
        {
            //We don't validate arguments here only because it's a private method.

            var unaryExpression = expression.Body as UnaryExpression;
            return unaryExpression != null ? unaryExpression.Operand : expression.Body;
        }

        //In your original method, you returned the name.
        //However, it could be even more useful to get the MemberInfo and store it.
        //Now we will have access to Name as well as the Type in which the property/method is declared.

        // There are lots of edge cases here. 
        // Refer to: http://stackoverflow.com/questions/671968/retrieving-property-name-from-lambda-expression and update this method to handle the edge cases that you care about.
        private static MemberInfo GetMemberInfo(Expression expression)
        {
            //We don't validate arguments here only because it's a private method.

            var memberExpression = expression as MemberExpression;

            if (memberExpression != null)
                return memberExpression.Member;

            var methodCallExpression = expression as MethodCallExpression;

            if (methodCallExpression != null)
                return methodCallExpression.Method;

            return null;
        }

        public static ExpressionDetail Create(LambdaExpression expression)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");

            var body = GetBody(expression);

            var memberInfo = GetMemberInfo(body);

            if (memberInfo == null)
                throw new InvalidExpressionException(
                    string.Format("The expression '{0}' is invalid. You must supply an expression that references a property or a function.",
                        expression.Body));

            return new ExpressionDetail { MemberInfo = memberInfo, Expression = expression };
        }

    }
}
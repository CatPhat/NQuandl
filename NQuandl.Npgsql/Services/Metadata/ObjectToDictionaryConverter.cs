using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;

namespace NQuandl.Npgsql.Services.Metadata
{
    // Why would you name a class like 'DictionaryThing'? It doesn't explain anything about what the class does. 
    // Let's give it a more descriptive 'ObjectToDictionaryConverter' name. You may prefer some other name but 
    // make sure that you put same thought in to naming your class as the code which goes inside the class.

    // Next, the original code was doing two things
    // 1. Inspecting the expression to derive a key
    // 2. Create the dictionary from the input object (while also catching the expression details)

    // Following SRP, let's separate it out in two classes so that each class is responsible for doing one thing.
    // This also means that you can use the Expression inspection code somewhere else if you choose to.
    public class ObjectToDictionaryConverter
    {
        //Always spell out the variable name in its full. (expCache -> expressionCache)
        private readonly ConcurrentDictionary<string, Delegate> _expressionCache = new ConcurrentDictionary<string, Delegate>();

        public Dictionary<string, object> GetDictionary<T>(T obj, params Expression<Func<T, object>>[] expressions)
        {
            //Always validate your arguments. 
            if (ReferenceEquals(obj, null))
                throw new ArgumentNullException("obj");

            if (expressions.Length == 0)
                throw new ArgumentException("You must specify at least one expression.");

            foreach (var expression in expressions)
            {
                if (expression == null)
                    throw new ArgumentException("You can not specify NULL expression.");
            }

            var result = new Dictionary<string, object>();

            foreach (var expression in expressions)
            {
                //The purpose of ExpressionDetail is to inspect our expression 
                var expressionDetail = ExpressionDetail.Create(expression);

                //A lambda expression can be a valid expression referring to a property or function.
                //But for our need, we will need to compile this expression to a delegate and run on object of Type T, let's make
                //sure that expression refers to the correct type.

                //IMPORTANT: We should not put this check in ExpressionDetail class. It is not
                //the responsibility of ExpressionDetail to enforce this type constraint. 
                //This type constraint is only needed for ObjectToDictionaryConverter class. 
                if (expressionDetail.DeclaringType != typeof(T))
                    throw new InvalidExpressionException("Expression " + expression.Body + " is invalid. Expression Property/Member Type " + expressionDetail.DeclaringType.FullName + ", expecting Type: " + typeof(T).FullName);

                //expressionDetail has properties like 'Name', 'FullName', 'Delegate' 
                var func = (Func<T, object>)_expressionCache.GetOrAdd(expressionDetail.FullName, expressionDetail.Delegate);

                result[expressionDetail.Name] = func(obj);
            }

            return result;
        }
    }
}
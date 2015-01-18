using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enexure.Sql.Dynamic.Queries
{
	public class Expression : IExpression
	{
		public static IExpression Const(object value)
		{
			return new Constant(value);
		}

		public static Like Like(IExpression expressionLeft, object value)
		{
			return new Like(expressionLeft, new Constant(value));
		}

		public static Like Like(IExpression expressionLeft, IExpression expressionRight)
		{
			return new Like(expressionLeft, expressionRight);
		}

		public static Equal Eq(IExpression expressionLeft, object value)
		{
			return new Equal(expressionLeft, new Constant(value));
		}

		public static Equal Eq(IExpression expressionLeft, IExpression expressionRight)
		{
			return new Equal(expressionLeft, expressionRight);
		}

		public static NotEqual Ne(IExpression expressionLeft, object value)
		{
			return new NotEqual(expressionLeft, new Constant(value));
		}

		public static NotEqual Ne(IExpression expressionLeft, IExpression expressionRight)
		{
			return new NotEqual(expressionLeft, expressionRight);
		}

		public static LessThan Lt(IExpression expressionLeft, object value)
		{
			return new LessThan(expressionLeft, new Constant(value));
		}

		public static LessThan Lt(IExpression expressionLeft, IExpression expressionRight)
		{
			return new LessThan(expressionLeft, expressionRight);
		}

		public static LessThanOrEqual Le(IExpression expressionLeft, object value)
		{
			return new LessThanOrEqual(expressionLeft, new Constant(value));
		}

		public static LessThanOrEqual Le(IExpression expressionLeft, IExpression expressionRight)
		{
			return new LessThanOrEqual(expressionLeft, expressionRight);
		}

		public static GreaterThan Gt(IExpression expressionLeft, object value)
		{
			return new GreaterThan(expressionLeft, new Constant(value));
		}

		public static GreaterThan Gt(IExpression expressionLeft, IExpression expressionRight)
		{
			return new GreaterThan(expressionLeft, expressionRight);
		}

		public static GreaterThanOrEqual Ge(IExpression expressionLeft, object value)
		{
			return new GreaterThanOrEqual(expressionLeft, new Constant(value));
		}

		public static GreaterThanOrEqual Ge(IExpression expressionLeft, IExpression expressionRight)
		{
			return new GreaterThanOrEqual(expressionLeft, expressionRight);
		}

		public static InSubQuery In(IExpression expression, SubQuery subQuery)
		{
			return new InSubQuery(expression, subQuery);
		}

		public static InValues In(IExpression expression, IEnumerable<object> values)
		{
			return new InValues(expression, values);
		}

		public static Concatenation Concat(IEnumerable<IExpression> expressions)
		{
			return new Concatenation(expressions);
		}

		public static Concatenation Concat(params IExpression[] expressions)
		{
			return new Concatenation(expressions);
		}

		public static Conjunction And(params IBoolean[] expressions)
		{
			return new Conjunction(expressions);
		}

		public static Disjunction Or(params IBoolean[] expressions)
		{
			return new Disjunction(expressions);
		}

		public static Coalesce Coalesce(params IExpression[] expressions)
		{
			return new Coalesce(expressions);
		}

		public static IBoolean Not(IBoolean expression)
		{
			return new Not(expression);
		}

		public static IBoolean IsNull(IExpression expression)
		{
			return new IsNull(expression);
		}

		public static IBoolean Between(IExpression testxpression, IExpression leftExpression, IExpression rightExpression)
		{
			return new Between(testxpression, leftExpression, rightExpression);
		}

		public static IBoolean Between(IExpression testxpression, object leftValue, object rightValue)
		{
			return new Between(testxpression, new Constant(leftValue), new Constant(rightValue));
		}

		public static Conjunction Conjunction()
		{
			return Queries.Conjunction.Empty;
		}

		public static Disjunction Disjunction()
		{
			return Queries.Disjunction.Empty;
		}
	
		// Non static

		public Count Count()
		{
			return new Count(this);
		}

		public Sum Sum()
		{
			return new Sum(this);
		}

		public Select As(string alias)
		{
			return new Select(this, alias);
		}

		public Select AsSelf()
		{
			return new Select(this, string.Empty);
		}

		public Function WithFunc(string function)
		{
			return new Function(function, this);
		}

		public IExpression CastTo(string type)
		{
			return new Cast(this, type);
		}
	}
}

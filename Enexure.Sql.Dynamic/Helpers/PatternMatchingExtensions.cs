using System;

namespace Enexure.Sql.Dynamic.Helpers
{
	public struct Match
	{
		public object Value;
		public bool Success;
	}

	public static class PatternMatchingExtensions
	{
		public static PatternMatching<T> Match<T>(this T instance)
		{
			return new PatternMatching<T>(() => new Match {Value = instance, Success = false});
		}

		public static PatternMatching<T> Default<T>(this PatternMatching<T> x, Func<object, object> f)
		{
			return x.With<T>(b => true, b => f(b));
		}
	}

	public class PatternMatching<T>
	{
		private readonly Func<Match> applyFunction;

		public PatternMatching(Func<Match> applyFunction)
		{
			this.applyFunction = applyFunction;
		}

		public PatternMatching<T> With<TType>(Func<TType, object> f)
		{
			return With(_ => true, f);
		}

		public PatternMatching<T> With<TType>(Func<TType, bool> p, Func<TType, object> f)
		{
			return new PatternMatching<T>(
				() => {
					var obj = applyFunction();
					return obj.Success
						? obj
						: (obj.Value is TType && p((TType) obj.Value)
							? Success(f, obj)
							: Fail(obj));
				});
		}

		private static Match Fail(Match obj)
		{
			return new Match {
				Value = obj.Value,
				Success = false
			};
		}

		private static Match Success<TType>(Func<TType, object> f, Match obj)
		{
			return new Match {
				Value = f((TType)obj.Value),
				Success = true
			};
		}

		public PatternMatching<T> Any(Func<object> f)
		{
			return new PatternMatching<T>(
				() => {
					var obj = applyFunction();
					return obj.Success ? obj : new Match {Value = f(), Success = true};
				});
		}

		public TResult Return<TResult>()
		{
			var ret = applyFunction();
			if (ret.Success && ret.Value is TResult) {
				return (TResult) ret.Value;
			}
			throw new MatchFailureException(String.Format("Failed to match: {0}", ret.Value.GetType()));
		}
	}

	public class MatchFailureException : Exception
	{
		public MatchFailureException(string message)
			: base(message)
		{
		}
	}
}

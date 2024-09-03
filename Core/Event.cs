using System.Linq.Expressions;

namespace Core;

class Event
{
	private static Dictionary<string, List<LambdaExpression>> Events;

	public static void AddEvent(string name)
	{
		Events[name] = [];
	}

	public static void AddListener(string eventName, LambdaExpression lambda)
	{
		Events[eventName].Add(lambda);
	}
}
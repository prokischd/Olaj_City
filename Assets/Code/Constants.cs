
using System.Collections.Generic;

public static class Constants
{
	public static IEnumerable<T> SingleItemAsEnumerable<T>(this T item)
	{
		yield return item;
	}
}

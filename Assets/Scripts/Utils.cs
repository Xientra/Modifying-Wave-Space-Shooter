using System.Collections.Generic;

public static class Utils
{
	// Return a random item from an array
	public static T Random<T>(this T[] arr)
	{
		return arr[UnityEngine.Random.Range(0, arr.Length)];
	}

	// Return a random item from a list
	public static T Random<T>(this List<T> list)
	{
		return list[UnityEngine.Random.Range(0, list.Count)];
	}
}

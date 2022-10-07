using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Stuff
{
	static public T GetRandom<T>(this T[] array)
	{
		if (array.Length == 0) return default;
		return array[Random.Range(0, array.Length)];
	}
	static public T GetRandom<T>(this T[] array, int start)
	{
		if (array.Length == 0) return default;
		if (array.Length <= start) return default;
		return array[Random.Range(start, array.Length)];
	}
	static public bool RandomBool(float chance)
	{
		return Random.value <= chance;
	}
}

public enum Directions
{
	Left, Right, Up, Down
}
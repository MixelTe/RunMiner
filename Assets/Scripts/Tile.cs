using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
	public int Strength;
	public Crack CrackPrefab;
	
	private Crack _crack;
	private int _damage;

	public bool Break()
	{
		if (!_crack) _crack = Instantiate(CrackPrefab, transform);
		_crack.Enlarge(1f / Strength);
		_damage++;
		return Strength - _damage <= 0;
	}
}

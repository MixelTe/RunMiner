using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Tile : MonoBehaviour
{
	public int Strength;
	public Crack CrackPrefab;
	public GameObject GoldOrePrefab;
	public GameObject DiamondOrePrefab;

	[HideInInspector] public bool HasOre = false;
	
	private Crack _crack;
	private int _damage;


	public void AddOre(bool diamond)
	{
		Instantiate(diamond ? DiamondOrePrefab : GoldOrePrefab, transform);
		HasOre = true;
	}

	public bool Break()
	{
		if (!_crack) _crack = Instantiate(CrackPrefab, transform);
		_crack.Enlarge(1f / Strength);
		_damage++;
		if (Strength - _damage <= 0)
		{
			Destroy(gameObject);
			return true;
		}
		return false;
	}
}

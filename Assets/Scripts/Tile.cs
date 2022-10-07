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
	[HideInInspector] public bool DiamondOre = false;

	private Crack _crack;
	private int _damage;


	public void AddOre(bool diamond)
	{
		Instantiate(diamond ? DiamondOrePrefab : GoldOrePrefab, transform);
		HasOre = true;
		DiamondOre = diamond;
	}

	public bool Break(int strength = 1)
	{
		if (!_crack) _crack = Instantiate(CrackPrefab, transform);
		_crack.Enlarge(1f / Strength * strength);
		_damage += strength;
		if (Strength - _damage <= 0)
		{
			if (HasOre)
			{
				if (DiamondOre) GameManager.Instance.GoldOreBroken();
				else GameManager.Instance.GoldOreBroken();
			}
			Destroy(gameObject);
			return true;
		}
		return false;
	}
}

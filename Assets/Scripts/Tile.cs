using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Tile : MonoBehaviour
{
	public int Strength;
	public Crack CrackPrefab;
	public bool Broken = false;
	
	private Crack _crack;
	private int _damage;
	private SpriteRenderer _renderer;

	private void Start()
	{
		_renderer = GetComponent<SpriteRenderer>();
	}

	public bool Break()
	{
		if (!_crack) _crack = Instantiate(CrackPrefab, transform);
		_crack.Enlarge(1f / Strength);
		_damage++;
		if (Strength - _damage <= 0)
		{
			Destroy(_crack.gameObject);
			_renderer.enabled = false;
			Broken = true;
		}
		return Broken;
	}
}

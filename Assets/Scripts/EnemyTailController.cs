using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTailController : MonoBehaviour
{
    public EnemyTail TailPrefab;
	public int Lenght;
    
    private EnemyTail[] _tail;

	private void Start()
	{
		_tail = new EnemyTail[Lenght];
	}

	public void Shift()
	{
		foreach (var tail in _tail)
		{
			if (tail) tail.Shift();
		}
	}
	public void Move(Vector2 pos, float speed)
	{
		for (int i = _tail.Length - 1; i >= 0; i--)
		{
			var tail = _tail[i];
			if (tail)
			{
				if (i > 0)
				{
					var tailPrev = _tail[i - 1];
					tail.SetNextPoin(tailPrev.transform.position);
				}
				else
				{
					tail.SetNextPoin(pos);
				}
			}
			else
			{
				if (i > 0)
				{
					var tailPrev = _tail[i - 1];
					if (tailPrev)
					{
						tail = Instantiate(TailPrefab, tailPrev.transform.position, Quaternion.identity);
						tail.I = i;
						_tail[i] = tail;
						tail.Speed = speed;
					}
				}
				else
				{
					tail = Instantiate(TailPrefab, pos, Quaternion.identity);
					tail.I = i;
					_tail[i] = tail;
					tail.Speed = speed;
				}
			}
		}
	}
}

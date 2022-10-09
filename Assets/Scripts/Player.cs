using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public Vector2Int Pos;
    public int Strength = 1;
    [HideInInspector] public UnityEvent OnMove;

    private TileMap _map;
    private Vector2 _movement;

    void Update()
    {
        transform.position = Pos + _map.Shift + _movement;
    }

    public void Setup(Vector2Int pos, TileMap map)
	{
        Pos = pos;
        _map = map;
        transform.position = pos + map.Shift;
    }
    public void Shift()
    {
        Pos.y += 1;
    }


    public void Move(Directions dir)
	{
        Vector2Int newPos = Pos;
        if (dir == Directions.Up) newPos += new Vector2Int(0, 1);
        else if (dir == Directions.Down) newPos += new Vector2Int(0, -1);
        else if (dir == Directions.Left) newPos += new Vector2Int(-1, 0);
        else if (dir == Directions.Right) newPos += new Vector2Int(1, 0);

        if (_map.Map.OutOfBounds(newPos)) return;

        var tile = _map.Map[newPos];
        if (!tile || tile.Break(Strength))
		{
            Pos = newPos;
            OnMove.Invoke();
        }
	}
}

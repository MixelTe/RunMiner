using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public Vector2Int Pos;
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

        if (newPos.x < 0 || newPos.x > _map.Map.Width ||
            newPos.y < 0 || newPos.y > _map.Map.Height) return;

        var tile = _map.Map[newPos];
        if (!tile || tile.Break())
		{
            Pos = newPos;
        }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public enum Directions
    {
        Left, Right, Up, Down
	}
    [HideInInspector] public Vector2Int Pos;
    private TileMap _map;
    private Vector2 _movement;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
        if (!tile)
		{
            Pos = newPos;
            return;
        }
        if (tile.Break())
		{
            _map.Map.DestroyTile(newPos);
            Pos = newPos;
        }
	}
}

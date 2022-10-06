using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour
{
    public SpriteRenderer MineTop;
    public Tile[] TilePrefabs;
    public Vector2 Shift;

    [HideInInspector] public GameMap Map;
    private int _layer = 0;


    public void GenerateMap(int width, int height, int top)
    {
        Map = new GameMap(width, height);
        _layer = top;

        for (int y = 0; y < top; y++)
		{
            for (int x = 0; x < Map.Width; x++)
			{
				var i = y == top - 1 ? 0 : Random.Range(1, TilePrefabs.Length);
                CreateTile(x, y, TilePrefabs[i]);
			}
        }

        MineTop.transform.position = new Vector2(0, height - 1.375f);
        MineTop.size = new Vector2(width, height - _layer);
    }

    public void Move()
    {
        Map.Move();
        _layer++;
        for (int y = 0; y < Map.Height; y++)
        {
            for (int x = 0; x < Map.Width; x++)
            {
                var tile = Map[x, y];
                if (!tile) continue;
                tile.transform.position = tile.transform.position + new Vector3(0, 1);
			}
        }

        for (int x = 0; x < Map.Width; x++)
        {
            CreateTile(x, 0, TilePrefabs.GetRandom(1));
        }

        if (MineTop)
		{
            var h = Map.Height - _layer;
            if (h > 0) 
            { 
                MineTop.size = new Vector2(Map.Width, h); 
            }
            else
            {
                Destroy(MineTop.gameObject);
                MineTop = null;
            }
		}
    }

    private void CreateTile(int x, int y, Tile tilePref)
    {
        Vector2 pos = new(x + Shift.x, y + Shift.y);
        var tile = Instantiate(tilePref, pos, Quaternion.identity, transform);
        Map[x, y] = tile;
    }


    public class GameMap
	{
        public readonly int Width;
        public readonly int Height;

        private Tile[,] _tiles;
        private int _indexOffset = 0;

        public GameMap(int width, int height)
		{
            Width = width;
            Height = height;
            _tiles = new Tile[height, width];
        }

        public void Move()
		{
            for (int x = 0; x < Width; x++)
            {
                DestroyTile(x, Height - 1);
            }
            _indexOffset = (_indexOffset - 1 + Height) % Height;
        }

        public void DestroyTile(int x, int y)
		{
            var tile = this[x, y];
            if (!tile) return;
            Destroy(this[x, y].gameObject);
            this[x, y] = null;
        }
        public void DestroyTile(Vector2Int pos)
        {
            DestroyTile(pos.x, pos.y);
        }

        public Tile this[int x, int y]
        {
            get
            {
                return _tiles[(y + _indexOffset) % Height, x];
            }
            set
            {
                _tiles[(y + _indexOffset) % Height, x] = value;
            }
        }
        public Tile this[Vector2Int pos]
        {
            get
            {
                return this[pos.x, pos.y];
            }
            set
            {
                this[pos.x, pos.y] = value;
            }
        }
    }
}

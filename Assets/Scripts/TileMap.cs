using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour
{
    public int Width;
    public Tile[] TilePrefabs;
    public Vector2 Shift;

    private GameMap _map;
    private int _layerPast = 0;
    private int _layer;

    void Start()
    {

    }

    void Update()
    {

    }

    public void GenerateMap(int height, int top)
    {
        _map = new GameMap(Width, height);
        _layer = top;

        for (int y = 0; y < top; y++)
		{
			for (int x = 0; x < _map.Width; x++)
			{
				var i = y == top - 1 ? 0 : Random.Range(1, TilePrefabs.Length);
                CreateTile(x, y, TilePrefabs[i]);
			}
		}
	}

    public void Move()
    {
        _map.Move();
        _layer += 1;
        _layerPast += 1;
        for (int y = 0; y < _map.Height; y++)
        {
            for (int x = 0; x < _map.Width; x++)
            {
                var tile = _map[x, y];
                if (!tile) continue;
                tile.transform.position = tile.transform.position + new Vector3(0, 1);
			}
        }

        for (int x = 0; x < Width; x++)
        {
            CreateTile(x, 0, TilePrefabs.GetRandom(1));
        }
    }

    public void CreateTile(int x, int y, Tile tilePref)
    {
        Vector2 pos = new(x + Shift.x, y + Shift.y);
        var tile = Instantiate(tilePref, pos, Quaternion.identity, transform);
        _map[x, y] = tile;
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
    }
}

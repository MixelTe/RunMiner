using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour
{
    public SpriteRenderer MineTop;
    public Vector2 Shift;
    public int LayerHeight;
    [Range(0, 0.2f)] public float OreChance;
    [Range(0, 0.3f)] public float DiamondOreChance;
    public Tile[] TilePrefabs;

    [HideInInspector] public GameMap Map;
    private int _layer = 0;


    public void GenerateMap(int width, int height, int top)
    {
        Map = new GameMap(width, height);
        _layer = 0;

        for (int y = top - 1; y >= 0; y--)
		{
            GenerateLayer(y);
        }

        MineTop.transform.position = new Vector2(0, height - 1.375f);
        MineTop.size = new Vector2(width, height - _layer);
    }

    public void Move()
    {
        Map.Move();
        for (int y = 0; y < Map.Height; y++)
        {
            for (int x = 0; x < Map.Width; x++)
            {
                var tile = Map[x, y];
                if (!tile) continue;
                tile.transform.position = tile.transform.position + new Vector3(0, 1);
			}
        }

        GenerateLayer();

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

    private Tile CreateTile(int x, int y, Tile tilePref)
    {
        Vector2 pos = new(x + Shift.x, y + Shift.y);
        var tile = Instantiate(tilePref, pos, Quaternion.identity, transform);
        Map[x, y] = tile;
        return tile;
    }

    private void GenerateLayer(int y = 0)
	{
        _layer++;
        if (_layer == 1)
        {
			for (int x = 0; x < Map.Width; x++) 
                CreateTile(x, y, TilePrefabs[0]);
            return;
        }
        var layer = Mathf.FloorToInt(_layer / LayerHeight);
        var nextLayerTileChance = (_layer - layer * LayerHeight - 1) / (float)LayerHeight;

        for (int x = 0; x < Map.Width; x++)
		{
            var nextLayer = Random.value < nextLayerTileChance ? 1 : 0;
            var i = layer + 1 + nextLayer;
            i = Mathf.Min(i, TilePrefabs.Length - 1);

            var tile = CreateTile(x, y, TilePrefabs[i]);
         
            if (Stuff.RandomBool(OreChance))
                tile.AddOre(Stuff.RandomBool(DiamondOreChance));
		}
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

        public bool OutOfBounds(int x, int y)
		{
            if (x < 0) return true;
            if (y < 0) return true;
            if (x >= Width) return true;
            if (y >= Height) return true;
            return false;
        }
        public bool OutOfBounds(Vector2Int pos)
        {
            return OutOfBounds(pos.x, pos.y);
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TileMap TileMap;
    public CameraConstantWidth Camera;
    public Player PlayerPrefab;
    public Vector2 SpawnPos;
    public float MoveSpeed;

    private Player _player;
    private float _movement = 0;
    private Vector2 _cameraStartPos;

    void Start()
    {
        _player = Instantiate(PlayerPrefab, SpawnPos, Quaternion.identity);
        _cameraStartPos = Camera.BottomLeft;

        var tileSizePixels = Camera.cam.scaledPixelWidth / TileMap.Width;
        var h = Camera.cam.scaledPixelHeight / tileSizePixels + 2;
        TileMap.GenerateMap(h, Mathf.FloorToInt(SpawnPos.y) + 1);
    }

    // Update is called once per frame
    void Update()
    {
        _movement += MoveSpeed * Time.deltaTime;
        if (_movement >= 1)
        {
            _movement -= 1;
            TileMap.Move();
            _player.transform.position = _player.transform.position + new Vector3(0, 1);
        }
        Camera.BottomLeft = _cameraStartPos - new Vector2(0, _movement);
    }

    public void MovePlayer(string dirs)
    {
        var dir = Player.Directions.Up;
        if (dirs == "down") dir = Player.Directions.Down;
        if (dirs == "left") dir = Player.Directions.Left;
        if (dirs == "right") dir = Player.Directions.Right;
        _player.Move(dir);
    }
}

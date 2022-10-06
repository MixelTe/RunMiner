using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TileMap TileMap;
    public CameraController MainCamera;
    public Player PlayerPrefab;
    public Vector2Int SpawnPos;
    public float MoveSpeed;
    public int MapWidth;

    private Player _player;
    private float _movement = 0;
    private Vector2 _cameraStartPos;
    public bool Running = true;

    void Start()
    {
        _player = Instantiate(PlayerPrefab);
        _player.Setup(SpawnPos, TileMap);
        _cameraStartPos = MainCamera.BottomLeft;
        MainCamera.Width = MapWidth;
        MainCamera.Aplly();

        TileMap.GenerateMap(MapWidth, MainCamera.Height + 1, SpawnPos.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (!Running) return;

        _movement += MoveSpeed * Time.deltaTime;
        if (_movement >= 1)
        {
            _movement -= 1;
            TileMap.Move();
            _player.Shift();
            if (_player.Pos.y >= TileMap.Map.Height)
			{
                Running = false;
			}
        }
        MainCamera.BottomLeft = _cameraStartPos - new Vector2(0, _movement);

        if (Input.GetKeyDown(KeyCode.W)) _player.Move(Player.Directions.Up);
        if (Input.GetKeyDown(KeyCode.S)) _player.Move(Player.Directions.Down);
        if (Input.GetKeyDown(KeyCode.A)) _player.Move(Player.Directions.Left);
        if (Input.GetKeyDown(KeyCode.D)) _player.Move(Player.Directions.Right);
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

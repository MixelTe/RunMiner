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

        if (Input.GetKeyDown(KeyCode.W)) _player.Move(Directions.Up);
        if (Input.GetKeyDown(KeyCode.S)) _player.Move(Directions.Down);
        if (Input.GetKeyDown(KeyCode.A)) _player.Move(Directions.Left);
        if (Input.GetKeyDown(KeyCode.D)) _player.Move(Directions.Right);
    }

    public void MovePlayer(Directions dir)
    {
        _player.Move(dir);
    }
}

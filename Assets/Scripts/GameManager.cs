using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public CameraController MainCamera;
    public TileMap TileMap;
    public int MapWidth;
    public Player PlayerPrefab;
    public Vector2Int SpawnPos;
    public float MoveSpeed;
    public Enemy EnemyPrefab;
    public int EnemyOffset;
    public Slider ProgressionBar;
    public float UpgradeCost;
    [Range(1, 1.5f)] public float UpgradeCostMultiplier;

    public bool Running = true;

    private Player _player;
    private Enemy _enemy;
    private float _movement = 0;
    private Vector2 _cameraStartPos;

	private void Awake()
	{
		if (Instance == null) Instance = this;
        else if (Instance != this) Destroy(gameObject);
    }

	void Start()
    {
        _player = Instantiate(PlayerPrefab);
        _player.Setup(SpawnPos, TileMap);

        _cameraStartPos = MainCamera.BottomLeft;
        MainCamera.Width = MapWidth;
        MainCamera.Aplly();

        ProgressionBar.minValue = 0;
        ProgressionBar.maxValue = UpgradeCost;
        ProgressionBar.value = 0;

        TileMap.GenerateMap(MapWidth, MainCamera.Height + 1, SpawnPos.y);

        _enemy = Instantiate(EnemyPrefab);
        _enemy.Setup(SpawnPos + new Vector2Int(0, EnemyOffset), TileMap, _player);
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
            _enemy.Shift();
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
        if (Input.GetKey(KeyCode.LeftShift))
		{
            if (Input.GetKey(KeyCode.W)) _player.Move(Directions.Up);
            if (Input.GetKey(KeyCode.S)) _player.Move(Directions.Down);
            if (Input.GetKey(KeyCode.A)) _player.Move(Directions.Left);
            if (Input.GetKey(KeyCode.D)) _player.Move(Directions.Right);
		}
    }

    public void MovePlayer(Directions dir)
    {
        if (!Running) return;
        _player.Move(dir);
    }

    public void GoldOreBroken()
	{
        ProgressionBar.value += 1;
        if (ProgressionBar.value == ProgressionBar.maxValue) 
        {
            UpgradeCost *= UpgradeCostMultiplier;
            ProgressionBar.maxValue = Mathf.FloorToInt(UpgradeCost);
            ProgressionBar.value = 0;
            _player.Strength += 1;
        }
	}
}

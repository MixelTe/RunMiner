using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyTailController))]
public class Enemy : MonoBehaviour
{
    public float Speed;
    
    private TileMap _map;
    private Player _player;
    private EnemyTailController _tail;
    private Queue<Vector2Int> _path;
    private Vector2 _prevPoint;
    private Vector2 _nextPoint = Vector2.left; // Vector2.left - no next point
    private float _lerpTime = 0;

	private void Start()
	{
        _tail = GetComponent<EnemyTailController>();
    }
	public void Setup(Vector2Int pos, TileMap map, Player player)
    {
        _map = map;
        _player = player;
        _player.OnMove.AddListener(OnPlayerMove);
        transform.position = pos + map.Shift;

        var offset = pos.y - player.Pos.y;
        _path = new Queue<Vector2Int>();
		for (int i = offset - 1; i >= 0; i--)
		{
            _path.Enqueue(new Vector2Int(pos.x, player.Pos.y + i - _map.Layer));
        }
        _prevPoint = pos;
        if (_path.Count > 0) UpdateNextPoint();
    }
    public void Shift()
    {
        transform.position += new Vector3(0, 1);
        _prevPoint += new Vector2(0, 1);
        if (_nextPoint != Vector2.left) _nextPoint += new Vector2(0, 1);
        _tail.Shift();
    }

    void Update()
    {
        if (_nextPoint == Vector2.left)
        {
            if (_path.Count == 0) return;
            UpdateNextPoint();
            _tail.Move(transform.position, Speed);
        }

        _lerpTime += Speed * Time.deltaTime;
        if (_lerpTime < 1)
		{
            transform.position = Vector2.Lerp(_prevPoint, _nextPoint, _lerpTime);
            return;
		}
        _lerpTime = 0;
        transform.position = new Vector2(_nextPoint.x, _nextPoint.y);
        _prevPoint = _nextPoint;
        _nextPoint = Vector2.left;
    }

    private void UpdateNextPoint()
	{
        _nextPoint = _path.Dequeue();
        _nextPoint += _map.Shift;
        _nextPoint += new Vector2(0, _map.Layer);
    }
    private void OnPlayerMove()
    {
        _path.Enqueue(new Vector2Int(_player.Pos.x, _player.Pos.y - _map.Layer));
    }
}

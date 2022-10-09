using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTail : MonoBehaviour
{
    public float Speed;
    public int I;

    private Vector2 _prevPoint;
    private Vector2 _nextPoint = Vector2.left; // Vector2.left - no next point
    private float _lerpTime = 0;

    private void Start()
	{
        _prevPoint = transform.position;
	}

	// Update is called once per frame
	void Update()
    {
        if (_nextPoint == Vector2.left) return;

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
    public void Shift()
    {
        transform.position += new Vector3(0, 1);
        _prevPoint += new Vector2(0, 1);
        if (_nextPoint != Vector2.left) _nextPoint += new Vector2(0, 1);
    }
    public void SetNextPoin(Vector2 pos)
	{
        _prevPoint = transform.position;
        _nextPoint = pos;
        _lerpTime = 0;
    }
}

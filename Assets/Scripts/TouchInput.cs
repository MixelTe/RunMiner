using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TouchInput : MonoBehaviour
{
	public float minSwipeDistane;
	public UnityEvent<Directions> onSwipe = new();

	private Vector2 _touchStart;

	private void Update()
	{
		if (Input.touchCount == 0) return;

		var touch = Input.GetTouch(0);
		if (touch.phase == TouchPhase.Began)
		{
			_touchStart = touch.position;
		}
		if (touch.phase == TouchPhase.Ended)
		{
			var swipe = touch.position - _touchStart;
			if (swipe.magnitude < minSwipeDistane) return;

			if (Mathf.Abs(swipe.x) > Mathf.Abs(swipe.y))
			{
				if (swipe.x > 0) onSwipe.Invoke(Directions.Right);
				else onSwipe.Invoke(Directions.Left);
			}
			else
			{
				if (swipe.y > 0) onSwipe.Invoke(Directions.Up);
				else onSwipe.Invoke(Directions.Down);
			}
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeSim : MonoBehaviour
{
    public GameObject SwipeTrial;
    public Vector2 SwipeStart;
    public float SwipeLenght;
    public float SwipeSpeed;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) StartCoroutine(Swipe(Directions.Up));
        if (Input.GetKeyDown(KeyCode.S)) StartCoroutine(Swipe(Directions.Down));
        if (Input.GetKeyDown(KeyCode.A)) StartCoroutine(Swipe(Directions.Left));
        if (Input.GetKeyDown(KeyCode.D)) StartCoroutine(Swipe(Directions.Right));
    }

    private IEnumerator Swipe(Directions dir)
	{
        var pos = SwipeStart;

        Vector2 speed;
        if (dir == Directions.Up) speed = new Vector2(0, 1);
        else if (dir == Directions.Down) speed = new Vector2(0, -1);
        else if (dir == Directions.Left) speed = new Vector2(-1, 0);
        else speed = new Vector2(1, 0);

        pos -= speed;
        speed *= SwipeSpeed;

        var trail = Instantiate(SwipeTrial, pos, Quaternion.identity, transform);
        
        while ((pos - SwipeStart).magnitude < SwipeLenght)
		{
            pos += speed * Time.deltaTime;
            trail.transform.position = pos;
            yield return null;
		}
	}
}

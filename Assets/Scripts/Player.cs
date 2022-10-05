using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public enum Directions
    {
        Left, Right, Up, Down
	}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(Directions dir)
	{
        var newPos = transform.position;
        if (dir == Directions.Up) newPos += new Vector3(0, 1);
        if (dir == Directions.Down) newPos += new Vector3(0, -1);
        if (dir == Directions.Left) newPos += new Vector3(-1, 0);
        if (dir == Directions.Right) newPos += new Vector3(1, 0);
        transform.position = newPos;
	}
}

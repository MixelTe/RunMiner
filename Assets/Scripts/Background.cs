using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Background : MonoBehaviour
{
    private bool set = false;
    void Update()
    {
        if (set) return;
        set = true;
        var h = Camera.main.orthographicSize * 2 + 1;
        var renderer = GetComponent<SpriteRenderer>();
        renderer.size = new Vector2(renderer.size.x, h);
        transform.position = new Vector3(transform.position.x, h / 2 - 1);
    }
}

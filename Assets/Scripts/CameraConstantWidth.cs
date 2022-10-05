using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraConstantWidth : MonoBehaviour
{
    public float Width = 10;
    public float MinHeight = 10;
    public Vector2 BottomLeft = new(0, 0);

    [HideInInspector]
    public Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        Aplly();
    }

    [ContextMenu("Aplly")]
    private void Aplly()
    {
        if (!cam) cam = GetComponent<Camera>();

        var size = Width / 2 * cam.scaledPixelHeight / cam.scaledPixelWidth;
        size = Mathf.Max(size, MinHeight / 2);
        cam.orthographicSize = size;
        cam.transform.position = new Vector3(BottomLeft.x + Width / 2, BottomLeft.y + size, cam.transform.position.z);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    public float MinHeight = 10;
    public Vector2 BottomLeft = new(0, 0);

    [Header("Only In Inspector")]
    public float Width = 10;

    [HideInInspector] public Camera cam;
    [HideInInspector] public int Height;


    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        Aplly();
    }

    [ContextMenu("Aplly")]
    public void Aplly()
    {
        if (!cam) cam = GetComponent<Camera>();

        var size = Width / 2 * cam.scaledPixelHeight / cam.scaledPixelWidth;
        size = Mathf.Max(size, MinHeight / 2);
        Height = Mathf.FloorToInt(size * 2) + 1;
        cam.orthographicSize = size;
        cam.transform.position = new Vector3(BottomLeft.x + Width / 2, BottomLeft.y + size, cam.transform.position.z);
    }
}

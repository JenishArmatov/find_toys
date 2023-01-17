using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFix : MonoBehaviour
{
    public Vector2 DefaultResolution = new Vector2(1080, 1920);
    [Range(0f, 1f)] public float WidthOrHeight = 0;
    public GameObject Playstation;

    private Camera componentCamera;

    private float initialSize;
    private float targetAspect;

    private float initialFov;
    private float horizontalFov = 120f;

    private void Start()
    {
        componentCamera = GetComponent<Camera>();
        initialSize = componentCamera.orthographicSize;

        targetAspect = DefaultResolution.x / DefaultResolution.y;

        initialFov = componentCamera.fieldOfView;
        horizontalFov = CalcVerticalFov(initialFov, 1 / targetAspect);
    }

    private void Update()
    {
        if (componentCamera.orthographic)
        {
            float constantWidthSize = initialSize * (targetAspect / componentCamera.aspect);
            componentCamera.orthographicSize = Mathf.Lerp(constantWidthSize, initialSize, WidthOrHeight);
            Playstation.GetComponent<Transform>().localScale = new Vector3(constantWidthSize/100, 1, constantWidthSize/100);
        }
        else
        {
            float constantWidthFov = CalcVerticalFov(horizontalFov, componentCamera.aspect);
            componentCamera.fieldOfView = Mathf.Lerp(constantWidthFov, initialFov, WidthOrHeight);
            Playstation.GetComponent<Transform>().localScale = new Vector3(1+(1-constantWidthFov / 90), 1, 1);

        }
    }

    private float CalcVerticalFov(float hFovInDeg, float aspectRatio)
    {
        float hFovInRads = hFovInDeg * Mathf.Deg2Rad;

        float vFovInRads = 2 * Mathf.Atan(Mathf.Tan(hFovInRads / 2) / aspectRatio);

        return vFovInRads * Mathf.Rad2Deg;
    }

}

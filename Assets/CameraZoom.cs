using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private float targetZoom, zoomFactor = 3f, zoomLerpSpeed = 10, finalZoom;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        targetZoom = cam.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        targetZoom = Mathf.Clamp(targetZoom, 0f, 100f);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * zoomLerpSpeed);
    }

    public void CamZoom(float zoom, float speed)
    {
        targetZoom = zoom;
        zoomLerpSpeed = speed;
    }

    public void Return()
    {
        CamZoom(finalZoom, zoomLerpSpeed);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerZoom : MonoBehaviour
{
    public Camera playerCamera;
    private Vector3 initialPlayerScale = new Vector3(50f, 50f, 1f);
    private float initialCameraSize = 540f;

    private Coroutine smoothZoom;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = initialPlayerScale;
        playerCamera.orthographicSize = initialCameraSize;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ZoomOut(int targetHeight)
    {
        float zoomFactor = (targetHeight / 2) / playerCamera.orthographicSize;
        if (smoothZoom != null)
        {
            StopCoroutine(smoothZoom);
        }
        smoothZoom = StartCoroutine(ZoomCoroutine(zoomFactor, 1f));
    }

    private IEnumerator ZoomCoroutine(float zoomFactor, float duration)
    {
        float startCameraSize = playerCamera.orthographicSize;
        float targetCameraSize = startCameraSize * zoomFactor;

        float startPlayerScaleX = transform.localScale.x;
        float startPlayerScaleY = transform.localScale.y;
        float targetPlayerScaleX = startPlayerScaleX * zoomFactor;
        float targetPlayerScaleY = startPlayerScaleY * zoomFactor;

        for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            float blend = t / duration;
            playerCamera.orthographicSize = Mathf.Lerp(startCameraSize, targetCameraSize, blend);
            transform.localScale = new Vector3(Mathf.Lerp(startPlayerScaleX, targetPlayerScaleX, blend),
                                               Mathf.Lerp(startPlayerScaleY, targetPlayerScaleY, blend), 1);

            yield return null;
        }
        playerCamera.orthographicSize = targetCameraSize;
        transform.localScale = new Vector3(targetPlayerScaleX, targetPlayerScaleY, 1);
        smoothZoom = null;
    }
}

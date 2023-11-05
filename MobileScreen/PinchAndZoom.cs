using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanZoom : MonoBehaviour
{
    Vector3 touchStart;
    public float zoomOutMin = 1;
    public float zoomOutMax = 8;

    public float panLimitLeft = -10;
    public float panLimitRight = 10;
    public float panLimitTop = 10;
    public float panLimitBottom = -10;

    private Camera thisCamera;

    // Automatická inicializace kamery při startu
    void Start()
    {
        if (thisCamera == null)
        {
            thisCamera = GetComponent<Camera>();
        }
    }

    public void Init(Camera camera)
    {
        thisCamera = camera;
    }

    void Update()
    {
        PerformPanAndZoom();
    }

    public void PerformPanAndZoom()
    {
        if (thisCamera == null) return;  // Přidáno pro bezpečnost

        if (Input.GetMouseButtonDown(0))
        {
            touchStart = thisCamera.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            zoom(difference * 0.01f);
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 direction = touchStart - thisCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 newPosition = thisCamera.transform.position + direction;

            // Make sure the camera stays within bounds.
            float camHalfHeight = thisCamera.orthographicSize;
            float camHalfWidth = thisCamera.aspect * camHalfHeight;

            float leftBound = panLimitLeft + camHalfWidth;
            float rightBound = panLimitRight - camHalfWidth;
            float topBound = panLimitTop - camHalfHeight;
            float bottomBound = panLimitBottom + camHalfHeight;

            newPosition.x = Mathf.Clamp(newPosition.x, leftBound, rightBound);
            newPosition.y = Mathf.Clamp(newPosition.y, bottomBound, topBound);

            thisCamera.transform.position = newPosition;
        }

        zoom(Input.GetAxis("Mouse ScrollWheel"));
    }

    public void OnDrawGizmos()
    {
        // Nastavíme barvu gizma
        Gizmos.color = Color.red;

        // Vykreslíme body
        Gizmos.DrawSphere(new Vector3(panLimitLeft, panLimitTop, 0), 0.3f);
        Gizmos.DrawSphere(new Vector3(panLimitRight, panLimitTop, 0), 0.3f);
        Gizmos.DrawSphere(new Vector3(panLimitLeft, panLimitBottom, 0), 0.3f);
        Gizmos.DrawSphere(new Vector3(panLimitRight, panLimitBottom, 0), 0.3f);
    }

    void zoom(float increment)
    {
        if (thisCamera != null)  // Přidáno pro bezpečnost
        {
            thisCamera.orthographicSize = Mathf.Clamp(thisCamera.orthographicSize - increment, zoomOutMin, zoomOutMax);
        }
    }
}
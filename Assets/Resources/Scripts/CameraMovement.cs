using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;   

    [SerializeField]
    private float smoothSpeed = 1f;

    [SerializeField]
    private float zoomFactor = 1.0f;
    [SerializeField]
    private float zoomSpeed = 5.0f;
    [SerializeField]
    private float baseCameraSize = 5f;

    private Camera cameraComponent;

    // Start is called before the first frame update
    void Start()
    {
        cameraComponent = GetComponent<Camera>();
        cameraComponent.orthographicSize = baseCameraSize;
    }

    // Update is called once per frame
    void Update()
    {
        var targetPosition = target.position + offset;        

        var newPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);

        transform.position = newPosition;

        var targetCameraSize = baseCameraSize * zoomFactor;
        if (targetCameraSize != cameraComponent.orthographicSize)
        {
            cameraComponent.orthographicSize = Mathf.Lerp(cameraComponent.orthographicSize, targetCameraSize, Time.deltaTime * zoomSpeed);
        }
    }

    private void LateUpdate()
    {
        zoomFactor -= Input.GetAxisRaw("Mouse ScrollWheel");        
    }
}

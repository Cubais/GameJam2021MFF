using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceOnSceneObject : MonoBehaviour
{
    public Transform sceneObject;
    public Vector3 offset;

    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (sceneObject == null)
        {
            Destroy(gameObject);
        }

        Vector3 canvasLocation = Camera.main.WorldToScreenPoint(sceneObject.position);
        rectTransform.position = canvasLocation + offset;
    }
}

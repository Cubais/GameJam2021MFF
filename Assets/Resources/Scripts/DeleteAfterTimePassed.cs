using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAfterTimePassed : MonoBehaviour
{
    public float secondsToPass = 5;

    private float timePassed;

    void Start()
    {
        timePassed = 0;
    }

    void Update()
    {
        timePassed += Time.unscaledDeltaTime;
        if (timePassed >= secondsToPass)
        {
            Destroy(gameObject);
        }
    }
}

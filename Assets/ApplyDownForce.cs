using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyDownForce : MonoBehaviour
{
    private float forceSize = 30;

    private void OnTriggerStay2D(Collider2D collision)
    {
        collision.attachedRigidbody.AddForce(Vector2.down * forceSize);
    }
}

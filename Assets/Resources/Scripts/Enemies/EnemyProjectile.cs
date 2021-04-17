using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public float speed;

    private Vector3 movementDirection;

    // Update is called once per frame
    void Update()
    {
        transform.position += movementDirection * speed * Time.deltaTime;
    }

    public void SetMoveDirection(Vector3 dir)
    {
        movementDirection = dir;
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
        if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("MeleeEnemy"))
		{            
            Destroy(gameObject);
        }
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
        if (!collision.gameObject.CompareTag("Player") && !collision.gameObject.CompareTag("MeleeEnemy"))
        {         
            Destroy(gameObject);
        }
    }
}

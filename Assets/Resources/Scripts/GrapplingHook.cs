using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    public float hookSpeed = 10;
    public float latchSpeed = 10;
    public float maxDistance = 15;
    public float minLatchDistance;
    public Transform maskPivot;

    private Vector2 m_moveDir;
    private GameObject m_character;
    private bool m_latched;

    void Start()
    {
        m_character = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        float playerDistance = Vector3.Distance(transform.position, m_character.transform.position);
        Vector3 playerDir3 = m_character.transform.position - transform.position;
        Vector2 playerDir = new Vector2(playerDir3.x, playerDir3.y).normalized;

        if (m_latched)
        {
            // Move player toward hook
            m_character.transform.position += (Vector3)(-playerDir) * latchSpeed * Time.deltaTime;
            if (playerDistance < minLatchDistance)
            {
                Destroy(gameObject);
            }
        }
        else
        { 
            // Move hook towards moveDir
            if (m_moveDir != null)
            {
                transform.position += (Vector3)(m_moveDir * hookSpeed * Time.deltaTime);
            }

            if (playerDistance > maxDistance)
            {
                Destroy(gameObject);
            }
        }

        // Update rotation towards player
        float zAngle = Vector2.Angle(Vector2.right, playerDir);
        if (playerDir.y < 0)
        {
            zAngle *= -1;
        }

        transform.eulerAngles = new Vector3(0, 0, zAngle);

        // Update mask scale to hide excess chain
        maskPivot.localScale = new Vector3(playerDistance / maxDistance, 1, 1);


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6) // wall
        {
            m_latched = true;
        }
    }

    public void SetMoveDirection(Vector2 dir)
    {
        m_moveDir = dir;
        Start();
        Update();
    }

    public void DeleteIfLatched()
    {
        if (m_latched)
        {
            Destroy(gameObject);
        }
    }

    public void SetHookSpeed(float speed)
    {
        hookSpeed = speed;
    }
}

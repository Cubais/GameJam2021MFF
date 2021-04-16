using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletPaper : MonoBehaviour
{
    public float speed = 10;
    public int hp = 2;

    private Transform playerTransform;
    private EntityMovement em;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.Find("Character").GetComponent<Transform>();
        em = GetComponent<EntityMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir3 = playerTransform.position - transform.position;
        Vector2 dir = new Vector2(dir3.x, dir3.y).normalized;

        em.SetMoveDirection(dir);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8) // projectile collision
        {
            DecreaseHp();
            Destroy(collision.gameObject);
        }
    }

    private void DecreaseHp()
    {
        if (--hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}

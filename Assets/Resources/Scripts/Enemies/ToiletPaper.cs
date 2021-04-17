using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletPaper : MonoBehaviour
{
    public int hp = 3;
    public float pickupInitialSpeed = 1;

    private Transform playerTransform;
    private Transform healthPickupParentTransform;
    private GameObject hpPickupPrefab;
    private EntityMovement entityMovement;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        healthPickupParentTransform = GameObject.Find("HealthPickupsParent").GetComponent<Transform>();
        hpPickupPrefab = Resources.Load<GameObject>("Prefabs/HealthPickup");
        entityMovement = GetComponent<EntityMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir3 = playerTransform.position - transform.position;
        Vector2 dir = new Vector2(dir3.x, dir3.y).normalized;

        entityMovement.SetMoveDirection(dir, false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("FriendlyBullet")) // player projectile collision
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
        int pickupsToSpawn = (int) (Random.value * 3) + 1;

        for (int i = 0; i < pickupsToSpawn; i++)
        {
            GameObject pickupGo = Instantiate(hpPickupPrefab, transform.position, Quaternion.identity, healthPickupParentTransform);
            var pickupRb = pickupGo.GetComponent<Rigidbody2D>();
            pickupRb.AddForce(new Vector2(Random.value * pickupInitialSpeed, Random.value * pickupInitialSpeed));
        }

        Destroy(gameObject);
    }
}

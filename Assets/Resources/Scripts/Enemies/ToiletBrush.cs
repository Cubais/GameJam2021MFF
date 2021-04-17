using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletBrush : MonoBehaviour
{    
    public int hp = 2;
    public float pickupInitialSpeed = 1;
    public float shootingFrequencySeconds = 5;
    public Transform ProjectileTransform;

    private Transform playerTransform;
    private Transform healthPickupParentTransform;
    private Transform projectilesParent;
    private GameObject hpPickupPrefab;
    private GameObject projectilePrefab;
    private EntityMovement entityMovement;
    private float timePassed;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.Find("Character").GetComponent<Transform>();
        healthPickupParentTransform = GameObject.Find("HealthPickupsParent").GetComponent<Transform>();
        projectilesParent = GameObject.Find("ProjectilesParent").GetComponent<Transform>();
        hpPickupPrefab = Resources.Load<GameObject>("Prefabs/HealthPickup");
        projectilePrefab = Resources.Load<GameObject>("Prefabs/EnemyProjectile");
        entityMovement = GetComponent<EntityMovement>();
        timePassed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate direction towards the player
        Vector3 dir3 = playerTransform.position - transform.position;
        Vector2 dir = new Vector2(dir3.x, dir3.y).normalized;

        // Move away from the player
        entityMovement.SetMoveDirection(-1 * dir, false);

        // Every few seconds, fire a projectile towards the player
        timePassed += Time.deltaTime;
        if (timePassed >= shootingFrequencySeconds)
        {
            GameObject projectile = Instantiate(projectilePrefab, ProjectileTransform.position, Quaternion.identity, projectilesParent);
            var projectileScript = projectile.GetComponent<EnemyProjectile>();
            projectileScript.SetMoveDirection(dir);
            timePassed = 0;
        }
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
        int pickupsToSpawn = (int)(Random.value * 3) + 1;

        for (int i = 0; i < pickupsToSpawn; i++)
        {
            GameObject pickupGo = Instantiate(hpPickupPrefab, transform.position, Quaternion.identity, healthPickupParentTransform);
            var pickupRb = pickupGo.GetComponent<Rigidbody2D>();
            pickupRb.AddForce(new Vector2(Random.value * pickupInitialSpeed, Random.value * pickupInitialSpeed));
        }

        Destroy(gameObject);
    }
}

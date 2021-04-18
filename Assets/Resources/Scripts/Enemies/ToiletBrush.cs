using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletBrush : MonoBehaviour
{    
    public int hp = 2;
    public float pickupInitialSpeed = 1;
    public float shootingFrequencySeconds = 5;
    public Transform ProjectileTransform;
    public float MinDistance = 3f;
    public float MaxDistance = 6f;

    private Transform playerTransform;
    private Transform healthPickupParentTransform;
    private Transform projectilesParent;
    private GameObject hpPickupPrefab;
    private GameObject projectilePrefab;
    private EntityMovement entityMovement;
    private float timePassed;
    private Rigidbody2D m_rigidbody;
    private AudioSource m_audioSource;
    private AudioClip m_enemyHitSound;
    private AudioClip m_enemyShootSound;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        healthPickupParentTransform = GameObject.Find("HealthPickupsParent").GetComponent<Transform>();
        projectilesParent = GameObject.Find("ProjectilesParent").GetComponent<Transform>();
        hpPickupPrefab = Resources.Load<GameObject>("Prefabs/HealthPickup");
        projectilePrefab = Resources.Load<GameObject>("Prefabs/EnemyProjectile");
        entityMovement = GetComponent<EntityMovement>();
        timePassed = 0;

        m_rigidbody = GetComponent<Rigidbody2D>();
        m_audioSource = GetComponent<AudioSource>();
        m_enemyHitSound = Resources.Load("Audio/FX/enemy_hit/hitenemy") as AudioClip;
        m_enemyShootSound = Resources.Load("Audio/FX/enemy_shooting/545199__cbj-student__pop-3") as AudioClip;
    }

    // Update is called once per frame
    void Update()
    {
        if (LevelInfo.instance.IsAtWaterTile(transform.position))
        {
            m_rigidbody.gravityScale = 0;

            // Calculate direction towards the player
            Vector3 dir3 = playerTransform.position - transform.position;
            Vector2 dir = new Vector2(dir3.x, dir3.y).normalized;
            var distance = Vector2.Distance(playerTransform.position, transform.position);

            dir *= (distance < MinDistance) ? -1 : (MaxDistance < 5) ? 0.5f : 1;

            entityMovement.SetMoveDirection(dir, false);

            // Every few seconds, fire a projectile towards the player
            timePassed += Time.deltaTime;
            if (timePassed >= shootingFrequencySeconds)
            {
                GameObject projectile = Instantiate(projectilePrefab, ProjectileTransform.position, Quaternion.identity, projectilesParent);
                var projectileScript = projectile.GetComponent<EnemyProjectile>();
                projectileScript.SetMoveDirection((playerTransform.position - transform.position).normalized);
                m_audioSource.PlayOneShot(m_enemyShootSound);
                timePassed = 0;
            }
        }
		else
		{
            m_rigidbody.gravityScale = 1;
		}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("FriendlyBullet")) // player projectile collision
        {
            DecreaseHp();
            m_audioSource.PlayOneShot(m_enemyHitSound);
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

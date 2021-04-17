using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialEnemy : MonoBehaviour
{
    public int hp = 3;
    public float pickupInitialSpeed = 1;

    private Transform m_playerTransform;
    private Transform m_healthPickupParentTransform;
    private GameObject m_killText;
    private GameObject m_pickUpText;
    private PlaceOnSceneObject m_pickUpPlacer;
    private GameObject m_hpPickupPrefab;
    private EntityMovement m_entityMovement;
    private PlayerHealth m_playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        m_playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        m_healthPickupParentTransform = GameObject.Find("HealthPickupsParent").GetComponent<Transform>();
        m_killText = GameObject.Find("KillText");
        m_pickUpText = GameObject.Find("PickUpText");
        m_pickUpPlacer = m_pickUpText.GetComponent<PlaceOnSceneObject>();
        m_hpPickupPrefab = Resources.Load<GameObject>("Prefabs/HealthPickup");
        m_entityMovement = GetComponent<EntityMovement>();
        m_playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir3 = m_playerTransform.position - transform.position;
        Vector2 dir = new Vector2(dir3.x, dir3.y).normalized;

        m_entityMovement.SetMoveDirection(dir, false);
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
        int pickupsToSpawn = (m_playerHealth.MaxHP - m_playerHealth.CurrentHP) / m_playerHealth.DamageSettings.PickUpHealth;
        if (m_playerHealth.CurrentHP % m_playerHealth.DamageSettings.PickUpHealth > 0)
        {
            pickupsToSpawn++;
        }

        GameObject firstPickup = null;
        for (int i = 0; i < pickupsToSpawn; i++)
        {
            GameObject pickupGo = Instantiate(m_hpPickupPrefab, transform.position, Quaternion.identity, m_healthPickupParentTransform);
            var pickupRb = pickupGo.GetComponent<Rigidbody2D>();
            pickupRb.AddForce(new Vector2(Random.value * pickupInitialSpeed, Random.value * pickupInitialSpeed));

            if (firstPickup == null)
            {
                firstPickup = pickupGo;
            }
        }

        m_killText.SetActive(false);
        m_pickUpText.SetActive(true);
        m_pickUpPlacer.sceneObject = firstPickup.transform;

        Destroy(gameObject);
    }
}

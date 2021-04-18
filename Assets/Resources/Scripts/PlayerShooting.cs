using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public float forceMultiplier;
    public float level2ForceMultiplier;
    public GameObject projectilePrefab;
    public Transform projectilesParent;
    public Transform projectilePosition;

    private AudioSource m_audioSource;
    private AudioClip m_shootSound;

    private void Start()
    {
        GetComponent<PlayerHealth>().levelUpEvent += LevelUpEvent;
        m_audioSource = GetComponent<AudioSource>();
        m_shootSound = Resources.Load("Audio/FX/shooting/202230__deraj__pop-sound") as AudioClip;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 dir3 = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            Vector2 dir = new Vector2(dir3.x, dir3.y).normalized;

            GameObject newProjectile = Instantiate(
                projectilePrefab,
                new Vector3(projectilePosition.position.x + dir.x / 20, projectilePosition.position.y + dir.y / 20, 0),
                Quaternion.identity, projectilesParent
            );

            var newProjectileRB = newProjectile.GetComponent<Rigidbody2D>();
            newProjectileRB.AddForce(dir * forceMultiplier);
            m_audioSource.PlayOneShot(m_shootSound);
        }
    }

    public void LevelUpEvent(int level)
    {
        if (level == 2)
        {
            forceMultiplier = level2ForceMultiplier;
        }
    }
}

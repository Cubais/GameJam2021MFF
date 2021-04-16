using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public float forceMultiplier;
    public GameObject projectilePrefab;
    public Transform projectilesParent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 dir3 = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            Vector2 dir = new Vector2(dir3.x, dir3.y).normalized;

            GameObject newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity, projectilesParent);
            var newProjectileRB = newProjectile.GetComponent<Rigidbody2D>();
            newProjectileRB.AddForce(dir * forceMultiplier);
        }
    }
}

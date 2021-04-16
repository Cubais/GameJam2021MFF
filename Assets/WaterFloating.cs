using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFloating : MonoBehaviour
{
    public float BounceDamp = 0.05f;

    private Rigidbody2D m_rigidbody2D;
    private EntityMovement m_entity;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_entity = GetComponent<EntityMovement>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (m_entity.GetInWater())
        {
            var upForce = -Physics.gravity * (1f - m_rigidbody2D.velocity.y * BounceDamp);
            Debug.Log(upForce);
            m_rigidbody2D.AddForce(upForce);
        }
    }
}

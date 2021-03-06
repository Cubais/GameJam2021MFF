using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    [Tooltip("Movement speed of the entity")]
    public float Speed = 1.0f;

    private LevelInfo m_level;
    private Rigidbody2D m_rigidbody;
    private WaterFloating m_waterFloating;
    private Animator m_animator;
    private bool m_leftSideOrientation = true;

    private Vector3 m_moveDirection = Vector3.zero;

    private bool m_inWater = false;
    
    // Start is called before the first frame update
    void Start()
    {
        m_level = LevelInfo.instance;
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_waterFloating = GetComponent<WaterFloating>();
        m_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
		{
            var tileType = m_level.GetTileAtPos(transform.position);
            Debug.Log(tileType.name);
		}

        SetInWater();
        HandleAnimations(m_moveDirection.x, m_moveDirection.y);
    }

	private void FixedUpdate()
	{        
        m_rigidbody.AddForce(m_moveDirection * Speed);
    }

    public void SetMoveDirection(Vector3 direction, bool changeWaterForce)
	{
        m_moveDirection = direction.normalized;        

        // Not applying force when going up or down
        if (changeWaterForce)        
            m_waterFloating.SetApplyWaterForce(m_moveDirection.y == 0f);
	}

    public bool InWater()
	{
        return m_inWater;
	}

    private void SetInWater()
	{
        m_inWater = m_level.IsAtWaterTile(transform.position);
    }

    private void HandleAnimations(float horizontal, float vertical)
    {
        if (!m_animator)
            return;

        var hor = horizontal;
        if (horizontal == 0f)
        {
            hor = (m_leftSideOrientation) ? -0.1f : 0.1f;
        }
        else
        {
            m_leftSideOrientation = horizontal < 0;
        }

        m_animator.SetFloat("Vertical", vertical);
        m_animator.SetFloat("Horizontal", hor);
    }
}

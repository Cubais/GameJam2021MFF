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

    [SerializeField]
    private Transform m_feetTransform;

    private Vector3 m_moveDirection = Vector3.zero;

    private bool m_inWater = false;
    
    // Start is called before the first frame update
    void Start()
    {
        m_level = LevelInfo.instance;
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_waterFloating = GetComponent<WaterFloating>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
		{
            var tileType = m_level.GetTileAtPos(m_feetTransform.position);
            Debug.Log(tileType.name);
		}

        SetInWater();
    }

	private void FixedUpdate()
	{
        m_rigidbody.AddForce(m_moveDirection * Speed);
    }

    public void SetMoveDirection(Vector3 direction)
	{
        m_moveDirection = direction.normalized;

        // Not applying force when going up or down
        m_waterFloating.SetApplyWaterForce(m_moveDirection.y == 0f);
	}

    public bool InWater()
	{
        return m_inWater;
	}

    private void SetInWater()
	{
        var tile = m_level.GetTileAtPos(m_feetTransform.position);
        m_inWater = !(tile == null || tile.name != "WaterTile");

        m_rigidbody.gravityScale = (m_inWater) ? 0 : 1;
    }
}

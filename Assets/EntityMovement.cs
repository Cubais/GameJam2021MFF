using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    [Tooltip("Movement speed of the entity")]
    public float Speed = 1.0f;

    private LevelInfo m_level;
    private Rigidbody2D m_rigidbody;

    [SerializeField]
    private Transform m_feetTransform;

    private float m_vertical;
    private float m_horizontal;
    
    // Start is called before the first frame update
    void Start()
    {
        m_level = LevelInfo.instance;
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
		{
            var tileType = m_level.GetTileAtPos(m_feetTransform.position);
            Debug.Log(tileType.name);
		}

        m_vertical = Input.GetAxisRaw("Vertical");
        m_horizontal = Input.GetAxisRaw("Horizontal");

        
    }

	private void FixedUpdate()
	{
        Vector3 move = new Vector3(m_horizontal, m_vertical, 0);

        m_rigidbody.MovePosition(transform.position + move * Time.fixedDeltaTime * Speed);
    }
}

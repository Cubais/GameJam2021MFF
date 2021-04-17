using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WaterFloating : MonoBehaviour
{
    public Tile WaterTile;

    public float BounceDamp = 0.05f;
    public Transform positionCheck;

    public bool DebugInfo = false;

    [Tooltip("Should turn off/on gravity when not in water")]
    public bool TurnOffGravity = false;

    private Rigidbody2D m_rigidbody2D;
    private bool m_applyWaterForce = true;


    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var tile = LevelInfo.instance.GetTileAtPos(positionCheck.position);

        if (DebugInfo) 
            Debug.Log(tile + " " + WaterTile + " " + tile.Equals(WaterTile));

        if (tile != null && tile.Equals(WaterTile) && m_applyWaterForce)
        {
            if (TurnOffGravity)
                m_rigidbody2D.gravityScale = 0;

            var upForce = -Physics.gravity * (1f - m_rigidbody2D.velocity.y * BounceDamp);
            m_rigidbody2D.AddForce(upForce);

            if (DebugInfo) Debug.Log(gameObject.name + " " + upForce);
        }
        else
        {
            if (TurnOffGravity)
                m_rigidbody2D.gravityScale = 1;
        }
        
    }

    public void SetApplyWaterForce(bool applyForce)
	{
        m_applyWaterForce = applyForce;
	}
}

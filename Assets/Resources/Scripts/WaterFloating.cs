using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class WaterFloating : MonoBehaviour
{
    public Tile WaterTile;

    public float BounceDamp = 0.05f;
    public Transform CenterPositionWaterCheck;
    public Transform BottomPositionWaterCheck;

    public bool DebugInfo = false;

    [Tooltip("Should turn off/on gravity when not in water")]
    public bool TurnOffGravity = false;

    [Tooltip("Gravity which will be applied when above water")]
    public float GravityScale = 1f;

    [Tooltip("Minimum timespan to switch gravity on/off, prevents laggy movement on water surface")]
    public float GravitySwitchTimeSpan = 0.5f;

    private Rigidbody2D m_rigidbody2D;
    private bool m_applyWaterForce = true;
    private float m_turnOnTime = 0f;
    private int offWaterCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        if (CenterPositionWaterCheck == null)
            CenterPositionWaterCheck = this.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var tileCenter = LevelInfo.instance.GetTileAtPos(CenterPositionWaterCheck.position);
        var gravityValue = GravityScale;

        if (DebugInfo) 
            Debug.Log(tileCenter + " " + WaterTile + " " + tileCenter.Equals(WaterTile));


        if (BottomPositionWaterCheck != null)
        {
            var tileBottom = LevelInfo.instance.GetTileAtPos(BottomPositionWaterCheck.position);
            if (!tileBottom.Equals(WaterTile))
            {
                offWaterCount++;
                gravityValue = (offWaterCount > 5f) ? 3 : GravityScale;
            }
            else
                offWaterCount = 0;
        }


        if (tileCenter != null && tileCenter.Equals(WaterTile) && m_applyWaterForce)
        {            
            if (TurnOffGravity)
            {
                m_rigidbody2D.gravityScale = 0;                
            }

            var upForce = -Physics.gravity * (1f - m_rigidbody2D.velocity.y * BounceDamp);
            m_rigidbody2D.AddForce(upForce);

            if (DebugInfo) Debug.Log(gameObject.name + " " + upForce);
        }
        else if (tileCenter != null && !tileCenter.Equals(WaterTile))
        {   
            if (TurnOffGravity && (Time.realtimeSinceStartup - m_turnOnTime > GravitySwitchTimeSpan))
			{
                m_rigidbody2D.gravityScale = gravityValue;
                m_turnOnTime = Time.realtimeSinceStartup;
            }   
        }        
    }

    public void SetApplyWaterForce(bool applyForce)
	{
        m_applyWaterForce = applyForce;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFloating : MonoBehaviour
{
    public float BounceDamp = 0.05f;
    public Transform positionCheck;

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
        if (tile != null && tile.name == "WaterTile" && m_applyWaterForce)
        {
            var upForce = -Physics.gravity * (1f - m_rigidbody2D.velocity.y * BounceDamp);
            Debug.Log(upForce);
            m_rigidbody2D.AddForce(upForce);
        }
    }

    public void SetApplyWaterForce(bool applyForce)
	{
        m_applyWaterForce = applyForce;
	}
}

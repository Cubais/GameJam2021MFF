using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteAfterTimePassed : MonoBehaviour
{
    public float secondsToPass = 5;

    private float m_timePassed;    
    private WaterFloating m_waterFloating;
    private SpriteRenderer m_spriteRenderer;
    private ParticleSystem m_bubbles;
    private bool m_outsideWater = false;

    void Start()
    {
        m_timePassed = 0;        
        m_waterFloating = GetComponent<WaterFloating>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_bubbles = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        m_timePassed += Time.unscaledDeltaTime;
        if (m_timePassed >= secondsToPass || m_outsideWater)
        {
            m_bubbles.Stop();
            m_spriteRenderer.enabled = false;
            Destroy(gameObject, 5);
        }
    }

	private void FixedUpdate()
	{
        m_outsideWater = !m_waterFloating.InWater();	
	}
}

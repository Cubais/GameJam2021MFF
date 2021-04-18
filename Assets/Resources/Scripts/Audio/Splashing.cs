using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splashing : MonoBehaviour
{
    public float timeDelay = 3;

    private float m_curTimeDelay;
    private bool m_prevFrameInWater = true;
    private AudioSource m_audioSource;
    private AudioClip m_splashSound;

    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
        m_splashSound = Resources.Load("Audio/FX/splashes/398032__swordofkings128__splash") as AudioClip;
        m_curTimeDelay = timeDelay;
    }

    void Update()
    {
        if (m_curTimeDelay < timeDelay)
        {
            m_curTimeDelay += Time.unscaledDeltaTime;
            return;
        }

        bool curInWater = LevelInfo.instance.IsAtWaterTile(transform.position);

        if (!m_prevFrameInWater && curInWater)
        {
            m_audioSource.PlayOneShot(m_splashSound);
            m_curTimeDelay = 0;
        }

        m_prevFrameInWater = curInWater;
    }
}

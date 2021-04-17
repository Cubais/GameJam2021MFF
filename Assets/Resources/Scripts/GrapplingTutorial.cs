using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingTutorial : MonoBehaviour
{
    public float timeInSeconds = 5;

    private float m_curTime = 0;
    private bool m_rmbPressed = false;
    private bool m_timePassed = false;

    void Update()
    {
        if (m_timePassed && m_rmbPressed)
        {
            gameObject.SetActive(false);
        }

        m_curTime += Time.unscaledDeltaTime;
        if (m_curTime >= timeInSeconds)
        {
            m_timePassed = true;
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            m_rmbPressed = true;
        }
    }
}

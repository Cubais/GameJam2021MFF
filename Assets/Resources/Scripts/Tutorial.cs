using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public float timeInSeconds = 5;
    public GameObject grapplingTutorial;

    private float m_curTime = 0;
    private bool m_timePassed = false;
    private bool m_wsadPressed = false;
    private bool m_lmbPressed = false;

    private void Start()
    {
        GameObject.Find("Character").GetComponent<PlayerHealth>().levelUpEvent += LevelUpEvent;
    }

    void Update()
    {
        if (m_timePassed && m_wsadPressed && m_lmbPressed)
        {
            gameObject.SetActive(false);
        }

        m_curTime += Time.unscaledDeltaTime;
        if (m_curTime >= timeInSeconds)
        {
            m_timePassed = true;
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            m_wsadPressed = true;
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            m_lmbPressed = true;
        }
    }

    public void LevelUpEvent(int level)
    {
        if (level == 1) 
        { 
            grapplingTutorial.SetActive(true);
        }
    }
}

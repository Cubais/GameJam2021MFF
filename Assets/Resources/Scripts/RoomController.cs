using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public List<GameObject> trapdoors;
    public float rotationSpeed = 10;

    private bool m_active;
    private AudioClip m_flushSound;

    private void Start()
    {
        m_flushSound = Resources.Load("Audio/FX/flushes/274448__lorenzgillner__toilet-flushing") as AudioClip;
    }

    void Update()
    {
        if (m_active && transform.childCount == 0)
        {
            // Level is clear
            StartCoroutine("RotateTrapdoorsCoroutine");
            GlobalFXController.instance.PlayGlobalAudioClip(m_flushSound);
            m_active = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            m_active = true;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(true);
            }
        }
    }

    private IEnumerator RotateTrapdoorsCoroutine()
    {
        bool iterate = true;
        while (iterate)
        {
            foreach (GameObject trapdoor in trapdoors)
            {
                if (trapdoor.transform.eulerAngles.z <= 1 || trapdoor.transform.eulerAngles.z >= 268)
                {
                    trapdoor.transform.eulerAngles = new Vector3(0, 0, trapdoor.transform.eulerAngles.z - rotationSpeed);
                }
                else
                {
                    iterate = false;
                }
            }

            yield return new WaitForEndOfFrame();
        }
    }
}

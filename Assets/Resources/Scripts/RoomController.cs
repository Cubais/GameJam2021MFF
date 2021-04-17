using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public List<GameObject> trapdoors;
    public float rotationSpeed = 10;

    private bool m_active;

    void Update()
    {
        if (m_active && transform.childCount > 0)
        {
            StartCoroutine("RotateTrapdoorsCoroutine");

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
                if (trapdoor.transform.eulerAngles.z > -92f)
                {
                    trapdoor.transform.eulerAngles = new Vector3(0, 0, trapdoor.transform.eulerAngles.z - rotationSpeed * Time.deltaTime);
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

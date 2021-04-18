using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public List<GameObject> trapdoors;
    public float rotationSpeed = 10;

    private bool m_active;
    private AudioClip m_flushSound;
    private List<string> enemies;
    private List<Vector3> enemyPositions;
    private GameObject tutorialEnemyPrefab, toiletPaperEnemyPrefab, toiletBrushEnemyPrefab;

    private void Start()
    {
        m_flushSound = Resources.Load("Audio/FX/flushes/274448__lorenzgillner__toilet-flushing") as AudioClip;
        tutorialEnemyPrefab = Resources.Load("Prefabs/Characters/TutorialEnemy") as GameObject;
        toiletPaperEnemyPrefab = Resources.Load("Prefabs/Characters/ToiletPaper") as GameObject;
        toiletBrushEnemyPrefab = Resources.Load("Prefabs/Characters/ToiletBrush") as GameObject;
        enemies = new List<string>();
        enemyPositions = new List<Vector3>();

        foreach (Transform child in transform)
        {
            enemies.Add(child.gameObject.name);
            enemyPositions.Add(child.position);
        }
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

    public void ResetLevel()
    {
        foreach (GameObject trapdoor in trapdoors)
        {
            trapdoor.transform.eulerAngles = new Vector3(0, 0, 0);
        }

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            GameObject prefabToSpawn;
            bool tutorial = false;
            if (enemies[i].Contains("aper"))
            {
                prefabToSpawn = toiletPaperEnemyPrefab;
            }
            else if (enemies[i].Contains("rush"))
            {
                prefabToSpawn = toiletBrushEnemyPrefab;
            }
            else
            {
                prefabToSpawn = tutorialEnemyPrefab;
                tutorial = true;
            }

            GameObject instantiatedEnemy = Instantiate(prefabToSpawn, enemyPositions[i], Quaternion.identity, transform);
            if (tutorial)
            {
                instantiatedEnemy.GetComponent<TutorialEnemy>().SecondStart();
            }

            instantiatedEnemy.SetActive(false);
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

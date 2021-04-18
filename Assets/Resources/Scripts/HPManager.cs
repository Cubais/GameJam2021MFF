using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPManager : MonoBehaviour
{
    public static HPManager instance;

    public int curHp = 50;
    public RectTransform curHpPanel;
    public Text curHpText;    
    public GameObject gameOverPanel;
    public GameObject KeysTutorialPanel, TutorialEnemyPanel, GrapplingTutorial;
    public PlaceOnSceneObject killText, pickupText;
    public Transform OutOfScreen;

    public GameObject Player;

    public Transform RoomControllersToReset;
    public Transform HealthPickupsParent, GrapplingHookParent, ProjectilesParent;

    private int m_maxHP = 0;

    // Start is called before the first frame update
    void Awake()
    {
        if (!instance)
            instance = this;
        else
            Debug.LogError("HP MANAGER ALREADY SET!");
    }

    void Update()
	{
        if (gameOverPanel.activeSelf && Input.GetKeyDown(KeyCode.Space))
		{
            StartCoroutine("ResetLevel");
		}
	}

    public IEnumerator ResetLevel()
    {          
        gameOverPanel.SetActive(false);
        KeysTutorialPanel.SetActive(false);
        TutorialEnemyPanel.SetActive(false);
        GrapplingTutorial.SetActive(false);
        Time.timeScale = 1;
        Player.GetComponent<PlayerHealth>().ResetPlayer();

        yield return null;

        foreach (Transform room in RoomControllersToReset)
        {
            RoomController roomRC = room.GetComponent<RoomController>();
            roomRC.ResetLevel();
        }

        foreach (Transform child in HealthPickupsParent)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in GrapplingHookParent)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in ProjectilesParent)
        {
            Destroy(child.gameObject);
        }

        UpdateVisuals();
        yield return null;
    }

    public void SetMaxHP(int amount)
	{
        m_maxHP = amount;
	}

    public void SetHP(int amount)
    {
        curHp = amount;
        UpdateVisuals();
    }

    private void ResetGame()
    {
        ResetLevel();
    }

    private void UpdateVisuals()
    {
        if (m_maxHP == 0)
            return;

        curHpText.text = curHp.ToString();
        curHpPanel.anchorMax = new Vector2(curHp / (float) m_maxHP, curHpPanel.anchorMax.y);
    }
    public void Die()
    {        
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }
}

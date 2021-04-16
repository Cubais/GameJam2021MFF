using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPManager : MonoBehaviour, IResettable
{
    public int curHp = 50;
    public RectTransform curHpPanel;
    public Text curHpText;
    public ParticleSystem LostHpParticles;
    public GameObject gameOverPanel;

    private int hpMax = 100;
    private bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dead && Input.GetKeyDown(KeyCode.Space))
        {
            ResetGame();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            ChangeHp(-5);
        }
    }

    public void ResetLevel()
    {
        LostHpParticles.Clear();
        dead = false;
        gameOverPanel.SetActive(false);
        Time.timeScale = 1;
        curHp = 50;
        UpdateVisuals();
    }

    public void ChangeHp(int amount)
    {
        if (dead) { 
            return;
        }

        curHp += amount;

        if (curHp <= 0)
        {
            Die();
        }

        if (curHp >= hpMax)
        {
            LevelUp();
        }

        if (amount < 0)
        {
            //LostHpParticles.Clear();
            //LostHpParticles.Play();
            LostHpParticles.Emit(5);
        }

        UpdateVisuals();
    }

    private void ResetGame()
    {
        ResetLevel();
    }

    private void UpdateVisuals()
    {
        curHpText.text = curHp.ToString();
        curHpPanel.anchorMax = new Vector2(curHp / (float) hpMax, curHpPanel.anchorMax.y);
    }

    private void Die()
    {
        dead = true;
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }

    private void LevelUp()
    {
        //TODO
    }
}

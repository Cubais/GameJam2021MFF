using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPManager : MonoBehaviour
{
    public int curHp = 50;
    public RectTransform curHpPanel;
    public Text curHpText;
    public ParticleSystem LostHpParticles;

    private int hpMax = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeHp(-1);
        }
    }

    public void ChangeHp(int amount)
    {
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
            LostHpParticles.Clear();
            LostHpParticles.Play();
        }

        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        curHpText.text = curHp.ToString();
        curHpPanel.anchorMax = new Vector2(curHp / (float) hpMax, curHpPanel.anchorMax.y);
    }

    private void Die()
    {

    }

    private void LevelUp()
    {

    }
}

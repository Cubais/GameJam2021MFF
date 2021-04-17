using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int MaxHP = 100;
    public int CurrentHP { get; private set; } = 50;

    public ParticleSystem LostParticles;

    private HPManager m_HPManager;    
    void Start()
    {
        m_HPManager = HPManager.instance;
        m_HPManager.SetMaxHP(MaxHP);
    }

	private void Update()
	{
        if (Input.GetKeyDown(KeyCode.H))
        {
            ChangeHp(-5);
        }
    }

	public void ChangeHp(int byAmount)
	{
        if (byAmount == 0)
            return;

        if (byAmount < 0)
            LostParticles.Emit(5);

        CurrentHP += byAmount;
        m_HPManager.SetHP(CurrentHP);

        if (CurrentHP <= 0)
		{
            Die();            
		}

        if (CurrentHP >= MaxHP)
		{
            LevelUP();
		}
	}

    private void LevelUP()
	{
        CurrentHP = MaxHP / 2;
        m_HPManager.SetHP(CurrentHP);
	}

    private void Die()
	{
        m_HPManager.Die();
    }
}

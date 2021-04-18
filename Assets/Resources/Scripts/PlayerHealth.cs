using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    public int MaxHP = 100;
    public int CurrentHP { get; private set; } = 50;

    public DamageSettingsSO DamageSettings;

    public ParticleSystem LostParticles;

    public event Action<int> levelUpEvent;

    private HPManager m_HPManager;
    private AudioSource m_audioSource;
    private AudioClip m_playerHitSound;
    private AudioClip m_playerLevelUpSound;
    private AudioClip m_playerDeathSound;
    private PlayerCharacterChange m_characterChange;

    void Start()
    {
        m_HPManager = HPManager.instance;
        m_HPManager.SetMaxHP(MaxHP);
        m_audioSource = GetComponent<AudioSource>();
        m_playerHitSound = Resources.Load("Audio/FX/hit/playerhit") as AudioClip;
        m_playerLevelUpSound = Resources.Load("Audio/FX/levelUp/141695__copyc4t__levelup") as AudioClip;
        m_playerDeathSound = Resources.Load("Audio/FX/gameOver/415096__wolderado__game-over") as AudioClip;
        m_characterChange = GetComponent<PlayerCharacterChange>();
    }

	private void Update()
	{
        /*if (Input.GetKeyDown(KeyCode.L))
            LevelUP();*/
	}

	public void ChangeHp(int byAmount)
	{
        if (byAmount == 0)
            return;

        if (byAmount < 0)
            LostParticles.Emit(5);

        CurrentHP += byAmount;
        m_HPManager.SetHP(Math.Min(CurrentHP, MaxHP));

        if (CurrentHP <= 0)
		{
            Die();            
		}

        if (CurrentHP >= MaxHP)
		{
            LevelUP();
		}
	}

    public void ResetPlayer()
	{
        LostParticles.Clear();
        ResetHP();
        GetComponent<PlayerInput>().ResetLevel();
        m_HPManager.SetHP(CurrentHP);
        gameObject.transform.position = LevelInfo.instance.StartPosition.position;
	}        

    private void LevelUP()
	{
        var playerInput = GetComponent<PlayerInput>();
        if (playerInput.PlayerLevel >= 2)
        {
            return;
        }
        playerInput.LevelUp();
        m_characterChange.ChangeSking(playerInput.PlayerLevel);

        m_audioSource.PlayOneShot(m_playerLevelUpSound);
        ResetHP();
        levelUpEvent(playerInput.PlayerLevel);
	}

    private void Die()
	{
        m_audioSource.PlayOneShot(m_playerDeathSound);
        m_HPManager.Die();
    }

    private void ResetHP()
	{
        CurrentHP = MaxHP / 2;
        m_HPManager.SetHP(CurrentHP);
    }

	private void OnCollisionEnter2D(Collision2D collision)
	{
        Debug.Log("COL " + collision.gameObject.tag);

        if (collision.gameObject.CompareTag("MeleeEnemy"))
		{
            ChangeHp(-DamageSettings.MeleeDamage);
            m_audioSource.PlayOneShot(m_playerHitSound);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
        Debug.Log("Trig " + collision.gameObject.tag);

        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            ChangeHp(-DamageSettings.BrushRangeDamage);
            m_audioSource.PlayOneShot(m_playerHitSound);
            Destroy(collision.gameObject);
        }
    }
}

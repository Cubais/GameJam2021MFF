using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPicking : MonoBehaviour
{
    private PlayerHealth m_PlayerHealth;
	private AudioSource m_audioSource;
	private AudioClip m_hpPickupSound;

	private void Start()
	{
		m_PlayerHealth = GetComponent<PlayerHealth>();
		m_audioSource = GetComponent<AudioSource>();
		m_hpPickupSound = Resources.Load("Audio/FX/hpPickup/332629__treasuresounds__item-pickup") as AudioClip;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Pickup"))
		{
			m_PlayerHealth.ChangeHp(m_PlayerHealth.DamageSettings.PickUpHealth);
			m_audioSource.PlayOneShot(m_hpPickupSound);
			Destroy(collision.gameObject);
		}
	}
}

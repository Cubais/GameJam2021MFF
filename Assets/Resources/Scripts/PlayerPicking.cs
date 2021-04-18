using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPicking : MonoBehaviour
{
    private PlayerHealth m_PlayerHealth;

	private void Start()
	{
		m_PlayerHealth = GetComponent<PlayerHealth>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Pickup"))
		{
			Destroy(collision.gameObject);
			m_PlayerHealth.ChangeHp(m_PlayerHealth.DamageSettings.PickUpHealth);
		}
	}
}

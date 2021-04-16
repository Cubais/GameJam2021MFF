using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPicking : MonoBehaviour
{
	public GameObject pickupPrefab;

    private HPManager m_HPManager;

	private void Start()
	{
		m_HPManager = HPManager.instance;
		m_HPManager.LevelUP += LevelUp;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Q))
		{
			GameObject.Instantiate(pickupPrefab, Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward * 10f, Quaternion.identity);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Pickup"))
		{
			
		}
	}

	private void LevelUp()
	{

	}
}

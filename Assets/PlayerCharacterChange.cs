using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterChange : MonoBehaviour
{
	public Sprite[] playerSkins;
	public Sprite[] playerLeftEye;
	public Sprite[] playerRightEye;
	public RuntimeAnimatorController[] playerAnimators;

	public SpriteRenderer leftEyeRend;
	public SpriteRenderer rightEyeRend;

	private Animator m_animator;
	private SpriteRenderer playerRenderer;

	void Awake()
	{
		m_animator = GetComponent<Animator>();
		playerRenderer = GetComponent<SpriteRenderer>();
	}

	public void ChangeSking(int lvl)
	{
		if (lvl > 2)
			return;

		playerRenderer.sprite = playerSkins[lvl];
		m_animator.runtimeAnimatorController = playerAnimators[lvl];
		leftEyeRend.sprite = playerLeftEye[lvl];
		rightEyeRend.sprite = playerRightEye[lvl];
	}
}

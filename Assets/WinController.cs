using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinController : MonoBehaviour
{
    public GameObject winScreen;
    private AudioClip winSound;
    void Start()
    {
        winSound = Resources.Load("Audio/FX/victory") as AudioClip;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            winScreen.SetActive(true);
            GlobalFXController.instance.PlayGlobalAudioClip(winSound);
        }
    }
}

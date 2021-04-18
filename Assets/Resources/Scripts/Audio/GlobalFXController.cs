using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalFXController : MonoBehaviour
{
    public static GlobalFXController instance;

    private AudioSource m_audioSource;

    private void Awake()
    {
        if (!instance)
            instance = this;
        else
        {
            Debug.LogError("GlobalFX controller Already Spawned!!!");
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
    }

    public void PlayGlobalAudioClip(AudioClip audioClip)
    {
        m_audioSource.PlayOneShot(audioClip);
    }
}

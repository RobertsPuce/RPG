using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFinish : MonoBehaviour
{
    [SerializeField] private AudioClip yaySound;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private GameObject winScreen;
    private void OnTriggerEnter(Collider other)
    {
        sfxSource.PlayOneShot(yaySound);
        winScreen.SetActive(true);
        Time.timeScale = 0f;
    }
}

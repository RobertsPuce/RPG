using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class HealthPotion : MonoBehaviour
{
    [SerializeField] private AudioClip healthSound;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private PlayerHealth health;

    private void OnTriggerEnter(Collider other)
    {
        health.CurrentHealth = health.currentMaxHealth;
        sfxSource.PlayOneShot(healthSound);
        Destroy(gameObject);
    }
}

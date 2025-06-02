using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int startingHealth = 80;
    [SerializeField] private Collider weapon;
    [SerializeField] private float xpPerKill;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioSource sfxSource;
    
    private int currentHealth;
    private Animator anim;
    private bool killed;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
    }

    public bool IsDead()
    {
        return currentHealth <= 0;
    }
    
    public void EnableWeapon()
    {
        weapon.enabled = true;
    }

    public void DisableWeapon()
    {
        weapon.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("PlayerWeapon"))
        {
            TakeDamage(WeaponDamage.Instance.playerDamage);
            //print("enemy: " + currentHealth);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth >0)
        {
            //print(currentHealth);
            anim.SetTrigger("Hit");
            sfxSource.PlayOneShot(hitSound);
        }
        else
        {
            if (!killed)
            {
                anim.SetTrigger("Dead");
                PlayerXP.Instance.UpdateXP(xpPerKill);
                sfxSource.PlayOneShot(deathSound);
                GetComponent<CapsuleCollider>().enabled = false;
                GetComponent<EnemyHealth>().enabled = false;
                StartCoroutine(WaitAndDie(3f));
                killed = true;
            }
        }
    }

    private IEnumerator WaitAndDie(float deathTime)
    {
        yield return new WaitForSeconds(deathTime);
        Destroy(gameObject);
    }
}

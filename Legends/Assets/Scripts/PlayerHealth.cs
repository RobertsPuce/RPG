using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private int startingHealth = 100;
    [SerializeField] private float timeBetweenHits = 1f;
    [SerializeField] private Collider[] weapons;
    private int currentHealth;
    public int currentMaxHealth;
    private float lastHitTime = 0;
    private Animator anim;

    
    

    public static bool isAlive = true;
    
    public int CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            if (value < 0)
                currentHealth = 0;
            else
                currentHealth = value;
        }
    }
    public void BeginAttack()
    {
        foreach (Collider weapon in weapons)
            weapon.enabled = true;
    }

    public void EndAttack()
    {
        foreach (Collider weapon in weapons)
            weapon.enabled = false;
        
    }
    private void Awake()
    {
        anim = GetComponent<Animator>();
        currentHealth = startingHealth;
        currentMaxHealth = startingHealth;
        isAlive = true;
        BeginAttack();
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.tag.Equals("EnemyWeapon") && Time.time - lastHitTime > timeBetweenHits && isAlive)
        {
            TakeDamage(WeaponDamage.Instance.enemyDamage);
        }
    }

    public void TakeDamage(int damage)
    {
        lastHitTime = Time.time;
        currentHealth -= damage;
        //print(currentHealth);
        if (currentHealth > 0)
        {
            anim.SetTrigger("Hurt");
        }
        else
        {
            anim.SetTrigger("isDead");
            isAlive = false;
            PlayerXP.Instance.currentLevel = 1;
            PlayerXP.Instance.currentXP = 0;
            PlayerXP.Instance.UpdateXP(0);
            deathScreen.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public float GetHealthRatio()
    {
        return (float)currentHealth / (float)currentMaxHealth;
    }
    
   
}

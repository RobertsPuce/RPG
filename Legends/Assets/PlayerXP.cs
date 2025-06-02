using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerXP : MonoBehaviour
{
    private static PlayerXP instance;
    public static PlayerXP Instance => instance;
    [SerializeField] private PlayerHealth health;
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject); 
    }

    public float currentXP, maxXP;
    public int currentLevel = 1;

    [SerializeField] private TMP_Text levelText;
    [SerializeField] private Slider xpSlider;


    private void Start()
    {
        xpSlider.maxValue = maxXP;
        UpdateLevel();
    }

    private void UpdateLevel()
    {
        if (currentXP >= maxXP)
        {
            currentLevel++; // palielināt līmeni
            xpSlider.value = 0; // reset slider
            maxXP += 20; //palielināt maksimālo xp
            levelText.text = "Level: " + currentLevel;
            health.currentMaxHealth += 20; // palielināt max health
            health.CurrentHealth = health.currentMaxHealth; // reset health
            //extra damage for player and enemy
            WeaponDamage.Instance.playerDamage += 7;
            WeaponDamage.Instance.enemyDamage += 3;
            
        }
        levelText.text = "Level: " + currentLevel;
    }

    public void UpdateXP(float xp)
    {
        currentXP += xp;
        xpSlider.value = currentXP;
        UpdateLevel();
    }
}

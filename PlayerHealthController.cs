using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;

    public int currentHealth;
    public int maxHealth;
    public float invincibleLength = 1f;
    private float invinceCount;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        currentHealth = maxHealth;
        UIController.instance.healthBar.maxValue = maxHealth;
        UIController.instance.healthBar.value = currentHealth;
        UIController.instance.healthText.text = currentHealth + " / " + maxHealth;
    }
    void Update()
    {
        if(invinceCount > 0)
        {
            invinceCount -= Time.deltaTime;
            if (invinceCount <= 0)
            {
                PlayerController.instance.bodySR.color = new Color(PlayerController.instance.bodySR.color.r, PlayerController.instance.bodySR.color.g, PlayerController.instance.bodySR.color.b, 1f);
            }
        } 
    }
    public void DecreaseHealth()
    {
        if (invinceCount <= 0)
        {
            PlayerController.instance.bodySR.color = new Color(PlayerController.instance.bodySR.color.r, PlayerController.instance.bodySR.color.g, PlayerController.instance.bodySR.color.b, .5f);
            currentHealth--;
            AudioManager.instance.PlaySFX(11);
            invinceCount = invincibleLength;
            if (currentHealth <= 0)
            {
                PlayerController.instance.gameObject.SetActive(false);
                UIController.instance.deathScreen.SetActive(true);
                AudioManager.instance.PlayGameOver();
            }
            UIController.instance.healthBar.value = currentHealth;
            UIController.instance.healthText.text = currentHealth + " / " + maxHealth;
        }
    }
    public void HealPlayer (int health)
    {
        currentHealth += health;
        AudioManager.instance.PlaySFX(7);
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
        UIController.instance.healthBar.value = currentHealth;
        UIController.instance.healthText.text = currentHealth + " / " + maxHealth;
    }
    public void MakeInvincible(float time)
    {
        invinceCount = time;
        PlayerController.instance.bodySR.color = new Color(PlayerController.instance.bodySR.color.r, PlayerController.instance.bodySR.color.g, PlayerController.instance.bodySR.color.b, .5f);
    }
    public void IncreaseMaxHealth(int increase)
    {
        maxHealth += increase;
        UIController.instance.healthBar.maxValue = maxHealth;
        UIController.instance.healthBar.value = currentHealth;
        UIController.instance.healthText.text = currentHealth + " / " + maxHealth;
    }
}

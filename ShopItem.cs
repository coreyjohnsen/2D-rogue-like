using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public GameObject buyText;
    public int price = 5;
    private bool inZone = false;
    public bool isHealthRestore, isHealthUpgrade, isWeapon;
    public bool limitBuy = false;
    void Start()
    {

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && inZone)
        {
            if (LevelManager.instance.currentCoins >= price)
            {
                if(isHealthRestore)
                {
                    PlayerHealthController.instance.HealPlayer(PlayerHealthController.instance.maxHealth);
                }
                if (isHealthUpgrade)
                {
                    PlayerHealthController.instance.IncreaseMaxHealth(1);
                }
                LevelManager.instance.RemoveCoins(price);
                AudioManager.instance.PlaySFX(18);
                if(limitBuy)
                {
                    inZone = false;
                    gameObject.SetActive(false);
                }
            }
            else
            {
                AudioManager.instance.PlaySFX(19);
            }

        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            buyText.SetActive(true);
            inZone = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            buyText.SetActive(false);
            inZone = false;
        }
    }
}

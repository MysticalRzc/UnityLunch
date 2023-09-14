using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("2d collider is running");
        if (collision.gameObject.CompareTag("CanBePickedUp"))
        {
            Item hitObject = collision.gameObject.GetComponent<Consumable>().item;

            if (hitObject != null)
            {
                print("Hit: " + hitObject.objectName);

                switch (hitObject.itemType)
                {
                    case Item.ItemType.COIN:
                        break;
                    case Item.ItemType.HEALTH:
                        AdjustHitPoints(hitObject.quantity);
                        break;
                    default:
                        break;
                }
                collision.gameObject.SetActive(false);
            }
        }
    }
    public void AdjustHitPoints(int amount)
    {
        hitPoints = hitPoints + amount;
        print("Adjusted hitpoints by: " + amount + ". New value: " + hitPoints);
        GameObject obj = GameObject.FindGameObjectWithTag("meter");
        if(obj != null)
        {
            Image img = obj.GetComponent<Image>();
            img.fillAmount = (float)(hitPoints / (maxHitPoints + 0.0));
            Debug.Log(obj.name + img.fillAmount);
        }
    }
}

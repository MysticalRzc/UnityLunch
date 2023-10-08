using System.Collections;
using UnityEngine;

public class Player : Character
{
    public HealthBar healthBarPrefab;
    public Inventory inventoryPrefab;
    HealthBar healthBar;
    Inventory inventory;
    public void Start()
    {
        hitPoints.value = startingHitPoints;
        healthBar = Instantiate(healthBarPrefab);
        healthBar.character = this;
        inventory = Instantiate(inventoryPrefab);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CanBePickedUp"))
        {
            Item hitObject = collision.gameObject.GetComponent<Consumable>().item;

            if (hitObject != null)
            {
                bool shouldDisappear = false;

                //Debug.Log("hitObject.itemType" + hitObject.itemType);
                switch (hitObject.itemType)
                {
                    case Item.ItemType.COIN:
                        shouldDisappear = inventory.AddItem(hitObject);
                        shouldDisappear = true;
                        break;
                    case Item.ItemType.HEALTH:
                        shouldDisappear = AdjustHitPoints(hitObject.quantity);
                        break;
                    default:
                        shouldDisappear = inventory.AddItem(hitObject);
                        shouldDisappear = true;
                        break;
                }

                if (shouldDisappear)
                {
                    collision.gameObject.SetActive(false);
                }
            }
        }
    }

    public bool AdjustHitPoints(int amount)
    {
        if (hitPoints.value < maxHitPoints)
        {
            hitPoints.value = hitPoints.value + amount;
            //print("Adjusted HP by: " + amount + ". New value: " + hitPoints.value);
            return true;
        }
        //print("didnt adjust hitpoints");
        return false;
    }
    public override void KillCharacter()
    {
        base.KillCharacter();
        Destroy(healthBar.gameObject);
        Destroy(inventory.gameObject);  
    }
    public override void ResetCharacter()
    {
        hitPoints.value = startingHitPoints;

        healthBar = Instantiate(healthBarPrefab);
        healthBar.character = this;
        inventory = Instantiate(inventoryPrefab);
    }

    public override IEnumerator DemageCharater(float damage, float interval)
    {
        while (true)
        {
            StartCoroutine(FlickerCharacter());
            hitPoints.value = hitPoints.value - damage;
            if (hitPoints.value <= float.Epsilon)
            {
                KillCharacter();
                break;
            }
            if (interval > float.Epsilon)
            {
                yield return new WaitForSeconds(interval);
            }
            else
            {
                //如果伤害间隔为0，那么只计算一次（只攻击一次）
                break;
            }
        }
    }
}

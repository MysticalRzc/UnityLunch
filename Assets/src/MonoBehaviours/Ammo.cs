using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    public int demageInflicted;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter2D is running" + collision.GetType());
        if (collision is CircleCollider2D)
        {
            Debug.Log("enemy demage is running");
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            StartCoroutine(enemy.DemageCharater(demageInflicted,0.0f));
            //gameObject.SetActive(false);
        }
    }


}

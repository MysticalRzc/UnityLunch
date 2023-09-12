using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("2d collider is running");
        if (collision.gameObject.CompareTag("CanBePickedUp"))
        {
            collision.gameObject.SetActive(false);
        }
    }
}

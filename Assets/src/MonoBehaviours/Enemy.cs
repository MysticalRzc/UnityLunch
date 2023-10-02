using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public float hitPoint;
    public float demageStrength;
    public float demageFrequency;
    Coroutine damageCoroutine;
    public override IEnumerator DemageCharater(float damage, float interval)
    {
        while (true)
        {
            hitPoint = hitPoint - damage;
            if(hitPoint <= float.Epsilon)
            {
                KillCharacter();
                break;
            }
            if(interval > float.Epsilon)
            {
                yield return new WaitForSeconds(interval);
            }else
            {
                //����˺����Ϊ0����ôֻ����һ�Σ�ֻ����һ�Σ�
                break;
            }
        }
    }

    public override void ResetCharacter()
    {
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            //Debug.Log("enemy collision is enter" + collision.gameObject.tag);
            Player player = collision.gameObject.GetComponent<Player>();
            if (damageCoroutine == null)
            {
                damageCoroutine = StartCoroutine(player.DemageCharater(demageStrength, demageFrequency));
            }
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (damageCoroutine != null)
            {
                StopCoroutine(damageCoroutine);
                damageCoroutine = null;
            }
        }
    }
}

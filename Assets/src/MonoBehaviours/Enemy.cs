using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    float hitPoint;

    public override IEnumerator DamageCharacter(int damage, float interval)
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
}

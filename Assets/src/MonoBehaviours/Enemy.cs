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
                //如果伤害间隔为0，那么只计算一次（只攻击一次）
                break;
            }
        }
    }

    public override void ResetCharacter()
    {
       
    }
}

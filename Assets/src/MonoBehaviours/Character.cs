using System.Collections;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    public HitPoints hitPoints;
    public float maxHitPoints;
    public float startingHitPoints;

    public enum CharacterCategory
    {
        PLAYER,
        ENEMY
    }
    public CharacterCategory characterCategory;

    public virtual void KillCharacter()
    {
        Destroy(gameObject);
    }

    public abstract void ResetCharacter();
    public abstract IEnumerator DemageCharater(float damage, float interval);

}

using UnityEngine;

public class EnemySlot : MonoBehaviour
{
    float health = 100;

    public void Damage(float damage)
    {
        health -= damage;
    }
}

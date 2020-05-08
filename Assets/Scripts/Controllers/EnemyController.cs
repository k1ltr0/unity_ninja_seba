using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update}
    public CharacterStats stats;

    public void TakeDamage(int damage)
    {
        stats.TakeDamage(damage);
    }

    public int Attack()
    {
        return stats.Attack();
    }
}

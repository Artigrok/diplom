using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArmor : BasicEnemyLogic
{

    public override void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            gameObject.GetComponentInParent<Grunt1>().Grunt1_shield = false;
            Destroy(gameObject);
        }
    }
}

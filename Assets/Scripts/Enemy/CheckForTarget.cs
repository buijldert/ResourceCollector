using Data;
using Enemy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForTarget : MonoBehaviour {

    [SerializeField]private EnemyAttack _enemyAttack;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == InlineStrings.PLAYERTAG)
        {
            _enemyAttack.HasTarget = true;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == InlineStrings.PLAYERTAG)
        {
            _enemyAttack.HasTarget = false;
        }
    }
}

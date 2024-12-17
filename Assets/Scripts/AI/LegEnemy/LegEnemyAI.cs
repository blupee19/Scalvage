using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using System.IO;
using TMPro.Examples;

public class LegEnemyAI : AIEnemyBase
{
    public Transform LegGFX;
    public GameObject legEnemy;
    public override void Flip()
    {
        if (target.position.x > transform.position.x)
        {
            legEnemy.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            legEnemy.GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}


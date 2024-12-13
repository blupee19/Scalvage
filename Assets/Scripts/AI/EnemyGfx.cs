using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyGfx : MonoBehaviour
{
    public AIPath aiPath;
        
    void Update()
    {
        Flip();
    }

    void Flip()
    {
        if (aiPath.desiredVelocity.x >= 0.01)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        } else if(aiPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

}

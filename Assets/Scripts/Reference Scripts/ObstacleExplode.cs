using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleExplode : Obstacle
{

    public override void HitPlayer(GameObject player)
    {
        base.HitPlayer(player);

        Destroy(gameObject);
    }
}

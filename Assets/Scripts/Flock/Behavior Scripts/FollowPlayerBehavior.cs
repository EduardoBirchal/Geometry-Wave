using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/FollowPlayer")]
public class FollowPlayerBehavior : FilteredFlockBehavior
{
    public GameObject player;

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {

        Vector2 followMove = Vector2.zero;

        player = GameObject.Find("Player");

        if(player == null) followMove = new Vector2(0, 0);
        else followMove = player.transform.position;

        followMove -= (Vector2)agent.transform.position;

        return followMove;

    }

}

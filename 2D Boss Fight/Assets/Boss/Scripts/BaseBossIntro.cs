using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBossIntro : BaseBoss
{
    [Tooltip("The distance from the boss to the player which wakes the boss up.")]
    public float visionRange;

    [Tooltip("The amount of time elapsed until the boss goes to its idle state.")]
    public float timeTillIdle;

    bool isAwake;


    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        isAwake = false;
        nextAnimation = "Idle";
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(Vector2.Distance(enemyPos.position, playerPos.position) < visionRange)
        {
            isAwake = true;
        }

        if(isAwake)
        {
            if(timeTillIdle > 0)
            {
                timeTillIdle -= Time.deltaTime;
            }
            else
            {
                Transition(animator);
            }
        }
    }
}

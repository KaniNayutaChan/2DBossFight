using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBossIdle : BaseBoss
{
    [Tooltip("List of attacks that the boss will use.")]
    public string[] attackList;
    int attackToUse;

    [Tooltip("The minimnum amount of time elapsed till the boss uses it's attack.")]
    public float minStartTimeTillAttack;
    [Tooltip("The maximum amount of time elapsed till the boss uses it's attack.")]
    public float maxStartTimeTillAttack;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        timeTillNextAnimation = Random.Range(minStartTimeTillAttack, maxStartTimeTillAttack);
        attackToUse = Random.Range(0, attackList.Length);
        nextAnimation = attackList[attackToUse];
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
    }
}

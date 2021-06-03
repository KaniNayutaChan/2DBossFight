using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBossAttack : BaseBoss
{
    public float startTimeTillNextAnimation;
    protected Vector2 spawnPos;
    public int numberOfAttacks;
    int attackCounter;
    public float startTimeTillAttack;
    public float startTimeBetweenAttacks;
    protected float timeTillAttack;
    public GameObject attackPrefab;

    public SpawnTimingType spawnTimingType;
    public enum SpawnTimingType
    {
        AfterTime,
        ReachDestination
    }

    public SpawnType spawnType;
    public enum SpawnType
    {
        NoSpawn,
        RelativeToPlayer,
        RelativeToEnemy,
        RandomPosition,
        SetPosition,
        InIncrements,
        InCircle
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        timeTillAttack = startTimeTillAttack;
        timeTillNextAnimation = startTimeTillNextAnimation;
        attackCounter = 0;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        switch (spawnTimingType)
        {
            case SpawnTimingType.AfterTime:
                if (timeTillAttack > 0)
                {
                    timeTillAttack -= Time.deltaTime;
                }
                else
                {
                    UseAttack();
                }
                break;

            case SpawnTimingType.ReachDestination:
                if (IsAtDestination())
                {
                    if (timeTillAttack > 0)
                    {
                        timeTillAttack -= Time.deltaTime;
                    }
                    else
                    {
                        UseAttack();
                    }
                }
                break;
        }
    }

    protected void UseAttack()
    {
        if(attackCounter < numberOfAttacks)
        {
            attackCounter++;
            timeTillAttack = startTimeBetweenAttacks;
            Instantiate(attackPrefab, spawnPos, enemyPos.rotation);
        }
    }
}

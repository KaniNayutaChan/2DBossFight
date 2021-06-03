using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBossAttack : BaseBoss
{
    public float startTimeTillNextAnimation;
    protected Vector2 spawnPos;

    public SpawnTimingType spawnTimingType;
    public enum SpawnTimingType
    {
        AfterTime,
        ReachDestination
    }

    public SpawnType spawnType;
    public float spawnRelativeToPlayerX;
    public float spawnRelativeToPlayerY;
    public float spawnRelativeToEnemyX;
    public float spawnRelativeToEnemyY;
    public float spawnMinRandomX;
    public float spawnMaxRandomX;
    public float spawnMinRandomY;
    public float spawnMaxRandomY;
    public float spawnSetX;
    public float spawnSetY;
    public float spawnStartIncrementX;
    public float spawnStartIncrementY;
    public float spawnIncrementalDistance;
    public float spawnCircleAmplitude;

    public enum SpawnType
    {
        NoSpawn,
        RelativeToPlayer,
        RelativeToEnemy,
        RandomPosition,
        Set,
        InIncrements,
        InCircle
    }

    public GameObject attackPrefab;
    public int numberOfAttacks;
    int attackCounter;
    public float startTimeTillAttack;
    public float startTimeBetweenAttacks;
    protected float timeTillAttack;

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

            switch (spawnType)
            {
                case SpawnType.RelativeToEnemy:
                    if(IsFacingRight())
                    {
                        spawnPos.Set(enemyPos.position.x + spawnRelativeToEnemyX, enemyPos.position.y + spawnRelativeToEnemyY);
                    }
                    else
                    {
                        spawnPos.Set(enemyPos.position.x - spawnRelativeToEnemyX, enemyPos.position.y + spawnRelativeToEnemyY);
                    }
                    break;

                case SpawnType.RelativeToPlayer:
                    if (IsFacingRight())
                    {
                        spawnPos.Set(playerPos.position.x + spawnRelativeToPlayerX, playerPos.position.y + spawnRelativeToPlayerY);
                    }
                    else
                    {
                        spawnPos.Set(playerPos.position.x - spawnRelativeToPlayerX, playerPos.position.y + spawnRelativeToPlayerY);
                    }
                    break;

                case SpawnType.RandomPosition:
                    spawnPos.Set(Random.Range(spawnMinRandomX, spawnMaxRandomX), Random.Range(spawnMinRandomY, spawnMaxRandomY));
                    break;

                case SpawnType.Set:
                    spawnPos.Set(spawnSetX, spawnSetY);
                    break;

                case SpawnType.InIncrements:
                    if (IsFacingRight())
                    {
                        spawnPos.Set(spawnStartIncrementX - (attackCounter * spawnIncrementalDistance), spawnStartIncrementY);
                    }
                    else
                    {
                        spawnPos.Set(-spawnStartIncrementX + (attackCounter * spawnIncrementalDistance), spawnStartIncrementY);
                    }
                    break;

                case SpawnType.InCircle:
                    spawnPos.Set(playerPos.position.x + (spawnCircleAmplitude * Mathf.Cos(2 * Mathf.PI * attackCounter / numberOfAttacks)), playerPos.position.y + (spawnCircleAmplitude * Mathf.Sin(2 * Mathf.PI * attackCounter / numberOfAttacks)));
                    break;
            }


            Instantiate(attackPrefab, spawnPos, enemyPos.rotation);
        }
    }
}

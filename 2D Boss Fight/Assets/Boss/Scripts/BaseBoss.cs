using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBoss : StateMachineBehaviour
{
    protected Transform playerPos;
    protected Transform enemyPos;
    protected BossHealth bossHealth;
    protected Vector2 destination;
    protected float timeTillNextAnimation;
    public string nextAnimation;

    [Tooltip("The movement speed of the boss")]
    public float speed;

    [Tooltip("The direction the boss will face.")]
    public DirectionType directionType;
    public enum DirectionType
    {
        FacePlayer,
        FaceDestination
    }

    [Tooltip("The type of movement along the X axis that the boss performs.")]
    public MovementType movementTypeX;

    [Tooltip("The type of movement along the Y axis that the boss performs.")]
    public MovementType movementTypeY;

    public enum MovementType
    {
        NoMovement,
        RelativeToEnemy,
        RelativeToPlayer,
        Set,
        Random
    }

    public float relativeToEnemyX;
    public float relativeToEnemyY;

    public float relativeToPlayerX;
    public float relativeToPlayerY;

    public float minRandomX;
    public float maxRandomX;
    
    public float minRandomY;
    public float maxRandomY;

    public float setX;
    public float setY;

    [Tooltip("The method in which the boss will transition into its attack.")]
    public TransitionType transitionType;
    public enum TransitionType
    {
        AfterTime,
        ReachDestination
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPos = Player.instance.transform;
        enemyPos = animator.transform;
        bossHealth = animator.GetComponent<BossHealth>();

        SetDestination(movementTypeX, movementTypeY);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        switch (directionType)
        {
            case DirectionType.FacePlayer:
                FacePlayer();
                break;

            case DirectionType.FaceDestination:
                FaceDestination();
                break;
        }

        switch (transitionType)
        {
            case TransitionType.AfterTime:
                if (timeTillNextAnimation > 0)
                {
                    timeTillNextAnimation -= Time.deltaTime;
                }
                else
                {
                    Transition(animator);
                }
                break;

            case TransitionType.ReachDestination:
                if (IsAtDestination())
                {
                    if (timeTillNextAnimation > 0)
                    {
                        timeTillNextAnimation -= Time.deltaTime;
                    }
                    else
                    {
                        Transition(animator);
                    }
                }
                break;
        }

        SetDestination(movementTypeX, movementTypeY);
        MoveToDestination();
    }

    protected void FaceLeft()
    {
        enemyPos.rotation = Quaternion.Euler(0, 0, 0);
    }

    protected void FaceRight()
    {
        enemyPos.rotation = Quaternion.Euler(0, 180, 0);
    }

    protected void FacePlayer()
    {
        if(enemyPos.position.x > playerPos.position.x)
        {
            FaceLeft();
        }
        else
        {
            FaceRight();
        }
    }

    protected void FaceDestination()
    {
        if (enemyPos.position.x > destination.x)
        {
            FaceLeft();
        }
        else
        {
            FaceRight();
        }
    }

    protected bool IsFacingRight()
    {
        if(enemyPos.eulerAngles.y == 180)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected void MoveToDestination()
    {
        enemyPos.position = Vector2.MoveTowards(enemyPos.position, destination, speed * Time.deltaTime);
    }


    protected bool IsAtDestination()
    {
        if(Vector2.Distance(enemyPos.position, destination) < 0.1f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected virtual void Transition(Animator animator)
    {
        animator.Play(nextAnimation);
    }

    protected void SetDestination(MovementType x, MovementType y)
    {
        switch (x)
        {
            case MovementType.NoMovement:
                destination.x = 0;
                break;

            case MovementType.RelativeToEnemy:
                if (IsFacingRight())
                {
                    destination.x = enemyPos.position.x + relativeToEnemyX;
                }
                else
                {
                    destination.x = enemyPos.position.x - relativeToEnemyX;
                }
                break;

            case MovementType.RelativeToPlayer:
                if (IsFacingRight())
                {
                    destination.x = playerPos.position.x + relativeToPlayerX;
                }
                else
                {
                    destination.x = playerPos.position.x - relativeToPlayerX;
                }
                break;

            case MovementType.Set:
                destination.x = setX;
                break;

            case MovementType.Random:
                destination.x = Random.Range(minRandomX, maxRandomX);
                break;
        }


        switch (y)
        {
            case MovementType.NoMovement:
                destination.y = 0;
                break;

            case MovementType.RelativeToEnemy:
                destination.y = enemyPos.position.y + relativeToEnemyY;
                break;

            case MovementType.RelativeToPlayer:
                destination.y = playerPos.position.y + relativeToPlayerY;

                break;

            case MovementType.Set:
                destination.y = setY;
                break;

            case MovementType.Random:
                destination.y = Random.Range(minRandomY, maxRandomY);
                break;
        }
    }
  
    
}


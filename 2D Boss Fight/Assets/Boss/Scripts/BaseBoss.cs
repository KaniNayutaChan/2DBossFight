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
    public float movementRelativeToEnemyX;
    public float movementRelativeToPlayerX;
    public float movementMinRandomX;
    public float movementMaxRandomX;
    public float movementSetX;

    [Tooltip("The type of movement along the Y axis that the boss performs.")]
    public MovementType movementTypeY;
    public float movementRelativeToEnemyY;
    public float movementRelativeToPlayerY;
    public float movementMinRandomY;
    public float movementMaxRandomY;
    public float movementSetY;

    public enum MovementType
    {
        NoMovement,
        RelativeToEnemy,
        RelativeToPlayer,
        Set,
        Random
    }

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
                    destination.x = enemyPos.position.x + movementRelativeToEnemyX;
                }
                else
                {
                    destination.x = enemyPos.position.x - movementRelativeToEnemyX;
                }
                break;

            case MovementType.RelativeToPlayer:
                if (IsFacingRight())
                {
                    destination.x = playerPos.position.x + movementRelativeToPlayerX;
                }
                else
                {
                    destination.x = playerPos.position.x - movementRelativeToPlayerX;
                }
                break;

            case MovementType.Set:
                destination.x = movementSetX;
                break;

            case MovementType.Random:
                destination.x = Random.Range(movementMinRandomX, movementMaxRandomX);
                break;
        }


        switch (y)
        {
            case MovementType.NoMovement:
                destination.y = 0;
                break;

            case MovementType.RelativeToEnemy:
                destination.y = enemyPos.position.y + movementRelativeToEnemyY;
                break;

            case MovementType.RelativeToPlayer:
                destination.y = playerPos.position.y + movementRelativeToPlayerY;

                break;

            case MovementType.Set:
                destination.y = movementSetY;
                break;

            case MovementType.Random:
                destination.y = Random.Range(movementMinRandomY, movementMaxRandomY);
                break;
        }
    }
  
    
}


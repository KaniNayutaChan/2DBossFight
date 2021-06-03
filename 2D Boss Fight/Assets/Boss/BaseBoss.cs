using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBoss : StateMachineBehaviour
{
    protected Transform playerPos;
    protected Transform enemyPos;
    protected BossHealth bossHealth;
    protected Vector2 destination;
    public float speed;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPos = Player.instance.transform;
        enemyPos = animator.transform;
        bossHealth = animator.GetComponent<BossHealth>();
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
}

using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;


[CustomEditor(typeof(BaseBossAttack), true)]
[CanEditMultipleObjects]
public class BaseBossAttackEditor : BassBossEditor
{
    //Add the ShowOnEnum methods in here
    public override void SetFieldCondition()
    {
        base.SetFieldCondition();

        ShowOnEnum("spawnType", "RelativeToEnemy", "numberOfAttacks");
        ShowOnEnum("spawnType", "RelativeToPlayer", "numberOfAttacks");
        ShowOnEnum("spawnType", "RandomPosition", "numberOfAttacks");
        ShowOnEnum("spawnType", "SetPosition", "numberOfAttacks");
        ShowOnEnum("spawnType", "InIncrements", "numberOfAttacks");
        ShowOnEnum("spawnType", "InCircle", "numberOfAttacks");

        ShowOnEnum("spawnType", "RelativeToEnemy", "startTimeTillAttack");
        ShowOnEnum("spawnType", "RelativeToPlayer", "startTimeTillAttack");
        ShowOnEnum("spawnType", "RandomPosition", "startTimeTillAttack");
        ShowOnEnum("spawnType", "SetPosition", "startTimeTillAttack");
        ShowOnEnum("spawnType", "InIncrements", "startTimeTillAttack");
        ShowOnEnum("spawnType", "InCircle", "startTimeTillAttack");

        ShowOnEnum("spawnType", "RelativeToEnemy", "startTimeBetweenAttacks");
        ShowOnEnum("spawnType", "RelativeToPlayer", "startTimeBetweenAttacks");
        ShowOnEnum("spawnType", "RandomPosition", "startTimeBetweenAttacks");
        ShowOnEnum("spawnType", "SetPosition", "startTimeBetweenAttacks");
        ShowOnEnum("spawnType", "InIncrements", "startTimeBetweenAttacks");
        ShowOnEnum("spawnType", "InCircle", "startTimeBetweenAttacks");

        ShowOnEnum("spawnType", "RelativeToEnemy", "attackPrefab");
        ShowOnEnum("spawnType", "RelativeToPlayer", "attackPrefab");
        ShowOnEnum("spawnType", "RandomPosition", "attackPrefab");
        ShowOnEnum("spawnType", "SetPosition", "attackPrefab");
        ShowOnEnum("spawnType", "InIncrements", "attackPrefab");
        ShowOnEnum("spawnType", "InCircle", "attackPrefab");
    }
}



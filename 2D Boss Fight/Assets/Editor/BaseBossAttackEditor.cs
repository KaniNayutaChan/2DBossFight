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

        ShowOnEnum("spawnType", "RelativeToPlayer", "spawnRelativeToPlayerX");
        ShowOnEnum("spawnType", "RelativeToPlayer", "spawnRelativeToPlayerY");

        ShowOnEnum("spawnType", "RelativeToEnemy", "spawnRelativeToEnemyX");
        ShowOnEnum("spawnType", "RelativeToEnemy", "spawnRelativeToEnemyY");

        ShowOnEnum("spawnType", "RandomPosition", "spawnMinRandomX");
        ShowOnEnum("spawnType", "RandomPosition", "spawnMaxRandomX");
        ShowOnEnum("spawnType", "RandomPosition", "spawnMinRandomY");
        ShowOnEnum("spawnType", "RandomPosition", "spawnMaxRandomY");

        ShowOnEnum("spawnType", "Set", "spawnSetX");
        ShowOnEnum("spawnType", "Set", "spawnSetY");

        ShowOnEnum("spawnType", "InIncrements", "spawnStartIncrementX");
        ShowOnEnum("spawnType", "InIncrements", "spawnStartIncrementY");
        ShowOnEnum("spawnType", "InIncrements", "spawnIncrementalDistance");

        ShowOnEnum("spawnType", "InCircle", "spawnCircleAmplitude");

    }
}



using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static partial class Events
{
    public static event Func<List<EnemyInfo>> OnGetEnemies = null;
    public static List<EnemyInfo> GetEnemies() => OnGetEnemies?.Invoke();

    public static event Action<List<EnemyInfo>> OnEnemiesSpawned = null;
    public static void DoFireOnEnemiesSpawned(List<EnemyInfo> enemies) => OnEnemiesSpawned?.Invoke(enemies);
}

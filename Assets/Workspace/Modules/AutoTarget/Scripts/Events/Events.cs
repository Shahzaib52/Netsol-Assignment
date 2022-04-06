using System;

public static partial class Events
{
    public static event Func<EnemyInfo> OnGetNearestEnemy = null;
    public static EnemyInfo GetNearestEnemy() => OnGetNearestEnemy?.Invoke();
}
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static partial class Events
{
    public static event Func<PlayerInfo> OnGetPlayerTarget = null;
    public static PlayerInfo GetPlayerInfo() => OnGetPlayerTarget?.Invoke();
}

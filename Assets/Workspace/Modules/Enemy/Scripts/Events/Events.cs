using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static partial class Events
{
    public static event Action<float, int> OnDealDamage = null;
    public static void DealDamage(float damage, int id) => OnDealDamage?.Invoke(damage, id);
}

using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public static class API
{
    public static event Action<List<POI>> OnPOIReceived = null;
    public static void DOFirePOIReceivedEvent(List<POI> point) => OnPOIReceived?.Invoke(point);
}

public struct POI
{
    public string Name;
    public string ID;
    public string Tag;
    public float Longtitude;
    public float Lattitude;
    public POI(string name, string id, string tag, float lon, float lat)
    {
        ID = id;
        Tag = tag;
        Name = name;
        Lattitude = lat;
        Longtitude = lon;
    }
}

using UnityEngine;
using SimpleJSON;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Threading.Tasks;

public static class WRLD
{
    private static WrldApiRequestHandler Handler;
    public static WrldApiRequestHandler RequestHandler
    {
        get
        {
            if (Handler == null)
            {
                Handler = new WrldApiRequestHandler();
            }

            return Handler;
        }
    }
}

public class WrldApiRequestHandler
{
    private const string BaseURL = "https://poi.wrld3d.com/v1.1";
    private const string Token = "3206b921cdf26dbecfd69502f8bbb04416fd1baf38c1c8f6f1d29c6e7790226bce1c364397451c39";
    private string POISetsURL = $"{BaseURL}/poisets/?token={Token}";
    private string POIsURL(string SID) => $"{BaseURL}/poisets/{SID}/pois/?token={Token}";

    private const string IDKey = "id";
    private const string LONKey = "lon";
    private const string LATKey = "lat";
    private const string TitleKey = "title";
    private const string TagsKey = "tags";

    public void RequestPOIs(UnityAction<List<POI>> callback)
    {
        FetchPOISets(callback);
    }

    private void FetchPOISets(UnityAction<List<POI>> callback = null)
    {
        var req = UnityWebRequest.Get(POISetsURL);
        var operation = req.SendWebRequest();

        operation.completed += new System.Action<AsyncOperation>((asyncOperation) =>
        {
            if (operation.isDone)
            {
                if (req.result == UnityWebRequest.Result.ConnectionError || req.result == UnityWebRequest.Result.DataProcessingError)
                {
                    Debug.LogError($"ERROR ---> {req.error}");
                    return;
                }

                var collection = JSON.Parse(req.downloadHandler.text);
                foreach (var o in collection)
                {
                    string id = o.Value[IDKey];
                    FetchPOI(id, callback);
                }
            }
        });
    }

    private void FetchPOI(string sid, UnityAction<List<POI>> callback = null)
    {
        var req = UnityWebRequest.Get(POIsURL(sid));
        var operation = req.SendWebRequest();

        operation.completed += new System.Action<AsyncOperation>((asyncOperation) =>
        {
            if (operation.isDone)
            {
                if (req.result == UnityWebRequest.Result.ConnectionError || req.result == UnityWebRequest.Result.DataProcessingError)
                {
                    Debug.LogError($"ERROR ---> {req.error}");
                    return;
                }

                var collection = JSON.Parse(req.downloadHandler.text);
                List<POI> points = new List<POI>();

                foreach (var o in collection)
                {
                    string id = o.Value[IDKey];
                    string tag = o.Value[TagsKey];
                    string title = o.Value[TitleKey];
                    string longtitude = o.Value[LONKey];
                    string lattitude = o.Value[LATKey];

                    var poi = new POI(title, id, tag, float.Parse(longtitude), float.Parse(lattitude));
                    points.Add(poi);
                }

                callback?.Invoke(points);
            }
        });
    }
}

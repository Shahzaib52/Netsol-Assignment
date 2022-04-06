using Wrld;
using Wrld.Space;
using UnityEngine;
using Wrld.Transport;
using System.Collections;
using System.Collections.Generic;

public class EnemiesHandler : MonoBehaviour
{
    [SerializeField] private Camera RayCamera;
    [SerializeField] private BaseEnemy EnemyPrefab = null;
    [SerializeField] private List<EnemyInfo> SpawnedObjects = new List<EnemyInfo>();
    private List<POI> PointsData = new List<POI>();

    private void OnEnable()
    {
        Events.OnGetEnemies += GetEnemies;
    }

    private void OnDisable()
    {
        Events.OnGetEnemies -= GetEnemies;
    }

    private List<EnemyInfo> GetEnemies() => SpawnedObjects;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        PointsData = new List<POI>();
        var handler = WRLD.RequestHandler;
        handler.RequestPOIs(OnPOIReceived);
    }

    private void OnPOIReceived(List<POI> points)
    {
        StartCoroutine(SpawnEnemies(points));
    }

    IEnumerator SpawnEnemies(List<POI> points)
    {
        Api.Instance.CameraApi.SetControlledCamera(RayCamera);

        for (int i = 0; i < points.Count; i++)
        {
            var point = points[i];
            var location = LatLong.FromDegrees(point.Lattitude, point.Longtitude);

            Api.Instance.SetOriginPoint(location);
            yield return new WaitForSeconds(0.25f);

            Api.Instance.CameraApi.MoveTo(location, distanceFromInterest: 400, headingDegrees: 0, tiltDegrees: 0);
            yield return new WaitForSeconds(0.25f);

            var ray = Api.Instance.SpacesApi.LatLongToVerticallyDownRay(location);
            LatLongAltitude buildingIntersectionPoint;
            yield return new WaitForSeconds(0.25f);

            if (point.Tag == "general")
            {
                var didIntersectBuilding = Api.Instance.BuildingsApi.TryFindIntersectionWithBuilding(ray, out buildingIntersectionPoint);
                if (didIntersectBuilding)
                {
                    var boxAnchor = Instantiate(EnemyPrefab);
                    boxAnchor.GetComponent<GeographicTransform>().SetPosition(buildingIntersectionPoint.GetLatLong());

                    boxAnchor.transform.localPosition = Vector3.up * (float)buildingIntersectionPoint.GetAltitude();
                    SpawnedObjects.Add(boxAnchor);
                    boxAnchor.name = point.Name;
                }
            }
            else
            {
                if (point.Tag == "transport")
                {
                    var options = new TransportPositionerOptionsBuilder()
                    .SetInputCoordinates(location.GetLatitude(), location.GetLongitude())
                    .SetMaxDistanceToMatchedPoint(20)
                    .Build();

                    TransportPositioner positioner = Api.Instance.TransportApi.CreatePositioner(options);
                    positioner.OnPointOnGraphChanged += Callback;

                    void Callback()
                    {
                        positioner.OnPointOnGraphChanged -= Callback;
                        if (positioner.IsMatched())
                        {
                            var pointOnGraph = positioner.GetPointOnGraph();
                            var outputLLA = LatLongAltitude.FromECEF(pointOnGraph.PointOnWay);

                            const double verticalOffset = 22.0;
                            outputLLA.SetAltitude(outputLLA.GetAltitude() + verticalOffset);
                            var outputPosition = Api.Instance.SpacesApi.GeographicToWorldPoint(outputLLA);

                            var boxAnchor = Instantiate(EnemyPrefab);
                            boxAnchor.GetComponent<GeographicTransform>().SetPosition(outputLLA.GetLatLong());
                            boxAnchor.transform.position += new Vector3(0, (float)verticalOffset, 0);
                            SpawnedObjects.Add(boxAnchor);
                            boxAnchor.name = point.Name;
                        }
                    }
                }
            }

            PointsData.Add(point);
        }

        Events.DoFireOnEnemiesSpawned(SpawnedObjects);
    }
}

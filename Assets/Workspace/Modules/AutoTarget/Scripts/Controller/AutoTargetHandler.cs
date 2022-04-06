using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTargetHandler : MonoBehaviour, AutoTarget
{
    [SerializeField] private Transform Reticle;

    private Transform MainCamera;
    private PlayerInfo Player;
    private EnemyInfo NearestEnemy;
    [SerializeField] private List<EnemyInfo> Enemies = new List<EnemyInfo>();

    private void Start()
    {
        Player = Events.GetPlayerInfo();
        MainCamera = Camera.main.transform;
    }

    private void OnEnable()
    {
        Events.OnGetNearestEnemy += GetNearestEnemy;
        Events.OnEnemiesSpawned += OnEnemiesSpawned;
    }

    private void OnDisable()
    {
        Events.OnGetNearestEnemy -= GetNearestEnemy;
        Events.OnEnemiesSpawned -= OnEnemiesSpawned;
    }

    private void Update()
    {
        PlaceReticle();
        FindNearestEnemy();
    }

    private void OnEnemiesSpawned(List<EnemyInfo> enemies) => Enemies = enemies;

    private void FindNearestEnemy()
    {
        if (Enemies.Count <= 0)
            return;

        RaycastHit hit;
        var distance = Mathf.Infinity;

        foreach (var e in Enemies)
        {
            if (!e.IsAlive()) continue;

            var A = Player.Position();
            var B = e.Position();

            var isBehind = Vector3.Dot(B - A, MainCamera.transform.forward) < 0;

            if (isBehind) continue;

            if (Physics.Linecast(A, B, out hit))
            {
                if (hit.collider.gameObject.GetInstanceID() != e.GetID())
                {
                    continue;
                }
            }

            var dist = Vector3.Distance(A, B);
            if (dist < distance)
            {
                distance = dist;
                NearestEnemy = e;
            }
        }
    }

    private void PlaceReticle()
    {
        Reticle.gameObject.SetActive(NearestEnemy != null && NearestEnemy.IsAlive());

        if (NearestEnemy != null)
        {
            Reticle.position = NearestEnemy.Position();
            Reticle.LookAt(MainCamera);
        }
    }

    public EnemyInfo GetNearestEnemy() => NearestEnemy;
}

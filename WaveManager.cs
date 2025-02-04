using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public List<Transform> Spawners;
    public List<GameObject> EnemyTypes;
    public List<int> EnemyTypesCount;
    public LayerMask LayerEnemies;
    public Collider[] Enemies;
    public int i;
    public int j;
    public float timer;
    public float t = 2f;
    public bool StageFinished;
    public float WaveTimer = 0f;
    public float WaveTimerIndicator;
    [SerializeField] private float arenaRadius;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponentInParent<Transform>().GetComponentInParent<NavigationBaker>().StartWave(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (WaveTimer >= WaveTimerIndicator)
        {
            if (t >= timer)
            {
                if (EnemyTypes.Count > 0)
                {
                    i = Random.Range(0, EnemyTypes.Count);
                    j = Random.Range(0, Spawners.Count);
                    if (EnemyTypesCount[i] > 0)
                    {
                        Instantiate(EnemyTypes[i], Spawners[j].position, Quaternion.identity);
                        EnemyTypesCount[i] -= 1;
                    }
                    if (EnemyTypesCount[i] <= 0)
                    {
                        EnemyTypes.Remove(EnemyTypes[i]);
                        EnemyTypesCount.Remove(EnemyTypesCount[i]);
                    }
                }
                t = 0;
            }
            t += Time.deltaTime;
            if (EnemyTypes.Count <= 0)
            {
                Enemies = Physics.OverlapSphere(gameObject.transform.position, arenaRadius, LayerEnemies, QueryTriggerInteraction.Ignore);
                
                if (Enemies.Length <= 0) { gameObject.GetComponent<WaveManager>().enabled = false; gameObject.GetComponentInParent<Transform>().GetComponentInParent<NavigationBaker>().StartWave(gameObject);  Debug.Log("Script Offline"); }
            }
        }
        else WaveTimer += Time.deltaTime;
    }
}

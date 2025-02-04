using System.Collections;
using Unity.AI.Navigation;
using UnityEngine;

public class NavigationBaker : MonoBehaviour
{
    public NavMeshSurface[] surfaces;
    public Transform[] objectsToRotate;
    public GameObject[] plane;
    public GameObject[] bossplane;
    public GameObject[] Enemies;
    public int[] EnemiesNumber;
    public float[] EnemiesWaveCoef;
    public float[] EnemiesBonusHP;
    public float[] EnemiesBonusDamage;
    public bool zaebalsa = false;
    public int WaveNumber = 0;
    public int WaveDifficulty;
    public GameObject Player;
    public int RoomNumber;
    public int BossRoomNumber;
    // Use this for initialization
    void Start()
    {
        Instantiate(plane[Random.Range(0,plane.Length)], gameObject.transform);
        Player.transform.position = gameObject.GetComponentInChildren<Transform>().GetChild(0).GetComponentInChildren<Transform>().GetChild(2).transform.position;
        GenerateNav();
        RoomNumber++;
    }
    void Update()
    {
        
    }
    public void SecondShtuka() 
    {
        if (RoomNumber < BossRoomNumber) { 
            Debug.Log("Spawen");
                Instantiate(plane[Random.Range(0, plane.Length)], gameObject.transform);
                GameObject.FindGameObjectWithTag("Player").transform.position = gameObject.GetComponentInChildren<Transform>().GetChild(1).GetComponentInChildren<Transform>().GetChild(2).transform.position;
                Destroy(gameObject.GetComponentInChildren<Transform>().GetChild(0).gameObject);
                GenerateNav();
                RoomNumber++;
        } else
        {
            Instantiate(bossplane[Random.Range(0, bossplane.Length)], gameObject.transform);
            GameObject.FindGameObjectWithTag("Player").transform.position = gameObject.GetComponentInChildren<Transform>().GetChild(1).GetComponentInChildren<Transform>().GetChild(2).transform.position;
            Destroy(gameObject.GetComponentInChildren<Transform>().GetChild(0).gameObject);
            GenerateNav();
            RoomNumber=0;
        }

    }
    public void StartWave(GameObject Room) 
    {
        WaveManager StageWave = Room.GetComponent<WaveManager>();
        if (StageWave.StageFinished == false)
        {
            WaveNumber++;
            if (WaveNumber % WaveDifficulty == 0) 
            {
                for (int i = 0; i < Enemies.Length; i++)
                {
                    Enemies[i].GetComponent<BasicEnemyLogic>().Health += EnemiesBonusHP[i];
                    Enemies[i].GetComponent<BasicEnemyLogic>().Damage += EnemiesBonusDamage[i];
                }
            }
            Debug.Log("Wave online");
            StageWave.WaveTimer = 0f;
            for (int i = 0; i < Enemies.Length; i++)
            {
                StageWave.EnemyTypes.Add(Enemies[i]);
                StageWave.EnemyTypesCount.Add((int)(EnemiesNumber[i] * (EnemiesWaveCoef[i] * (WaveNumber%WaveDifficulty+1))));
            }
            Debug.Log("Array online");
            Room.GetComponent<WaveManager>().enabled = true;
            Debug.Log("Script online");
        }
        else { Room.GetComponent<Transform>().parent.GetChild(3).gameObject.SetActive(true); Debug.Log("SPAWEN "+Room.GetComponent<Transform>().parent.GetChild(3).gameObject.name); }
    }
    public void GenerateNav() 
    {
        StartCoroutine(ABC());
    }
    IEnumerator ABC()
    {

        //returning 0 will make it wait 1 frame
        yield return 0;
        for (int i = 0; i < surfaces.Length; i++)
        {
            
            surfaces[i].BuildNavMesh();
        }
        //code goes here


    }
}
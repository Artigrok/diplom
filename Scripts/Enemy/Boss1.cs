using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
public class Boss1 : BasicEnemyLogic
{
    public GameObject[] waypoints;
    public GameObject WaveManager;
    public float rageLevel;
    public float RageStage1;
    public float resist;
    public bool RageStage1activated;
    public float StagePercentage;
    public GameObject bulletPrefab; // Префаб пули
    public Transform firePoint; // Точка, откуда будут выпускаться пули
    public float bulletForce = 20f; // Сила, с которой будет выпущена пуля
    public GameObject dieEffect;
    public TMP_Text hptext;
    public float fireRate = 6f;
    private float t;

    private int currentWaypointIndex = 0; // Индекс текущей целевой точки
    // Start is called before the first frame update
    new void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        nav = gameObject.GetComponent<NavMeshAgent>();
        t = fireRate;
        SetNextWaypoint();
    }
    new void Update()
    {
        hptext.text = "Boss Health: " + Health;
        if (nav.remainingDistance < nav.stoppingDistance)
        {
            SetNextWaypoint();
        }
        if (Time.time >= t )
        {
            Shoot(); // Выстрел
            t = Time.time + fireRate; // Обновляем время следующего выстрела
        }
    }
    public override void TakeDamage(float damage)
    {
        Health -= damage * (1 - resist); 
        rageLevel += damage;
        if (rageLevel < RageStage1)
        {
            if (RageStage1activated)
            RageStageOneDeExecute();
        }
        if (rageLevel >= RageStage1)
        {
            if (!RageStage1activated)
                RageStageOneExecute();
        }
        if (Health <= 0)
        {
            GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().Score += 500;
            GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().Money += 10000;
            WaveManager.GetComponent<WaveManager>().StageFinished = true;
            RageStageOneDeExecute() ;
            Instantiate(dieEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void RageStageOneExecute() 
    {
        RageStage1activated = true;
        WaveManager.GetComponent<WaveManager>().WaveTimerIndicator /= 2;
        WaveManager.GetComponent<WaveManager>().timer /= 2;
        resist = 0.5f;
        nav.speed *= 3;
        nav.angularSpeed*= 3;
    }
    void RageStageOneDeExecute()
    {
        RageStage1activated = false;
        WaveManager.GetComponent<WaveManager>().WaveTimerIndicator *= 2;
        WaveManager.GetComponent<WaveManager>().timer *= 2;
        resist = 0f;
        nav.speed /= 3;
        nav.angularSpeed /= 3;
    }
    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation); // Создаем пулю
        Rigidbody rb = bullet.GetComponent<Rigidbody>(); // Получаем компонент Rigidbody2D пули

        if (rb != null)
        {
            rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse); // Наносим импульс для движения пули
        }
    }
    void SetNextWaypoint()
    {
        // Выбираем следующую точку из массива
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        // Устанавливаем эту точку как цель для NavMeshAgent
        nav.SetDestination(waypoints[currentWaypointIndex].transform.position);
    }
}

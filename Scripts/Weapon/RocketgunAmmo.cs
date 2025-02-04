using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketgunAmmo : MonoBehaviour
{
    public float explosionRadius = 1f; // Радиус взрыва
    public int damage = 10; // Урон по области
    public int directDamage = 0; // Прямой урон игроку
    public LayerMask m_Mask = -1;
    public GameObject explosionSound;
    private void Start()
    {
    }
    private void OnCollisionEnter(Collision collision)
    {
        // Наносим урон по области при контакте с поверхностью
        Explode();

        
        Instantiate(explosionSound, transform.position, Quaternion.identity);
        // Уничтожаем пулю
        Destroy(gameObject);
    }

    void Explode()
    {
        // Получаем все коллайдеры в радиусе взрыва
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        // Проходимся по всем найденным коллайдерам
        foreach (Collider nearbyObject in colliders)
        {
            // Получаем компонент здоровья объекта, чтобы нанести урон
            BasicEnemyLogic enemyHealth = nearbyObject.gameObject.GetComponent<BasicEnemyLogic>();
            if (enemyHealth != null)
            {
                Debug.Log("SPLAAAASH");
                enemyHealth.TakeDamage(damage);
            }
        }
    }
}

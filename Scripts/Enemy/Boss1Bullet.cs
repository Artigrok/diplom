using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss1Bullet : MonoBehaviour
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

        // Если пуля столкнулась с игроком, то наносим ему прямой урон
        PlayerLogic playerHealth = collision.gameObject.GetComponent<PlayerLogic> ();
        if (collision.transform.CompareTag("Player"))
        {
            Debug.Log("DIIIIIRECT");
            playerHealth.PlayerTakeDamage(directDamage);
        }
        Instantiate(explosionSound,transform.position,Quaternion.identity);
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
            PlayerLogic playerHealth = nearbyObject.gameObject.GetComponent<PlayerLogic>();
            if (playerHealth != null)
            {
                Debug.Log("SPLAAAASH");
                playerHealth.PlayerTakeDamage(damage);
            }
        }
    }
}

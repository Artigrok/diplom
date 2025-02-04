using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1AutoAim : MonoBehaviour
{
    public Transform target; // Цель, к которой будет направлен игровой объект
    public float rotationSpeed = 10f; // Скорость поворота объекта
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {
        if (target == null)
            return; // Если цели нет, ничего не делаем

        Vector3 targetDirection = target.position - transform.position; // Направление к цели
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection); // Получаем необходимый поворот

        // Плавно поворачиваем игровой объект в сторону цели
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}

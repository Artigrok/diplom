using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPositionSwing : MonoBehaviour
{
    public float maxRotationX = 10f; // ћаксимальное значение поворота по оси X
    public float maxRotationY = 10f; // ћаксимальное значение поворота по оси Y
    public float rotationSpeed = 1f; // —корость изменени€ поворота
    public float stabilize = 10f;
    private Quaternion initialRotation;
    private Quaternion targetLocalRotation;

    void Start()
    {
        initialRotation = transform.localRotation; // —охран€ем начальное вращение объекта в локальных координатах
        GenerateRandomTargetLocalRotation(); // √енерируем случайное целевое вращение в локальных координатах
    }

    void Update()
    {
        // ѕлавно мен€ем вращение к целевому вращению в локальных координатах
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetLocalRotation, rotationSpeed * Time.deltaTime);

        // ≈сли приблизились к целевому вращению, генерируем новое случайное целевое вращение в локальных координатах
        if (Quaternion.Angle(transform.localRotation, targetLocalRotation) < stabilize)
        {
            GenerateRandomTargetLocalRotation();
        }
    }

    // √енераци€ случайного целевого вращени€ в локальных координатах
    void GenerateRandomTargetLocalRotation()
    {
        float randomRotationX = Random.Range(-maxRotationX, maxRotationX);
        float randomRotationY = Random.Range(-maxRotationY, maxRotationY);
        targetLocalRotation = initialRotation * Quaternion.Euler(randomRotationX, randomRotationY, 0f);
    }
}
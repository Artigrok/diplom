using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitchScript : MonoBehaviour
{
    public GameObject[] weapons; // Массив объектов оружия

    void Start()
    {

    }

    void Update()
    {
        if (gameObject.GetComponent<PlayerLogic>().playerdied == false) { 
        // Проверяем нажатие клавиш 1-4
        for (int i = 0; i < weapons.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i)) // Alpha1 = 49, Alpha2 = 50, и т.д.
            {
                SwitchWeapon(i);
                break;
            }
        }
        }
    }

    void SwitchWeapon(int index)
    {
        // Отключаем все оружия
        foreach (GameObject weapon in weapons)
        {
            weapon.SetActive(false);
        }

        // Включаем оружие по индексу
        if (index >= 0 && index < weapons.Length)
        {
            weapons[index].SetActive(true);
            gameObject.GetComponent<PlayerLogic>().activeWeapon = weapons[index];
            weapons[index].GetComponent<Gun>().Modif_Crosshair_Switch();
            GameObject.FindGameObjectWithTag("UICanvas").GetComponent<PlayerInterface>().Gun = weapons[index];

        }
    }
}

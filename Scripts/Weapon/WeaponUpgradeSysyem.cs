using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUpgradeSysyem : MonoBehaviour
{
    public GameObject[] upgrade;
    [SerializeField] public bool[] upgraded;
    [SerializeField] public int upgradeLevel = 0;
    public int WeaponID;
    public GameObject magazine;
    // Start is called before the first frame update
    public void CallUpgrade()
    {
        // Проверяем, изменилось ли значение булевой переменной
        if (upgraded[upgradeLevel])
        {
            // Вызываем функцию
            Upgrade(upgradeLevel, WeaponID);
        }
    }
    public void Upgrade(int i, int ID) 
    {

        if (ID == 1) Upgrade_Uzi(i);
        else if (ID == 2) Upgrade_Shotgun(i);
        else Debug.Log("Недопустимое значение переменной ID: " + i);



    }
    void Upgrade_Uzi(int i) 
    {
        if (i == 0)
        {
            upgrade[0].SetActive(true);
            Gun wp = gameObject.GetComponent<Gun>();
            wp.accuracy *= 0.5f;
            wp.stabilize *= 5f;
            wp.gameObject.GetComponent<AudioSource>().pitch = 2.5f;
            wp.gameObject.GetComponent<AudioSource>().volume /= 5f;
            upgradeLevel++;
            wp.Modif_Crosshair();
        }
        else if (i == 1)
        {
            upgrade[1].SetActive(true);
            magazine.SetActive(true);
            Gun wp = gameObject.GetComponent<Gun>();
            wp.accuracy *= 0.8f;
            wp.Clip_Ammo_Config += 30;
            upgradeLevel++;
            wp.Modif_Crosshair();
        }
        else Debug.Log("Недопустимое значение переменной типа int: " + i);
    }
    void Upgrade_Shotgun(int i)
    {
        if (i == 0)
        {
            upgrade[0].SetActive(true);
            Shotgun wp = gameObject.GetComponent<Shotgun>();
            wp.damage *= 2;
            upgradeLevel++;
        }
        else if (i == 1)
        {
            upgrade[1].SetActive(true);
            Shotgun wp = gameObject.GetComponent<Shotgun>();
            wp.pellets *= 2;
            upgradeLevel++;
        }
        else Debug.Log("Недопустимое значение переменной типа int: " + i);
    }
}

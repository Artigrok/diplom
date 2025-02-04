using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerInterface : MonoBehaviour
{
    public GameObject Player;
    public GameObject Score;
    public GameObject Gun;
    public GameObject LevelManager;
    public GameObject PlayerUI;
    public GameObject Boss;
    private float HP;
    private float Armor;
    private float money;
    private float ammo;
    private int wave;
    private int room;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Score = GameObject.FindGameObjectWithTag("ScoreManager");
        Gun = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLogic>().activeWeapon;
        HP = -1;
        Armor= -1;
        money= -1;
        ammo= -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (HP != Player.GetComponent<PlayerLogic>().HP) 
        {
            HP = Player.GetComponent<PlayerLogic>().HP;
            PlayerUI.transform.GetChild(0).GetComponent<TMP_Text>().text="HP: "+HP.ToString("F" + 2);
        }
        if (Armor != Player.GetComponent<PlayerLogic>().armor)
        {
            Armor = Player.GetComponent<PlayerLogic>().armor;
            PlayerUI.transform.GetChild(1).GetComponent<TMP_Text>().text = "Armor: " + Armor.ToString("F" + 2);
        }
        if (money != Score.GetComponent<ScoreManager>().Money)
        {
            money = Score.GetComponent<ScoreManager>().Money;
            PlayerUI.transform.GetChild(2).GetComponent<TMP_Text>().text = "Money: " + money;
        }
        if (ammo != Gun.GetComponent<Gun>().Clip_Ammo)
        {
            ammo = Gun.GetComponent<Gun>().Clip_Ammo;
            PlayerUI.transform.GetChild(3).GetComponent<TMP_Text>().text = "Ammo: " + ammo.ToString("F" + 0); 
        }
        if (wave != LevelManager.GetComponent<NavigationBaker>().WaveNumber)
        {
            wave = LevelManager.GetComponent<NavigationBaker>().WaveNumber;
            PlayerUI.transform.GetChild(4).GetComponent<TMP_Text>().text = "Wave: " + wave;
        }
        if (room != LevelManager.GetComponent<NavigationBaker>().RoomNumber)
        {
            room = LevelManager.GetComponent<NavigationBaker>().RoomNumber;
            if (room == 0) { PlayerUI.transform.GetChild(5).GetComponent<TMP_Text>().text = "Room: Boss"; }
            else PlayerUI.transform.GetChild(5).GetComponent<TMP_Text>().text = "Room: " + room;
          
        }
    }

}

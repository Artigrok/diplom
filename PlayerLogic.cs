using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    public bool playerdied = false;
    public float HP;
    public float dodge;
    public float armor;
    public GameObject woundEffect;
    public GameObject activeWeapon;
    public float HP_Config;
    public float dodge_Config;
    public float armor_Config;
    public GameObject d_text;
    public AudioClip woundsound;
    // float woundKD = 0;
    // Start is called before the first frame update
    void Start()
    {
        HP_Config =100f + GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().Bonus_HP;
        HP = HP_Config;
        armor_Config = 200f + GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().Bonus_Armor;
        dodge = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().Bonus_Dodge;
        gameObject.GetComponent<PlayerMovement>().speed = gameObject.GetComponent<PlayerMovement>().ArmorSpeedDebuff(armor);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayerTakeDamage(float damage)
    {
     //   woundEffect.SetActive(true);
        damage = ArmorAbsorb(damage);
        HP -= damage;
        gameObject.GetComponent<AudioSource>().PlayOneShot(woundsound);
        if (HP < 0) HP = 0;
        if (HP <= 0 && playerdied == false) PlayerDie();
    }
    public void PlayerDie()
    {
      //  GameObject.FindGameObjectWithTag("GOText").GetComponent<TextMeshProUGUI>().text = string.Format("Game over");
        if (GameObject.FindGameObjectWithTag("Weapon").activeSelf == true) GameObject.FindGameObjectWithTag("Weapon").SetActive(false);
        GetComponent<PlayerMovement>().enabled = false;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerMouseLook>().enabled = false;
        playerdied = true;
        Debug.Log("Player Dead");
        d_text.SetActive(true);

    }
    public void PlayerGetRayCast(float damage) 
    {
        if (Random.Range(0, 100) >= dodge) 
        { Debug.Log("RayCast HIT"); PlayerTakeDamage(damage); } 
        else { Debug.Log("RayCast MISSED"); }
    }
    float ArmorAbsorb(float damage) 
    {
        float absorb=damage*armor/armor_Config;
        armor-=absorb;
        gameObject.GetComponent<PlayerMovement>().speed = gameObject.GetComponent<PlayerMovement>().ArmorSpeedDebuff(armor);
        damage -=absorb;
        return damage;
    }
}

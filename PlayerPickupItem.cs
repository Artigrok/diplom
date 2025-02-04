using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickupItem : MonoBehaviour
{
    [SerializeField] private float HP_Pickup;
    [SerializeField] private float armor_Pickup;
    [SerializeField] private float ammo_Pickup;
    [SerializeField] private int price;
    public float chance;
    public GameObject text;
    // Start is called before the first frame update
    void Start()
    {
        chance *= GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().chance_multi;
        float f = Random.Range(0.01f, 1f);
        Debug.Log(f);
        if (f > chance) Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") == true )
        {
            text.SetActive(true);
            if (Input.GetButton("Use") && GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().Money >= price)
            {
            if (HP_Pickup != 0) { other.GetComponent<PlayerLogic>().HP += HP_Pickup;if (other.GetComponent<PlayerLogic>().HP > other.GetComponent<PlayerLogic>().HP_Config) other.GetComponent<PlayerLogic>().HP = other.GetComponent<PlayerLogic>().HP_Config; }
            if (armor_Pickup != 0) { other.GetComponent<PlayerLogic>().armor += armor_Pickup; if (other.GetComponent<PlayerLogic>().armor > other.GetComponent<PlayerLogic>().armor_Config) other.GetComponent<PlayerLogic>().armor = other.GetComponent<PlayerLogic>().armor_Config; }
            if (ammo_Pickup != 0) { other.GetComponent<PlayerLogic>().activeWeapon.GetComponent<Gun>().Clip_Ammo += ammo_Pickup/100* other.GetComponent<PlayerLogic>().activeWeapon.GetComponent<Gun>().Clip_Ammo_Config; if (other.GetComponent<PlayerLogic>().activeWeapon.GetComponent<Gun>().Clip_Ammo > other.GetComponent<PlayerLogic>().activeWeapon.GetComponent<Gun>().Clip_Ammo_Config) other.GetComponent<PlayerLogic>().activeWeapon.GetComponent<Gun>().Clip_Ammo = other.GetComponent<PlayerLogic>().activeWeapon.GetComponent<Gun>().Clip_Ammo_Config; }
            GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().Money -= price;
            Destroy(gameObject);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") == true) text.SetActive(false);
    }
}

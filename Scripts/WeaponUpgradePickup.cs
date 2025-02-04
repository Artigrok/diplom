using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUpgradePickup : MonoBehaviour
{
    public Transform[] spawns;
    public float chance;
    // Start is called before the first frame update
    void Start()
    {
        chance *= GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().chance_multi;
        float f = Random.Range(0.01f, 1f);
        Debug.Log(f);
        if (f <= chance) transform.position = spawns[Random.Range(0, spawns.Length)].position;
        if (f > chance) Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") == true) 
        { 
        if (other.GetComponent<PlayerLogic>().activeWeapon.GetComponent<WeaponUpgradeSysyem>().WeaponID < 3) 
        { 
            if (other.CompareTag("Player") == true && GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().Money>=1000) 
            {
            
                other.GetComponent<PlayerLogic>().activeWeapon.GetComponent<WeaponUpgradeSysyem>().upgraded[other.GetComponent<PlayerLogic>().activeWeapon.GetComponent<WeaponUpgradeSysyem>().upgradeLevel] = true;
                other.GetComponent<PlayerLogic>().activeWeapon.GetComponent<WeaponUpgradeSysyem>().CallUpgrade();
                GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().Money -= 1000;
                Destroy(gameObject);
            }
        }
        }
    }
}

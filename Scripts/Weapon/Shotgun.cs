using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Shotgun : Gun
{
    public int pellets;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.keepAnimatorStateOnDisable = true;
        pellets += GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().Bonus_shotgun;
        Clip_Ammo_Config *= 1+(GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().Bonus_Ammo_Clip/100);
        Clip_Ammo_Config= Mathf.Round(Clip_Ammo_Config);
        Clip_Ammo = Clip_Ammo_Config;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(shootPosition.transform.position, shootPosition.transform.forward * range, Color.red);
        Physics.Raycast(shootPosition.transform.position, shootPosition.transform.forward, out RaycastHit hit, range, m_Mask.value, QueryTriggerInteraction.Ignore);
        raznica = Camera.main.WorldToScreenPoint(hit.point);
        CrossHair.transform.position = new Vector3(raznica.x, raznica.y, raznica.z);
        //  CrossHair.transform.position = hit.point;
        // if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().x != 0 || GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().z != 0)
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            animator.SetTrigger("Move");
        else animator.SetTrigger("NotMove");
        if (Input.GetButton("Fire1") && Time.time >= nextFire && Time.time >= Reloadtimer && GameObject.FindGameObjectWithTag("UICanvas").GetComponent<PauseMenu>().isPaused == false && Clip_Ammo>0)
        {

                nextFire = Time.time + 1f / fireRate;
                ShotgunShoot();
                Clip_Ammo--;
                //GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerUI>().ChangeAmmoText(Clip_Ammo);

        }
      //  if (Input.GetButton("Fire1") == false || Clip_Ammo <= 0) animator.SetTrigger("NotFire");
        /*if (Time.time < Reloadtimer) GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponChanger>().enabled = false; else GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponChanger>().enabled = true;*/
    }

    void ShotgunShoot() 
    {
        GetComponent<AudioSource>().PlayOneShot(Fire);
        for (int i = 0; i <= pellets; i++)
        {
 
        Vector3 direction = shootPosition.transform.forward;
        Vector3 spread = Vector3.zero;
        spread += shootPosition.transform.up * Random.Range(-1f, 1f); 
        spread += shootPosition.transform.right * Random.Range(-1f, 1f); 
        direction += spread.normalized * Random.Range(0f, 0.2f);
        //  Debug.Log("Transform: " + shootPosition.transform.position);
        if (Physics.Raycast(shootPosition.transform.position, shootPosition.transform.forward + spread.normalized * Random.Range(0f, 0.2f), out RaycastHit hit, range, m_Mask.value, QueryTriggerInteraction.Ignore))
        {
            Debug.DrawLine(shootPosition.transform.position, hit.point, Color.green, 1f);
            if (hit.collider.CompareTag("Enemy"))
            {
                // Debug.Log(hit.collider.name);
                BasicEnemyLogic target = hit.collider.GetComponent<BasicEnemyLogic>();
                target.TakeDamage(damage);
            }
        }
        
              GameObject effect = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
              Destroy(effect, 0.5f);
            // Effects
        }
        animator.SetTrigger("Fire");
        // Effects
        FireEffect.Play();
    }
}

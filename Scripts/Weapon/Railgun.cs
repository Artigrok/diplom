using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Railgun : Gun
{
    public int pierce_force = 10;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.keepAnimatorStateOnDisable = true;
        Clip_Ammo_Config *= 1 + (GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().Bonus_Ammo_Clip / 100);
        Clip_Ammo_Config = Mathf.Round(Clip_Ammo_Config);
        Clip_Ammo = Clip_Ammo_Config;
        pierce_force += GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().Bonus_railgun;
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
        if (Input.GetButton("Fire1") && Time.time >= nextFire && Time.time >= Reloadtimer && GameObject.FindGameObjectWithTag("UICanvas").GetComponent<PauseMenu>().isPaused == false && Clip_Ammo > 0)
        {

            nextFire = Time.time + 1f / fireRate;
            RailgunShoot();
            Clip_Ammo--;
            //GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerUI>().ChangeAmmoText(Clip_Ammo);

        }
    }
    void RailgunShoot() 
    {
        GetComponent<AudioSource>().PlayOneShot(Fire);
        RaycastHit[] hit = new RaycastHit[pierce_force];
        int numHits = Physics.RaycastNonAlloc(shootPosition.transform.position, shootPosition.transform.forward, hit, range, m_Mask.value, QueryTriggerInteraction.Ignore);
        for (int i = 0; i < numHits; i++)
        {
            if (hit[i].collider.CompareTag("Enemy"))
            {
                // Debug.Log(hit.collider.name);
                BasicEnemyLogic target = hit[i].collider.GetComponent<BasicEnemyLogic>();
                target.TakeDamage(damage);
            }
        }
        animator.SetTrigger("Fire");
        // Effects
        FireEffect.Play();
        // GameObject effect = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        //  Destroy(effect, 0.5f);
        // Effects
    }
}

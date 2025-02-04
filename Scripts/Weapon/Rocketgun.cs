using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocketgun : Gun
{
    public GameObject bulletPrefab;
    public float bulletForce = 20f;
    public float radius = 5f;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.keepAnimatorStateOnDisable = true;
    
        Clip_Ammo_Config *= 1 + (GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().Bonus_Ammo_Clip / 100);
        Clip_Ammo_Config = Mathf.Round(Clip_Ammo_Config);
        Clip_Ammo = Clip_Ammo_Config;
        radius += GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().Bonus_rocketgun;
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
            RocketShoot();
            Clip_Ammo--;
            //GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerUI>().ChangeAmmoText(Clip_Ammo);

        }
        //  if (Input.GetButton("Fire1") == false || Clip_Ammo <= 0) animator.SetTrigger("NotFire");
        /*if (Time.time < Reloadtimer) GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponChanger>().enabled = false; else GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponChanger>().enabled = true;*/
    }
    void RocketShoot()
    {
        GetComponent<AudioSource>().PlayOneShot(Fire);

        //  Debug.Log("Transform: " + shootPosition.transform.position);
            GameObject bullet = Instantiate(bulletPrefab, shootPosition.transform.position, shootPosition.transform.rotation); // Создаем пулю
            bulletPrefab.GetComponent<RocketgunAmmo>().damage = (int)damage;
        bulletPrefab.GetComponent<RocketgunAmmo>().explosionRadius = radius;
            Rigidbody rb = bullet.GetComponent<Rigidbody>(); // Получаем компонент Rigidbody2D пули

            if (rb != null)
            {
                rb.AddForce(shootPosition.transform.forward * bulletForce, ForceMode.Impulse); // Наносим импульс для движения пули
            }
            

            // GameObject effect = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            //  Destroy(effect, 0.5f);
            // Effects

        animator.SetTrigger("Fire");
        // Effects
        FireEffect.Play();
    }
}

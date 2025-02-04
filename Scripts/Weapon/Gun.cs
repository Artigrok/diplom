using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public AudioClip Fire;
    [HideInInspector] public Animator animator;
    public float damage = 10f;
    public float range = 100f;
    public Camera fpsCam;
    public GameObject shootPosition;
    public GameObject CrossHair;
    public Vector3 raznica;
    public ParticleSystem FireEffect;
    public GameObject impactEffect;
    public float fireRate = 5f;
    public float Reload = 0f;
    public float Clip_Ammo_Config;
    public float Clip_Ammo;
    [HideInInspector] public float nextFire = 0f;
    [HideInInspector] public float Reloadtimer = 0f;
    public LayerMask m_Mask = -1;
    [SerializeField] public float stabilize;
    [SerializeField] public float accuracy;
    [SerializeField] public float stabilize_validate;
    [SerializeField] public float accuracy_validate;
    public ShootPositionSwing _aim;
    // Start is called before the first frame update

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.keepAnimatorStateOnDisable = true;
        Clip_Ammo = Clip_Ammo_Config;
        _aim.maxRotationX = accuracy;
        _aim.maxRotationY = accuracy;
        _aim.rotationSpeed = 10/stabilize;
        _aim.stabilize= stabilize;
        accuracy_validate = accuracy;
        stabilize_validate = stabilize;
    }
    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(shootPosition.transform.position, shootPosition.transform.forward * range, Color.red) ;
        Physics.Raycast(shootPosition.transform.position, shootPosition.transform.forward, out RaycastHit hit, range, m_Mask.value, QueryTriggerInteraction.Ignore);
        raznica = Camera.main.WorldToScreenPoint(hit.point);
        CrossHair.transform.position = new Vector3(raznica.x , raznica.y , raznica.z);
      //  CrossHair.transform.position = hit.point;
        // if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().x != 0 || GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().z != 0)
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            animator.SetTrigger("Move");
        else animator.SetTrigger("NotMove");
        if (Input.GetButtonDown("Reload") && Clip_Ammo != Clip_Ammo_Config)
        {
            Reloading();
        }
        if (Input.GetButton("Fire1") && Time.time >= nextFire && Time.time >= Reloadtimer && GameObject.FindGameObjectWithTag("UICanvas").GetComponent<PauseMenu>().isPaused == false)
        {

            if (Clip_Ammo <= 0) Reloading();
            else
            {
                nextFire = Time.time + 1f / fireRate;
                Shoot();
                Clip_Ammo--;
                //GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerUI>().ChangeAmmoText(Clip_Ammo);
            }
        }
        if (Input.GetButton("Fire1") == false || Clip_Ammo <= 0) animator.SetTrigger("NotFire");
        /*if (Time.time < Reloadtimer) GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponChanger>().enabled = false; else GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponChanger>().enabled = true;*/
    }
    void Shoot()
    {
          GetComponent<AudioSource>().PlayOneShot(Fire);
      //  Debug.Log("Transform: " + shootPosition.transform.position);
        if (Physics.Raycast(shootPosition.transform.position, shootPosition.transform.forward, out RaycastHit hit, range, m_Mask.value, QueryTriggerInteraction.Ignore))
        {
           
            // 

            if (hit.collider.CompareTag("Enemy"))
            {
               // Debug.Log(hit.collider.name);
                BasicEnemyLogic target = hit.collider.GetComponent<BasicEnemyLogic>();
                target.TakeDamage(damage);
            }
        }
        animator.SetTrigger("Fire");
        // Effects
        FireEffect.Play();
        GameObject effect = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(effect, 0.5f);
        // Effects
    }
    void Reloading()
    {
        animator.SetTrigger("Reload");
        Reloadtimer = Time.time + Reload;
        Clip_Ammo = Clip_Ammo_Config;
        //GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerUI>().ChangeAmmoText(Clip_Ammo);
    }
    public void Modif_Crosshair()
    {
        // Проверяем, изменилось ли значение булевой переменной
        if (stabilize!=stabilize_validate)
        {
            
            _aim.rotationSpeed = 10 / stabilize;
            _aim.stabilize = stabilize;
            stabilize_validate = stabilize;
        }
        if (accuracy != accuracy_validate)
        {
            _aim.maxRotationX = accuracy;
            _aim.maxRotationY = accuracy;
            accuracy_validate = accuracy;
        }
    }
    public void Modif_Crosshair_Switch()
    {
        // Проверяем, изменилось ли значение булевой переменной


            _aim.rotationSpeed = 10 / stabilize;
            _aim.stabilize = stabilize;



            _aim.maxRotationX = accuracy;
            _aim.maxRotationY = accuracy;


    }
}
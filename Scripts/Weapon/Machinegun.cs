using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Machinegun : Gun
{
    public int temp;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.keepAnimatorStateOnDisable = true;
        Clip_Ammo_Config *= 1 + (GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().Bonus_Ammo_Clip / 100);
        Clip_Ammo_Config = Mathf.Round(Clip_Ammo_Config);
        Clip_Ammo = Clip_Ammo_Config;
        damage += GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().Bonus_machinegun;
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
            MachinegunShoot();
            Clip_Ammo--;
            //GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerUI>().ChangeAmmoText(Clip_Ammo);
        }
            if (Input.GetButton("Fire1") == false || Clip_Ammo <= 0) animator.SetTrigger("NotFire");
        /*if (Time.time < Reloadtimer) GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponChanger>().enabled = false; else GameObject.FindGameObjectWithTag("Player").GetComponent<WeaponChanger>().enabled = true;*/
    }
    void MachinegunShoot()
    {
        GetComponent<AudioSource>().PlayOneShot(Fire);
        //  Debug.Log("Transform: " + shootPosition.transform.position);
        Vector3 spread = Vector3.zero;
        spread += shootPosition.transform.up * Random.Range(-1f, 1f);
        spread += shootPosition.transform.right * Random.Range(-1f, 1f);
        if (Physics.Raycast(shootPosition.transform.position, shootPosition.transform.forward + spread.normalized * Random.Range(0f, 0.2f), out RaycastHit hit, range, m_Mask.value, QueryTriggerInteraction.Ignore))
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
    IEnumerator DecreaseVariable()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            temp--; // ”меньшаем переменную на 1
            Debug.Log("“екущее значение переменной: " + temp);

            // ћожно добавить условие дл€ прекращени€ уменьшени€, например:
            // if (variableToDecrease <= 0) {
            //     break;
            // }
        }
    }
}

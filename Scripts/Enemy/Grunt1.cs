using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grunt1 : BasicEnemyLogic
{
    public bool Support1_Affected;
    public bool Grunt1_shield;
    public GameObject Support1_Object;
    public Animator enemyAnim;
    public GameObject support1_affect_effect;
    public Material mat1;
    public ParticleSystem woundp;
    public GameObject dieEffect;
    private bool gained = false;
    private float berserk_speed=12f;
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        enemyAnim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
        if (nav.enabled == true) nav.SetDestination(Player.transform.position);

            if (Support1_Affected) 
            {
            support1_affect_effect.SetActive(true);
            }
            else support1_affect_effect.SetActive(false);
            sex += Time.deltaTime;
            dist = Vector3.Distance(Player.transform.position, transform.position);
            /* if (dist > rad)
             {
                 nav.enabled = false;
                 //enemy1anim.SetTrigger("Idle");
             }*/
            if (!Seeking)
            {
                if ((dist > attackrad))
                {
                    nav.enabled = true;
                    if (!Grunt1_shield) { nav.speed = berserk_speed; nav.angularSpeed = 1080f; nav.acceleration = 20f; }
                    nav.SetDestination(Player.transform.position);
                    enemyAnim.SetTrigger("Moving");
                }
                if (dist < attackrad)
                {
                    if (Grunt1_shield) { if (sex >= attackCD) Grunt1_EnemyAttaking(); }
                    else if (!Grunt1_shield) { if (sex >= (attackCD/2)) Grunt1_EnemyAttaking(); }

                    nav.enabled = false;
                    Vector3 targetDirection = Player.transform.position - transform.position; // Направление к цели
                    Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                    targetRotation.x = transform.rotation.x;
                    targetRotation.z = transform.rotation.z;
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 120f * Time.deltaTime);
                }
            }
            if (Seeking)
            {
                nav.enabled = true;
                nav.SetDestination(Player.transform.position);
            }
    }
    void Grunt1_EnemyAttaking()
    {
        enemyAnim.SetTrigger("Attacking");
        // Player.GetComponent<PlayerStatement>().PlayerTakeDamage(20);
        sex = 0;
        Debug.Log("He attack");
        Player.GetComponent<PlayerLogic>().PlayerTakeDamage(Damage);
        //  Player.GetComponent<PlayerUI>().HPChangeText();
    }
    public override void TakeDamage(float damage)
    {
      //  Debug.Log("damage");
        Health -= damage;
        woundp.Play();
        if (Health <= 0) 
        {
            if (Support1_Affected) { if (Support1_Object != null) { Support1_Object.GetComponent<Support1>().Support1_Grunt1_Deaffect(gameObject, 5, 5); } else Debug.Log("SAP1 PUSTOY!!!!!!!!!!"); }
            if (!gained) { 
            GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().Score++;
            GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().Money += 100;
                gained = true;
            }
            Instantiate(dieEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            
        }
    }

}

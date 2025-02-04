using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Support1 : BasicEnemyLogic
{
    public int Support1_Allies = 0;
    [SerializeField] private bool dead;
    [SerializeField] public List<GameObject> Allies_Grunt1;
    public LayerMask FireObstacles;
    public RaycastHit firehit;
    public Animator enemy1anim;
    public GameObject dieEffect;
    private bool gained = false;
    new void Start()
    {
        base.Start();
        enemy1anim= GetComponent<Animator>();
    }
    new void Update()
    {
        base.Update();
        if (nav.enabled == true) nav.SetDestination(Player.transform.position);

        sex += Time.deltaTime;
        dist = Vector3.Distance(Player.transform.position, transform.position);
        if (!Seeking)
        {
            if ((dist > attackrad))
            {
                nav.enabled = true;
                nav.SetDestination(Player.transform.position);
                enemy1anim.SetTrigger("Moving");
            }
            if (dist < attackrad)
            {
                if (sex >= attackCD) Support1_EnemyAttaking();
                
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
    void Support1_EnemyAttaking()
    {
        // Player.GetComponent<PlayerStatement>().PlayerTakeDamage(20);
        sex = 0;
       // Debug.Log("Support attack");
        if (Physics.Linecast(transform.position, Player.transform.position, out firehit, FireObstacles))
        {
            if (firehit.collider.CompareTag("Player") == false)
            {
                //Debug.Log();
                Debug.Log("blocked");

            }
            if (firehit.collider.CompareTag("Player") == true)
            {
                // Debug.Log("YA VISHSU");
                enemy1anim.SetTrigger("Attacking");
                Player.GetComponent<PlayerLogic>().PlayerGetRayCast(Damage);
            }
            //  Player.GetComponent<PlayerUI>().HPChangeText();
        }
    }
    private void Support1_Grunt1_Affect(GameObject _Ally, float rad, float kd) 
    {
        if (_Ally.CompareTag("Enemy"))
            if (_Ally.GetComponent<BasicEnemyLogic>().Grunt1) if (_Ally.GetComponent<Grunt1>().Support1_Affected == false)
            {
                _Ally.GetComponent<BasicEnemyLogic>().attackrad += rad;
                _Ally.GetComponent<BasicEnemyLogic>().attackCD += kd;
                Support1_Allies += 1;
                _Ally.GetComponent<Grunt1>().Support1_Affected = true;
                _Ally.GetComponent<Grunt1>().Support1_Object = gameObject;
                Debug.Log("Ally affected");
                Allies_Grunt1.Add(_Ally);
            }
    }
    public void Support1_Grunt1_Deaffect(GameObject _Ally, float rad, float kd)
    {
        if (_Ally == null)
        {
             Allies_Grunt1.Remove(_Ally);
        }
        if (_Ally != null) { 
        if (_Ally.CompareTag("Enemy"))
            if (_Ally.GetComponent<BasicEnemyLogic>().Grunt1) if (_Ally.GetComponent<Grunt1>().Support1_Affected == true)
            {
                _Ally.GetComponent<BasicEnemyLogic>().attackrad -= rad;
                _Ally.GetComponent<BasicEnemyLogic>().attackCD -= kd;
                Support1_Allies -= 1;
                _Ally.GetComponent<Grunt1>().Support1_Affected = false;
                _Ally.GetComponent<Grunt1>().Support1_Object = null;
                Debug.Log("Ally deaffected");
                if(!dead) Allies_Grunt1.Remove(_Ally);
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        //Debug.Log("Что-то есть");
        if (Support1)
        {
            GameObject Ally = other.gameObject;
            Support1_Grunt1_Affect(Ally, 0, 1);
        }
    }
    private void OnTriggerExit(Collider other)
    {

        if (Support1)
        {
            GameObject Ally = other.gameObject;
            Support1_Grunt1_Deaffect(Ally, 0, 1);
        }
    }
    public override void TakeDamage(float damage)
    {
        if (Support1) { 
       // Debug.Log("damage");
        Health -= damage - 0.01f*Support1_Allies;
        if (Health <= 0) 
        {
            dead = true;
                for (int i = 0; i < Allies_Grunt1.Count; i++) 
            {
                    GameObject _Grunts1 = Allies_Grunt1[i];
                    Support1_Grunt1_Deaffect(_Grunts1, 0, 1);
            }
                if (!gained) { 
                GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().Score++;
                GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().Money+=150;
                gained = true;
                }
                Instantiate(dieEffect, transform.position, Quaternion.identity);
                Destroy(gameObject);
        }
        }
    }
}

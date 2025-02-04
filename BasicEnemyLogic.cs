using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//using static UnityEngine.GraphicsBuffer;

public class BasicEnemyLogic : MonoBehaviour
{
    public NavMeshAgent nav;
    public GameObject Player;

    public bool Seeking = false;

    public bool Grunt1;

    public bool Support1;

    public bool Boss1;

    public float dist;
    public float attackrad;
    private RaycastHit hitInfo;
    public LayerMask LineObstacles;
   // public float rad;
    public float attackCD;
    public float sex = 0;

    public float Health;
    public float Damage;
    public float FindTimer;
    // Start is called before the first frame update
    protected void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    protected void Update()
    {
        if (FindTimer >= 2f) { 
        if (Physics.Linecast(transform.position, Player.transform.position, out hitInfo, LineObstacles))
        {
            if (hitInfo.collider.CompareTag("Player") == false)
            {
                //Debug.Log("blocked");
                Seeking = true;
            }
            if (hitInfo.collider.CompareTag("Player") == true)
            {
                // Debug.Log("YA VISHSU");
                Seeking = false;
            }


        }
            FindTimer = 0f;
        }
        FindTimer+= Time.deltaTime;
    }
    public virtual void TakeDamage(float damage)
    {

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RoomManager : MonoBehaviour
{
    public bool activate = false;
    public bool bossexit = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerStay(Collider Player) 
    {
        if (Player.CompareTag("Player") && Input.GetButton("Use"))
        {

            if (!activate) 
            {
               if(!bossexit) gameObject.GetComponentInParent<Transform>().GetComponentInParent<NavigationBaker>().SecondShtuka(); activate = true;
                if (bossexit)
                {
                    GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().total_Score += GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().Score * 2;
                    GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().Score = 0;
                    GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().Money = 0;
                    GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().Save();
                    SceneManager.LoadScene("MainMenu");
                    Cursor.lockState = CursorLockMode.None;
                    activate = true;
                }
            }
        }
    }
}

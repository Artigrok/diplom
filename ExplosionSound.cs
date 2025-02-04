using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSound : MonoBehaviour
{
    public float timer = 3f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(tmr());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator tmr()
    {

        //returning 0 will make it wait 1 frame
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
        //code goes here


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective1 : MonoBehaviour
{
    public int Score;
    public int ObjectiveWave;
    public int WaveScore;
    // Start is called before the first frame update
    void Start()
    {
        //   Score = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().Score;
        WaveScore = gameObject.GetComponentInParent<Transform>().GetComponentInParent<NavigationBaker>().WaveNumber;
        ObjectiveWave = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().ObjectiveWaveMult;
    }
    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponentInParent<Transform>().GetComponentInParent<NavigationBaker>().WaveNumber >= (WaveScore + ObjectiveWave-1)) { gameObject.GetComponent<WaveManager>().StageFinished = true; }
    }
}

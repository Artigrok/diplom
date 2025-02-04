using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuScore : MonoBehaviour
{
    public ScoreManager sc;
    public int score;
    public TMP_Text hp;
    public TMP_Text armor;
    public TMP_Text dodge;
    public TMP_Text Clip_Ammo;
    public TMP_Text shotgun;
    public TMP_Text machinegun;
    public TMP_Text railgun;
    public TMP_Text rocketgun;
    public TMP_Text waves;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelayedStart());
   
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator DelayedStart()
    {
        // ∆дем один кадр
        yield return null;
        sc = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
        score = sc.total_Score;
        gameObject.GetComponent<TMP_Text>().text = "Score: " + score.ToString();
        hp.text = "Bonus HP: " + sc.Bonus_HP.ToString();
        armor.text = "Bonus Armor: " + sc.Bonus_Armor.ToString();
        dodge.text = "Bonus Dodge: " + sc.Bonus_Dodge.ToString();
        Clip_Ammo.text = "Bonus max Ammo: " + sc.Bonus_Ammo_Clip.ToString() + "%";
        shotgun.text = "Bonus pellets: " +sc.Bonus_shotgun.ToString();
        machinegun.text = "Bonus MG damage: " + sc.Bonus_machinegun.ToString();
        railgun.text = "Bonus rail force: " + sc.Bonus_railgun.ToString();
        rocketgun.text = "Bonus rocket area: " + sc.Bonus_rocketgun.ToString();
        waves.text = "Waves: " + sc.ObjectiveWaveMult;
    }
    public void Buy_HP() 
    {
        if (score > 0) 
        {
            sc.total_Score -= 1;
            sc.Bonus_HP++;
            sc.Save();
            StartCoroutine(DelayedStart());
        }
    }
    public void Buy_Armor()
    {
        if (score > 99)
        {
            sc.total_Score -= 100;
            sc.Bonus_Armor++;
            sc.Save();
            StartCoroutine(DelayedStart());
        }
    }
    public void Buy_Dodge()
    {
        if (score > 999)
        {
            sc.total_Score -= 1000;
            sc.Bonus_Dodge++;
            sc.Save();
            StartCoroutine(DelayedStart());
        }
    }
    public void Buy_Clip_Ammo()
    {
        if (score > 1999)
        {
            sc.total_Score -= 2000;
            sc.Bonus_Ammo_Clip++;
            sc.Save();
            StartCoroutine(DelayedStart());
        }
    }
    public void Buy_Shotgun() 
    {
        if (score > 3999)
        {
            sc.total_Score -= 4000;
            sc.Bonus_shotgun++;
            sc.Save();
            StartCoroutine(DelayedStart());
        }
    }
    public void Buy_Machinegun()
    {
        if (score > 3999)
        {
            sc.total_Score -= 4000;
            sc.Bonus_machinegun++;
            sc.Save();
            StartCoroutine(DelayedStart());
        }
    }
    public void Buy_Railgun()
    {
        if (score > 3999)
        {
            sc.total_Score -= 4000;
            sc.Bonus_railgun++;
            sc.Save();
            StartCoroutine(DelayedStart());
        }
    }
    public void Buy_Rocketgun()
    {
        if (score > 3999)
        {
            sc.total_Score -= 4000;
            sc.Bonus_rocketgun+=0.1f;
            sc.Save();
            StartCoroutine(DelayedStart());
        }
    }
    public void WavePlus()
    {
        sc.ObjectiveWaveMult++;
        waves.text = "Waves: " + sc.ObjectiveWaveMult;
    }
    public void WaveMinus()
    {
        if (sc.ObjectiveWaveMult>0) sc.ObjectiveWaveMult--;
        waves.text = "Waves: " + sc.ObjectiveWaveMult;
    }
}

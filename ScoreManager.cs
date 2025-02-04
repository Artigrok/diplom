using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class ScoreManager : MonoBehaviour
{
    private static ScoreManager instance;
    public int Score;
    public int Money;
    public float chance_multi;
    public int total_Score;
    public float Bonus_HP;
    public float Bonus_Armor;
    public float Bonus_Dodge;
    public float Bonus_Ammo_Clip;
    public int Bonus_shotgun;
    public int Bonus_machinegun;
    public int Bonus_railgun;
    public float Bonus_rocketgun;
    public int ObjectiveWaveMult;
    public GameObject MenuScore;
    [System.Serializable]
    public class PlayerData

    {
        public int score;
        public float Bonus_HP;
        public float Bonus_Armor;
        public float Bonus_Dodge;
        public float Bonus_Ammo_Clip;
        public int Bonus_shotgun;
        public int Bonus_machinegun;
        public int Bonus_railgun;
        public float Bonus_rocketgun;
    }

    private string savePath;

    void Start()
    {
        
        if (instance == null)
        {
            // Если нет, сохраняем ссылку на этот экземпляр
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Если экземпляр уже существует, уничтожаем этот объект
            Destroy(gameObject);
        }
        savePath = Application.persistentDataPath + "/save.json";
        Debug.Log(savePath);
        LoadMoney();
    }
    private void Awake()
    {
        
    }
    public void Save()
    {
        PlayerData data = new PlayerData();
        data.score = total_Score;
        data.Bonus_HP = Bonus_HP;
        data.Bonus_Armor = Bonus_Armor;
        data.Bonus_Dodge = Bonus_Dodge;
        data.Bonus_Ammo_Clip = Bonus_Ammo_Clip;
        data.Bonus_shotgun = Bonus_shotgun;
        data.Bonus_machinegun = Bonus_machinegun;
        data.Bonus_railgun = Bonus_railgun;
        data.Bonus_rocketgun = Bonus_rocketgun;
        string jsonData = JsonUtility.ToJson(data);
        File.WriteAllText(savePath, jsonData);
        Debug.Log(jsonData);
        
    }

    public PlayerData Load()
    {
        if (File.Exists(savePath))
        {
            string jsonData = File.ReadAllText(savePath);
            return JsonUtility.FromJson<PlayerData>(jsonData);
        }
        else
        {
            Debug.LogWarning("Save file not found!");
            return null;
        }
    }
    public void LoadMoney() 
    {
        PlayerData data = Load();
        total_Score = data.score;
        Bonus_HP = data.Bonus_HP;
        Bonus_Armor = data.Bonus_Armor;
        Bonus_Dodge = data.Bonus_Dodge;
        Bonus_Ammo_Clip = data.Bonus_Ammo_Clip;
        Bonus_shotgun= data.Bonus_shotgun;
        Bonus_machinegun= data.Bonus_machinegun;
        Bonus_railgun = data.Bonus_railgun;
        Bonus_rocketgun = data.Bonus_rocketgun;
        Debug.Log("LoadF");
    }
}

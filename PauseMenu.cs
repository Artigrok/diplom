using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;

    public bool isPaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        // Возобновляем игру
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    void Pause()
    {
        // Останавливаем игру
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;

    }

    public void LoadMainMenu()
    {
        // Загрузка главного меню
        Time.timeScale = 1f; // Устанавливаем нормальную скорость времени перед загрузкой меню
        // Здесь может быть код для загрузки сцены главного меню, например:
        GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().total_Score+= GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().Score;
        GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().Score = 0;
        GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().Money = 0;
        GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>().Save();
        SceneManager.LoadScene("MainMenu");
    }
}

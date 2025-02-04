using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    public void StartGame()
    {
        // При нажатии кнопки "Start" загружаем сцену с игрой
        SceneManager.LoadScene("SampleScene"); // замените "GameScene" на имя вашей сцены с игрой
    }

    public void QuitGame()
    {
        // При нажатии кнопки "Exit" выходим из приложения
        Application.Quit();
    }
}

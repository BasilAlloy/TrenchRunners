using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject ship;
    private bool isPaused;

    [Header("Main Menu Scene")]
    public string _mainMenuLevel;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;

            if (isPaused)
            {
                ActivateMenu();
            } else {
                DeactivateMenu();
            }
        }
    }


    public void ActivateMenu()
    {   
        Time.timeScale = 0;
        ship.GetComponent<Movement>().Pause();
        pauseMenuUI.SetActive(true);
    }

    public void DeactivateMenu()
    {
        Time.timeScale = 1;
        ship.GetComponent<Movement>().Resume();
        pauseMenuUI.SetActive(false);
        isPaused = false;
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(_mainMenuLevel);
    }
}

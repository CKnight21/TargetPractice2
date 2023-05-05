using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject ui;

    public static bool isPaused = true;

    private void Start()
    {
        ui.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
           PauseGame();
        }             
    }

    public void PauseGame()
    {
        isPaused = true;
        Debug.Log("isPaused");
        ui.SetActive(true);
        Time.timeScale = 0;   
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
    public void Resume()
    {
        ui.SetActive (false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        isPaused = false;
        Debug.Log("isResumed");
    }

    public void toggle()
    {
        ui.SetActive(!ui.activeSelf);

        if (ui.activeSelf)
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.Locked;

        }
        else
        {
            Time.timeScale = 1f;
        }
    }
    public void Restart()
    {
        toggle();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }
    public void Menu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}

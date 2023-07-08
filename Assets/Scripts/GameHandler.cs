using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    [SerializeField] GameObject enemyManager;
    [SerializeField] EnvironmentScroller environmentScroller;
    [SerializeField] GameObject gameOverPanel;

    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        
    }

    private void Update()
    {
        /*if (Input.GetMouseButtonDown(1))
        {
            ScreenCapture.CaptureScreenshot("gamephoto.png");

        }*/
    }



    public void StartTheGame()
    {
        enemyManager.SetActive(true);
        environmentScroller.enabled = true;
        playerController.isGameActive = true;
    }

    public void StopTheGame()
    {
        enemyManager.SetActive(false);
        environmentScroller.enabled = false;
        playerController.isGameActive = false;
        Time.timeScale = 0;
        Cursor.visible = true;
    }
    
    public void RestartTheGame()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void HandleOnFinishedGame()
    {
        StopTheGame();
        gameOverPanel.SetActive(true);
        gameOverPanel.GetComponent<Animator>().SetTrigger("PlayGameOver");
    }
}

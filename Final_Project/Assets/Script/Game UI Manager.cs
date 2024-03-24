using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    public GameObject UI_Pause;
    public GameObject UI_GameOver;
    public GameObject UI_GameisFinished;
    public GameObject UI_GameisFinishedLove;


    private enum GameUI_State
    {
        GamePlay, GamePause, GameOver, GameisFinished, GameisFinishedLove
    }

    GameUI_State currentState;
    // Start is called before the first frame update
    void Start()
    {
        SwitchUIState(GameUI_State.GamePlay);
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseUI();
        }
        if(PlayerController.instance.isDead)
        {
            StartCoroutine(delayGUIGameOver());
        }
        if (CheckWinner.instance.isWinner)
        {
            StartCoroutine(delayGUIGameFinished());
        }
        if (CheckWinner.instance.isWinner && GhostController.instance.isLove)
        {
            StartCoroutine(delayGUIGameFinishedLove());
        }
    }

    private void SwitchUIState(GameUI_State state)
    {
        UI_Pause.SetActive(false);
        UI_GameisFinished.SetActive(false);
        UI_GameisFinishedLove.SetActive(false);
        UI_GameOver.SetActive(false);

        Time.timeScale = 1.0f;

        switch (state)
        {
            case GameUI_State.GamePlay:
                break;
            case GameUI_State.GamePause:
                Time.timeScale = 0f;
                UI_Pause.SetActive(true);
                break;
            case GameUI_State.GameOver:
                Time.timeScale = 0f;
                UI_GameOver.SetActive(true);
                break;
            case GameUI_State.GameisFinished:
                Time.timeScale = 0f;
                UI_GameisFinished.SetActive(true);
                break;
            case GameUI_State.GameisFinishedLove:
                Time.timeScale = 0f;
                UI_GameisFinishedLove.SetActive(true);
                break;
        }
        currentState = state;
    }

    private void TogglePauseUI()
    {
        if(currentState == GameUI_State.GamePlay)
        {
            SwitchUIState(GameUI_State.GamePause);
        }
        else if(currentState == GameUI_State.GamePause)
        {
            SwitchUIState(GameUI_State.GamePlay);
        }
    }

    public void Button_MainMenu()
    {
        SceneManager.LoadScene("Main");
    }
    public void Button_Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Button_Resume()
    {
        SwitchUIState(GameUI_State.GamePlay);
    }

    IEnumerator delayGUIGameFinished()
    {
        yield return new WaitForSeconds(1f);
        SwitchUIState(GameUI_State.GameisFinished);
    }

    IEnumerator delayGUIGameFinishedLove()
    {
        yield return new WaitForSeconds(1f);
        SwitchUIState(GameUI_State.GameisFinishedLove);
    }

    IEnumerator delayGUIGameOver()
    {
        yield return new WaitForSeconds(3f);
        SwitchUIState(GameUI_State.GameOver);
    }
}

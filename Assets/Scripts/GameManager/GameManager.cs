using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

enum UIButton
{
    PauseBtn = 0,
    PlayBtn = 1,
}

public class GameManager : MonoBehaviour
{
    #region Variable
        #region UI
            [SerializeField] private GameObject gameOverUI;
            [SerializeField] private GameObject victoryUI;
            [SerializeField] private GameObject[] generateUI;
            [SerializeField] private GameObject[] player;
            [SerializeField] private GameObject levelPanel;
            [SerializeField] private TextMeshProUGUI[] levelValue;
        #endregion
        #region Level
            [SerializeField] private int curLevel;
            [SerializeField] private GameObject[] levelScenes;
        #endregion
        #region Audio
            [SerializeField] private AudioSource backgroundSound;
        #endregion
        #region const
            private const int TOTAL_LEVEL = 10;
        #endregion
        #region condition
            private bool isPause = false;
            private bool enableSound = true;
        #endregion
        #region Event
            public UnityEvent ResetCamTarget;
        #endregion
    #endregion

    private void Awake()
    {
        if(levelScenes.Length <= 0) return;

        LoadLevelScene();
    }

    private void LoadLevelScene()
    {
        for(int i = 0; i < levelScenes.Length; i++)
        {
            levelScenes[i].SetActive(false);
        }

        curLevel = PlayerPrefs.GetInt("levelAt");

        levelScenes[curLevel-1].SetActive(true);

        UpdateLevelValue();

        PlayerPrefs.Save();

        ResetCamTarget.Invoke();
    }

    public void LoadGameScene(int level) 
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);

        PlayerPrefs.DeleteKey("levelAt");

        PlayerPrefs.SetInt("levelAt", level);
    } 

    public void LoadMainMenuScene() => SceneManager.LoadScene("MainMenuScene", LoadSceneMode.Single);

    public void LoadCurrentLevel() => LoadGameScene(PlayerPrefs.GetInt("levelAt"));

    public void LoadNextLevel()
    {
        if(curLevel < PlayerPrefs.GetInt("curLevel")) return;

        int nextLevel = PlayerPrefs.GetInt("curLevel") + 1;

        if(nextLevel > TOTAL_LEVEL)
        {
            victoryUI.SetActive(true);
            PlayerPrefs.Save();
            return;
        }

        PlayerPrefs.SetInt("curLevel", nextLevel);

        PlayerPrefs.Save();
    }

    public void Pause()
    {
        isPause = !isPause;
        
        generateUI[(int) UIButton.PlayBtn].SetActive(isPause);

        EnableSound();

        Time.timeScale = isPause ? 0 : 1;
    }

    public void EnableSound()
    {
        enableSound = !enableSound;
        
        backgroundSound.enabled = enableSound;
    }


    public void GameOver()
    {
        gameOverUI.SetActive(true);

        for (int i = 0; i < player.Length; i++)
            player[i].SetActive(false);

        int index = (curLevel < 5) ? 0 : ((curLevel < 8) ? 1 : 2);

        player[index].SetActive(true);

        PlayerPrefs.Save();
    }

    public void Victory()
    {
        if(curLevel >= PlayerPrefs.GetInt("passedLevel") || !PlayerPrefs.HasKey("passedLevel")) PlayerPrefs.SetInt("passedLevel", curLevel);
        
        curLevel++;

        LoadNextLevel(); 
        if(curLevel > TOTAL_LEVEL) return;
        
        PlayerPrefs.SetInt("levelAt", curLevel);
        LoadLevelScene();
    }

    public void ResetTimeScale() => Time.timeScale = 1;

    private void UpdateLevelValue()
    {
        if(levelValue.Length <= 0) return;

        for(int i = 0; i < levelValue.Length; i++)
            levelValue[i].text = (curLevel < 10) ? "0" + curLevel : curLevel + "";
    }
}

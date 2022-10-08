using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Button[] level_Btns;
    [SerializeField] private Image[] lock_Btns;
    [SerializeField] private Image[] crown_Btns;
    private int curLevel;
    private int passedLevel = 0;

    private void Awake() 
    {
        if(!PlayerPrefs.HasKey("curLevel")) PlayerPrefs.SetInt("curLevel", 1);

        curLevel = PlayerPrefs.GetInt("curLevel");

        UpdateLevel();
    } 

    public void UpdateLevel()
    {
        if(PlayerPrefs.HasKey("passedLevel")) passedLevel = PlayerPrefs.GetInt("passedLevel");
        
        for (int i = 0; i < level_Btns.Length; i++)
        {
            if (i + 1 <= curLevel) continue;

            level_Btns[i].interactable = false;
            lock_Btns[i].enabled = true;
        }

        for (int i = 0; i < level_Btns.Length; i++)
        {
            if(i < passedLevel) continue;

            crown_Btns[i].enabled = false;
        }
        
    }

    public void ResetLevel() 
    {
        PlayerPrefs.DeleteKey("curLevel");
        PlayerPrefs.DeleteKey("passedLevel");
        SceneManager.LoadScene("MainMenuScene", LoadSceneMode.Single);
    } 

    public void OpenFullLevel()
    {
        PlayerPrefs.SetInt("curLevel", 10);
        PlayerPrefs.SetInt("passedLevel", 10);
        SceneManager.LoadScene("MainMenuScene", LoadSceneMode.Single);
    }

}

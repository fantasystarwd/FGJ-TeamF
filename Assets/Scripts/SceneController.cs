using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour { 

    [SerializeField]
    private int nowSceneNumber = 0;

    [SerializeField]
    private KeyCode m_keyCode = KeyCode.Space;

    private void Awake()
    {
        nowSceneNumber = SceneManager.GetActiveScene().buildIndex;
    }

    public void ChangeNextScene()
    {
        nowSceneNumber++;
        if(nowSceneNumber >= SceneManager.sceneCountInBuildSettings)
        {
            nowSceneNumber = 0;
        }
        SceneManager.LoadSceneAsync(nowSceneNumber);
    }

    public void ChangeNextSceneByIndex(int sceneIndex)
    {
        nowSceneNumber = sceneIndex;
        if (nowSceneNumber >= SceneManager.sceneCountInBuildSettings)
        {
            nowSceneNumber = 0;
        }
        SceneManager.LoadSceneAsync(nowSceneNumber);
    }

    public int GetNowScene()
    {
        return nowSceneNumber;
    }
}
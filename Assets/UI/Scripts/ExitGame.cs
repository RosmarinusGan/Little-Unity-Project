using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitGame : MonoBehaviour
{
    public static string sceneName;

    public static bool needExit;

    private void Start()
    {
    }
    private void Update()
    {
        isExitScene();
    }

    //Fight�����°���ESCѡ��Exit������ ʧ�� ��ʤ �˳���Ϸ
    public static void exitGame()
    {
        needExit = true;
        if (sceneName == "FightLose")
        {
            SceneManager.UnloadSceneAsync("FightLose");
        }
        if(sceneName == "FightWin")
        {
            SceneManager.UnloadSceneAsync("FightWin");
        }
        if(sceneName == "ContinueFight")
        {
            Fight.isExit = false;
            SceneManager.UnloadSceneAsync("ContinueFight");
        }
    }

    //Fight�����°���ESCѡ��Continue
    public static void continueGame()
    {
        Fight.isExit = false;
        SceneManager.UnloadSceneAsync("ContinueFight");
    }

    //Fight�����°���ESC����ESC
    private void isExitScene()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Fight.isExit)
            {
                Fight.isExit = false;
                SceneManager.UnloadSceneAsync("ContinueFight");
            }
        }
    }
}

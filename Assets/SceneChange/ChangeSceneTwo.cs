using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeSceneTwo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ExitScene();
    }

    //开始游戏，加载Audio界面
    public void StartGame()
    {
        SceneManager.LoadSceneAsync("Audio");
    }

    //按下ESC后选择Continue
    public void StartGameTwo()
    {
        ChangeScene.isPushExit = false;
        SceneManager.UnloadSceneAsync("ContinueUI");
    }

    //按下ESC后再次按下ESC
    private void ExitScene()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (ChangeScene.isPushExit)
            {
                ChangeScene.isPushExit = false;
                SceneManager.UnloadSceneAsync("ContinueUI");
            }
        }
    }
}

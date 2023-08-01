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

    //��ʼ��Ϸ������Audio����
    public void StartGame()
    {
        SceneManager.LoadSceneAsync("Audio");
    }

    //����ESC��ѡ��Continue
    public void StartGameTwo()
    {
        ChangeScene.isPushExit = false;
        SceneManager.UnloadSceneAsync("ContinueUI");
    }

    //����ESC���ٴΰ���ESC
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

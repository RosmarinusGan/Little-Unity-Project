using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeScene : MonoBehaviour
{
    public static bool isPushExit;
    public static bool isFighting;

    public GameObject playerEvent;
    public GameObject playerCamera;
    // Start is called before the first frame update
    void Start()
    {
        isPushExit = false;
        isFighting = false;
    }

    // Update is called once per frame
    void Update()
    {
        OpenFight();
        CloseFight();
        CloseEvent();
    }

    //按下E触发对战界面
    private void OpenFight()
    {
        if(Input.GetKeyDown(KeyCode.E) && JudgeChangeScene.isCanChangeScene)
        {
            isFighting = true;
            SceneManager.LoadSceneAsync("FightUI", LoadSceneMode.Additive);
        }
    }

    //按下ESC触发ContinueUI界面
    private void CloseFight()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPushExit && !isFighting)
            {
                isPushExit = true;
                SceneManager.LoadSceneAsync("ContinueUI", LoadSceneMode.Additive);
            }
        }
    }

    private void CloseEvent()
    {
        if (isFighting)
        {
            playerEvent.SetActive(false);
            playerCamera.SetActive(false);
        }

        if (!isFighting)
        {
            playerEvent.SetActive(true);
            playerCamera.SetActive(true);
        }
    }
}

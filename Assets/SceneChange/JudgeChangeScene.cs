using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JudgeChangeScene : MonoBehaviour
{
    public static bool isCanChangeScene;

    public Text attackVisale;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            attackVisale.text = "按下E可进入对战状态";
            if (!isCanChangeScene)
            {
                isCanChangeScene = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            attackVisale.text = null;
            if (isCanChangeScene)
            {
                isCanChangeScene = false;
            }
        }
    }
}

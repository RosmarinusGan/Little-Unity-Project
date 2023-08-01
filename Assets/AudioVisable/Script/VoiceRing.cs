using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VoiceRing : MonoBehaviour
{
    public GameObject audioVis;

    public Button button;

    public Text text;

    public Sprite[] image = new Sprite[2];

    private bool isRing;
    // Start is called before the first frame update
    void Start()
    {
        audioVis.SetActive(false);
        isRing = false;

        button.onClick.AddListener(delegate
        {
            if (button.name == "VoiceRing")
            {
                isVoiceRing();
            }
        });
    }

    private void isVoiceRing()
    {
        if (!isRing)
        {
            Debug.Log("caonima");

            audioVis.SetActive(true);
            isRing = true;
            button.image.sprite = image[1];
            text.text = "环绕音环" + "\n" + "(开启)";
        }
        else
        {
            audioVis.SetActive(false);
            isRing = false;
            button.image.sprite = image[0];
            text.text = "环绕音环" + "\n" + "(关闭)";
        }
    }
}

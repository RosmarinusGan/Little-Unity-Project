using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AudioManager : MonoBehaviour
{
    //音乐相关信息显示
    public Text musicName;
    public Text nowTime;
    public Text totalTime;
    public Slider slide;

    public Button ButtonChange;
    public Sprite[] ContinueOrPause = new Sprite[2];

    //播放的音乐
    public AudioClip[] music;
    public int currentMusic = 0;

    //按钮集
    public Transform Buttons;

    //音乐源
    internal AudioSource musicPlay;

    //播放进度
    private int currentHour, currentMinute, currentSecond;
    private int clipHour, clipMinute, clipSecond;

    private bool isPlaying;

    static private Coroutine cor;

    public static AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = this;

        //musicPlay = GameObject.Find("GameScript").GetComponent<AudioSource>();
        musicPlay = GetComponent<AudioSource>();
        musicPlay.Stop();

        musicPlay.clip = music[currentMusic];

        foreach(Transform button in Buttons)
        {
            button.GetComponent<Button>().onClick.AddListener(delegate
            {
                switch (button.name)
                {
                    case "Continue":
                        ContinueMusic();
                        break;
                    case "Pause":
                        PauseMusic();
                        break;
                    case "Next":
                        NextMusic();
                        break;
                    case "Previous":
                        PreviousMusic();
                        break;
                }
            });
        }

    }

    // Update is called once per frame
    void Update()
    {
        NowTime();
        TotalTime();
        NowMusic();
    }

    public void ChangeSlide()
    {
        if (isPlaying)
        {
            musicPlay.time = slide.value * musicPlay.clip.length;
            StopCoroutine(cor);
            cor = StartCoroutine(PlayRound(musicPlay.clip.length - musicPlay.time));
        }
        else
        {
            musicPlay.time = slide.value * musicPlay.clip.length;
        }
    }

    private void UpdateSlide()
    {
        if (!SliderManager.isDrag)
        {
            slide.value = musicPlay.time / musicPlay.clip.length;
        }
    }

    private void NowTime()
    {
        currentHour = (int)musicPlay.time / 3600;
        currentMinute = (int)(musicPlay.time - currentHour * 3600) / 60;
        currentSecond = (int)(musicPlay.time - currentHour * 3600 - currentMinute * 60);
        nowTime.text = currentMinute + " : " + currentSecond;

        UpdateSlide();
    }

    private void TotalTime()
    {
        clipHour = (int)musicPlay.clip.length / 3600;
        clipMinute = (int)(musicPlay.clip.length - clipHour * 3600) / 60;
        clipSecond = (int)(musicPlay.clip.length - clipHour * 3600 - clipMinute * 60);

        totalTime.text = clipMinute + " : " + clipSecond;
    }

    private void NowMusic()
    {
        AudioClip clip = musicPlay.clip;

        string[] Name = clip.name.Split('-');

        musicName.text = Name[0];

        currentMusic = Array.IndexOf(music, clip);

    }

    //按钮接口函数
    public void PauseMusic()
    {
        musicPlay.Pause();
        isPlaying = false;
        ButtonChange.image.sprite = ContinueOrPause[0];
        ButtonChange.name = "Continue";
        StopCoroutine(cor);
    }

    public void ContinueMusic()
    {
        musicPlay.Play();
        isPlaying = true;
        ButtonChange.image.sprite = ContinueOrPause[1];
        ButtonChange.name = "Pause";
        cor = StartCoroutine(PlayRound(musicPlay.clip.length - musicPlay.time));
    }

    public void PreviousMusic()
    {
        if(currentMusic > 0)
        {
            currentMusic--;
        }
        else if(currentMusic == 0)
        {
            currentMusic = music.Length - 1;
        }

        musicPlay.clip = music[currentMusic];
        if (cor != null)
        {
            StopCoroutine(cor);
        }
        musicPlay.time = 0;
        musicPlay.Play();
        cor = StartCoroutine(PlayRound(musicPlay.clip.length - musicPlay.time));
    }

    public void NextMusic()
    {
        if(currentMusic < music.Length - 1)
        {
            currentMusic++;
        }else if(currentMusic == music.Length - 1)
        {
            currentMusic = 0;
        }
        musicPlay.clip = music[currentMusic];
        if (cor != null)
        {
            StopCoroutine(cor);
        }
        musicPlay.time = 0;
        musicPlay.Play();
        cor = StartCoroutine(PlayRound(musicPlay.clip.length - musicPlay.time));
    }

    public void ChangeVoice(float v)
    {
        musicPlay.volume = v;
    }

    IEnumerator PlayRound(float currentProcess)
    {
        yield return new WaitForSeconds(currentProcess);
        NextMusic();
    }
}

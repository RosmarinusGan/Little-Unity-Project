using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VoiceManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public static bool isDrag;

    private Slider slide;

    void Start()
    {
        slide = GetComponent<Slider>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDrag = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnMouseUp();
        isDrag = false;
    }

    void OnMouseUp()
    {
        AudioManager.audioManager.musicPlay.volume = 1 - slide.value;
    }

}

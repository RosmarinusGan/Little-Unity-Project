using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SliderManager : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public static bool isDrag;

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
        if(AudioManager.audioManager.slide.value == 1)
        {
            AudioManager.audioManager.NextMusic();
            return;
        }
        AudioManager.audioManager.ChangeSlide();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragImage : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Vector3 startPosition;
    private RectTransform imageTrans;
    private Vector3 offset;

    private bool isColl;
    private GameObject Target;
    private Image myImage;

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPosition = imageTrans.position;

        RectTransformUtility.ScreenPointToWorldPointInRectangle(imageTrans, eventData.position, Camera.main, out Vector3 worldPosi);
        offset = transform.position - worldPosi;
        SetPosition(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        SetPosition(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        ChangeGuesture();
        RecoverPosition();
    }

    // Start is called before the first frame update
    void Start()
    {
        myImage = GetComponent<Image>();
        imageTrans = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((this.tag == "ElementPlayer" && collision.tag == "ElementRandom") || (collision.tag == "ElementPlayer" && this.tag == "ElementRandom"))
        {
            if (!isColl) isColl = true;
            Target = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((this.tag == "ElementPlayer" && collision.tag == "ElementRandom") || (collision.tag == "ElementPlayer" && this.tag == "ElementRandom"))
        {
            if (isColl) isColl = false;
            Target = null;
        }
    }

    private void ChangeGuesture()
    {
        if (isColl)
        {
            if(Target.GetComponent<Image>().sprite == null)
            {
                Target.GetComponent<Image>().sprite = myImage.sprite;
                myImage.sprite = null;
            }   
        }
    }

    private void SetPosition(PointerEventData eventData)
    {
        if(RectTransformUtility.ScreenPointToWorldPointInRectangle(imageTrans, eventData.position, Camera.main, out Vector3 worldPosi))
        {
            imageTrans.position = offset + worldPosi;
        }
    }

    private void RecoverPosition()
    {
        imageTrans.position = startPosition;
    }
}

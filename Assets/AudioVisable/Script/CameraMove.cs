using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class CameraMove : MonoBehaviour
{
    public Canvas canvas;

    public Transform target;

    public Transform target2;

    public Transform start;

    private Camera _camera;

    private bool isMove;

    private bool isChangeSee;

    // Start is called before the first frame update
    void Start()
    {
        DOTween.Init();
        _camera = GetComponent<Camera>();
        _camera.transform.position = start.localPosition;
        canvas.transform.localScale = new Vector3(0.005f, 0f, 0.005f);
        isMove = false;
        isChangeSee = false;

    }

    // Update is called once per frame
    void Update()
    {
        TargetMove();
    }

    private void TargetMove()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (!isMove)
            {
                _camera.transform.DOLocalMove(target.localPosition, 1f, false);
                _camera.transform.DORotate(target.eulerAngles, 1f);
                canvas.transform.DOScale(new Vector3(0.005f, 0.005f, 0.005f), 1f);
                isMove = true;
            }
            else
            {
                _camera.transform.DOLocalMove(start.localPosition, 1f, false);
                _camera.transform.DORotate(start.eulerAngles, 1f);
                canvas.transform.DOScale(new Vector3(0.005f, 0f, 0.005f), 1f);
                isMove = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!isMove)
            {
                if (!isChangeSee)
                {
                    _camera.transform.DOLocalMove(target2.localPosition, 1f, false);
                    _camera.transform.DORotate(target2.eulerAngles, 1f);
                    isChangeSee = true;
                }
                else
                {
                    _camera.transform.DOLocalMove(start.localPosition, 1f, false);
                    _camera.transform.DORotate(start.eulerAngles, 1f);
                    isChangeSee = false;
                }
            }
        }
    }

}

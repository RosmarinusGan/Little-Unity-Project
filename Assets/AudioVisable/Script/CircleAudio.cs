using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleAudio : MonoBehaviour
{

    /// ��Դ
    public AudioSource _audio;

    /// ���Ƶ�����ݵ����鳤��    tips:���ȱ���Ϊ2��n�η�����С64�����8192(�����ĵ����ܳ���ס�Ļ�)
    [Range(64, 128 * 2)]
    public int _sampleLenght = 64 * 2;

    /// ��ƵƵ������
    private float[] _samples;

    /// UIList
    private List<GameObject> _uiList = new List<GameObject>();

    /// UI���ڵĸ�����
    public Transform _uiParentRect;

    /// ��Ƶ������Ԥ��
    public GameObject _prefab;

    /// ����ÿ������������һ��UI
    //public float _uiDistance;

    /// �½��ķ��ȱ�ֵ
    [Range(1, 30)]
    public float UpLerp = 12;

    public float Circle = 6;
    void Start()
    {
        //���ɲ���ȡȫ��UI
        CreatUI();

        _samples = new float[_sampleLenght];
    }



    /// ��̬����UI
    private void CreatUI()
    {
        for (int i = 0; i < _sampleLenght; i++)
        {
            GameObject _prefab_GO = Instantiate(_prefab, _uiParentRect.transform);
            //Ϊ���ɵ�ui����
            _prefab_GO.name = string.Format("Sample[{0}]", i + 1);
            _uiList.Add(_prefab_GO);

            //����λ��
            Transform _rectTransform = _prefab_GO.GetComponent<Transform>();
            _rectTransform.Rotate(0, (i + 1) * 360 / _sampleLenght, 0, Space.Self);
            _rectTransform.transform.position += _rectTransform.transform.forward * Circle;
        }
    }

    void Update()
    {
        //��ȡƵ��
        _audio.GetSpectrumData(_samples, 0, FFTWindow.BlackmanHarris);
        //ѭ��
        for (int i = 0; i < _uiList.Count; i++)
        {
            //ʹ��Mathf.Clamp���м�λ�õĵ�y������һ����Χ���������
            //Ƶ��ʱԽ���ԽС�ģ�Ϊ�����������ݱ仯�����ԣ���������samples[i]ʱ������50+i * i*0.5f
            Vector3 _v3 = _uiList[i].transform.localScale;
            _v3 = new Vector3(1, Mathf.Clamp(_samples[i] * (50 + i * i * 0.5f), 0, 50), 1);
            _uiList[i].transform.localScale = Vector3.Lerp(_uiList[i].transform.localScale, _v3, Time.deltaTime * UpLerp);
        }
    }


}
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Audio : MonoBehaviour
{

    /// 声源
    public AudioSource _audio;

    /// 存放频谱数据的数组长度    tips:长度必须为2的n次方，最小64，最大8192(如果你的电脑能承受住的话)
    [Range(64, 128 * 2)]
    public int _sampleLenght = 64 * 2;

    /// 音频频率数组
    private float[] _samples;

    /// UIList
    private List<Image> _uiList = new List<Image>();

    /// UI所在的父物体
    public RectTransform _uiParentRect;

    /// 音频波动条预设
    public GameObject _prefab;

    /// 设置每隔多大距离生成一个UI
    public float _uiDistance;

    /// 下降的幅度比值
    [Range(1, 30)]
    public float UpLerp = 12;


    void Start()
    {
        //生成并获取全部UI
        CreatUI();
        _samples = new float[_sampleLenght];
    }



    /// 动态生成UI
    private void CreatUI()
    {
        for (int i = 0; i < _sampleLenght; i++)
        {
            GameObject _prefab_GO = Instantiate(_prefab, _uiParentRect.transform);
            //为生成的ui命名
            _prefab_GO.name = string.Format("Sample[{0}]", i + 1);
            _uiList.Add(_prefab_GO.GetComponent<Image>());
            RectTransform _rectTransform = _prefab_GO.GetComponent<RectTransform>();
            //设置位置
            _rectTransform.localPosition = new Vector3(_rectTransform.sizeDelta.x + _uiDistance * i, 0, 0);
        }
    }

    void Update()
    {
        //获取频谱
        _audio.GetSpectrumData(_samples, 0, FFTWindow.BlackmanHarris);
        //循环
        for (int i = 0; i < _uiList.Count; i++)
        {
            //使用Mathf.Clamp将中间位置的的y限制在一定范围，避免过大
            //频谱时越向后越小的，为避免后面的数据变化不明显，故在扩大samples[i]时，乘以50+i * i*0.5f
            Vector3 _v3 = _uiList[i].transform.localScale;
            _v3 = new Vector3(1, Mathf.Clamp(_samples[i] * (50 + i * i * 0.5f), 0, 50), 1);
            _uiList[i].transform.localScale = Vector3.Lerp(_uiList[i].transform.localScale, _v3, Time.deltaTime * UpLerp);
        }
    }


}
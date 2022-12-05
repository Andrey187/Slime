using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private string _volumeParameter;

    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private Slider _slider;
    [SerializeField] private Toggle _toggle;

    private bool _disableToggleEvent;
    private const float _multiplier = 20f;

    private void Awake()
    {
        _slider.onValueChanged.AddListener(HandleSliderValudeChanged);
        _toggle.onValueChanged.AddListener(HableToggleValueChanged);
    }

    private void Start()
    {
        _slider.value = PlayerPrefs.GetFloat(_volumeParameter, _slider.value);
    }

    public void HandleSliderValudeChanged(float value)
    {
        _mixer.SetFloat(_volumeParameter, Mathf.Log10(value) * _multiplier);
        _disableToggleEvent = true;
        _toggle.isOn = _slider.value > _slider.minValue;
        _disableToggleEvent = false;
    }

    public void OnValueChanged(string name)
    {
       Debug.Log(this.gameObject.name); 
    }
    private void HableToggleValueChanged(bool enableSound)
    {
        if (_disableToggleEvent)
            return;
    
        if (enableSound)
            _slider.value = _slider.maxValue;
        else
            _slider.value = _slider.minValue;
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(_volumeParameter, _slider.value);
    }
}
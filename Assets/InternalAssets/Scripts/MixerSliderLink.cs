using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class MixerSliderLink : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private string _mixerParameter;
    private Slider _slider;

    private float m_volumeValue;
    private const float _multiplier = 80f;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _slider.onValueChanged.AddListener(SliderValueChange);
    }

    private void SliderValueChange(float value)
    {
        m_volumeValue = Mathf.Log10(value) * _multiplier;
        _mixer.SetFloat(_mixerParameter, m_volumeValue);
    }

    private void Start()
    {
        m_volumeValue = PlayerPrefs.GetFloat(_mixerParameter, Mathf.Log10(_slider.value) * _multiplier);
        _slider.value = Mathf.Pow(10f, m_volumeValue / _multiplier);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(_mixerParameter, m_volumeValue);
    }
}
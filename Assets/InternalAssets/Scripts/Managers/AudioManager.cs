using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance
    {
        get
        {
            if (s_Instance != null)
                return s_Instance;

            s_Instance = FindObjectOfType<AudioManager>();

            return s_Instance;
        }
    }

    protected static AudioManager s_Instance;

    [SerializeField] private AudioMixerGroup[] _mixerGroup = {_musicOutput, _effectOutput };

    [Header("Music Settings")]
    [SerializeField] private AudioClip _musicAudioClip;
    [SerializeField] private bool _musicPlayOnAwake = true;
    private static AudioMixerGroup _musicOutput;
    protected AudioSource m_MusicAudioSource;
    
    [Header("Effect Settings")]
    [SerializeField] public AudioClip[] _effectAudioClip;
    private static AudioMixerGroup _effectOutput;
    public AudioSource m_EffectAudioSource { get; set; }

    protected string[] _volimeParameter = {"Music", "SoundEffect"};
    protected bool m_TransferMusicTime, m_TransferEffectTime;
    protected AudioManager m_OldInstanceToDestroy = null;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            
            if (Instance._musicAudioClip == _musicAudioClip)
            {
                m_TransferMusicTime = true;
            }
            for(int i = 0; i < _effectAudioClip.Length; i++)
            {
                if (Instance._effectAudioClip == _effectAudioClip)
                {
                    m_TransferEffectTime = true;
                }
            }
            m_OldInstanceToDestroy = Instance;
        }

        s_Instance = this;

        DontDestroyOnLoad(gameObject);

        m_MusicAudioSource = gameObject.AddComponent<AudioSource>();
        m_MusicAudioSource.clip = _musicAudioClip;
        m_MusicAudioSource.outputAudioMixerGroup = _mixerGroup[0];
        m_MusicAudioSource.loop = true;

        if (_musicPlayOnAwake)
        {
            StartCoroutine(StartMusic());
        }
        for (int i = 0; i < _effectAudioClip.Length; i++)
        {
            m_EffectAudioSource = gameObject.AddComponent<AudioSource>();
            m_EffectAudioSource.clip = _effectAudioClip[i];
            m_EffectAudioSource.outputAudioMixerGroup = _mixerGroup[1];
            m_EffectAudioSource.playOnAwake = false;
            m_EffectAudioSource.loop = false;
        }
    }
    private void Start()
    {
        foreach (var value in _volimeParameter)
        {
            var volumeValue = PlayerPrefs.GetFloat(value);
            foreach (AudioMixerGroup mixer in _mixerGroup)
            {
                mixer.audioMixer.SetFloat(value, volumeValue);
            }
        }

        if (m_OldInstanceToDestroy != null)
        {
            for (int i = 0; i < _effectAudioClip.Length; i++)
            {
                if (m_TransferEffectTime) m_EffectAudioSource.timeSamples = m_OldInstanceToDestroy.m_EffectAudioSource.timeSamples;
                if (m_TransferMusicTime) m_MusicAudioSource.timeSamples = m_OldInstanceToDestroy.m_MusicAudioSource.timeSamples;
                m_OldInstanceToDestroy.Stop();
                Destroy(m_OldInstanceToDestroy.gameObject);
            }
        }
    }

    private IEnumerator StartMusic()
    {
        yield return new WaitForSeconds(0.3f);
        m_MusicAudioSource.time = 0.5f;
        m_MusicAudioSource.Play();
    }

    public void Stop()
    {
        StopJustAmbient();
        StopJustMusic();
    }

    public void StopJustMusic()
    {
        m_MusicAudioSource.Stop();
    }

    public void StopJustAmbient()
    {
        m_EffectAudioSource.Stop();
    }
}

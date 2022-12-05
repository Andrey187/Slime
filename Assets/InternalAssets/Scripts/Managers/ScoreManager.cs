using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public ScriptManager _scriptManager;
    private int currentScore = 0;
    [SerializeField]private AudioClip nomSound;
    [SerializeField] private AudioClip badNomSound;

    private void Awake()
    {
        EventManager.OnGateActivate.AddListener(AddScore);
        nomSound = AudioManager.Instance._effectAudioClip[1];
        badNomSound = AudioManager.Instance._effectAudioClip[2];
    }

    private void AddScore(int scoreAmount, bool isBad, Color vegeColor)
    {
        if (!isBad)
        {
            currentScore += scoreAmount;
            _scriptManager._playerTail.AddBodyPart(vegeColor);
            AudioManager.Instance.m_EffectAudioSource.clip = nomSound;
            AudioManager.Instance.m_EffectAudioSource.time = 0.6f;
            AudioManager.Instance.m_EffectAudioSource.Play();
        }
        else
        {
            _scriptManager._playerTail.RemoveBodyPart();
            AudioManager.Instance.m_EffectAudioSource.clip = badNomSound;
            AudioManager.Instance.m_EffectAudioSource.time = 2f;
            AudioManager.Instance.m_EffectAudioSource.Play();
        }

    }
}

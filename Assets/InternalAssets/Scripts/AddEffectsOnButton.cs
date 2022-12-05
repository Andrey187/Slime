using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AddEffectsOnButton : MonoBehaviour
{
    [SerializeField] private Button[] _buttons;
    [SerializeField] private AudioSource _effectSourse;
    [SerializeField] private AudioClip[] _effectClip;

    private void Start()
    {
        for (int i = 0; i < _effectClip.Length; i++)
        {
            _effectSourse = AudioManager.Instance.m_EffectAudioSource;
            _effectClip[i] = AudioManager.Instance._effectAudioClip[i];
            _effectSourse.clip = _effectClip[i];
        }

        if (SceneManager.GetSceneByName("UIMenu").isLoaded)
        {
            foreach (Button button in _buttons)
            {
                button.onClick.AddListener(() => _effectSourse.PlayOneShot(_effectClip[0]));
            }
        }
    }
}

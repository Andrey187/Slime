using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ButtonSettings : MonoBehaviour
{
    [SerializeField] private int _levelNumber;
    [SerializeField] private string _textLevel;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private int _select;
    [SerializeReference] private Settings _settings;
    
    private void Start()
    {
        switch (_select)
        {
            case 0:
                _text.text = _textLevel + " " + _levelNumber.ToString();
                break;
            case 1:
                _text.text = _textLevel.ToString();
                break;
        }
    }

    public void SwitchSettings(ButtonSettings settings)
    {
        switch (settings._settings)
        {
            case 0:
                Time.timeScale = 0f;
                break;
            case (Settings)1:
                ExitPause();
                break;
            case (Settings)2:
                RestartLevel();
                break;
            case (Settings)3:
                OpenScene(0);
                break;
            case (Settings)4:
                break;
            case (Settings)5:
                PlayerPrefs.DeleteKey("levelAt");
                LeveLSelection.instance.LevelInteractable();
                break;
        }
    }

    public void OpenScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
        ExitPause();
    }

    public void ExitPause()
    {
        ExitButton.ExitInstance.Unpause();
    }

    public void RestartLevel()
    {
        ExitPause();
        SceneController.RestartZone();
    }
}


public enum Settings
{
    Pause = 0,
    Resume = 1,
    Restart = 2,
    Exit = 3,
    NoFunction = 4,
    Reset = 5
}

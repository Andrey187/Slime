using UnityEngine;
using UnityEngine.UI;

public class LeveLSelection : MonoBehaviour
{
    public static LeveLSelection instance = null;
    [SerializeField] private Button[] _lvlButtons;
    private int levelAt;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        LevelInteractable();
    }

    public void LevelInteractable()
    {
        levelAt = PlayerPrefs.GetInt("levelAt", 1);
        for (int i = 0; i < _lvlButtons.Length; i++)
        {
            if (i + 1 > levelAt) { _lvlButtons[i].interactable = false; }
        }
    }
}

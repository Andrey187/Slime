using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    static protected ExitButton s_ExitButton;
    static public ExitButton ExitInstance { get { return s_ExitButton; } }

    protected bool m_InPause = false;

    private void Awake()
    {
        s_ExitButton = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneController.Instance.GetSceneName();
            if (!m_InPause)
            {
                m_InPause = true;
                Time.timeScale = 0;
                SceneManager.LoadSceneAsync("UIMenu", LoadSceneMode.Additive);
            }
            else
            {
                Unpause();
            }
        }
    }


    public void Unpause()
    {
        if (Time.timeScale > 0)
            return;

        StartCoroutine(UnpauseCoroutine());
    }

    protected IEnumerator UnpauseCoroutine()
    {
        Time.timeScale = 1;
        SceneManager.UnloadSceneAsync("UIMenu");
        Input.GetKeyDown(KeyCode.Escape);
        yield return new WaitForFixedUpdate();
        yield return new WaitForEndOfFrame();
        m_InPause = false;
    }

}



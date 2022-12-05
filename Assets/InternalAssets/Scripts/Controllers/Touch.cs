using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch : MonoBehaviour
{
    [SerializeField] private Canvas _startCanvas;
    [SerializeField] private Vector2 _startPos;
    [SerializeField] private Vector2 _direction;
    [SerializeField] private bool _directionChosen;

    private void Start()
    {
        _startCanvas.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            switch (Input.GetTouch(0).phase)
            {
                // Determine direction by comparing the current touch position with the initial one.
                case TouchPhase.Moved:
                    _direction = Input.GetTouch(0).position - _startPos;
                    break;

                // Report that a direction has been chosen when the finger is lifted.
                case TouchPhase.Ended:
                    _directionChosen = true;
                    break;
            }
        }
        if (_directionChosen)
        {
            _startCanvas.gameObject.SetActive(false);
            ExitButton.ExitInstance.Unpause();
        }
    }
}

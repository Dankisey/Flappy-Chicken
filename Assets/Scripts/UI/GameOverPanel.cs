using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private Ground _ground;

    private CanvasGroup _canvasGroup;

    private void TurnOn()
    {
        Time.timeScale = 0f;
        _canvasGroup.interactable = true;
        _canvasGroup.alpha = 1f;
    }

    private void TurnOff()
    {
        Time.timeScale = 1f;
        _canvasGroup.interactable = false;
        _canvasGroup.alpha = 0f;
    }

    private void OnPlayerFell()
    {
        TurnOn();
    }

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        TurnOff();
    }

    private void OnEnable()
    {
        _ground.PlayerFell += OnPlayerFell;
    }

    private void OnDisable()
    {
        _ground.PlayerFell -= OnPlayerFell;
    }
}
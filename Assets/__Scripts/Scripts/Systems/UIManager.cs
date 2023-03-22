using Euphrates;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI's")]
    [SerializeReference] GameObject _levelWonUI;
    [SerializeReference] GameObject _gameOverUI;
    [SerializeReference] GameObject _startUI;
    [SerializeReference] TextMeshProUGUI _currentLevelUI;
    [SerializeReference] TextMeshProUGUI _coinText;

    [Space, Header("Events")]
    [SerializeReference] TriggerChannelSO _start;
    [SerializeReference] TriggerChannelSO _win;
    [SerializeReference] TriggerChannelSO _lose;
    [SerializeReference] TriggerChannelSO _next;
    [SerializeReference] TriggerChannelSO _restart;

    [Space, Header("Game Values")]
    [SerializeReference] IntSO _currentLevelIndex;
    [SerializeReference] IntSO _coin;

    private void OnEnable()
    {
        _start.AddListener(HideStart);
        _win.AddListener(SetWin);
        _lose.AddListener(SetLose);
        _next.AddListener(SetStart);
        _restart.AddListener(SetStart);

        _coin.OnChange += CoinUpdate;
        _currentLevelIndex.OnChange += LevelUpdate;
    }

    private void OnDisable()
    {
        _start.RemoveListener(HideStart);
        _win.RemoveListener(SetWin);
        _lose.RemoveListener(SetLose);
        _next.RemoveListener(SetStart);
        _restart.RemoveListener(SetStart);

        _coin.OnChange -= CoinUpdate;
        _currentLevelIndex.OnChange -= LevelUpdate;
    }

    private void Start()
    {
        SetStart();
    }

    void SetStart()
    {
        LevelUpdate(0);
        HideWin();
        HideLose();

        _startUI.SetActive(true);
    }

    void HideStart() => _startUI.SetActive(false);

    void SetWin() => _levelWonUI.SetActive(true);

    void HideWin() => _levelWonUI.SetActive(false);

    void SetLose() => _gameOverUI.SetActive(true);

    void HideLose() => _gameOverUI.SetActive(false);

    void CoinUpdate(int _) => _coinText.text = _coin.ToString();

    void LevelUpdate(int _) => _currentLevelUI.text = $"Level: {_currentLevelIndex}";
}

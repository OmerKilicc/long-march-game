using Euphrates;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Events")]
    [SerializeReference] TriggerChannelSO _next;
    [SerializeReference] TriggerChannelSO _restart;

    [SerializeReference] IntSO _currentLevel;
    int _loadedSceneIndex = -1;

    private void OnEnable()
    {
        _next.AddListener(LoadNextLevel);
        _restart.AddListener(RestartLevel);
    }

    private void OnDisable()
    {
        _next.RemoveListener(LoadNextLevel);
        _restart.RemoveListener(RestartLevel);
    }

    void Start() => LoadStart();

    void LoadStart()
    {
        _currentLevel.Value = PlayerPrefs.HasKey("Level") ? PlayerPrefs.GetInt("Level") : 1;
        LoadScene(_currentLevel);
    }

    void LoadNextLevel()
    {
        _currentLevel.Value += 1;
        LoadScene(_currentLevel.Value);
        PlayerPrefs.SetInt("Level", _currentLevel);
        PlayerPrefs.Save();
    }

    void RestartLevel()
    {
        LoadScene(_currentLevel);
    }

    async void LoadScene(int index)
    {
        int count = SceneManager.sceneCountInBuildSettings;

        if (index > count - 1)
            index = Random.Range(1, count - 1);

        bool unloaded = true;

        if (_loadedSceneIndex != -1)
        {
            unloaded = false;

            // SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(0));
            SceneManager.UnloadSceneAsync(_loadedSceneIndex).completed += (_) => unloaded = true;
        }

        while (!unloaded)
            await Task.Yield();

        SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive)/*.completed += (_) => SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(index))*/;
        _loadedSceneIndex = index;
    }
}

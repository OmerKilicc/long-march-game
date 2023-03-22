using UnityEngine;
using UnityEngine.Events;
using Euphrates;

public class ChannelListener : MonoBehaviour
{
    [SerializeField] int _expectedInt = 0; 
    [SerializeReference] TriggerChannelSO _listened;
    [SerializeReference] IntChannelSO _listenedInt;
    public UnityEvent OnTrigger;

    private void OnEnable()
    {
        if (_listened != null)
            _listened.AddListener(Invoke);

        if (_listenedInt != null)
            _listenedInt.AddListener(Invoke);
    }

    private void OnDisable()
    {
        if (_listened != null)
            _listened.RemoveListener(Invoke);

        if (_listenedInt != null)
            _listenedInt.RemoveListener(Invoke);
    }

    public void Invoke(int val)
    {
        if (val != _expectedInt)
            return;

        Invoke();
    }

    public void Invoke() => OnTrigger.Invoke();
}

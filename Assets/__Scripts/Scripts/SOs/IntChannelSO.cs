using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Trigger Channel", menuName = "SO Channels/Int")]
public class IntChannelSO : ScriptableObject
{
	[SerializeField] UnityEvent<int> OnTrigger;

	public bool Silent { get; set; } = false;

    public void AddListener(UnityAction<int> listener) => OnTrigger.AddListener(listener);
	public void RemoveListener(UnityAction<int> listener) => OnTrigger.RemoveListener(listener);

    public void Invoke(int val)
    {
        if (Silent)
            return;

        OnTrigger?.Invoke(val);
    }
}

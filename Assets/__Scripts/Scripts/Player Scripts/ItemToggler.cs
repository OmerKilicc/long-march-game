using System.Collections.Generic;
using UnityEngine;

public class ItemToggler : MonoBehaviour
{
    [SerializeField] int _item = 0;

    [Header("Triggers"), Space]
    [SerializeField] IntChannelSO _activateTrigger;
    [SerializeField] IntChannelSO _deactivateTrigger;

    void OnEnable()
    {
        _activateTrigger.AddListener(Activate);
        _deactivateTrigger.AddListener(Deactivate);
    }

    void OnDisable()
    {
        _activateTrigger.RemoveListener(Activate);
        _deactivateTrigger.RemoveListener(Deactivate);
    }

    void Activate(int item) => Toggle(item, true);

    void Deactivate(int item) => Toggle(item, false);

    void Toggle(int item, bool state)
    {
        if (item != _item)
            return;

        transform.GetChild(0).gameObject.SetActive(state);
    }
}

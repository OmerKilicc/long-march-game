using TMPro;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    TextMeshPro _statText;

    // Start is called before the first frame update
    void Awake()
    {
        _statText = GetComponentInChildren<TextMeshPro>();
    }

    public void UpdateStats(int power)
    {
        _statText.text = "Level " + power.ToString();
    }
}

using System.Collections.Generic;
using UnityEngine;
using Euphrates;

public class PlayerUpgradeController : MonoBehaviour
{
    public List<UpgradeLineSO> upgradeLines;
    public List<int> IDList = new List<int> { 1, 2, 3, 4, 5, 6, 6, 6, 6 };

    public int playerPower = 1;

    //TextMeshPro playerStatText;

    [SerializeReference] IntChannelSO _upgrade;
    [SerializeReference] TriggerChannelSO _downgrade;
    [SerializeReference] IntChannelSO _activate;
    [SerializeReference] IntChannelSO _deactivate;
    [SerializeReference] TriggerChannelSO _lose;


    //Managers
    StatsManager statsManager;
    AudioManager audioManager;

    private void OnEnable()
    {
        _upgrade.AddListener(UpgradeLine);
        _downgrade.AddListener(DowngradeLine);
    }

    private void OnDisable()
    {
        _upgrade.RemoveListener(UpgradeLine);
        _downgrade.RemoveListener(DowngradeLine);
    }

    // Start is called before the first frame update
    void Start()
    {
        statsManager = GetComponent<StatsManager>();
        audioManager = FindObjectOfType<AudioManager>();

        statsManager.UpdateStats(playerPower);

        foreach (var item in upgradeLines)
            item.Index = 0;
    }

    private void FixedUpdate()
    {
        if (playerPower <= 0) 
        {
            _lose.Invoke();
        }
    }

    int _lastUpgradedLine = 0;
    public void UpgradeLine(int line)
    {
        foreach (var item in upgradeLines)
        {
            if (item.LineId != line)
                continue;

            if (item.Index > item.ItemIds.Count - 2)
                return;

            _deactivate.Invoke(item.ItemIds[item.Index++]);
            _activate.Invoke(item.ItemIds[item.Index]);
            playerPower++;
            audioManager.Play("Upgrade");
            _lastUpgradedLine = item.LineId;
        }
        statsManager.UpdateStats(playerPower);
    }

    void DowngradeLine()
    {
        foreach (var item in upgradeLines)
        {
            if (item.LineId != _lastUpgradedLine)
                continue;

            item.Index = Mathf.Clamp(item.Index, 0, item.ItemIds.Count - 1);

            _deactivate.Invoke(item.ItemIds[item.Index]);
            playerPower--;
            audioManager.Play("Downgrade");
            statsManager.UpdateStats(playerPower);
            if (item.Index == 0) 
                return;
            
            _activate.Invoke(item.ItemIds[--item.Index]);
            return;
        }
        playerPower = 0;
    }
}

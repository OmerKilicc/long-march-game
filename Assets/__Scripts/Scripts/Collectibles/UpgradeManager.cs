using UnityEngine;


public class UpgradeManager : MonoBehaviour
{
    [SerializeField] int _upgradeId;
    [SerializeField] IntChannelSO _upgrade;

    void OnTriggerEnter(Collider other)
    {
        //Get the player handler play the powered up animation
        if (other.gameObject.layer != 3)
            return;
        
        _upgrade.Invoke(_upgradeId);
    }
}

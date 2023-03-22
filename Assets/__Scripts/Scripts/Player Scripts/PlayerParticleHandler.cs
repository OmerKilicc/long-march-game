using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticleHandler : MonoBehaviour
{
    [Header("Instances")]
    [SerializeField] public ParticleSystem coinFX;
    [SerializeField] public ParticleSystem deathFX;
    [SerializeField] ParticleSystem _upgradeFX;
    [SerializeField] ParticleSystem _downgradeFX;

    PlayerUpgradeController playerUpgradeScript;

    private void Start()
    {
        playerUpgradeScript = GetComponent<PlayerUpgradeController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
            coinFX.Play();
        
        if (other.gameObject.CompareTag("Upgrade"))
          _upgradeFX.Play();

        if (other.gameObject.CompareTag("Obstacle"))
            _downgradeFX.Play();

        if (playerUpgradeScript.playerPower == 0)
            deathFX.Play();
        

    }
}

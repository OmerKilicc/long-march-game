using Euphrates;
using UnityEngine;

public class FinishHandler : MonoBehaviour
{
    [SerializeField] ParticleSystem _leftFlare;
    [SerializeField] ParticleSystem _rightFlare;
    AudioManager audioManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            audioManager = FindObjectOfType<AudioManager>();
            audioManager.Play("Finish");
            _leftFlare.Play();
            _rightFlare.Play();
        }
        
    }
}

using Euphrates;
using UnityEngine;

[RequireComponent(typeof(Animator))] 
public class PlayerAnimationsManager : MonoBehaviour
{
    Animator _animator;

    [Space, Header("Triggers")]
    [SerializeField] TriggerChannelSO _start;
    [SerializeField] IntChannelSO _upgrade;
    [SerializeField] TriggerChannelSO _downgrade;
    [SerializeField] TriggerChannelSO _win;
    [SerializeField] TriggerChannelSO _lose;

    private void Awake() => _animator = GetComponent<Animator>();

    private void OnEnable()
    {
        _start.AddListener(GameStart);
        _upgrade.AddListener(Upgraded);
        _downgrade.AddListener(Downgraded);
        _win.AddListener(Win);
        _lose.AddListener(Death);
    }

    private void OnDisable()
    {
        _start.RemoveListener(GameStart);
        _upgrade.RemoveListener(Upgraded);
        _downgrade.RemoveListener(Downgraded);
        _win.RemoveListener(Win);
        _lose.RemoveListener(Death);
    }

    void GameStart() => _animator.SetBool("isWalking", true);

    void Upgraded(int _) => _animator.SetTrigger("isPoweredUp");

    void Downgraded() => _animator.SetTrigger("isDowngraded");

    void Win() => _animator.SetBool("isWon", true);

    void Death() => _animator.SetBool("isDead", true);
}

using UnityEngine;

public class CoinController : MonoBehaviour
{
    [Header("Coins Attributes")]
    [SerializeField] public int _valueOfTheCoin = 1;

    AudioManager audioManager;
    MoneySystem money;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            money = MoneySystem.Instance;
            audioManager = FindObjectOfType<AudioManager>();
            audioManager.Play("Coin");
            money.AddCoin(_valueOfTheCoin);
            Destroy(this.gameObject);
        }
    }
}

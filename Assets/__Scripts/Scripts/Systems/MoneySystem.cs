using Euphrates;
using UnityEngine;

public class MoneySystem : Singleton<MoneySystem>
{

    [SerializeReference] IntSO _coin;

    void Start()
    {
        GetTotalMoney();
    }

    private void GetTotalMoney() => _coin.Value = PlayerPrefs.HasKey("Coin") ? PlayerPrefs.GetInt("Coin") : 0;

    public void AddCoin(int _coinValue)
    {
        //Add given value to total coins
        _coin.Value += _coinValue;

        //Level sonuna finishe tasi
        PlayerPrefs.SetInt("Coin", _coin);
        PlayerPrefs.Save();
    }
}

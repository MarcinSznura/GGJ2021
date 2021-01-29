using UnityEngine;
using TMPro;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerHP = null;

    public void UpdateUI(int _playerHP, int _maxplayerHP)
    {
        playerHP.text = "HP: " + _playerHP.ToString() + "/" + _maxplayerHP;
    }
}

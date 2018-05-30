using System.Collections.Generic;
using UnityEngine;

public class GodGame : MonoBehaviour
{
    private List<GodPlayer> _players;
    [SerializeField] private PlayerUI[] _playerUI;
    [SerializeField] private GameObject[] _playerPrefabs;

    private void Awake()
    {
        _players = new List<GodPlayer>();

        AddPlayer();
        AddPlayer();
    }

    private void Start()
    {

    }

    private void Update()
    {
        UpdatePlayerUI();
    }

    private void UpdatePlayerUI()
    {
        for(int index0 = 0; index0 < _players.Count; index0++)
        {
            // Stamina.
            _playerUI[index0]._staminaBar.value = _players[index0].Stamina;
            _playerUI[index0]._staminaBar.maxValue = _players[index0].MaxStamina;

            // Health.
            _playerUI[index0]._healthBar.value = _players[index0].HealthManager._currentHealthPoints;
            _playerUI[index0]._healthBar.maxValue = _players[index0].HealthManager._maxHealthPoints;

            // Score.
            _playerUI[index0]._scoreTxt.text = _players[index0].Score.ToString();
        }
    }

    private void AddPlayer()
    {
        GodPlayer playerGod = Instantiate(_playerPrefabs[Random.Range(0, _playerPrefabs.Length)], transform.position, transform.rotation).GetComponent<GodPlayer>();

        _players.Add(playerGod);
        playerGod.HorizontalInputName = "J" + _players.Count + "Horizontal";
        playerGod.VerticalInputName = "J" + _players.Count + "Vertical";
        playerGod.LookHorizontalInputName = "J" + _players.Count + "LookHorizontal";
        playerGod.LookVerticalInputName = "J" + _players.Count + "LookVertical";
        playerGod.AttackBtnName = "J" + _players.Count + "Attack";
        playerGod.SpecialBtnName = "J" + _players.Count + "Special";
    }
}
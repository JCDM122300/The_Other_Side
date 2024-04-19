using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleDataPasser : MonoBehaviour
{
    [SerializeField] private Image PlayerSprite;
    [SerializeField] private Image CreatureSprite;

    private Player PlayerData;
    private Enemy CreatureData;

    public void PassPlayerData(Player player, Sprite playerSprite)
    {
        PlayerData = player;
        PlayerSprite.sprite = playerSprite;
    }

    public void PassEnemyData(Enemy enemy, Sprite enemySprite)
    {
        CreatureData = enemy;
        CreatureSprite.sprite = enemySprite;
    }
}

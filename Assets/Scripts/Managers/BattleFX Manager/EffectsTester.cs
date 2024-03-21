using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsTester : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Enemy;



    void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            BattleEffectsManager.Instance().ApplyAttackFX(Player, Enemy, DamageVisual.SHAKE, Color.gray);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            BattleEffectsManager.Instance().ApplyAttackFX(Enemy, Player, DamageVisual.SHAKE, Color.gray);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            BattleEffectsManager.Instance().ApplyAttackFX(Enemy, Player, DamageVisual.CRUSH, Color.gray);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            BattleEffectsManager.Instance().ApplyAttackFX(Player, Enemy, DamageVisual.CRUSH, Color.gray);
        }
    }
}

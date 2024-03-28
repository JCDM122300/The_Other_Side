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

    /// <summary>
    ///     
    ///     Battle Scene Shader Properties;
    ///     
    ///     float _VertexX (moves the shader along X-axis) Range 0-1
    ///     
    /// 
    /// </summary>
    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            BattleEffectsManager.Instance().ApplyAttackFX(Player, Enemy, DamageVisual.SHAKE, Color.black);
            //Player.GetComponent<SpriteRenderer>().material.SetFloat("_VertexX", 1);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            BattleEffectsManager.Instance().ApplyAttackFX(Enemy, Player, DamageVisual.SHAKE, Color.black);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            BattleEffectsManager.Instance().ApplyAttackFX(Enemy, Player, DamageVisual.CRUSH, Color.black);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            BattleEffectsManager.Instance().ApplyAttackFX(Player, Enemy, DamageVisual.CRUSH, Color.black);
        }
    }
}

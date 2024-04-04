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
            BattleEffectsManager.Instance().ScrollEffect(Player, 0.2f, 1, 1.3f, 0.4f, Color.red);
            BattleEffectsManager.Instance().CharcterFlash(Player, 0.5f);
            BattleEffectsManager.Instance().CharacterHitColorEffect(Player, 0.3f, Color.blue);
            BattleEffectsManager.Instance().CharacterSquishEffect(Player, new Vector3(30, 0, 0),  0.2f, 0.5f);
            BattleEffectsManager.Instance().CharacterShake(Player, 0.5f, 1.0f, 3, LockMovement.NONE);

            //BattleEffectsManager.Instance().ApplyAttackFX(Player, Enemy, DamageVisual.SHAKE, Color.black);
            //Player.GetComponent<SpriteRenderer>().material.SetFloat("_VertexX", 1);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            BattleEffectsManager.Instance().ScrollEffect(Enemy, 3f, -1, 1.3f, 0.4f, Color.green);
            BattleEffectsManager.Instance().CharcterFlash(Enemy, 0.5f);
            BattleEffectsManager.Instance().CharacterSquishEffect(Enemy, new Vector3(30, 0, 0), 0.2f, 0.5f);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            //BattleEffectsManager.Instance().ApplyAttackFX(Enemy, Player, DamageVisual.CRUSH, Color.black);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            //BattleEffectsManager.Instance().ApplyAttackFX(Player, Enemy, DamageVisual.CRUSH, Color.black);
        }
    }
}

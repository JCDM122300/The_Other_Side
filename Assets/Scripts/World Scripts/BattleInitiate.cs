using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BattleInitiate : MonoBehaviour
{
    private Sprite enemySprite;
    private int enemyLevel = 0;

    public static event EventHandler OnBattleInitiate;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnBattleInitiate?.Invoke(this, EventArgs.Empty);
            TransitionManager.instance.EnterBattleTransition(true);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BattleInitiate : MonoBehaviour
{
    private Creature EnemyData;
    private Sprite EnemySprite;

    public static event EventHandler OnBattleInitiate;
    public static event EventHandler<(Creature, Sprite)> OnEnemyInitiate;

    private (Creature, Sprite) Data;

    private void Start()
    {
        EnemyData = GetComponent<Creature>();
        EnemySprite = GetComponent<SpriteRenderer>().sprite;

        Data = (EnemyData, EnemySprite);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnBattleInitiate?.Invoke(this, EventArgs.Empty);

            OnEnemyInitiate?.Invoke(this, Data);

            TransitionManager.instance.EnterBattleTransition(true);
        }
    }
}

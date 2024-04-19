using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleInitiate : MonoBehaviour
{
    [Tooltip("Searches for Gameobject of this name. Battle Screen- name")]
    [SerializeField] private string ScreenName;

    private Sprite enemySprite;
    private int enemyLevel = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && ScreenName != null)
        {
            TransitionManager.instance.Transition(true, ScreenName, enemySprite);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleInitiate : MonoBehaviour
{
    [SerializeField] private string SceneName;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && SceneName != null)
        {
            //WorldManager.instance.LoadBattleScene(SceneName);
            TransitionManager.instance.Transition(true, SceneName);
        }

    }
}

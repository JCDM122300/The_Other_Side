using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenActivate : MonoBehaviour
{
    [SerializeField] private GameObject BattleScreen;

    private void Start()
    {
        if (BattleScreen != null)
        {
            BattleScreen.SetActive(false);
        }
    }

    public void ToggleBattleScreen(bool toggle)
    {
        BattleScreen.SetActive(toggle);
    }
}

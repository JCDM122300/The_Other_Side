using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenActivate : MonoBehaviour
{
    [SerializeField] private GameObject BattleScreen;

    private void Start()
    {
        TransitionManager.OnEnableScreen += OnTranstitionFinsiedEnter;
        TransitionManager.OnDisableScreen += OnTransitionFinsihedFlee;

        ToggleChildren(false);
    }

    private void OnTransitionFinsihedFlee(object sender, System.EventArgs e)
    {
        ToggleChildren(false);
    }

    private void OnTranstitionFinsiedEnter(object sender, System.EventArgs e)
    {
        ToggleChildren(true);
    }

    public void ToggleBattleScreen(bool toggle)
    {
        BattleScreen.SetActive(toggle);
    }

    private void ToggleChildren(bool toggle)
    {
        foreach (Transform child in gameObject.transform)
        {
            child.gameObject.SetActive(toggle);
        }
    }

    private void OnDestroy()
    {
        TransitionManager.OnEnableScreen -= OnTranstitionFinsiedEnter;
        TransitionManager.OnDisableScreen -= OnTransitionFinsihedFlee;
    }
}

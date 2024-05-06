using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    [SerializeField] private AudioSource WorldSounds;
    void Start()
    {
        BattleInitiate.OnBattleInitiate += StopWorld;
        BattleManager.OnBattleFlee += ReturnToWorld;
    }

    private void ReturnToWorld(object sender, System.EventArgs e)
    {
        if (WorldSounds != null)
        {
            WorldSounds.Play();
        }
        Time.timeScale = 1;
    }

    private void StopWorld(object sender, System.EventArgs e)
    {
        if (WorldSounds != null)
        {
            WorldSounds.Stop();
        }
        Time.timeScale = 0;
    }

    private void OnDestroy()
    {
        BattleInitiate.OnBattleInitiate -= StopWorld;
        BattleManager.OnBattleFlee -= ReturnToWorld;
    }
}

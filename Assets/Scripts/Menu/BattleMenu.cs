using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMenu : MonoBehaviour
{
    BattleManager BM;

    // Start is called before the first frame update
    void Start()
    {
        BM = new BattleManager();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FightButton(Creature C)
    {
        BM.Fight(C);
    }
}

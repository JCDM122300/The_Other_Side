using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    string[] EnemyNames = new string[] { "Kiara", "Alastor", "Cessair", "Snake", "Drift", "Apex", "Blitz",
        "Pitfall", "Hex" };
    System.Random random;

    // Start is called before the first frame update
    void Start()
    {
        random = new System.Random();
        int WhichName = random.Next(0, EnemyNames.Length);
        this.Name = EnemyNames[WhichName];
        //this.CombindedATK = CalculateCombindedATK();
        this.Coins = 20;
    }

    public void CalculateLevelAndCoins(int Round)
    {
        //this.Level = CalculateLevel();
        Coins *= Round;
    }
}

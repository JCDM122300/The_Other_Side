using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] int StartingLevel = 1;
    [SerializeField] int StartingCoins = 25;

    //public Team Team;

    public List<Creature> AllMonsters = new List<Creature>(); //All Monsters the have

    public List<Creature> DeadMonsters = new List<Creature>();//Can be healed for coins

    // Start is called before the first frame update
    void Start()
    {
        this.Name = name;
        //this.CombindedATK = CalculateCombindedATK();
        this.Level = StartingLevel;
        this.Coins = StartingCoins;
    }

    public void AddMonsterToAllMonsters(Creature c)
    {
        if (CheckIfTeamIsFull() == true)
        {
            GamePrintout.TxtPrintOut += "\nYour Team is full\nThe Monster has been added to your collection";
            this.AllMonsters.Add(c);
        }
        else
        {
            this.Team.Add(c);
            GamePrintout.TxtPrintOut += "\nThis Monster has been added to your Team!";
        }

    }

    public void AddMonsterToDeadMonsters(Creature c)
    {
        this.DeadMonsters.Add(c);
        this.Team.Remove(c);
    }

    public bool CheckIfTeamIsFull()
    {
        if (this.Team.Count == 3)
        {
            return true;
        }
        return false;
    }

    public int GetDeadMonsterCount()
    {
        return this.DeadMonsters.Count;
    }
}

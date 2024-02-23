using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

public class RaffleManager : MonoBehaviour
{
    Player P;

    MonsterManager mm;
    [SerializeField] int Round;

    System.Random random;

    //public Color RaffleElement;

    public Texture2D PulledMonsterSprite, WhatMonster;
    public Rectangle MonsterPulledLoc;

    // Start is called before the first frame update
    void Start()
    {
        P = GetComponent<Player>();

        mm = GetComponent<MonsterManager>(); 

        random = new System.Random();
        //WhatMonster = game.Content.Load<Texture2D>("What");
        //MonsterPulledLoc = new Rectangle(game.GraphicsDevice.Viewport.Width / 4, 100, 250, 250);
    }

    int RaffleCost = 20;
    public bool PullFromRaffel;
    Creature MonsterPulled;
    public void MonsterRaffle()
    {
        if (!PullFromRaffel)
        {
            GamePrintout.TxtPrintOut = "Welcome to the Raffle!\n";
            if (P.Coins >= RaffleCost)
            {
                GamePrintout.TxtPrintOut += "You have enough Coins to pull from the Raffle";
            }
            else { GamePrintout.TxtPrintOut += $"You do not have enough to participate in the Raffle come again when you have {RaffleCost} Coins"; }
        }

    }

    Creature FreePulledMonster;
    public Creature PullFreeMonster(int Round)
    {
        int WhichMonster = random.Next(0, mm.Monsters.Count);
        FreePulledMonster = mm.Monsters[WhichMonster];
        FreePulledMonster.GetStats(Round);

        return FreePulledMonster;
    }

    void PullMonster()
    {
        PullFromRaffel = true;

        P.Coins -= RaffleCost;
        int WhichMonster = random.Next(0, mm.Monsters.Count);//Sets a Random between the bounds of the Monters one can get
        MonsterPulled = mm.Monsters[WhichMonster];//Sets the Monster gotten locally
        MonsterPulled.GetStats(Round);//Gets there Stats

        //PulledMonsterSprite = MonsterPulled.spriteTexture;

        GamePrintout.TxtPrintOut = $"You pulled {MonsterPulled.Name}\nStats:\nHP: {MonsterPulled.HP} | ATK Score: {MonsterPulled.ATKScore} | DEF: {MonsterPulled.DEFScore}";

        P.AddMonsterToAllMonsters(MonsterPulled);
    }

    public void RaffleInput()
    {
        //if (input.KeyboardState.WasKeyPressed(Keys.P))
        //{
        //    PullMonster();
        //}
        //if (input.KeyboardState.WasKeyPressed(Keys.E))
        //{
        //    ExitRaffle = true;
        //}
    }

    public bool ExitRaffle = false;
    public bool GetExitRaffle() { return ExitRaffle; }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UIElements;

public enum BattleState { Playing, Won, Lost, Forfit, Paused }

public class BattleManager : MonoBehaviour
{
    //InputHandler input;

    public BattleState BattleState { get; set; }
    public GameDifficulty GameDifficulty { get; }


    Player P;
    Creature E;
    //Enemy E;

    public int Turn;

    bool MoveMade = false;

    public string ButtonGuideTxt { get; set; }
    public Vector2 ButtonGuideLoc { get; set; }

    //Randoms:
    System.Random RunAttempt = new System.Random();
    System.Random WillRun = new System.Random();

    //public Color BattleElement;

    //DrawValues
    public Rectangle CPMLoc, CEMLoc;
    public Vector2 PMLocation, EMLocation;

    Vector2 TurntxtLoc;
    Vector2 PM_HPLocation, E_TextLocation, EM_HPLocation;

    bool PlayerDid;

    public bool LockedIn;

    //Allow the enemy AI to check CPM's type and try to swith to a monster that beats it?

    int margin = 50;

    [SerializeField] private Transform MenuReference;

    [SerializeField] Slider PlayerHealth;
    [SerializeField] Slider EnemyHealth;

    [SerializeField] int currentPlayerHealth;
    [SerializeField] int maxPlayerHealth;

    [SerializeField] int currentEnemyHealth;
    [SerializeField] int maxEnemyHealth;

    public static event EventHandler OnBattleFlee;

    
    public BattleManager()
    {       
        //P = new Player();
        //E = new Enemy();

        //P = GetComponent<Player>();
        //E = GetComponent<Enemy>();

        //this.BattleState = BattleState.Playing;
        //BattleElement = Color.White;

        LoadBattleEvelemts();
    }

    // Start is called before the first frame update
    void Start()
    {
        //P = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        //E = MenuReference.GetComponent<BattleMenu>().creature;
        //LoadBattleEvelemts();
        //P.CurrentMonster = P.Team[0];
    }

    private void OnEnable()
    {
        P = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        E = MenuReference.GetComponent<BattleMenu>().creature;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FleeBattle()
    {
        OnBattleFlee?.Invoke(this, EventArgs.Empty);
    }
    void LoadBattleEvelemts()
    {
        //PMLocation = new Vector2(margin, game.GraphicsDevice.Viewport.Height - 400);
        //EMLocation = new Vector2((game.GraphicsDevice.Viewport.Width - E.CurrentMonster.spriteTexture.Width) - margin, game.GraphicsDevice.Viewport.Height - 400);

        //CPMLoc = new Rectangle((int)PMLocation.X, (int)PMLocation.Y, 350, 350);
        //CEMLoc = new Rectangle((int)EMLocation.X, (int)EMLocation.Y, 350, 350);

        //TurntxtLoc = new Vector2(game.GraphicsDevice.Viewport.Width / 2 - 25, 70);

        //PM_HPLocation = new Vector2(20, 80);
        //E_TextLocation = new Vector2(game.GraphicsDevice.Viewport.Width - 250, 20);
        //EM_HPLocation = new Vector2(game.GraphicsDevice.Viewport.Width - 250, 80);

        //ButtonGuideLoc = new Vector2(20, 1025);
    }


    public double Fight(Player P, Creature C)
    {
        Debug.Log("Attack is called:");
        return P.Attack(P.ATKScore, C.DEFScore);
    }

    public double Fight(Creature C, Player P)
    {
        Debug.Log("Attack is called:");
        return C.Attack(C.ATKScore, P.DEFScore);
    }

    //public bool Won, BattleOver, Paused;
    //public void CheckBattleState()
    //{
    //    switch (BattleState)
    //    {
    //        case BattleState.Playing:
    //            BattleOver = false;
    //            break;
    //        case BattleState.Won:
    //            Won = true;
    //            BattleOver = true;
    //            break;
    //        case BattleState.Lost:
    //            Won = false;
    //            BattleOver = true;
    //            break;
    //        case BattleState.Forfit:
    //            Won = false;
    //            BattleOver = true;
    //            break;
    //        case BattleState.Paused:
    //            Paused = true;
    //            break;
    //    }
    //}

    //string results;
    //public string BattleResults()
    //{
    //    LockedIn = false;
    //    if (BattleState == BattleState.Won)
    //    {
    //        results = $"You defeated Enemy {E.Name}\nYou can now progress to the next Round!";
    //        results += $"\n{CheckCoinsGained()}";
    //    }

    //    if (BattleState == BattleState.Lost)
    //    {
    //        results = $"Enemy {E.Name} defeated you!\nTo progress to the next Round you must defeat the Enemy!";
    //        results += $"\n{CheckCoinsLost()}";
    //    }

    //    if (BattleState == BattleState.Forfit)
    //    {
    //        if (PlayerDid) { results = "You ran before defeating the Enemy\nTo progress to the next Round you must defeat the Enemy!"; }
    //        if (!PlayerDid) { results = "All of the Enemy's Monsters ran before you could defeat them\nTo progress to the next Round you must defeat the Enemy!"; }
    //    }

    //    return results;
    //}


    //void CallPause()
    //{
    //    if (!BattleOver) { GamePrintout.TxtPrintOut += $"\n\nPress Space to contine..."; }
    //}

    //#region Turn
    //public bool TurnOver = false;
    //public void FullTurn(bool Attack)
    //{
    //    MoveMade = true;
    //    if (!TurnOver)
    //    {
    //        BattleState = BattleState.Playing;

    //        CheckLife();

    //        if (Attack)//If the player desides to attack
    //        {
    //            GamePrintout.TxtPrintOut = "You have decided to Attack!";
    //            PlayerTurn();
    //        }
    //        else
    //        {
    //            GamePrintout.TxtPrintOut = "You have decided to Run!";
    //            Run(P);
    //        }

    //        if (BattleState == BattleState.Playing)
    //        {
    //            EnemyTurn();
    //            TurnOver = true;
    //        }

    //    }

    //    if (TurnOver == true && BattleState == BattleState.Playing)
    //    {
    //        CallPause();
    //        BattleState = BattleState.Paused;
    //    }

    //}


    //double Damage;
    //bool PlayerHasDamaged = false;
    //bool EnemyHasDamaged = false;
    //public void PlayerTurn()
    //{
    //    Damage = P.CurrentMonster.Attack(P.CurrentMonster.ATKScore, E.CurrentMonster.DEFScore);

    //    DamageMonster(E.CurrentMonster, (int)Damage); ;
    //    GamePrintout.TxtPrintOut += $"\n{P.CurrentMonster.Name} Damaged {E.CurrentMonster.Name} for {(int)Damage} HP points\n{E.CurrentMonster.Name}'s HP is now at {E.CurrentMonster.HP}";
    //    if (E.CurrentMonster.HP == 0)
    //    {
    //        BattleState = BattleState.Won;
    //        TurnOver = true;
    //    }

    //}


    //public void EnemyTurn()
    //{
    //    if (!E.CurrentMonster.Dead)
    //    {
    //        if (!CheckProbablilityOfLoss())
    //        {
    //            if (BattleState == BattleState.Playing)
    //            {
    //                Damage = E.CurrentMonster.Attack(E.CurrentMonster.ATKScore, P.CurrentMonster.DEFScore);
    //                DamageMonster(P.CurrentMonster, (int)Damage);
    //                GamePrintout.TxtPrintOut += $"\n{E.CurrentMonster.Name} Damaged {P.CurrentMonster.Name} for {(int)Damage} HP points\n{P.CurrentMonster.Name}'s HP is now at {P.CurrentMonster.HP}";
    //            }
    //        }
    //    }
    //    if (E.CurrentMonster.Dead)
    //    { GamePrintout.TxtPrintOut += $"\n{E.Name}'s Monster has been Defeated!"; }

    //    GamePrintout.TxtPrintOut += "\nTurn Completed";
    //    TurnOver = true;

    //}

    //public void TurnCompleted()
    //{
    //    PlayerHasDamaged = EnemyHasDamaged = false;
    //    Turn++;
    //    GamePrintout.TxtPrintOut = "";

    //    //Check Team Status
    //    if (!CheckMonsterStatus(P))
    //    {
    //        GamePrintout.TxtPrintOut += "\nAll of your Monsters are still standing!";
    //    }
    //    else
    //    {
    //        if (P.Team.Count != 0)
    //        {
    //            GamePrintout.TxtPrintOut += $"\n{P.Team[0].Name} is now up";
    //            P.CurrentMonster = P.Team[0];
    //        }
    //    }

    //    if (CheckMonsterStatus(E))
    //    {
    //        if (E.Team.Count != 0)
    //        {
    //            GamePrintout.TxtPrintOut += $"\n{E.Team[0].Name} is now up";
    //            P.CurrentMonster = P.Team[0];
    //        }
    //    }

    //    CheckLife();
    //    if (BattleState == BattleState.Playing)
    //    {
    //        MoveMade = false;
    //        TurnOver = false;
    //        GamePrintout.TxtPrintOut += $"\n{E.Name} is still standing\nWhat's your next move?";

    //    }
    //}



    //#endregion
    //int RemainingHp;
    //void DamageMonster(Creature WhichMonster, int Damage)
    //{
    //    if (WhichMonster == E.CurrentMonster)//Player Attacking
    //    {
    //        if (!PlayerHasDamaged) { Damaging(WhichMonster, Damage); }
    //        PlayerHasDamaged = true;
    //    }
    //    if (WhichMonster == P.CurrentMonster)//Enemy Attacking
    //    {
    //        if (!EnemyHasDamaged) { Damaging(WhichMonster, Damage); }
    //        EnemyHasDamaged = true;
    //    }
    //}

    //void Damaging(Creature WhichMonster, int Damage)
    //{
    //    RemainingHp = WhichMonster.HP - Damage;
    //    if (RemainingHp <= 0)
    //    {
    //        WhichMonster.HP = 0;
    //        WhichMonster.Dead = true;
    //    }
    //    else { WhichMonster.HP -= Damage; }
    //}


    //public void OptomizeEnemySwap()
    //{
    //    if (GameDifficulty == GameDifficulty.Hard)
    //    {
    //        //allow the enemy to swap there monster for one that is better equipt at fighing the players monster
    //    }
    //}

    //bool LostMonster;
    //public bool CheckMonsterStatus(Character WhichCharacter)
    //{

    //    LostMonster = false;
    //    if (WhichCharacter.CurrentMonster.HP <= 0)
    //    {
    //        LostMonster = true;
    //        GamePrintout.TxtPrintOut += $"\n{WhichCharacter.CurrentMonster.Name}'s Hp hit 0\nThey can no longer Battle";
    //        WhichCharacter.Team.Remove(WhichCharacter.CurrentMonster);
    //        if (WhichCharacter == P) { P.DeadMonsters.Add(P.CurrentMonster); }

    //    }
    //    return LostMonster;
    //}

    //#region Run

    //int Decision;
    //public bool CheckProbablilityOfLoss()
    //{
    //    if (E.Team.Count == 1)
    //    {
    //        if (P.CurrentMonster.ATKScore > E.CurrentMonster.HP)//if the enemy will lose on the next round
    //        {
    //            Decision = WillRun.Next(0, 100);
    //            //Need to have the Enemy not forfit every time
    //            if (Decision > 50)
    //            {
    //                GamePrintout.TxtPrintOut += $"\nThe Enemy is attempting to Run!";
    //                //Attempt run
    //                Run(E);
    //                return true;
    //            }

    //        }
    //    }
    //    return false;

    //}


    //int RunSucessRate;
    //public void Run(Character WhichCharacter)
    //{
    //    RunSucessRate = RunAttempt.Next(100);
    //    if (RunSucessRate <= 60)
    //    {
    //        RunSucess(WhichCharacter);
    //    }
    //    else
    //    {
    //        GamePrintout.TxtPrintOut += $"\nThe {WhichCharacter.CurrentMonster.Name} was unsucesfull in their Run attempt";
    //    }

    //}

    //public void RunSucess(Character WhichCharacter)
    //{
    //    if (WhichCharacter == E)
    //    {
    //        PlayerDid = false;
    //        EnemyMonsterAbandoned();

    //    }
    //    if (WhichCharacter == P)
    //    {
    //        GamePrintout.TxtPrintOut += $"\nYou sucessfully Ran";
    //        PlayerDid = true;
    //        TurnOver = true;
    //        BattleState = BattleState.Forfit;
    //    }
    //}

    ////The Enemy dose not run their monsters choose to abondon them to save themselves
    //void EnemyMonsterAbandoned()
    //{
    //    GamePrintout.TxtPrintOut += $"\n{E.CurrentMonster.Name} abondomed {E.Name}";
    //    E.Team.Remove(E.CurrentMonster);
    //    if (E.Team.Count != 0)
    //    {
    //        E.CurrentMonster = E.Team[0];
    //    }
    //    else { BattleState = BattleState.Forfit; }
    //}

    //#endregion

    //int PlayerCombinedHP, EnemyCombinedHP;
    //public void CheckLife()
    //{
    //    PlayerCombinedHP = P.CalculateTeamHP();
    //    EnemyCombinedHP = E.CalculateTeamHP();

    //    if (PlayerCombinedHP <= 0)
    //    {
    //        if (this.BattleState != BattleState.Forfit)
    //        {
    //            GamePrintout.TxtPrintOut += "\nAll of your Monsters are a 0 HP";
    //            this.BattleState = BattleState.Lost;
    //        }

    //    }
    //    if (EnemyCombinedHP <= 0)
    //    {
    //        if (this.BattleState != BattleState.Forfit)
    //        {
    //            GamePrintout.TxtPrintOut += $"\nAll of {E.Name}'s Monsters are a 0 HP";
    //            this.BattleState = BattleState.Won;
    //        }

    //    }
    //}

    #region Coins

    string coinstatus;
    public string CheckCoinsLost()
    {
        //depends on difficulty
        if (GameDifficulty == GameDifficulty.Easy)
        {
            P.Coins -= (P.Coins / 3); //Looses a 1/3
            coinstatus = "\nYou lost a 1/3 of your Coins";
        }
        if (GameDifficulty == GameDifficulty.Medium)
        {
            P.Coins -= ((P.Coins / 3) * 2); //Looses 2/3
            coinstatus = "\nYou lost a 2/3 of your Coins";
        }
        if (GameDifficulty == GameDifficulty.Hard) //Looses all
        {
            P.Coins -= P.Coins;
            coinstatus = "\nYou lost all of your Coins";
        }
        return coinstatus;
    }

    //public string CheckCoinsGained()
    //{
    //    //depends on difficulty
    //    if (GameDifficulty == GameDifficulty.Easy)
    //    {
    //        P.Coins += (E.Coins / 3); // Gains a 1/3 of the Enemies coins
    //        coinstatus = $"\nYou gaid {E.Coins / 3} Coins from the Enemy";
    //    }
    //    if (GameDifficulty == GameDifficulty.Medium)
    //    {
    //        P.Coins += ((E.Coins / 3) * 2); //Gains 2/3 of the Enemies coins
    //        coinstatus = $"\nYou gaid {E.Coins / 2} Coins from the Enemy";
    //    }
    //    if (GameDifficulty == GameDifficulty.Hard)
    //    {
    //        P.Coins += E.Coins; // Gains all of Enemies Coins
    //        coinstatus = $"\nYou gaid {E.Coins} Coins from the Enemy";
    //    }
    //    return coinstatus;
    //}
    #endregion

    #region Input
    public void InBattleInput()
    {
        //if (!MoveMade)
        //{
        //    if (input.KeyboardState.WasKeyPressed(Keys.A))
        //    {
        //        this.FullTurn(true);//Attack
        //    }
        //    if (input.KeyboardState.WasKeyPressed(Keys.R))
        //    {
        //        this.FullTurn(false);//Run
        //    }

        //}

        //if (Paused)
        //{
        //    if (input.KeyboardState.WasKeyPressed(Keys.Space))
        //    {
        //        Paused = false;
        //        if (BattleState != BattleState.Forfit) { BattleState = BattleState.Playing; }

        //        TurnCompleted();
        //    }
        //}
    }

    public void MonsterSwapInput()
    {
        //Switches which monster the user is using at the moment
        //Probally create a bool value for is the player is attacking/ being attacked so they cant switch mid that
        //if (input.KeyboardState.WasKeyPressed(Keys.D1))
        //{
        //    P.CurrentMonster = P.Team[0];
        //    GamePrintout.TxtPrintOut = "You have Swapped to your 1st Monster!\nLock in to begin!";
        //}
        //if (input.KeyboardState.WasKeyPressed(Keys.D2))
        //{
        //    if (P.Team.Count == 1)//No 2nd Monster
        //    {
        //        GamePrintout.TxtPrintOut = "You only have 1 Monster in your team and cannot swap\nLock in to begin!";
        //    }
        //    else
        //    {
        //        P.CurrentMonster = P.Team[1];
        //        GamePrintout.TxtPrintOut = "You have Swapped to your 2st Monster!\nLock in to begin!";
        //    }
        //}
        //if (input.KeyboardState.WasKeyPressed(Keys.D3))
        //{
        //    if (P.Team.Count < 3)//No 3rd
        //    {
        //        if (P.Team.Count == 1)
        //            GamePrintout.TxtPrintOut = "You only have 1 Monster in your Team and cannot Swap\nLock in to begin!";
        //        else
        //            GamePrintout.TxtPrintOut = "You only have 2 Monster in your Team and cannot Swap to your 3rd\nTry Swaping to the 1st or 2nd Monster";
        //    }
        //    else
        //    {
        //        P.CurrentMonster = P.Team[2];
        //        GamePrintout.TxtPrintOut = "You have Swapped to your 3st Monster!\nLock in to begin!";
        //    }
        //}
        //if (input.KeyboardState.WasKeyPressed(Keys.L))
        //{
        //    LockedIn = true;
        //    GamePrintout.TxtPrintOut = $"You have selected {P.CurrentMonster.Name}\nThe Battle will commense!";
        //    //ButtonGuideTxt = "A: Attack | R: Run";
        //    BattleElement = Color.White;
        //}
    }
    #endregion

}

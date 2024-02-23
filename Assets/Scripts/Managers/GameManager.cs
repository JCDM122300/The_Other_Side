using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.Windows;

public enum GameMode { PickStarter, InBattle, OutBattle, MonsterSwap, Healing, Raffel, ManageTeam }
public enum GameState { Won, Playing, Lost }
public enum GameDifficulty { Easy, Medium, Hard }

public class GameManager : MonoBehaviour, IInteractable
{

    public GameMode GameMode { get; set; }
    public GameState GameState { get; set; }

    public GameDifficulty GameDifficulty { get; set; }


    public Player P;
    public Enemy E;

    //Text Draw Locations
    Vector2 RoundtxtLoc, CointxtLoc, P_TextLocation;


    //SpriteBatch sb;

    Color OutOfBattleElement, BattleElement, AlwaysShow;

    public int Round;

    MonsterManager mm;
    HealManager hm;
    RaffleManager rm;
    TeamManager tm;

    BattleManager CurrentBattle;

    public string ButtonGuideTxt { get; set; }
    public Vector2 ButtonGuideLoc { get; set; }



    // Start is called before the first frame update
    void Start()
    {

        GameState = GameState.Playing;

        P = new Player();
        E = new Enemy();


        mm = GetComponent<MonsterManager>();
        hm = GetComponent<HealManager>();

        LoadGameElements();

    }

    void LoadGameElements()
    {
        GamePrintout.TxtPrintOut = "Welcome to Battle Monsters!";

        //AlwaysShow = Color.White;

        Round = 1;

        GameMode = GameMode.PickStarter;

        GameLost = false;

        #region Text Draw Set
        //RoundtxtLoc = new Vector2((g.GraphicsDevice.Viewport.Width / 2) - 50, 20);
        //CointxtLoc = new Vector2((g.GraphicsDevice.Viewport.Width - 150), 1025);
        //P_TextLocation = new Vector2(20, 20);
        #endregion

#if TESTING
            SetUpTestingValues();
#endif
        ButtonGuideTxt = "B: Battle | H: Heal | R: Raffle | T: Manage Team";
        ButtonGuideLoc = new Vector2(20, 1025);
    }

    void SetUpTestingValues()
    {
        P.Coins = 100;
        Round = 1;

        //Healing
        Creature HealableMonster;
        HealableMonster = mm.Monsters[7];
        HealableMonster.GetStats(Round);
        P.DeadMonsters.Add(HealableMonster);

        HealableMonster = mm.Monsters[12];
        HealableMonster.GetStats(Round);
        P.DeadMonsters.Add(HealableMonster);

        //Team Manage
        P.AllMonsters.Add(mm.Monsters[9]);

        //Full Team
        Creature AddToTeam;
        //AddToTeam = mm.WaterJelly;
        //AddToTeam.GetStats(Round);
        //P.Team.Add(AddToTeam);

        //AddToTeam = mm.EarthBird;
        //AddToTeam.GetStats(Round);
        //P.Team.Add(AddToTeam);
    }

    // Update is called once per frame
    void Update()
    {
        CheckGameMode();
        CheckGameState();
        if (CurrentBattle != null) { CurrentBattle.CheckBattleState(); }
        UpdateGameInfoTxt();

        //HandleInput(gameTime);

        //base.Update(gameTime);
    }

    void Replay()
    {
        LoadGameElements();
        //mm.PickStarterElement = Color.White;
    }

    bool BattleStarted;
    #region State Changes
    public void CheckGameMode()
    {
        switch (this.GameMode)
        {
            case GameMode.PickStarter:
                //mm.PickStarterElement = Color.White;
                if (!mm.PickedStarter) { GamePrintout.TxtPrintOut = "Which Starter would you like to pick?"; }

                if (mm.PickedStarter)
                {
                    //mm.PickStarterElement = Color.Transparent;
                    GameMode = GameMode.OutBattle;

                    P.CurrentMonster.GetStats(Round);
                    E.CurrentMonster.GetStats(Round);
                }
                break;
            case GameMode.InBattle:

                if (!BattleStarted)
                {
                    WhichBattle();
                }

                //BattleElement = Color.White;
                //OutOfBattleElement = Color.Transparent;

                if (CurrentBattle.BattleOver == true && CurrentBattle.TurnOver == true)
                {
                    GameMode = GameMode.OutBattle;
                    GamePrintout.TxtPrintOut = CurrentBattle.BattleResults();
                    //CurrentBattle.BattleElement = Color.Transparent;

                    if (CurrentBattle.Won)
                    {
                        if (Round + 1 != 7) { Round++; }
                        else { GameState = GameState.Won; }

                        CurrentBattle.Won = false;
                    }
                }

                break;
            case GameMode.OutBattle:
                //OutOfBattleElement = Color.White;
                //BattleElement = Color.Transparent;
                ButtonGuideTxt = "B: Battle | H: Heal | R: Raffle | T: Manage Team";
                break;
            case GameMode.MonsterSwap:
                //BattleElement = Color.White;
                //OutOfBattleElement = Color.Transparent;
                ButtonGuideTxt = "L: Lock in Monster 1: Swap to First | 2: Swap to Second | 3: Swap to Third";
                if (CurrentBattle.LockedIn) { GameMode = GameMode.InBattle; }
                break;
            case GameMode.Healing:
                //hm.HealElement = Color.White;
                hm.HealMonster();

                //if (hm.GetExitHealing()) { ExitMode(hm.HealElement); }
                break;
            case GameMode.Raffel:
                //rm.RaffleElement = Color.White;
                rm.MonsterRaffle();
                ButtonGuideTxt = "P: Pull from Raffle | E: Exit";

                if (rm.GetExitRaffle())
                {
                    //ExitMode(rm.RaffleElement);
                }
                break;
            case GameMode.ManageTeam:
                //tm.TeamManageElement = Color.White;
                tm.ManageTeam();

                //if (tm.GetExitTeamManager()) { ExitMode(tm.TeamManageElement); }
                break;
        }
    }

    public void CheckGameState()
    {
        switch (this.GameState)
        {
            case GameState.Won:
                GamePrintout.TxtPrintOut = "You have defeated all 6 Rounds of Enemies and have won the game!\nHit R to Replay";
                break;
            case GameState.Playing:
                if (CheckIfLost()) { GameState = GameState.Lost; }
                break;
            case GameState.Lost:
                GamePrintout.TxtPrintOut = $"All of your Monsters have fallen...\nYou do not have enough to heal them\nYou have Lost\nHit R to Replay";
                break;
        }
    }

    bool GameLost;
    bool CheckIfLost()
    {
        GameLost = false;
        if (P.Team.Count == 0 && P.Coins < hm.HealCost) { GameLost = true; }
        return GameLost;
    }
    #endregion

    public void WhichBattle()
    {
        BattleStarted = true;
        if (Round == 1) { NewBattle(); }
        if (Round == 2) { NewBattle(); }
        if (Round == 3) { NewBattle(); }
        if (Round == 4) { NewBattle(); }
        if (Round == 5) { NewBattle(); }
        if (Round == 6) { NewBattle(); }

        CurrentBattle.Turn = 1;
    }

    void NewBattle()
    {
        if (Round != 1) { RandomEnemyMonster(); }
        E.CalculateLevelAndCoins(Round);
        //P.CurrentMonster = P.Team[0];
        //CurrentBattle = new BattleManager(g, P, E);
    }

    void RandomEnemyMonster()
    {
        //if (rm == null) { rm = new RaffleManager(g, P, mm, Round); }
        E.CurrentMonster = rm.PullFreeMonster(Round);
        E.Team.Add(E.CurrentMonster);
    }


    int EasyDifficultyThreshold = 2;
    int MediumDifficultyThreshold = 4;
    int HardDifficultyThreshold = 6;
    public void UpdateDifficulty()
    {
        if (Round <= EasyDifficultyThreshold)
        {
            GameDifficulty = GameDifficulty.Easy;
        }
        else if (Round <= MediumDifficultyThreshold)
        {
            GameDifficulty = GameDifficulty.Medium;
        }
        else if (Round <= HardDifficultyThreshold)
        {
            GameDifficulty = GameDifficulty.Hard;
        }
    }

    void ExitMode(Color color)
    {
        GameMode = GameMode.OutBattle;
        //color = Color.Transparent;
        GamePrintout.TxtPrintOut = "";
    }

    #region Input

    public void HandleInput()
    {
        //Controls differ whether the player is in battle
        //Don't want controls to work in the other game mode
        //if (mm.PickedStarter == false) { mm.PickStarter(); }

        //if (GameMode == GameMode.OutBattle)
        //{
        //    if (input.KeyboardState.WasKeyPressed(Keys.B))
        //    {
        //        GameMode = GameMode.MonsterSwap;
        //        //Battle
        //        BattleStarted = false;
        //        GamePrintout.TxtPrintOut = $"Round {Round} shall commense!";
        //        if (P.Team.Count == 0)
        //        {
        //            P.Team.Add(P.CurrentMonster);
        //            E.Team.Add(E.CurrentMonster);
        //        }
        //    }
        //    if (input.KeyboardState.WasKeyPressed(Keys.H))
        //    {
        //        GameMode = GameMode.Healing;
        //        hm.MonsterHealed = false;
        //        hm.ExitHealing = false;

        //    }
        //    if (input.KeyboardState.WasKeyPressed(Keys.R))
        //    {
        //        rm = new RaffleManager(g, P, mm, Round);
        //        rm.PullFromRaffel = false;
        //        rm.ExitRaffle = false;
        //        GameMode = GameMode.Raffel;
        //    }
        //    if (input.KeyboardState.WasKeyPressed(Keys.T))
        //    {
        //        //Manage team
        //        tm = new TeamManager(g, P, mm);
        //        GamePrintout.TxtPrintOut = "Lets manage your Team!";
        //        GameMode = GameMode.ManageTeam;
        //    }

        //}
        //if (GameMode == GameMode.InBattle)
        //{
        //    CurrentBattle.InBattleInput();
        //}
        //if (GameMode == GameMode.MonsterSwap)
        //{
        //    //Dont love this 
        //    if (CurrentBattle == null) { WhichBattle(); }
        //    CurrentBattle.MonsterSwapInput();
        //}
        //if (GameMode == GameMode.Healing)
        //{
        //    hm.HealInput();
        //}
        //if (GameMode == GameMode.Raffel)
        //{
        //    rm.RaffleInput();
        //}
        //if (GameMode == GameMode.ManageTeam) { tm.TeamManageInput(); }

        //if (GameState != GameState.Playing)
        //{
        //    if (input.KeyboardState.WasKeyPressed(Keys.R))
        //    {
        //        GamePrintout.TxtPrintOut = "Welcome Back to Battle Monsters";
        //        Replay();
        //    }
        //}
    }

    string GameUpdateTxt;
    void UpdateGameInfoTxt()
    {
        GameUpdateTxt = GamePrintout.TxtPrintOut;
    }

    int CalculateStringWidth(string s)//From what I read this is a very taxing function but I was unable to find a better alternative
    {
        if (s != null)
        {
            //Vector2 size = GamePrintout.font.MeasureString(s);
            //return (int)size.X;
        }

        return 0;

    }

    Vector2 GameInfoLoc;


    #endregion
}


public static class GamePrintout
{
    public static string TxtPrintOut;

    //public static SpriteFont font;
    //public static SpriteFont bigfont;

    public static void PrintToGameOutput(string output)
    {
        TxtPrintOut = output;
    }
}

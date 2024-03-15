using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Windows;

public class TeamManager : MonoBehaviour, IInteractable
{
    Player P;

    public bool ExitTeamManager;

    public string ButtonGuideTxt { get; set; }
    public Vector2 ButtonGuideLoc { get; set; }

    Rectangle TeamEditLoc;
    Texture2D TeamEditSprite;

    //string WhichTeamMember;
    int WhichTeamMember, WhichAllMonster;

    Creature ThisMonster;

    //public Color TeamManageElement;

    MonsterManager mm;

    // Start is called before the first frame update
    void Start()
    {
        P = GetComponent<Player>();
        mm = GetComponent<MonsterManager>();

        LoadTeamElements();
    }

    // Update is called once per frame
    void Update()
    {

    }


    void LoadTeamElements()
    {
        ButtonGuideTxt = "L: Lock in Monster | ->: Next Monster | E: Exit";
        //ButtonGuideLoc = new Vector2(20, g.GraphicsDevice.Viewport.Height - 50);

        //TeamEditLoc = new Rectangle(g.GraphicsDevice.Viewport.Width / 4, 100, 250, 250);

        WhichAllMonster = WhichTeamMember = 0;
        WhichTeamMember = 0;
        //if (P.Team.Count != 0) { TeamEditSprite = P.Team[0].spriteTexture; }
        //else { TeamEditSprite = P.AllMonsters[0].spriteTexture; }
    }

    bool Manageable;
    bool StartInfoDisplayed = false;
    public void ManageTeam()
    {
        if (!StartInfoDisplayed)
        {
            GamePrintout.TxtPrintOut = "Welcome to the Team Manager!";

            //if (P.AllMonsters.Count != 0)
            //{
            //    GamePrintout.TxtPrintOut += "\nWhen you are happy with a monster hit L to lock in";
            //    Manageable = true;
            //}
            //else
            //{
            //    GamePrintout.TxtPrintOut += "\nYou don't have any extra Monsters and cannot manage your Team\nCome back when you have more Monsters";
            //    Manageable = false;
            //}
        }

    }

    int TeamBounds, AllMonsterBounds;
    Creature ReplacedMonster;

    bool Swapping = false;
    public void TeamManageInput()
    {
        //if (input.KeyboardState.WasKeyPressed(Keys.L))
        //{
        //    if (Manageable)
        //    {
        //        StartInfoDisplayed = true;
        //        SaveReplacedMonster();

        //        LockMonsterIn();

        //        GamePrintout.TxtPrintOut = $"{ReplacedMonster.Name} has been swapped for {P.Team[WhichTeamMember].Name}\n{ReplacedMonster.Name} has been added to your other Monsters\n\nNow Swap for the next monster!";

        //        NextTeamMember();
        //    }

        //}
        //if (input.KeyboardState.WasKeyPressed(Keys.Right))
        //{
        //    StartInfoDisplayed = true;
        //    Swapping = true;
        //    if (Manageable)
        //    {
        //        NextMonster();
        //    }
        //}
        //if (input.KeyboardState.WasKeyPressed(Keys.E))
        //{
        //    StartInfoDisplayed = true;
        //    ExitTeamManager = true;
        //}
    }

    public bool GetExitTeamManager() { return ExitTeamManager; }

    #region Lock In
    void SaveReplacedMonster()
    {
        if (WhichTeamMember == 0) { ReplacedMonster = P.Team[0]; }
        if (WhichTeamMember == 1) { ReplacedMonster = P.Team[1]; }
        if (WhichTeamMember == 2) { ReplacedMonster = P.Team[2]; }
    }

    void LockMonsterIn()
    {
        if (Swapping)
        {
            //P.Team[WhichTeamMember] = P.AllMonsters[WhichAllMonster];
            //P.AllMonsters.Remove(P.AllMonsters[WhichAllMonster]);
            //P.AllMonsters.Add(ReplacedMonster);
            P.CurrentMonster = P.Team[0];
        }
        else
        {
            GamePrintout.TxtPrintOut += "You did not change this Monster";
        }

        Swapping = false;

    }

    void NextTeamMember()
    {
        WhichTeamMember++;
        TeamBounds = P.Team.Count - 1;
        if (WhichTeamMember > TeamBounds)
        {
            WhichTeamMember = 0;
            GamePrintout.TxtPrintOut += "\nAll of your Team has been Locked in\nE: Exit";
        }

        //TeamEditSprite = P.Team[WhichTeamMember].spriteTexture;
    }
    #endregion

    void NextMonster()
    {
        WhichAllMonster++;
        //AllMonsterBounds = P.AllMonsters.Count - 1;
        //if (WhichAllMonster > AllMonsterBounds) { WhichAllMonster = 0; }
        //GamePrintout.TxtPrintOut = $"Swap Monster with {P.AllMonsters[WhichAllMonster].Name}?\n";
        //TeamEditSprite = P.AllMonsters[WhichAllMonster].spriteTexture;
        //GamePrintout.TxtPrintOut += CompareStats();
    }

    string stats;
    //string CompareStats()
    //{
    //    stats = $"\nCurrent Stats\nHP: {P.Team[WhichTeamMember].HP}/{P.Team[WhichTeamMember].HPMax} | ATK: {P.Team[WhichTeamMember].ATKScore} | DEF: {P.Team[WhichTeamMember].DEFScore}\n";
    //    stats += $"{P.AllMonsters[WhichAllMonster].Name}'s Stats\nHP: {P.AllMonsters[WhichAllMonster].HP}/{P.AllMonsters[WhichAllMonster].HPMax} | ATK: {P.AllMonsters[WhichAllMonster].ATKScore} | DEF: {P.AllMonsters[WhichAllMonster].DEFScore}\n";
    //    return stats;
    //}

}



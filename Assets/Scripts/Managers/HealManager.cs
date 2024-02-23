using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Windows;

public class HealManager : MonoBehaviour
{
    Player P;

    public bool ExitHealing;
    int NeedsHeal;
    public bool MonsterHealed;

    Rectangle MonsterHealedLoc;
    Texture2D HealedMonsterSprite, NoMonsterSprite;

    //public Color HealElement;

    public string ButtonGuideTxt { get; set; }
    public Vector2 ButtonGuideLoc { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        P = GetComponent<Player>();
        ButtonGuideTxt = "T: Heal This Monster | ->: Next Monster | E: Exit";
        //MonsterHealedLoc = new Rectangle(g.GraphicsDevice.Viewport.Width / 4, 100, 250, 250);
        //NoMonsterSprite = g.Content.Load<Texture2D>("Empty");
        //ButtonGuideLoc = new Vector2(20, g.GraphicsDevice.Viewport.Height - 50);
        //WhichMonster = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Healing

    public int HealCost = 10;

    Creature HealedMonster;

    public void HealMonster()
    {
        if (!MonsterHealed)
        {
            GamePrintout.TxtPrintOut = "Welcome to the Healing Station!\n";

            NeedsHeal = P.GetDeadMonsterCount();

            if (NeedsHeal == 0)
            {
                GamePrintout.TxtPrintOut += "You have 0 Monsters that need healing";
            }
            else
            {
                if (P.Coins >= HealCost)//Can heal at least one Monster
                {
                    PickWhichToHeal();
                }
                else
                {
                    GamePrintout.TxtPrintOut += $"You only have {P.Coins} Coins\nYou need {HealCost} Coins to heal a Monster";
                }
            }
        }

    }

    public void Heal(Creature c)
    {
        P.Coins -= HealCost;

        P.DeadMonsters.Remove(c);//Remove the first monster from DeadMonsters
        HealedMonster = c;//Set it locally
        P.AllMonsters.Add(HealedMonster);//Add to All Monsters
        GamePrintout.TxtPrintOut = $"{HealedMonster.Name} has been Healed!\nThey are now back in your colection\nE: Exit";
        MonsterHealed = true;
    }

    int WhichMonster = 0;
    void PickWhichToHeal()
    {
        if (NeedsHeal == 1)
        {
            WhichMonster = 0;
            //HealedMonsterSprite = P.DeadMonsters[0].spriteTexture;
            GamePrintout.TxtPrintOut += $"Only {P.DeadMonsters[WhichMonster].Name} needs healing\n{P.DeadMonsters[WhichMonster].DisplayStats()}";
        }
        else
        {
            if (WhichMonster > NeedsHeal - 1) { WhichMonster = 0; }//Cycle Through
            //HealedMonsterSprite = P.DeadMonsters[WhichMonster].spriteTexture;
            GamePrintout.TxtPrintOut += $"Would you like to heal {P.DeadMonsters[WhichMonster].Name}\n{P.DeadMonsters[WhichMonster].DisplayStats()}";
        }

    }
    #endregion


    public void HealInput()
    {
        //if (input.KeyboardState.WasKeyPressed(Keys.T))
        //{
        //    Heal(P.DeadMonsters[WhichMonster]);

        //}
        //if (input.KeyboardState.WasKeyPressed(Keys.Right))
        //{
        //    WhichMonster++;
        //    //Goes to next monster
        //}
        //if (input.KeyboardState.WasKeyPressed(Keys.E))
        //{
        //    ExitHealing = true;
        //}
    }

    public bool GetExitHealing() { return ExitHealing; }

    //public void Draw(SpriteBatch sb)
    //{
    //    sb.DrawString(GamePrintout.font, ButtonGuideTxt, ButtonGuideLoc, HealElement);
    //    if (NeedsHeal == 0) { sb.Draw(NoMonsterSprite, MonsterHealedLoc, HealElement); }
    //    else { sb.Draw(HealedMonsterSprite, MonsterHealedLoc, HealElement); }
    //}

}

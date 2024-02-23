using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Windows;

public class MonsterManager : MonoBehaviour, IInteractable
{
    //public Creature CreatureName;

    public List<Creature> Monsters;

    public bool PickedStarter;

    Player P;
    Enemy E;


    public Rectangle TeamOneLoc, TeamTwoLoc, TeamThreeLoc;
    public Texture2D EmptySpot;

    public Rectangle Starter1Loc, Starter2Loc, Starter3Loc;

    public string ButtonGuideTxt { get; set; }
    public Vector2 ButtonGuideLoc { get; set; }

    //public Color PickStarterElement;

    // Start is called before the first frame update
    void Start()
    {

        P = GetComponent<Player>();
        E = GetComponent<Enemy>();

        Monsters = new List<Creature>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

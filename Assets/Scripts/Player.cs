using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Player : Character
{
    //player movement 
    
    /*
    private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float MaxJumpHeight; 
    private BoxCollider2D boxCollider;
    private bool IsGrounded;
    public Transform groundCheck;
    public float checkRadius;
    private Animator anim; 
    [SerializeField] private LayerMask groundLayer;
    */

    [SerializeField] int StartingLevel = 1;
    [SerializeField] int StartingCoins = 25;


    public int HP;
    public int HPMax;
    public int ATKScore;
    public int DEFScore;

    System.Random stats;

    public bool Dead;

    [SerializeField] int MinHP = 10;
    [SerializeField] public int MaxHP = 20;
    [SerializeField] int MinATKScore = 5;
    [SerializeField] int MaxATKScore = 15;
    [SerializeField] int MinDEFScore = 5;
    [SerializeField] int MaxDEFScore = 15;
    private void Awake()
    {
        
    }
    
    void Start()
    {
        this.Level = StartingLevel;
        this.Coins = StartingCoins;
    }

    public void GetStats(int Round)
    {
        stats = new System.Random();

        int hp = stats.Next((MinHP * Round), (MaxHP * Round));
        this.HP = hp;
        this.HPMax = hp;

        int atk = stats.Next((MinATKScore * Round), (MaxATKScore * Round));
        this.ATKScore = atk;

        int def = stats.Next((MinDEFScore * Round), (MaxDEFScore * Round));
        this.DEFScore = def;
    }

    double Damage;
    public double Attack(int AttackerATK, int DefenderDEF)
    {
        Damage = AttackerATK * Defence(DefenderDEF, AttackerATK);

        return Damage;
    }

    double DamagePercentage;
    public double Defence(int DefenderDEF, int AtackerATK)
    {
        if (DefenderDEF > AtackerATK)
        {
            //Midigates a percentage of damage
            DamagePercentage = .9;
        }
        else if (DefenderDEF == AtackerATK)
        {
            //Normal Damage
            DamagePercentage = 1;
        }
        else if (DefenderDEF < AtackerATK)
        {
            //Attacker dose more damage
            DamagePercentage = 1.1;
        }

        return DamagePercentage;
    }
}

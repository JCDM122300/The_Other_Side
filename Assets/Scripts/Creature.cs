using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour, IAttack, IDefend
{
    [SerializeField] public string Name;
    [SerializeField] public int HP;
    [SerializeField] public int HPMax;

    [SerializeField] public int atkScore;
    public int ATKScore { get { return atkScore; }
    set { atkScore = value; } }
    [SerializeField] public int defScore;
    [SerializeField] public int DEFScore { get {return defScore; } 
        set { defScore = value; } }


    System.Random stats;

    public bool Dead;

    [SerializeField] int MinHP = 10;
    [SerializeField] int MaxHP = 20;
    [SerializeField] int MinATKScore = 5;
    [SerializeField] int MaxATKScore = 15;
    [SerializeField] int MinDEFScore = 5;
    [SerializeField] int MaxDEFScore = 15;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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


    public string DisplayStats()
    {
        return $"Stats:\nHp: {this.HP}/{this.HPMax}\nATK Score: {this.ATKScore}\nDEF Score: {this.DEFScore}";
    }
}

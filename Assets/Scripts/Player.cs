using System.Collections;
using System.Collections.Generic;
//using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Player : Character
{
    //player movement 
    private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    private BoxCollider2D boxCollider;
    private Animator anim; 
    [SerializeField] private LayerMask groundLayer;
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
        //grab references from gameobject 
        rb = GetComponent<Rigidbody2D>(); 
        boxCollider = GetComponent<BoxCollider2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        //prevents player from falling over and rotating when they jump 
        anim = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        
        float horizontalInput = Input.GetAxis("Horizontal");
        //player walking left/right
        rb.velocity = new Vector2(horizontalInput * speed,rb.velocity.y);
        
        //flips player left/right
      if (horizontalInput> 0.01f)
        { 
            //checks if player moves right
        transform.localScale = Vector3.one; 
        
        }
        else if (horizontalInput < -0.01f)
        {
            //checks if player moves left
            transform.localScale = new Vector3(-1,1,1);

        }

        //Player jump
        if (Input.GetKey(KeyCode.Space) && IsGrounded())
        {
            Jump(); 
        }

        //set run anim parameters 
        anim.SetBool("Run", horizontalInput !=0);

        //adjust jump height in case 
        //if (Input.GetKey(KeyCode.Space) && rb.velocity.y > 0)
        //{
         //rb.velocity = new Vector2(rb.velocity.x,rb.velocity.y / 2);
        //}
    }
    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center,boxCollider.bounds.size,0,Vector2.down,0.1f,groundLayer);
        return raycastHit.collider !=null; 
    }
    private void Jump()
    {
        //controls jump speed
        rb.velocity = new Vector2(rb.velocity.x, jumpPower);
     
        
       
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

    //public void AddMonsterToAllMonsters(Creature c)
    //{
    //    if (CheckIfTeamIsFull() == true)
    //    {
    //        GamePrintout.TxtPrintOut += "\nYour Team is full\nThe Monster has been added to your collection";
    //        this.AllMonsters.Add(c);
    //    }
    //    else
    //    {
    //        this.Team.Add(c);
    //        GamePrintout.TxtPrintOut += "\nThis Monster has been added to your Team!";
    //    }

    //}

    //public void AddMonsterToDeadMonsters(Creature c)
    //{
    //    this.DeadMonsters.Add(c);
    //    this.Team.Remove(c);
    //}

    //public bool CheckIfTeamIsFull()
    //{
    //    if (this.Team.Count == 3)
    //    {
    //        return true;
    //    }
    //    return false;
    //}

    //public int GetDeadMonsterCount()
    //{
    //    return this.DeadMonsters.Count;
    //}
}

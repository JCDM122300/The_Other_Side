using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttack
{
    int ATKScore { get; set; }

    double Attack(int AttackerATK, int DefenderDEF);
}

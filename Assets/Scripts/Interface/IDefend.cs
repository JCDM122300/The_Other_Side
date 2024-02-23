using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDefend
{
    int DEFScore { get; set; }

    double Defence(int DefenderDEF, int AtackerATK);
}

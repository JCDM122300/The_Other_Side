using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentContainer : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}

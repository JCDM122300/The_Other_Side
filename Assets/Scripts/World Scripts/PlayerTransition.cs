using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject m = GameObject.Find("Player");

        m.transform.position = transform.position;
    }
}

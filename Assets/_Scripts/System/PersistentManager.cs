using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentManager : MonoBehaviour
{
    public float ExpWaiting;

    void GetExp()
    {
        GameObject o = GameObject.Find("PlayerCharacter");
        o.GetComponent<_PlayerController>().mExp += ExpWaiting;
        ExpWaiting = 0;
    }

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        ExpWaiting = 0;
        Application.targetFrameRate = 300;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

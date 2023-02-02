using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : MonoBehaviour
{
    [SerializeField]
    private int minerNumber;

    [SerializeField]
    private ReadData readData;

    [SerializeField]
    private float timer;

    [SerializeField]
    private bool timerIsActive;

    private float timerStartValue;
    void Start()
    {
        timerStartValue=timer;
    }

    // Update is called once per frame
    void Update()
    {
        if(timerIsActive)
        {
            timer-=Time.deltaTime;
            if(timer<0)
            {
                readData.MoveBySignal(gameObject,minerNumber);
                timer=timerStartValue;
            }

        }
        
    }

    
}

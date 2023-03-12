using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class MinerList
{
    public List<RecieverList> minerRecieverList= new List<RecieverList>();
}

[System.Serializable]
public class RecieverList
{
    public List<Receiver> recieverList=new List<Receiver>();
}

[System.Serializable]
public class Receiver
{
    public string recieverName;
    public int signalCount;
}
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

    [SerializeField]
    private List<Receiver> mineRecieverList=new List<Receiver>();
    private float timerStartValue;

    private bool InDanger=false;
    private SpriteRenderer sprite;

    void Start()
    {
        timerStartValue=timer;
        sprite=transform.GetChild(0).GetComponent<SpriteRenderer>();
    }
    
    void LateUpdate()
    {
        if(timerIsActive)
        {
            timer-=Time.deltaTime;
            if(timer<0)
            {
                readData.MoveBySignal(gameObject,mineRecieverList);
                timer=timerStartValue;
            }

        }
        
    }

    private void IsInDanger()
    {
        sprite.color=Color.red;
        if(!InDanger)
        {
            sprite.color=Color.white;
        }
    }

    
}

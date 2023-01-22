using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadData : MonoBehaviour
{
    
    [SerializeField]
    private List<GameObject> signalList=new List<GameObject>();

    private float firstSignal=0,secondSignal=0;
    private string firstSignalName,secondSignalName;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private ReceiverList ReadJson()
    {
        string JsonData = System.IO.File.ReadAllText(Application.persistentDataPath + "/RecieverList.json");
        ReceiverList receiver = JsonUtility. FromJson<ReceiverList>(JsonData);
        return receiver;
    }

    public void MoveBySignal(GameObject miner)
    {
        Vector3 firstPos=Vector3.zero;
        Vector3 secondPos=Vector3.zero;

        GetTwoBigRecieverSignal();

        for (int i = 0; i < signalList.Count; i++)
        {
            if(signalList[i].name==firstSignalName)
            {
                firstPos=signalList[i].transform.position;
            }
            else if(signalList[i].name==secondSignalName)
            {
                secondPos=signalList[i].transform.position;
            }
        }
        Debug.Log(firstSignal);
        Debug.Log(secondSignal);
        
        var desSignalCount=firstSignal

        //var desPosZ=((Math.Abs(secondPos.z-firstPos.z))*(firstSignal/(firstSignal+secondSignal))+secondPos.z);
        var desPos=new Vector3((Math.Abs(secondPos.x-firstPos.x))*(firstSignal/(firstSignal+secondSignal))+secondPos.x
        ,Math.Abs(secondPos.y-firstPos.y)*(firstSignal/(firstSignal+secondSignal))+secondPos.y,
        (Math.Abs(secondPos.z-firstPos.z)));
        Debug.Log(desPos);

        miner.transform.position=Vector3.down;
        firstSignal=0;
        secondSignal=0;

    }


    private void GetTwoBigRecieverSignal()
    {
        var recieverList=ReadJson().receiverList;
        var tempSignal=recieverList[0];

        for (int i = 0; i < recieverList.Count ; i++)
        {
            var tempName=recieverList[i].recieverName;
            
            if(recieverList[i].signalCount>firstSignal)
            {
                firstSignal=recieverList[i].signalCount;
                firstSignalName=tempName;
                tempSignal=recieverList[i];
                
            }
        }
        recieverList.Remove(tempSignal);
        for (int i = 0; i < recieverList.Count ; i++)
        {
            var tempName=recieverList[i].recieverName;
            
            if(recieverList[i].signalCount>secondSignal)
            {
                secondSignal=recieverList[i].signalCount;
                secondSignalName=tempName;
                
            }
        }
        recieverList.Add(tempSignal);

        Debug.Log(firstSignal);
        Debug.Log(secondSignal);
        
    }
}
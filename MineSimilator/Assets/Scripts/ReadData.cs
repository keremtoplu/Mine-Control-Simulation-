using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadData : MonoBehaviour
{
    
    [SerializeField]
    private List<GameObject> signalList=new List<GameObject>();



    private float bigSignal=0,secondBigSignal=0,firstSignal=0,secondSignal=0;
    private  int countBig,countSmall;
    private string bigSignalName,secondBigSignalName;
    private GameObject firstPos,secondPos,secondBigPos,bigPos;
                                    
                                
    
    

    private MinerList ReadJson()
    {
        string JsonData = System.IO.File.ReadAllText(Application.persistentDataPath + "/RecieverList.json");
        MinerList minerList = JsonUtility. FromJson<MinerList>(JsonData);
        return minerList;
    }

    public void MoveBySignal(GameObject miner, int minerNumber)
    {
    

        GetTwoBigRecieverSignal(minerNumber);

        for (int i = 0; i < signalList.Count; i++)
        {
            if(signalList[i].name==bigSignalName)
            {
                bigPos=signalList[i];
                countBig=i;
            }
            else if(signalList[i].name==secondBigSignalName)
            {
                secondBigPos=signalList[i];
                countSmall=i;
            }
        }
        
        if(countBig>countSmall)
        {
            firstPos=secondBigPos;
            secondPos=bigPos;
        }
        else
        {
            firstPos=bigPos;
            secondPos=secondBigPos;
        }

        Debug.Log(bigSignal);
        Debug.Log(secondBigSignal);
        Debug.Log(bigPos);
        Debug.Log(secondBigPos);

        var desPosZ=((Math.Abs(firstPos.transform.position.z-secondPos.transform.position.z))*(secondSignal/(firstSignal+secondSignal))+firstPos.transform.position.z);
        // var desPos=new Vector3((Math.Abs(secondPos.x-firstPos.x))*(firstSignal/(firstSignal+secondSignal))+secondPos.x
        // ,Math.Abs(secondPos.y-firstPos.y)*(firstSignal/(firstSignal+secondSignal))+secondPos.y,
        // (Math.Abs(secondPos.z-firstPos.z)));
        Debug.Log(desPosZ);
        LeanTween.move(miner,new Vector3(secondPos.transform.position.x,miner.transform.position.y,desPosZ),.5f).setEaseInCubic().setOnComplete(()=>
        {
            bigSignal=0;
            secondBigSignal=0;
        });
        //miner.transform.position=Vector3.down;
        

    }


    private void GetTwoBigRecieverSignal(int minerNumber)
    {
        
        var recieverList=ReadJson().minerRecieverList[minerNumber].recieverList;
        var tempSignal=recieverList[0];

        for (int i = 0; i < recieverList.Count ; i++)
        {
            var tempName=recieverList[i].recieverName;
            
            if(recieverList[i].signalCount>bigSignal)
            {
                bigSignal=recieverList[i].signalCount;
                bigSignalName=tempName;
                tempSignal=recieverList[i];
                firstSignal=i;
                
            }
        }
        recieverList.Remove(tempSignal);
        for (int i = 0; i < recieverList.Count ; i++)
        {
            var tempName=recieverList[i].recieverName;
            
            if(recieverList[i].signalCount>secondBigSignal)
            {
                secondBigSignal=recieverList[i].signalCount;
                secondBigSignalName=tempName;
                secondSignal=i;
                
            }
        }
        recieverList.Add(tempSignal);

        if(firstSignal>secondSignal)
        {
            firstSignal=secondBigSignal;
            secondSignal=bigSignal;
        }
        else
        {
            firstSignal=bigSignal;
            secondSignal=secondBigSignal;
        }

        Debug.Log(bigSignal);
        Debug.Log(secondBigSignal);
        
    }
}
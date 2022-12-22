using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadData : MonoBehaviour
{
    
    [SerializeField]
    private List<GameObject> signalList=new List<GameObject>();

    private int firstSignal=0,secondSignal=0;
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

    private void GetTwoBigRecieverSignal()
    {
        var recieverList=ReadJson().receiverList;
        int receiverListCount=recieverList.Count;
        for (int i = 0; i < receiverListCount ; i++)
        {
            var tempSignal=recieverList[i].signalCount;
            var tempName=recieverList[i].recieverName;
            if(tempSignal>firstSignal)
            {
                firstSignal=tempSignal;
                firstSignalName=tempName;
            }
            else if(tempSignal>secondSignal && tempSignal<firstSignal)
            {
                secondSignal=tempSignal;
                secondSignalName=tempName;
            }
            Debug.Log(recieverList[i]);

        }
         Debug.Log(firstSignalName);
        Debug.Log(secondSignalName);

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

        var desPosZ=-((Math.Abs(firstPos.z-secondPos.z)*(secondSignal/(firstSignal+secondSignal)))+firstPos.z);
        Debug.Log(desPosZ);

        miner.transform.position=new Vector3(miner.transform.position.x,miner.transform.position.y,desPosZ);

    }
}

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
public class AddReciever : MonoBehaviour
{
    // [SerializeField]
    // private List<Receiver> _recieverList=new List<Receiver>();

    // [SerializeField]
    // private List<Receiver> _recieverList1=new List<Receiver>();

    // [SerializeField]
    // private List<Receiver> _recieverList2=new List<Receiver>();
    
    [SerializeField]
    private GameObject recieverPref;

    [SerializeField]
    private GameObject recieversParent;

    [SerializeField]
    private ReadData readData;

    // public void _JsonSave()
    // {

    //     MinerList miner=new MinerList();
    //     RecieverList recieverList=new RecieverList();
    //     RecieverList recieverList2=new RecieverList();
    //     RecieverList recieverList3=new RecieverList();
        
    //     for (int i = 0; i < _recieverList.Count; i++)
    //     {
    //         recieverList.recieverList.Add(_recieverList[i]);
    //         recieverList2.recieverList.Add(_recieverList1[i]);
    //         recieverList3.recieverList.Add(_recieverList2[i]);
    //     }

    //     miner.minerRecieverList.Add(recieverList);
    //     miner.minerRecieverList.Add(recieverList2);
    //     miner.minerRecieverList.Add(recieverList3);
        
    //     string convertJson=JsonUtility.ToJson(miner);
    //     System.IO.File.WriteAllText(Application.persistentDataPath+"/RecieverList.json",convertJson);
    // }



    private void Start() 
    {
        
        for (int i = 1; i <= PlayerPrefs.GetInt("currentSaveReciever"); i++)
        {
            var x=PlayerPrefs.GetFloat("posx"+i);
            var y=PlayerPrefs.GetFloat("posy"+i);
            var z=PlayerPrefs.GetFloat("posz"+i);
            var desPos= new Vector3(x,y,z);

           var newReciever= Instantiate(recieverPref,desPos,Quaternion.identity,recieversParent.transform);
           newReciever.GetComponent<Reciever>().IsBreak=true;
           newReciever.name="A"+(readData.signalList.Count+1);
           readData.signalList.Add(newReciever);
        }    
    }

    //anten ekleme

    public void ProduceReciever()
    {
        var newReciever=Instantiate(recieverPref);
        PlayerPrefs.SetInt("currentSaveReciever",PlayerPrefs.GetInt("currentSaveReciever")+1);
        newReciever.name="A"+(readData.signalList.Count+1);
        readData.signalList.Add(newReciever);


    }



}

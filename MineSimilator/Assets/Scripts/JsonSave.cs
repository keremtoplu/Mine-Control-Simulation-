using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ReceiverList
{
    public List<Receiver> receiverList= new List<Receiver>();
}

[System.Serializable]
public class Receiver
{
    public string recieverName;
    public int signalCount;
}
public class JsonSave : MonoBehaviour
{
    [SerializeField]
    private int _signalCount;
    [SerializeField]
    private string _recieverName;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void _JsonSave(ReceiverList receiver)
    {
        Receiver receiver1=new Receiver();
        receiver1.recieverName=_recieverName;
        receiver1.signalCount=_signalCount;
        receiver.receiverList.Add(receiver1);
        string convertJson=JsonUtility.ToJson(receiver);
        System.IO.File.WriteAllText(Application.persistentDataPath+"/RecieverList.json",convertJson);
    }

    private ReceiverList ReadJson()
    {
        string JsonData = System.IO.File.ReadAllText(Application.persistentDataPath + "/RecieverList.json");
        ReceiverList receiver = JsonUtility. FromJson<ReceiverList>(JsonData);
        return receiver;
    }
}

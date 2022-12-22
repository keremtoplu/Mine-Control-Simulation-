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
public class AddReciever : MonoBehaviour
{
    [SerializeField]
    private List<Receiver> recieverList=new List<Receiver>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void _JsonSave()
    {
        ReceiverList receiver=new ReceiverList();
        for (int i = 0; i < recieverList.Count; i++)
        {
            receiver.receiverList.Add(recieverList[i]);
            
        }
        
        string convertJson=JsonUtility.ToJson(receiver);
        System.IO.File.WriteAllText(Application.persistentDataPath+"/RecieverList.json",convertJson);
    }

}

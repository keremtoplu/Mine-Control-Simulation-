
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class TagManager : Singleton<TagManager>
{
    [SerializeField]
    private GameObject tagPref;

    [SerializeField]
    private Vector3 startPos;

    [SerializeField]
    private int maxTagCount;

    [SerializeField]
    private ReadData readData;

    [SerializeField]
    private RecieverManager recieverManager;

    private List<GameObject> tagList=new List<GameObject>();
    private string[] data;
    
    void Start()
    {
            
        for (int i = 0; i < maxTagCount; i++)
        {
           var newTag= Instantiate(tagPref,startPos,Quaternion.identity);
            newTag.GetComponent<Miner>().readData = readData;
            newTag.GetComponent<Miner>().MinerNumber=i;
            tagList.Add(newTag);
            newTag.name = i.ToString();
            newTag.SetActive(false);
            Debug.Log(newTag);
        }
        recieverManager._JsonSave(maxTagCount);
    }

    void Update()
    {

        
    }


   
    public void UpdateMinersRecieverListCount()
    {
        StartCoroutine(GetRequest("http://127.0.0.1:8080/sql/sql6.php"));

            for (int i = 0; i < tagList.Count; i++)
            {
                string[] fin = data[i].Split("/");
                Debug.Log(fin[0]);

                if (tagList[i].name == fin[0])
                {

                var mineRecieverList = readData.ReadJson().minerRecieverList[i].recieverList;
                
                    for (int x = 0; x < mineRecieverList.Count; x++)
                    {
                        mineRecieverList[x].recieverName=readData.signalList[x].name;
                        if (mineRecieverList[x].recieverName == fin[3])
                        {
                            //mineRecieverList[x].signalCount = Convert.ToInt32(fin[2]);
                            recieverManager.UpdateJsonSave(mineRecieverList,Convert.ToInt32(fin[2]),x);
                        }
                    }
                }
            }
        

    }

    
    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);

                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    string raw = webRequest.downloadHandler.text;
                    Debug.Log(raw);
                    //example raw= 35/-13,33/-32,
                    data = raw.Split(",");

                    break;
            }
        }
    }

}

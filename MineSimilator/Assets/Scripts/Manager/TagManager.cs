using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagManager : Singleton<TagManager>
{
    [SerializeField]
    private GameObject tagPref;

    [SerializeField]
    private Vector3 startPos;

    [SerializeField]
    private int maxTagCount;

    private List<GameObject> tagList=new List<GameObject>();
    void Start()
    {
        for (int i = 1; i < maxTagCount; i++)
        {
           var newTag= Instantiate(tagPref,startPos,Quaternion.identity);
           tagList.Add(newTag);
           newTag.SetActive(false);
        }
    }

    void Update()
    {

        
    }



    private void UpdateMinersRecieverListCount()
    {
        
        if(/* tag ın numara kontrolü yapılacak*/maxTagCount>50)
        {
            for (int i = 0; i < tagList.Count; i++)
            {
                if(tagList[i].name==88.ToString())
                {
                    
                }
            }
        }
    }
}

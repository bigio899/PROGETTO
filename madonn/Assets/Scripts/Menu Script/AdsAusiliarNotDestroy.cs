using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsAusiliarNotDestroy : MonoBehaviour
{
    [SerializeField] GameObject adsManager;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(adsManager); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

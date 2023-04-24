using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyAdsManag : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
}

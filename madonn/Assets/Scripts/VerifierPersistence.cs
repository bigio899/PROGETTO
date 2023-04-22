using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerifierPersistence : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindWithTag("AuxPersistence"))
        {
            Destroy(GameObject.FindWithTag("AuxPersistence"));
            DontDestroyOnLoad(GameObject.FindWithTag("Persistence"));
            GameObject.FindWithTag("Persistence").tag = "AuxPersistence";
        }
    }
}

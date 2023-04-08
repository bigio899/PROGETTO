using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchLevelToMenu : MonoBehaviour
{
    public void SwtichLevelToMenu()
    {
        SceneManager.LoadScene(0);
    }
}

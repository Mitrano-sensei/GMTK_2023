using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSceneManager : MonoBehaviour
{
    public void loadFirstLevel()
    {
        // Load Level_1
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level_1");
    }
}

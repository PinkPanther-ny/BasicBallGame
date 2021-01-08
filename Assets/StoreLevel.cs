using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoreLevel : MonoBehaviour
{
    private void Awake()
    {
        PlayerPrefs.SetString("LastLevel", SceneManager.GetActiveScene().name);
        Debug.Log(PlayerPrefs.GetString("LastLevel"));
    }

}

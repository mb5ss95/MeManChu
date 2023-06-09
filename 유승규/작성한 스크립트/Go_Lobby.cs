using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class Go_Lobby : MonoBehaviour
{
    
    public void Lobby()
    {
        Debug.Log("°¡ÀÚ");  
        SceneManager.LoadScene("MainScene");
    }
}
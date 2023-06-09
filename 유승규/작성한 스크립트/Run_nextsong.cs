using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Run_nextsong : MonoBehaviour
{
    public Button btn1, btn2;
    // Start is called before the first frame update
    void Start()
    {
        btn1.onClick.AddListener(nextsong);
        btn2.onClick.AddListener(songplay);
    }
    void nextsong()
    {
        GameObject.Find("Plane").GetComponent<Run_Bgm>().AS.Stop();
        Debug.Log("¥Ÿ¿Ω ∞Ó");
    }
    void songplay()
    {
        GameObject.Find("Plane").GetComponent<Run_Bgm>().AS.Stop();
        GameObject.Find("Plane").GetComponent<Run_Bgm>().check = !GameObject.Find("Plane").GetComponent<Run_Bgm>().check;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Run_clear : MonoBehaviour
{
    private GameObject exit;
    private float runtime;
    private TextMeshProUGUI cleartime;
    private bool Goal = false;
    private int death;
    // Start is called before the first frame update
    private void Awake()
    {
        exit = GameObject.Find("Panel");
    }
    private void Start()
    {   
        exit.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {   
        if (Goal == false)
        {
        exit.SetActive(true);
        cleartime = GameObject.Find("ClearTime").GetComponent<TextMeshProUGUI>();
        runtime = GameObject.Find("Plane").GetComponent<Run_stopwatch>().time;
        death = GameObject.Find("Plane").GetComponent<Run_stopwatch>().deathcount;
        float min = Mathf.Floor(runtime / 59);
        float sec = Mathf.RoundToInt(runtime % 59);
        cleartime.text = "경과 시간 : " + min + "분" + sec + "초" +" " + "떨어진 횟수 : " + death;
        Goal= true;

        }
    }
}

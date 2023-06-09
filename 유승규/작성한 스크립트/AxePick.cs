using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxePick : MonoBehaviour
{
    public bool HaveAxe = false;
    
    private void OnCollisionStay(Collision collision)
    {
      
        if (Input.GetKey(KeyCode.C))
        {
            print("도끼를 주었다!");
            HaveAxe = true;
       
        }
    }
}

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
            print("������ �־���!");
            HaveAxe = true;
       
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrushTable : MonoBehaviour
{
    private AxePick axepick;
    // Start is called before the first frame update
    private void OnCollisionStay(Collision collision)
    {
        
        if (Input.GetKey(KeyCode.C))
        {
            if(GameObject.Find("Axe").GetComponent<AxePick>().HaveAxe == false)
            {
                print("도끼가 필요해!");
            }
            else
            if(GameObject.Find("Axe").GetComponent<AxePick>().HaveAxe == true)
            {
                Destroy(gameObject, .5f);
            }

        }
    }
}

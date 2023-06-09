using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Magic : MonoBehaviour
{
    [SerializeField] GameObject parentBox;
    BoxCollider boxCollider;
    public RectTransform MagicGroup;
    public TMP_Text spell;
    horrorPlayer player;
    bool isOpen;

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            player = other.GetComponent<horrorPlayer>();
            player.accessText.text = "비밀스러운 벽이다...";
            player.accessText.gameObject.SetActive(true);

            if (player.eDown)
            {
                spell.text = "";
                parentBox.GetComponent<BoxCollider>().enabled = false;
                boxCollider.enabled = false;
                player.accessText.gameObject.SetActive(false);
                player.moveFalse();
                MagicGroup.anchoredPosition = Vector3.zero;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            player.moveTrue();
            player.accessText.gameObject.SetActive(false);
            MagicGroup.anchoredPosition = Vector3.down * 1000;

        }
    }

    public void SpellCheck()
    {
        if (!isOpen)
        {
            player.moveTrue();  
            MagicGroup.anchoredPosition = Vector3.down * 1000;
            if (spell.text == "김태영짱짱맨")
            {
                isOpen = true;
                StartCoroutine(Success());
                StartCoroutine(OpenShelf());
            }
            else
            {
                spell.text = "";
                StartCoroutine(Fail());
            }       
        }
    }

    IEnumerator Success()
    {
        player.accessText.text = "비밀 공간이 열립니다.";
        player.accessText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        player.accessText.gameObject.SetActive(false);
    }

    IEnumerator Fail()
    {
        player.accessText.color = Color.red;
        player.accessText.text = "주문이 틀렸습니다.";
        player.accessText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        player.accessText.color = Color.white;
        player.accessText.gameObject.SetActive(false);
        this.GetComponent<BoxCollider>().enabled = true;
    }

    IEnumerator OpenShelf ()
    {
        for (int i = 0; i < 90; i++)
        {
            yield return new WaitForSeconds(0.005f);
            transform.Rotate(0, -1, 0);
        }
    }
}

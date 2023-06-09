using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 15f;
    private Rigidbody bulletRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        
        // ���� ������Ʈ���� Rigidbody ������Ʈ�� ã�� bulletRigidbody�� �Ҵ�
        bulletRigidbody = GetComponent<Rigidbody>();
        // ������ٵ��� �ӵ� = dkvWhrqkdgid ���ʹ��� * �̵� �ӷ�
        bulletRigidbody.velocity = transform.forward* speed;

        // 3�� �ڿ� �ڽ��� ���� ������Ʈ �ı�
        Destroy(gameObject, 3f);
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}

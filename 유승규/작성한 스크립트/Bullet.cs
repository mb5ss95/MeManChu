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
        
        // 게임 오브젝트에서 Rigidbody 컴포넌트를 찾아 bulletRigidbody에 할당
        bulletRigidbody = GetComponent<Rigidbody>();
        // 리지드바디의 속도 = dkvWhrqkdgid 앞쪽방향 * 이동 속력
        bulletRigidbody.velocity = transform.forward* speed;

        // 3초 뒤에 자신의 게임 오브젝트 파괴
        Destroy(gameObject, 3f);
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}

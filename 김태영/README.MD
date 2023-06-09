Mesh Renderer를 끄면 겉보기만 감춰짐 (collider는 그대로 남아있음)

프리펩(Prefab) : 게임오브젝트를 에셋으로 보관한 형태

Player에 필요한 컴포넌트 : Collider + Rigidbody + Script

Input 값은 Edit > Project Settings > Input Manager에서 변경 가능

GetAxisRaw() : Axis 값을 정수로 반환하는 함수
normalized : 방향 값이 1로 보정된 벡터 (대각선도 속도 같게)

transform 이동은 Time.deltaTime 곱해주기

Player가 관성에 의해 안 엎어지게 하려면 Rigidbody > Constraints > Freeze Rotation X, Z 체크

transform 이동은 물리 충돌을 무시하는 경우가 발생
-> Rigidbody > Collision Detection을 Continuous로 변경
( Static과 충돌할 때 효과적 )

애니메이터 사용시
애니메이션 상태 사이를 Transition 으로 연결
파라메터(매개변수) 추가
파라메터를 Condition에 사용하여 Transition 완성
애니메이션 생성 시 Loop Time 체크

LookAt() : 지정된 벡터를 향해서 회전시켜주는 함수
        // 우리가 나아가는 방향으로 바라본다.
        transform.LookAt(transform.position + moveVec);

코드 기능에 따라 구분되게 함수로 분리하기

키보드를 누르고 있을 때
Input.GetButton();
키보드를 누른 그 순간만
Input.GetButtonDown();

AddForce() 함수로 물리적인 힘을 가하기
OnCollisionEnter() 이벤트 함수로 착지 구현

일회성 애니메이션은 Trigger 파라메터 사용

중력 설정
Edit > Project Settings > Physics

바닥 Rigidbody 설정
Use Gravity 체크 해제
IsKinematic 체크 ( 직접 움직이지 않는 한 안 움직임 )

마찰력이 없는 벽을 만들기 위해 Physics Material 추가

Invoke() 함수로 시간차 함수 호출

RotateAround(위치, 회전축, 회전수치) : 타겟 주위를 회전하는 함수 
// 목표가 움직이면 일그러지는 단점이 있음

오브젝트 꾸미기
- Light
	Type : Point
	Range : 빛의범위
	Color : 빛의 색깔
	Intensity : 빛의 강도
- Particle System
	Renderer : Material 찾아서 연결
	(색상이 자홍색 이면 재질을 못 찾았다는 의미)
	(Default-Line 연결)
	Emission : 파티클 입자 출력양
		- Rate over Distance : 파티클의 위치 변화에 따라 입자 생성
	Shape : 파티클 입자 출력 모양
	Color Over Life Time : 시간에 따른 색상변화
	(아래쪽은 색, 위쪽은 투명도)
	Size Over Life Time : 시간에 따른 크기변화
	Limit Velocity over Life Time : 시간에 따른 속도제한
	(Drag값 조정해서 퍼지는 범위 결정)
	Start Life Time : 입자 생존 시간
	Start Speed : 입자 시작 속도

상호작용 위한 Collider는 꼭 IsTrigger 체크

OnTriggerEnter : trigger 달린 놈이랑 접촉하면
OnTriggerStay : trigger 달린 놈이랑 접촉 중이면
OnTriggerExit : 접촉에서 벗어나면

프리팹 위치는 항상 0으로 초기화

캐릭터가 물건 손에 들게 하려면
해당 위치에 Creat 3D Object한 후 물건 프리펩 끌어다 놓기

Trail Renderer : 잔상을 그려주는 컴포넌트
	Width Curve : 꼬리 모양 내도록 넓이 설정
	Time : 잔상의 길이
	Min Vertex Distance : 잔상 꺾임을 설정
	Color : 잔상 색상

코루틴 함수 : 메인루틴 + 코루틴 (동시 실행)
	StartCoroutine("") : 코루틴 실행 함수
	StartCoroutine("") : 코루틴 중단 함수
	IEnumerator : 열거형 함수 클래스
	yield : 결과를 전달하는 키워드
		- yield 키워드를 여러개 사용하여 시간차 로직 작성 가능
		- WaitForSeconds() : 주어진 수치만큼 기다리는 함수
		- yield break로 코루틴 탈출
	While문에는 꼭 yield return null을 포함

프리펩 인스턴스 생성
GameObject instant = Instantiate(프리펩, 위치, 각도);

Rigidbody 메서드
	- AddForce() : 힘 가하기
	- AddTorque() : 회전 시키기
			
마우스에 의한 캐릭터 회전 구현
            // ScreenPointToRay() : 스크린에서 월드로 Ray를 쏘는 함수
            Ray ray = followCamera.ScreenPointToRay(Input.mousePosition);
            // RaycastHit 정보를 저장할 변수 추가
            RaycastHit rayHit;
            // out : return처럼 반환값을 주어진 변수에 저장하는 키워드
            if (Physics.Raycast(ray, out rayHit, 100))
            {
                // RayCastHit의 마우스 클릭 위치 활용하여 회전을 구현
                Vector3 nextVec = rayHit.point - transform.position;
                transform.LookAt(transform.position + nextVec);
            }

상속 시 주의사항
	Awake() 함수는 자식 스크립트만 단독 실행!

Rigidbody
AngularVelocity : 물리 회전 속도

Edit > Physics > Layer Collsion Masks : 레이어들의 상호간 충돌 설정

벽 관통 방지
DrawRay(시작 위치, 쏘는 방향, 색깔) : Scene내에서 Ray를 보여주는 함수
Raycast(시작 위치, 방향, 길이, 레이어마스크) : Ray를 쏘아 닿는 오브젝트를 감지하는 함수

Material은  Mesh Renderer 컴포넌트에서 접근 가능

프리펩 생성
GameObject 변수 = Instantiate(프리펩, 위치, 각도)

구체 모양의 레이캐스팅 (모든 오브젝트)
RaycastHit[] rayHits = Physics.SphereCastAll(위치, 반지름, 쏘는 방향, 레이를 쏘는 길이, 레이어 마스크)

SetDestination() : 도착할 목표 위치 지정 함수
# A.I.
Nav 관련 클래스는 UnityEngine.AI 네임스페이스 사용 
NavMeshAgent : Navigation을 사용하는 인공지능 컴포넌트
NavAgent를 사용하기 위해선 NavMesh가 필요
NavMesh : NavAgent가 경로를 그리기 위한 바탕 (Mesh)
window > AI > Navigation
Navigation 창에서 NavMesh를 Bake로 생성
NavMesh는 Static 오브젝트만 Bake 가능
Navigation의 Agent 세팅 값으로 NavMesh 결정

# UI
UI 클래스 사용을 위해 using UnityEngine.UI 네임스페이스 호출
UI생성 : 하이어라키 + > UI > Canvas
 - EventSystem : UI에서 키 입력을 결정해주는 컴포넌트

Panel 생성 :  UI > Panel
Image 생성 : UI > Image
	- 이미지 추가 후 스프라이트를 Image > Source Image에 드래그하여 적용
Text 생성 : UI > Legacy > Text
	- Text의 크기를 무시하고 보여주려면 OverFlow 선택
	- Text가 흐려 보일 때 : Text Scale의 값을 작게 할 수록 뚜렷한 느낌을 얻을 수 있다.
Button 생성 : UI > Legacy > Button
	- 이미지 깨져 보일 때 : Image Type(Simple) > Set Native Size > Image Type(Sliced)
Set Native Size : UI 크기를 이미지 원래 크기로 맞추는 기능

Canvas > Canvas > Pixel Perfect 체크 : sprite 도트 이미지 사용 시
Canvas > Canvas Scaler > UI Scale Mode -> Scale With Screen Size (어느 해상도에서도 크기가 동일하게 출력)
	해상도 설정 Reference Resolution - x : 1920, y : 1080

Text 대신 TextProMesh 사용시
- 네임 스페이스 선언 using TMPro


UI 생성 시 게임 오브젝트 크기는 0으로 만들기(위치만 잡아주기)
기준점 위치에 따라서 크기가 작아지는 방향이 달라짐 (앵커로 기준점 잡기)
Canvas 크기는 메인카메라 크기와 동일
인게임 : 월드 좌표계 - Transform
UI : 스크린 좌표계 - RectTransform
앵커(Anchor) : UI 중심과 위치의 방향 기준
	Shift : UI 중심
	Alt : UI 위치
UI 화면 중앙에 띄우기 : ui(RectTransform형태).anchoredPosition = Vector3.zero
Button에 On Click() 이벤트에 함수 연결 가능
키보드 입력이 버튼을 클릭하지 않도록 Navigation 설정 (None으로 설정)
this : 자기 자신에 접근할 때

PlayerPrefs : 유니티에서 제공한는 간단한 저장 기능 (Float, Int, String 가능)
	저장하기 : PlayerPrefs.SetInt("변수", 변수값);
	불러오기 : PlayerPrefs.GetInt("변수", 변수값);
첫 실행할 때는 PlayerPrefs의 Key 세팅 필요
HasKey()함수로 Key가 있는지 확인 후, 없다면 0으로 저장

string.Format() : 함수로 문자열 양식 적용
LateUpdate() : Update()가 끝난 후 호출되는 생명주기
3D Text : 3D 공간의 텍스트 컴포넌트 (최신 버전 : 3D Object > Legacy > Text Mesh)
Quaternion.identity : 회전 안줄 때
프리펩은 Scene에 올라온 오브젝트에 접근 불가능
while문을 안전하게 돌리기 위해서는 꼭 yield return 포함

Scene 관련 함수를 사용하려면 SceneManagement 필요
	LoadScene() : 씬을 다시 불러 재시작 구현 (SceneManager.LoadScene(0))

AudioSource : 사운드 사용시
	Play On Awake 해제
	소리가 나야할 액션, 활동 로직에 Play() 함수 호출

빌드 시 게임에 들어갈 Scene을 Scenes in build에 추가
Resolution 항목에서 해상도 조정

Find 관련 함수 사용 시 주의
	: 바로 자기 자식까지 접근가능, 그 이상은 /로 구분지어서 타겟오브젝트까지 들어가야함

# Scene
using UnityEngine.SceneManagement;
현재 씬 재로드 : SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
이름으로 씬 불러오기 : SceneManager.LoadScene("씬이름");
번호로 씬 불러오기 : SceneManager.LoadScene(0); - 0번째 씬

씬 넘어갈 때 데이터 파괴되지 않게 하는 법
```c#
public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    public Color TeamColor;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
```
유니티에서 씬이 변경 될 때는 일반적으로 static으로 선언된 변수를 제외하고 모든 데이터가 소실됨
유지시키고 싶은 데이터를 가지는 오브젝트와 스크립트를 생성 후 public static키워드로 전역에서 참조할 수 있는 객체의 인스턴스를 선언.
DontDestroyOnLoad(gameObject)를 통해 씬이 넘어갈 때 gameObject가 파괴되는 걸 막음
인스턴스가 이미 할당 되었다면 새로 생성되는 gameObject를 파괴해서 여러개의 gameObject가 생성되는 것을 막는다.
	
# TIP

## 1. 태그 이름 설정
Tag는 문자열로 비교하므로 대소문자를 정확히 지켜야한다. Tag, Sorting Layer 및 Layer는 모두 대문자로 작성하는 것이 좋다.(<- 개인의 취향 차이)
<hr>

## 2. 태그 비교
태그를 비교할 때 collider.collider.tag == "문자열" 또는 coll.gameObject.tag == "문자열"과 같이 게임오브젝트의 문자 속성을 가져오는 코드는 실행 시 해당 문자열의 복사본을 생성한다.
이 복사본은 특정 메모리에 할당되며 가비지 컬렉션의 대상이 된다.
가비지 컬렉션이 처리해야할 대상이 많아질수록 해제하는 시점에 잠시 점추는 렉 현상이 발생한다.
가비지 컬렉션이 발생하지 않는 CompareTag 함수를 사용하는 방법을 적극적으로 권장한다.
```C#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBullet : MonoBehaviour
{
    // 충돌이 시작할 때 발생하는 이벤트
    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.collider.tag == "BULLET")
        if (collision.collider.CompareTag("BULLET"))
        {
            Destroy(collision.gameObject);
        }
    }
}
```

<hr>

## 3. Create Empty Child
게임 개발을 진행하다 보면 어떤 게임오브젝트 하위로 차일드화해서 빈 게임오비젝트를 생성해야 할 경우가 많다.
### - 1. 부모가 될 게임오브젝트를 선택하고 [+]버튼을 클릭해 [Create Empty Child]를 선택하거나 부모가 될 게임오브젝트에서 마우스 오른쪽 클릭해 팝업되는 컨텍스트 메뉴에서 [Create Empty Child]를 선택한다.
### - 2. 게임 오브젝트를 선택하고 단축키(윈도우 : Shift + Alt + N, 맥 : Shift + Option + N) 사용

<hr>

## 4. 빈 게임 오브젝트의 기즈모 활용
Transform 컴포넌트만 가진 게임오브젝트는 씬 뷰에서 직접 마우스로 클릭하거나 영역을 드래그해서 선택할 수 없다.
```C#
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGizmos : MonoBehaviour
{
    public Color _color = Color.yellow;
    public float _radius = 0.1f;

    private void OnDrawGizmos()
    {
        Gizmos.color = _color;
        Gizmos.DrawSphere(transform.position, _radius);
    }
}
```
<hr>

## 5. 쿼터니언
우리가 흔히 알고있는 각도인 90도는 오일러 각이다. 하지만 유니티는 이 오일러 각을 내부적으로 Quaternion 타입으로 변형해 사용한다.
오일러 각은 수학자 오일러가 고안한 것으로 3차원 공간의 절대 좌표를 기준으로 물체가 얼마나 회전했는지 측정하는 방식이다.
그런데 오일러 각을 이용한 회전 방식이 회전하는 동안 X, Y, Z 축 중 2개의 축이 겹쳐지면 어느 축으로도 회전하지 않고 잠기는 현상이 발생한다.
이 현상을 짐벌 락이라고 한다.

이러한 문제점을 해결하기 위해 쿼터니언이라는 대안이 제시됐다.
쿼터니언은 4차원 복소수로서, 오일러 각이 각축에 대해 차례로 회전시켰다면 쿼터니언은 세 개의 축을 동시에 회전시켜 짐벌 락 현상을 방지한다.
```C#
transform.Rotate(0, 30, 0);
transform.rotation = Quaternion.Eular(0, 30, 0);
```
위의 두 개는 같은 결과다. 다만, rotation의 경우 Quaternion.Eular(x, y, z) 함수를 이용해 오일러 각을 Quaternion 타입으로 변환해 대입해야 한다.

<hr>

## 6. Collsion.contacts(x) / GetContact, GetContacts(o)
충돌 지점의 접점 정보는 Collision.contacts 속성으로도 알 수 있지만, contacts 속성은 가비지 컬렉션을 발생시키기 때문에 GetContact, GetContacts 함수를 사용하기를 권한다.

<hr>

## 7. Physics.OverlapSphere (x) / Physics.OverlapSphereNonAlloc (o)
Physics.OverlapSphere 함수는 실행 시 Sphere 범위에 검출될 개수가 명확하지 않을 떄만 사용해야 한다. 메모리 Garbage가 발생하기 떄문이다.
따라서 Sphere 범위에 검출될 개수가 명확할 때는 Garbage가 발생하지 않는 Physics.OverlapSphereNonAlloc 함수를 사용하기를 권장한다.

<hr>

## 8. 연산 처리 속도가 빠른 Collider
Sphere Collider
Capsule Collider
Box Collider
Mesh Collider는 움직이지 않는 것만 적용하는 것이 좋다. 연산 처리량이 높기 때문이다.

<hr>

## 9. 여러 대의 카메라를 배치했을 때 반드시 확인해야 하는 주의사항
카메라에는 기본적으로 AudioListener 컴포넌트가 추가돼 있으므로 하나의 씬에 여러 개의 AudioListener 컴포넌트가 있다는 경고 메시지를 볼 수 있다.
그래서 어느 카메라에 있는 AudioListener 컴포넌트를 사용할 것인지 결정하고 나머지는 반드시 삭제해야 한다.

중복된 AudioListener를 삭제하지 않는다면 성능 저하가 생긴다.

<hr>

## 10. 코루틴 주의사항
코루틴 함수를 호출할 때 전달하는 인자는 함수의 원형(포인터)을 사용한다.
호출할 코루틴 함수명을 문자열로 전달할 수도 있지만, 그럴 경우 가비지 컬렉션이 발생하며 문자열로 호출한 코루틴은 개별적으로 정지시킬 수 없다는 문제가 있다.

StartCoroutine(Fire()); // 함수의 원형을 전달하는 방식(권장)
StartCoroutine("Fire"); // 함수명을 문자열로 전달하는 방식

<hr>

## 11. 차일드화 된 게임오브젝트 좌표, 회전 각도, 스케일 수정 할 때
```C#
muzzleFlash = firePos.GetComponentChildren<MeshRenderer>();
```
이와 같은 코드에서 muzzleFlash의 좌표, 회전 각도, 스케일을 수정하 싶을 때는 localPosition, localRotation, localScale 속성을 사용해야 한다.

```c#
muzzleFlash.transform.localRotation = Quaternion.Euler(0, 0, angle);
//위와 같은 코드
muzzleFlash.transform.localRotation = Quaternion.Euler(Vector3.forward * angle);

muzzleFlash.transform.localScale = Vector3.one * scale;
```

<hr>

좀비 ai 구현관련
navMesh, navMeshAgent
순찰 하다가 범위안에 들어오면 플레이어를 쫓는 형식
순찰 : 어떤 오브젝트(target)까지 목표를 정하고 이동
목표에 도착 시 잠시 쉬었다가 다른 목적지 까지 이동
순찰 도중에 레이캐스트 범위 안에 플레이어가 들어오면 플레이어를 일정시간 쫓음 (코루틴으로 구현)

23/04/04

메인 씬 오브젝트 추가

- 호텔 촌, 먹거리 타운, 오브젝트 재배치

음향 추가 (메인씬 메인ost - lostark mokoko village)

23/04/05

채팅 UI 수정
메인씬 음향 효과 수정
메인씬 terrain 수정
호러 하우스 수정
맵 이탈 방지 Plane 설치


23/04/06

최종 마무리
메인씬 음향 업데이트 (그냥 배경으로 깔았음)
메인씬 오브젝트 재배치
호러하우스 업데이트


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//public enum GameState
//{
//    Swiping = 0,
//    Running,
//    CarStoped,
//    GameEnd,
//}

public class CarController : MonoBehaviour
{
    public float speed = 0;
    Vector2 startPos;

    public int num = 0;

    //GameState gameState;        // 상태 : swipe 하고 있을 때부터 시작

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;       // 프레임 속도 60프레임으로 고정
        QualitySettings.vSyncCount = 0;

        
    }

    // Update is called once per frame
    void Update()
    {
        // 스와이프 길이를 구한다.
        if (Input.GetMouseButtonDown(0))             // 마우스를 클릭하면
        {
            if (speed != 0 || num >= 3)
                return;
            //this.speed = 0.2f;                      // 처음 속도를 설정

            // 마우스 단추를 클릭한 좌표
            this.startPos = Input.mousePosition;        // Input으로 mousePosition(마우스 좌표)을 받아옴
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (speed != 0 || num >= 3)
                return;

            // 마우스 버튼에서 손가락을 떼었을 때 좌표
            Vector2 endPos = Input.mousePosition;
            float swipeLength = (endPos.x - this.startPos.x);

            // 스와이프 길이를 처음 속도로 변경한다.
            if (swipeLength < 0)
                speed = 0.0005f;
            else
                this.speed = swipeLength / 500.0f;          // 바로 적용하면 너무 빠르므로 500으로 나눠줌

            // 효과음을 재생
            GetComponent<AudioSource>().Play();

            //gameState = GameState.Running;
            num++;

        }            

        transform.Translate(this.speed, 0, 0);      // 좌표 누적(x, y, z) ==> x축으로 이동
        this.speed *= 0.98f;                        // 감속

    }


}

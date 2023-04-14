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

    //GameState gameState;        // ���� : swipe �ϰ� ���� ������ ����

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;       // ������ �ӵ� 60���������� ����
        QualitySettings.vSyncCount = 0;

        
    }

    // Update is called once per frame
    void Update()
    {
        // �������� ���̸� ���Ѵ�.
        if (Input.GetMouseButtonDown(0))             // ���콺�� Ŭ���ϸ�
        {
            if (speed != 0 || num >= 3)
                return;
            //this.speed = 0.2f;                      // ó�� �ӵ��� ����

            // ���콺 ���߸� Ŭ���� ��ǥ
            this.startPos = Input.mousePosition;        // Input���� mousePosition(���콺 ��ǥ)�� �޾ƿ�
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (speed != 0 || num >= 3)
                return;

            // ���콺 ��ư���� �հ����� ������ �� ��ǥ
            Vector2 endPos = Input.mousePosition;
            float swipeLength = (endPos.x - this.startPos.x);

            // �������� ���̸� ó�� �ӵ��� �����Ѵ�.
            if (swipeLength < 0)
                speed = 0.0005f;
            else
                this.speed = swipeLength / 500.0f;          // �ٷ� �����ϸ� �ʹ� �����Ƿ� 500���� ������

            // ȿ������ ���
            GetComponent<AudioSource>().Play();

            //gameState = GameState.Running;
            num++;

        }            

        transform.Translate(this.speed, 0, 0);      // ��ǥ ����(x, y, z) ==> x������ �̵�
        this.speed *= 0.98f;                        // ����

    }


}

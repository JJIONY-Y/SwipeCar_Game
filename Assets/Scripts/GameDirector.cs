// 거리 표시 텍스트 UI
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    GameObject car;
    GameObject flag;
    GameObject distance;

    public Button Reset_btn;

    public Text Player1;
    public Text Player2;
    public Text Player3;

    public Text ResultText;
    public Text[] PlayerUI;

    float Score1;
    float Score2;
    float Score3;

    float m_Length;



    // Start is called before the first frame update
    void Start()
    {
        this.car = GameObject.Find("car");              // 유니티 Hierarchy 목록에서 GameObject를 찾아옴
        this.flag = GameObject.Find("flag");            // public으로 명시해주면 유니티에서 연결해서 사용할 수 있음
        this.distance = GameObject.Find("Distance");

        if (distance != null)
            ResultText = this.distance.GetComponent<Text>();

        if (Reset_btn != null)
            Reset_btn.onClick.AddListener(Reset);

    }

    // Update is called once per frame
    void Update()
    {
        CarController Player = GameObject.Find("car").GetComponent<CarController>();

        
        
        float length = this.flag.transform.position.x - this.car.transform.position.x;
        length = Mathf.Abs(length);     // 길이 절댓값 구하기

        ResultText.text = "목표 지점까지 " + length.ToString("F2") + "m";     // F2 : 소수점 둘째자리까지 표시
        m_Length = length;

        //if (length < 0f)
        //{
        //    length *= -1.0f;
        //}


        if (Player.speed <= 0.0005f && Player.speed != 0)
        {
            if (Player.num == 1)
            {
                Player.speed = 0;
                Score1 = length;
                Player1.text = "Player 1 : " + length.ToString("F2") + "m";

            }
            else if (Player.num == 2)
            {
                Player.speed = 0;
                Score2 = length;
                Player2.text = "Player 2 : " + length.ToString("F2") + "m";
            }
            else if (Player.num == 3)
            {
                Player.speed = 0;
                Score3 = length;
                Player3.text = "Player 3 : " + length.ToString("F2") + "m";
                Rank();
                Reset_btn.gameObject.SetActive(true);
                
            }

        }
        else if (Player.speed == 0)
            CarReset();
    }

    // 각 플레이어가 도착하면 기록을 화면에 표시하고 저장해 놓기 위한 함수
    public void RecordLength()
    {
        CarController Player = GameObject.Find("car").GetComponent<CarController>();
        if (Player.num < PlayerUI.Length)
        {
            PlayerUI[Player.num].text =
                "Player " + (Player.num + 1).ToString() + " : " + m_Length.ToString("F2") + "m";
        }

        // 자동차가 멈추는 순간마다 게임 종료 조건 판단하는 코드
        if (Player.num >= 3)
        {
            //s_State = GameState.GameEnd;          // enum 으로 상태 선언해서 사용

            // 순위 판정

            // 리플레이 버튼 활성화
            Reset_btn.gameObject.SetActive(true);
        }
    }

    void CarReset()
    {
        car.transform.position = new Vector3(-7f, -3.7f, 0);
    }


    void Rank()
    {
        //Debug.Log(Score1 + ", " + Score2 + ", " + Score3);

        if (Score1 == Score2 && Score2 == Score3)
        {
            Player1.text += " 1 등";
            Player2.text += " 2 등";
            Player3.text += " 3 등";
        }
        else if (Score1 >= Score2 && Score1 >= Score3)
        {
            //Debug.Log("1이 젤 멈");
            if (Score2 == Score3)
            {
                Player1.text += " 3 등";
                Player2.text += " 1 등";
                Player3.text += " 2 등";
            }
            else if (Score2 > Score3)
            {
                Player1.text += " 3 등";
                Player2.text += " 2 등";
                Player3.text += " 1 등";
            }
            else   // (Score3 > Score2)
            {
                Player1.text += " 3 등";
                Player2.text += " 1 등";
                Player3.text += " 2 등";
            }
        }
        else if (Score2 >= Score1 && Score2 >= Score3)
        {
            //Debug.Log("2 젤 멈");
            if (Score1 == Score3)
            {
                Player1.text += " 1 등";
                Player2.text += " 3 등";
                Player3.text += " 2 등";
            }
            else if (Score1 > Score3)
            {
                Player1.text += " 2 등";
                Player2.text += " 3 등";
                Player3.text += " 1 등";
            }
            else
            {
                Player1.text += " 1 등";
                Player2.text += " 3 등";
                Player3.text += " 2 등";
            }
        }
        else
        {
            //Debug.Log("3 젤 멈");
            if (Score1 == Score2)
            {
                Player1.text += " 1 등";
                Player2.text += " 2 등";
                Player3.text += " 3 등";
            }
            else if (Score1 > Score2)
            {
                Player1.text += " 2 등";
                Player2.text += " 1 등";
                Player3.text += " 3 등";
            }
            else
            {
                Player1.text += " 1 등";
                Player2.text += " 2 등";
                Player3.text += " 3 등";
            }
        }





    }

    void Reset()
    {
        SceneManager.LoadScene("GameScene");
    }
}

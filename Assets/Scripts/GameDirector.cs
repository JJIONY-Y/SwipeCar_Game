// �Ÿ� ǥ�� �ؽ�Ʈ UI
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
        this.car = GameObject.Find("car");              // ����Ƽ Hierarchy ��Ͽ��� GameObject�� ã�ƿ�
        this.flag = GameObject.Find("flag");            // public���� ������ָ� ����Ƽ���� �����ؼ� ����� �� ����
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
        length = Mathf.Abs(length);     // ���� ���� ���ϱ�

        ResultText.text = "��ǥ �������� " + length.ToString("F2") + "m";     // F2 : �Ҽ��� ��°�ڸ����� ǥ��
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

    // �� �÷��̾ �����ϸ� ����� ȭ�鿡 ǥ���ϰ� ������ ���� ���� �Լ�
    public void RecordLength()
    {
        CarController Player = GameObject.Find("car").GetComponent<CarController>();
        if (Player.num < PlayerUI.Length)
        {
            PlayerUI[Player.num].text =
                "Player " + (Player.num + 1).ToString() + " : " + m_Length.ToString("F2") + "m";
        }

        // �ڵ����� ���ߴ� �������� ���� ���� ���� �Ǵ��ϴ� �ڵ�
        if (Player.num >= 3)
        {
            //s_State = GameState.GameEnd;          // enum ���� ���� �����ؼ� ���

            // ���� ����

            // ���÷��� ��ư Ȱ��ȭ
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
            Player1.text += " 1 ��";
            Player2.text += " 2 ��";
            Player3.text += " 3 ��";
        }
        else if (Score1 >= Score2 && Score1 >= Score3)
        {
            //Debug.Log("1�� �� ��");
            if (Score2 == Score3)
            {
                Player1.text += " 3 ��";
                Player2.text += " 1 ��";
                Player3.text += " 2 ��";
            }
            else if (Score2 > Score3)
            {
                Player1.text += " 3 ��";
                Player2.text += " 2 ��";
                Player3.text += " 1 ��";
            }
            else   // (Score3 > Score2)
            {
                Player1.text += " 3 ��";
                Player2.text += " 1 ��";
                Player3.text += " 2 ��";
            }
        }
        else if (Score2 >= Score1 && Score2 >= Score3)
        {
            //Debug.Log("2 �� ��");
            if (Score1 == Score3)
            {
                Player1.text += " 1 ��";
                Player2.text += " 3 ��";
                Player3.text += " 2 ��";
            }
            else if (Score1 > Score3)
            {
                Player1.text += " 2 ��";
                Player2.text += " 3 ��";
                Player3.text += " 1 ��";
            }
            else
            {
                Player1.text += " 1 ��";
                Player2.text += " 3 ��";
                Player3.text += " 2 ��";
            }
        }
        else
        {
            //Debug.Log("3 �� ��");
            if (Score1 == Score2)
            {
                Player1.text += " 1 ��";
                Player2.text += " 2 ��";
                Player3.text += " 3 ��";
            }
            else if (Score1 > Score2)
            {
                Player1.text += " 2 ��";
                Player2.text += " 1 ��";
                Player3.text += " 3 ��";
            }
            else
            {
                Player1.text += " 1 ��";
                Player2.text += " 2 ��";
                Player3.text += " 3 ��";
            }
        }





    }

    void Reset()
    {
        SceneManager.LoadScene("GameScene");
    }
}

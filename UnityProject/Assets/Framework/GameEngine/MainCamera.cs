using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

// ���� ī�޶�
// �䱸 ���Ӽ� | MapScanArea | Doctor

public class MainCamera : MonoBehaviour
{
    //������ ��������Ʈ �ҷ�����
    public Transform Doctor;
    public GameObject MapScanArea;
    private MapScanArea MapScanCode;

    //���� �����ǰ� �ִ� ���� �̸��� ������ ���� (���߿�)
    public string dev_Mapname = "";

    // �÷��̾�� ī�޶� ������ �Ÿ�
    public Vector2 offset; 

    //ī�޶� �÷��̾��� �̵��� ������ ������ ����
    public bool TracePlayer = true;

    //ī�޶� ���� �÷��̾��� ��ġ �ӽð�
    private Vector3 target;

    // ī�޶� �ε巴�� �÷��̾ ���󰥶� ���� ���͹�
    public float smoothTime = 0.1f; 

    //ī�޶� �̵��� ���� ���ӵ� �ӽð�
    private Vector3 velocity = Vector3.zero;

    //�� ���� �����¿� �������� ������ ��ųʸ�
    private Dictionary<string, List<float>> MapCameraEdge = new Dictionary<string, List<float>>();


    private void Start()
    {
        Scene Scene = SceneManager.GetActiveScene();
        MapScanCode = MapScanArea.GetComponent<MapScanArea>();

        target = new Vector3(Doctor.position.x, Doctor.position.y, 1);

        
        //�� �̵��� ������ �����ͺ��̽�
        //Y�Ʒ�, Y��, X��, X��
        MapCameraEdge.Add("T1Map1", new List<float> { -35.99f, -15.19f, 13.0f, 25.58f });

        MapCameraEdge.Add("B2Map1", new List<float> { -40f, -40f, 10f, 50f });
        MapCameraEdge.Add("B2Map2", new List<float> { -37f, -32f, 0f, 500f });
        MapCameraEdge.Add("B2Map3", new List<float> { -37f, -15f, 30f, 220f });

        MapCameraEdge.Add("B1Map1", new List<float> { -226f, 0f, 10f, 30f });
        MapCameraEdge.Add("B1Map2", new List<float> { -160f, -148f, 15f, 300f });
        MapCameraEdge.Add("B1Map3", new List<float> { -230f, -153f, 260f, 310f });
        MapCameraEdge.Add("B1Map4", new List<float> { -230f, -70f, 290f, 466f });
        MapCameraEdge.Add("B1Map5", new List<float> { -90f, -73.7f, 200f, 410f });
        MapCameraEdge.Add("B1Map6", new List<float> { -72f, -30f, 190f, 230f });
        MapCameraEdge.Add("B1Map7", new List<float> { -34f, -17f, 210f, 460f });

        MapCameraEdge.Add("G1Map1", new List<float> { -76f, -70f, 11f, 70f });
        MapCameraEdge.Add("G1Map2", new List<float> { -85f, -41f, 40f, 70f });
        MapCameraEdge.Add("G1Map3", new List<float> { -44f, -42.5f, 15f, 80f });
        MapCameraEdge.Add("G1Map4", new List<float> { -44f, -12f, 15f, 40f });
        MapCameraEdge.Add("G1Map5", new List<float> { -11f, -5f, 15f, 78f });

        MapCameraEdge.Add("G2Map1", new List<float> { -170f, -27f, 10f, 185f });

        MapCameraEdge.Add("Boss", new List<float> { -40f, -10f, -20f, 100f });

        MapCameraEdge.Add("Final", new List<float> { -40f, 40f, -40f, 40f });
    }


    void LateUpdate()
    {
        string key = SceneManager.GetActiveScene().name + MapScanCode.TriggeredMapArea;
        dev_Mapname = key;

        if (MapCameraEdge.TryGetValue(key, out List<float> values))
        {

            if (TracePlayer)
            {
                // ī�޶��� ��ġ�� �÷��̾� ��ġ�� offset�� ���ؼ� ����
                if (Doctor.position.x >= values[2] && Doctor.position.x <= values[3])
                {
                    target.x = Doctor.position.x + offset.x;
                }

                if (Doctor.position.y >= values[0] && Doctor.position.y <= values[1])
                {
                    target.y = Doctor.position.y + offset.y;
                }

                target.z = transform.position.z;

                transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, smoothTime);
            }
        }
        else
        {
            Debug.Log("�� ��ȣ �޾ƿ��� ����!");
        }
    }
}


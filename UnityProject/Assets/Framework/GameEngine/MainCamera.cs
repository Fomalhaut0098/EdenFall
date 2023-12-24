using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

// 메인 카메라
// 요구 종속성 | MapScanArea | Doctor

public class MainCamera : MonoBehaviour
{
    //종속적 스프라이트 불러오기
    public Transform Doctor;
    public GameObject MapScanArea;
    private MapScanArea MapScanCode;

    //현재 감지되고 있는 맵의 이름을 보여줄 변수 (개발용)
    public string dev_Mapname = "";

    // 플레이어와 카메라 사이의 거리
    public Vector2 offset; 

    //카메라가 플레이어의 이동을 따라갈지 지정할 변수
    public bool TracePlayer = true;

    //카메라가 따라갈 플레이어의 위치 임시값
    private Vector3 target;

    // 카메라가 부드럽게 플레이어를 따라갈때 생길 인터벌
    public float smoothTime = 0.1f; 

    //카메라 이동에 쓰일 가속도 임시값
    private Vector3 velocity = Vector3.zero;

    //각 맵의 상하좌우 오프셋을 저장한 딕셔너리
    private Dictionary<string, List<float>> MapCameraEdge = new Dictionary<string, List<float>>();


    private void Start()
    {
        Scene Scene = SceneManager.GetActiveScene();
        MapScanCode = MapScanArea.GetComponent<MapScanArea>();

        target = new Vector3(Doctor.position.x, Doctor.position.y, 1);

        
        //맵 이동시 오프셋 데이터베이스
        //Y아래, Y위, X왼, X오
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
                // 카메라의 위치를 플레이어 위치에 offset을 더해서 갱신
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
            Debug.Log("맵 번호 받아오기 실패!");
        }
    }
}


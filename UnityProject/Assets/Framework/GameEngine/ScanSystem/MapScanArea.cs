using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Doctor가 서있는 맵의 MapArea를 감지하는 Doctor 하위 스프라이트
// 요구 종속성 | 없음

public class MapScanArea : MonoBehaviour
{
    //감지된 맵의 MapArea가 저장되는 변수
    public string TriggeredMapArea = "";

    // 새로운 맵 구역과 닿았을때 실행
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 새로운 맵 구역과 닿은 경우
        if (collision.gameObject.layer == LayerMask.NameToLayer("MapArea"))
        {
            Debug.Log($"맵 구역 이동 감지됨: {collision.gameObject.name}");
            TriggeredMapArea = collision.gameObject.name;
        }
    }
}

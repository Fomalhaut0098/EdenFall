using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Doctor�� ���ִ� ���� MapArea�� �����ϴ� Doctor ���� ��������Ʈ
// �䱸 ���Ӽ� | ����

public class MapScanArea : MonoBehaviour
{
    //������ ���� MapArea�� ����Ǵ� ����
    public string TriggeredMapArea = "";

    // ���ο� �� ������ ������� ����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���ο� �� ������ ���� ���
        if (collision.gameObject.layer == LayerMask.NameToLayer("MapArea"))
        {
            Debug.Log($"�� ���� �̵� ������: {collision.gameObject.name}");
            TriggeredMapArea = collision.gameObject.name;
        }
    }
}

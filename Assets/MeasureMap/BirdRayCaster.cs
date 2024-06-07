using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdRayCaster : MonoBehaviour
{
    // Start is called before the first frame update
    public float rayLength = 10f;  
    public LayerMask layerMask;   


    void Update()
    {
        // 세 방향으로 레이캐스트: 좌측, 위쪽(천장), 뒤쪽
        CastRay(Vector3.left, "Left Wall");
        CastRay(Vector3.up, "Ceiling");
        CastRay(Vector3.back, "Back Wall");
        
    }

    void CastRay(Vector3 direction, string wallName)
    {
        RaycastHit hit;
        


        // 레이캐스트 발사
        
        if (Physics.Raycast(transform.position, direction, out hit, rayLength, layerMask))
        {
            var cube = hit.collider.GetComponent<Wall>();
            if (cube != null)
            {
                cube.SetTouched(true);  // 큐브의 bool 값을 true로 설정
            }
        }
    }
}

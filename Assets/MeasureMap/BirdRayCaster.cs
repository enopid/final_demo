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
        // �� �������� ����ĳ��Ʈ: ����, ����(õ��), ����
        CastRay(Vector3.left, "Left Wall");
        CastRay(Vector3.up, "Ceiling");
        CastRay(Vector3.back, "Back Wall");
        
    }

    void CastRay(Vector3 direction, string wallName)
    {
        RaycastHit hit;
        


        // ����ĳ��Ʈ �߻�
        
        if (Physics.Raycast(transform.position, direction, out hit, rayLength, layerMask))
        {
            var cube = hit.collider.GetComponent<Wall>();
            if (cube != null)
            {
                cube.SetTouched(true);  // ť���� bool ���� true�� ����
            }
        }
    }
}

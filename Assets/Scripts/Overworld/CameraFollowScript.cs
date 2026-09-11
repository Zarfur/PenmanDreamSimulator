using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{

    [SerializeField] Vector3 offset;
    public GameObject target;
    

    void LateUpdate()
    {
        if(target != null)
            transform.position = target.transform.position + offset;
    }
}

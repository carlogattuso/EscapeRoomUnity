using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject followTarget;
    private Vector3 targetPosition;
    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.targetPosition = new Vector3(this.followTarget.transform.position.x, this.followTarget.transform.position.y, this.transform.position.z);
        this.transform.position = Vector3.Lerp(this.transform.position, this.targetPosition, moveSpeed*Time.deltaTime);
    }
}

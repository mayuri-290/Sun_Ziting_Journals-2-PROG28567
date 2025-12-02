using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform targetHolder;
    public float followIntensity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       transform.position = Vector3.Lerp(transform.position, targetHolder.position, followIntensity * Time.deltaTime); 
    }
}

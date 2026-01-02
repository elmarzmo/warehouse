using UnityEngine;

public class ForkliftCenterOfMass : MonoBehaviour

{

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0f, -0.4f, -0.2f); 

    }

    // Update is called once per frame
   /* void Update()
    {
        
    }
   */
}

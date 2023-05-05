using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObj : MonoBehaviour
{
    [SerializeField] private Rigidbody objectRigibody;

    [SerializeField] private Transform objectGrabPointTransform;

    [SerializeField] public GameObject tempParent;

    public float throwOffset;

    public float throwForce;

    public float lerpSpeed = 10f;

  

    private void Awake()
    {
        objectRigibody = GetComponent<Rigidbody>();
    }

    public void Grab(Transform ojectGrabPointTransform)
    {
        this.objectGrabPointTransform = ojectGrabPointTransform;
        objectRigibody.useGravity = false;
    }

    public void Throw()
    {
        this.objectGrabPointTransform = null;
        objectRigibody.useGravity = true;
      // objectGrabPointTransform.GetComponent<Rigidbody>().detectCollisions = true;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
        Vector3 throwDir = (mousePos - transform.position).normalized;
        GetComponent<Rigidbody>().AddForce(throwDir * throwForce, ForceMode.VelocityChange);
        

    }
    private void Update()
    {
       // objectGrabPointTransform.GetComponent<Rigidbody>().velocity = Vector3.zero;
      //  objectGrabPointTransform.GetComponent <Rigidbody>().angularVelocity = Vector3.zero;
      //  objectGrabPointTransform.transform.SetParent(tempParent.transform);
        
    }
    private void FixedUpdate()
    {
        if (objectGrabPointTransform != null)
        {

            Vector3 newPosition = Vector3.Lerp(transform.position, objectGrabPointTransform.position, Time.deltaTime * lerpSpeed);
            objectRigibody.MovePosition(newPosition);
        }
    }
}

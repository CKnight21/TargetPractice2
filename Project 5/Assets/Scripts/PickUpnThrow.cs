using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpnThrow : MonoBehaviour
{
    [SerializeField] public Transform playerCameraTransform;
    [SerializeField] public Transform objectGrabPointTransform;
    [SerializeField] public LayerMask pickUplayerMask;

    private PickUpObj objectGrabbable;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (objectGrabbable == null)
            {
                float pickUpDistance = 2f;
                if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickUpDistance))
                {
                    if (raycastHit.transform.TryGetComponent(out objectGrabbable))
                    {
                        objectGrabbable.Grab(objectGrabPointTransform);
                    }
                }
            }
            else
            {
                objectGrabbable.Throw();
                objectGrabbable = null;
            }
        }
    }
}

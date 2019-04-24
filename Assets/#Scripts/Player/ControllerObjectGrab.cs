using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerObjectGrab : MonoBehaviour
{
    private SteamVR_TrackedObject trackedObj;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }
    
    private GameObject collidingObject;
   
    private GameObject objectInHand;

    private GameObject door;



    private bool openingDoor = false;

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    private void SetCollidingObject(Collider col)
    {
        if (collidingObject || !col.GetComponent<Rigidbody>())
        {
            return;
        }
      
        collidingObject = col.gameObject;
    }

    // Update is called once per frame
    void Update()
    {

        if (Controller.GetHairTriggerDown())
        {
            if (collidingObject)
            {
                GrabObject();
            }
            if (openingDoor)
                FindHinge();
        }

       
        if (Controller.GetHairTriggerUp())
        {
            if (objectInHand)
            {
                ReleaseObject();
            }
            if (openingDoor)
                ReleaseDoor();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PickUp"))
            SetCollidingObject(other);
        if (other.CompareTag("Door"))
        {
            openingDoor = true;
            door = other.gameObject;
        }
            
    }

    public void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("PickUp"))
            SetCollidingObject(other);
        if (other.CompareTag("Door"))
        {
            openingDoor = true;
            door = other.gameObject;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        openingDoor = false;
    }

    public void OnTriggerExit(Collider other)
    {
        if (!collidingObject)
        {
            return;
        }
        if (other.CompareTag("Door"))
        {
            openingDoor = false;

        }
        collidingObject = null;
    }

    private void GrabObject()
    {
        
        objectInHand = collidingObject;
        collidingObject = null;
        
        var joint = AddFixedJoint();
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
    }

    
    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }

    private void ReleaseObject()
    {

        if (GetComponent<FixedJoint>())
        {

            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());

            objectInHand.GetComponent<Rigidbody>().velocity = Controller.velocity;
            objectInHand.GetComponent<Rigidbody>().angularVelocity = Controller.angularVelocity;
        }
        objectInHand = null;
    }
    private void FindHinge()
    {
        door = door.transform.parent.transform.parent.gameObject;
        door.GetComponent<DoorRotator>().target = this.transform;
        door.GetComponent<DoorRotator>().Openingdoor = true; 
    }

    private void ReleaseDoor()
    {
        door.GetComponent<DoorRotator>().Openingdoor = false;
        door = null;
    }
}

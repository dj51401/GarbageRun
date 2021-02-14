using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public float walkSpeed;
    public float grabHeight;
    public float grabDistance;
    public float throwForce;

    public float lookSmoothSpeed = .125f;

    public Transform holder;

    float moveSpeed;

    Vector3 input;
    [SerializeField] bool canGrab = false;
    Rigidbody rb;
    Rigidbody grabbedItem;
    float slowSpeed;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = walkSpeed;
        canGrab = false;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Debug.DrawRay(transform.position + new Vector3(0, grabHeight, 0), transform.forward, Color.white);
        if(CheckGrabable() != null && grabbedItem == null)
        {
            canGrab = true;
        }
        else
        {
            canGrab = false;
        }
            
        if(grabbedItem != null) {
            moveSpeed = walkSpeed * slowSpeed;
            HoldItem(grabbedItem.gameObject); 
        }
        else
        {
            moveSpeed = walkSpeed;
        }
    }

    GameObject CheckGrabable()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + new Vector3(0, grabHeight, 0), transform.forward, out hit, grabDistance))
        {
            //does hit
            if (hit.collider.tag == "Grabable")
            {
                return hit.collider.gameObject;
            }
            else
            {
                return null;
            }
        }
        else
        {
            //doesnt hit
            return null;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection += input;

        rb.AddForce(moveDirection * moveSpeed);

        if(input == Vector3.zero) { return; }
        Quaternion lookDirection = Quaternion.LookRotation(input);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookDirection, lookSmoothSpeed);
    }

    void HoldItem(GameObject go)
    {
        slowSpeed = go.GetComponent<TrashScript>().so.slowDownSpeed;
        go.transform.position = holder.position;
        go.transform.rotation = transform.localRotation;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        input = new Vector3(context.ReadValue<Vector2>().x, 0, context.ReadValue<Vector2>().y);
    }

    public void OnGrab(InputAction.CallbackContext context)
    {
        if (context.performed && canGrab == true)
        {
            if (CheckGrabable() != null)
            grabbedItem = CheckGrabable().GetComponent<Rigidbody>();
        }
        else if(context.performed && canGrab == false)
        {
            if(grabbedItem == null) { return; }
            grabbedItem.transform.parent = null;
            grabbedItem = null;
        }
    }

    public void OnThrow(InputAction.CallbackContext context)
    {
        if(context.performed && grabbedItem != null)
        {
            grabbedItem.transform.parent = null;
            grabbedItem.AddForce((transform.forward + transform.up) * throwForce, ForceMode.Impulse);
            grabbedItem = null;
        }
        else
        {
            return;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.EventSystems;
using TMPro;



public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    float moveSpeedIns = 0.1f;

    public float moveSpeed {get{return moveSpeedIns;} set{moveSpeedIns = value;}}

    [SerializeField]
    float rotateSpeedIns = 0.01f;

    public float rotateSpeed {get{return rotateSpeedIns;} set{rotateSpeedIns = value;}}

    [SerializeField,Range(0.0f,1000.0f)]
    float jumpPowerIns = 500.0f;

    public float jumpPower {get{return jumpPowerIns;} set{jumpPowerIns = value;}}

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Rigidbody rd;

        rd = GetComponent<Rigidbody>();

        MoveRigitBody(rd);
        MoveTransform(rd);
    }

    void MoveRigitBody(Rigidbody rd)
    {

        if (rd == null) return;

        rd.MoveRotation(InputRotation());

        Vector3 move = InputMove();

        rd.MovePosition(move);

        rd.useGravity = true;

        var collider = GetComponent<Collider>();

    }

    void MoveTransform(Rigidbody rd)
    {

        if (rd != null) return;

        transform.rotation = InputRotation();

        Vector3 move = InputMove();

        transform.position = move;
    }

    Quaternion InputRotation()
    {
        Vector2 stickR = Vector2.zero;

        if(Input.GetKey(KeyCode.UpArrow))
        {
            //stickR += Vector2.up;
        }

        if (Input.GetKey(KeyCode.A))
        {
            stickR += Vector2.left;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            //stickR += Vector2.down;
        }

        if (Input.GetKey(KeyCode.D))
        {
            stickR += Vector2.right;
        }

        return (Quaternion.Euler(0.0f, stickR.x * rotateSpeedIns, 0.0f) * transform.rotation);
    }

    Vector3 InputMove()
    {
        Vector2 stickL = Vector2.zero;


        if(Input.GetKey(KeyCode.W))
        {
            stickL += Vector2.up;
        }

        if (Input.GetKey(KeyCode.A))
        {
            //stickL += Vector2.left;
        }

        if (Input.GetKey(KeyCode.S))
        {
            stickL += Vector2.down;
        }

        if (Input.GetKey(KeyCode.D))
        {
            //stickL += Vector2.right;
        }

        Vector3 frontBackMove = transform.rotation * Vector3.forward;
        Vector3 leftRightMove = transform.rotation * Vector3.right;

        frontBackMove.y = leftRightMove.y = 0.0f;
        frontBackMove.Normalize();
        leftRightMove.Normalize();

        frontBackMove *= moveSpeedIns * stickL.y;
        leftRightMove *= moveSpeedIns * stickL.x;

        return frontBackMove + leftRightMove + transform.position;

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class samplePlayerMove : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;

    Rigidbody2D rigid;
    
    
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update(){
        inputVec.x = Input.GetAxisRaw("Horizontal");
    }

    void FixedUpdate()
    {
        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }

}

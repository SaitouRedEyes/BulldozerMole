using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinersC : MonoBehaviour
{
    public float speed = 3;
    public Transform front;

    private new Rigidbody2D rigidbody;
    private bool LookingRight;
    public float TurnCD;
    
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        TurnCD = 0.3F;
    }

    
    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector2 direcao = new Vector2(speed, rigidbody.velocity.y);
        rigidbody.velocity = direcao;
        ControlaDirecao();
    }

    private void ControlaDirecao()
    {
        RaycastHit2D paredeInfo = Physics2D.Raycast(front.position,
                                                    Vector2.right,
                                                    0.8f,
                                                    LayerMask.GetMask("Enemy"));

        
        if(paredeInfo == true)
        {
            TurnCD -= Time.deltaTime;
            if (TurnCD <= 0){
            if(LookingRight == false)
            {
                speed *= -1;
                transform.localRotation = Quaternion.Euler(0, 180, 0);
                LookingRight = true;
                TurnCD = 0.3F;
            }
            else
            {
                speed *= -1;
                transform.localRotation = Quaternion.Euler(0, 0, 0);
                LookingRight = false;
                TurnCD = 0.3F;
            }
            }
        }
        
        
    }

}
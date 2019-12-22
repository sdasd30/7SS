﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHeadTowardsPlayer : AIBase
{
    public float ChaseDistance = 10f;
    public float AbandonDistance = 15f;
    public float DelayAfterAttack = 1.0f;

    private Transform m_targetObj;
    private BasicMovement m_playerChar;
    private float m_nextMoveOKTime = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        BasicMovement[] ListMovements = FindObjectsOfType<BasicMovement>();
        foreach ( BasicMovement bm in ListMovements)
        {
            if (bm.IsCurrentPlayer)
            {
                m_targetObj = bm.transform;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override InputPacket AITemplate()
    {
        InputPacket ip = new InputPacket();
        if (m_targetObj != null && Time.timeSinceLevelLoad > m_nextMoveOKTime)
            ip.movementInput = new Vector2((m_targetObj.position.x > transform.position.x) ? 1f : -1f,
                (m_targetObj.position.y > transform.position.y) ? 1f : -1f);
        return ip;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Attackable>() != null && GetComponent<Attackable>().CanAttack(other.gameObject.GetComponent<Attackable>()))
            m_nextMoveOKTime = Time.timeSinceLevelLoad + DelayAfterAttack;
    }


    //void oldMovement()
    //{
    //    if (targetSet)
    //    {
    //        if (targetObj)
    //        {
    //            if (followObj == null)
    //            {
    //                endTarget();
    //                return;
    //            }
    //            targetPoint = followObj.transform.position;
    //        }
    //        moveToPoint(targetPoint);
    //    }
    //}


    //internal void baseMovement(InputPacket ip)
    //{
    //    if (m_physics.onGround) { canDoubleJump = true; }
    //    inputX = 0.0f;
    //    inputY = 0.0f;
    //    if (!autonomy && m_physics.canMove && targetSet)
    //    {
    //        if (targetObj)
    //        {
    //            if (followObj == null)
    //            {
    //                endTarget();
    //                return;
    //            }
    //            targetPoint = followObj.transform.position;
    //        }
    //        moveToPoint(targetPoint);
    //    }
    //    else if (m_physics.canMove && autonomy)
    //    {
    //        inputY = ip.movementInput.y;
    //        inputX = ip.movementInput.x;
    //        if (ip.jump)
    //        {
    //            if (inputY < -0.9f)
    //            {
    //                GetComponent<PhysicsSS>().setDropTime(1.0f);
    //            }
    //            else if (m_physics.collisions.below)
    //            {
    //                m_physics.addSelfForce(m_jumpVector, 0f);
    //                jumpPersist = 0.2f;
    //            }
    //            else if (canDoubleJump)
    //            {
    //                velocity.y = m_jumpVelocity;
    //                m_physics.addSelfForce(m_jumpVector, 0f);
    //                canDoubleJump = false;
    //            }
    //        }
    //    }
    //    //m_physics logic
    //    float targetVelocityX = inputX * MoveSpeed;
    //    velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref m_velocityXSmoothing, (m_physics.collisions.below) ? m_accelerationTimeGrounded : m_accelerationTimeAirborne);
    //    Vector2 input = new Vector2(inputX, inputY);
    //    m_physics.Move(velocity, input);
    //    m_physics.AttemptingMovement = (inputX != 0.0f);
    //}

}

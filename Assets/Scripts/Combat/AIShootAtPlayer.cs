using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIShootAtPlayer : AIBase 
{
    private Transform m_targetObj;
    public float StartShootingRange;
    public float ShootProbability = 0.0f;
    public float ShootCheckInterval = 1.0f;
    public float ShootHoldBurstTime = 0.1f;
    public Vector2 TargetOffset = new Vector2();
    public Vector2 RandomRangeX = new Vector2();
    public Vector2 RandomRangeY = new Vector2();
    private float m_nextShootTime = 0.0f;

    private float m_fireHoldTime;
    void Start()
    {
        BasicMovement[] ListMovements = FindObjectsOfType<BasicMovement>();
        foreach (BasicMovement bm in ListMovements)
        {
            if (bm.IsCurrentPlayer)
            {
                m_targetObj = bm.transform;
            }
        }
    }
    public override InputPacket AITemplate()
    {
        InputPacket ip = new InputPacket();
        float t = Time.timeSinceLevelLoad;
        if (m_targetObj != null)
        {

            float d = Vector2.Distance(m_targetObj.transform.position, transform.position);
            if (d < StartShootingRange)
            {
                Vector2 target = new Vector2(m_targetObj.transform.position.x + TargetOffset.x, m_targetObj.transform.position.y + TargetOffset.y);
                target += new Vector2(Random.Range(RandomRangeX.x, RandomRangeX.y), Random.Range(RandomRangeY.x, RandomRangeY.y));
                target.x = target.x - transform.position.x;
                target.y = target.y - transform.position.y;
                ip.MousePointWorld = target;
                if (t < m_fireHoldTime)
                    ip.fire1 = true;
                if (ShootProbability > 0.0f && t > m_nextShootTime)
                {
                    if (Random.Range(0, 100) < ShootProbability)
                    {
                        m_nextShootTime = t + ShootCheckInterval;
                        ip.fire1Press = true;
                        m_fireHoldTime = t + ShootHoldBurstTime;
                    }
                }
            }
        }
        return ip;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Attackable))]
public class EnemyContactHurt : MonoBehaviour
{
    public float damage = 3f;
    public float coolDownTime = 1f;
    public Vector2 knockback = new Vector2(10f, 0);
    public Vector2 recoilKnockback = new Vector2(5f, 5f);
    public float recoilDamage = 0.0f;
    public float MinimumArmTime = 0.0f;
    private float m_nextRefreshTime;
    private FactionHolder m_factionHolder;
    private float m_toArmTime;
    // Start is called before the first frame update
    void Start()
    {
        m_factionHolder = GetComponent<FactionHolder>();
        m_toArmTime = Time.timeSinceLevelLoad + MinimumArmTime;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (Time.timeSinceLevelLoad < m_nextRefreshTime)
            return;

        if (other.gameObject.GetComponent<Attackable>() != null && Time.timeSinceLevelLoad > m_toArmTime)
        {
            Attackable a = other.gameObject.GetComponent<Attackable>();
            Vector2 kb = new Vector2( knockback.x * ((a.transform.position.x > transform.position.x) ? 1f : -1f),knockback.y);
            Vector2 rkb = new Vector2(recoilKnockback.x * ((a.transform.position.x > transform.position.x) ? 1f : -1f), recoilKnockback.y);
            if (m_factionHolder.CanAttack(a))
            {
                a.TakeDamage(damage);
                a.TakeKnockback(kb);
                GetComponent<Attackable>().TakeDamage(recoilDamage);
                GetComponent<Attackable>().TakeKnockback(rkb);
            }
            m_nextRefreshTime = Time.timeSinceLevelLoad + coolDownTime;
        }
    }
}

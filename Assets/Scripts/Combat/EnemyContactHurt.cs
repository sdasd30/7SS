using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Attackable))]
public class EnemyContactHurt : MonoBehaviour
{
    public float damage = 3f;
    public float coolDownTime = 1f;
    public Vector2 knockback = new Vector2(10f, 0);

    private float m_nextRefreshTime;
    private Attackable m_parentAttackable;
    // Start is called before the first frame update
    void Start()
    {
        m_parentAttackable = GetComponent<Attackable>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (Time.timeSinceLevelLoad < m_nextRefreshTime)
            return;

        if (other.gameObject.GetComponent<Attackable>() != null)
        {
            Attackable a = other.gameObject.GetComponent<Attackable>();
            Vector2 kb = new Vector2( knockback.x * ((a.transform.position.x > transform.position.x) ? 1f : -1f),knockback.y);
            if (a.anarchy)
            {
                a.TakeDamage(damage);
                a.TakeKnockback(kb);
            }
            else if (m_parentAttackable.allied && !a.allied)
            {
                a.TakeDamage(damage);
                a.TakeKnockback(kb);
            }
            else if (!m_parentAttackable.allied && a.allied)
            {
                a.TakeDamage(damage);
                a.TakeKnockback(kb);
            }
            m_nextRefreshTime = Time.timeSinceLevelLoad + coolDownTime;
        }
    }
}

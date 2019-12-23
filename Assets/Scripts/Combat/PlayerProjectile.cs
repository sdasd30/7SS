using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody2D))]
public class PlayerProjectile : MonoBehaviour {
    WeaponStats firedFrom;
	Rigidbody2D m_body;
	public bool isAllied;
	public bool anarchy;
	private bool speed;
	public bool collides = true;
	public bool piercing = false;

	public void SetAngle (float rotation) {
		m_body.transform.rotation = Quaternion.Euler (new Vector3(0f,0f,rotation));
	}

	void Awake () {
		m_body = GetComponent<Rigidbody2D> ();
	}

	public void SetWeapon(WeaponStats sent){
		firedFrom = sent;
	}

	public float getDamage(){
		return firedFrom.damage;
	}

	// Update is called once per frame
	void Update () {
		m_body.transform.Translate (new Vector2(0f,firedFrom.speed * Time.deltaTime)); //new Vector2 (Mathf.Cos(angle) * speed * Time.deltaTime,Mathf.Sin(angle) * speed * Time.deltaTime));
	}
    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Entered A Collider");
        //if (other.gameObject.CompareTag("Collider"))
        //{
            Debug.Log("Collided With Terrain");
            Destroy(gameObject);
        //}
    }
    void OnTriggerEnter2D(Collider2D other) {
 
        if (other.gameObject.GetComponent<Attackable> () != null) {
            Debug.Log("Collided With Enemy");
            Attackable a = other.gameObject.GetComponent<Attackable> ();
			if (a.anarchy) {
                DoHit(a);
			} else if (isAllied && !a.allied) {
                DoHit(a);
            } else if (!isAllied && a.allied) {
                DoHit(a);
            }
		}
	}

    private void DoHit(Attackable a)
    {
        a.TakeDamage(firedFrom.damage);
        float angle = transform.localRotation.z ;
        
        Vector2 kb = new Vector2(Mathf.Cos( angle + Mathf.PI / 2f), Mathf.Sin( angle + Mathf.PI / 2f));
        //Debug.Log(Mathf.Rad2Deg * angle);
        if (Mathf.Abs(Mathf.Rad2Deg * angle) > 45f)
            kb.y *= -1f;
        kb = kb * firedFrom.knockbackMult;
        //Debug.Log(kb);
        a.TakeKnockback(kb);
        if (!piercing)
            Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        foreach (GameObject obj in firedFrom.OnDeathCreate)
        {
            GameObject go = Instantiate(obj, transform.position, transform.rotation);
        }
    }
}
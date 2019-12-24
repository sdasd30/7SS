using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooter : OffensiveTemplate
{
    WeaponStats Weapon;
    public Vector2 Offset;
    bool firing;
    float coolDown = 0;

    void Start()
    {
        Weapon = GetComponent<WeaponStats>();
    }
    public override void HandleInput(InputPacket ip)
    {
        if (Weapon.auto)
        {
            if (ip.fire1)
            {
                if (coolDown <= 0)
                {
                    fire(ip.MousePointWorld);
                }
            }
        }
        else
        {
            if (ip.fire1)
            {
                if (coolDown <= 0)
                {
                    fire(ip.MousePointWorld);
                }
            }
        }

        if (coolDown >= 0)
        {
            coolDown -= 1 * Time.deltaTime;
        }
    }

    public void fire(Vector2 targetPoint)
    {
        for (int i = 0; i < Weapon.shots; i++)
        {
            float angle = Mathf.Atan2(targetPoint.y - transform.position.y, targetPoint.x - transform.position.x) * Mathf.Rad2Deg - 90f;
            //float angle = (transform.rotation.eulerAngles.z + 90) * Mathf.Deg2Rad;
            GameObject bullet = GameObject.Instantiate(Weapon.bullet, transform.position + new Vector3(Offset.x * Mathf.Cos(angle), Offset.y * Mathf.Sin(angle), +.5f), Quaternion.identity);

            Debug.Log("from: " + transform.position + " to: " + targetPoint + " ang: " + angle);
            bullet.GetComponent<PlayerProjectile>().SetAngle(angle + Random.Range(-Weapon.spread, Weapon.spread));
            bullet.GetComponent<PlayerProjectile>().SetWeapon(Weapon);
            GetComponent<FactionHolder>().SetFaction(bullet);
            Destroy(bullet, Weapon.duration);
        }
        coolDown = Weapon.firerate / 1000;
    }

}

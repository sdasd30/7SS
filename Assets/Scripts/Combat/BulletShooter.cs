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
            float angle = Vector2.Angle(transform.position, targetPoint) * Mathf.Deg2Rad;
            //float angle = (transform.rotation.eulerAngles.z + 90) * Mathf.Deg2Rad;
            GameObject bullet = GameObject.Instantiate(Weapon.bullet, transform.position + new Vector3(Offset.x * Mathf.Cos(angle), Offset.y * Mathf.Sin(angle), +.5f), Quaternion.identity);
            float baseAngle = Vector2.Angle(transform.position, targetPoint);
            bullet.GetComponent<PlayerProjectile>().SetAngle(baseAngle + Random.Range(-Weapon.spread, Weapon.spread) + 90);
            bullet.GetComponent<PlayerProjectile>().SetWeapon(Weapon);
            bullet.GetComponent<PlayerProjectile>().isAllied = GetComponent<Attackable>().allied;
            Destroy(bullet, Weapon.duration);
        }
        coolDown = Weapon.firerate / 1000;
    }

}

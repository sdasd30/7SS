using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpUI : MonoBehaviour
{
    public GameObject HealthIcon,SlowIcon,SpeedIcon,JumpIcon;
    private bool healthActive, slowActive, speedActive, jumpActive;

    public void CreateHealth()
    {
        if (!healthActive)
        {
            healthActive = true;
            GameObject tmp = Instantiate(HealthIcon, new Vector3(0f, 0f, 0f), Quaternion.identity);
            tmp.gameObject.transform.SetParent(this.transform);
        }
    }

    public void DestroyHealth()
    {
        GameObject tmp = this.transform.Find(HealthIcon.name + "(Clone)" ).gameObject;
        Destroy(tmp.gameObject);
        healthActive = false;
    }

    public void CreateSpeed()
    {
        if (!speedActive)
        {
            speedActive = true;
            GameObject tmp = Instantiate(SpeedIcon, new Vector3(0f, 0f, 0f), Quaternion.identity);
            tmp.gameObject.transform.SetParent(this.transform);
        }
    }

    public void DestroySpeed()
    {
        GameObject tmp = this.transform.Find(SpeedIcon.name + "(Clone)").gameObject;
        Destroy(tmp.gameObject);
        speedActive = false;
    }

    public void CreateSlow()
    {
        if (!slowActive)
        {
            slowActive = true;
            GameObject tmp = Instantiate(SlowIcon, new Vector3(0f, 0f, 0f), Quaternion.identity);
            tmp.gameObject.transform.SetParent(this.transform);
        }
    }

    public void DestroySlow()
    {
        GameObject tmp = this.transform.Find(SlowIcon.name + "(Clone)").gameObject;
        Destroy(tmp.gameObject);
        slowActive = false;
    }

    public void CreateJump()
    {
        if (!jumpActive)
        {
            jumpActive = true;
            GameObject tmp = Instantiate(JumpIcon, new Vector3(0f, 0f, 0f), Quaternion.identity);
            tmp.gameObject.transform.SetParent(this.transform);
        }
    }

    public void DestroyJump()
    {
        GameObject tmp = this.transform.Find(JumpIcon.name + "(Clone)").gameObject;
        Destroy(tmp.gameObject);
        jumpActive = false;
    }

}

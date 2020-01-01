using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour {
	public List<GameObject> weapons;
	public float weaponSwitchDelay;
	[HideInInspector] public GameObject nextWeapon;
    [HideInInspector] public GameObject currWeapon;
	public float cooldown;

    const float DELAY = 7;

    void Start(){
        begin();
        //fillLoadout();
    }

	void Update(){
		cooldown -= Time.deltaTime;
        if (nextWeapon.GetComponent<WeaponStats>().name == transform.GetChild(0).GetComponent<WeaponStats>().name)
            NextWeaponLoad();
        if (cooldown <= 0){
            //Debug.Log("Cooldown Low");
            FillLoadout();
		}
        if (poweredUpFast)
        {
            speedPowerUpCooldown -= Time.deltaTime;
            if (speedPowerUpCooldown <= 0)
            {
                poweredUpFast = false;
                weaponSwitchDelay = DELAY;
                FindObjectOfType<PowerUpUI>().DestroySpeed();
            }
        }

	}

    public void begin()
    {
        cooldown = weaponSwitchDelay;
        nextWeapon = weapons[Random.Range(0, weapons.Count)];
        if (currWeapon != null)
        {
            GameObject temp = currWeapon;
            GameObject.Destroy(currWeapon.gameObject);
            currWeapon = GameObject.Instantiate(nextWeapon, temp.transform.position, Quaternion.identity);
            currWeapon.transform.parent = transform;
            currWeapon.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        }
        else
        {
            currWeapon = GameObject.Instantiate(nextWeapon, transform.position + new Vector3(.75f, 0, -1f), Quaternion.identity);
            currWeapon.transform.parent = transform;
        }
        NextWeaponLoad();
    }

    private void FillLoadout()
    {
        cooldown = weaponSwitchDelay;
        GameObject temp = currWeapon;
        GameObject.Destroy(currWeapon.gameObject);
        currWeapon = GameObject.Instantiate(nextWeapon, temp.transform.position, Quaternion.identity);
        currWeapon.transform.parent = transform;
        currWeapon.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        NextWeaponLoad();
    }

    private void NextWeaponLoad()
    {
        if (poweredUpSlow)
        {
            FindObjectOfType<PowerUpUI>().DestroySlow();
            poweredUpSlow = false;
        }

        nextWeapon = weapons[Random.Range(0, weapons.Count)];
        while (nextWeapon.GetComponent<WeaponStats>().name == transform.GetChild(0).GetComponent<WeaponStats>().name)
        {
            nextWeapon = weapons[Random.Range(0, weapons.Count)];
        }
        return;
    }

    #region Power Up Functions
    private bool poweredUpSlow;
    private bool poweredUpFast;
    private float speedPowerUpCooldown;

    public void SlowDown(float seconds)
    {
        cooldown = seconds;
        poweredUpSlow = true;
        FindObjectOfType<PowerUpUI>().CreateSlow();
        if (poweredUpFast)
        {
            FindObjectOfType<PowerUpUI>().DestroySpeed();
            poweredUpFast = false;
        }
        
    }

    public void SpeedUp(float seconds, float ncooldown)
    {
        poweredUpFast = true;
        speedPowerUpCooldown = seconds;
        weaponSwitchDelay = ncooldown;
        if (cooldown > weaponSwitchDelay)
            cooldown = weaponSwitchDelay;
        FindObjectOfType<PowerUpUI>().CreateSpeed();
        if (poweredUpSlow)
        {
            FindObjectOfType<PowerUpUI>().DestroySlow();
            poweredUpSlow = false;
        }
    }

    public void GiveSuperWeapon(float seconds)
    {
        if (poweredUpSlow)
        {
            FindObjectOfType<PowerUpUI>().DestroySlow();
            poweredUpSlow = false;
        }
        Object[] loadedObjs = Resources.LoadAll("Weapons", typeof(GameObject));
        List<GameObject> ptWeapons = new List<GameObject>();
        foreach (GameObject weapon in loadedObjs)
        {
            if (weapon.GetComponent<WeaponStats>().Cost == 5) ptWeapons.Add(weapon);
        }
        GameObject temp = currWeapon.gameObject;
        GameObject.Destroy(currWeapon.gameObject);
        GameObject newWeapon = ptWeapons[Random.Range(0, ptWeapons.Count)];
        currWeapon = GameObject.Instantiate(newWeapon, temp.transform.position, Quaternion.identity);
        currWeapon.transform.parent = transform;
        currWeapon.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        cooldown = seconds;

    }

    public void RandomizeWeapons()
    {
        if (poweredUpSlow)
        {
            FindObjectOfType<PowerUpUI>().DestroySlow();
            poweredUpSlow = false;
        }

        for (int i = 0; i < weapons.Count; i++)
        {
            weapons[i] = null;
        }

        Object[] loadedObjs = Resources.LoadAll("Weapons", typeof(GameObject));
        GameObject loadedWeapon;
        for (int i = 0; i < weapons.Count; i++)
        {
            loadedWeapon = (GameObject)loadedObjs[Random.Range(0, loadedObjs.Length)];
            while (weapons.Contains(loadedWeapon))
            {
                loadedWeapon = (GameObject)loadedObjs[Random.Range(0, loadedObjs.Length)];
            }
            weapons[i] = loadedWeapon;
        }
        FillLoadout();
    }

    public float ReturnPowerCooldownSpeed()
    {
        return speedPowerUpCooldown;
    }
    //IEnumerator setCoolDown(float length, float newCool)
    //{
    //    float oldCool = weaponSwitchDelay;
    //    cooldown = newCool;
    //    weaponSwitchDelay = newCool;
    //    if (cooldown > weaponSwitchDelay)
    //        cooldown = weaponSwitchDelay;
    //    yield return new WaitForSeconds(length);
    //    weaponSwitchDelay = oldCool;
    //}

    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBarrier : MonoBehaviour
{
    public float deathBarrier = -9999; //What Y position they will have this trigger
    public float fallPenalty; //Reduce the fallers health by this percent of their current Hp
    public bool important; //True if the object will be returned to return point on falling
    public Vector3 returnPoint = new Vector3 (0, 0, 0); //Where the object will reapear on falling.
    private Attackable hpVar;
    // Start is called before the first frame update
    void Start()
    {
        hpVar = GetComponent<Attackable>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < deathBarrier)
        {
            FallDown();
        }
    }

    public void FallDown()
    {
        if (important)
        {
            hpVar.hp -= hpVar.hp * .25f;
            this.transform.position = returnPoint;
        }

        else Destroy(this.gameObject);
    }
}

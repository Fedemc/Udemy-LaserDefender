using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    public float daño = 100f;


    public float GetDamage()
    {
        return daño;
    }

    public void Hit()
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMovement : MonoBehaviour
{
    float angleToWind;

    Rigidbody boatRB;

    public GameObject windManager;
    public WindManager windManagerScript;

    [Range(1, 50)]
    public float turnSpeed; 
    private float sailSetting = 1f;
    public float SailSetting
    {
        get { return sailSetting; }
        set { sailSetting = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        boatRB = GetComponent<Rigidbody>();
        windManagerScript = windManager.GetComponent<WindManager>();
    }

    // Update is called once per frame
    public void Update()
    {
        angleToWind = (transform.eulerAngles.y - windManager.transform.eulerAngles.y) * (Mathf.PI / 180); // finds the angle between the boat and the wind and converts to radians
        //Debug.Log(1 + Mathf.Cos(angleToWind));

        transform.Translate(Vector3.forward * (sailSetting * windManagerScript.WindSpeed * (1 + (Mathf.Cos(angleToWind))) * Time.deltaTime)); // constantly moves the player boat forwards relative to their angle to the wind
    }
}

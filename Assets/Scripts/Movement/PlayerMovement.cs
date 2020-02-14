using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : BoatMovement // since the player is a boat the player movement is derived from the standard boat movement, but with more inputs.
{
    public GameObject playerGroup;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    new void Update()
    {
        transform.eulerAngles += new Vector3(0f, Input.GetAxisRaw("Horizontal"), 0f) * turnSpeed * Time.deltaTime; // standard A/D rotatation controls 
        base.Update(); // this is just the basic boat movement from the parent class
        playerGroup.transform.position = transform.position; // ensure the player group is always in the same place as the boat
    }
}

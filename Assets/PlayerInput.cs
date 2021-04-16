using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{    
    private EntityMovement entity;

    // Start is called before the first frame update
    void Start()
    {
        entity = GetComponent<EntityMovement>();    
    }

    // Update is called once per frame
    void Update()
    {
        var vertical = Input.GetAxisRaw("Vertical");
        var horizontal = Input.GetAxisRaw("Horizontal");

        //entity.SetMoveDirection(new Vector3(horizontal, vertical, 0));
    }
}

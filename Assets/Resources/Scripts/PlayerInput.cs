using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{    
    private EntityMovement entity;

    private int playerLevel = 0;

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

        // Cannot go Up if not in the water
        if (!entity.InWater())
        {
            vertical = Mathf.Clamp(vertical, -1.0f, 0f);            
        }

        //Debug.Log("Vertical " + vertical);

        entity.SetMoveDirection(new Vector3(horizontal, vertical, 0), true);
    }

    public void LevelUp()
	{
        playerLevel++;
	}

    public void ResetLevel()
	{
        playerLevel = 0;
	}

	private void OnGUI()
	{
        GUI.Label(new Rect(Screen.width - 150, 0, 150, 50), "LEVEL " + playerLevel.ToString());
	}
}

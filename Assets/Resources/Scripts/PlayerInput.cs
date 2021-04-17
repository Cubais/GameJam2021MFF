using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float hookSpeed = 1;
    public float hookDistance = 10;
    public GameObject grapplingHook;
    public Transform grapplingHookParent;

    private int playerLevel = 0;
    private EntityMovement m_entity;
    private GrapplingHook m_lastHook;

    // Start is called before the first frame update
    void Start()
    {
        m_entity = GetComponent<EntityMovement>();    
    }

    // Update is called once per frame
    void Update()
    {
        var vertical = Input.GetAxisRaw("Vertical");
        var horizontal = Input.GetAxisRaw("Horizontal");

        // Cannot go Up if not in the water
        if (!m_entity.InWater())
        {
            vertical = Mathf.Clamp(vertical, -1.0f, 0f);            
        }

        //Debug.Log("Vertical " + vertical);

        m_entity.SetMoveDirection(new Vector3(horizontal, vertical, 0), true);

        // Grappling hook
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 dir3 = mousePosition - transform.position;
            Vector2 dir = new Vector2(dir3.x, dir3.y).normalized;

            foreach (Transform child in grapplingHookParent)
            {
                Destroy(child.gameObject);
            }

            GameObject hook = Instantiate(grapplingHook, transform.position + (Vector3)dir * 0.3f, Quaternion.identity, grapplingHookParent);
            m_lastHook = hook.GetComponent<GrapplingHook>();
            m_lastHook.SetMoveDirection(dir);
        }

        // If a movement key is pressed, delete latched hook
        if (
            Input.GetKey(KeyCode.W) ||
            Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.D) ||
            Input.GetKey(KeyCode.LeftArrow) ||
            Input.GetKey(KeyCode.RightArrow) ||
            Input.GetKey(KeyCode.UpArrow) ||
            Input.GetKey(KeyCode.DownArrow)
        )
        {
            if (m_lastHook != null)
            {
                m_lastHook.DeleteIfLatched();
            }
        }
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

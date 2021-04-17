using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float hookSpeed = 1;
    public float hookDistance = 10;
    public GameObject grapplingHook;
    public Transform grapplingHookParent;

    public int PlayerLevel { get; private set; } = 0;

    private EntityMovement m_entity;
    private GrapplingHook m_lastHook;
    private Animator m_animator;

    private bool m_leftSideOrientation = true;

    // Start is called before the first frame update
    void Start()
    {
        m_entity = GetComponent<EntityMovement>();
        m_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        var vertical = Input.GetAxisRaw("Vertical");
        var horizontal = Input.GetAxisRaw("Horizontal");

        HandleAnimations(horizontal, vertical);

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
        PlayerLevel++;
	}

    public void ResetLevel()
	{
        PlayerLevel = 0;
	}

	private void OnGUI()
	{
        GUI.Label(new Rect(Screen.width - 150, 0, 150, 50), "LEVEL " + PlayerLevel.ToString());
	}

    private void HandleAnimations(float horizontal, float vertical)
	{
        var hor = horizontal;
        if (horizontal == 0f)
		{
            hor = (m_leftSideOrientation) ? -0.1f : 0.1f;
		}
        else
		{
            m_leftSideOrientation = horizontal < 0;
		}

        m_animator.SetFloat("Vertical", vertical);
        m_animator.SetFloat("Horizontal", hor);
    }
}

using UnityEngine;

public class camMouseLook : MonoBehaviour
{
	Vector2 mouseLook;
	Vector2 smoothV;
	public float sensitivity = 5.0f;
	public float smoothing = 2.0f;
    public float maxAngle = 75;
    public float minAngle = -75;
    public float moveSpeed = 10f;
    public float JumpForce = 10;
    public bool isJumping;
    GameObject character;
    public GameObject Camera;

	void OnEnable()
	{
		//character = transform.parent.gameObject;
    }

    void Update()
    {
        Move();
        MouseLook();
    }

    void Move()
    {
        float translation = Input.GetAxis("Vertical") * moveSpeed;
        float straffe = Input.GetAxis("Horizontal") * moveSpeed;
        translation *= Time.deltaTime;
        straffe *= Time.deltaTime;
    }

    void MouseLook()
	{
		var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
		md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
		smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
		smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
		mouseLook += smoothV; 
		transform.rotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        //Camera.transform.rotation = Quaternion.AngleAxis(mouseLook.x, Camera.transform.up);    
		if(mouseLook.y < minAngle)
		{
			mouseLook.y = minAngle;
		}
		if(mouseLook.y > maxAngle)
		{
			mouseLook.y = maxAngle;
		}
	}
}﻿

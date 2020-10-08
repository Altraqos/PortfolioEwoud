using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

	public float moveSpeed = 10f;
	Vector2 mouseLook;
	Vector2 smoothV;
	public float sensitivity = 5.0f;
	public float smoothing = 2.0f;
	public GameObject character;
    public GameObject Camera;
    public Text devText;

	void Start ()
    {
		Cursor.lockState = CursorLockMode.Locked;
	}

	void Update ()
    {
		Move ();
		MouseLook ();
	}

	void Move ()
	{
		float translation = Input.GetAxis ("Vertical") * moveSpeed;
		float straffe = Input.GetAxis ("Horizontal") * moveSpeed;
		translation *= Time.deltaTime;
		straffe *= Time.deltaTime;
        //devText.text = "Horizontal: " + straffe + " - Vertical: " + translation;
        transform.Translate (straffe, 0, translation);
	}

	void MouseLook()
	{
		var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
		md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
		smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
		mouseLook += smoothV; 
		transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        Camera.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, Camera.transform.up);
        //character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);    
	}
}
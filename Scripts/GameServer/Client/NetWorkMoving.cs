using UnityEngine;

public class NetWorkMoving : MonoBehaviour
{
    public float speed;
    public NetworkManager nManager;
    public string playerName;
    public GameObject startPos;
    public Animator anim;
    public float inputX;
    public float inputZ;
    public bool isShooting = false;
    public bool isAiming = false;
    public float JumpForce = 1000;

    public void OnEnable()
    {
        nManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
        if (nManager != null)
            SendPosition();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
            inputZ = 1.0f;
        else if (Input.GetKey(KeyCode.S))
            inputZ = -1.0f;
        else
            inputZ = 0.0f;
        if (Input.GetKey(KeyCode.A))
            inputX = -1.0f;
        else if (Input.GetKey(KeyCode.D))
            inputX = 1.0f;
        else
            inputX = 0.0f;
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            anim.SetBool("isAiming", true);
            isAiming = true;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            anim.SetBool("isShooting", true);
            isShooting = true;
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            anim.SetBool("isAiming", false);
            isAiming = false;
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            anim.SetBool("isShooting", false);
            isShooting = false;
        }
        anim.SetFloat("InputX", inputX);
        anim.SetFloat("InputZ", inputZ);
        if (nManager != null)
            SendPosition();
    }

    public void SendPosition()
    {
        nManager.ForwardPlayerPos(
            nManager.playerID + "#" + 
            transform.position.x.ToString("n4") + "*" + 
            transform.position.y.ToString("n4") + "*" + 
            transform.position.z.ToString("n4") + "#" + 
            transform.rotation.eulerAngles.x.ToString("n4") + "*" + 
            transform.rotation.eulerAngles.y.ToString("n4") + "*" + 
            transform.rotation.eulerAngles.z.ToString("n4") + "#" +
            inputX + "*" + 
            inputZ + "*" +
            isAiming + "*" +
            isShooting);
    }
}

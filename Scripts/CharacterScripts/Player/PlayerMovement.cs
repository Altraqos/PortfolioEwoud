using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float mouseSpeed = 10;
    public float speed = 5;
    public float runSpeedMult = 1.2f;
    public float jumpHeight = 10;
    public float xVal;
    public float yVal;
    public float gravity = 5;
    public GameObject playerCamera;

    public CharacterController cController;

    public bool lockMouse = false;
    public bool isRunning = false;
    public bool spamFilter = false;

    Vector3 moveDir;
    public float drainSpeed;
    PlayerStamina pStamina;
    PlayerMaster pMaster;

    private void OnEnable()
    {
        SetInitialReferences();
        StartCoroutine("regenerateStamina");
    }

    void SetInitialReferences()
    {
        cController = GetComponent<CharacterController>();
        pStamina = GetComponent<PlayerStamina>();
        pMaster = GetComponent<PlayerMaster>();
    }

    private void Update()
    {
        mouseLook();
        movePlayer();
    }

    public void movePlayer()
    {
        if (Input.GetAxis("Run") > 0)
        {
            if (pMaster.stamina > 0)
            {
                if (!spamFilter)
                {
                    spamFilter = true;
                    StopCoroutine("regenerateStamina");
                    StartCoroutine("drainStamina");
                }
                isRunning = true;
            }
        }
        else if (Input.GetAxis("Run") == 0 || pMaster.stamina == 0)
        {
            if (spamFilter)
                spamFilter = false;
            StopCoroutine("drainStamina");
            isRunning = false;
            StartCoroutine("regenerateStamina");
        }
        if (cController.isGrounded)
        {
            float moveSpeed = 1;
            if (isRunning && pMaster.stamina > 0)
                moveSpeed = speed * runSpeedMult;
            if (!isRunning || pMaster.stamina == 0)
                moveSpeed = speed;
            moveDir = transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"))) * moveSpeed;
            if (Input.GetButton("Jump"))
                moveDir.y = jumpHeight;
        }
        moveDir.y -= gravity * Time.deltaTime;
        cController.Move(moveDir * Time.deltaTime);
    }

    public void mouseLook()
    {
        if (!lockMouse)
        {
            xVal += mouseSpeed * Input.GetAxis("Mouse X");
            yVal -= mouseSpeed * Input.GetAxis("Mouse Y");
            playerCamera.transform.eulerAngles = new Vector3(yVal, xVal, 0.0f);
            transform.eulerAngles = new Vector3(0, xVal, 0);
        }
        else { return; }
    }

    public IEnumerator drainStamina()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(drainSpeed);
            pStamina.DeductStamina(1);
        }
    }
    public IEnumerator regenerateStamina()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(drainSpeed);
            pStamina.IncreaseStamina(1);
        }
    }
}

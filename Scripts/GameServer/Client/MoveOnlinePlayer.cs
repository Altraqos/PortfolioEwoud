using TMPro;
using UnityEngine;

public class MoveOnlinePlayer : MonoBehaviour
{
    public int playerID;
    public Vector3 currentPos;
    public Vector3 currentRot;
    public Animator anim;
    public float inputX;
    public float inputZ;
    public bool isAiming;
    public bool isShooting;
    public bool isJumping;
    public bool isGrounded;
    public string playerName;
    public TextMeshPro playerNameText;

    void Update()
    {
        playerNameText.text = playerName;
        transform.position = currentPos;
        transform.localRotation = Quaternion.Euler(currentRot.x, currentRot.y, currentRot.z);
        anim.SetFloat("InputX", inputX);
        anim.SetFloat("InputZ", inputZ);
        anim.SetBool("isShooting", isShooting);
        anim.SetBool("isAiming", isAiming);        
        anim.SetBool("isJumping", isJumping);        
        anim.SetBool("isGrounded", isGrounded);
    }

    public void isHit(float damage)
    {
        NetworkManager.instance.ForwardEnemyState(damage.ToString());
    }
}

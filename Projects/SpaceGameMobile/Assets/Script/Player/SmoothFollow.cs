using UnityEngine;
using System.Collections;

public class SmoothFollow : MonoBehaviour
{
	public Transform target;
	public float speed;

    public void FixedUpdate()
	{
		transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * speed);
		transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, Time.deltaTime * speed);
	}
}
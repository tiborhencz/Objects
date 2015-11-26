using UnityEngine;
using System.Collections;

public class RotateObject : MonoBehaviour
{	
	public float RotationSpeed = 1f;
	Vector3 m_Rotation;

	void Update()
	{
		m_Rotation += Vector3.one * Time.deltaTime * RotationSpeed;
		transform.rotation = Quaternion.Euler(m_Rotation);
	}
}

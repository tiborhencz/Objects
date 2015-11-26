using UnityEngine;
using System.Collections;

public class RotateToMusic : AnimateToMusic
{
	public Vector3 RotateDimensions = Vector3.one;
	
	Vector3 m_BaseRotation;
	
	protected override void Setup ()
	{
		m_BaseRotation = transform.localEulerAngles;
	}
	
	protected override void Tween(float scale)
	{
		transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, m_BaseRotation + RotateDimensions * scale, Time.deltaTime * 10f);
	}
}

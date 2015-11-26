using UnityEngine;
using System.Collections;

public class ScaleToMusic : AnimateToMusic
{
	public Vector3 ScaleDimensions = Vector3.one;

	Vector3 m_BaseScale;

	protected override void Setup ()
	{
		m_BaseScale = transform.localScale;
	}

	protected override void Tween(float scale)
	{
		transform.localScale = Vector3.Lerp(transform.localScale, m_BaseScale + ScaleDimensions * scale, Time.deltaTime * 10f);
	}
}

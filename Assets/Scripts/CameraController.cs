using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	public float ZoomOutSpeed = 1f;

	IEnumerator Start()
	{
		Camera camera = GetComponent<Camera>();
		yield return new WaitForSeconds(1f);
		for (float f = camera.orthographicSize; f < 3f; f += Time.deltaTime * ZoomOutSpeed)
		{
			camera.orthographicSize = f;
			yield return null;
		}
	}
}

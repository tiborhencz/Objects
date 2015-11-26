using UnityEngine;
using System.Collections;

public class AnimateToMusic : MonoBehaviour
{
	[Range(0, 22000)]
	public int StartHertz = 270;
	public int PickupFrequencyWidth = 100;

	
	void Start()
	{
		Setup();
		StartCoroutine_Auto(DoAnimation());
	}

	protected virtual void Setup() {}

	IEnumerator DoAnimation()
	{
		while (true)
		{
			float avgIntensity = 0;
			int startSpectrumIndex = Mathf.FloorToInt((StartHertz / (AudioProcessor.Frequency / 2f)) * (AudioProcessor.SpectrumData.Length - 1));
			int endSpectrumIndex = Mathf.CeilToInt(((StartHertz + PickupFrequencyWidth) / (AudioProcessor.Frequency / 2f)) * (AudioProcessor.SpectrumData.Length - 1));
			for (int i = startSpectrumIndex; i < endSpectrumIndex; i++)
			{
				avgIntensity += AudioProcessor.SpectrumData[i];
			}
			avgIntensity /= (endSpectrumIndex - startSpectrumIndex);
			float scaleValue = (avgIntensity - 0.005f) * 16;
			Tween(scaleValue);
			yield return null;
		}
	}

	protected virtual void Tween(float scale)
	{

	}
}

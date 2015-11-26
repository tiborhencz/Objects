using UnityEngine;
using System.Collections;

public class AudioProcessor : MonoBehaviour
{
	public AudioSource Source;

	public static float[] SpectrumData = new float[128];
	public static int Frequency = 44100;

	void Start()
	{
		Frequency = Source.clip.frequency;
	}

	void Update()
	{
		Source.GetSpectrumData(SpectrumData, 0, FFTWindow.BlackmanHarris);
	}
}

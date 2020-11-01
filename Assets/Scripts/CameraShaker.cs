using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraShaker : MonoBehaviour
{
	[Range(0,2)]
	[SerializeField] private float shakeDuration;
	[Range(0,0.5f)]
	[SerializeField] private float durationRandomness;

	[Space(20)]
	[Range(0,40)]
	[SerializeField] private float shakeFrequencyX;
	[Range(0,1)]
	[SerializeField] private float xMagnitude;
	[Range(1, 5)]
	[SerializeField] private float dampModifier;

	[Space(20)]
	[Range(0, 40)]
	[SerializeField] private float shakeFrequencyY;
	[Range(0,1)]
	[SerializeField] private float yMagnitude;

	private Camera cam;
	private Vector3 defaultPosition;

	void Start()
	{
		cam = transform.GetComponent<Camera>();
		defaultPosition = transform.position;
	}


	public void ShakeCamera()
    {
		StartCoroutine(ShakeEnum());
    }

	private IEnumerator ShakeEnum()
    {
		float timer = 0;
		float durationRand = Random.Range(-1, 1) * durationRandomness;

		float randMagX = Random.Range(0.5f, 1) * xMagnitude;
		float randMagY = Random.Range(0.5f, 1) * yMagnitude;

		while (timer < shakeDuration + durationRand)
		{
			float xOffset = Mathf.Sin(timer * shakeFrequencyX) * randMagX / (1 + timer * dampModifier);
			float yOffset = Mathf.Sin(timer * shakeFrequencyY) * randMagY / (1 + timer * dampModifier);
			transform.position = defaultPosition + new Vector3(xOffset, yOffset, 0);
			timer += Time.deltaTime;
			yield return null;
        }

		transform.position = defaultPosition;
    }
}

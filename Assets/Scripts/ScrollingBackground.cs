using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ThisNamespace{
	public class ScrollingBackground : MonoBehaviour
	{
		[SerializeField] private Image backgroundImage;
		[SerializeField] private float xSpeed;
		[SerializeField] private float ySpeed;
		void Start()
		{
			if (backgroundImage == null) backgroundImage = transform.GetComponent<Image>();
		}

		void Update()
		{
			ScrollImage();
		}

		private void ScrollImage()
        {
			Vector2 currOffset = backgroundImage.material.mainTextureOffset;
			currOffset += new Vector2(xSpeed, ySpeed) * Time.deltaTime;
			backgroundImage.material.mainTextureOffset = currOffset;
        }
	}
}
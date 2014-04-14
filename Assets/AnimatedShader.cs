using UnityEngine;
using System.Collections;

public class AnimatedShader : MonoBehaviour
{
	public Vector2 uvAnimationRate = new Vector2 (1.0f, 0.0f);

	Vector2 uvOffset = Vector2.zero;

	void LateUpdate ()
	{
		uvOffset += (uvAnimationRate * Time.deltaTime);
		if (renderer.enabled) {
			renderer.material.mainTextureOffset = uvOffset;
		}
	}
}

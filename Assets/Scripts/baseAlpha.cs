using UnityEngine;
using System.Collections;

public class baseAlpha : MonoBehaviour {


	[Range (0f, 1f)]
	public float alphaValue =0.9f;



	// Change the base alpha of the element
	void Start () 
	{
		Color myColor = gameObject.renderer.material.color;
		myColor.a = alphaValue;
		gameObject.renderer.material.color = myColor;
	}
}

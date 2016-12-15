using UnityEngine;
using System.Collections;

public class lookAt : MonoBehaviour {


	public Transform myTarget;


	void Start () 
	{
		transform.LookAt (myTarget);
	}
}

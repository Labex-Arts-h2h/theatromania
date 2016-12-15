using UnityEngine;
using System.Collections;

public class PuzzleAngle : MonoBehaviour
{

	public Transform arCamera;

	private Vector3 camForwardPoint;

	void Update ()
	{
		Vector3 camPuzzleDist = arCamera.position - transform.position;
		float magnitude = camPuzzleDist.magnitude;
		camForwardPoint = arCamera.forward * magnitude;
		float yDiff = transform.position.y - camForwardPoint.y;
		Debug.Log (yDiff);
	}

	void OnDrawGizmos ()
	{
		Gizmos.color = Color.magenta;
		Gizmos.DrawSphere (camForwardPoint, 1);
	}
}

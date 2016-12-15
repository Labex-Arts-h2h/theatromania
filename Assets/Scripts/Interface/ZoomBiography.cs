using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ZoomBiography : MonoBehaviour
{
	public GameObject biographyZoom;
	public GameObject arCamera;
	public Camera cam;
	public mainGestion mainGestionScript;
	public GameObject crosshair;
	public ZoomBiography[] otherBioZoomScripts;

	public bool clickIsActive { get ; set; }

	private Material material;
	private SpriteRenderer spriteRenderer;

	void Start ()
	{
		material = GetComponent<Renderer> ().material;
		spriteRenderer = GetComponent<SpriteRenderer> ();
		clickIsActive = true;
	}

	void Update ()
	{
		if (Input.touchCount == 1 && Input.touches [0].phase == TouchPhase.Began && material.color.a > 0) {
			biographyZoom.transform.parent.gameObject.SetActive (true);
			Vector2 touchPos2D = Input.GetTouch (0).position;
			Vector3 touchPos = new Vector3 (touchPos2D.x, touchPos2D.y, 0);
			Ray ray = cam.ScreenPointToRay (touchPos);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 1000)) {
				if (hit.transform.gameObject == gameObject && clickIsActive) {
					foreach (ZoomBiography zoomScript in otherBioZoomScripts) {
						zoomScript.Reset ();
					}
					StartCoroutine ("LaunchZoomProcess");
				}
			}
		}
	}

	public void Reset ()
	{
		StopAllCoroutines ();
		biographyZoom.SetActive (false);
		clickIsActive = true;
		if (spriteRenderer != null)
			spriteRenderer.enabled = true;
	}

	private IEnumerator LaunchZoomProcess ()
	{
		clickIsActive = false;

		mainGestionScript.disableInteractions ();
		crosshair.SetActive (false);
		spriteRenderer.enabled = false;
		biographyZoom.SetActive (true);

		Vector3 startScale = new Vector3 (0.5f, 0.5f, 1);
		Vector3 endScale = new Vector3 (1, 1, 1);
		Vector3 startPivot = cam.WorldToViewportPoint (transform.position);
		Vector3 endPivot = new Vector3 (0.5f, 0.5f, 0);
		Quaternion startRotation = Quaternion.Euler (0, 0, -arCamera.transform.eulerAngles.z);
		Quaternion endRotation = Quaternion.Euler (0, 0, 0);
		int time = 0;
		while (time <= 100) {
			biographyZoom.transform.localScale = Vector3.Lerp (startScale, endScale, time / 100f);
			Vector3 pos = Vector3.Lerp (startPivot, endPivot, time / 100f);
			((RectTransform)biographyZoom.transform).pivot = new Vector2 (pos.x, pos.y);
			biographyZoom.transform.localRotation = Quaternion.Lerp (startRotation, endRotation, time / 100f);
			yield return new WaitForSeconds (0.02f);
			time += 2;
		}
	}
}

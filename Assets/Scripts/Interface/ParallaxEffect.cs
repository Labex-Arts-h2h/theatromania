using UnityEngine;
using System.Collections;

public class ParallaxEffect : MonoBehaviour
{
	public bool activeRotation;
	public bool activeTranslation;
	public float minAcceleration;
	public float maxAcceleration;
	public float firstPlanTranslation;
	public float secondPlanTranslation;
	public float thirdPlanTranslation;
	public float allPlansRotation;

	public GameObject[] firstPlan;
	public GameObject[] secondPlan;
	public GameObject[] thirdPlan;

	float[] firstPlanInitialX;
	float[] secondPlanInitialX;
	float[] thirdPlanInitialX;

	float smoothedAcceleration;
	float accelerationBuf;
	float accelerationCount;
	float timeCount;
	float smoothTime;
	float initAcceleration;

	void Start ()
	{
		firstPlanInitialX = new float[firstPlan.Length];
		for (int i = 0; i < firstPlanInitialX.Length; i++) {
			firstPlanInitialX [i] = firstPlan [i].transform.localPosition.x;
		}

		secondPlanInitialX = new float[secondPlan.Length];
		for (int i = 0; i < secondPlanInitialX.Length; i++) {
			secondPlanInitialX [i] = secondPlan [i].transform.localPosition.x;
		}

		thirdPlanInitialX = new float[thirdPlan.Length];
		for (int i = 0; i < thirdPlanInitialX.Length; i++) {
			thirdPlanInitialX [i] = thirdPlan [i].transform.localPosition.x;
		}

		smoothTime = 0.1f;
		smoothedAcceleration = -1;
		timeCount = 0;
		accelerationCount = 0;
		accelerationBuf = 0;
		initAcceleration = Input.acceleration.x;
	}

	void Update ()
	{
		//Get rotation angles of the gyroscope
		float inputAcceleration = ScaleFloat (Input.acceleration.x - initAcceleration, minAcceleration, maxAcceleration, -1f, 1f);
		if (smoothedAcceleration == -1)
			smoothedAcceleration = inputAcceleration;
		else {
			if (timeCount >= smoothTime) {
				accelerationBuf /= accelerationCount;
				smoothedAcceleration = accelerationBuf;
				timeCount = 0;
				accelerationBuf = 0;
				accelerationCount = 0;
			} else {
				timeCount += Time.deltaTime;
				accelerationBuf += inputAcceleration;
				accelerationCount++;
			}
		}

		//Apply it to the images
		for (int i = 0; i < firstPlan.Length; i++) {
			GameObject img = firstPlan [i];
			float newLocalPositionX = firstPlanInitialX [i] + (activeTranslation ? smoothedAcceleration : 0) * firstPlanTranslation; 
			img.transform.localPosition = new Vector3 (newLocalPositionX, img.transform.localPosition.y, img.transform.localPosition.z);
			float newLocalRotationY = img.transform.localRotation.y + (activeRotation ? smoothedAcceleration : 0) * allPlansRotation;
			img.transform.localRotation = Quaternion.Euler (img.transform.localRotation.x, newLocalRotationY, img.transform.localRotation.z);
		}
		for (int i = 0; i < secondPlan.Length; i++) {
			GameObject img = secondPlan [i];
			float newLocalPositionX = secondPlanInitialX [i] + (activeTranslation ? smoothedAcceleration : 0) * secondPlanTranslation; 
			img.transform.localPosition = new Vector3 (newLocalPositionX, img.transform.localPosition.y, img.transform.localPosition.z);
			float newLocalRotationY = img.transform.localRotation.y + (activeRotation ? smoothedAcceleration : 0) * allPlansRotation;
			img.transform.localRotation = Quaternion.Euler (img.transform.localRotation.x, newLocalRotationY, img.transform.localRotation.z);
		}
		for (int i = 0; i < thirdPlan.Length; i++) {
			GameObject img = thirdPlan [i];
			float newLocalPositionX = thirdPlanInitialX [i] + (activeTranslation ? smoothedAcceleration : 0) * thirdPlanTranslation; 
			img.transform.localPosition = new Vector3 (newLocalPositionX, img.transform.localPosition.y, img.transform.localPosition.z);
			float newLocalRotationY = img.transform.localRotation.y + (activeRotation ? smoothedAcceleration : 0) * allPlansRotation;
			img.transform.localRotation = Quaternion.Euler (img.transform.localRotation.x, newLocalRotationY, img.transform.localRotation.z);
		}
	}

	private float ScaleFloat (float val, float oldMin, float oldMax, float newMin, float newMax)
	{
		float newVal = newMin + ((val - oldMin) / (oldMax - oldMin)) * (newMax - newMin);
		return newVal;
	}
}

using UnityEngine;
using System.Collections;

public class TestLocationService : MonoBehaviour
{

	public DebugConsole debugScript;
	public GameObject buttonGoodCoords;
	public GameObject buttonLaunchDiorama;
	public bool disableButton = true;
	public bool alwaysEnableButton = false;

	IEnumerator Start()
	{

		// First, check if user has location service enabled
		if (!Input.location.isEnabledByUser)
			yield break;
		
		// Start service before querying location
		Input.location.Start(0.1f,0.1f);
		
		// Wait until service initializes
		int maxWait = 20;
		while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
		{
			yield return new WaitForSeconds(1);
			maxWait--;
		}
		
		// Service didn't initialize in 20 seconds
		if (maxWait < 1)
		{
			print("Timed out"); 
			//debugScript.AddMessage("Timed out");
			yield break;
		}
		
		// Connection has failed
		if (Input.location.status == LocationServiceStatus.Failed)
		{
			print("Unable to determine device location"); 
			//debugScript.AddMessage("Unable to determine device location");
			yield break;
		}
		else
		{
			// Access granted and location value could be retrieved
			print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
			//debugScript.AddMessage("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
		}
		
		// Stop service if there is no need to query location updates continuously
		//Input.location.Stop();
	}


	void Update ()
	{
		/**
		// Location tests
		if ((Input.location.lastData.latitude >= 48.88984f && Input.location.lastData.latitude <= 48.88995f) 
		    && (Input.location.lastData.longitude >= 2.37085f && Input.location.lastData.longitude <= 2.37096f))
		{
			debugScript.ClearMessages ();
			debugScript.AddMessage("Atelier 19");
		}
		else if ((Input.location.lastData.latitude >= 48.889900f && Input.location.lastData.latitude <= 48.890050f) 
		         && (Input.location.lastData.longitude >= 2.37150f && Input.location.lastData.longitude <= 2.37167f))
		{
			debugScript.ClearMessages ();
			debugScript.AddMessage("Entrée 104");
		}
		else
		{
			// Shows update location
			debugScript.ClearMessages ();
			debugScript.AddMessage("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude);
		}
		**/



		// Location Theatre des Varietes
		if ((Input.location.lastData.latitude >= 48.871400f && Input.location.lastData.latitude <= 48.871625f) 
		    && (Input.location.lastData.longitude >= 2.342000f && Input.location.lastData.longitude <= 2.342390f))
		{
			if (disableButton == false)
			{
				buttonLaunchDiorama.SetActive (true);
			}
		}
		else
		{
			if (disableButton == false)
			{
				buttonLaunchDiorama.SetActive (false);// Reverse to false for final app
			}
		}

		// Doesn't need to be near the theatre
		if (alwaysEnableButton == true)
		{
			buttonLaunchDiorama.SetActive (true);
		}
	}



	// Show/Hide button depending on user location
	public void disableButtonLaunchDiorama ()
	{
		disableButton = true;
	}
	public void enableButtonLaunchDiorama ()
	{
		disableButton = false;
	}



	// Show/Hide button depending on "away mode"
	public void enableAwayMode ()
	{
		alwaysEnableButton = true;
	}
	public void disableAwayMode ()
	{
		alwaysEnableButton = false;
	}



	void OnApplicationQuit ()
	{
		Input.location.Stop ();
	}

}
using UnityEngine;
using System.Collections;

namespace Vuforia
{

	public class persisentTracking : MonoBehaviour {


		private bool mPersistActivated = false;
		public GameObject debugScriptObject;
		DebugConsole debugScript;


		void Start()
		{
			debugScript = debugScriptObject.GetComponent<DebugConsole> ();
		} 


		void Update()
		{
			if (!mPersistActivated) 
			{
				ObjectTracker tracker = TrackerManager.Instance.GetTracker<ObjectTracker>();
				tracker.PersistExtendedTracking ( true ); 
				mPersistActivated = true;
				//debugScript.AddMessage("Persistent mode ok");
			}
		}
	}
}

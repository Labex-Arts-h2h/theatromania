// 
//  TestMap.cs
//  
//  Author:
//       Jonathan Derrough <jonathan.derrough@gmail.com>
//  
//  Copyright (c) 2012 Jonathan Derrough
// 
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
// 
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
// 
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.

using UnityEngine;

using System;

using UnitySlippyMap;
using ProjNet.CoordinateSystems;
using ProjNet.CoordinateSystems.Transformations;
using ProjNet.Converters.WellKnownText;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class TestMap : MonoBehaviour
{
	private Map		map;
	
	public Texture	LocationTexture;
	public Texture	MarkerTexture;
	public Texture  theatreVarietesTexture;
	public Texture  palaisRoyalTexture;

	private float	guiXScale;
	private float	guiYScale;
	private Rect	guiRect;
	
	private bool 	isPerspectiveView = false;
	private float	perspectiveAngle = 30.0f;
	private float	destinationAngle = 0.0f;
	private float	currentAngle = 0.0f;
	private float	animationDuration = 0.5f;
	private float	animationStartTime = 0.0f;

	private List<Layer> layers;
	private int     currentLayerIndex = 0;

	private GameObject scriptObject;
	private DebugConsole debugScript;



	//+++++++++++++++++++++++++ Private functions +++++++++++++++++++++++++


	bool Toolbar (Map map)
	{
		/**
		GUI.matrix = Matrix4x4.Scale(new Vector3(guiXScale, guiXScale, 1.0f));
		
		GUILayout.BeginArea(guiRect);
		
		GUILayout.BeginHorizontal();
		
		//GUILayout.Label("Zoom: " + map.CurrentZoom);
		**/
		bool pressed = false;
		/**

        if (GUILayout.RepeatButton("+", GUILayout.ExpandHeight(true)))
		{
			map.Zoom(1.0f);
			pressed = true;
		}
        if (Event.current.type == EventType.Repaint)
        {
            Rect rect = GUILayoutUtility.GetLastRect();
            if (rect.Contains(Event.current.mousePosition))
                pressed = true;
        }



        if (GUILayout.Button("2D/3D", GUILayout.ExpandHeight(true)))
		{
			if (isPerspectiveView)
			{
				destinationAngle = -perspectiveAngle;
			}
			else
			{
				destinationAngle = perspectiveAngle;
			}
			
			animationStartTime = Time.time;
			
			isPerspectiveView = !isPerspectiveView;
		}
        if (Event.current.type == EventType.Repaint)
        {
            Rect rect = GUILayoutUtility.GetLastRect();
            if (rect.Contains(Event.current.mousePosition))
                pressed = true;
        }



        if (GUILayout.Button("Center", GUILayout.ExpandHeight(true)))
        {
            map.CenterOnLocation();
        }
        if (Event.current.type == EventType.Repaint)
        {
            Rect rect = GUILayoutUtility.GetLastRect();
            if (rect.Contains(Event.current.mousePosition))
                pressed = true;
        }



        string layerMessage = String.Empty;
        if (map.CurrentZoom > layers[currentLayerIndex].MaxZoom)
            layerMessage = "\nZoom out!";
        else if (map.CurrentZoom < layers[currentLayerIndex].MinZoom)
            layerMessage = "\nZoom in!";
        


		 
		  //Hide the button to change layers, because not needed anymore
		 
		
		if (GUILayout.Button(((layers != null && currentLayerIndex < layers.Count) ? layers[currentLayerIndex].name + layerMessage : "Layer"), GUILayout.ExpandHeight(true)))
        {
#if UNITY_3_0 || UNITY_3_1 || UNITY_3_2 || UNITY_3_3 || UNITY_3_4 || UNITY_3_5 || UNITY_3_6 || UNITY_3_7 || UNITY_3_8 || UNITY_3_9
            layers[currentLayerIndex].gameObject.SetActiveRecursively(false);
#else
			layers[currentLayerIndex].gameObject.SetActive(false);
#endif
            ++currentLayerIndex;
            if (currentLayerIndex >= layers.Count)
                currentLayerIndex = 0;
#if UNITY_3_0 || UNITY_3_1 || UNITY_3_2 || UNITY_3_3 || UNITY_3_4 || UNITY_3_5 || UNITY_3_6 || UNITY_3_7 || UNITY_3_8 || UNITY_3_9
            layers[currentLayerIndex].gameObject.SetActiveRecursively(true);
#else
			layers[currentLayerIndex].gameObject.SetActive(true);
#endif
            map.IsDirty = true;
        }




        if (GUILayout.RepeatButton("-", GUILayout.ExpandHeight(true)))
		{
			map.Zoom(-1.0f);
			pressed = true;
		}
        if (Event.current.type == EventType.Repaint)
        {
            Rect rect = GUILayoutUtility.GetLastRect();
            if (rect.Contains(Event.current.mousePosition))
                pressed = true;
        }


		GUILayout.EndHorizontal();
					
		GUILayout.EndArea();
		**/
		return pressed;
	}
	
	private
#if !UNITY_WEBPLAYER
        IEnumerator
#else
        void
#endif
        Start ()
	{
		// Set the debuglog for android build
		scriptObject = GameObject.Find ("Scripts");
		//debugScript = scriptObject.GetComponent<DebugConsole> ();
		
		// setup the gui scale according to the screen resolution
		guiXScale = (Screen.orientation == ScreenOrientation.Landscape ? Screen.width : Screen.height) / 480.0f;
		guiYScale = (Screen.orientation == ScreenOrientation.Landscape ? Screen.height : Screen.width) / 640.0f;
		// setup the gui area
		guiRect = new Rect (16.0f * guiXScale, 4.0f * guiXScale, Screen.width / guiXScale - 32.0f * guiXScale, 32.0f * guiYScale);

		// create the map singleton
		map = Map.Instance;
		map.CurrentCamera = Camera.main;
		map.InputDelegate += UnitySlippyMap.Input.MapInput.BasicTouchAndKeyboard;
		map.CurrentZoom = 15.0f;
		// 9 rue Gentil, Lyon
		map.CenterWGS84 = new double[2] { 4.83527, 45.76487 };
		map.UseLocation = true;
		map.InputsEnabled = true;
		map.ShowGUIControls = true;

		map.GUIDelegate += Toolbar;

		layers = new List<Layer> ();

		// create an OSM tile layer
		OSMTileLayer osmLayer = map.CreateLayer<OSMTileLayer> ("OSM");
		osmLayer.BaseURL = "http://a.tile.openstreetmap.org/";
		
		layers.Add (osmLayer);

		// create a WMS tile layer
		WMSTileLayer wmsLayer = map.CreateLayer<WMSTileLayer> ("WMS");
		//wmsLayer.BaseURL = "http://129.206.228.72/cached/osm?"; // http://www.osm-wms.de : seems to be of very limited use
		//wmsLayer.Layers = "osm_auto:all";
		wmsLayer.BaseURL = "http://vmap0.tiles.osgeo.org/wms/vmap0";
		wmsLayer.Layers = "basic";

#if UNITY_3_0 || UNITY_3_1 || UNITY_3_2 || UNITY_3_3 || UNITY_3_4 || UNITY_3_5 || UNITY_3_6 || UNITY_3_7 || UNITY_3_8 || UNITY_3_9
        wmsLayer.gameObject.SetActiveRecursively(false);
#else
		wmsLayer.gameObject.SetActive (false);
#endif

		layers.Add (wmsLayer);

		// create a VirtualEarth tile layer
		VirtualEarthTileLayer virtualEarthLayer = map.CreateLayer<VirtualEarthTileLayer> ("VirtualEarth");
		// Note: this is the key UnitySlippyMap, DO NOT use it for any other purpose than testing
		virtualEarthLayer.Key = "ArgkafZs0o_PGBuyg468RaapkeIQce996gkyCe8JN30MjY92zC_2hcgBU_rHVUwT";
#if UNITY_WEBPLAYER
        virtualEarthLayer.ProxyURL = "http://reallyreallyreal.com/UnitySlippyMap/demo/veproxy.php";
#endif
#if UNITY_3_0 || UNITY_3_1 || UNITY_3_2 || UNITY_3_3 || UNITY_3_4 || UNITY_3_5 || UNITY_3_6 || UNITY_3_7 || UNITY_3_8 || UNITY_3_9
        virtualEarthLayer.gameObject.SetActiveRecursively(false);
#else
		virtualEarthLayer.gameObject.SetActive (false);
#endif

		layers.Add (virtualEarthLayer);


#if !UNITY_WEBPLAYER // FIXME: SQLite won't work in webplayer except if I find a full .NET 2.0 implementation (for free)

		// create an MBTiles tile layer
		bool error = false;

		// on iOS, you need to add the db file to the Xcode project using a directory reference
		string mbTilesDir = "MBTiles/";
		//string filename = "UnitySlippyMap_World_0_8.mbtiles";
		string filename = "paris17.mbtiles";
		string filepath = null;
		if (Application.platform == RuntimePlatform.IPhonePlayer) {
			filepath = Application.streamingAssetsPath + "/" + mbTilesDir + filename;
		} else if (Application.platform == RuntimePlatform.Android) {

			// Note: Android is a bit tricky, Unity produces APK files and those are never unzip on the device.
			// Place your MBTiles file in the StreamingAssets folder (http://docs.unity3d.com/Documentation/Manual/StreamingAssets.html).
			// Then you need to access the APK on the device with WWW and copy the file to persitentDataPath
			// to that it can be read by SqliteDatabase as an individual file

			string newfilepath = Application.temporaryCachePath + "/" + filename;
			if (File.Exists (newfilepath) == false) {
				Debug.Log ("DEBUG : file doesn't exist: " + newfilepath);
				//debugScript.AddMessage("DEBUG : file doesn't exist: " + newfilepath);

				filepath = Application.streamingAssetsPath + "/" + mbTilesDir + filename;
				// TODO: read the file with WWW and write it to persitentDataPath
				WWW loader = new WWW (filepath);
				yield return loader;
				if (loader.error != null) {
					Debug.LogError ("ERROR : " + loader.error);
					//debugScript.AddMessage("ERROR : " + loader.error);
					error = true;
				} else {
					Debug.Log ("DEBUG : will write: '" + filepath + "' to: '" + newfilepath + "'");
					//debugScript.AddMessage("DEBUG : will write: '" + filepath + "' to: '" + newfilepath + "'");
					File.WriteAllBytes (newfilepath, loader.bytes);
				}
			} else
				Debug.Log ("DEBUG : exists: " + newfilepath);
			//debugScript.AddMessage("DEBUG : exists: " + newfilepath);
			filepath = newfilepath;
		} else {
			filepath = Application.streamingAssetsPath + "/" + mbTilesDir + filename;
		}

		if (error == false) {
			Debug.Log ("DEBUG : using MBTiles file: " + filepath);
			//debugScript.AddMessage("DEBUG : using MBTiles file: " + filepath);
			MBTilesLayer mbTilesLayer = map.CreateLayer<MBTilesLayer> ("MBTiles");
			mbTilesLayer.Filepath = filepath;
#if UNITY_3_0 || UNITY_3_1 || UNITY_3_2 || UNITY_3_3 || UNITY_3_4 || UNITY_3_5 || UNITY_3_6 || UNITY_3_7 || UNITY_3_8 || UNITY_3_9
            mbTilesLayer.gameObject.SetActiveRecursively(false);
#else
			mbTilesLayer.gameObject.SetActive (false);
#endif

			layers.Add (mbTilesLayer);
		} else {
			Debug.LogError ("ERROR: MBTiles file not found!");
			//debugScript.AddMessage("ERROR: MBTiles file not found!");
		}

#endif

		/**
		 * 
		 * Sometimes bug and don't appear, so we create them just after the init is done
		 * 
		
        // create some test 2D markers
		GameObject go = Tile.CreateTileTemplate(Tile.AnchorPoint.BottomCenter).gameObject;
		go.renderer.material.mainTexture = MarkerTexture;
		go.renderer.material.renderQueue = 4001;
		go.transform.localScale = new Vector3(0.70588235294118f, 1.0f, 1.0f);
		go.transform.localScale /= 7.0f;
        go.AddComponent<CameraFacingBillboard>().Axis = Vector3.up;


		//// different marker texture :

		// Lieu 1
		GameObject lieu1 = Tile.CreateTileTemplate(Tile.AnchorPoint.BottomCenter).gameObject;
		lieu1.renderer.material.mainTexture = theatreVarietesTexture;
		lieu1.renderer.material.renderQueue = 4001;
		lieu1.transform.localScale = new Vector3(0.70588235294118f, 1.0f, 1.0f);
		lieu1.transform.localScale /= 7.0f;
		lieu1.AddComponent<CameraFacingBillboard>().Axis = Vector3.up;

		// Lieu 2
		GameObject lieu2 = Tile.CreateTileTemplate(Tile.AnchorPoint.BottomCenter).gameObject;
		lieu2.renderer.material.mainTexture = palaisRoyalTexture;
		lieu2.renderer.material.renderQueue = 4001;
		lieu2.transform.localScale = new Vector3(0.70588235294118f, 1.0f, 1.0f);
		lieu2.transform.localScale /= 7.0f;
		lieu2.AddComponent<CameraFacingBillboard>().Axis = Vector3.up;


		GameObject markerGO;
		markerGO = Instantiate(go) as GameObject;
		map.CreateMarker<Marker>("test marker - 9 rue Gentil, Lyon", new double[2] { 4.83527, 45.76487 }, markerGO);

		markerGO = Instantiate(lieu1) as GameObject;
		map.CreateMarker<Marker>("test marker - 31 rue de la Bourse, Lyon", new double[2] { 4.83699, 45.76535 }, markerGO);
		
		markerGO = Instantiate(go) as GameObject;
		map.CreateMarker<Marker>("test marker - 1 place St Nizier, Lyon", new double[2] { 4.83295, 45.76468 }, markerGO);

		markerGO = Instantiate(go) as GameObject;
		map.CreateMarker<Marker>("test marker - 104, Paris", new double[2] { 48.890078, 2.370971 }, markerGO);
		Debug.Log("markers OK");


		DestroyImmediate(go);
		DestroyImmediate(lieu1);
		DestroyImmediate(lieu2);


		// create the location marker
		go = Tile.CreateTileTemplate().gameObject;
		go.renderer.material.mainTexture = LocationTexture;
		go.renderer.material.renderQueue = 4000;
		go.transform.localScale /= 27.0f;
		
		markerGO = Instantiate(go) as GameObject;
		map.SetLocationMarker<LocationMarker>(markerGO);

		DestroyImmediate(go);

		**/
	}



	//+++++++++++++++++++++++++ Unity functions +++++++++++++++++++++++++


	void OnApplicationQuit ()
	{
		map = null;
	}


	void Update ()
	{
		if (destinationAngle != 0.0f) {
			Vector3 cameraLeft = Quaternion.AngleAxis (-90.0f, Camera.main.transform.up) * Camera.main.transform.forward;
			if ((Time.time - animationStartTime) < animationDuration) {
				float angle = Mathf.LerpAngle (0.0f, destinationAngle, (Time.time - animationStartTime) / animationDuration);
				Camera.main.transform.RotateAround (Vector3.zero, cameraLeft, angle - currentAngle);
				currentAngle = angle;
			} else {
				Camera.main.transform.RotateAround (Vector3.zero, cameraLeft, destinationAngle - currentAngle);
				destinationAngle = 0.0f;
				currentAngle = 0.0f;
				map.IsDirty = true;
			}
			
			map.HasMoved = true;
		}
	}



	//+++++++++++++++++++++++++ Public functions +++++++++++++++++++++++++


	public void centerMap ()
	{
		map.CenterOnLocation ();
	}



	// Place markers and set mbtiles map when needed, 
	// because having them placed in the Start() can cause errors
	public void placeMarker ()
	{

		//// Set MBTiles map

		// Hide default map
		layers [currentLayerIndex].gameObject.SetActive (false);

		// Set to mbtiles layer
		currentLayerIndex = 3;

		// if the layer doesn't exist, then go back to basic OSM
		if (currentLayerIndex >= layers.Count) {
			currentLayerIndex = 0;
		}

		layers [currentLayerIndex].gameObject.SetActive (true);
		map.IsDirty = true;



		//// create some test 2D markers

		// Basic marker
		GameObject go = Tile.CreateTileTemplate (Tile.AnchorPoint.BottomCenter).gameObject;
		go.renderer.material.mainTexture = MarkerTexture;
		go.renderer.material.renderQueue = 4001;
		go.transform.localScale = new Vector3 (0.70588235294118f, 1.0f, 1.0f);
		go.transform.localScale /= 2.0f;
		go.AddComponent<CameraFacingBillboard> ().Axis = Vector3.up;
		
		
		//// different marker texture :
		
		// Théatre des Variétés
		GameObject theatreVarietes = Tile.CreateTileTemplate (Tile.AnchorPoint.BottomCenter).gameObject;
		theatreVarietes.renderer.material.mainTexture = theatreVarietesTexture;
		theatreVarietes.renderer.material.renderQueue = 4001;
		theatreVarietes.transform.localScale = new Vector3 (0.70588235294118f, 1.0f, 1.0f);
		theatreVarietes.transform.localScale /= 1.0f;
		theatreVarietes.AddComponent<CameraFacingBillboard> ().Axis = Vector3.up;
		
		// Palais Royal
		GameObject palaisRoyal = Tile.CreateTileTemplate (Tile.AnchorPoint.BottomCenter).gameObject;
		palaisRoyal.renderer.material.mainTexture = palaisRoyalTexture;
		palaisRoyal.renderer.material.renderQueue = 4001;
		palaisRoyal.transform.localScale = new Vector3 (0.70588235294118f, 1.0f, 1.0f);
		palaisRoyal.transform.localScale /= 1.0f;
		palaisRoyal.AddComponent<CameraFacingBillboard> ().Axis = Vector3.up;
		


		// Place the created markers on the map

		GameObject markerGO;

		markerGO = Instantiate (go) as GameObject;
		map.CreateMarker<Marker> ("test marker - 1 place St Nizier, Lyon", new double[2] { 4.83295, 45.76468 }, markerGO);

		markerGO = Instantiate (go) as GameObject;
		map.CreateMarker<Marker> (" ", new double[2] { 4.83000, 45.76500 }, markerGO);
		markerGO = Instantiate (go) as GameObject;
		map.CreateMarker<Marker> (" ", new double[2] { 4.83010, 45.76500 }, markerGO);
		markerGO = Instantiate (go) as GameObject;
		map.CreateMarker<Marker> (" ", new double[2] { 4.83020, 45.76500 }, markerGO);

		//markerGO = Instantiate(go) as GameObject;
		//map.CreateMarker<Marker>("test marker - 104, Paris", new double[2] { 2.37097, 48.89007 }, markerGO);
		markerGO = Instantiate (theatreVarietes) as GameObject;
		map.CreateMarker<Marker> ("test marker - theatre des varietes, Paris", new double[2] { 2.34218, 48.87149 }, markerGO);
		markerGO = Instantiate (palaisRoyal) as GameObject;
		map.CreateMarker<Marker> ("test marker - palais royal, Paris", new double[2] { 2.33767, 48.86616 }, markerGO);

		Debug.Log ("markers OK");


		// Destroy the temporary objects
		DestroyImmediate (go);
		DestroyImmediate (theatreVarietes);
		DestroyImmediate (palaisRoyal);


		// create the location marker
		go = Tile.CreateTileTemplate ().gameObject;
		go.renderer.material.mainTexture = LocationTexture;
		go.renderer.material.renderQueue = 4000;
		go.transform.localScale /= 4.0f; // originally 27.0f
		
		markerGO = Instantiate (go) as GameObject;
		map.SetLocationMarker<LocationMarker> (markerGO);
		
		DestroyImmediate (go);
	}




#if DEBUG_PROFILE
	void LateUpdate()
	{
		Debug.Log("PROFILE:\n" + UnitySlippyMap.Profiler.Dump());
		UnitySlippyMap.Profiler.Reset();
	}
#endif
}


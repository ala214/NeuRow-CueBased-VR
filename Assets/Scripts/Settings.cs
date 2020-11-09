using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// used

public class Settings : MonoBehaviour
{
	public GameObject panel;

	public GameObject gui;
	public GameObject scoregui;
	public GameObject traininggui;
	public GameObject waypoint; 
	public GameObject arrow;
//	public GameObject fireworks;
	
	public GameObject leap, hands, cross, leftarrow, rightarrow;
	public GameObject rMaleHand, lMaleHand, rFemaleHand, lFemaleHand;
	public GameObject arrowLFr, arrowRFr, arrowLFb, arrowRFb, arrowBg, arrowBw;

	//public GameObject rewardtext;
	
	public static bool leapOn = false;
	public static bool isTraining = false;

	public static bool noAPE;

	public static GameObject MainCamera, OculusCamera;

	public Vector3 scaleChange;

	// object reference required to access non-static member "Settings.cross"
	public static Settings Instance;
	void Awake(){
		Instance = this;
	}
	

//General Settings -------------------------
//	static float boatSpeed;
//	static float turnSpeed;
//	static float cutOffAngle;
	static bool audioOn;
	static bool musicOn;  // for future if music is used
	public static bool reverseHands;
	public static bool haptic = true;
	public static bool male;
	public static bool female;
	public static bool stimS;
	public static bool stimM;
	public static bool stimL;
	public static bool points;
	public static bool percentage;
	public static bool flash = true;
	public static bool blackwhite = true;
	public static bool redgray;
	//public static bool flash = false;	// change to true for default SSVEP
//------------------------------------------

//Network Settings -------------------------
	static string ip;
	static string port;
	static bool useNetwork;
//------------------------------------------

//Network Settings -------------------------
	public static bool oculusRift = false;
	static bool leapMotion;
//------------------------------------------

//Old Haptics Settings -------------------------
	public static string comPort = "5";
	public static float m1Power = 20f;
	public static float m2Power = 20f;
	public static float m3Power = 20f;
	public static float m4Power = 20f;
	public static float m5Power = 20f;
	public static float m6Power = 20f;
//------------------------------------------

	public static string duration;
	public static bool Timeout = false;

	public static string logDir = string.Empty;
	public InputField dir;

	public static AudioSource waveSound;
	public AudioClip levelDone; // flag sound?


	void Start(){
		rMaleHand = GameObject.Find("RightHandMale");
		lMaleHand = GameObject.Find("LeftHandMale");
		rFemaleHand = GameObject.Find("RightHandFemale");
		lFemaleHand = GameObject.Find("LeftHandFemale");
		Settings.Instance.rFemaleHand.SetActive(false);
		Settings.Instance.lFemaleHand.SetActive(false);
		
		arrowLFr = GameObject.Find("arrowLFr");
		arrowRFr = GameObject.Find("arrowRFr");
		arrowLFb = GameObject.Find("arrowLFb");
		arrowRFb = GameObject.Find("arrowRFb");
		arrowBg = GameObject.Find("arrowBg");
		arrowBw = GameObject.Find("arrowBw");


		scoregui.SetActive (false);
		traininggui.SetActive (false);
		// will not work for Online if not commented out but needed for training
//		Settings.Instance.rewardtext.SetActive(false);
//		fireworks.SetActive(false);

		waveSound = GetComponent<AudioSource>();

		getLogDirectory ();

		duration = "600";
		Scoring.updateDuration(duration);

		//MainCamera = GameObject.Find("Main Camera");
		//OculusCamera = GameObject.Find("OVRCameraController");

		//OculusCamera.SetActive (false);
//		isTraining = true;

		if(Application.loadedLevelName == "Game") // BCI MODE
		{
			isTraining = false;
			leapOn = false;
			noAPE = true;
			reverseHands = true;
			Debug.Log("Game Mode!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
//			gamePause();
		}
//		else if(Application.loadedLevelName == "Game_With_APE") // BCI MODE
//		{
//			isTraining = false;
//			leapOn = false;
//			noAPE = false;
//			//			gamePause();
//		}
		else if(Application.loadedLevelName == "Leap") // HAND TRACKING MODE
		{
//			isTraining = true;
			leapOn = true;
			reverseHands = false;
		}
//		else if (Application.loadedLevelName == "Training")
//		{
//			isTraining = true;
//			leapOn = false;
//		}



		gamePause ();
	}

	void showScorePanel(){
		scoregui.SetActive (true);
		Time.timeScale = 0f;
		waveSound.PlayOneShot (levelDone); 
		Debug.Log ("showScorePanel---------------------------------");
	}

	void showEndOfTraining(){
		traininggui.SetActive (true);
		Time.timeScale = 0f;
		waveSound.PlayOneShot (levelDone); 
		Debug.Log ("showEndOfTraining-------------------------------");
	}

	void hideScorePanel(){
		scoregui.SetActive (false);
		Time.timeScale = 1f;
	}

	public void gamePause(){
		gui.SetActive (false);
		panel.SetActive (true);
		Time.timeScale = 0f;
//		if(audioOn)
//		audio.Pause();
	}

	public void gameResume(){
		gui.SetActive (true);
		panel.SetActive (false);
		Time.timeScale = 1f;
		scoregui.SetActive (false);
		traininggui.SetActive (false);
	//	audio.Play();
	}

	void getLogDirectory(){
		Debug.Log ("getting the log directory!!!!!!!!!!!!!");
		logDir = Application.persistentDataPath;
		dir.text = logDir;
		Debug.Log ("Log Directory: " + dir.text);
	}

	// Update is called once per frame
	void Update () {
		if (female) {
			Settings.Instance.rFemaleHand.SetActive(true);
			Settings.Instance.lFemaleHand.SetActive(true);
		}
		if (Timeout && !isTraining) {
			//gamePause();
			showScorePanel();
			Timeout = false;
		}
		if (Timeout && isTraining) {
			//gamePause();
			showEndOfTraining();
			Timeout = false;
		}

//		Debug.Log ("leap: " + DisconnectionNotice.IsConnected());

//		if(Application.loadedLevel == 2 || Application.loadedLevel == 3)
//		{
			//enable/disable settings panel
		if (Input.GetKey (KeyCode.P)) {
			gamePause();
		}
//			if (Input.GetKey (KeyCode.L)) {
//				gameResume();
//			}
//			
		if(Input.GetKey(KeyCode.Escape))
		{
			Application.LoadLevel("MainMenu");
		}
			
//			if (Input.GetKey (KeyCode.T))
//			{
//				Application.LoadLevel("Training");
////				isTraining = true;
////				leapOn = true;
//			}
//			if (Input.GetKey (KeyCode.F1))
//			{
//				Application.LoadLevel("Game_No_APE");
//			}
//			if (Input.GetKey (KeyCode.F2))
//			{
//			Application.LoadLevel("Game_With_APE");
//			}
//			if (Input.GetKey (KeyCode.G)) {
//				isTraining = false;
//				leapOn = false;
//			}
			
			
			//enable/disable BCI training mode
			if (isTraining) {
				//			gui.SetActive (false);
				waypoint.SetActive (false);
				arrow.SetActive (false);
				Debug.Log("isTraining");
			}
			
			if (!isTraining) {
				//			gui.SetActive (true);
				waypoint.SetActive (true);
				arrow.SetActive (true);
				Debug.Log("!isTraining");
			}
			
			//enable/disable leapmotion
			if (leapOn) {
				leap.SetActive (true);
				waypoint.SetActive (true);
				arrow.SetActive (true);
				//	hands.SetActive (false);
			}
			if (!leapOn) {
				leap.SetActive (false);
				hands.SetActive (true);
			}
		//}
	}


	public static void updateVariables(string name, string value)
	{
		switch(name)
		{
		case "SendIPAddress":
//			print(name+": "+value);
			ip = value;
			break;
		case "SendPort":
//			print(name+": "+value);
			port = value;
			break;
		case "LocalIPAddress":
//			print(name+": "+value);
			ip = value;
			break;
		case "LocalPort":
//			print(name+": "+value);
			UDPReceive.portField = value;
			break;
		case "ComPort":
//			print(name+": "+value);
			comPort = value;
			break;
		case "Duration":
			duration = value;
			Scoring.updateDuration(duration);
			Timeout=false;
			break;
		}
	}

	public static void updateVariables(string name, bool value)
	{
		switch(name)
		{
		case "BackgroundSound":
//			print(name+": "+value);
			audioOn = value;
			if(audioOn)
				waveSound.Play();
			else
				waveSound.Pause();
			break;
		case "BackgroundHap":
			print("Haptic: "+value);
			haptic = value;
			break;
		case "BackgroundMusic":  // for future if music used
			// figure out how to start and stop music
			print(name+": "+value);
			musicOn = value;
			// turn music on and off
			break;
// find way to load female scene?
		case "BackgroundMale":
			print(name+": "+value);
			male = value;
			Settings.Instance.rMaleHand.SetActive(true);
			Settings.Instance.lMaleHand.SetActive(true);
			Settings.Instance.rFemaleHand.SetActive(false);
			Settings.Instance.lFemaleHand.SetActive(false);
			break;
// or find way to switch out avatar hands
		case "BackgroundFemale":
			print(name+": "+value);
			female = value;
			Settings.Instance.rFemaleHand.SetActive(true);
			Settings.Instance.lFemaleHand.SetActive(true);
			Settings.Instance.rMaleHand.SetActive(false);
			Settings.Instance.lMaleHand.SetActive(false);
			break;
// cross width & height = 100 && arrow width & height
		case "BackgroundS":
//			print(name+": "+value);
			stimS = value;
			//change stim size to S
			Settings.Instance.cross.transform.localScale = new Vector3(1f,1f,1f);
			Settings.Instance.cross.transform.localPosition = new Vector3(-0.929f,0f,-0.95f);
			Settings.Instance.leftarrow.transform.localScale = new Vector3(1f,1f,1f);
			Settings.Instance.leftarrow.transform.localPosition = new Vector3(-0.89f, 0f, 2.21f);
			Settings.Instance.rightarrow.transform.localScale = new Vector3(1f,1f,1f);
			Settings.Instance.rightarrow.transform.localPosition = new Vector3(4.7f, 0f, 2.21f);
//			rewardtext.GetComponent<Text>().fontSize = 30f;
//			Settings.Instance.rewardtext.transform.localScale = new Vector3(0.5f,0.5f,1f);
			break;
// cross width & height = 300 && arrow width & height
		case "BackgroundMd":
//			print(name+": "+value);
			stimM = value;
			Settings.Instance.cross.transform.localScale = new Vector3(3f,3f,1f);
			Settings.Instance.cross.transform.localPosition = new Vector3(-0.94f, 4.05f, -0.95f);
			Settings.Instance.leftarrow.transform.localScale = new Vector3(3f,3f,1f);
			Settings.Instance.leftarrow.transform.localPosition = new Vector3(-0.6f, 4.1f, 2.21f);
			Settings.Instance.rightarrow.transform.localScale = new Vector3(3f,3f,1f);
			Settings.Instance.rightarrow.transform.localPosition = new Vector3(15.6299f, 4.1f, 2.21f);
//			Settings.Instance.rewardtext.fontSize = 50.0f;
//			Settings.Instance.rewardtext.transform.localScale = new Vector3(1f,1f,1f);
			break;
// cross width & height = 500 && arrow width & height
		case "BackgroundL":
			print(name+": "+value);
			stimL = value;
			// change stim size to L
			Settings.Instance.cross.transform.localScale = new Vector3(5f,5f,1f);
			Settings.Instance.cross.transform.localPosition = new Vector3(-0.94f, 8.7f, -0.95f);
			Settings.Instance.leftarrow.transform.localScale = new Vector3(5f,5f,1f);
			Settings.Instance.leftarrow.transform.localPosition = new Vector3(-0.4f, 9f, 2.21f);
			Settings.Instance.rightarrow.transform.localScale = new Vector3(5f,5f,1f);
			Settings.Instance.rightarrow.transform.localPosition = new Vector3(26.8f, 9f, 2.21f);
//			Settings.Instance.rewardtext.fontSize = 75.0f;
//			Settings.Instance.rewardtext.transform.localScale = new Vector3(2f,2f,1f);
			break;
		case "BackgroundFlash":
			//print(name+": "+value);
			flash = value;
			break;
		case "BackgroundB/W":
			blackwhite = value;
			Settings.Instance.arrowLFr.SetActive(false); 
			Settings.Instance.arrowRFr.SetActive(false);
			Settings.Instance.arrowBg.SetActive(false);
			break;
		case "BackgroundR/G":
			redgray = value;
			Settings.Instance.arrowLFb.SetActive(false);
			Settings.Instance.arrowRFb.SetActive(false); 
			Settings.Instance.arrowBw.SetActive(false);
			break;
		case "BackgroundPoints": // reward points
			//print(name+": "+value);
			points = value;
			break;
		case "BackgroundPer": // reward accuracy percentages
			percentage = value;
			break;
		case "Use":
//			print(name+": "+value);
			useNetwork = value;
			break;
//		case "OculusRift":
//			print(name+": "+value);
//			oculusRift = value;
//			switchCamera(oculusRift);
//			break;
		case "LeapMotion":
//			print(name+": "+value);
			Settings.leapOn = value;
			if(value)
				Application.LoadLevel("Leap");
			else{
				Application.LoadLevel("Game");
			}
			break;
		case "Training":
			isTraining = value;
			break;
		}
	}

//	public static void switchCamera(bool oculus){

//		if (oculus) {
//			OculusCamera.SetActive(true);
//			MainCamera.SetActive(false);
//		}
//		else{
//			OculusCamera.SetActive(false);
//			MainCamera.SetActive(true);
//		}
//	}


	public static void updateVariables(string name, float value)
	{
//		print(name+": "+value);

		switch(name)
		{
		case "BoatSpeed":
//			print(name+": "+value);
			MoveBoat.boatspeed = value;
			break;
		case "TurnSpeed":
//			print(name+": "+value);
			MoveBoat.turnspeed = value;
			break;
		case "CutOffAngle":
//			print(name+": "+value);
			MoveBoat.stoppingAngle = value;
			break;
		case "M1":
//			print(name+": "+value);
			m1Power = value;
			break;
		case "M2":
//			print(name+": "+value);
			m2Power = value;
			break;
		case "M3":
//			print(name+": "+value);
			m3Power = value;
			break;
		case "M4":
//			print(name+": "+value);
			m4Power = value;
			break;
		case "M5":
//			print(name+": "+value);
			m5Power = value;
			break;
		case "M6":
//			print(name+": "+value);
			m6Power = value;
			break;
		}
	}

}

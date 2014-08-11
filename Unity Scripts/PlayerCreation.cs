using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerCreation : MonoBehaviour
{
	/*
	 * TODO: Convert large switch statements to delegate functions
	 * TODO: See about converting string items to enums 
	 * TODO: Remove the absolute values for the gui objects
	*/

	public GUISkin StoneGuiSkin;
	bool debugOn;

	private SkinnedMeshRenderer hairMorphs;
	private SkinnedMeshRenderer bodyMorphs;
	private SkinnedMeshRenderer armMorphs;
	private SkinnedMeshRenderer skinMorphs;
	private SkinnedMeshRenderer waistMorphs;
	private SkinnedMeshRenderer legMorphs;
	private SkinnedMeshRenderer feetMorphs;

	static Hashtable blendShapeMap = new Hashtable();

	private GameObject head;
	private GameObject[] body;
	private GameObject[] arms;
	private GameObject[] legs;
	private GameObject[] pectoral;
	
	float tSliderValue = 0.0f;
	float thinValue = 0.0f;
	float hSliderValue = 0.0f;
	float heavyValue = 0.0f;
	float mSliderValue = 0.0f;
	float legsSliderValue = 1.0f;
	float pectoralSliderValue = 1.0f;
	
	private int genderInt = 3;
	private int selectedRaceIndex = 0;
	private string[] genderBarStrings = {"Male", "Female", "???"};
	private string[] raceBarStrings = {"Human", "Reptilian", "Feline", "Lupine", "Bovine"};
	
	private int brawn = 10;
	private int vim = 10;
	private int acuity = 10;
	private int wit = 10;
	
	private bool isAddLimit = false;
	private bool isSubLimit = false;
	
	private string name = "";
	
	Rect windowRect1 = new Rect(Screen.width/2 - 60, Screen.height/2 - 35, Screen.width/4, Screen.height/5);	
	bool doWindow1 = false;

	int[] faceAreas = {20,21,23};
	int[] eyeAreas = {2,4,5,19,22};
	int[] mouthAreas = {6,7,8,9};
	int[] torsoAreas = {11,12,13,16,17};
	int[] limbAreas = {0,10,14,15,18,24,25};
	
	PlayerCreation playerCreationScript;
	PlayerEntity playerStats;
	StoryWindow storyWindowScript;
	GameObject playerCam;
	
	GUIContent[] comboBoxList;
	private ComboBox comboBoxControl;// = new ComboBox();
	private GUIStyle listStyle = new GUIStyle();
	
	void Start ()
	{	
		// Wierd Unity Inheritance stuff here
		playerCreationScript = GetComponent<PlayerCreation>();
		playerStats = GetComponent<PlayerEntity>();
		debugOn = GetComponent<MainMenu>().getMainDebug();
		playerStats.setDebug(debugOn);
		storyWindowScript = GetComponent<StoryWindow>();
		playerCam = GameObject.FindGameObjectWithTag("Player");


		// Will need to revist incase more then one GameObject that
		// shares a player outfit mesh like a hat or coat.
		GameObject hairMesh = GameObject.FindGameObjectWithTag("Hair");
		GameObject bodyMesh = GameObject.FindGameObjectWithTag("Body");
//		GameObject armMesh = GameObject.FindGameObjectWithTag("Arms");
		GameObject skinMesh = GameObject.FindGameObjectWithTag("Skin");
		GameObject waistMesh = GameObject.FindGameObjectWithTag("Waist");
		GameObject legMesh = GameObject.FindGameObjectWithTag("Legs");
		GameObject feetMesh = GameObject.FindGameObjectWithTag("Feet");

		// Getting the Mesh that the blendshapes are attached...
		hairMorphs = hairMesh.GetComponent<SkinnedMeshRenderer>();
		bodyMorphs = bodyMesh.GetComponent<SkinnedMeshRenderer>();
//		armMorphs = armMesh.GetComponent<SkinnedMeshRenderer>();
		skinMorphs = skinMesh.GetComponent<SkinnedMeshRenderer>();
		waistMorphs = waistMesh.GetComponent<SkinnedMeshRenderer>();
		legMorphs = legMesh.GetComponent<SkinnedMeshRenderer>();
		feetMorphs = feetMesh.GetComponent<SkinnedMeshRenderer>();

		// Getting the indexs from the player meshes...
		getBlendShapeIndexes (hairMorphs);
		getBlendShapeIndexes (bodyMorphs);
//		getBlendShapeIndexes (armMorphs);
		getBlendShapeIndexes (skinMorphs);
		getBlendShapeIndexes (waistMorphs);
		getBlendShapeIndexes (legMorphs);
		getBlendShapeIndexes (feetMorphs);

		// Combo Box
		comboBoxList = new GUIContent[raceBarStrings.Length];
		for(int idxRace = 0;idxRace < raceBarStrings.Length;idxRace++)
		{
			comboBoxList[idxRace] = new GUIContent(raceBarStrings[idxRace]);
		}
		listStyle.normal.textColor = Color.white;
		listStyle.fontSize = 12;
		listStyle.onHover.background =
			listStyle.hover.background = new Texture2D(1,1);
		listStyle.padding.left =
		listStyle.padding.right =
			listStyle.padding.top =
				listStyle.padding.bottom = 4;
		
		comboBoxControl = new ComboBox(new Rect(220, 125, 100, 30), comboBoxList[0], comboBoxList, "button", "box", listStyle);
	}
	
	void OnGUI()
	{
		GUI.skin = StoneGuiSkin;

		GUI.Box(new Rect(0,0,((Screen.width*3)/4),Screen.height), "Player Creation Menu");
		//Debug.Log((Screen.width-145) + "/ 425: 318/" + (Screen.height-2));
		
		// Sliders
		GUI.Label (new Rect (25, 62, 100, 30), "Thin");
		setBlendShapes("FBMThin", thinValue);
		tSliderValue = GUI.HorizontalSlider (new Rect (20, 80, 100, 30), tSliderValue, -100.0f, 100.0f);
		hSliderValue = -tSliderValue;
		if (hSliderValue > 0.0f)
			thinValue = 0.0f;
		else
			thinValue = tSliderValue;
		
		GUI.Label (new Rect (25, 92, 100, 30), "Heavy");
		setBlendShapes("FBMHeavy", heavyValue);
		hSliderValue = GUI.HorizontalSlider (new Rect (20, 110, 100, 30), hSliderValue, -100.0f, 100.0f);
		tSliderValue = -hSliderValue;
		if (tSliderValue > 0.0f)
			heavyValue = 0.0f;
		else
			heavyValue = hSliderValue;

		GUI.Label (new Rect (25, 122, 120, 30), "Body Tone");
		setBlendShapes("FBMBodyBuilder", mSliderValue);
		setBlendShapes("FBMBodyBuilderDetails", mSliderValue);
		mSliderValue = GUI.HorizontalSlider (new Rect (20, 140, 100, 30), mSliderValue, 0.0f, 100.0f);
		
//		GUI.Label (new Rect (25, 152, 100, 30), "Legs");
//		legsSliderValue = GUI.HorizontalSlider (new Rect (20, 170, 100, 30), legsSliderValue, 0.5f, 2.0f);
		
//		GUI.Label (new Rect (25, 182, 100, 30), "Pectorals");
//		pectoralSliderValue = GUI.HorizontalSlider (new Rect (20, 200, 100, 25), pectoralSliderValue, 0.5f, 2.0f);
		
		// Buttons
		genderInt = GUI.Toolbar (new Rect (145, 85, 250, 30), genderInt, genderBarStrings);
		if (genderInt == 0 && selectedRaceIndex != 2) // Male
		{
			setBlendShapes("FBMBasicMale", 100.0f);
			setBlendShapes("FBMBasicFemale", 0.0f);
		}
		else if (genderInt == 1 && selectedRaceIndex != 2) // Female
		{
			setBlendShapes("FBMBasicMale", 0.0f);
			setBlendShapes("FBMBasicFemale", 100.0f);
		}
		else if(genderInt > 1 || genderInt < 0) //Genderless
		{
			setBlendShapes("FBMBasicMale", 0.0f);
			setBlendShapes("FBMBasicFemale", 0.0f);
		}

		if(GUI.Button (new Rect (((Screen.width*3)/4)/2+60,50,60,30), "Create"))
			doWindow1 = true;

		// Race Selection Via ComboBox
		selectedRaceIndex = comboBoxControl.Show();
		switch(selectedRaceIndex)
		{
			case 0: // Human
				setMaterialTexture(faceAreas, "M4JeremyRRFaceM1");
				setMaterialTexture(torsoAreas, "M4JeremyRRTorsoM_NG");
				setMaterialTexture(limbAreas, "M4JeremyRRLimbsM");
				setMaterialTexture(mouthAreas, "M4JeremyRRInMouthM");
				setMaterialTexture(eyeAreas, "M4JeremyRREyesM");

				setBlendShapes("Raw_Mino_Genesis", 0.0f);
				setBlendShapes("Raw_Drakon", 0.0f);
				setBlendShapes("CW_MLion", 0.0f);
				setBlendShapes("CW_FLion", 0.0f);
				setBlendShapes("FBMAnubis", 0.0f);
				break;

			case 1: //Rept
				setMaterialTexture(faceAreas, "1_V4Drak_face_T2a");
				setMaterialTexture(torsoAreas, "2_V4Drak_Torso_T2a");
				setMaterialTexture(limbAreas, "3_V4Drak_limbs_T2a");
				setMaterialTexture(mouthAreas, "4_DrakMouth_T");
				setMaterialTexture(eyeAreas, "5_DrakeEye-T");

				setBlendShapes("Raw_Mino_Genesis", 0.0f);
				setBlendShapes("Raw_Drakon", 100.0f);
				setBlendShapes("CW_MLion", 0.0f);
				setBlendShapes("CW_FLion", 0.0f);
				setBlendShapes("FBMAnubis", 0.0f);
				break;

			case 2: //Feline
				setMaterialTexture(faceAreas, "1_CWJagFace_T");
				setMaterialTexture(torsoAreas, "2_CWJagTorso_T");
				setMaterialTexture(limbAreas, "3_CWJagLimbs_T");
				setMaterialTexture(mouthAreas, "4_CWLionMouth_T");
				setMaterialTexture(eyeAreas, "5_CWJagEye-T");
				
				setBlendShapes("Raw_Mino_Genesis", 0.0f);
				setBlendShapes("Raw_Drakon", 0.0f);
				setBlendShapes("FBMAnubis", 0.0f);
				if(genderInt == 0)
				{
					setBlendShapes("CW_MLion", 100.0f);
					setBlendShapes("FBMBasicMale", 0.0f);
					setBlendShapes("CW_FLion", 0.0f);
					setBlendShapes("FBMBasicFemale", 0.0f);
				}
				else if(genderInt == 1)
				{
					setBlendShapes("CW_MLion", 0.0f);
					setBlendShapes("FBMBasicMale", 0.0f);
					setBlendShapes("CW_FLion", 100.0f);
					setBlendShapes("FBMBasicFemale", 0.0f);
				}
				else
				{
					setBlendShapes("CW_MLion", 50.0f);
					setBlendShapes("FBMBasicMale", 0.0f);
					setBlendShapes("CW_FLion", 50.0f);
					setBlendShapes("FBMBasicFemale", 0.0f);
				}
				break;

			case 3: // Lupine
				setMaterialTexture(faceAreas, "AnubisHeadBlack");
				setMaterialTexture(torsoAreas, "AnubisBodyBlack");
				setMaterialTexture(limbAreas, "AnubisBodyBlack");
				setMaterialTexture(mouthAreas, "AnubisMouth");
				setMaterialTexture(eyeAreas, "AnubisEyesGreen");

				setBlendShapes("Raw_Mino_Genesis", 0.0f);
				setBlendShapes("Raw_Drakon", 0.0f);
				setBlendShapes("CW_MLion", 0.0f);
				setBlendShapes("CW_FLion", 0.0f);
				setBlendShapes("FBMAnubis", 100.0f);
				break;

			case 4: //Bovine
				setMaterialTexture(faceAreas, "1_Taur2Face_T");
				setMaterialTexture(torsoAreas, "2_Taur2Torso_T");
				setMaterialTexture(limbAreas, "3_Taur2Limbs_T");
				setMaterialTexture(mouthAreas, "4_Taur2Mouth_T");
				setMaterialTexture(eyeAreas, "5_MinoEye-T");

				setBlendShapes("Raw_Mino_Genesis", 100.0f);
				setBlendShapes("Raw_Drakon", 0.0f);
				setBlendShapes("CW_MLion", 0.0f);
				setBlendShapes("CW_FLion", 0.0f);
				setBlendShapes("FBMAnubis", 0.0f);
				break;

			default:
				setMaterialTexture(faceAreas, "M4JeremyRRFaceM1");
				setMaterialTexture(torsoAreas, "M4JeremyRRTorsoM_NG");
				setMaterialTexture(limbAreas, "M4JeremyRRLimbsM");
				setMaterialTexture(mouthAreas, "M4JeremyRRInMouthM");
				setMaterialTexture(eyeAreas, "M4JeremyRREyesM");

				setBlendShapes("Raw_Mino_Genesis", 0.0f);
				setBlendShapes("Raw_Drakon", 0.0f);
				setBlendShapes("CW_MLion", 0.0f);
				setBlendShapes("CW_FLion", 0.0f);
				setBlendShapes("FBMAnubis", 0.0f);
				break;
		}
		
		// Spinners
		// TODO: Make spinners a class or somthing for less
		// 		 clutter in the code.

		GUI.BeginGroup(new Rect (25, 222, 165, 30));
		GUI.Label (new Rect (0, 0, 100, 30), "Brawn");
		GUI.enabled = false;
		string t1 = GUI.TextField(new Rect(75, 0, 30, 20), brawn.ToString(), 3);
		brawn = int.Parse(t1);
		GUI.enabled = !isAddLimit && (brawn < 20);
		if(GUI.Button (new Rect (110,0,20,20), "+"))
			brawn++;
		GUI.enabled = !isSubLimit && (brawn > 2);
		if(GUI.Button (new Rect (135,0,20,20), "-"))
			brawn--;
		GUI.enabled = true;
		GUI.EndGroup();
		
		GUI.BeginGroup(new Rect (25, 242, 165, 30));
		GUI.Label (new Rect (0, 0, 100, 30), "Vim");
		GUI.enabled = false;
		string t2 = GUI.TextField(new Rect(75, 0, 30, 20), vim.ToString(), 3);
		vim = int.Parse(t2);
		GUI.enabled = !isAddLimit && (vim < 20);
		if(GUI.Button (new Rect (110,0,20,20), "+"))
			vim++;
		GUI.enabled = !isSubLimit && (vim > 2);
		if(GUI.Button (new Rect (135,0,20,20), "-"))
			vim--;
		GUI.enabled = true;
		GUI.EndGroup();
		
		GUI.BeginGroup(new Rect (25, 262, 165, 30));
		GUI.Label (new Rect (0, 0, 100, 30), "Acuity");
		GUI.enabled = false;
		string t3 = GUI.TextField(new Rect(75, 0, 30, 20), acuity.ToString(), 3);
		acuity = int.Parse(t3);
		GUI.enabled = !isAddLimit && (acuity < 20);
		if(GUI.Button (new Rect (110,0,20,20), "+"))
			acuity++;
		GUI.enabled = !isSubLimit && (acuity > 2);
		if(GUI.Button (new Rect (135,0,20,20), "-"))
			acuity--;
		GUI.enabled = true;
		GUI.EndGroup();
		
		GUI.BeginGroup(new Rect (25, 282, 165, 30));
		GUI.Label (new Rect (0, 0, 100, 30), "Wit");
		GUI.enabled = false;
		string t4 = GUI.TextField(new Rect(75, 0, 30, 20), wit.ToString(), 3);
		wit = int.Parse(t4);
		GUI.enabled = !isAddLimit && (wit < 20);
		if(GUI.Button (new Rect (110,0,20,20), "+"))
			wit++;
		GUI.enabled = !isSubLimit && (wit > 2);
		if(GUI.Button (new Rect (135,0,20,20), "-"))
			wit--;
		GUI.enabled = true;
		GUI.EndGroup();
		
		int sumStats = brawn + vim + acuity + wit;
		if(sumStats > 47)
		{
			isAddLimit = true;
			isSubLimit = false;
		}
		else if(sumStats < 41)
		{
			isAddLimit = false;
			isSubLimit = true;
		}
		else
		{
			isAddLimit = false;
			isSubLimit = false;
		}
		
		// TextFields
		GUI.Label (new Rect (((Screen.width*3)/4)/2-100, 57, 50, 30), "Name:");
		name = GUI.TextField(new Rect(((Screen.width*3)/4)/2-45, 55, 100, 20), name, 25);

		// Window
		// TODO: Fix the window texture
		if(doWindow1)
		{
			GUI.Window(0, windowRect1, loadWindow, "Are you shure?");
		}
	}
	
	void loadWindow(int windowID)
	{
		if (GUI.Button(new Rect(5, windowRect1.height - 35, (windowRect1.width/2) - 10, 30), "Yes"))
		{
			playerStats.setName(name);
			playerStats.setRaceType(selectedRaceIndex);
			playerStats.setThin(tSliderValue);
			playerStats.setHeavy(hSliderValue);
			playerStats.setBuild(mSliderValue);
			playerStats.setHitPoints(5*vim);
			playerStats.setEndurence(10*brawn);
			storyWindowScript.enabled = true;
			playerCam.active = false;
			playerCreationScript.enabled = false;
            doWindow1 = false;
		}
		if (GUI.Button(new Rect((windowRect1.width/2) + 5, windowRect1.height - 35, (windowRect1.width/2) - 10, 30), "No"))
            doWindow1 = false;
	}

	// This is to store all the BlendShape values to a
	// HashTable for reduced calls on the mesh itself
	// and also to advoid adding indexs by hand on each
	// new model upload do to the scrambling of it's
	// placement. Also it can be expanded out for more
	// blends as time goes on.

	void getBlendShapeIndexes(SkinnedMeshRenderer smr)
	{
		Mesh mSkin = smr.sharedMesh;

		for(int i = 0; i < mSkin.blendShapeCount; i++)
		{
			string s = mSkin.GetBlendShapeName(i);
			if(s.Contains("FBMBasicMale"))
			{
				blendShapeMap.Add (s, i);
			}
			if(s.Contains("FBMBasicFemale"))
			{
				blendShapeMap.Add (s, i);
			}
			if(s.Contains("FBMBodyBuilder"))
			{
				if(s.Contains("FBMBodyBuilderDetails"))
				{
					blendShapeMap.Add (s, i);
				}
				else blendShapeMap.Add (s, i);
			}
			if(s.Contains("FBMHeavy"))
			{
				blendShapeMap.Add (s, i);
			}
			if(s.Contains("FBMThin"))
			{
				blendShapeMap.Add (s, i);
			}
			if(s.Contains("Raw_Mino_Genesis"))
			{
				blendShapeMap.Add (s, i);
			}
			if(s.Contains("Raw_Drakon"))
			{
				blendShapeMap.Add (s, i);
			}
			if(s.Contains("CW_MLion"))
			{
				blendShapeMap.Add (s, i);
			}
			if(s.Contains("CW_FLion"))
			{
				blendShapeMap.Add (s, i);
			}if(s.Contains("FBMAnubis"))
			{
				blendShapeMap.Add (s, i);
			}
		}
	}

	// Reduce the reptiveness of setting the blend shape
	// weight and make it more dynamic for future additions
	// like more clothing or weapons.

	void setBlendShapes(string blendName, float precentBlend)
	{
		if(blendShapeMap.ContainsKey("WildMane for Genesis." + blendName))
			hairMorphs.SetBlendShapeWeight((int)blendShapeMap["WildMane for Genesis." + blendName], precentBlend);
		if(blendShapeMap.ContainsKey("G1MPrShirt." + blendName))
			bodyMorphs.SetBlendShapeWeight((int)blendShapeMap["G1MPrShirt." + blendName], precentBlend);
		if(blendShapeMap.ContainsKey("Genesis." + blendName))
			skinMorphs.SetBlendShapeWeight((int)blendShapeMap["Genesis." + blendName], precentBlend);
		if(blendShapeMap.ContainsKey("G1MPrSash." + blendName))
			waistMorphs.SetBlendShapeWeight((int)blendShapeMap["G1MPrSash." + blendName], precentBlend);
		if(blendShapeMap.ContainsKey("G1MPrPants." + blendName))
			legMorphs.SetBlendShapeWeight((int)blendShapeMap["G1MPrPants." + blendName], precentBlend);
		if(blendShapeMap.ContainsKey("G1MPrBoots." + blendName))
			feetMorphs.SetBlendShapeWeight((int)blendShapeMap["G1MPrBoots." + blendName], precentBlend);
	}

	// Sets the materal as long as it not the current materal
	// and reduces the number calls to the mesh.

	void setMaterialTexture(int[] materialArea, string textureName)
	{
		if (!textureName.Equals(skinMorphs.GetComponent<SkinnedMeshRenderer>().materials[materialArea[0]].mainTexture.name))
		{
			Texture2D texMap = (Texture2D)Resources.Load<Texture> ("Textures/" + textureName);
			foreach(int idxMaterial in materialArea)
			{
				skinMorphs.GetComponent<SkinnedMeshRenderer>().materials[idxMaterial].mainTexture = texMap;
			}
		}
	}
}

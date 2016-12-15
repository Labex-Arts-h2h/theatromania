using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Text;
using System.IO; 
using System;

public class PrompterSubtitles : MonoBehaviour
{
	[Range (0f, 20f)]
	public float
		defaultTimeBetweenTexts = 5f;
	public Text prompterText;
	public bool skipText = false;
	public DebugConsole debugScript;

	private string textName = null;
	private Color tempColor;
	public string filePath;
	private string[] allLines = new string[10];

	private string[] TB1 = new string[6];
	private string[] TB3 = new string[1];
	private string[] TB4 = new string[2];
	private string[] TB5 = new string[2];
	private string[] TB6 = new string[2];
	private string[] TB7 = new string[2];

	private string[] TC1 = new string[1];
	private string[] TC2 = new string[1];
	private string[] TC3 = new string[4];
	private string[] TC4 = new string[1];
	private string[] TC5 = new string[1];
	private string[] TC6 = new string[1];
	private string[] TC7 = new string[1];



	void Start ()
	{
		loadScripts ();
	}



	void Update ()
	{
		// For testing (delete after)
		if (Input.GetKeyDown (KeyCode.T)) {
			StartCoroutine ("setPrompterText", "TB1");
		}
	}

	//+++++++++++++++++++++++++ Private functions +++++++++++++++++++++++++
	
	// Useless until the "www" is fixed (but worked in editor)
	private IEnumerator LoadText (string fileName)
	{
		// Handle any problems that might arise when reading the text
		//	try
		//	{
		string line;

		// Text file path
		/**
			#if UNITY_EDITOR
			filePath = Application.streamingAssetsPath + "/Texts/Prompter Subtitles/" + fileName + ".txt"; 
			
			#elif UNITY_ANDROID 
			filePath = Application.streamingAssetsPath + "/Texts/Prompter Subtitles/" + fileName;
			
			WWW loader = new WWW(filePath);
			yield return loader;
			#endif
			**/
			
		// Create a new StreamReader, tell it which file to read and what encoding the file was saved as
		StreamReader theReader = new StreamReader (filePath, Encoding.Default);
			
		// Immediately clean up the reader after this block of code is done.
		using (theReader) {
			int i = 0;

			// While there's lines left in the text file, do this:
			do {
				line = theReader.ReadLine ();

				if (line != null) {
					allLines [i] = line;
					i++;
				}
			} while (line != null);
				
			// Done reading, close the reader and return true to broadcast success    
			theReader.Close ();
		}
		//	}


		/**	
	 	catch (Exception e)
		{
			debugScript.AddMessage("Error : "+ e.Message);
		}
		**/

		yield return null;
	}

	//+++++++++++++++++++++++++ Public gestion of the 'Subtitles GUI Text' +++++++++++++++++++++++++

	// Set the prompter text
	public IEnumerator setPrompterText (string aTextName)
	{
		// Get the text
		//LoadText(aTextName);
		//StartCoroutine ("LoadText", aTextName);
		String[] myLines = new string[10];	
		myLines = getScript (aTextName);

		// Add lines to the prompter then wait before adding new ones
		foreach (string aLine in myLines) {
			try {
				prompterText.text = aLine;
			} catch (Exception e) {
				debugScript.AddMessage ("Error : " + e.Message);
			}
			if (skipText == false) {
				yield return new WaitForSeconds (defaultTimeBetweenTexts);
			}

			// Clear the lines stocked and prompter text
			//allLines[i] = null; i++;
			clearSubtitles ();
		}

		skipText = false;
	}


	// Toggle the "skip text" function to skip the lines and end the actual text
	public void toggleSkipText ()
	{
		if (skipText == true) {
			skipText = false;
		} else {
			skipText = true;
		}
	}


	// Clear subtitles
	public void clearSubtitles ()
	{
		prompterText.text = " ";
	}


	// Toggle alpha of subtitles between 0% and 100%
	public void toggleSubtitles ()
	{
		if (prompterText.color.a == 0f) {
			tempColor = prompterText.color;
			tempColor.a = 1f;
			prompterText.color = tempColor;
		} else {
			tempColor = prompterText.color;
			tempColor.a = 0f;
			prompterText.color = tempColor;
		}
	}

	//+++++++++++++++++++++++++++++ Prompter Scripts +++++++++++++++++++++++++++++++++
	private void loadScripts ()
	{
		//// TB 
		TB1 [0] = "Dimanche 14 Avril 1867 !";
		TB1 [1] = "Encadré par deux grands immeubles, voici le Théâtre des Variétés. Édifié par Jacques Cellerier et Jean-Antoine Alavoine,";
		TB1 [2] = "ce bâtiment de style néo classique a entendu les voix de Virginie Déjazet ou Hortense Schneider, Odry, Lemaître ou Bouffé… ";
		TB1 [3] = "et la musique de Jacques Offenbach.";
		TB1 [4] = "C'est aux Variétés, que sous le second Empire triomphent les opéras-bouffes d’Offenbach. ";
		TB1 [5] = "Désormais, opérettes, vaudevilles et comédies s’y partagent l’affiche !";

		TB3 [0] = "C'est dans cette salle que Jacques Offenbach fait sensation à l'occasion de la seconde Exposition Universelle en créant La Grande Duchesse de Gérolstein.";

		TB4 [0] = "En mettant en musique les caprices amoureux de la Grande Duchesse du pays imaginaire de Gérolstein,";
		TB4 [1] = "Offenbach, Meilac et Halévy racontent l'éducation sentimentale d'une souveraine encore adolescente.";

		TB5 [0] = "Non seulement Offenbach et ses librettistes proposent au Théâtre des Variétés leur dernière création sulfureuse, mais à quelques rues de là,";
		TB5 [1] = "les amateurs d’Opéra-Bouffe peuvent aussi découvrir leur Vie Parisienne qui, depuis 1866, fait les beaux soirs d'une autre salle fameuse, le Théâtre du Palais Royal.";

		TB6 [0] = "Ce 14 avril 1867 des spectateurs venus du monde entier, pour visiter l'Exposition Universelle, se bousculent sur le trottoir du Théâtre des Variétés afin d'assister à la première de la Grande Duchesse de Gérolstein.";
		TB6 [1] = "Cette nouvelle farce de Jacques Offenbach et de ses deux librettistes Henri Meilhac et Ludocic Halévy va se jouer sous vos yeux. Touchez l'affiche ! ";

		TB7 [0] = "Voici LE modèle du genre opéra bouffe (un type d’opéra traitant d’un sujet comme ici comique ou léger).";
		TB7 [1] = "Retrouvez les personnages de cette intrigue où pouvoir et amour mènent le bal en chantant.";


		//// TC
		TC1 [0] = "La Grande-Duchesse de Gérolstein est un spectacle corrosif qui ne dut qu'à la renommée de Jacques Offenbach de ne pas tomber sous le coup de la censure de Napoléon III.";

		TC2 [0] = "A vous de remonter la pièce ! Retrouvez les 3 rôles principaux !";

		TC3 [0] = "Amoureuse en secret de Fritz, la jeune Grande-duchesse le nomme général en chef de l’armée de Gérolstein au grand dam de son ministre de la Guerre, le Général Boum.";
		TC3 [1] = "Au rythme du « Dites lui ! » chanté par la grande Hortense Schneider nous vivrons les tribulations de ce « héros malgré lui » qui mène ces troupes de pacotille à la victoire.";
		TC3 [2] = "Sourd à l'amour que lui porte sa souveraine - occupé à admirer la jeune Wanda et à ridiculiser le général Boum et son armée qui vibrent à l’évocation du « Sabre de papa ! »";
		TC3 [3] = "Fritz sauvera sa tête en redevenant simple soldat et en épousant Wanda.";

		TC4 [0] = "Retrouvez le soldat Fritz (joué par José Dupuis) !";

		TC5 [0] = "Retrouvez le général Boum (joué par Henri Couder) !";

		TC6 [0] = "Retrouvez la Grande-duchesse (joué par Hortense Schneider) !";

		TC7 [0] = "Vous avez découvert les trois rôles principaux de cette pièce ! Vous pouvez continuer votre aventure en choisissant un guide parmi ces trois acteurs.";

	}

	private string[] getScript (string scriptName)
	{
		if (scriptName == "TB1") {
			return TB1;
		} // TB2 is inside TB1
		else if (scriptName == "TB3") {
			return TB3;
		} else if (scriptName == "TB4") {
			return TB4;
		} else if (scriptName == "TB5") {
			return TB5;
		} else if (scriptName == "TB6") {
			return TB6;
		} else if (scriptName == "TB7") {
			return TB7;
		} else if (scriptName == "TC1") {
			return TC1;
		} else if (scriptName == "TC2") {
			return TC2;
		} else if (scriptName == "TC3") {
			return TC3;
		} else if (scriptName == "TC4") {
			return TC4;
		} else if (scriptName == "TC5") {
			return TC5;
		} else if (scriptName == "TC6") {
			return TC6;
		} else if (scriptName == "TC7") {
			return TC7;
		} else {
			return null;
		}
	}
	// Please, if you read this, just fix the "www" way to properly get the txt files for android
	// that I couldn't get to work in time :/
}






﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using TMPro;

public class Editor : MonoBehaviour
{

    public GameMode gm;

    public enum ClickPosition { TopLeft, TopRight, BotLeft, BotRight };

	private string _previousScene = "Menu";

    // UI
    [SerializeField]
    private List<GameObject> hiddenPanelAtStart;
    [SerializeField]
    private GameObject resultPanel;
    [SerializeField]
    private GameObject resultHeader;
    [SerializeField]
    private GameObject textHeader;
    [SerializeField]
    private GameObject gatesMenu;
    [SerializeField]
    private GameObject settingsPanel;

    //BWaaba
    [SerializeField]
    private GameObject gateMenu;
    [SerializeField]
    private GameObject customGateMenu;
    [SerializeField]
    private GameObject textCurrentCircuit;

    public GameObject background;

    public Camera cam;

    public GameObject GateNameInputPanel;
    public InputField GateNameInputField;

    [SerializeField]
    private GameObject sessionNameInputPanel;

    public InputField chooseSessionNameInput;


    public GameObject gridGame;

    public GameObject actionsGatePanel;
    public GameObject actionsGridPanel;


    public GameObject whiteBall;
    public GameObject blackBall;
	public TextMeshProUGUI Texte;
	public GameObject tip1;
	public GameObject tip2;

    private EditorState _currentState;
    public EditorState CurrentState
    {
        set
        {
            _currentState?.OnExit();
            _currentState = value;
            _currentState.OnEnter();
        }
        get
        {
            return _currentState;
        }
    }

    private List<QCS.Circuit> circuits;

    private int _currentCircuitIndex;
    public int CurrentCircuitIndex
    {
        set
        {
            _currentCircuitIndex = value;
            textCurrentCircuit.GetComponent<Text>().text = _currentCircuitIndex.ToString();
            gridBoard.LoadCircuit(currentCircuit);
        }
        get { return _currentCircuitIndex; }
    }
    public QCS.Circuit currentCircuit
    {
        get { return circuits[CurrentCircuitIndex]; }
    }

    /// <summary>
    ///  Liste des portes par défaut que peux avoir l'utilisateur
    /// </summary>
	private Dictionary<string, QCS.Gate> _defaultGates;

    /// <summary>
    ///  Liste des portes créé par l'utilisateur
    /// </summary>
	private Dictionary<string, QCS.Gate> _customGates;

    /// <summary>
    ///  Facade permettant la gestion de la grille de jeux de la scène Unity
    /// </summary>
    public GridBoard gridBoard;

    void Start()
    {
        HidePanels();

        

        _defaultGates = new Dictionary<string, QCS.Gate>();
        _customGates = new Dictionary<string, QCS.Gate>();

        GameMode.gates.ForEach(AddDefaultGate);
		GameMode.customGates.ForEach(AddCustomGateToMenu);

		circuits = GameMode.circuits;

        _currentCircuitIndex = 0;

        gridBoard = new GridBoard(gridGame, currentCircuit);
        gridBoard.PositionInitialCamera();

        
        if(Input.touchSupported)
            CurrentState = new Smartphone_States.DefaultState(this);
        else
            CurrentState = new PC_States.DefaultState(this);
        
        inputController = new InputController(this);
    }

    private InputController inputController;

    void Update()
    {
        if (CurrentState == null)
            return;

        inputController.Update();
    }

    /**
	 * ###############################################################################################
	 * ###############################################################################################
	 * ###############################################################################################
	 * ############################################# UI ##############################################
	 * ###############################################################################################
	 * ###############################################################################################
	 * ###############################################################################################
	 */
    public void SetresultPanelScale(Vector3 scale)
    {
        resultPanel.transform.localScale = scale;
    }
 
    private void HidePanels()
    {
        foreach (GameObject panel in hiddenPanelAtStart)
            panel.SetActive(false);
    }

    
	/*
	 * 
██╗     ███████╗███████╗     ██████╗ █████╗ ██╗     ██╗     ██████╗  █████╗  ██████╗██╗  ██╗███████╗
██║     ██╔════╝██╔════╝    ██╔════╝██╔══██╗██║     ██║     ██╔══██╗██╔══██╗██╔════╝██║ ██╔╝██╔════╝
██║     █████╗  ███████╗    ██║     ███████║██║     ██║     ██████╔╝███████║██║     █████╔╝ ███████╗
██║     ██╔══╝  ╚════██║    ██║     ██╔══██║██║     ██║     ██╔══██╗██╔══██║██║     ██╔═██╗ ╚════██║
███████╗███████╗███████║    ╚██████╗██║  ██║███████╗███████╗██████╔╝██║  ██║╚██████╗██║  ██╗███████║
╚══════╝╚══════╝╚══════╝     ╚═════╝╚═╝  ╚═╝╚══════╝╚══════╝╚═════╝ ╚═╝  ╚═╝ ╚═════╝╚═╝  ╚═╝╚══════╝
                                                                                                    

██╗██╗     ███████╗    ███████╗ ██████╗ ███╗   ██╗████████╗     ██████╗ ██╗   ██╗    ██████╗ 
██║██║     ██╔════╝    ██╔════╝██╔═══██╗████╗  ██║╚══██╔══╝    ██╔═══██╗██║   ██║    ╚════██╗
██║██║     ███████╗    ███████╗██║   ██║██╔██╗ ██║   ██║       ██║   ██║██║   ██║      ▄███╔╝
██║██║     ╚════██║    ╚════██║██║   ██║██║╚██╗██║   ██║       ██║   ██║██║   ██║      ▀▀══╝ 
██║███████╗███████║    ███████║╚██████╔╝██║ ╚████║   ██║       ╚██████╔╝╚██████╔╝      ██╗   
╚═╝╚══════╝╚══════╝    ╚══════╝ ╚═════╝ ╚═╝  ╚═══╝   ╚═╝        ╚═════╝  ╚═════╝       ╚═╝   
                                                                                             
*/

	// Display du Result panel
    public void ShowResultPanel(bool active)
    {
        resultPanel.SetActive(active);
    }



	public void SetTipsDown()
	{
		tip1.SetActive (false);
		tip2.SetActive (false);
	}

	public void SetTipsUp()
	{
		tip1.SetActive (true);
		tip2.SetActive (true);
	}


    public void SetResultHeader(string header)
    {
        textHeader.GetComponent<Text>().text = header;
    }

	// TEXT MESH PRO
    public void SetResultText(string text)
    {
        //resultHeader.GetComponent<Text>().text = text;
		Texte.GetComponent<TextMeshProUGUI>().text = text;
		SetTipsDown ();
    }

	// Dimension du text final
	public void SetResultSize(int size)
	{
		resultHeader.GetComponent<Text>().fontSize = size;
	}

	public int TuyauxLiee (){

		int NbRow = circuits[CurrentCircuitIndex].NbRow;
		int NbCol = circuits[CurrentCircuitIndex].NbCol;

		//Debug.Log (" Nombre de Col : "+NbCol+" ___ Nombre de Row :"+NbRow);

		int ret = 18283;

		int deux=0;
		int trois=0;


		for (int i = 0; i < NbRow; i++) {
			for (int j = 0; j < NbCol; j += circuits[CurrentCircuitIndex].rows[i][j].gate.NbEntries){


				//Debug.Log (" Nom de la porte : "+circuits [CurrentCircuitIndex].rows [i] [j].gate.Name);
				//Debug.Log (" j = "+ j);

				if (!(circuits [CurrentCircuitIndex].rows[i][j].gate.Name.Equals ("•")) &&
					!(circuits [CurrentCircuitIndex].rows[i][j].gate.Name.Equals ("1")) &&
					!(circuits [CurrentCircuitIndex].rows[i][j].gate.Name.Equals ("X")) &&
					!(circuits [CurrentCircuitIndex].rows[i][j].gate.Name.Equals ("H"))) {

					if (circuits [CurrentCircuitIndex].rows [i] [j].gate.NbEntries == 3) {
						deux = 1;
						trois = 1;
					} else {
						if (j == 0)
							deux = 1;
						if (j == 1)
							trois = 1;
					}
				}

			}	
		}

		if (deux == 1 && trois == 1)
			return 123;
		if (deux == 1 )
			return 1283;
		if (trois == 1)
			return 1823;

		return ret;
	}



	public int TuyauSel(float pos)
	{
		int TuyauSelectionne = 0 ;


		if(pos < 400)
			TuyauSelectionne=1;
		
		if(pos == 400)
			TuyauSelectionne=2;
		
		if(pos > 400)
			TuyauSelectionne=3;

		return TuyauSelectionne;

		Debug.Log ("TuyauSelectionne = "+TuyauSelectionne );
	}


	public string BruteForce(string res)
	{
		string output1 = res.Replace ("|000>", "| <sprite=1><sprite=1><sprite=1>>");
		string output2 = output1.Replace ("|001>", "| <sprite=1><sprite=1><sprite=0>>");
		string output3 = output2.Replace ("|010>", "| <sprite=1><sprite=0><sprite=1>>");
		string output4 = output3.Replace ("|011>", "| <sprite=1><sprite=0><sprite=0>>");
		string output5 = output4.Replace ("|100>", "| <sprite=0><sprite=1><sprite=1>>");
		string output6 = output5.Replace ("|101>", "| <sprite=0><sprite=1><sprite=0>>");
		string output7 = output6.Replace ("|110>", "| <sprite=0><sprite=0><sprite=1>>");
		string output8 = output7.Replace ("|111>", "| <sprite=0><sprite=0><sprite=0>>");
		string output9 = output8.Replace ("|0>", "|<sprite=1>>");
		string output10 = output9.Replace ("|1>", "|<sprite=0>>");
		string output11 = output10.Replace ("|00>", "|<sprite=1><sprite=1>>");
		string output12 = output11.Replace ("|01>", "|<sprite=1><sprite=0>>");
		string output13 = output12.Replace ("|10>", "|<sprite=0><sprite=1>>");
		string output14 = output13.Replace ("|11>", "|<sprite=0><sprite=0>>");
		//string output15 = output14.Replace ("|000>", "|<sprite=0><sprite=0><sprite=0>");
		return output14;
	}

	// enumération des cas
	public int isRelated(int res){
		if(res == 18283){
			return 1;
		}

		if(res == 1283){
			return 2;
		}

		if(res == 1823){
			return 3;
		}

		if(res == 123){
			return 4;
		}

		if(res == 182){
			return 5;
		}

		if(res == 12){
			return 6;
		}
		return 0;

	}

	// Fonction qui prends en arg le num du tuyau sur lequel on a cliqué et qui donne les relations de liaisons des tuyaux
	public int Choice(int numTuyau, int related){
		
		if( related == 1 ){
			return numTuyau;
		}

		if( (related == 2) && ( (numTuyau == 1) || (numTuyau == 2)) ){
			return 12;
		}

		if( (related == 2) &&  (numTuyau == 3) ){
			return 3;
		}

		if( (related == 1) &&  (numTuyau == 2) ){
			return 2;
		}

		if( (related == 3) && (numTuyau == 1) ){
			return 1;
		}

		if( (related == 3) && ( (numTuyau == 2) || (numTuyau == 3)) ){
			return 23;
		}


		if(related == 4){
			return 123;
		}

		if( related == 5 ){
			return numTuyau;
		}


		if(related == 6){
			return 12;
		}
		return 0;
	}



	// Fonction qui applique le choix sur la string normale il faut ensuite envoyer la chaine dans la fct BruteForce
	public string ApplyingChoiceOnRes(string resNormal, int choix){
		if(choix == 12){
			string modif1 = resNormal.Replace ("|000>", "|00>");
			string modif2 = modif1.Replace ("|001>", "|00>");
			string modif3 = modif2.Replace ("|010>", "|01>");
			string modif4 = modif3.Replace ("|011>", "|01>");
			string modif5 = modif4.Replace ("|100>", "|10>");
			string modif6 = modif5.Replace ("|101>", "|10>");
			string modif7 = modif6.Replace ("|110>", "|11>");
			string modif8 = modif7.Replace ("|111>", "|11>");
			return modif8;
		}

		if(choix == 2){
			string modif1 = resNormal.Replace ("|000>", "|0>");
			string modif2 = modif1.Replace ("|001>", "|0>");
			string modif3 = modif2.Replace ("|010>", "|1>");
			string modif4 = modif3.Replace ("|011>", "|1>");
			string modif5 = modif4.Replace ("|100>", "|0>");
			string modif6 = modif5.Replace ("|101>", "|0>");
			string modif7 = modif6.Replace ("|110>", "|1>");
			string modif8 = modif7.Replace ("|111>", "|1>");
			return modif8;
		}

		if(choix == 1){
			string modif1 = resNormal.Replace ("|000>", "|0>");
			string modif2 = modif1.Replace ("|001>", "|0>");
			string modif3 = modif2.Replace ("|010>", "|0>");
			string modif4 = modif3.Replace ("|011>", "|0>");
			string modif5 = modif4.Replace ("|100>", "|1>");
			string modif6 = modif5.Replace ("|101>", "|1>");
			string modif7 = modif6.Replace ("|110>", "|1>");
			string modif8 = modif7.Replace ("|111>", "|1>");
			return modif8;
		}

		if(choix == 3){
			string modif1 = resNormal.Replace ("|000>", "|0>");
			string modif2 = modif1.Replace ("|001>", "|1>");
			string modif3 = modif2.Replace ("|010>", "|0>");
			string modif4 = modif3.Replace ("|011>", "|1>");
			string modif5 = modif4.Replace ("|100>", "|0>");
			string modif6 = modif5.Replace ("|101>", "|1>");
			string modif7 = modif6.Replace ("|110>", "|0>");
			string modif8 = modif7.Replace ("|111>", "|1>");
			return modif8;
		}

		if(choix == 123){
			return resNormal;
		}

		if(choix == 23){
			string modif1 = resNormal.Replace ("|000>", "|00>");
			string modif2 = modif1.Replace ("|001>", "|01>");
			string modif3 = modif2.Replace ("|010>", "|10>");
			string modif4 = modif3.Replace ("|011>", "|11>");
			string modif5 = modif4.Replace ("|100>", "|00>");
			string modif6 = modif5.Replace ("|101>", "|01>");
			string modif7 = modif6.Replace ("|110>", "|10>");
			string modif8 = modif7.Replace ("|111>", "|11>");
			return modif8;
		}
		return " ";

	}





    // Setting panel

    public void ShowSettingsPanel(bool active)
    {
        settingsPanel.SetActive(active);
    }

    public void ShowSessionNameInputPanel(bool active)
    {
        sessionNameInputPanel.SetActive(active);
    }


    // Gate panel

    public void ShowGatesMenuPanel(bool active)
    {
		if (active) {
			gatesMenu.GetComponent<OpenCloseMenu> ().Open ();
			SetTipsDown ();
		} 

		else {
			gatesMenu.GetComponent<OpenCloseMenu> ().Close ();
			SetTipsUp ();
		}
    }

    public void AddDefaultGate(QCS.Gate gate)
    {
        _defaultGates.Add(gate.Name, gate);
        MakeGateButton(gate);
    }

    public void AddCustomGateToMenu(QCS.Gate gate)
    {
        if (!_customGates.ContainsKey(gate.Name))
        {
            _customGates.Add(gate.Name, gate);
            MakeCustomGateButton(gate);
        }
        else
        {
            _customGates.Remove(gate.Name);
            _customGates.Add(gate.Name, gate);
        }
    }

    public void MakeGateButton(QCS.Gate gate)
    {
        GameObject gateButton = Instantiate(
            Resources.Load("02_prefabs/BtnGate", typeof(GameObject)),
            gateMenu.transform) as GameObject;

        gateButton.name = gate.Name;

        Button buttonComponent = gateButton.GetComponent<Button>();

        buttonComponent.onClick.AddListener(delegate
        {
            CurrentState.OnSelectGate(gate);
        });

        gateButton.GetComponent<Text>().text = gate.Name;

        RectTransform rectTransform = gateButton.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(
            71.5f,
            (_defaultGates.Count) * -60,
            0);
    }

    public void MakeCustomGateButton(QCS.Gate gate)
    {
        GameObject gateButton = Instantiate(
            Resources.Load("02_prefabs/BtnGate", typeof(GameObject)),
            customGateMenu.transform) as GameObject;

        gateButton.name = gate.Name;

        Button buttonComponent = gateButton.GetComponent<Button>();

        buttonComponent.onClick.AddListener(delegate
        {
            CurrentState.OnSelectGate(_customGates[gate.Name]);
        });

        gateButton.GetComponent<Text>().text = gate.Name;

        RectTransform rectTransform = gateButton.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(
            71.5f,
            (_customGates.Count) * -60,
            0);
    }

    // Action gate/grid menu

    public void ShowActionsGatePanel(bool active)
    {
        actionsGatePanel.SetActive(active);
    }

    public void SetActionGatePanelPosition(Vector3 position)
    {
        actionsGatePanel.transform.position = position;
    }

    public void ShowActionsGridPanel(bool active)
    {
        actionsGridPanel.SetActive(active);
    }

    
    public void SetActionGridPanelPosition(Vector3 position)
    {
        actionsGridPanel.transform.position = position;
    }

    public Vector3 GetGridBoardPosition(Vector3 screenPosition)
    {
        Collider collider = background.GetComponent<Collider>();
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (collider.Raycast(ray, out hit, 1000))
            return new Vector3(hit.point.x / 10, GridBoard.gateThikness, hit.point.y / 10);

        throw new Exception("Raycast entre Background et Camera échoué !");
    }
    
    public GameObject GetOnScreenObject(Vector3 onScreenPosition)
    {
        GameObject gameObject = null;
        RaycastHit hitInfo;
        Ray ray = Camera.main.ScreenPointToRay(onScreenPosition);
        if (Physics.Raycast(ray, out hitInfo))
            gameObject = hitInfo.collider.gameObject;

        if (gameObject == null)
            return null;

        return gameObject;
    }

    public Tuple<int, int, Editor.ClickPosition> GetOnScreenGridPosition(Vector3 onScreenPosition)
    {
        GameObject gameObject = null;
        RaycastHit hitInfo;
        Ray ray = cam.ScreenPointToRay(onScreenPosition);
        if (Physics.Raycast(ray, out hitInfo))
            gameObject = hitInfo.collider.gameObject;

        if (gameObject == null)
            return null;
        if (gameObject.tag != "pipe" && gameObject.tag != "gate")
            return null;

        GateObject gateObject = gameObject.GetComponent<GateObject>();
        QCS.Circuit.GateStruct gateStruct = gateObject.gateStruct;

        int row = gateStruct.row;
        int col = gateStruct.col;
        int nb_entries = gateStruct.gate.NbEntries;

        Editor.ClickPosition position;
        Vector3 posOnCase = hitInfo.point - hitInfo.collider.transform.position;

        // if the gate has many entries we need to do some modulos ..
        if (nb_entries > 1)
        {
            posOnCase.x += (nb_entries / 2f) * GridBoard.realColumnWidth;
            col += (int)(posOnCase.x / GridBoard.realColumnWidth);
            posOnCase.x %= GridBoard.realColumnWidth;
            posOnCase.x -= GridBoard.realColumnWidth / 2;
        }

        if (posOnCase.y > 0)
            if (posOnCase.x < 0)
                position = Editor.ClickPosition.TopLeft;
            else
                position = Editor.ClickPosition.TopRight;
        else
            if (posOnCase.x < 0)
            position = Editor.ClickPosition.BotLeft;
        else
            position = Editor.ClickPosition.BotRight;

        return Tuple.Create(row, col, position);
    }
    
    public void BackToPreviousScene()
    {
        SceneManager.LoadScene(_previousScene);
    }
    
    public void PreviousCircuit()
    {
        if (CurrentCircuitIndex == 0)
            return;
        CurrentCircuitIndex--;
    }

    public void NextCircuit()
    {
        if (CurrentCircuitIndex == circuits.Count - 1)
            circuits.Add(GameMode.BaseCircuit());
        CurrentCircuitIndex++;
    }

	public void DeleteCurrentCircuit()
	{
		if (circuits.Count == 1)
			return;

		circuits.RemoveAt (CurrentCircuitIndex);
		gridBoard.LoadCircuit(currentCircuit);
	}

	/**
	 * ###############################################################################################
	 * ###############################################################################################
	 * ###############################################################################################
	 * ######################################### GETTERS #############################################
	 * ###############################################################################################
	 * ###############################################################################################
	 * ###############################################################################################
	 */

	public List<QCS.Circuit> GetCircuits()
	{
		return circuits;
	}

	public Dictionary<string, QCS.Gate> GetCustomGates()
	{
		return _customGates;
	}
}


/*    public GameObject CreateWhiteBall(float x , float y , float a)
    {

        GameObject wBall = Instantiate(whiteBall);

        wBall.transform.SetParent(resultPanel.transform);
        
        wBall.transform.localPosition =new Vector3(x, y, 0);
        wBall.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f) * a;
        wBall.SetActive(true);
        //wBall.transform.SetParent(resultPanel.transform);
        return wBall;
    }


    public GameObject CreateBlackBall(float x, float y, float a)
    {
        GameObject bBall = Instantiate(blackBall);
        bBall.transform.SetParent(resultPanel.transform);
        bBall.SetActive(true);
        bBall.transform.localPosition= new Vector3(x, y, 0);
        bBall.transform.localScale = new Vector3 ( 1.0f , 1.0f, 1.0f )* a;
        
        
        return bBall;
    }
*/
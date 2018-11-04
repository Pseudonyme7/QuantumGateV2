using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EasyCampaign : MonoBehaviour {


	public void OnClick()
	{
		List<QCS.Circuit> circuits = new List<QCS.Circuit>
		{
			new QCS.Circuit(10, 1)
		};

		GameMode.nameGame = "";
		GameMode.circuits = circuits;
		GameMode.gates = new List<QCS.Gate>() { QCS.Gate.NOT, QCS.Gate.CONTROL};
		GameMode.customGates = new List<QCS.Gate>();

		SceneManager.LoadScene("SandBox 1", LoadSceneMode.Single);
	}
}

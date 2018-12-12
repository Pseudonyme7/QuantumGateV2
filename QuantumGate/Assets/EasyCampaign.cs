using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class EasyCampaign : MonoBehaviour {


	public void OnClick()
	{
		SceneManager.LoadScene("SelectLevels", LoadSceneMode.Single);
	}
}
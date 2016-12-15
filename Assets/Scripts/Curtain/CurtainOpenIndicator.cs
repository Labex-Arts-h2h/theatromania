using UnityEngine;
using System.Collections;

public class CurtainOpenIndicator : MonoBehaviour
{

	private bool isOpened;

	void Start ()
	{
		isOpened = false;
	}

	public void SetCurtainOpened ()
	{
		isOpened = true;
	}

	public bool IsCurtainOpened ()
	{
		return isOpened;
	}
}

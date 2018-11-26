using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ####################################################################################
public class DayNightCycle : MonoBehaviour {

	public		float			time	=	1;
	private		float			cycle;

	public		Text			stats;

	private		string			versionTitle	=	"Building Blocks v";
	public		string			versionData		=	"1.0";
	private		string			dayTitle		=	"Day: ";
	public		string			dayData			=	"1";
	private		string			timeTitle		=	"Time: ";
	public		string			timeData		=	"00:00";

	// --------------------------------------------------------------------------------
	void Start () {
		cycle = (float) (0.1 / time * -1);
	}
	
	// --------------------------------------------------------------------------------
	void Update () {
		transform.Rotate(0, cycle, 0);
		//transform.Rotate(0, 0, cycle, Space.World);

		float	timeAll			=	transform.rotation.eulerAngles.y;
		float	timeHourF		=	timeAll / (360/24);
		int		timeHour		=	(int) timeHourF;
		int		timeMinut		=	(int) ((timeHourF - (int) timeHourF) * 60);	

		if (timeHour == 0 && timeMinut == 0) { dayData = (int.Parse(dayData)+1).ToString(); } 

		string	stringHour		=	timeHour.ToString();
		if (timeHour < 10) { stringHour = "0" + stringHour; }
		string	stringMinut		=	timeMinut.ToString();
		if (timeMinut < 10) { stringMinut = "0" + stringMinut; }
				
		timeData = stringHour + ":" + stringMinut;
		stats.text = versionTitle + versionData + "\n" + dayTitle + dayData + "\n" + timeTitle + timeData;
	}

	// --------------------------------------------------------------------------------
}
// ####################################################################################
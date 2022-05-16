using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory("DES Actions")]
	[Tooltip("Gives player control of Aoi again")]
	public class ResumePlayer : FsmStateAction
	{

		// Code that runs on entering the state.
		public override void OnEnter()
		{
			GameManager.Instance.ResumePlayer();
			Finish();
		}


	}

}

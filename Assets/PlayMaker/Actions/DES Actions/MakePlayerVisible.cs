using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory("DES Actions")]
	[Tooltip("Makes Aoi Visible")]
	public class MakePlayerVisible : FsmStateAction
	{

		// Code that runs on entering the state.
		public override void OnEnter()
		{
			GameManager.Instance.MakePlayerVisible();
			Finish();
		}


	}

}

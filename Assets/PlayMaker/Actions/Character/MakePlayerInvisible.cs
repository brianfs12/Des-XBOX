using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{

	[ActionCategory(ActionCategory.Character)]
	[Tooltip("Makes Aoi Invisible")]
	public class MakePlayerInvisible : FsmStateAction
	{

		// Code that runs on entering the state.
		public override void OnEnter()
		{
			GameManager.Instance.MakePlayerInvisible();
			Finish();
		}


	}

}

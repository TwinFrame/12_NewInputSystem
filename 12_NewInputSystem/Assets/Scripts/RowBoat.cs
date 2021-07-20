using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RowBoat : IInputInteraction
{
	public float Duration = 0.2f;

	[UnityEditor.InitializeOnLoadMethod]
	private static void Register()
	{
		InputSystem.RegisterInteraction<RowBoat>();
	}

	public void Process(ref InputInteractionContext context)
	{
		if (context.timerHasExpired)
		{
			context.Canceled();
			return;
		}

		switch (context.phase)
		{
			case InputActionPhase.Waiting:
				if (context.ReadValue<float>() == 1)
				{
					context.Started();
					context.SetTimeout(Duration);
				}
				break;
			case InputActionPhase.Started:
				if (context.ReadValue<float>() == -1)
				{
					Debug.Log("Process RowBoat");
					context.Performed();
				}
				break;
			case InputActionPhase.Performed:
				Debug.Log("Row is Done");
				break;
		}
	}

	public void Reset()
	{
	}
}

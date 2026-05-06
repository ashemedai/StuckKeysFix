using System.Runtime.InteropServices;
using BepInEx;
using UnityEngine;

[BepInPlugin("com.ashemedai.stuckkeysfix", "StuckKeysFix", "1.0.0")]
public class StuckKeysFixPlugin : BaseUnityPlugin
{
	private void Awake()
	{
		Logger.LogInfo("Steam Stuck Keys Fix loaded. Monitoring modifier key state...");

		gameObject.AddComponent<InputResetHandler>();
	}
}

public class InputResetHandler : MonoBehaviour
{
	// Win32 virtual-key codes for the modifier keys most commonly affected
	// by the Steam Overlay stuck-key bug.
	private const int VK_LSHIFT   = 0xA0;
	private const int VK_RSHIFT   = 0xA1;
	private const int VK_LCONTROL = 0xA2;
	private const int VK_RCONTROL = 0xA3;
	private const int VK_LMENU    = 0xA4;
	private const int VK_RMENU    = 0xA5;

	private static readonly KeyCode[] ModifierKeyCodes =
	{
		KeyCode.LeftShift,   KeyCode.RightShift,
		KeyCode.LeftControl, KeyCode.RightControl,
		KeyCode.LeftAlt,     KeyCode.RightAlt,
	};

	private static readonly int[] ModifierVKeys =
	{
		VK_LSHIFT,   VK_RSHIFT,
		VK_LCONTROL, VK_RCONTROL,
		VK_LMENU,    VK_RMENU,
	};

	[DllImport("user32.dll")]
	private static extern short GetAsyncKeyState(int vKey);

	private bool _wasResetting;

	private void Update()
	{
		KeyCode stuck = KeyCode.None;

		for (int i = 0; i < ModifierKeyCodes.Length; i++)
		{
			// Desync: Unity's input cache says the key is held, but Windows
			// reports it as not currently down. This is the stuck-key state
			// the Steam Overlay leaves behind.
			if (Input.GetKey(ModifierKeyCodes[i]) && !IsPhysicallyDown(ModifierVKeys[i]))
			{
				stuck = ModifierKeyCodes[i];
				break;
			}
		}

		bool resetting = stuck != KeyCode.None;

		if (resetting)
		{
			Input.ResetInputAxes();

			if (!_wasResetting)
			{
				Debug.Log($"[StuckKeysFix] Detected stuck {stuck}; reset input axes.");
			}
		}

		_wasResetting = resetting;
	}

	private static bool IsPhysicallyDown(int vKey)
	{
		// GetAsyncKeyState's high bit indicates the key is currently down.
		return (GetAsyncKeyState(vKey) & 0x8000) != 0;
	}
}

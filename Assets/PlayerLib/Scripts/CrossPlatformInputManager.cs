using System;
using UnityEngine;
using CS.Namespace.Player;

// ################################################################################
namespace CS.Namespace.Player {
	public static class CrossPlatformInputManager {

		private static VirtualInput activeInput;
		private static VirtualInput s_HardwareInput;

		// ------------------------------------------------------------------------
		static CrossPlatformInputManager() {
			s_HardwareInput = new StandaloneInput();
			activeInput = s_HardwareInput;
		}

		// ------------------------------------------------------------------------
		public static bool AxisExists(string name) { return activeInput.AxisExists(name); }
		public static void UnRegisterVirtualAxis(string name) {
			if (name == null) { throw new ArgumentNullException("name"); }
			activeInput.UnRegisterVirtualAxis(name);
		}
		public static void UnRegisterVirtualButton(string name) { activeInput.UnRegisterVirtualButton(name); }
		public static float GetAxis(string name) { return GetAxis(name, false); }
		private static float GetAxis(string name, bool raw) { return activeInput.GetAxis(name, raw); }
		public static bool GetButtonDown(string name) { return activeInput.GetButtonDown(name); }
		public static bool GetButtonUp(string name) { return activeInput.GetButtonUp(name); }

		// ------------------------------------------------------------------------
		public class VirtualAxis {
			public string name { get; private set; }
			private float m_Value;
			public bool matchWithInputManager { get; private set; }
			public VirtualAxis(string name) : this(name, true) { }
			public VirtualAxis(string name, bool matchToInputSettings) {
				this.name = name;
				matchWithInputManager = matchToInputSettings;
			}
			public void Remove() { UnRegisterVirtualAxis(name); }	
			public void Update(float value) { m_Value = value; }
			public float GetValue { get { return m_Value; } }
			public float GetValueRaw { get { return m_Value; } }
		}

		// ------------------------------------------------------------------------
		public class VirtualButton {
			public string name { get; private set; }
			public bool matchWithInputManager { get; private set; }
			private int m_LastPressedFrame = -5;
			private int m_ReleasedFrame = -5;
			private bool m_Pressed;

			public VirtualButton(string name) : this(name, true) {}
			public VirtualButton(string name, bool matchToInputSettings) {
				this.name = name;
				matchWithInputManager = matchToInputSettings;
			}
			public void Pressed() {
				if (m_Pressed) { return; } 
				m_Pressed = true;
				m_LastPressedFrame = Time.frameCount;
			}
			public void Released() {
				m_Pressed = false;
				m_ReleasedFrame = Time.frameCount;
			}
			public void Remove() { UnRegisterVirtualButton(name); }
			public bool GetButton { get { return m_Pressed; } }
			public bool GetButtonDown { get { return m_LastPressedFrame - Time.frameCount == -1; } }
			public bool GetButtonUp { get { return (m_ReleasedFrame == Time.frameCount - 1); } }
		}

		// ------------------------------------------------------------------------
	}
}
// ################################################################################
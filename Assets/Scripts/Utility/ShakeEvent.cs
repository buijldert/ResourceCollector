/*
	ShakeEvent.cs
	Created 10/2/2017 11:55:05 AM
	Project Resource Collector by Base Games
*/

namespace Utility
{
	public class ShakeEvent 
	{
        public delegate void ShakeEventAction();
        public static event ShakeEventAction OnShakeEvent;

        public static void SendShakeEvent()
        {
            if (OnShakeEvent != null)
                OnShakeEvent();
        }
	}
}
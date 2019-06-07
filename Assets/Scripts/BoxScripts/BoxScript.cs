using UnityEngine;

namespace BoxScripts
{
	public class BoxScript : MonoBehaviour {

		// Set Box as Ground Tag or Actual if there is a collion between Box and Sphere
		void Start () {
			SetTag();
		}
		protected virtual void SetTag()
		{
			tag = "Ground";
		}
		public virtual void OnCollisionExit(Collision other) {
			SetTag();
		}
		public virtual void OnCollisionEnter(Collision other)
		{
			tag = "ActualBox";
		}
	}
}

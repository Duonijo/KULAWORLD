using UnityEngine;

namespace BoxScripts
{
	public class BoxScript : MonoBehaviour {

		// Use this for initialization
		public GameObject collideObject;
		void Start () {
			SetTag();
		}
	
		// Update is called once per frame
		void Update () {
		
		}

		public virtual void SetTag()
		{
			this.tag = "Ground";
		}

		public virtual void OnCollisionExit(Collision other) {
			this.SetTag();
		}
		public virtual void OnCollisionEnter(Collision other)
		{
			collideObject = other.gameObject;
			this.tag = "ActualBox";
			// print("Sphere position : " + collideObject.transform.position);
			//throw new System.NotImplementedException();
		}
	}
}

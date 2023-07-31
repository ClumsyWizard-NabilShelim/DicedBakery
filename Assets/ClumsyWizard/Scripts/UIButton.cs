using System.Collections;
using UnityEngine;

namespace ClumsyWizard.Utilities
{
	public class UIButton : MonoBehaviour
	{
		public void Clicked()
		{
			AudioManager.PlayAudio("ButtonClick");
		}

		public void DestroyGameObject(GameObject gameObject)
        {
			Destroy(gameObject);
        }

	}
}
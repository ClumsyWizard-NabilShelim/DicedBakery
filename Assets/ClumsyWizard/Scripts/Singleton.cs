using UnityEngine;

namespace ClumsyWizard.Utilities
{
	public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		protected static T Instance { get; private set; }

		protected virtual void Awake()
		{
			if (Instance != null)
			{
				Destroy(gameObject);
				return;
			}

			Instance = this as T;
		}
	}
}
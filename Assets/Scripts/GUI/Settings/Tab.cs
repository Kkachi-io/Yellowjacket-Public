using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class Tab : MonoBehaviour, ISelectHandler
{
    #region Variables

        public event Action<Tab> IsSelected;

        [SerializeField] private CanvasGroup canvasGroup;

		[SerializeField] private ISettingsTabScreen tabScreen;

        public Vector3 TabPosition { get => this.transform.position + new Vector3(0, -30, 0); }
	    
        public CanvasGroup CanvasGroup { get => canvasGroup; }

		public ISettingsTabScreen TabScreen { get => tabScreen; }

	#endregion

	private void Awake()
	{
		tabScreen = canvasGroup.GetComponent<ISettingsTabScreen>();
	}

	public void OnSelect(BaseEventData eventData)
	{
		IsSelected?.Invoke(this);
	}






}

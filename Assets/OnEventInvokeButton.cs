using UnityEngine;
using UnityEngine.UI;

public class OnEventInvokeButton : MonoBehaviour
{
   [Header("References")]
   [SerializeField] private Button _button;

   [Header("Events")]
   [SerializeField] private MonoEvent _monoEvent;

   #region MonoBehaviour

   private void OnValidate()
   {
      _button ??= GetComponent<Button>();
   }

   private void OnEnable()
   {
      _monoEvent.onMonoCall += InvokeButton;
   }

   private void OnDisable()
   {
      _monoEvent.onMonoCall -= InvokeButton;
   }

   #endregion

   private void InvokeButton()
   {
      _button.onClick.Invoke();
   }
}

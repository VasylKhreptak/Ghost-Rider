using System;
using UnityEngine;

public class CarSpecificationSlider : UIUpdatableItem
{
   [Header("References")]
   [SerializeField] protected MainCarSpawner _mainCarSpawner;
   [SerializeField] protected SmoothSlider _smoothSlider;

   [Header("Events")]
   [SerializeField] private MonoEvent _updateEvent;

   #region MonoBehaviour

   private void OnValidate()
   {
      _mainCarSpawner ??= FindObjectOfType<MainCarSpawner>();
      _smoothSlider ??= GetComponent<SmoothSlider>();
      _updateEvent ??= GetComponent<MonoEvent>();
   }

   #endregion
   
   private void OnEnable()
   {
      _updateEvent.onMonoCall += UpdateValue;
   }

   private void OnDisable()
   {
      _updateEvent.onMonoCall -= UpdateValue;
   }

   public override void UpdateValue()
   {
      throw new NotImplementedException();
   }
}

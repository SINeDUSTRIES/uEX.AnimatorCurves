using UnityEngine;

namespace SINeDUSTRIES.Unity
{
  /// <summary>
  /// <see cref="AAnimatorCurve{TProperty}"/> which targets a <see cref="RectTransform"/>;
  /// </summary>
  public abstract class AAnimatorCurveRectTransform<TProperty> : AAnimatorCurve<TProperty>
  {
    #region Properties/Public

    /// <summary>
    /// Animated component;
    /// </summary>
    public RectTransform Target
    {
      get
      {
        return this.target;
      }

      set
      {
        this.target = value;
      }
    }

    #endregion

    #region Private/Fields

    [SerializeField] private RectTransform target;

    #endregion
  }
}
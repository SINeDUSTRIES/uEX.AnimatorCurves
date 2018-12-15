using System;

using UnityEngine;

namespace SINeDUSTRIES.Unity
{
  /// <summary>
  /// <see cref="AAnimatorCurve"/> which modifies a <see cref="RectTransform.anchoredPosition"/>;
  /// </summary>
  public class AnimatorCurveRectTransformAnchoredPositionX : AAnimatorCurveRectTransform<Single>
  {
    #region Protected/Methods

    /// <summary>
    /// Concrete <see cref="AAnimatorCurve{TProperty}.propertyGet()"/>;
    /// </summary>
    protected override Single propertyGet()
    => this.Target.anchoredPosition.x;

    /// <summary>
    /// Concrete <see cref="AAnimatorCurve{TProperty}.propertySet(float)"/>;
    /// </summary>
    protected override void propertySet(Single value)
    {
      Vector2 anchoredPosition = this.Target.anchoredPosition;
      anchoredPosition.x = value;
      this.Target.anchoredPosition = anchoredPosition;
    }

    #endregion

    #region Protected/Properties

    /// <summary>
    /// Concrete <see cref="AAnimatorCurve{TProperty}.delta"/>;
    /// </summary>
    protected override Single delta
    => Time.deltaTime;

    #endregion
  }
}

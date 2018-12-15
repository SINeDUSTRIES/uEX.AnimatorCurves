using System;

using UnityEngine;

namespace SINeDUSTRIES.Unity
{
  /// <summary>
  /// <see cref="AAnimatorCurve{TProperty}"/> which modifies a <see cref="RectTransform.sizeDelta"/>;
  /// </summary>
  public class AnimatorCurveRectTransformSizeDeltaX : AAnimatorCurveRectTransform<Single>
  {
    #region Protected/Methods

    /// <summary>
    /// Concrete <see cref="AAnimatorCurve{TProperty}.propertyGet()"/>;
    /// </summary>
    protected override Single propertyGet()
    => this.Target.sizeDelta.x;

    /// <summary>
    /// Concrete <see cref="AAnimatorCurve{TProperty}.propertySet(float)"/>;
    /// </summary>
    override protected void propertySet(Single value)
    {
      Vector2 sizeDelta = this.Target.sizeDelta;
      sizeDelta.x = value;
      this.Target.sizeDelta = sizeDelta;
    }

    #endregion
  }
}

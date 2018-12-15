using UnityEngine;

namespace SINeDUSTRIES.Unity
{
  /// <summary>
  /// <see cref="AAnimatorCurve{TProperty}"/> for <see cref="Transform.localScale"/>;
  /// </summary>
  public class AnimatorCurveTransformLocalScale : AAnimatorCurve<Vector3>
  {
    #region Protected/Properties

    protected override Vector3 propertyGet()
    => this.target.localScale;

    protected override void propertySet(System.Single value)
    => this.target.localScale = new Vector3(value, value, value);

    #endregion

    #region Private/Fields/Insepctor

    [SerializeField] private Transform target;

    #endregion
  }
}

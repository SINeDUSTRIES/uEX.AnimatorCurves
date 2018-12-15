using System;

using UnityEngine.Events;

namespace SINeDUSTRIES.Unity
{
  /// <summary>
  /// Animate a single property given a <see cref="AnimationCurve"/>;
  /// </summary>
  public interface IPlayableAnimatorCurve
  {
    #region Methods

    /// <summary>
    /// Start increasing the time/ propertyl
    /// </summary>
    Boolean PlayTry(Single playSpeed);

    /// <summary>
    /// Start decreasing the time/ property;
    /// </summary>
    Boolean StopTry();

    #endregion

    #region Public/Properties

    Boolean IsPlaying { get; }

    Single PlaySpeed { get; set; }

    Boolean IsTimeMax { get; }

    Boolean IsTimeMin { get; }

    UnityEvent OnTimeMax { get; }

    UnityEvent OnTimeMin { get; }

    #endregion
  }
}

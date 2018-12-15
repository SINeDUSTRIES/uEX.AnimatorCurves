using System;
using System.Collections;

using UnityEngine;
using UnityEngine.Events;

namespace SINeDUSTRIES.Unity
{
  /// <summary>
  /// Animate a value using a <see cref="AnimationCurve"/>;
  /// </summary>
  /// <remarks>Convenient way to animate a single property without creating animation clip etc.;</remarks>
  public abstract class AAnimatorCurve<TProperty> : MonoBehaviour, IPlayableAnimatorCurve
  {
    // TODO: make this a playable..?
    // TODO: mirror the API of legacy animator

    #region Lifecycle

    /// <summary>
    /// MonoBehaviour message.
    /// </summary>
    virtual protected void Start()
    {
      this.determineMaxTimeTry();

      this.setTimeAndProperty(this.timeCurrent);
    }

    #endregion

    #region Methods/Public

    /// <summary>
    /// Try to stop playing.
    /// Fails if was already playing.
    /// </summary>
    public Boolean StopTry()
    {
      if (this.IsPlaying)
      {
        //Debug.LogFormat(
        //  "{0}/{1}.{2}: isPlaying",
        //  this.gameObject.name,
        //  this.GetType().Name,
        //  nameof(PlayTry)
        //);

        this.StopCoroutine(this.coroutinePlaying);
        this.coroutinePlaying = null;

        this.IsPlaying = false;

        this.PlaySpeed = 0;

        return true;
      }
      else
      {
        return false;
      }
    }

    /// <summary>
    /// Try to start playing. Fails if was not playing.
    /// </summary>
    public Boolean PlayTry(Single playSpeed)
    {
      //Debug.LogFormat(
      //  "{0}/{1}.{2}: {3}",
      //  this.gameObject.name,
      //  this.GetType().Name,
      //  nameof(PlayTry),
      //  playSpeed
      //);

      this.PlaySpeed = playSpeed;

      if (!this.IsPlaying)
      {
        this.coroutinePlaying = this.StartCoroutine(this.playing()); // start coroutine

        this.IsPlaying = true;

        return true;
      }
      else
      {
        return false;
      }
    }

    #endregion

    #region Public/Properties

    /// <summary>
    /// Play speed of the animator;
    /// </summary>
    public Single PlaySpeed { get; set; }

    /// <summary>
    /// Is the animator playing?
    /// </summary>
    public Boolean IsPlaying { get; private set; }

    /// <summary>
    /// Curve being played;
    /// </summary>
    public AnimationCurve AnimationCurve
    {
      get
      {
        return this.curve;
      }
      set
      {
        this.curve = value;
        this.determineMaxTimeTry();
      }
    }

    /// <summary>
    /// Is the current time the max time?
    /// </summary>
    public Boolean IsTimeMax
    => this.timeCurrent == this.timeMax;

    /// <summary>
    /// Is the current time the minimum time?
    /// </summary>
    public Boolean IsTimeMin
    => this.timeCurrent == 0;

    /// <summary>
    /// Invoked when the time becomes the max time.
    /// </summary>
    public UnityEvent OnTimeMax
    => this.onTimeSetMax;

    /// <summary>
    /// Invoked when the time becomes the min time.
    /// </summary>
    public UnityEvent OnTimeMin
    => this.onTimeSetMin;

    #endregion

    #region Private/Methods

    /// <summary>
    /// Set the property using value of curve;
    /// </summary>
    /// <param name="value"></param>
    abstract protected void propertySet(Single value);

    /// <summary>
    /// Get current value of the property;
    /// </summary>
    abstract protected TProperty propertyGet();

    private Int32 setTimeAndProperty(Single time)
    {
      this.timeCurrent = time;

      if (this.timeCurrent <= this.timeMin)
      {
        this.timeCurrent = this.timeMin;

        this.propertySet(this.curve.Evaluate(this.timeCurrent));

        this.OnTimeMin.Invoke();

        return -1;
      }
      else if (this.timeCurrent >= this.timeMax)
      {
        this.timeCurrent = this.timeMax;

        this.propertySet(this.curve.Evaluate(this.timeCurrent));

        this.OnTimeMax.Invoke();

        return 1;
      }
      else
      {
        this.timeCurrent = time;

        this.propertySet(this.curve.Evaluate(time));

        return 0;
      }
    }

    // shrinking

    private IEnumerator playing()
    {
      while (true)
      {
        //Debug.LogFormat(
        //  "{0}/{1}.{2}: {3}",
        //  this.gameObject.name,
        //  this.GetType().Name,
        //  nameof(playing),
        //  this.propertyGet()
        //);

        yield return null;

        this.timeCurrent = this.timeCurrent + this.delta * this.PlaySpeed;

        if (this.timeCurrent >= this.timeMax)
        {
          this.timeCurrent = this.timeMax;
          this.propertySet(this.curve.Evaluate(this.timeCurrent));

          this.StopTry();
          this.OnTimeMin.Invoke();
        }
        else if (this.timeCurrent <= this.timeMin)
        {
          this.timeCurrent = this.timeMin;
          this.propertySet(this.curve.Evaluate(this.timeCurrent));

          this.StopTry();
          this.OnTimeMin.Invoke();
        }
        else
        {
          this.propertySet(this.curve.Evaluate(this.timeCurrent));
        }
      }
    }

    private Boolean determineMaxTimeTry()
    {
      if (this.curve.keys.Length > 0)
      {
        this.timeMax = this.curve.keys.Last().time;

        return true;
      }
      else
      {
        return false;
      }
    }

    #endregion

    #region Protected/Properties

    /// <summary>
    /// Amount to change by;
    /// </summary>
    virtual protected Single delta
    => Time.deltaTime;

    #endregion

    #region Private/Fields

    private Single timeMin = 0;

    private Single timeMax;

    private Single timeCurrent = 0;

    /// <summary>
    /// Single instance of <see cref="playing"/>;
    /// </summary>
    private Coroutine coroutinePlaying;

    #endregion

    #region Private/Fields/Inspector

    [SerializeField] private AnimationCurve curve;

    [SerializeField] private UnityEvent onTimeSetMin;

    [SerializeField] private UnityEvent onTimeSetMax;

    #endregion
  }
}

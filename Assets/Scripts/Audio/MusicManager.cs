using System.Collections;

using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public float transitionDuration = 1f;
    public AudioSource @base, running, flying, rolling;

    private AudioSource[] layers;
    private AudioSource currentLayer;
    private AudioSource toLayer;

    //public static MusicManager Instance;

    private Coroutine transitionRoutine;

    private void Awake()
    {
        //Instance = this;
        layers = new[] { running, flying, rolling };
        for(int i = 0; i < layers.Length; i++) layers[i].volume = 0f;

        currentLayer = running;
        currentLayer.volume = 1f;
    }

    private void Start()
    {
        StateMachine.Instance.OnStateChanged += StateMachine_OnStateChanged;
    }

    private void StateMachine_OnStateChanged()
    {
        TransitionLayer(StateMachine.Instance.GetState());
    }

    private void TransitionLayer(Mode mode)
    {
        AudioSource to = GetLayerAudioSource(mode);
        StartTransitionLayerRoutine(currentLayer, to);
    }

    private void StartTransitionLayerRoutine(AudioSource from, AudioSource to)
    {
        if(transitionRoutine != null)
        {
            currentLayer.volume = 0f;
            toLayer.volume = 1f;
            currentLayer = toLayer;
            StopCoroutine(transitionRoutine);
        }
        transitionRoutine = StartCoroutine(TransitionLayerRoutine(from, to));
    }

    private IEnumerator TransitionLayerRoutine(AudioSource from, AudioSource to)
    {
        float start = Time.time;
        toLayer = to;

        while(Time.time < start + transitionDuration)
        {
            float t = (Time.time - start)/transitionDuration;
            SetLayerVolumesFromTo(t, from, to);
            yield return null;
        }

        SetLayerVolumesFromTo(1f, from, to);
        currentLayer = to;
        transitionRoutine = null;
    }

    private AudioSource GetLayerAudioSource(Mode mode)
    {
        switch(mode)
        {
            case Mode.Running: return running;
            case Mode.Flying: return flying;
            case Mode.Rolling: return rolling;
            default: throw new System.Exception($"Missing case {mode}");
        }
    }

    void SetLayerVolumesFromTo(float t, AudioSource from, AudioSource to)
    {
        if(t < 0.5f)
        {
            from.volume = t*2f;
            to.volume = 0f;
        }
        else
        {
            from.volume = 0f;
            to.volume = t*2f - 1f;
        }
    }
}

using System.Collections;
using System.Linq;

using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public float transitionDuration = 1f;
    public float phaseTransitionDuration = 1f;
    public AudioSource[] @base, running, flying, rolling;

    private AudioSource[] layers;
    private AudioSource[] currentLayer;
    private AudioSource[] toLayer;

    private Coroutine transitionRoutine;
    private float lastPhaseChange;

    public int PhaseIndex => GamePhases.Instance.activePhase.index;

    private void Awake()
    {
        //Instance = this;
        layers = running.Concat(flying).Concat(rolling).ToArray();
        for(int i = 0; i < layers.Length; i++) layers[i].volume = 0f;
        SetLayerVolumes(0f, @base);

        currentLayer = running;
        currentLayer[0].volume = 1f;
        @base[0].volume = 1f;
    }

    private void Start()
    {
        StateMachine.Instance.OnStateChanged += OnStateChanged;
        GameManager.Instance.OnPhaseIncreased += OnPhaseIncreased;
        GameManager.Instance.OnPhaseDecreased += OnPhaseDecreased;
    }

    private void SetLayerVolumes(float volume, AudioSource[] layers)
    {
        for(int i = 0; i < layers.Length; i++) layers[i].volume = volume;
    }

    private void OnPhaseIncreased()
    {
        lastPhaseChange = Time.time;
        StartCoroutine(TransitionPhaseRoutine(PhaseIndex - 1, PhaseIndex));
    }

    private void OnPhaseDecreased()
    {
        lastPhaseChange = Time.time;
        StartCoroutine(TransitionPhaseRoutine(PhaseIndex + 1, PhaseIndex));
    }

    private void OnStateChanged()
    {
        TransitionLayer(StateMachine.Instance.GetState());
    }

    private void TransitionLayer(Mode mode)
    {
        AudioSource[] to = GetLayerAudioSources(mode);
        StartTransitionLayerRoutine(currentLayer, to);
    }

    private void StartTransitionLayerRoutine(AudioSource[] from, AudioSource[] to)
    {
        if(transitionRoutine != null)
        {
            SetLayerVolumes(0f, currentLayer);
            //toLayer.volume = 1f;
            currentLayer = toLayer;
            StopCoroutine(transitionRoutine);
        }
        transitionRoutine = StartCoroutine(TransitionLayerRoutine(from, to));
    }

    private IEnumerator TransitionPhaseRoutine(int from, int to)
    {
        Debug.Log($"Transition phase from {from} to {to}");
        float start = Time.time;
        while(Time.time < start + phaseTransitionDuration)
        {
            float phaseVolume = Mathf.Clamp01((Time.time - lastPhaseChange)/phaseTransitionDuration);
            if(to < from) phaseVolume = 1f - phaseVolume;
            currentLayer[to].volume = phaseVolume;
            @base[to].volume = phaseVolume;
            yield return null;
        }

        float finalVolume = 1f;
        if(to < from) finalVolume = 0f;
        currentLayer[to].volume = finalVolume;
        @base[to].volume = finalVolume;
    }

    private IEnumerator TransitionLayerRoutine(AudioSource[] from, AudioSource[] to)
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

    private AudioSource[] GetLayerAudioSources(Mode mode)
    {
        switch(mode)
        {
            case Mode.Running: return running;
            case Mode.Flying: return flying;
            case Mode.Rolling: return rolling;
            default: throw new System.Exception($"Missing case {mode}");
        }
    }

    void SetLayerVolumesFromTo(float t, AudioSource[] from, AudioSource[] to)
    {
        for(int phase = 0; phase < from.Length; phase++)
        {
            float phaseVolume = Mathf.Clamp01((Time.time - lastPhaseChange)/phaseTransitionDuration);
            from[phase].volume = t*phaseVolume;
            to[phase].volume = (1f - t)*phaseVolume;
        }
    }
}

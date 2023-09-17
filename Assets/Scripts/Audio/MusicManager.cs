using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public float transitionDuration = 1f;
    public AudioSource[] @base, running, flying, rolling;

    private AudioSource[] layers;
    private AudioSource[] currentLayer;

    private int PhaseIndex => GamePhases.Instance.activePhase.index;
    private Mode CurrentMode => StateMachine.Instance.GetState();

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
        StateMachine.Instance.OnStateChanged += OnPhaseOrModeChanged;
        GameManager.Instance.OnPhaseIncreased += OnPhaseOrModeChanged;
        GameManager.Instance.OnPhaseDecreased += OnPhaseOrModeChanged;
    }

    private void SetLayerVolumes(float volume, AudioSource[] layers)
    {
        for(int i = 0; i < layers.Length; i++) layers[i].volume = volume;
    }

    private void OnPhaseOrModeChanged()
    {
        StartFadeRoutines();
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

    void StartFadeRoutines()
    {
        AudioSource[] modeTracks = GetLayerAudioSources(CurrentMode);
        for(int i = 0; i < modeTracks.Length; i++)
        {
            // Fade base and current layer tracks based on phase index
            float toVolume = i <= PhaseIndex ? 1f : 0f;
            StartCoroutine(FadeTrackRoutine(modeTracks[i], modeTracks[i].volume, toVolume, transitionDuration));
            StartCoroutine(FadeTrackRoutine(@base[i], @base[i].volume, toVolume, transitionDuration));
        }

        IEnumerable<AudioSource> otherTracks = layers.Where(x => !modeTracks.Contains(x));
        foreach(var other in otherTracks)
        {
            // Fade out all other tracks
            StartCoroutine(FadeTrackRoutine(other, other.volume, 0f, transitionDuration));
        }
    }

    IEnumerator FadeTrackRoutine(AudioSource track, float from, float to, float duration)
    {
        float start = Time.time;
        while(Time.time < start + duration)
        {
            float t = Mathf.Lerp(from, to, (Time.time - start)/duration);
            track.volume = t;
            yield return null;
        }

        track.volume = to;
    }
}

using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Audio;

using Random = UnityEngine.Random;

public class Sfx : MonoBehaviour
{
    public static Sfx Instance;

    [SerializeField] private AudioSource _source;

    public AudioClip[] startGameSiren;
    public AudioClip[] flyingBoosterLoop;
    public AudioClip[] flyingStartLiftOff;
    public AudioClip[] saw;
    public AudioClip[] speedUp;
    public AudioClip[] speedUp2;
    public AudioClip[] squelchGoo;
    public AudioClip[] playerCollision;

    private Dictionary<AudioClip[], List<float>> playingSounds = new();

    private void Start()
    {
        StateMachine.Instance.OnStateChanged +=Instance_OnStateChanged;
    }

    private void Instance_OnStateChanged()
    {
        if(StateMachine.Instance.GetState() == Mode.Flying)
        {
            Play(flyingStartLiftOff, 1f, 1);
        }
    }

    private List<float> _toRemove = new List<float>();
    private void Update()
    {
        foreach(var kvp in playingSounds)
        {
            _toRemove.Clear();
            foreach(float endTime in kvp.Value)
            {
                if(Time.time > endTime)
                {
                    _toRemove.Add(endTime);
                }
            }
            foreach(float t in _toRemove)
            {
                kvp.Value.Remove(t);
            }
        }
    }

    public void Play(AudioClip[] clips, float volume = 1f, int maxPlaying = -1, float chance = 1f)
    {
        if(chance != 1f && Random.value > chance) return;

        List<float> playing = playingSounds.GetOrAdd(clips);
        AudioClip clip = clips.Choose();
        if(maxPlaying == -1)
        {
            playing.Add(Time.time + clip.length);
            _source.PlayOneShot(clip, volume);
        }
        else
        {
            if(playing.Count < maxPlaying)
            {
                playing.Add(Time.time + clip.length);
                _source.PlayOneShot(clip, volume);
            }
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    struct PlayingClip
    {
        public AudioClip[] Source;
        public AudioClip Clip;
    }
}
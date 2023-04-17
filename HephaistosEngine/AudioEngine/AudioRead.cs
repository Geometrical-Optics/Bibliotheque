using System;
using NAudio.Wave;
namespace AudioEngine;


public class AudioWavRead
{
    public AudioFileReader _sound;
    
    public AudioWavRead(string way)
    {
        var audioFile = new AudioFileReader(way);
        _sound = audioFile;
    }
    
    public void Url(string way)
    {
        using(var mf = new MediaFoundationReader(way))
        using(var wo = new WasapiOut())
        {
            wo.Init(mf);
            wo.Play();
            while (wo.PlaybackState == PlaybackState.Playing)
            {
                Thread.Sleep(1000);
            }
        }
    }

    public void Play()
    {
        using(var outputDevice = new WaveOutEvent())
        {
            outputDevice.Init(_sound);
            outputDevice.Play();
            while (outputDevice.PlaybackState == PlaybackState.Playing)
            {
                Thread.Sleep(1000);
            }
        }
    }

    public void Stop()
    {
        
    }
}
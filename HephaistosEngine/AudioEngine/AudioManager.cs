using SFML.Audio;
using SFML.System;
using System.Collections.Generic;

namespace AudioEngine;

public class AudioManager
{
    private Dictionary<int, VolumeManager> _volumeManagerDictionary;

    public AudioManager()
    {
        _volumeManagerDictionary = new Dictionary<int, VolumeManager>();
    }

    public void AddSound(int id, string filePath)
    {
        if (!_volumeManagerDictionary.ContainsKey(id))
        {
            VolumeManager volumeManager = new VolumeManager();
            SoundBuffer soundBuffer = new SoundBuffer(filePath);
            volumeManager.SetBuffer(soundBuffer);
            _volumeManagerDictionary.Add(id, volumeManager);
        }
    }

    public void PlaySound(int id)
    {
        if (_volumeManagerDictionary.ContainsKey(id))
        {
            VolumeManager volumeManager = _volumeManagerDictionary[id];
            volumeManager.Play();
        }
    }

    public void PauseSound(int id)
    {
        if (_volumeManagerDictionary.ContainsKey(id))
        {
            VolumeManager volumeManager = _volumeManagerDictionary[id];
            volumeManager.Pause();
        }
    }

    public void StopSound(int id)
    {
        if (_volumeManagerDictionary.ContainsKey(id))
        {
            VolumeManager volumeManager = _volumeManagerDictionary[id];
            volumeManager.Stop();
        }
    }

    public void SetSoundPosition(int id, SFML.System.Vector3f position)
    {
        if (_volumeManagerDictionary.ContainsKey(id))
        {
            VolumeManager volumeManager = _volumeManagerDictionary[id];
            volumeManager.Position = position;
        }
    }

    public void SetSoundVolume(int id, int volume)
    {
        if (_volumeManagerDictionary.ContainsKey(id))
        {
            VolumeManager volumeManager = _volumeManagerDictionary[id];
            volumeManager.Volume = volume;
        }
    }
}
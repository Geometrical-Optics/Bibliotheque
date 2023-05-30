using SFML.Audio;
using SFML.System;
using System;

namespace AudioMoteur;

public class VolumeManager
{
    
    private int _volume;
    private Sound _sound;
    private Vector3f _position;

    public int Volume
    {
        get { return _volume; }
        set
        {
            _volume = Math.Clamp(value, 0, 100);
            UpdateVolume();
        }
    }

    public Vector3f Position
    {
        get { return _position; }
        set
        {
            _position = value;
            UpdateSoundProperties();
        }
    }

    public VolumeManager()
    {
        _volume = 100;
        _sound = new Sound();
        _position = new Vector3f(0f, 0f, 0f);
    }

    private void UpdateVolume()
    {
        _sound.Volume = (float)_volume;
    }

    private void UpdateSoundProperties()
    {
        _sound.Position = _position;
    }

    public void SetBuffer(SoundBuffer buffer)
    {
        _sound.SoundBuffer = buffer;
    }

    public void Play()
    {
        _sound.Play();
    }

    public void Pause()
    {
        _sound.Pause();
    }

    public void Stop()
    {
        _sound.Stop();
    }
}
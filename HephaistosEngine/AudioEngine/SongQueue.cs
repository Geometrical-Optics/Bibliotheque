using System.Media;
using NAudio.Wave;
using NAudio;

namespace AudioEngine;

public class SongQueue
{
    private Queue<SoundPlayer> _queue;

    public SongQueue(Queue<string> songs)
    {
        _queue = new Queue<SoundPlayer>();
        while (songs.Count != 0)
        {
            var reader = new System.Media.SoundPlayer();
            reader.SoundLocation = songs.Dequeue();
            _queue.Enqueue(reader);
        }
    }

    public void Next()
    {
        if (_queue.Count != 0)
        {
            _queue.Dequeue().Stop();
            _queue.Peek().Play();
        }
    }

    public void Stop()
    {
        if (_queue.Count != 0)
        {
            _queue.Dequeue().Stop();
        }
    }

    public void Play()
    {
        if (_queue.Count != 0)
        {
            _queue.Peek().Play();
        }
    }
}
namespace AudioMotor;
using System;
using NAudio;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

public class AudioMotor
{
    private WaveOutEvent outputDevice;
    private AudioFileReader audioFile;

    public AudioMotor(string str)
    {
        if (outputDevice == null)
        { 
            outputDevice = new WaveOutEvent();
            outputDevice.PlaybackStopped = 
        }

        if (audioFile == null)
        {
            if ()
            audioFile = new AudioFileReader()
        }
    }
    public void Lecture(string str)
    {
        //Verifier que le chemin de lecture est valide puis lancer l'audio en fonction de son .
        
        throw new NotImplementedException();
    }

    public void Pause()
    {
        //Mets l'audio en pause
        throw new NotImplementedException();
    }

    public void Stop()
    {
        //Arrête l'audio en cours et le supprime de la liste de lecture
        throw new NotImplementedException();
    }

    public void Replay()
    {
        //Relance l'audio qui se joue
        throw new NotImplementedException();
    }

    public void Boucle()
    {
        //Joue en continue l'audio
        throw new NotImplementedException();
    }
}
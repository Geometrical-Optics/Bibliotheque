// See https://aka.ms/new-console-template for more information

using AudioMotor;
using GLib;
using NAudio.Wave;
using Thread = System.Threading.Thread;

var le = new Queue<string>();
le.Enqueue("Nirvana - Smells Like Teen Spirit (Official Music Video).wav");
var test = new SongQueue(le);
Thread.Sleep(5000);
test.Stop();





//le.Enqueue("C:\\Users\\clem_\\OneDrive\\Documents\\EPITA\\2027\\C#\\2nd\\Projet S2\\Nirvana - Smells Like Teen Spirit (Official Music Video).wav");
/*var audioFile = new AudioFileReader("C:\\Users\\clem_\\OneDrive\\Documents\\EPITA\\2027\\C#\\2nd\\Projet S2\\HeadswillRoll.mp3");
//var audioFile = new AudioFileReader("C:\\Users\\clem_\\OneDrive\\Documents\\EPITA\\2027\\C#\\2nd\\Projet S2\\Nirvana - Smells Like Teen Spirit (Official Music Video).wav");
using(var outputDevice = new WaveOutEvent())
{
    outputDevice.Init(audioFile);
    outputDevice.Play();
    while (outputDevice.PlaybackState == PlaybackState.Playing)
    {
        Thread.Sleep(1000);
    }
}
AudioFileReader audioFileReader = new AudioFileReader("C:\\Users\\clem_\\Music\\SoundTrack\\HeadswillRoll.mp3");
SoundPlayer soundPlayer = new SoundPlayer("C:\\Users\\clem_\\Downloads\\Adudule.wav");
soundPlayer.PlaySync(); */
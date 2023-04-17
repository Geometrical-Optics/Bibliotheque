using NAudio.Wave.SampleProviders;
using NAudio.Wave;

namespace AudioEngine;

public class AudioFileReader : WaveStream, ISampleProvider
{ 
        private WaveStream readerStream;
        private readonly SampleChannel sampleChannel;
        private readonly int destBytesPerSample;
        private readonly int sourceBytesPerSample;
        private readonly long length;
        private readonly object lockObject;
        
        public AudioFileReader(string fileName)
        {
            lockObject = new object();
            FileName = fileName;
            CreationReaderStream(fileName);
            sourceBytesPerSample = (readerStream.WaveFormat.BitsPerSample / 8) * readerStream.WaveFormat.Channels;
            sampleChannel = new SampleChannel(readerStream, false);
            destBytesPerSample = 4*sampleChannel.WaveFormat.Channels;
            length = SourceToDest(readerStream.Length);
        }
        
        private void CreationReaderStream(string fileName)
        {
            if (fileName.EndsWith(".wav", StringComparison.OrdinalIgnoreCase))
            {
                readerStream = new WaveFileReader(fileName);
                if (readerStream.WaveFormat.Encoding != WaveFormatEncoding.Pcm && readerStream.WaveFormat.Encoding != WaveFormatEncoding.IeeeFloat)
                {
                    readerStream = WaveFormatConversionStream.CreatePcmStream(readerStream);
                    readerStream = new BlockAlignReductionStream(readerStream);
                }
            }
            else if (fileName.EndsWith(".mp3", StringComparison.OrdinalIgnoreCase))
            {
                if (Environment.OSVersion.Version.Major < 6)
                {
                    readerStream = new Mp3FileReader(fileName);
                }
                else
                {
                    readerStream = new MediaFoundationReader(fileName);
                }
            }
        }
        
        public string FileName { get; }
        public override WaveFormat WaveFormat => sampleChannel.WaveFormat;
        public override long Length => length;
        
        public override long Position
        {
            get { return SourceToDest(readerStream.Position); }
            set { lock (lockObject) { readerStream.Position = DestToSource(value); }  }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            var waveBuffer = new WaveBuffer(buffer);
            int samplesRequired = count / 4;
            int samplesRead = Read(waveBuffer.FloatBuffer, offset / 4, samplesRequired);
            return samplesRead * 4;
        }
        
        public int Read(float[] buffer, int offset, int count)
        {
            lock (lockObject)
            {
                return sampleChannel.Read(buffer, offset, count);
            }
        }
        
        public float Volume
        {
            get { return sampleChannel.Volume; }
            set { sampleChannel.Volume = value; } 
        }
        private long SourceToDest(long sourceBytes)
        {
            return destBytesPerSample * (sourceBytes / sourceBytesPerSample);
        }

        private long DestToSource(long destBytes)
        {
            return sourceBytesPerSample * (destBytes / destBytesPerSample);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (readerStream != null) {
                    readerStream.Dispose();
                    readerStream = null;
                }
            }
            base.Dispose(disposing);
        }
}
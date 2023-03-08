using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Wave;

namespace AudioConverter
{
    public partial class AudioConverter : Form
    {
        public AudioConverter()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "MP3 File (*.mp3)|*.mp3;";
            if (open.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "WAV File (*.wav)|*.wav;";
            if (save.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            using (Mp3FileReader mp3 = new Mp3FileReader(open.FileName))
            {
                using (WaveStream pcm = WaveFormatConversionStream.CreatePcmStream(mp3))
                {
                    WaveFileWriter.CreateWaveFile(save.FileName, pcm);
                }
            }
        }

      /*  private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "WAV File (*.wav)|*.wav;";
            if (open.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "MP3 File (*.mp3)|*.mp3;";
            if (save.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            using (WaveFileReader wave = new WaveFileReader(open.FileName))
            {
                using (Mp3 pcm = Mp3WaveFormat.(wave))
                {
                    WaveFileWriter.CreateWaveFile(save.FileName, pcm);
                }
            }

        }
      */
    }
}

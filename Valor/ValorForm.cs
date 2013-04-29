using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio;
using NAudio.Wave;
using System.IO;

namespace Valor
{
    public class ValorForm : Form
    {
        private IWavePlayer waveOutDevice;
        private WaveStream mainOutputStream;

        public ValorEngine Engine { get; set; }

        public ValorForm()
        {
            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            WindowState = FormWindowState.Maximized;
            FormBorderStyle = FormBorderStyle.None;
            KeyDown += TestForm_KeyDown;
            this.Paint += TestForm_Paint;
            RunMusic();
        }

        ~ValorForm()
        {
            waveOutDevice.Dispose();
            mainOutputStream.Dispose();
        }

        private void RunMusic()
        {
            var fileName = String.Format("..\\..\\{0}", ConfigurationManager.AppSettings["music"]);

            if (fileName.EndsWith(".mp3") && File.Exists(fileName))
            {
                mainOutputStream = new Mp3FileReader(fileName);

                waveOutDevice = new WaveOut();
                waveOutDevice.PlaybackStopped += waveOutDevice_PlaybackStopped;
                waveOutDevice.Init(mainOutputStream);
                waveOutDevice.Play();
            }
        }

        private void waveOutDevice_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            mainOutputStream.Position = 0;
            waveOutDevice.Init(mainOutputStream);
            waveOutDevice.Play();
        }

        private void TestForm_Paint(object sender, PaintEventArgs e)
        {
            this.InitGraphics(e.Graphics);
            Engine.Render(e.Graphics, e.ClipRectangle.Width / ValorEngine.SCALE, e.ClipRectangle.Height / ValorEngine.SCALE);
        }

        private void InitGraphics(Graphics g)
        {
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.ScaleTransform(ValorEngine.SCALE, ValorEngine.SCALE);
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
        }

        private void TestForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
    }
}

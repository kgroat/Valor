namespace Valor
{
    using System;
    using System.Configuration;
    using System.Drawing;
    using System.Windows.Forms;
    using NAudio.Wave;
    using System.IO;

    public sealed class ValorForm : Form
    {
        private IWavePlayer waveOutDevice;
        private WaveStream mainOutputStream;

        public ValorEngine Engine { get; set; }

        public ValorForm()
        {
            this.Text = "VALOR";
            DoubleBuffered = true;
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            WindowState = FormWindowState.Maximized;
            FormBorderStyle = FormBorderStyle.None;
            KeyDown += this.TestFormKeyDown;
            this.Paint += this.TestFormPaint;
            this.Shown += (sender, args) => this.ValorMain();
        }

        ~ValorForm()
        {
            if(waveOutDevice != null)
                waveOutDevice.Dispose();
            if(mainOutputStream != null)
                mainOutputStream.Dispose();
        }

        private void ValorMain()
        {
            ValorEngine.Init(this);
            Engine.Mode = new MainMenuMode();
            var fileName = ConfigurationManager.AppSettings["music"];

            if (fileName.EndsWith(".mp3") && File.Exists(fileName))
            {
                mainOutputStream = new Mp3FileReader(fileName);

                waveOutDevice = new WaveOut();
                waveOutDevice.PlaybackStopped += this.WaveOutDevicePlaybackStopped;
                waveOutDevice.Init(mainOutputStream);
                waveOutDevice.Play();
            }
        }

        private void WaveOutDevicePlaybackStopped(object sender, StoppedEventArgs e)
        {
            mainOutputStream.Position = 0;
            waveOutDevice.Init(mainOutputStream);
            waveOutDevice.Play();
        }

        private void TestFormPaint(object sender, PaintEventArgs e)
        {
            InitGraphics(e.Graphics);
            Engine.Render(e.Graphics, e.ClipRectangle.Width / ValorEngine.Scale, e.ClipRectangle.Height / ValorEngine.Scale);
        }

        private void TestFormKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private static void InitGraphics(Graphics g)
        {
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            g.ScaleTransform(ValorEngine.Scale, ValorEngine.Scale);
            g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
            g.PageUnit = GraphicsUnit.Pixel;
        }
    }
}

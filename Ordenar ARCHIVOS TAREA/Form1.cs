using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using PixelFormat = System.Windows.Media.PixelFormat;
using Vlc.DotNet.Forms;
using WMPLib;

namespace Ordenar_ARCHIVOS_TAREA
{
    public partial class Form1 : Form
    {
        private VlcControl vlcControl;

        public Form1()
        {
            InitializeComponent();
        }
        

        private void button1_Click(object sender, EventArgs e)
        {

            // Mostrar el FolderBrowserDialog
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                // Obtener la ruta de la carpeta seleccionada
                string selectedPath = folderDialog.SelectedPath;

                // Crear carpetas para cada tipo de archivo si no existen
                string imagesFolder = Path.Combine(selectedPath, "Imágenes");
                if (!Directory.Exists(imagesFolder))
                {
                    Directory.CreateDirectory(imagesFolder);
                }

                string videosFolder = Path.Combine(selectedPath, "Videos");
                if (!Directory.Exists(videosFolder))
                {
                    Directory.CreateDirectory(videosFolder);
                }

                string musicFolder = Path.Combine(selectedPath, "Música");
                if (!Directory.Exists(musicFolder))
                {
                    Directory.CreateDirectory(musicFolder);
                }
                string othersFolder = Path.Combine(selectedPath, "Otros");
                if (!Directory.Exists(othersFolder))
                {
                    Directory.CreateDirectory(othersFolder);
                }

                // Obtener una lista de todos los archivos en la carpeta seleccionada
                string[] files = Directory.GetFiles(selectedPath);

                // Mover los archivos a las carpetas correspondientes y mostrar la primera imagen de cada tipo en un PictureBox separado
                foreach (string file in files)
                {
                    string extension = Path.GetExtension(file);
                    string fileName = Path.GetFileName(file);

                    if (extension.Equals(".jpg") || extension.Equals(".jpeg") || extension.Equals(".png") || extension.Equals(".jfif"))
                    {
                        string destFile = Path.Combine(imagesFolder, fileName);
                        File.Move(file, destFile);

                    }
                    else if (extension.Equals(".mp4") || extension.Equals(".avi") || extension.Equals(".mov") || extension.Equals(".wmv")|| extension.Equals(".gif"))
                    {

                        string destFile = Path.Combine(videosFolder, fileName);
                        File.Move(file, destFile);

                    }
                    else if (extension.Equals(".mp3") || extension.Equals(".wav") || extension.Equals(".flac"))
                    {
                        string destFile = Path.Combine(musicFolder, fileName);
                        File.Move(file, destFile);

                        
                    }
                    else
                    {
                        string destFile = Path.Combine(othersFolder, fileName);
                        File.Move(file, destFile);

                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos multimedia|*.mp3;*.wav;*.flac;*.mp4;*.avi;*.mov;*.jpg;*.png;*.gif;*.jfif;*.jpeg";
            DialogResult result = openFileDialog.ShowDialog();

            try
            {
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(openFileDialog.FileName))
                {

                    string filePath = openFileDialog.FileName;
                    textBox1.Text = filePath;
                    vlcControl1.Play(new Uri(filePath));
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            if (btnPlay.Text == "Pause")
            {
                vlcControl1.Pause();
                btnPlay.Text = "Play";
            }
            else if (btnPlay.Text == "Play")
            {
                vlcControl1.Play();
                btnPlay.Text = "Pause";
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            vlcControl1.Stop();
        }

        private void btnAvanzar_Click(object sender, EventArgs e)
        {
            vlcControl1.Time += 10000; // Avanzar 10 segundos
        }

        private void btnRetroceder_Click(object sender, EventArgs e)
        {
            vlcControl1.Time -= 10000; // Retroceder 10 segundos
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            vlcControl1.Audio.Volume = trackBar1.Value * 10;
        }
    }
} 
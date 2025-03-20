using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dz_1
{
    public partial class Gallery : Form
    {
        private Dictionary<string, string> imageTitles = new Dictionary<string, string>();
        private List<string> imagePaths = new List<string>();
        public Gallery()
        {
            InitializeComponent();
            this.AutoScaleMode = AutoScaleMode.Font;
            PictureBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ImageListBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            TextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            LoadButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            SaveButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            PictureBox.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "Text Files (*.txt)|*.txt";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var lines = imagePaths.Select(path => $"{path},{imageTitles[path]}").ToArray();
                    File.WriteAllLines(saveFileDialog.FileName, lines);
                }
            }
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Text Files (*.txt)|*.txt";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    imagePaths.Clear();
                    imageTitles.Clear();
                    ImageListBox.Items.Clear();

                    foreach (var line in File.ReadAllLines(openFileDialog.FileName))
                    {
                        var parts = line.Split(',');
                        var path = parts[0];
                        var title = parts.Length > 1 ? parts[1] : "";

                        imagePaths.Add(path);
                        imageTitles[path] = title;
                        ImageListBox.Items.Add(Path.GetFileName(path));
                    }

                    if (ImageListBox.Items.Count > 0)
                    {
                        ImageListBox.SelectedIndex = 0;
                    }
                }
            }
        }

        private void ImageListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ImageListBox.SelectedIndex >= 0)
            {
                var selectedPath = imagePaths[ImageListBox.SelectedIndex];
                PictureBox.Image = Image.FromFile(selectedPath);
                TextBox.Text = imageTitles[selectedPath];
            }
        }

        private void PictureBox_Click(object sender, EventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            if (ImageListBox.SelectedIndex >= 0)
            {
                var selectedPath = imagePaths[ImageListBox.SelectedIndex];
                imageTitles[selectedPath] = TextBox.Text;
            }
        }
    }
}

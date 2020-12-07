using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using BastionTimeConverter.Properties;

namespace BastionTimeConverter
{
    public partial class BastionTimeConverter : Form
    {
        Converter converter;
        TextBoxWriter writer;
        Exporter exporter;

        public BastionTimeConverter()
        {
            InitializeComponent();

            converter = new Converter();
        }

        private void BastionTimeConverter_Load(object sender, EventArgs e)
        {

        }

        private void OpenFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (!openFileDialog.FileName.Equals(null))
                {
                    converter.SetFile(openFileDialog.FileName);

                    if (!converter.File.IsValid)
                    {
                        MessageBox.Show("Error: File not recognized as a Bastion splits file.");
                    }
                    else if (converter.File.CatDetect.Equals("Error"))
                    {
                        MessageBox.Show("Error: No valid category detected.");
                    }
                    else if (!converter.File.AutoSplitSet)
                    {
                        MessageBox.Show("Error: No autosplitter settings detected. Make sure this file has autosplitter settings, then try again.");
                    }
                    else if (converter.File.Time.Equals(SplitsFile.Timing.Error))
                    {
                        MessageBox.Show("Error: No timing settings detected. Make sure this file is set to either Skyway or Load timing, then try again.");
                    }
                    else if (converter.File.PBSplits == null || converter.File.SOBSplits == null)
                    {
                        MessageBox.Show("Error: Splits are missing or incomplete in this file.");
                    }
                    else
                    {
                        FileName.Text = $"File Name: {openFileDialog.SafeFileName}";
                        CatRead.Text = $"Category Detected: {converter.File.CatDetect}";
                        TimeRead.Text = $"Timing Detected: {converter.File.Time}";

                        TablePB.Text = "";
                        TableSOB.Text = "";

                        writer = new TextBoxWriter(TablePB);
                        Console.SetOut(writer);
                        converter.Convert(SplitsFile.Comparison.PersonalBest);

                        writer = new TextBoxWriter(TableSOB);
                        Console.SetOut(writer);
                        converter.Convert(SplitsFile.Comparison.SumOfBest);

                        ExportSkyway.Enabled = true;
                        ExportLoad.Enabled = true;
                    }
                }
            }
        }

        private void ExportSkyway_Click(object sender, EventArgs e)
        {
            saveFileDialogSkyway.ShowDialog();
        }

        private void ExportLoad_Click(object sender, EventArgs e)
        {
            saveFileDialogLoad.ShowDialog();
        }

        private void saveFileDialogSkyway_FileOk(object sender, CancelEventArgs e)
        {
            exporter = new Exporter(saveFileDialogSkyway.FileName,
                converter.File,
                converter.PBSkyway,
                converter.SOBSkyway,
                converter.PBLoad,
                converter.SOBLoad);

            exporter.Export(Exporter.Target.Skyway);
        }

        private void saveFileDialogLoad_FileOk(object sender, CancelEventArgs e)
        {
            exporter = new Exporter(saveFileDialogLoad.FileName,
                converter.File,
                converter.PBSkyway,
                converter.SOBSkyway,
                converter.PBLoad,
                converter.SOBLoad);

            exporter.Export(Exporter.Target.Load);
        }
    }
}

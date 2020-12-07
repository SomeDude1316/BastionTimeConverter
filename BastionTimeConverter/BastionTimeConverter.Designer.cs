
namespace BastionTimeConverter
{
    partial class BastionTimeConverter
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BastionTimeConverter));
            this.OpenFile = new System.Windows.Forms.Button();
            this.FileName = new System.Windows.Forms.Label();
            this.CatRead = new System.Windows.Forms.Label();
            this.TimeRead = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.LabelPB = new System.Windows.Forms.Label();
            this.LabelSOB = new System.Windows.Forms.Label();
            this.TablePB = new System.Windows.Forms.Label();
            this.TableSOB = new System.Windows.Forms.Label();
            this.ExportSkyway = new System.Windows.Forms.Button();
            this.ExportLoad = new System.Windows.Forms.Button();
            this.saveFileDialogSkyway = new System.Windows.Forms.SaveFileDialog();
            this.saveFileDialogLoad = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // OpenFile
            // 
            this.OpenFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OpenFile.Location = new System.Drawing.Point(229, 12);
            this.OpenFile.Name = "OpenFile";
            this.OpenFile.Size = new System.Drawing.Size(607, 37);
            this.OpenFile.TabIndex = 0;
            this.OpenFile.Text = "Open Splits File";
            this.OpenFile.UseMnemonic = false;
            this.OpenFile.UseVisualStyleBackColor = true;
            this.OpenFile.Click += new System.EventHandler(this.OpenFile_Click);
            // 
            // FileName
            // 
            this.FileName.AutoSize = true;
            this.FileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FileName.Location = new System.Drawing.Point(389, 59);
            this.FileName.Name = "FileName";
            this.FileName.Size = new System.Drawing.Size(0, 20);
            this.FileName.TabIndex = 1;
            // 
            // CatRead
            // 
            this.CatRead.AutoSize = true;
            this.CatRead.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CatRead.Location = new System.Drawing.Point(389, 88);
            this.CatRead.Name = "CatRead";
            this.CatRead.Size = new System.Drawing.Size(0, 20);
            this.CatRead.TabIndex = 2;
            // 
            // TimeRead
            // 
            this.TimeRead.AutoSize = true;
            this.TimeRead.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimeRead.Location = new System.Drawing.Point(389, 117);
            this.TimeRead.Name = "TimeRead";
            this.TimeRead.Size = new System.Drawing.Size(0, 20);
            this.TimeRead.TabIndex = 3;
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Splits Files (*.lss)|*.lss";
            this.openFileDialog.Title = "Select a Splits File";
            // 
            // LabelPB
            // 
            this.LabelPB.AutoSize = true;
            this.LabelPB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelPB.Location = new System.Drawing.Point(215, 143);
            this.LabelPB.Name = "LabelPB";
            this.LabelPB.Size = new System.Drawing.Size(115, 20);
            this.LabelPB.TabIndex = 6;
            this.LabelPB.Text = "Personal Best";
            // 
            // LabelSOB
            // 
            this.LabelSOB.AutoSize = true;
            this.LabelSOB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelSOB.Location = new System.Drawing.Point(773, 143);
            this.LabelSOB.Name = "LabelSOB";
            this.LabelSOB.Size = new System.Drawing.Size(102, 20);
            this.LabelSOB.TabIndex = 7;
            this.LabelSOB.Text = "Sum of Best";
            // 
            // TablePB
            // 
            this.TablePB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TablePB.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TablePB.Location = new System.Drawing.Point(12, 174);
            this.TablePB.Name = "TablePB";
            this.TablePB.Size = new System.Drawing.Size(524, 470);
            this.TablePB.TabIndex = 8;
            // 
            // TableSOB
            // 
            this.TableSOB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TableSOB.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TableSOB.Location = new System.Drawing.Point(542, 174);
            this.TableSOB.Name = "TableSOB";
            this.TableSOB.Size = new System.Drawing.Size(525, 470);
            this.TableSOB.TabIndex = 9;
            // 
            // ExportSkyway
            // 
            this.ExportSkyway.Enabled = false;
            this.ExportSkyway.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExportSkyway.Location = new System.Drawing.Point(110, 657);
            this.ExportSkyway.Name = "ExportSkyway";
            this.ExportSkyway.Size = new System.Drawing.Size(306, 43);
            this.ExportSkyway.TabIndex = 10;
            this.ExportSkyway.Text = "Export Skyway Time";
            this.ExportSkyway.UseVisualStyleBackColor = true;
            this.ExportSkyway.Click += new System.EventHandler(this.ExportSkyway_Click);
            // 
            // ExportLoad
            // 
            this.ExportLoad.Enabled = false;
            this.ExportLoad.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExportLoad.Location = new System.Drawing.Point(659, 658);
            this.ExportLoad.Name = "ExportLoad";
            this.ExportLoad.Size = new System.Drawing.Size(306, 43);
            this.ExportLoad.TabIndex = 11;
            this.ExportLoad.Text = "Export Load Time";
            this.ExportLoad.UseMnemonic = false;
            this.ExportLoad.UseVisualStyleBackColor = true;
            this.ExportLoad.Click += new System.EventHandler(this.ExportLoad_Click);
            // 
            // saveFileDialogSkyway
            // 
            this.saveFileDialogSkyway.DefaultExt = "lss";
            this.saveFileDialogSkyway.Filter = "Splits Files (*.lss)|*.lss";
            this.saveFileDialogSkyway.Title = "Save Splits File";
            this.saveFileDialogSkyway.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialogSkyway_FileOk);
            // 
            // saveFileDialogLoad
            // 
            this.saveFileDialogLoad.DefaultExt = "lss";
            this.saveFileDialogLoad.Filter = "Splits Files (*.lss)|*.lss";
            this.saveFileDialogLoad.Title = "Save Splits File";
            this.saveFileDialogLoad.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialogLoad_FileOk);
            // 
            // BastionTimeConverter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1079, 713);
            this.Controls.Add(this.ExportLoad);
            this.Controls.Add(this.ExportSkyway);
            this.Controls.Add(this.TableSOB);
            this.Controls.Add(this.TablePB);
            this.Controls.Add(this.LabelSOB);
            this.Controls.Add(this.LabelPB);
            this.Controls.Add(this.TimeRead);
            this.Controls.Add(this.CatRead);
            this.Controls.Add(this.FileName);
            this.Controls.Add(this.OpenFile);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BastionTimeConverter";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "Bastion Timing Converter";
            this.Load += new System.EventHandler(this.BastionTimeConverter_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OpenFile;
        private System.Windows.Forms.Label FileName;
        private System.Windows.Forms.Label CatRead;
        private System.Windows.Forms.Label TimeRead;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label LabelPB;
        private System.Windows.Forms.Label LabelSOB;
        private System.Windows.Forms.Label TablePB;
        private System.Windows.Forms.Label TableSOB;
        private System.Windows.Forms.Button ExportSkyway;
        private System.Windows.Forms.Button ExportLoad;
        private System.Windows.Forms.SaveFileDialog saveFileDialogSkyway;
        private System.Windows.Forms.SaveFileDialog saveFileDialogLoad;
    }
}


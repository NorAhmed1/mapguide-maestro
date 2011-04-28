﻿namespace Maestro.Base.UI.Preferences
{
    partial class EditorPreferencesCtrl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chkValidateOnSave = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnBrowseXsdPath = new System.Windows.Forms.Button();
            this.txtXsdPath = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox4.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.chkValidateOnSave);
            this.groupBox4.Location = new System.Drawing.Point(3, 3);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(399, 50);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Editing";
            // 
            // chkValidateOnSave
            // 
            this.chkValidateOnSave.AutoSize = true;
            this.chkValidateOnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkValidateOnSave.Location = new System.Drawing.Point(19, 19);
            this.chkValidateOnSave.Name = "chkValidateOnSave";
            this.chkValidateOnSave.Size = new System.Drawing.Size(163, 17);
            this.chkValidateOnSave.TabIndex = 2;
            this.chkValidateOnSave.Text = "Validate Resources On Save";
            this.chkValidateOnSave.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnBrowseXsdPath);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtXsdPath);
            this.groupBox1.Location = new System.Drawing.Point(4, 60);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(398, 66);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "XML Editor";
            // 
            // btnBrowseXsdPath
            // 
            this.btnBrowseXsdPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseXsdPath.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnBrowseXsdPath.Location = new System.Drawing.Point(367, 22);
            this.btnBrowseXsdPath.Name = "btnBrowseXsdPath";
            this.btnBrowseXsdPath.Size = new System.Drawing.Size(25, 23);
            this.btnBrowseXsdPath.TabIndex = 14;
            this.btnBrowseXsdPath.Text = "...";
            this.btnBrowseXsdPath.UseVisualStyleBackColor = true;
            this.btnBrowseXsdPath.Click += new System.EventHandler(this.btnBrowseXsdPath_Click);
            // 
            // txtXsdPath
            // 
            this.txtXsdPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtXsdPath.Location = new System.Drawing.Point(112, 24);
            this.txtXsdPath.Name = "txtXsdPath";
            this.txtXsdPath.ReadOnly = true;
            this.txtXsdPath.Size = new System.Drawing.Size(249, 20);
            this.txtXsdPath.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label4.Location = new System.Drawing.Point(15, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Xml Schema Path";
            // 
            // EditorPreferencesCtrl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox4);
            this.Name = "EditorPreferencesCtrl";
            this.Size = new System.Drawing.Size(405, 326);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox chkValidateOnSave;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnBrowseXsdPath;
        private System.Windows.Forms.TextBox txtXsdPath;
        private System.Windows.Forms.Label label4;
    }
}
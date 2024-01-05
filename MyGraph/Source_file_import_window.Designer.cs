
namespace MyGraph
{
    partial class Source_file_import_window
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
            this.File_label = new System.Windows.Forms.Label();
            this.Path_textbox = new System.Windows.Forms.TextBox();
            this.Brows_button = new System.Windows.Forms.Button();
            this.Delimiter_label = new System.Windows.Forms.Label();
            this.Comment_label = new System.Windows.Forms.Label();
            this.Delimiter = new System.Windows.Forms.TextBox();
            this.Comment = new System.Windows.Forms.TextBox();
            this.Previw_window = new System.Windows.Forms.TextBox();
            this.Inport_button = new System.Windows.Forms.Button();
            this.Preview_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // File_label
            // 
            this.File_label.AutoSize = true;
            this.File_label.Location = new System.Drawing.Point(144, 356);
            this.File_label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.File_label.Name = "File_label";
            this.File_label.Size = new System.Drawing.Size(29, 15);
            this.File_label.TabIndex = 0;
            this.File_label.Text = "File";
            // 
            // Path_textbox
            // 
            this.Path_textbox.Location = new System.Drawing.Point(184, 352);
            this.Path_textbox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Path_textbox.Name = "Path_textbox";
            this.Path_textbox.Size = new System.Drawing.Size(357, 22);
            this.Path_textbox.TabIndex = 1;
            // 
            // Brows_button
            // 
            this.Brows_button.Location = new System.Drawing.Point(551, 350);
            this.Brows_button.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Brows_button.Name = "Brows_button";
            this.Brows_button.Size = new System.Drawing.Size(100, 29);
            this.Brows_button.TabIndex = 2;
            this.Brows_button.Text = "Brows";
            this.Brows_button.UseVisualStyleBackColor = true;
            this.Brows_button.Click += new System.EventHandler(this.Brows_button_Click);
            // 
            // Delimiter_label
            // 
            this.Delimiter_label.AutoSize = true;
            this.Delimiter_label.Location = new System.Drawing.Point(15, 394);
            this.Delimiter_label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Delimiter_label.Name = "Delimiter_label";
            this.Delimiter_label.Size = new System.Drawing.Size(63, 15);
            this.Delimiter_label.TabIndex = 3;
            this.Delimiter_label.Text = "Delimiter";
            // 
            // Comment_label
            // 
            this.Comment_label.AutoSize = true;
            this.Comment_label.Location = new System.Drawing.Point(223, 394);
            this.Comment_label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Comment_label.Name = "Comment_label";
            this.Comment_label.Size = new System.Drawing.Size(68, 15);
            this.Comment_label.TabIndex = 4;
            this.Comment_label.Text = "Comment";
            // 
            // Delimiter
            // 
            this.Delimiter.Location = new System.Drawing.Point(104, 390);
            this.Delimiter.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Delimiter.Name = "Delimiter";
            this.Delimiter.Size = new System.Drawing.Size(33, 22);
            this.Delimiter.TabIndex = 5;
            this.Delimiter.Text = ",";
            this.Delimiter.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Comment
            // 
            this.Comment.Location = new System.Drawing.Point(319, 390);
            this.Comment.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Comment.Name = "Comment";
            this.Comment.Size = new System.Drawing.Size(33, 22);
            this.Comment.TabIndex = 6;
            this.Comment.Text = ";";
            this.Comment.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Previw_window
            // 
            this.Previw_window.Location = new System.Drawing.Point(16, 15);
            this.Previw_window.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Previw_window.Multiline = true;
            this.Previw_window.Name = "Previw_window";
            this.Previw_window.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.Previw_window.Size = new System.Drawing.Size(748, 316);
            this.Previw_window.TabIndex = 7;
            // 
            // Inport_button
            // 
            this.Inport_button.Location = new System.Drawing.Point(664, 387);
            this.Inport_button.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Inport_button.Name = "Inport_button";
            this.Inport_button.Size = new System.Drawing.Size(100, 29);
            this.Inport_button.TabIndex = 9;
            this.Inport_button.Text = "Inport";
            this.Inport_button.UseVisualStyleBackColor = true;
            this.Inport_button.Click += new System.EventHandler(this.Inport_button_Click);
            // 
            // Preview_button
            // 
            this.Preview_button.Location = new System.Drawing.Point(551, 387);
            this.Preview_button.Name = "Preview_button";
            this.Preview_button.Size = new System.Drawing.Size(100, 29);
            this.Preview_button.TabIndex = 10;
            this.Preview_button.Text = "Preview";
            this.Preview_button.UseVisualStyleBackColor = true;
            this.Preview_button.Click += new System.EventHandler(this.Preview_button_Click);
            // 
            // Source_file_import_window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(781, 425);
            this.Controls.Add(this.Preview_button);
            this.Controls.Add(this.Inport_button);
            this.Controls.Add(this.Previw_window);
            this.Controls.Add(this.Comment);
            this.Controls.Add(this.Delimiter);
            this.Controls.Add(this.Comment_label);
            this.Controls.Add(this.Delimiter_label);
            this.Controls.Add(this.Brows_button);
            this.Controls.Add(this.Path_textbox);
            this.Controls.Add(this.File_label);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Source_file_import_window";
            this.Text = "Source File Import Window";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label File_label;
        private System.Windows.Forms.TextBox Path_textbox;
        private System.Windows.Forms.Button Brows_button;
        private System.Windows.Forms.Label Delimiter_label;
        private System.Windows.Forms.Label Comment_label;
        private System.Windows.Forms.TextBox Delimiter;
        private System.Windows.Forms.TextBox Comment;
        private System.Windows.Forms.TextBox Previw_window;
        private System.Windows.Forms.Button Inport_button;
        private System.Windows.Forms.Button Preview_button;
    }
}
namespace BinaryViewerV2
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            result_richTextBox = new RichTextBox();
            filePath_button = new Button();
            offset_textBox = new TextBox();
            offset_button = new Button();
            fileSize_label = new Label();
            SuspendLayout();
            // 
            // result_richTextBox
            // 
            result_richTextBox.Location = new Point(46, 97);
            result_richTextBox.Name = "result_richTextBox";
            result_richTextBox.ReadOnly = true;
            result_richTextBox.Size = new Size(717, 354);
            result_richTextBox.TabIndex = 0;
            result_richTextBox.Text = "";
            result_richTextBox.WordWrap = false;
            result_richTextBox.VScroll += result_richTextBox_VScroll;
            // 
            // filePath_button
            // 
            filePath_button.Location = new Point(46, 38);
            filePath_button.Name = "filePath_button";
            filePath_button.Size = new Size(112, 34);
            filePath_button.TabIndex = 1;
            filePath_button.Text = "Выбрать файл";
            filePath_button.UseVisualStyleBackColor = true;
            filePath_button.Click += filePath_button_Click;
            // 
            // offset_textBox
            // 
            offset_textBox.Location = new Point(520, 45);
            offset_textBox.Name = "offset_textBox";
            offset_textBox.Size = new Size(100, 23);
            offset_textBox.TabIndex = 2;
            // 
            // offset_button
            // 
            offset_button.Location = new Point(626, 38);
            offset_button.Name = "offset_button";
            offset_button.Size = new Size(137, 34);
            offset_button.TabIndex = 3;
            offset_button.Text = "Перейти к смещению";
            offset_button.UseVisualStyleBackColor = true;
            offset_button.Click += offset_button_Click;
            // 
            // fileSize_label
            // 
            fileSize_label.AutoSize = true;
            fileSize_label.Location = new Point(207, 53);
            fileSize_label.Name = "fileSize_label";
            fileSize_label.Size = new Size(0, 15);
            fileSize_label.TabIndex = 4;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(821, 489);
            Controls.Add(fileSize_label);
            Controls.Add(offset_button);
            Controls.Add(offset_textBox);
            Controls.Add(filePath_button);
            Controls.Add(result_richTextBox);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            Text = "BinaryViewerV2";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox result_richTextBox;
        private Button filePath_button;
        private TextBox offset_textBox;
        private Button offset_button;
        private Label fileSize_label;
    }
}
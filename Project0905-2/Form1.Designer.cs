namespace Lesson1_2
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
            this.components = new System.ComponentModel.Container();
            this.Formula = new System.Windows.Forms.Label();
            this.UserAnswerTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.NextButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.ResultLabel = new System.Windows.Forms.Label();
            this.AnswerTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // Formula
            // 
            this.Formula.AutoSize = true;
            this.Formula.Font = new System.Drawing.Font("Microsoft YaHei UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Formula.Location = new System.Drawing.Point(82, 169);
            this.Formula.Name = "Formula";
            this.Formula.Size = new System.Drawing.Size(55, 21);
            this.Formula.TabIndex = 0;
            this.Formula.Text = "label1";
            // 
            // UserAnswerTextBox
            // 
            this.UserAnswerTextBox.Location = new System.Drawing.Point(182, 238);
            this.UserAnswerTextBox.Name = "UserAnswerTextBox";
            this.UserAnswerTextBox.Size = new System.Drawing.Size(100, 23);
            this.UserAnswerTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(88, 244);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Your Answer: ";
            // 
            // NextButton
            // 
            this.NextButton.Location = new System.Drawing.Point(288, 238);
            this.NextButton.Name = "NextButton";
            this.NextButton.Size = new System.Drawing.Size(75, 23);
            this.NextButton.TabIndex = 4;
            this.NextButton.Text = "Next";
            this.NextButton.UseVisualStyleBackColor = true;
            this.NextButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(126, 289);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Result: ";
            // 
            // ResultLabel
            // 
            this.ResultLabel.AutoSize = true;
            this.ResultLabel.Location = new System.Drawing.Point(182, 289);
            this.ResultLabel.Name = "ResultLabel";
            this.ResultLabel.Size = new System.Drawing.Size(43, 17);
            this.ResultLabel.TabIndex = 6;
            this.ResultLabel.Text = "label3";
            // 
            // AnswerTimer
            // 
            this.AnswerTimer.Enabled = true;
            this.AnswerTimer.Interval = 1000;
            this.AnswerTimer.Tick += new System.EventHandler(this.AnswerTimer_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ResultLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.NextButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.UserAnswerTextBox);
            this.Controls.Add(this.Formula);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label Formula;
        private TextBox UserAnswerTextBox;
        private Label label1;
        private Button NextButton;
        private Label label2;
        private Label ResultLabel;
        private System.Windows.Forms.Timer AnswerTimer;
    }
}
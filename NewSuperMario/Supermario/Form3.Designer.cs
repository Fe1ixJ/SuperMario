namespace Supermario
{
    partial class Form3
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
            this.btn_testWorld = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_testWorld
            // 
            this.btn_testWorld.Location = new System.Drawing.Point(92, 100);
            this.btn_testWorld.Name = "btn_testWorld";
            this.btn_testWorld.Size = new System.Drawing.Size(75, 55);
            this.btn_testWorld.TabIndex = 0;
            this.btn_testWorld.Text = "Test World";
            this.btn_testWorld.UseVisualStyleBackColor = true;
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_testWorld);
            this.Name = "Form3";
            this.Text = "Form3";
            this.ResumeLayout(false);

        }

        #endregion

        private Button btn_testWorld;
    }
}
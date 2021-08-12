
namespace CBF_FaceRecognition.Forms
{
    partial class Main
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnObjFaceDetection = new System.Windows.Forms.Button();
            this.btnFilterDemo = new System.Windows.Forms.Button();
            this.btnFaceLiveDetection = new System.Windows.Forms.Button();
            this.btnPasswordValidator = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnObjFaceDetection
            // 
            this.btnObjFaceDetection.Font = new System.Drawing.Font("Lato Black", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnObjFaceDetection.Location = new System.Drawing.Point(12, 12);
            this.btnObjFaceDetection.Name = "btnObjFaceDetection";
            this.btnObjFaceDetection.Size = new System.Drawing.Size(376, 185);
            this.btnObjFaceDetection.TabIndex = 0;
            this.btnObjFaceDetection.Text = "Object/Face Detection";
            this.btnObjFaceDetection.UseVisualStyleBackColor = true;
            // 
            // btnFilterDemo
            // 
            this.btnFilterDemo.Font = new System.Drawing.Font("Lato Black", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFilterDemo.Location = new System.Drawing.Point(12, 203);
            this.btnFilterDemo.Name = "btnFilterDemo";
            this.btnFilterDemo.Size = new System.Drawing.Size(376, 185);
            this.btnFilterDemo.TabIndex = 1;
            this.btnFilterDemo.Text = "Filter Demo";
            this.btnFilterDemo.UseVisualStyleBackColor = true;
            // 
            // btnFaceLiveDetection
            // 
            this.btnFaceLiveDetection.Font = new System.Drawing.Font("Lato Black", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFaceLiveDetection.Location = new System.Drawing.Point(394, 12);
            this.btnFaceLiveDetection.Name = "btnFaceLiveDetection";
            this.btnFaceLiveDetection.Size = new System.Drawing.Size(376, 185);
            this.btnFaceLiveDetection.TabIndex = 2;
            this.btnFaceLiveDetection.Text = "Face Live Detection";
            this.btnFaceLiveDetection.UseVisualStyleBackColor = true;
            // 
            // btnPasswordValidator
            // 
            this.btnPasswordValidator.Font = new System.Drawing.Font("Lato Black", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPasswordValidator.Location = new System.Drawing.Point(394, 203);
            this.btnPasswordValidator.Name = "btnPasswordValidator";
            this.btnPasswordValidator.Size = new System.Drawing.Size(376, 185);
            this.btnPasswordValidator.TabIndex = 3;
            this.btnPasswordValidator.Text = "Password Validator";
            this.btnPasswordValidator.UseVisualStyleBackColor = true;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(783, 403);
            this.Controls.Add(this.btnPasswordValidator);
            this.Controls.Add(this.btnFaceLiveDetection);
            this.Controls.Add(this.btnFilterDemo);
            this.Controls.Add(this.btnObjFaceDetection);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Face Detection";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnObjFaceDetection;
        private System.Windows.Forms.Button btnFilterDemo;
        private System.Windows.Forms.Button btnFaceLiveDetection;
        private System.Windows.Forms.Button btnPasswordValidator;
    }
}


﻿namespace UMLCreator
{
    partial class EditForm
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
            this.components = new System.ComponentModel.Container();
            this.listBox_Properties = new System.Windows.Forms.ListBox();
            this.listBox_Methods = new System.Windows.Forms.ListBox();
            this.label_Name = new System.Windows.Forms.Label();
            this.Type = new System.Windows.Forms.Label();
            this.textBox_Name = new System.Windows.Forms.TextBox();
            this.textBox_Type = new System.Windows.Forms.TextBox();
            this.comboBox_Access = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.btn_Save = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_Class_Name = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBox_Abstract = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.btn_Add_Property = new System.Windows.Forms.Button();
            this.btn_Remove_Property = new System.Windows.Forms.Button();
            this.btn_Remove_Method = new System.Windows.Forms.Button();
            this.btn_Add_Method = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // listBox_Properties
            // 
            this.listBox_Properties.FormattingEnabled = true;
            this.listBox_Properties.HorizontalScrollbar = true;
            this.listBox_Properties.ItemHeight = 15;
            this.listBox_Properties.Location = new System.Drawing.Point(12, 56);
            this.listBox_Properties.Name = "listBox_Properties";
            this.listBox_Properties.Size = new System.Drawing.Size(379, 199);
            this.listBox_Properties.TabIndex = 0;
            this.listBox_Properties.SelectedIndexChanged += new System.EventHandler(this.listBox_Properties_SelectedIndexChanged);
            // 
            // listBox_Methods
            // 
            this.listBox_Methods.FormattingEnabled = true;
            this.listBox_Methods.ItemHeight = 15;
            this.listBox_Methods.Location = new System.Drawing.Point(409, 56);
            this.listBox_Methods.Name = "listBox_Methods";
            this.listBox_Methods.Size = new System.Drawing.Size(379, 199);
            this.listBox_Methods.TabIndex = 1;
            this.listBox_Methods.SelectedIndexChanged += new System.EventHandler(this.listBox_Methods_SelectedIndexChanged);
            // 
            // label_Name
            // 
            this.label_Name.AutoSize = true;
            this.label_Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label_Name.Location = new System.Drawing.Point(12, 288);
            this.label_Name.Name = "label_Name";
            this.label_Name.Size = new System.Drawing.Size(74, 25);
            this.label_Name.TabIndex = 2;
            this.label_Name.Text = "Name:";
            // 
            // Type
            // 
            this.Type.AutoSize = true;
            this.Type.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Type.Location = new System.Drawing.Point(409, 290);
            this.Type.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.Type.Name = "Type";
            this.Type.Size = new System.Drawing.Size(66, 25);
            this.Type.TabIndex = 4;
            this.Type.Text = "Type:";
            // 
            // textBox_Name
            // 
            this.textBox_Name.Location = new System.Drawing.Point(189, 290);
            this.textBox_Name.Name = "textBox_Name";
            this.textBox_Name.Size = new System.Drawing.Size(156, 23);
            this.textBox_Name.TabIndex = 13;
            this.textBox_Name.TextChanged += new System.EventHandler(this.textBox_Name_TextChanged);
            //this.textBox_Name.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_Name_Validating);
            // 
            // textBox_Type
            // 
            this.textBox_Type.Location = new System.Drawing.Point(586, 292);
            this.textBox_Type.Name = "textBox_Type";
            this.textBox_Type.Size = new System.Drawing.Size(156, 23);
            this.textBox_Type.TabIndex = 12;
            this.textBox_Type.TextChanged += new System.EventHandler(this.textBox_Type_TextChanged);
            // 
            // comboBox_Access
            // 
            this.comboBox_Access.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_Access.FormattingEnabled = true;
            this.comboBox_Access.Location = new System.Drawing.Point(189, 331);
            this.comboBox_Access.Name = "comboBox_Access";
            this.comboBox_Access.Size = new System.Drawing.Size(156, 23);
            this.comboBox_Access.TabIndex = 11;
            this.comboBox_Access.SelectedIndexChanged += new System.EventHandler(this.comboBox_Access_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(12, 329);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(171, 25);
            this.label3.TabIndex = 9;
            this.label3.Text = "Access Modifier:";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(12, 430);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(75, 23);
            this.btn_Save.TabIndex = 19;
            this.btn_Save.Text = "Save";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(12, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(131, 30);
            this.label5.TabIndex = 20;
            this.label5.Text = "Class Name:";
            // 
            // textBox_Class_Name
            // 
            this.textBox_Class_Name.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBox_Class_Name.Location = new System.Drawing.Point(163, 6);
            this.textBox_Class_Name.Name = "textBox_Class_Name";
            this.textBox_Class_Name.Size = new System.Drawing.Size(150, 36);
            this.textBox_Class_Name.TabIndex = 21;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(332, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 30);
            this.label6.TabIndex = 22;
            this.label6.Text = "Abstract:";
            // 
            // checkBox_Abstract
            // 
            this.checkBox_Abstract.AutoSize = true;
            this.checkBox_Abstract.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.checkBox_Abstract.Location = new System.Drawing.Point(448, 21);
            this.checkBox_Abstract.Name = "checkBox_Abstract";
            this.checkBox_Abstract.Size = new System.Drawing.Size(15, 14);
            this.checkBox_Abstract.TabIndex = 23;
            this.checkBox_Abstract.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBox1.Location = new System.Drawing.Point(-11, 48);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(823, 2);
            this.pictureBox1.TabIndex = 24;
            this.pictureBox1.TabStop = false;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Location = new System.Drawing.Point(713, 430);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_Cancel.TabIndex = 25;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // btn_Add_Property
            // 
            this.btn_Add_Property.Location = new System.Drawing.Point(332, 259);
            this.btn_Add_Property.Name = "btn_Add_Property";
            this.btn_Add_Property.Size = new System.Drawing.Size(25, 25);
            this.btn_Add_Property.TabIndex = 26;
            this.btn_Add_Property.Text = "+";
            this.btn_Add_Property.UseVisualStyleBackColor = true;
            this.btn_Add_Property.Click += new System.EventHandler(this.btn_Add_Property_Click);
            // 
            // btn_Remove_Property
            // 
            this.btn_Remove_Property.Location = new System.Drawing.Point(366, 259);
            this.btn_Remove_Property.Name = "btn_Remove_Property";
            this.btn_Remove_Property.Size = new System.Drawing.Size(25, 25);
            this.btn_Remove_Property.TabIndex = 27;
            this.btn_Remove_Property.Text = "-";
            this.btn_Remove_Property.UseVisualStyleBackColor = true;
            this.btn_Remove_Property.Click += new System.EventHandler(this.btn_Remove_Property_Click);
            // 
            // btn_Remove_Method
            // 
            this.btn_Remove_Method.Location = new System.Drawing.Point(763, 259);
            this.btn_Remove_Method.Name = "btn_Remove_Method";
            this.btn_Remove_Method.Size = new System.Drawing.Size(25, 25);
            this.btn_Remove_Method.TabIndex = 29;
            this.btn_Remove_Method.Text = "-";
            this.btn_Remove_Method.UseVisualStyleBackColor = true;
            this.btn_Remove_Method.Click += new System.EventHandler(this.btn_Remove_Method_Click);
            // 
            // btn_Add_Method
            // 
            this.btn_Add_Method.Location = new System.Drawing.Point(729, 259);
            this.btn_Add_Method.Name = "btn_Add_Method";
            this.btn_Add_Method.Size = new System.Drawing.Size(25, 25);
            this.btn_Add_Method.TabIndex = 28;
            this.btn_Add_Method.Text = "+";
            this.btn_Add_Method.UseVisualStyleBackColor = true;
            this.btn_Add_Method.Click += new System.EventHandler(this.btn_Add_Method_Click);
            // 
            // EditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.ClientSize = new System.Drawing.Size(800, 465);
            this.Controls.Add(this.btn_Remove_Method);
            this.Controls.Add(this.btn_Add_Method);
            this.Controls.Add(this.btn_Remove_Property);
            this.Controls.Add(this.btn_Add_Property);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.checkBox_Abstract);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox_Class_Name);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.textBox_Name);
            this.Controls.Add(this.textBox_Type);
            this.Controls.Add(this.comboBox_Access);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Type);
            this.Controls.Add(this.label_Name);
            this.Controls.Add(this.listBox_Methods);
            this.Controls.Add(this.listBox_Properties);
            this.Name = "EditForm";
            this.Text = "EditForm";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ListBox listBox_Properties;
        private ListBox listBox_Methods;
        private Label label_Name;
        private Label Type;
        private TextBox textBox_Name;
        private TextBox textBox_Type;
        private ComboBox comboBox_Access;
        private Label label3;
        private ErrorProvider errorProvider;
        private Button btn_Save;
        private CheckBox checkBox_Abstract;
        private Label label6;
        private TextBox textBox_Class_Name;
        private Label label5;
        private PictureBox pictureBox1;
        private Button btn_Cancel;
        private Button btn_Remove_Method;
        private Button btn_Add_Method;
        private Button btn_Remove_Property;
        private Button btn_Add_Property;
    }
}
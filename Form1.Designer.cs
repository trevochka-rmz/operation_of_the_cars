using System.Windows.Forms;

namespace Labss
{
    partial class Form1
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
        private System.Windows.Forms.ListBox listBoxTables;
        private System.Windows.Forms.ListBox listBoxCosts;
        private System.Windows.Forms.ListBox listBoxMiscellaneous;
        #region Windows Form Designer generated code
        private System.Windows.Forms.ListBox listBoxInsurance;
        private System.Windows.Forms.ListBox listBoxMaintenance;
        private System.Windows.Forms.ListBox listBoxFueling;


        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuTables;

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.listBoxTables = new System.Windows.Forms.ListBox();
            this.comboBoxBrands = new System.Windows.Forms.ComboBox();
            this.car_maintenance_costsDataSet = new Labss.Car_maintenance_costsDataSet();
            this.автомобилиBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.автомобилиTableAdapter = new Labss.Car_maintenance_costsDataSetTableAdapters.АвтомобилиTableAdapter();
            this.listBoxCosts = new System.Windows.Forms.ListBox();
            this.listBoxMiscellaneous = new System.Windows.Forms.ListBox();
            this.listBoxInsurance = new System.Windows.Forms.ListBox();
            this.listBoxMaintenance = new System.Windows.Forms.ListBox();
            this.listBoxFueling = new System.Windows.Forms.ListBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.menuTables = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.car_maintenance_costsDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.автомобилиBindingSource)).BeginInit();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(138, 86);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersWidth = 51;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(373, 420);
            this.dataGridView.TabIndex = 0;
            this.dataGridView.SelectionChanged += new System.EventHandler(this.DataGridView_SelectionChanged);
            // 
            // listBoxTables
            // 
            this.listBoxTables.ItemHeight = 16;
            this.listBoxTables.Location = new System.Drawing.Point(12, 86);
            this.listBoxTables.Name = "listBoxTables";
            this.listBoxTables.Size = new System.Drawing.Size(120, 420);
            this.listBoxTables.TabIndex = 1;
            this.listBoxTables.SelectedIndexChanged += new System.EventHandler(this.listBoxTables_SelectedIndexChanged);

            //this.listBoxTables.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.GenericListBox_DrawItem);
            //this.listBoxTables.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.GenericListBox_MeasureItem);
            // 
            // comboBoxBrands
            // 
            this.comboBoxBrands.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBrands.FormattingEnabled = true;
            this.comboBoxBrands.Location = new System.Drawing.Point(12, 45);
            this.comboBoxBrands.Name = "comboBoxBrands";
            this.comboBoxBrands.Size = new System.Drawing.Size(208, 24);
            this.comboBoxBrands.TabIndex = 1;
            this.comboBoxBrands.SelectedIndexChanged += new System.EventHandler(this.ComboBoxBrands_SelectedIndexChanged);
            // 
            // car_maintenance_costsDataSet
            // 
            this.car_maintenance_costsDataSet.DataSetName = "Car_maintenance_costsDataSet";
            this.car_maintenance_costsDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // автомобилиTableAdapter
            // 
            this.автомобилиTableAdapter.ClearBeforeFill = true;
            // 
            // listBoxCosts
            // 
            this.listBoxCosts.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.listBoxCosts.FormattingEnabled = true;
            this.listBoxCosts.ItemHeight = 16;
            this.listBoxCosts.Location = new System.Drawing.Point(520, 35);
            this.listBoxCosts.Name = "listBoxCosts";
            this.listBoxCosts.Size = new System.Drawing.Size(368, 84);
            this.listBoxCosts.TabIndex = 4;
            this.listBoxCosts.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.GenericListBox_DrawItem);
            this.listBoxCosts.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.GenericListBox_MeasureItem);
            // 
            // listBoxMiscellaneous
            // 
            this.listBoxMiscellaneous.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.listBoxMiscellaneous.FormattingEnabled = true;
            this.listBoxMiscellaneous.ItemHeight = 16;
            this.listBoxMiscellaneous.Location = new System.Drawing.Point(520, 136);
            this.listBoxMiscellaneous.Name = "listBoxMiscellaneous";
            this.listBoxMiscellaneous.Size = new System.Drawing.Size(368, 84);
            this.listBoxMiscellaneous.TabIndex = 5;
            this.listBoxMiscellaneous.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.GenericListBox_DrawItem);
            this.listBoxMiscellaneous.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.GenericListBox_MeasureItem);
            // 
            // listBoxInsurance
            // 
            this.listBoxInsurance.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.listBoxInsurance.FormattingEnabled = true;
            this.listBoxInsurance.ItemHeight = 16;
            this.listBoxInsurance.Location = new System.Drawing.Point(520, 243);
            this.listBoxInsurance.Name = "listBoxInsurance";
            this.listBoxInsurance.Size = new System.Drawing.Size(368, 84);
            this.listBoxInsurance.TabIndex = 6;
            this.listBoxInsurance.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.GenericListBox_DrawItem);
            this.listBoxInsurance.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.GenericListBox_MeasureItem);
            // 
            // listBoxMaintenance
            // 
            this.listBoxMaintenance.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.listBoxMaintenance.FormattingEnabled = true;
            this.listBoxMaintenance.ItemHeight = 16;
            this.listBoxMaintenance.Location = new System.Drawing.Point(520, 351);
            this.listBoxMaintenance.Name = "listBoxMaintenance";
            this.listBoxMaintenance.Size = new System.Drawing.Size(368, 84);
            this.listBoxMaintenance.TabIndex = 7;
            this.listBoxMaintenance.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.GenericListBox_DrawItem);
            this.listBoxMaintenance.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.GenericListBox_MeasureItem);
            // 
            // listBoxFueling
            // 
            this.listBoxFueling.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.listBoxFueling.FormattingEnabled = true;
            this.listBoxFueling.ItemHeight = 16;
            this.listBoxFueling.Location = new System.Drawing.Point(520, 457);
            this.listBoxFueling.Name = "listBoxFueling";
            this.listBoxFueling.Size = new System.Drawing.Size(368, 84);
            this.listBoxFueling.TabIndex = 8;
            this.listBoxFueling.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.GenericListBox_DrawItem);
            this.listBoxFueling.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(this.GenericListBox_MeasureItem);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(12, 512);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(120, 25);
            this.buttonAdd.TabIndex = 2;
            this.buttonAdd.Text = "Добавить запись";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(138, 512);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(120, 25);
            this.buttonDelete.TabIndex = 3;
            this.buttonDelete.Text = "Удалить запись";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Location = new System.Drawing.Point(264, 512);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(120, 25);
            this.buttonUpdate.TabIndex = 4;
            this.buttonUpdate.Text = "Изменить запись";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);

            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuTables});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(900, 28);
            this.menuStrip.TabIndex = 5;
            this.menuStrip.Text = "menuStrip";
            // 
            // menuTables
            // 
            this.menuTables.Name = "menuTables";
            this.menuTables.Size = new System.Drawing.Size(85, 24);
            this.menuTables.Text = "Таблицы";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(520, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 16);
            this.label1.TabIndex = 9;
            this.label1.Text = "Затраты";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(520, 117);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 16);
            this.label2.TabIndex = 10;
            this.label2.Text = "Прочие расходы";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(517, 224);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 16);
            this.label3.TabIndex = 11;
            this.label3.Text = "Страхование";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(517, 332);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(191, 16);
            this.label4.TabIndex = 12;
            this.label4.Text = "Техническое обслуживание";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(517, 438);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 16);
            this.label5.TabIndex = 13;
            this.label5.Text = "Топливо";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 546);
            this.Controls.Add(this.label5);
            
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonUpdate);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.menuStrip);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.comboBoxBrands);
            this.Controls.Add(this.listBoxCosts);
            this.Controls.Add(this.listBoxMiscellaneous);
            this.Controls.Add(this.listBoxInsurance);
            this.Controls.Add(this.listBoxMaintenance);
            this.Controls.Add(this.listBoxFueling);
            this.Controls.Add(this.listBoxTables);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "Form1";
            this.Text = "Car Maintenance Costs";
            this.Load += new System.EventHandler(this.MainForm_Load);
            //this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.car_maintenance_costsDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.автомобилиBindingSource)).EndInit();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.ComboBox comboBoxBrands;
        private Car_maintenance_costsDataSet car_maintenance_costsDataSet;
        private System.Windows.Forms.BindingSource автомобилиBindingSource;
        private Car_maintenance_costsDataSetTableAdapters.АвтомобилиTableAdapter автомобилиTableAdapter;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
    }
}

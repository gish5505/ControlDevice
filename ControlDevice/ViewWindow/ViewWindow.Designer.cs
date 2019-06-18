namespace ViewWindow
{
    partial class ViewWindow
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
        protected internal void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.Title title3 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.btnStart = new System.Windows.Forms.Button();
            this.voltageBox = new System.Windows.Forms.TextBox();
            this.outputPendingBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.inputChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.doubleFixedSizeQueueBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.outputActiveBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.voltageAverageBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.outputChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.correctionCheckBox = new System.Windows.Forms.CheckBox();
            this.adcCurrentOutput = new System.Windows.Forms.RadioButton();
            this.generatorPowerOutput = new System.Windows.Forms.RadioButton();
            this.generatorCurrentOutput = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.generatorCurrentShown = new System.Windows.Forms.RadioButton();
            this.generatorPowerShown = new System.Windows.Forms.RadioButton();
            this.dacVoltage = new System.Windows.Forms.RadioButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.inputChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.doubleFixedSizeQueueBindingSource)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.outputChart)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(786, 606);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(136, 41);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "Старт";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.startButton_Click);
            // 
            // voltageBox
            // 
            this.voltageBox.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.voltageBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.voltageBox.Location = new System.Drawing.Point(9, 32);
            this.voltageBox.Name = "voltageBox";
            this.voltageBox.ReadOnly = true;
            this.voltageBox.Size = new System.Drawing.Size(195, 13);
            this.voltageBox.TabIndex = 6;
            // 
            // outputPendingBox
            // 
            this.outputPendingBox.Location = new System.Drawing.Point(9, 33);
            this.outputPendingBox.Name = "outputPendingBox";
            this.outputPendingBox.Size = new System.Drawing.Size(64, 20);
            this.outputPendingBox.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Enabled = false;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Вольтаж на магнетроне, В";
            // 
            // inputChart
            // 
            this.inputChart.BackColor = System.Drawing.Color.Transparent;
            this.inputChart.BackSecondaryColor = System.Drawing.Color.Transparent;
            chartArea1.Name = "ChartArea1";
            this.inputChart.ChartAreas.Add(chartArea1);
            this.inputChart.DataSource = this.doubleFixedSizeQueueBindingSource;
            this.inputChart.Location = new System.Drawing.Point(12, 28);
            this.inputChart.Name = "inputChart";
            this.inputChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
            this.inputChart.Size = new System.Drawing.Size(685, 299);
            this.inputChart.TabIndex = 10;
            this.inputChart.Text = "inputChart";
            title1.Alignment = System.Drawing.ContentAlignment.TopCenter;
            title1.DockedToChartArea = "ChartArea1";
            title1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            title1.DockingOffset = -2;
            title1.IsDockedInsideChartArea = false;
            title1.Name = "Title1";
            title1.Text = "Время, сек";
            this.inputChart.Titles.Add(title1);
            // 
            // doubleFixedSizeQueueBindingSource
            // 
            this.doubleFixedSizeQueueBindingSource.DataSource = typeof(ControlDevice.Calculations.DoubleFixedSizeQueue);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(118, 33);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(89, 26);
            this.button1.TabIndex = 11;
            this.button1.Text = "Применить";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.pushButton_Click);
            // 
            // outputActiveBox
            // 
            this.outputActiveBox.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.outputActiveBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.outputActiveBox.Location = new System.Drawing.Point(6, 132);
            this.outputActiveBox.Name = "outputActiveBox";
            this.outputActiveBox.ReadOnly = true;
            this.outputActiveBox.Size = new System.Drawing.Size(198, 13);
            this.outputActiveBox.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 116);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(201, 13);
            this.label3.TabIndex = 13;
            this.label3.Text = "Активное значение тока на выход, мА";
            // 
            // voltageAverageBox
            // 
            this.voltageAverageBox.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.voltageAverageBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.voltageAverageBox.Location = new System.Drawing.Point(9, 82);
            this.voltageAverageBox.Name = "voltageAverageBox";
            this.voltageAverageBox.ReadOnly = true;
            this.voltageAverageBox.Size = new System.Drawing.Size(195, 13);
            this.voltageAverageBox.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(136, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Усредненный вольтаж, В";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 660);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(946, 22);
            this.statusStrip1.TabIndex = 16;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.outputPendingBox);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Location = new System.Drawing.Point(703, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(219, 80);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text ="Выставленное значение тока, мА";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.voltageBox);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.voltageAverageBox);
            this.groupBox2.Controls.Add(this.outputActiveBox);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(706, 294);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(220, 168);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Активные значения";
            // 
            // outputChart
            // 
            this.outputChart.BackColor = System.Drawing.Color.Transparent;
            chartArea2.Name = "ChartArea1";
            this.outputChart.ChartAreas.Add(chartArea2);
            this.outputChart.Location = new System.Drawing.Point(12, 333);
            this.outputChart.Name = "outputChart";
            this.outputChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
            this.outputChart.Size = new System.Drawing.Size(685, 314);
            this.outputChart.TabIndex = 19;
            this.outputChart.Text = "chart2";
            title2.Alignment = System.Drawing.ContentAlignment.BottomCenter;
            title2.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            title2.Name = "Title1";
            title2.Text = "Время, сек";
            title3.Alignment = System.Drawing.ContentAlignment.BottomCenter;
            title3.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Left;
            title3.DockingOffset = 2;
            title3.Name = "Title2";
            title3.Text = "Напряжение ЦАП, В";
            this.outputChart.Titles.Add(title2);
            this.outputChart.Titles.Add(title3);
            // 
            // correctionCheckBox
            // 
            this.correctionCheckBox.AutoSize = true;
            this.correctionCheckBox.Location = new System.Drawing.Point(9, 19);
            this.correctionCheckBox.Name = "correctionCheckBox";
            this.correctionCheckBox.Size = new System.Drawing.Size(118, 17);
            this.correctionCheckBox.TabIndex = 20;
            this.correctionCheckBox.Text = "Режим коррекции";
            this.correctionCheckBox.UseVisualStyleBackColor = true;
            this.correctionCheckBox.CheckedChanged += new System.EventHandler(this.correctionCheckBox_Checked);
            // 
            // adcCurrentOutput
            // 
            this.adcCurrentOutput.AutoSize = true;
            this.adcCurrentOutput.Location = new System.Drawing.Point(6, 21);
            this.adcCurrentOutput.Name = "adcCurrentOutput";
            this.adcCurrentOutput.Size = new System.Drawing.Size(91, 17);
            this.adcCurrentOutput.TabIndex = 16;
            this.adcCurrentOutput.Text = "Ток АЦП, мА";
            this.adcCurrentOutput.UseVisualStyleBackColor = true;
            // 
            // generatorPowerOutput
            // 
            this.generatorPowerOutput.AutoSize = true;
            this.generatorPowerOutput.Location = new System.Drawing.Point(6, 44);
            this.generatorPowerOutput.Name = "generatorPowerOutput";
            this.generatorPowerOutput.Size = new System.Drawing.Size(157, 17);
            this.generatorPowerOutput.TabIndex = 21;
            this.generatorPowerOutput.Text = "Мощность генератора, Вт";
            this.generatorPowerOutput.UseVisualStyleBackColor = true;
            // 
            // generatorCurrentOutput
            // 
            this.generatorCurrentOutput.AutoSize = true;
            this.generatorCurrentOutput.Location = new System.Drawing.Point(6, 67);
            this.generatorCurrentOutput.Name = "generatorCurrentOutput";
            this.generatorCurrentOutput.Size = new System.Drawing.Size(118, 17);
            this.generatorCurrentOutput.TabIndex = 22;
            this.generatorCurrentOutput.Text = "Ток генератора, А";
            this.generatorCurrentOutput.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.generatorCurrentOutput);
            this.groupBox3.Controls.Add(this.adcCurrentOutput);
            this.groupBox3.Controls.Add(this.generatorPowerOutput);
            this.groupBox3.Location = new System.Drawing.Point(706, 102);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(216, 90);
            this.groupBox3.TabIndex = 23;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Управляющие параметры";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.generatorCurrentShown);
            this.groupBox4.Controls.Add(this.generatorPowerShown);
            this.groupBox4.Controls.Add(this.dacVoltage);
            this.groupBox4.Location = new System.Drawing.Point(706, 198);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(216, 90);
            this.groupBox4.TabIndex = 24;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Отображаемые параметры";
            // 
            // generatorCurrentShown
            // 
            this.generatorCurrentShown.AutoSize = true;
            this.generatorCurrentShown.Location = new System.Drawing.Point(6, 67);
            this.generatorCurrentShown.Name = "generatorCurrentShown";
            this.generatorCurrentShown.Size = new System.Drawing.Size(118, 17);
            this.generatorCurrentShown.TabIndex = 25;
            this.generatorCurrentShown.TabStop = true;
            this.generatorCurrentShown.Text = "Ток генератора, А";
            this.generatorCurrentShown.UseVisualStyleBackColor = true;
            // 
            // generatorPowerShown
            // 
            this.generatorPowerShown.AutoSize = true;
            this.generatorPowerShown.Location = new System.Drawing.Point(6, 44);
            this.generatorPowerShown.Name = "generatorPowerShown";
            this.generatorPowerShown.Size = new System.Drawing.Size(157, 17);
            this.generatorPowerShown.TabIndex = 1;
            this.generatorPowerShown.TabStop = true;
            this.generatorPowerShown.Text = "Мощность генератора, Вт";
            this.generatorPowerShown.UseVisualStyleBackColor = true;
            // 
            // dacVoltage
            // 
            this.dacVoltage.AutoSize = true;
            this.dacVoltage.Location = new System.Drawing.Point(6, 21);
            this.dacVoltage.Name = "dacVoltage";
            this.dacVoltage.Size = new System.Drawing.Size(128, 17);
            this.dacVoltage.TabIndex = 0;
            this.dacVoltage.TabStop = true;
            this.dacVoltage.Text = "Напряжение ЦАП, В";
            this.dacVoltage.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.button2);
            this.groupBox5.Controls.Add(this.textBox1);
            this.groupBox5.Controls.Add(this.comboBox1);
            this.groupBox5.Controls.Add(this.correctionCheckBox);
            this.groupBox5.Location = new System.Drawing.Point(706, 468);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(220, 98);
            this.groupBox5.TabIndex = 25;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Прочие функции";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(139, 61);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 26;
            this.button2.Text = "Запуск";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(6, 42);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(198, 13);
            this.textBox1.TabIndex = 27;
            this.textBox1.Text = "Автоматическая генерация импульса";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Меандр"});
            this.comboBox1.Location = new System.Drawing.Point(6, 61);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 26;
            // 
            // ViewWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(946, 682);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.outputChart);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.inputChart);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "ViewWindow";
            this.Text = "magcontrol";
            this.Load += new System.EventHandler(this.ViewWindow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.inputChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.doubleFixedSizeQueueBindingSource)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.outputChart)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox voltageBox;
        private System.Windows.Forms.TextBox outputPendingBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataVisualization.Charting.Chart inputChart;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox outputActiveBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox voltageAverageBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.BindingSource doubleFixedSizeQueueBindingSource;
        private System.Windows.Forms.DataVisualization.Charting.Chart outputChart;
        private System.Windows.Forms.CheckBox correctionCheckBox;
        private System.Windows.Forms.RadioButton adcCurrentOutput;
        private System.Windows.Forms.RadioButton generatorPowerOutput;
        private System.Windows.Forms.RadioButton generatorCurrentOutput;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton generatorPowerShown;
        private System.Windows.Forms.RadioButton dacVoltage;
        private System.Windows.Forms.RadioButton generatorCurrentShown;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}


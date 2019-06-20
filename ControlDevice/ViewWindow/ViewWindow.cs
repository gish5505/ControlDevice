using ControlDevice.Calculations;
using ControlDevice.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

//set this project as startup to enable window mode

namespace ViewWindow
{
    public partial class ViewWindow : Form
    {

        private bool _isStarted = false;
        private bool _useMock = true;
        private CalculationViewModel _viewModel;

        private SynchronizedNotifyPropertyChanged<CalculationViewModel> _threadSafeVM;

        public ViewWindow()
        {
            InitializeComponent();

            var intervals = (RangeConfiguration)ConfigurationManager.GetSection("RangeConfiguration");

            _viewModel = new CalculationViewModel(intervals.Ranges);

            _threadSafeVM = new SynchronizedNotifyPropertyChanged<CalculationViewModel>(_viewModel, this);
        }


        public void startButton_Click(object sender, EventArgs e) //event for start/stop button
        {
            if (!_isStarted)
            {
                _isStarted = !_isStarted;
                btnStart.Text = (_isStarted) ? "Стоп" : "Старт";

                _viewModel.Start();
                _viewModel.OutputBoardPush(0);

            }
            else
            {
                _isStarted = !_isStarted;
                btnStart.Text = (_isStarted) ? "Стоп" : "Старт";

                _viewModel.OutputBoardPush(0);
                _viewModel.Stop();

            }
        }

        private void BindControls()
        {
            voltageBox.DataBindings.Add(new Binding("Text", _threadSafeVM, "InboundVoltage") { DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged });
            voltageAverageBox.DataBindings.Add(new Binding("Text", _threadSafeVM, "InboundVoltageAverage") { DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged });
            outputActiveBox.DataBindings.Add(new Binding("Text", _threadSafeVM, "OutboundCurrentActive") { DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged });
        }


        private void pushButton_Click(object sender, EventArgs e)
        {
            bool isParsable = float.TryParse(outputPendingBox.Text,System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture, out float result);
            if (_isStarted & isParsable)
            {
                float pushValue = float.Parse(outputPendingBox.Text, System.Globalization.CultureInfo.InvariantCulture);
                _viewModel.OutputBoardPush(pushValue);
                outputActiveBox.Text = _viewModel.OutboundCurrentActive.ToString();
            }

        }



        private void ViewWindow_Load(object sender, EventArgs e)
        {

            InputChartSetup();
            OutputChartSetup();

            BindControls();

            drawChart();
            FormClosing += ViewWindow_FormClosing;

            adcCurrentOutput.CheckedChanged += RadioButtons_CheckedChanged;
            generatorPowerOutput.CheckedChanged += RadioButtons_CheckedChanged;
            generatorCurrentOutput.CheckedChanged += RadioButtons_CheckedChanged;
            dacVoltage.CheckedChanged += RadioButtons_CheckedChanged;
            generatorPowerShown.CheckedChanged += RadioButtons_CheckedChanged;
            generatorCurrentShown.CheckedChanged += RadioButtons_CheckedChanged;
        }

        private void ViewWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            _viewModel.OutputBoardPush(0);
        }

        private void RadioButtons_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;

            if (radioButton.Checked == true)
            {
                switch (radioButton.Name)
                {
                    case "dacVoltage":

                        inputChart.Series["YInternalQueueValues"].Enabled = true;
                        inputChart.Series["YAnodeCurrentValues"].Enabled = false;
                        inputChart.Series["YPowerValues"].Enabled = false;
                        inputChart.ChartAreas[0].AxisY.Maximum = 10;
                        inputChart.Titles[1].Text = "Напряжение, В";
                        groupBox1.Text = "Выставленное значение тока, мА";
                        outputPendingBox.Clear();
                        break;

                    case "generatorPowerShown":
                        inputChart.Series["YInternalQueueValues"].Enabled = false;
                        inputChart.Series["YAnodeCurrentValues"].Enabled = false;
                        inputChart.Series["YPowerValues"].Enabled = true;
                        inputChart.Titles[1].Text = "Мощность, кВт";
                        inputChart.ChartAreas[0].AxisY.Maximum = 20;
                        groupBox1.Text = "Выставленное значение мощности, кВт";
                        outputPendingBox.Clear();
                        break;

                    case "generatorCurrentShown" :
                        inputChart.Series["YInternalQueueValues"].Enabled = false;
                        inputChart.Series["YAnodeCurrentValues"].Enabled = true;
                        inputChart.Series["YPowerValues"].Enabled = false;
                        inputChart.Titles[1].Text = "Ток анода, А";
                        groupBox1.Text = "Выставленное значение анодного тока, А";
                        inputChart.ChartAreas[0].AxisY.Maximum = 10;
                        outputPendingBox.Clear();
                        break;

                    case "adcCurrentOutput":

                        break;

                    case "generatorPowerOutput":

                        break;
                    case "generatorCurrentOutput":

                        break;

                }
            }
        }

        private void drawChart()
        {
            int _pointRefreshLimitYAxis = 0;


            _threadSafeVM.PropertyChanged += (s, args) =>
            {
                if (args.PropertyName == "InternalQueue")
                {
                    //var point = ((SynchronizedNotifyPropertyChanged<CalculationViewModel>)s).Source.Values.Queue.Last();

                    if (_viewModel.InternalQueue.Queue != null)
                    {
                        _pointRefreshLimitYAxis = _viewModel.InternalQueue.Queue.Count();
                    }

                    inputChart.Series["YInternalQueueValues"].Points.Clear();

                    //chart1.ChartAreas[0].AxisX.Maximum += 1;
                    //chart1.ChartAreas[0].AxisX.Minimum += 1;

                    for (int _localPointCounter = 0; _localPointCounter < _pointRefreshLimitYAxis - 1; ++_localPointCounter)
                    {
                        //chart1.Series["YValues"].Points.AddXY(_viewModel.XAxisTimerQueue.Queue.ElementAt(_localPointCounter), _viewModel.InternalQueue.Queue.ElementAt(_localPointCounter));
                        inputChart.Series["YInternalQueueValues"].Points.AddY(_viewModel.InternalQueue.Queue.ElementAt(_localPointCounter));
                    }
                }

                if (args.PropertyName == "OutboundAnodeCurrentActive")
                {
                    if (_viewModel.InternalOutputQueue.Queue != null)
                    {
                        _pointRefreshLimitYAxis = _viewModel.InternalOutputAnodeQueue.Queue.Count();
                    }

                    inputChart.Series["YAnodeCurrentValues"].Points.Clear();

                    for (int _localPointCounter = 0; _localPointCounter < _pointRefreshLimitYAxis - 1; ++_localPointCounter)
                    {
                        //chart1.Series["YValues"].Points.AddXY(_viewModel.XAxisTimerQueue.Queue.ElementAt(_localPointCounter), _viewModel.InternalQueue.Queue.ElementAt(_localPointCounter));
                        inputChart.Series["YAnodeCurrentValues"].Points.AddY(_viewModel.InternalOutputAnodeQueue.Queue.ElementAt(_localPointCounter));
                    }

                }

                if (args.PropertyName == "OutboundPowerActive")
                {
                    if (_viewModel.InternalOutputQueue.Queue != null)
                    {
                        _pointRefreshLimitYAxis = _viewModel.InternalOutputPowerQueue.Queue.Count();
                    }

                    inputChart.Series["YPowerValues"].Points.Clear();

                    for (int _localPointCounter = 0; _localPointCounter < _pointRefreshLimitYAxis - 1; ++_localPointCounter)
                    {
                        //chart1.Series["YValues"].Points.AddXY(_viewModel.XAxisTimerQueue.Queue.ElementAt(_localPointCounter), _viewModel.InternalQueue.Queue.ElementAt(_localPointCounter));
                        inputChart.Series["YPowerValues"].Points.AddY(_viewModel.InternalOutputPowerQueue.Queue.ElementAt(_localPointCounter));
                    }

                }

                if (args.PropertyName == "OutboundCurrentActive")
                {
                    if (_viewModel.InternalOutputQueue.Queue != null)
                    {
                        _pointRefreshLimitYAxis = _viewModel.InternalOutputQueue.Queue.Count();
                    }

                    outputChart.Series["YOutboundCurrentActiveValues"].Points.Clear();

                    for (int _localPointCounter = 0; _localPointCounter < _pointRefreshLimitYAxis - 1; ++_localPointCounter)
                    {
                        //chart1.Series["YValues"].Points.AddXY(_viewModel.XAxisTimerQueue.Queue.ElementAt(_localPointCounter), _viewModel.InternalQueue.Queue.ElementAt(_localPointCounter));
                        outputChart.Series["YOutboundCurrentActiveValues"].Points.AddY(_viewModel.InternalOutputQueue.Queue.ElementAt(_localPointCounter));
                    }
                }

                if (args.PropertyName == "AngleValueK")
                {
                    if (_viewModel.AngleValueK.Queue != null)
                    {
                        _pointRefreshLimitYAxis = _viewModel.AngleValueK.Queue.Count();
                    }

                    outputChart.Series["YAngleValueKValues"].Points.Clear();

                    for (int _localPointCounter = 0; _localPointCounter < _pointRefreshLimitYAxis - 1; ++_localPointCounter)
                    {
                        //chart1.Series["YValues"].Points.AddXY(_viewModel.XAxisTimerQueue.Queue.ElementAt(_localPointCounter), _viewModel.InternalQueue.Queue.ElementAt(_localPointCounter));
                        outputChart.Series["YAngleValueKValues"].Points.AddY(_viewModel.AngleValueK.Queue.ElementAt(_localPointCounter));
                    }
                }
            };

        }

        public void InputChartSetup()
        {
            string[] _axisLabels = { "10", "9", "8", "7", "6", "5", "4", "3", "2", "1","0" };

            int startOffset = -2;
            int endOffset = 2;
            foreach (string _axisNumber in _axisLabels)
            {
                CustomLabel _axisLabel = new CustomLabel(startOffset, endOffset, _axisNumber, 0, LabelMarkStyle.None);
                inputChart.ChartAreas[0].AxisX.CustomLabels.Add(_axisLabel);
                outputChart.ChartAreas[0].AxisX.CustomLabels.Add(_axisLabel);
                startOffset = startOffset + 10;
                endOffset = endOffset + 10;
            }

            if (!inputChart.Series.Any(s => s.Name == "YInternalQueueValues"))
            {
                inputChart.Series.Add("YInternalQueueValues");
                inputChart.Series.Add("YAnodeCurrentValues");
                inputChart.Series.Add("YPowerValues");
            }

            inputChart.Series["YInternalQueueValues"].ChartType = SeriesChartType.Spline;
            inputChart.Series["YAnodeCurrentValues"].ChartType = SeriesChartType.Spline;
            inputChart.Series["YPowerValues"].ChartType = SeriesChartType.Spline;

            inputChart.ChartAreas[0].AxisX.LabelStyle.Format = "mm:ss";

            inputChart.ChartAreas[0].AxisX.Maximum = 100;
            inputChart.ChartAreas[0].AxisX.Minimum = 0;

            inputChart.ChartAreas[0].AxisY.Maximum = 15;

            inputChart.ChartAreas[0].AxisX.MajorGrid.Interval = 10;
            inputChart.ChartAreas[0].AxisX.LabelStyle.Interval = 10;

        }

        public void OutputChartSetup()
        {
            outputChart.Series.Add("YOutboundCurrentActiveValues");
            outputChart.Series.Add("YAngleValueKValues");

            outputChart.Series["YOutboundCurrentActiveValues"].ChartType = SeriesChartType.Spline;
            outputChart.Series["YAngleValueKValues"].ChartType = SeriesChartType.Spline;


            //chart1.ChartAreas[0].AxisX.LabelStyle.Format = "mm:ss";

            outputChart.ChartAreas[0].AxisX.IsMarginVisible = false;

            outputChart.ChartAreas[0].AxisX.Maximum = 100;
            outputChart.ChartAreas[0].AxisX.Minimum = 0;

            outputChart.ChartAreas[0].AxisY.Maximum = 10;

            outputChart.ChartAreas[0].AxisX.MajorGrid.Interval = 5;
            outputChart.ChartAreas[0].AxisX.LabelStyle.Interval = 10;

        }

        private void correctionCheckBox_Checked(object sender, EventArgs e)
        {
            switch(correctionCheckBox.CheckState)
            {
                case (CheckState.Checked):
                    outputPendingBox.Text = "yes";
                    break;
                case (CheckState.Unchecked):
                    outputPendingBox.Text = "no";
                    break;
            }
        }

    }


    
}

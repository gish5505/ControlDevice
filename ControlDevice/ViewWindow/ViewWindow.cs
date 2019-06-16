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

//set this project as startup to enable window mode

namespace ViewWindow
{
    public partial class ViewWindow : Form
    {

        private bool _isStarted = false;
        private bool _useMock = true;
        private CalculationViewModel _viewModel;

        private SynchronizedNotifyPropertyChanged<CalculationViewModel> _threadSafeVM;

        public DoubleFixedSizeQueue _chartInputQueue { get; private set; }
        public DoubleFixedSizeQueue _chartOutputQueue { get; private set; }

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
            bool isParsable = float.TryParse(outputPendingBox.Text, out float result);
            if (_isStarted & isParsable)
            {
                float pushValue = float.Parse(outputPendingBox.Text, System.Globalization.CultureInfo.InvariantCulture);



                _viewModel.OutputBoardPush(pushValue);
                //outputActiveBox.Text = outputPendingBox.Text;
                outputActiveBox.Text = _viewModel.OutboundCurrentActive.ToString();
            }

        }


        private void ViewWindow_Load(object sender, EventArgs e)
        {

            #region OutputChartSetup
            outputChart.Series.Add("YValues");

            outputChart.Series["YValues"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;

            //chart1.ChartAreas[0].AxisX.LabelStyle.Format = "mm:ss";

            outputChart.ChartAreas[0].AxisX.IsMarginVisible = false;

            outputChart.ChartAreas[0].AxisX.Maximum = 100;
            outputChart.ChartAreas[0].AxisX.Minimum = 0;

            outputChart.ChartAreas[0].AxisY.Maximum = 10;

            outputChart.ChartAreas[0].AxisX.MajorGrid.Interval = 5;
            outputChart.ChartAreas[0].AxisX.LabelStyle.Interval = 5;
            #endregion
            _chartInputQueue = new DoubleFixedSizeQueue(100);

            InputChartSetup();

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
                    case "adcCurrentOutput":
                        inputChart.Series["YInternalQueueValues"].Enabled = true;
                        inputChart.Series["YAnodeCurrentValues"].Enabled = false;
                        outputPendingBox.Text = "1";
                        break;

                    case "generatorPowerOutput":
                        inputChart.Series["YInternalQueueValues"].Enabled = false;
                        inputChart.Series["YAnodeCurrentValues"].Enabled = true;
                        outputPendingBox.Text = "2";
                        break;

                    case "generatorCurrentOutput":
                        outputPendingBox.Text = "3";
                        break;

                    case "dacVoltage":

                        break;

                    case "generatorPowerShown":

                        break;
                    case "generatorCurrentShown":

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

                    if (_chartInputQueue != null)
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

                    outputChart.Series["YValues"].Points.Clear();

                    for (int _localPointCounter = 0; _localPointCounter < _pointRefreshLimitYAxis - 1; ++_localPointCounter)
                    {
                        //chart1.Series["YValues"].Points.AddXY(_viewModel.XAxisTimerQueue.Queue.ElementAt(_localPointCounter), _viewModel.InternalQueue.Queue.ElementAt(_localPointCounter));
                        outputChart.Series["YValues"].Points.AddY(_viewModel.InternalOutputQueue.Queue.ElementAt(_localPointCounter));
                    }
                }


            };

        }

        public void InputChartSetup()
        {
            if (!inputChart.Series.Any(s => s.Name == "YInternalQueueValues"))
            {
                inputChart.Series.Add("YInternalQueueValues");
                inputChart.Series.Add("YAnodeCurrentValues");
                inputChart.Series.Add("YPowerValues");
            }

            inputChart.Series["YInternalQueueValues"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            inputChart.Series["YAnodeCurrentValues"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;

            //chart1.ChartAreas[0].AxisX.LabelStyle.Format = "mm:ss";

            inputChart.ChartAreas[0].AxisX.IsMarginVisible = false;

            inputChart.ChartAreas[0].AxisX.Maximum = 100;
            inputChart.ChartAreas[0].AxisX.Minimum = 0;

            inputChart.ChartAreas[0].AxisY.Maximum = 10;

            inputChart.ChartAreas[0].AxisX.MajorGrid.Interval = 5;
            inputChart.ChartAreas[0].AxisX.LabelStyle.Interval = 5;

        }

    }


    
}

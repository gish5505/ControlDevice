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
                
            }
            else
            {
                _isStarted = !_isStarted;
                btnStart.Text = (_isStarted) ? "Стоп" : "Старт";

                _viewModel.Stop();
                
            }
        }        

        private void BindControls()
        {                        
            voltageBox.DataBindings.Add(new Binding("Text", _threadSafeVM, "InboundVoltage") { DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged });
            voltageAverageBox.DataBindings.Add(new Binding("Text", _threadSafeVM, "InboundVoltageAverage") { DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged });                                                            
        }
       

        private void pushButton_Click(object sender, EventArgs e)
        {
            //bool isParsable= float.TryParse(outputPendingBox.Text, out float result)
            if (_isStarted /*& isParsable*/) 
            {
                float pushValue = float.Parse(outputPendingBox.Text, System.Globalization.CultureInfo.InvariantCulture);



                _viewModel.OutputBoardPush(pushValue);
                outputActiveBox.Text = outputPendingBox.Text;
            }

        }

        

        private void ViewWindow_Load(object sender, EventArgs e)
        {
            chart1.Series.Add("YValues");
            chart1.Series["YValues"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;

            //chart1.ChartAreas[0].AxisX.LabelStyle.Format = "mm:ss";
            
            chart1.ChartAreas[0].AxisX.IsMarginVisible = false;

            chart1.ChartAreas[0].AxisX.Maximum = 100;
            chart1.ChartAreas[0].AxisX.Minimum = 0;

            chart1.ChartAreas[0].AxisY.Maximum = 5;

            chart1.ChartAreas[0].AxisX.MajorGrid.Interval = 5;
            chart1.ChartAreas[0].AxisX.LabelStyle.Interval = 5;
            

            BindControls();

            int _pointRefreshLimitYAxis = 0;

            _threadSafeVM.PropertyChanged += (s, args) => {
                if (args.PropertyName == "InternalQueue")
                {
                    //var point = ((SynchronizedNotifyPropertyChanged<CalculationViewModel>)s).Source.Values.Queue.Last();
                    if (_viewModel.InternalQueue.Queue != null)
                    {
                        _pointRefreshLimitYAxis = _viewModel.InternalQueue.Queue.Count();
                    }

                    chart1.Series["YValues"].Points.Clear();

                    //chart1.ChartAreas[0].AxisX.Maximum += 1;
                    //chart1.ChartAreas[0].AxisX.Minimum += 1;

                    for (int _localPointCounter = 0;  _localPointCounter < _pointRefreshLimitYAxis - 1; ++_localPointCounter)
                    {
                        //chart1.Series["YValues"].Points.AddXY(_viewModel.XAxisTimerQueue.Queue.ElementAt(_localPointCounter), _viewModel.InternalQueue.Queue.ElementAt(_localPointCounter));
                        chart1.Series["YValues"].Points.AddY(_viewModel.InternalQueue.Queue.ElementAt(_localPointCounter));
                    }
                }


            };
        }


    }


    
}

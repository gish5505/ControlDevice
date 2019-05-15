using ControlDevice.Calculations;
using ControlDevice.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private CalculationViewModel _vm = new CalculationViewModel();

        private SynchronizedNotifyPropertyChanged<CalculationViewModel> _threadSafeVM;

        public ViewWindow()
        {
            InitializeComponent();
            
            _threadSafeVM = new SynchronizedNotifyPropertyChanged<CalculationViewModel>(_vm, this);
        }


        public void startButton_Click(object sender, EventArgs e) //event for start/stop button
        {
            if (!_isStarted)
            {
                _isStarted = !_isStarted;
                btnStart.Text = (_isStarted) ? "Стоп" : "Старт";
                
                _vm.Start();
                
            }
            else
            {
                _isStarted = !_isStarted;
                btnStart.Text = (_isStarted) ? "Стоп" : "Старт";

                _vm.Stop();
                
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
                _vm.OutputBoardPush(pushValue);
                outputActiveBox.Text = outputPendingBox.Text;
            }

        }

        

        private void ViewWindow_Load(object sender, EventArgs e)
        {
            this.chart1.Series.Add("YValues");
            this.chart1.Series["YValues"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            chart1.ChartAreas[0].AxisX.IsMarginVisible = false;
            SetStyle(ControlStyles.FixedWidth, true);

            BindControls();

            int _pointRefreshLimit = 0;

            _threadSafeVM.PropertyChanged += (s, args) => {
                if (args.PropertyName == "InternalQueue")
                {
                    //var point = ((SynchronizedNotifyPropertyChanged<CalculationViewModel>)s).Source.Values.Queue.Last();
                    if (_vm.InternalQueue.Queue != null)
                    {
                        _pointRefreshLimit = _vm.InternalQueue.Queue.Count();
                    }

                    chart1.Series["YValues"].Points.Clear();
                    chart1.ChartAreas[0].AxisX.Maximum = 100;
                    chart1.ChartAreas[0].AxisY.Maximum = 5;



                    for (int _localPointCounter = 0;  _localPointCounter < _pointRefreshLimit - 1; ++_localPointCounter)
                    {
                        this.chart1.Series["YValues"].Points.AddY(_vm.InternalQueue.Queue.ElementAt(_localPointCounter));
                    }
                }
            };
        }


    }


    public class SynchronizedNotifyPropertyChanged<T> : INotifyPropertyChanged, ICustomTypeDescriptor           //threadsafe ipropertychanged
        where T : INotifyPropertyChanged
    {
        private readonly T _source;
        private readonly ISynchronizeInvoke _syncObject;

        public SynchronizedNotifyPropertyChanged(T source, ISynchronizeInvoke syncObject)
        {
            _source = source;
            _syncObject = syncObject;

            _source.PropertyChanged += (sender, args) => OnPropertyChanged(args.PropertyName);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null) return;

            var handler = PropertyChanged;
            _syncObject.BeginInvoke(handler, new object[] { this, new PropertyChangedEventArgs(propertyName) });
        }

        public T Source { get { return _source; } }

        #region ICustomTypeDescriptor
        public AttributeCollection GetAttributes()
        {
            return new AttributeCollection(null);
        }

        public string GetClassName()
        {
            return TypeDescriptor.GetClassName(typeof(T));
        }

        public string GetComponentName()
        {
            return TypeDescriptor.GetComponentName(typeof(T));
        }

        public TypeConverter GetConverter()
        {
            return TypeDescriptor.GetConverter(typeof(T));
        }

        public EventDescriptor GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(typeof(T));
        }

        public PropertyDescriptor GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(typeof(T));
        }

        public object GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(typeof(T), editorBaseType);
        }

        public EventDescriptorCollection GetEvents()
        {
            return TypeDescriptor.GetEvents(typeof(T));
        }

        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(typeof(T), attributes);
        }

        public PropertyDescriptorCollection GetProperties()
        {
            return TypeDescriptor.GetProperties(typeof(T));
        }

        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            return TypeDescriptor.GetProperties(typeof(T), attributes);
        }

        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return _source;
        }
        #endregion ICustomTypeDescriptor
    }
}

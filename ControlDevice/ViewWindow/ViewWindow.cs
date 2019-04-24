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

        public ViewWindow()
        {
            InitializeComponent();
            
            
        }


        public void button1_Click(object sender, EventArgs e) //event for start/stop button
        {
            if (!_isStarted)
            {
                _isStarted = !_isStarted;
                btnStart.Text = (_isStarted) ? "Стоп" : "Старт";
                

                _vm.Start();

                //if (txtResult.InvokeRequired)
                //await Task.Run(() => txtResult.Invoke( new Action(() => { _vm.Start(); })));
                //await Task.Run(() =>  { _vm.Start(); }).ConfigureAwait(true);


                #region
                /*try
                {
                   using (IListenerBoard board = GetListenerBoard(0))
                    {
                    
                        try
                        {
                            for (int i = 0; i < 100; i++)
                            {
                                board.CardSearch();
                                //var results = 1; board.ReadBuffer();

                                var toShow = String.Join(",", results);
                                txtResult.Text += toShow;
                                //await Task.Delay(100).ConfigureAwait(false);
                            }
                        }
                        catch (Exception err)
                        {
                            txtResult.Text = err.Message + Environment.NewLine + err.StackTrace;
                        }
                        finally
                        {
                           // board.StopOperation();
                        }
                    
                    }
                }
                catch(Exception err)
                {
                    txtResult.Text = err.Message + Environment.NewLine + err.StackTrace;
                }*/
                #endregion

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
            var threadSafeVM = new SynchronizedNotifyPropertyChanged<CalculationViewModel>(_vm, this);

            //txtResult.DataBindings.Add(new Binding("Text", _vm, "Text") { DataSourceUpdateMode = DataSourceUpdateMode.Never });
            voltageBox.DataBindings.Add(new Binding("Text", threadSafeVM, "InboundVoltage") { DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged });
            voltageAverageBox.DataBindings.Add(new Binding("Text", threadSafeVM, "InboundVoltageAverage") { DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged });
            //outputActiveBox.DataBindings.Add(new Binding("Text", threadSafeVM, "OutboundCurrentActive") { DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged });
        }


        private void txtResult_TextChanged(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
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
            BindControls();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void calculationViewModelBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

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

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


        public  async void button1_Click(object sender, EventArgs e) //event for start/stop button
        {
            if (!_isStarted)
            {
                _isStarted = !_isStarted;
                btnStart.Text = (_isStarted) ? "Стоп" : "Старт";
                txtResult.Text = String.Empty;




                await Task.Factory.StartNew(() => this.Invoke( new Action(() => { _vm.Start(); })));


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
            //txtResult.DataBindings.Add(new Binding("Text", _vm, "Text") { DataSourceUpdateMode = DataSourceUpdateMode.Never });
            txtResult.DataBindings.Add(new Binding("Text", _vm, "InboundVoltage") { DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged });
            //txtResult.DataBindings.Add(new Binding("Text", _vm, "InboundVoltageAverage") { DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged });
            //txtResult.DataBindings.Add(new Binding("Text", _vm, "OutboundCurrent") { DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged });
            //txtResult.DataBindings.Add(new Binding("Text", _vm, "OutboundCurrentActive") { DataSourceUpdateMode = DataSourceUpdateMode.OnPropertyChanged });
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
    }
}

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
        private static System.Timers.Timer cardPollTimer;

        public ViewWindow()
        {
            InitializeComponent();
            
        }


        public  void button1_Click(object sender, EventArgs e) //event for start/stop button
        {
            if (_isStarted == false)
            {
                _isStarted = !_isStarted;
                btnStart.Text = (_isStarted) ? "Стоп" : "Старт";
                txtResult.Text = String.Empty;

                //Foo();
                
                
                    IListenerBoard board = GetListenerBoard(0);

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

                SetPollTimer();
            

            }

            else
            {
                _isStarted = !_isStarted;
                btnStart.Text = (_isStarted) ? "Стоп" : "Старт";
                Dispose();
            }
        }


        public  IListenerBoard GetListenerBoard(int boardNo) // method for activating board in system
        {            

            IListenerBoard result;
           

            //if (_useMock)
            //    result = new ListenerBoardMock(boardNo);
            //else
                result = new ListenerBoard(boardNo);

            return result;
        }

        private void SetPollTimer()
        {
            cardPollTimer = new System.Timers.Timer(1000);
            cardPollTimer.Elapsed += OnTimerEvent;
            cardPollTimer.AutoReset = true;
            cardPollTimer.Enabled = true;


        }

        private void OnTimerEvent(object source,ElapsedEventArgs e)
        {
            using (IListenerBoard board=GetListenerBoard(0)) //NOT A SOLUTION
            {
                float cardPollValue = board.CardPoll();
            }


        }

        /*private void Foo()
        {

            foreach (var ctl in this.Controls)
            {
                if (ctl is TextBox)
                {
                    ((TextBox)ctl).BackColor = Color.White;
                }
            }
        }*/

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

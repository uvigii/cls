using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using JulMar.Tapi3;
using QuartzTypeLib;

namespace IncomingSample
{
    public partial class frmmf2040 : Form
    {
        string sURL = "http://server/api/call/new/";        
        public frmmf2040()
        {
            InitializeComponent();
        }

        private void IncomingForm_Load(object sender, EventArgs e)
        {
            _tapiMgr.Initialize();

            foreach (TAddress addr in _tapiMgr.Addresses)
            {
                if (addr.QueryMediaType(TAPIMEDIATYPES.AUDIO))
                {
                    try
                    {
                        // Supports audio -- attempt to select video as well if the provider supports it.
                        TAPIMEDIATYPES mt = TAPIMEDIATYPES.AUDIO;
                        if (addr.QueryMediaType(TAPIMEDIATYPES.VIDEO))
                            mt |= TAPIMEDIATYPES.VIDEO;

                        // Open - this owns inbound calls
                        addr.Open(mt);
                    }
                    catch(TapiException ex)
                    {
                        if (ex.ErrorCode == unchecked((int)0x80040004))
                        {
                            try
                            {
                                addr.Open(TAPIMEDIATYPES.DATAMODEM);
                            }
                            catch
                            {
                            }
                        }
                        else
                            MessageBox.Show(string.Format("Open(VOICE) {0}: {1}", addr.AddressName, ex.Message));
                    }

                    // Add any existing calls.
                    foreach (TCall call in addr.Calls)
                    {
                        lbCalls.Items.Add(call);
                    }
                }
            }
        }

        void OnSelectedCallChange(object sender, EventArgs e)
        {
            TCall call = (TCall) lbCalls.SelectedItem;
            if (call != null)
            {
                if (call.Privilege == CALL_PRIVILEGE.CP_OWNER)
                {
                    btnAnswer.Enabled = call.CallState == CALL_STATE.CS_OFFERING;
                    btnDisconnect.Enabled = true;
                    return;
                }
            }

            btnAnswer.Enabled = false;
            btnDisconnect.Enabled = false;
        }

        private void OnAnswerClicked(object sender, EventArgs e)
        {
            TCall call = (TCall)lbCalls.SelectedItem;

            try
            {                
                call.SelectDefaultTerminals();
                call.Answer();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void OnDisconnectClicked(object sender, EventArgs e)
        {
            TCall call = (TCall)lbCalls.SelectedItem;
            try
            {
                call.Disconnect(DISCONNECT_CODE.DC_NORMAL);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void IncomingForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _tapiMgr.Shutdown();
        }

        private void OnCallStateChange(object sender, TapiCallStateEventArgs e)
        {
            if (e.Call.Privilege == CALL_PRIVILEGE.CP_OWNER)
            {
                lbCalls.Items.Remove(e.Call);
                if (e.State != CALL_STATE.CS_DISCONNECTED)
                    lbCalls.Items.Add(e.Call);
            }
        }

        private void OnNewCall(object sender, TapiCallNotificationEventArgs e)
        {
            if (e.Call.Privilege == CALL_PRIVILEGE.CP_OWNER)
                //lbCalls.Items.Add(e.Call);                       
            lbRing.Text = e.Call.get_CallInfo(CALLINFO_STRING.CIS_CALLERIDNUMBER);
            lbRing.BackColor = Color.FromArgb(0, 255, 0);
            this.BackColor = Color.FromArgb(0, 255, 0);
            this.LogWrite(sURL + lbRing.Text);
            if (lbRing.Text != "")
            {
                try
                {
                    // Create a new 'HttpWebRequest' Object to the mentioned URL.
                    System.Net.HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(sURL + lbRing.Text);
                    myHttpWebRequest.Timeout = 10000;
                    myHttpWebRequest.ReadWriteTimeout = 10000;
                    myHttpWebRequest.KeepAlive = false;
                    myHttpWebRequest.ProtocolVersion = HttpVersion.Version11;
                    myHttpWebRequest.Method = "GET";
                    System.Net.HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
                    myHttpWebResponse.Close();

                    myHttpWebResponse = null;
                    myHttpWebRequest = null;                    
                    //System.Net.WebRequest req = System.Net.WebRequest.Create(sURL + lbRing.Text);
                    //System.Net.WebResponse resp = req.GetResponse();

                }
                catch (Exception err)
                {
                    lbRing.BackColor = Color.FromArgb(255, 0, 0);
                    this.BackColor = Color.FromArgb(255, 0, 0);
                    tbLog.Text = err.ToString();
                    this.LogWrite(err.ToString());
                }
            }
            else
            {
                this.LogWrite("No number ?!");
            }
            
        }

        public void LogWrite(string logMessage)
        {            
            try
            {
                using (StreamWriter w = File.AppendText("log.txt"))
                {
                    Log(logMessage, w);
                }
            }
            catch (Exception ex)
            {
            }
        }

        public void Log(string logMessage, TextWriter txtWriter)
        {
            try
            {                
                txtWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),  logMessage);
            }
            catch (Exception ex)
            {
            }
        }

        private void OnMediaChange(object sender, TapiCallMediaEventArgs e)
        {
            if (e.Call.Privilege != CALL_PRIVILEGE.CP_OWNER)
                return;

            // Find out if this stream has a video render terminal. If not,
            // we don't need to do anything with this stream. Also note
            // if this is the video capture stream or the video render stream.
            if (e.Event == CALL_MEDIA_EVENT.CME_STREAM_ACTIVE)
            {
                TStream stm = e.Stream;
                if (stm != null && stm.MediaType == TAPIMEDIATYPES.VIDEO)
                {
                    if (stm.Direction == TERMINAL_DIRECTION.TD_RENDER ||
                        stm.Direction == TERMINAL_DIRECTION.TD_CAPTURE)
                        DisplayVideoWindow(stm, stm.MediaType, stm.Direction);
                }
            }
        }

        private void DisplayVideoWindow(TStream stm, TAPIMEDIATYPES mediaType, TERMINAL_DIRECTION direction)
        {
            TTerminal t = stm.FindTerminal(mediaType, direction);
            if (t != null)
            {
                IVideoWindow videoWindow = t.QueryInterface(typeof(IVideoWindow)) as IVideoWindow;
                if (videoWindow != null)
                {
                    videoWindow.Owner = (direction == TERMINAL_DIRECTION.TD_RENDER) ? localVidPlaceholder.Handle.ToInt32() : remoteVidPlaceholder.Handle.ToInt32();
                    videoWindow.WindowStyle = 0x40800000; // WS_CHILD | WS_BORDER;
                    videoWindow.SetWindowPosition(0, 0, localVidPlaceholder.Width, localVidPlaceholder.Height);
                    videoWindow.Visible = 1;
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
       
  
    }
}
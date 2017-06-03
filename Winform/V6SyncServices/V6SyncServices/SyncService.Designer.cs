namespace V6SyncServices
{
    partial class SyncService
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.timerRunning = new System.Timers.Timer();
            this.timerSleep = new System.Timers.Timer();
            this.eventLog1 = new System.Diagnostics.EventLog();
            ((System.ComponentModel.ISupportInitialize)(this.timerRunning)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timerSleep)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).BeginInit();
            // 
            // timerRunning
            // 
            this.timerRunning.Enabled = true;
            this.timerRunning.Interval = 60000D;
            this.timerRunning.Elapsed += new System.Timers.ElapsedEventHandler(this.timerStatus_Elapsed);
            // 
            // timerSleep
            // 
            this.timerSleep.Enabled = true;
            this.timerSleep.Interval = 1000D;
            this.timerSleep.Elapsed += new System.Timers.ElapsedEventHandler(this.timerSleep_Tick);
            // 
            // SyncService
            // 
            this.ServiceName = "V6SyncServices";
            ((System.ComponentModel.ISupportInitialize)(this.timerRunning)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timerSleep)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).EndInit();

        }

        #endregion

        private System.Timers.Timer timerRunning;
        private System.Timers.Timer timerSleep;
        private System.Diagnostics.EventLog eventLog1;
    }
}

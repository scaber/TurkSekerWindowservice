namespace VeriAktarimi
{
    partial class ProjectInstaller
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
            this.TurkSekerServis = new System.ServiceProcess.ServiceProcessInstaller();
            this.TurkSekerVeriAktarimi = new System.ServiceProcess.ServiceInstaller();
            // 
            // TurkSekerServis
            // 
            this.TurkSekerServis.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.TurkSekerServis.Password = null;
            this.TurkSekerServis.Username = null;
            // 
            // TurkSekerVeriAktarimi
            // 
            this.TurkSekerVeriAktarimi.ServiceName = "TurkSekerServis";
            this.TurkSekerVeriAktarimi.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.TurkSekerServis,
            this.TurkSekerVeriAktarimi});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller TurkSekerServis;
        private System.ServiceProcess.ServiceInstaller TurkSekerVeriAktarimi;
    }
}
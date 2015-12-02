using log4net;
using MsCommon.ClickOnce;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AzureCloudserviceDeployer
{
    public class BaseForm : Form
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(BaseForm));

        private Dictionary<Control, bool> controlState = new Dictionary<Control, bool>();
        private int busyCount = 0;
        private bool isBusy = false;

        public BaseForm()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseForm));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        }

        #region PerformWork & PerformWorkAsync

        public void PerformWork(BaseForm control, Action bgwork, Action fgwork, Action<Exception> customErrorHandler = null)
        {
            control.IsBusy = true;
            new Thread(() =>
            {
                Exception exception = null;
                try
                {
                    if (bgwork != null)
                        bgwork();
                }
                catch (Exception ex)
                {
                    exception = ex;
                }
                control.Invoke((Action)(() =>
                {
                    if (exception != null)
                    {
                        if (customErrorHandler != null)
                            customErrorHandler(exception);
                        else
                            new ReportBugForm(exception).ShowDialog(this);
                        IsBusy = false;
                        return;
                    }
                    try
                    {
                        if (fgwork != null)
                            fgwork();
                    }
                    catch (Exception ex)
                    {
                        new ReportBugForm(ex).ShowDialog(this);
                    }
                    finally
                    {
                        IsBusy = false;
                    }
                }));
            }).Start();
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public async Task PerformWorkAsync(BaseForm control, Func<Task> bgwork, Func<Task> fgwork, Action<Exception> customErrorHandler = null)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            control.IsBusy = true;
            await Task.Factory.StartNew(async () =>
            {
                Exception exception = null;
                try
                {
                    if (bgwork != null)
                        await bgwork();
                }
                catch (Exception ex)
                {
                    exception = ex;
                }
                control.Invoke((Func<Task>)(async () =>
                {
                    if (exception != null)
                    {
                        if (customErrorHandler != null)
                            customErrorHandler(exception);
                        else
                            new ReportBugForm(exception).ShowDialog(this);
                        IsBusy = false;
                        return;
                    }
                    try
                    {
                        if (fgwork != null)
                            await fgwork();
                    }
                    catch (Exception ex)
                    {
                        new ReportBugForm(ex).ShowDialog(this);
                    }
                    finally
                    {
                        IsBusy = false;
                    }
                }));
            });
        }

        #endregion

        #region Busy & ControlState

        public bool IsBusy
        {
            get
            {
                return isBusy;
            }
            set
            {
                if (value)
                    busyCount++;
                else
                    busyCount--;

                if (isBusy && busyCount == 0)
                    DisableBusy();
                else if (!isBusy && busyCount == 1)
                    EnableBusy();
            }
        }

        private void EnableBusy()
        {
            isBusy = true;
            controlState.Clear();
            RecursiveDisable(this);
            Cursor.Current = Cursors.WaitCursor;
        }

        private void RecursiveDisable(Control c)
        {
            foreach (Control inner in c.Controls)
                RecursiveDisable(inner);
            SaveControlStateAndDisable(c);
        }

        private void DisableBusy()
        {
            isBusy = false;
            RecursiveRestore(this);
            Cursor.Current = Cursors.Default;
        }

        private void RecursiveRestore(Control c)
        {
            foreach (Control inner in c.Controls)
                RecursiveRestore(inner);
            RestoreControlState(c);

            // Controls could be removed in the meantime. To prevent odd behaviour, we explicitely restore the state of each recorded control
            foreach (Control control in controlState.Keys)
            {
                RestoreControlState(control);
            }
        }

        public void UpdateControlEnabledState(Control control, bool enabled)
        {
            if (IsBusy)
            {
                if (controlState.ContainsKey(control))
                    controlState[control] = enabled;
                else
                    controlState.Add(control, enabled);
            }
            else
            {
                control.Enabled = enabled;
            }
        }


        private void SaveControlStateAndDisable(Control control)
        {
            if (control is Form)
                return;
            controlState.Add(control, control.Enabled);
            if (control.Tag is string && (string)control.Tag == "KEEP_ENABLED")
                return;
            control.Enabled = false;
            if (control is DataGridView)
            {
                var dgv = (DataGridView)control;
                dgv.ForeColor = Color.Gray;
                dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gray;
            }
        }
        
        private void RestoreControlState(Control control)
        {
            if (control is Form)
                return;
            if (controlState.ContainsKey(control))
            {
                control.Enabled = controlState[control];
                if (control is DataGridView)
                {
                    var dgv = (DataGridView)control;
                    dgv.ForeColor = Control.DefaultForeColor;
                    dgv.ColumnHeadersDefaultCellStyle.ForeColor = Control.DefaultForeColor;
                    foreach (Control scrollbar in dgv.Controls)
                    {
                        scrollbar.Enabled = controlState[control]; // Enable if parent is enabled
                    }
                }
            }
        }

        #endregion

    }
}

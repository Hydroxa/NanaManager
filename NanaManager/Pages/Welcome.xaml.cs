﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Threading;

using NanaManagerAPI;
using NanaManagerAPI.IO;
using NanaManagerAPI.UI;
using NanaManagerAPI.Threading;

namespace NanaManager
{
    /// <summary>
    /// Interaction logic for Welcome.xaml
    /// </summary>
    public partial class Welcome : Page
    {
        public static bool Complete;

        private BackgroundWorker bgWork = new BackgroundWorker();
        private Thread waitThread;
        public Welcome() {
            InitializeComponent();
            Globals.ChangeStatus += onStatusChange;
        }

        private void onStatusChange( string status, double progress, double? Maximum ) {
            Dispatcher.Invoke( new Action( () =>
            {
                lblStatus.Content = status;
                if ( progress == -1 )
                    pgProgress.IsIndeterminate = true;
                else {
                    pgProgress.Maximum = (int)Maximum;
                    pgProgress.Value = progress;
                }
            } ) );
        }

        private void Page_Loaded( object sender, RoutedEventArgs e ) {
            pgProgress.Visibility = lblStatus.Visibility = Visibility.Visible;
            waitThread = new Thread( () => Thread.Sleep( 2000 ) );
            bgWork.DelegateThread( () =>
            {
                if ( !ContentFile.CheckValidity() )
                    ContentFile.Decrypt( Login.Password );
                ContentFile.LoadData();
                Dispatcher.Invoke( () => { pgProgress.Visibility = lblStatus.Visibility = Visibility.Collapsed; } );
                SpinWait.SpinUntil( () => !waitThread.IsAlive );
                if ( MainWindow.ImportOnLogin )
                    Paging.LoadPage( Pages.Import );
                else
                    Paging.LoadPage( Pages.Viewer );
            } );
            waitThread.Start();
        }
    }
}

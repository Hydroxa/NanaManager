﻿using NanaManagerAPI;
using NanaManagerAPI.IO;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace NanaManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private App() {
            InitializeComponent();
        }

#pragma warning disable IDE1006 // Naming Styles

        [STAThread]
        [LoaderOptimization( LoaderOptimization.MultiDomain )]
        private static void Main() {
#pragma warning restore IDE1006 // Naming Styles

            ContentFile.LoadEnvironment();
            Logging.Init();

            NanaManager.Properties.Settings.Default.ToImport = new System.Collections.Specialized.StringCollection();

            try {
                App app = new App();
                Splash wnd = new Splash(); //Load and run the application
                app.Run( wnd );

                //This part executes after the application closes

                string[] logs = Directory.GetFiles( ContentFile.LogPath );
                if ( logs.Length > 5 ) {
                    for ( int i = 0; i < logs.Length - 5; i++ )
                        File.Delete( logs[i] );
                }
            } catch ( Exception e ) {
                Logging.Write( e, "Core", LogLevel.Fatal );
                string[] logs = Directory.GetFiles( ContentFile.LogPath );
                if ( logs.Length > 4 ) {
                    for ( int i = 0; i < logs.Length - 5; i++ )
                        File.Delete( logs[i] );
                }
                if ( Debugger.IsAttached ) {
                    ContentFile.Archive.Dispose();
                    throw;
                }
            } finally {
                Logging.SaveLogs();
                ContentFile.Archive?.Dispose();
            }
        }
    }
}
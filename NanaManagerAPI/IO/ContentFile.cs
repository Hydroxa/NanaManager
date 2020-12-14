﻿using NanaManagerAPI.IO.Cryptography;
using NanaManagerAPI.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Runtime.CompilerServices;
using System.Security;
using System.Windows;

[assembly: InternalsVisibleTo( "NanaManager" )] //Allows the application to access internal objects (Not visible to anything besides this assembly and friend assemblies)

namespace NanaManagerAPI.IO
{
    /// <summary>
    /// Information and handling for the user's content
    /// </summary>
    public static class ContentFile
    {
        private const int ERROR_DRIVE_MISSING = 0x001;
        private const int ERROR_INVALID_PATH = 0x002; //Error codes
        private const int ERROR_GENERIC_IO = 0x003;

        /// <summary>
        /// A delegate for encrypting and decrypting data
        /// </summary>
        /// <param name="Data">The data to perform the cryptographic function on</param>
        /// <param name="Password">The password for the function</param>
        /// <returns>The data resultant of the cryptographic function</returns>
        public delegate byte[] CryptographyFunction( byte[] Data, string Password );

        ///<summary>
        /// The provider class for cryptography. This class is referenced and called upon whenever the content file is encrypted or decrypted
        ///</summary>
        public static ICryptographyProvider CryptographyProvider;

        /// <summary>
        /// The directory containing application data for all Hydroxa programs
        /// </summary>
        public static readonly string HydroxaPath = Path.Combine( Environment.GetFolderPath( Environment.SpecialFolder.ApplicationData ), "Hydroxa" );

        /// <summary>
        /// The file path to the NanaManager directory
        /// </summary>
        public static readonly string RootPath = Path.Combine( HydroxaPath, "NanaManager" );

        /// <summary>
        /// The file location of the Content file
        /// </summary>
        public static readonly string ContentPath = Path.Combine( RootPath, "content.nana" );

        /// <summary>
        /// The file path to the Temp directory
        /// </summary>
        public static readonly string TempPath = Path.Combine( RootPath, "temp" );

        /// <summary>
        /// The file path to the Logs directory
        /// </summary>
        public static readonly string LogPath = Path.Combine( RootPath, "logs" );

        /// <summary>
        /// The directory where media exports go to
        /// </summary>
        public static readonly string ExportPath = Path.Combine( RootPath, "exports" );

        /// <summary>
        /// The directory where plugins are found
        /// </summary>
        public static readonly string PluginPath = Path.Combine( RootPath, "plugins" );

        /// <summary>
        /// The file location of the latest.log file
        /// </summary>
        public static readonly string LatestLogPath = Path.Combine( LogPath, "latest.log" );

        /// <summary>
        /// Represents whether the Content File is open or not
        /// </summary>
        public static bool IsOpen { private set; get; }

        /// <summary>
        /// The ContentFile's Archive
        /// </summary>
        public static ZipArchive Archive;

        private static readonly byte[] ZIP_SIGNATURE = new byte[] { 80, 75, 5, 6, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; //The signature for an empty zip file

        /// <summary>
        /// A list of all active encoders. These will be run when saving and loading data
        /// </summary>
        public static readonly List<IEncoder> ActiveEncoders = new List<IEncoder>();

        ///<summary>
        /// Generates the required folders and files, if missing
        ///</summary>
        internal static void LoadEnvironment() {
            try {
                if ( !Directory.Exists( HydroxaPath ) )
                    Directory.CreateDirectory( HydroxaPath );
                if ( !Directory.Exists( RootPath ) )
                    Directory.CreateDirectory( RootPath );
                if ( !Directory.Exists( TempPath ) )
                    Directory.CreateDirectory( TempPath );
                if ( !Directory.Exists( LogPath ) )
                    Directory.CreateDirectory( LogPath );
                if ( !Directory.Exists( ExportPath ) )
                    Directory.CreateDirectory( ExportPath );
                if ( !Directory.Exists( PluginPath ) )
                    Directory.CreateDirectory( PluginPath );
                if ( !File.Exists( ContentPath ) )
                    File.WriteAllBytes( ContentPath, ZIP_SIGNATURE ); //Blank signature for a zip file
            } catch ( NotSupportedException e ) {
                MessageBox.Show( $"The expected drive was not found (Check the connection)\nMessage: {e.Message}", "Nana Manager Pre-load Error", MessageBoxButton.OK, MessageBoxImage.Error );
                Environment.Exit( ERROR_DRIVE_MISSING );
            } catch ( DirectoryNotFoundException e ) {
                MessageBox.Show( e.Message, "Nana Manager Pre-load Error", MessageBoxButton.OK, MessageBoxImage.Error );
                Environment.Exit( ERROR_INVALID_PATH );
            } catch ( IOException e ) {
                MessageBox.Show( e.Message, "Nana Manager Pre-load Error", MessageBoxButton.OK, MessageBoxImage.Error );
                Environment.Exit( ERROR_GENERIC_IO );
            }

            //Loads the icon for audio files
            UI.UI.AudioSymbol = Resources.Music_Icon.ToBitmapImage( System.Windows.Media.Imaging.BitmapCacheOption.OnDemand );
        }

        ///<summary>
        /// Sets the archive to Write Mode. Call for every new file you write to (Can only write once!)
        ///</summary>
        /// <returns>A reference to the archive in write mode. Can also be accessed via <see cref="ContentFile.Archive"/></returns>
        public static ZipArchive SetArchiveWrite() {
            Archive?.Dispose(); //Dispose the existing archive if instanced, as to free up consumed resources
            Archive = ZipFile.Open( ContentPath, ZipArchiveMode.Update ); //Open the archive in Update mode, to signify to add new objects
            return Archive;
        }

        ///<summary>
        /// Sets the archive to Read Mode. Call whenever performing a batch of reading (Can read indefinitely)
        ///</summary>
        /// <returns>A reference to the archive in read mode. Can also be accessed via <see cref="ContentFile.Archive"/></returns>
        public static ZipArchive SetArchiveRead() {
            if ( Archive == null || Archive.Mode != ZipArchiveMode.Read ) { //Don't do anything if the archive already exists in read mode, otherwise instance
                Archive?.Dispose(); //Ensure the archive is disposed, to minimise resource consumption
                Archive = ZipFile.OpenRead( ContentPath ); //Open the archive in Read mode, to allow reading of information from the archive
            }
            return Archive;
        }

        /// <summary>
        /// Checks if the content file can be read
        /// </summary>
        /// <returns>Returns true if the content file can be read. False if corrupt or encrypted</returns>
        public static bool CheckValidity() {
            try {
                if ( Archive == null ) { //If the archive has not yet been instanced, create a temporary instance for the validity check
                    using var zipFile = ZipFile.OpenRead( ContentPath );
                    var test = zipFile.Entries; //Attempt to read the entries. If this errs, it is not a valid file to read
                    return true; //Returning stops further execution, so the next part will not execute
                }
                var entries = Archive.Entries; //Attempt to read the entries. Erring will return false, otherwise execution will continue
                return true;
            } catch ( InvalidDataException ) { //The expected error to occur from reading. If this is a different error, it will be thrown because something actually went wrong.
                return false;
            }
        }

        /// <summary>
        /// Reads a file from the content file
        /// </summary>
        /// <param name="Name">The name of the file to read</param>
        /// <returns>A byte array representation of the data</returns>
        public static byte[] ReadFile( string Name ) {
            if ( string.IsNullOrWhiteSpace( Name ) ) //TODO - Maybe replace with regex "[\\/:"*?<>|]+" for general invalid file names?
                throw new ArgumentException( "File name cannot be null or whitespace", nameof( Name ) ); //Throw if the name given was null or whitespace

            SetArchiveRead();
            ZipArchiveEntry entry = Archive.GetEntry( Name ); //Try to get the file from the archive
            if ( entry == null ) //If the entry is null, it did not exist and so throw an exception
                throw new FileNotFoundException( "The specified file was not found within the database", Name );
            else { 
                string location = Path.Combine( TempPath, Name ); //Extract the file, to give it an EoF to read from
                entry.ExtractToFile( location ); //TODO - HANDLE ERRORS
                byte[] data = File.ReadAllBytes( location ); //Read the relevant data
                File.Delete( location ); //Delete the extracted file (Unextracted is still inside the archive)
                return data;
            }
        }

        /// <summary>
        /// Writes to a file within the content file, if it exists. Creates one otherwise
        /// </summary>
        /// <param name="Name">The name of the file</param>
        /// <param name="Data">The data to write</param>
        public static void WriteFile( string Name, string Data ) {
            if ( string.IsNullOrWhiteSpace( Name ) ) //See comment on ln174 and ln175
                throw new ArgumentException( "File name cannot be null or whitespace", nameof( Name ) );

            if ( Exists( Name ) ) { //Checks if the file exists inside the archive
                SetArchiveWrite(); 
                ZipArchiveEntry entry = Archive.GetEntry( Name ); 
                using Stream entryStream = entry.Open(); //Open the existing entry and overwrite with the new data
                using StreamWriter writer = new StreamWriter( entryStream );
                writer.Write( Data );
            }
            else {
                //TODO - HANDLE ERRORS
                SetArchiveWrite();
                string location = Path.Combine( TempPath, Name );
                File.WriteAllText( location, Data ); //Create temporary file and write to it, then compress into the archive
                Archive.CreateEntryFromFile( location, Name );
                File.Delete( location ); .//Delete the temporary file
            }
        }

        /// <summary>
        /// Writes to a file within the content file, if it exists. Creates one otherwise
        /// </summary>
        /// <param name="Name">The name of the file</param>
        /// <param name="Data">The data to write</param>
        public static void WriteFile( string Name, byte[] Data ) {
            if ( string.IsNullOrWhiteSpace( Name ) )
                throw new ArgumentException( "File name cannot be null or whitespace", nameof( Name ) );

            if ( Exists( Name ) ) {
                ZipArchiveEntry entry = Archive.GetEntry( Name );
                using Stream entryStream = entry.Open();
                using StreamWriter writer = new StreamWriter( entryStream );
                writer.Write( Data );
            }
            else {
                string location = Path.Combine( TempPath, Name );
                File.WriteAllBytes( location, Data );
                Archive.CreateEntryFromFile( location, Name );
                File.Delete( location );
            }
        }

        /// <summary>
        /// Checks whether a file exists in the database
        /// </summary>
        /// <param name="Name">The name of the file to check</param>
        /// <returns>True if the file exists</returns>
        public static bool Exists( string Name ) {
            if ( string.IsNullOrWhiteSpace( Name ) )
                throw new ArgumentException( "File name cannot be null or whitespace", nameof( Name ) );
            SetArchiveRead();
            ZipArchiveEntry entry = Archive?.GetEntry( Name );
            return entry != null;
        }

        /// <summary>
        /// Invokes all encoders to save their relevant data
        /// </summary>
        public static void SaveData() {
            foreach ( IEncoder encoder in ActiveEncoders )
                encoder.SaveData();
        }

        /// <summary>
        /// Invokes all encoders to load their relevant data
        /// </summary>
        public static void LoadData() {
            foreach ( IEncoder encoder in ActiveEncoders )
                encoder.LoadData();
        }

        /// <summary>
        /// Encrypts the content file using <see cref="CryptographyProvider"/>
        /// </summary>
        /// <param name="Password">The password to encrypt the data with</param>
        public static void Encrypt( string Password ) {
            try {
                Archive?.Dispose();
                CryptographyProvider.Initialise( Password );
                File.WriteAllBytes( ContentPath, CryptographyProvider.Encrypt( File.ReadAllBytes( ContentPath ) ) );
                CryptographyProvider.Terminate();
                IsOpen = false;
            } catch ( IOException e ) {
                //TODO - HANDLE I/O ERROR
                throw e;
            } catch ( UnauthorizedAccessException e ) {
                //TODO - HANDLE ERROR
                //File that is readonly
                //File that is hidden
                //Do not have permission
                throw e;
            } catch ( SecurityException e ) {
                //TODO - HANDLE ACCESS ERROR
                throw e;
            }
        }

        /// <summary>
        /// Decrypts the content file using <see cref="CryptographyProvider"/>
        /// </summary>
        /// <param name="Password">The password to decrypt the data with</param>
        public static void Decrypt( string Password ) {
            try {
                CryptographyProvider.Initialise( Password );
                File.WriteAllBytes( ContentPath, CryptographyProvider.Decrypt( File.ReadAllBytes( ContentPath ) ) );
                CryptographyProvider.Terminate();
                Archive = ZipFile.Open( ContentPath, ZipArchiveMode.Update );
                IsOpen = true;
            } catch ( ArgumentNullException e ) {
                //Path is null or byte array was empty
                Logging.Write( $"Attempted to write an empty array to the file\nStack Trace:\n\t{e.StackTrace}", "ContentDecryption", LogLevel.Fatal );
                throw e;
            } catch ( DirectoryNotFoundException e ) {
                Logging.Write( "The Content Path was not found. Attempting to generate new files.", "ContentDecryption", LogLevel.Error );
                LoadEnvironment();
                if ( !File.Exists( ContentPath ) )
                    throw e;
            } catch ( PathTooLongException e ) {
                //Path was too long
                Logging.Write( $"Content Path was too long: \"{ContentPath}\"", "ContentDecrpytion", LogLevel.Fatal );
                throw e;
            } catch ( SecurityException e ) {
                //TODO - Does not have required permissions

                throw e;
            } catch ( IOException e ) {
                Logging.Write( $"Could not decrypt file ({e.Message})\nStack Trace:\n\t{e.StackTrace}", "ContentDecryption", LogLevel.Fatal );
                throw e;
            } catch ( UnauthorizedAccessException e ) {
                //TODO - HANDLE ERROR
                if ( Directory.Exists( ContentPath ) )
                    Logging.Write( $"Content Path was a directory: \"{ContentPath}\"", "ContentDecryption", LogLevel.Fatal );
                else {
                    FileInfo fi = new FileInfo( ContentPath );

                    if ( fi.IsReadOnly )
                        Logging.Write( $"Content Path was read only: \"{ContentPath}\"", "ContentDecryption", LogLevel.Fatal );
                    //File that is readonly
                    //File that is hidden
                    //Do not have permission
                }
                throw e;
            }
        }
    }
}

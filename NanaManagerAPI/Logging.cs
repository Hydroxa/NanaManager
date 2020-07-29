﻿using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

using NanaManagerAPI.IO;

namespace NanaManagerAPI
{
	/// <summary>
	/// The log manager. It saves it to the log files, as well as outputting to the debug output
	/// </summary>
	public static class Logging
	{
		private const string FORMAT = "[{3}:{4}:{5}.{6} D{2}] [{1}] [{7}] {0}";
		private const string NUM_FORMAT = "[{8}] [{3}:{4}:{5}.{6} D{2}] [{1}] [{7}] {0}";
		private const string ERROR_FORMAT = "An Exception occurred at {0}: {1}\n\nA Stack Trace of the error follows:\n{2}";
		private static Queue<string> logs;

		/// <summary>
		/// Is true if the Logger is active and accepting writes
		/// </summary>
		public static bool Initialised { get; private set; }
		/// <summary>
		/// Handles new Log entries
		/// </summary>
		/// <param name="Message">The message added to the logs</param>
		public delegate void LogHandler( string Message );
		/// <summary>
		/// Fires whenever a new message is added to the logs
		/// </summary>
		public static event LogHandler NewMessage;

		/// <summary>
		/// Appends the logs to the latest.log file
		/// </summary>
		public static void SaveLogs() {
			StringBuilder sb = new StringBuilder();
			for ( int i = 0; i < logs.Count; i++ )
				sb.AppendLine( logs.Dequeue() );
			File.AppendAllText( ContentFile.LatestLogPath, sb.ToString() );
		}

		/// <summary>
		/// Write a message to the logs with no formatting
		/// </summary>
		/// <param name="Message">The message to add to the logs</param>
		public static void Write( string Message ) {
			if ( !Initialised )
				throw new InvalidOperationException( "The logger was not initialised. Call Logging.Init() to initialise." );
			logs.Enqueue( Message );
			NewMessage?.Invoke( Message );
		}
		/// <summary>
		/// Writes a debug message to the logs from the specified location
		/// </summary>
		/// <param name="Message">The message to display</param>
		/// <param name="Location">The location the message came from</param>
		public static void Write( string Message, string Location ) {
			try {
				Write( string.Format( FORMAT, Message, Location, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond, "Debug" ) );
			} catch ( InvalidOperationException ex ) {
				throw ex;
			}
		}
		/// <summary>
		/// Writes a message to the logs from the specified location at the specified severity level
		/// </summary>
		/// <param name="Message">The message to display</param>
		/// <param name="Location">The location the message came from</param>
		/// <param name="Level">The severity of the message</param>
		public static void Write( string Message, string Location, LogLevel Level ) {
			try {
				Write( string.Format( FORMAT, Message, Location, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond, Enum.GetName( typeof( LogLevel ), Level ) ) );
			} catch ( InvalidOperationException ex ) {
				throw ex;
			}
		}
		/// <summary>
		/// Writes a message to the logs from the specified location at the specified severity level, prefixing with the supplied LogCode
		/// </summary>
		/// <param name="Message">The message to display</param>
		/// <param name="Location">The location the message came from</param>
		/// <param name="Level">The severity of the message</param>
		/// <param name="LogCode">The code of the message. Used for watching for specific logs</param>
		public static void Write( string Message, string Location, LogLevel Level, int LogCode ) {
			try {
				Write( string.Format( NUM_FORMAT, Message, Location, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond, Enum.GetName( typeof( LogLevel ), Level ) ), Convert.ToString( LogCode, 16 ) );
			} catch ( InvalidOperationException ex ) {
				throw ex;
			}
		}
		/// <summary>
		/// Writes an error to the logs from the specified location
		/// </summary>
		/// <param name="Error">The error to display</param>
		/// <param name="Location">The location the message came from</param>
		public static void Write( Exception Error, string Location ) {
			try {
				Write( Error, Location, LogLevel.Error );
			} catch ( InvalidOperationException ex ) {
				throw ex;
			}
		}
		/// <summary>
		/// Writes an error to the logs from the specified location at the specified severity level
		/// </summary>
		/// <param name="Error">The error to display</param>
		/// <param name="Location">The location of the error</param>
		/// <param name="Level">The severity of the error</param>
		public static void Write( Exception Error, string Location, LogLevel Level ) {
			try {
				Write( string.Format( FORMAT, string.Format( ERROR_FORMAT, Error.Source, Error.Message, Error.StackTrace ), Location, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond, Enum.GetName( typeof( LogLevel ), Level ) ) );
			} catch ( InvalidOperationException ex ) {
				throw ex;
			}
		}

		/// <summary>
		/// Initialises the logger. The Logger is often initialised before plugins, though this may not always be the case
		/// </summary>
		public static void Init() {
			if ( Initialised )
				return;
			logs = new Queue<string>();
			if ( File.Exists( ContentFile.LatestLogPath ) ) {
				string[] data = File.ReadAllLines( ContentFile.LatestLogPath );
				File.WriteAllLines( Path.Combine( ContentFile.LogPath, data[0] ), data );
			}
			DateTime now = DateTime.Now;
			File.WriteAllText( ContentFile.LatestLogPath, $"{now.Year}.{now.Month}.{now.Day}-{now.Hour}.{now.Minute}.{now.Second}.{now.Millisecond}.log\n" );
			Initialised = true;
			Write( "Logger initialised!", "Logger", LogLevel.Info );
		}
	}
	public enum LogLevel : byte
	{
		/// <summary>
		/// The default Log Level. Often filtered out by log displays. Dump as much as you want here
		/// </summary>
		Debug = 0,
		/// <summary>
		/// The information Log Level. This is to let debuggers know about categorised tasks. Like Debug, but less spam
		/// </summary>
		Info,
		/// <summary>
		/// The warning Log Level. This is to alert of something that should not necessarily be the case, but isn't too detrimental.
		/// </summary>
		Warn,
		/// <summary>
		/// The error Log Level. This is to alert of something bad, but not crash-worthy. Often used in recoverable errors
		/// </summary>
		Error,
		/// <summary>
		/// The fatal Log Level. Irrecoverable issue that requires the application to crash or use a long duration error handler. These are automatically handled by the error page.
		/// </summary>
		Fatal
	}
}
﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using NanaManagerAPI.Types;
using NanaManagerAPI.EventArgs;

namespace NanaManagerAPI.UI.Controls
{
    /// <summary>
    /// Interaction logic for TagSelector.xaml
    /// </summary>
    public partial class TagSelector : UserControl
    {
        private bool clearing = false;
        private bool init = false;

        /// <summary>
        /// A delegate which handles when a tag is checked inside a <see cref="TagSelector"/>
        /// </summary>
        /// <param name="Sender">The origin of the event</param>
        /// <param name="EventData">The information of the tag that was checked</param>
        public delegate void CheckedTagHandler( object Sender, TagCheckEventArgs EventData );
        /// <summary>
        /// Fires whenever a tag is toggled
        /// </summary>
        public event CheckedTagHandler TagChecked;

        #region DependencyProperties


        public bool DoAliases
        {
            get { return (bool)GetValue( DoAliasesProperty ); }
            set { SetValue( DoAliasesProperty, value ); }
        }
        public static readonly DependencyProperty DoAliasesProperty = DependencyProperty.Register( "DoAliases", typeof( bool ), typeof( TagSelector ), new PropertyMetadata( true ) );
        public bool ShowHiddenTags
        {
            get => (bool)GetValue( ShowHiddenTagsProperty );
            set => SetValue( ShowHiddenTagsProperty, value );
        }
        public static readonly DependencyProperty ShowHiddenTagsProperty = DependencyProperty.Register( "ShowHiddenTags", typeof( bool ), typeof( TagSelector ), new PropertyMetadata( false ) );
        public Brush GroupBoxBrush
        {
            get { return (Brush)GetValue( GroupBoxBrushProperty ); }
            set { SetValue( GroupBoxBrushProperty, value ); }
        }
        public static readonly DependencyProperty GroupBoxBrushProperty = DependencyProperty.Register( "GroupBoxBrush", typeof( Brush ), typeof( TagSelector ), new PropertyMetadata( new SolidColorBrush( Color.FromArgb( 255, 0, 0, 0 ) ) ) );
        public Brush LoadingBrush
        {
            get { return (Brush)GetValue( LoadingBrushProperty ); }
            set { SetValue( LoadingBrushProperty, value ); }
        }
        public static readonly DependencyProperty LoadingBrushProperty = DependencyProperty.Register( "LoadingBrush", typeof( Brush ), typeof( TagSelector ), new PropertyMetadata( new SolidColorBrush( Color.FromArgb( 255, 0, 0, 0 ) ) ) );
        public bool AllowRejection
        {
            get { return (bool)GetValue( AllowRejectionProperty ); }
            set { SetValue( AllowRejectionProperty, value ); }
        }
        public static readonly DependencyProperty AllowRejectionProperty = DependencyProperty.Register( "AllowRefection", typeof( bool ), typeof( TagSelector ), new PropertyMetadata( false ) );

        #endregion

        private readonly Dictionary<int, ListBox> groups = new Dictionary<int, ListBox>();
        private readonly List<int> checkedTags = new List<int>();
        private readonly List<int> rejected = new List<int>();

        public TagSelector() {
            InitializeComponent();
        }

        #region Events
        private void gb_MouseLeftButtonDown( object sender, System.EventArgs ev ) {
            GroupBox gb = (GroupBox)sender;
            bool? c = (bool?)gb.Tag;
            if ( MessageBox.Show( $"{(c == true ? "Unc" : "C")}heck all tags in this group?", "Check Tags", MessageBoxButton.YesNo ) == MessageBoxResult.Yes ) {
                if ( c == true )
                    c = false;
                else
                    c = true;
                ListBox content = (ListBox)gb.Content;
                foreach ( ToggleButton tb in content.Items )
                    tb.IsChecked = c;
                gb.Tag = c;
            }
        }
        private void gb_MouseRightButtonDown( object sender, System.EventArgs ev ) {
            if ( AllowRejection ) {
                GroupBox gb = (GroupBox)sender;
                bool? c = (bool?)gb.Tag;
                if ( MessageBox.Show( $"{(c == null ? "Un-" : "")}Reject all tags in this group?", "Check Tags", MessageBoxButton.YesNo ) == MessageBoxResult.Yes ) {
                    if ( c == null )
                        c = false;
                    else
                        c = null;
                    ListBox content = (ListBox)gb.Content;
                    Style rejStyle = (Style)Resources["RejectedTagButton"];
                    Style stdStyle = (Style)Resources["Tag Button"];
                    foreach ( ToggleButton tb in content.Items ) {
                        int id = (int)tb.Tag;
                        tb.IsChecked = false;
                        if ( c == null && !rejected.Contains( id ) ) {
                            tb.Style = rejStyle;
                            rejected.Add( id );
                        }
                        else if ( checkedTags.Contains( id ) ) {
                            tb.Style = stdStyle;
                            rejected.Remove( id );
                        }
                    }
                    gb.Tag = c;
                }
            }
        }
        private void tag_MouseRightButtonDown( object sender, System.EventArgs ev ) {
            if ( AllowRejection ) {
                ToggleButton b = (ToggleButton)sender;
                b.IsChecked = false;
                int id = (int)b.Tag;
                if ( rejected.Contains( id ) ) { //Unreject
                    b.Style = (Style)Resources["Tag Button"];
                    rejected.Remove( id );
                    if ( !clearing )
                        TagChecked?.Invoke( this, new TagCheckEventArgs() { IsActive = true, Rejection = true, TagIndex = id } );
                }
                else { //Reject
                    b.Style = (Style)Resources["RejectedTagButton"];
                    if ( checkedTags.Contains( id ) )
                        checkedTags.Remove( id );
                    rejected.Add( id );
                    if ( !clearing )
                        TagChecked?.Invoke( this, new TagCheckEventArgs() { IsActive = false, Rejection = true, TagIndex = id } );
                }
            }
        }
        private void tag_Checked( object sender, System.EventArgs ev ) {
            ToggleButton b = (ToggleButton)sender;
            int id = (int)b.Tag;
            if ( !checkedTags.Contains( id ) ) { //Accept
                b.Style = (Style)Resources["Tag Button"];
                if ( rejected.Contains( id ) )
                    rejected.Remove( id );
                checkedTags.Add( id );
                if ( !clearing )
                    TagChecked?.Invoke( this, new TagCheckEventArgs() { IsActive = true, TagIndex = id } );
                if ( DoAliases )
                    foreach ( int t in Data.Tags[Data.TagLocations[id]].GetAliases() )
                        CheckTags( t );
            }
        }
        private void tag_Unchecked( object sender, System.EventArgs ev ) {
            ToggleButton b = (ToggleButton)sender;
            int id = (int)b.Tag;
            if ( checkedTags.Contains( id ) ) { //Unaccept
                b.Style = (Style)Resources["Tag Button"];
                checkedTags.Remove( id );

                if ( !clearing )
                    TagChecked?.Invoke( this, new TagCheckEventArgs() { IsActive = false, TagIndex = id } );
            }
        }
        #endregion

        #region Generation
        private void loadInformation() {
            Dispatcher.Invoke( () => bdrLoading.Visibility = Visibility.Visible );

            checkedTags.Clear();
            rejected.Clear();
            stkGroups.Children.Clear();
            groups.Clear();
            addGroup( -1, "Misc" );

            foreach ( KeyValuePair<int, string> s in Data.Groups )
                addGroup( s.Key, s.Value );
            foreach ( Tag t in Data.Tags )
                if ( !Data.HiddenTags.Contains( t.Index ) || ShowHiddenTags )
                    addTag( t.Index ); //Index may not necessarily be the same as location

            UIElement[] childs = new UIElement[stkGroups.Children.Count];
            stkGroups.Children.CopyTo( childs, 0 );
            foreach ( GroupBox gb in childs )
                if ( ((string)gb.Header) != "Misc" && ((ListBox)gb.Content).Items.Count == 0 ) //Remove any empty groups unless group is Misc
                    stkGroups.Children.Remove( gb );

            Dispatcher.Invoke( () => bdrLoading.Visibility = Visibility.Collapsed );
            init = true;
        }
        private void addGroup( int key, string name ) {
            GroupBox gb = new GroupBox() { Header = name, Style = (Style)Resources["Tag Groupbox"], Margin = new Thickness( 10, 0, 0, 10 ), Width = 171, FontSize = 18, Tag = false };
            gb.MouseLeftButtonDown += gb_MouseLeftButtonDown;
            gb.MouseRightButtonDown += gb_MouseRightButtonDown;
            ListBox content = new ListBox() { Background = new SolidColorBrush( Color.FromArgb( 0, 0, 0, 0 ) ), BorderThickness = new Thickness( 0 ), Foreground = (Brush)Application.Current.Resources["DarkText"], HorizontalContentAlignment = HorizontalAlignment.Stretch, VerticalContentAlignment = VerticalAlignment.Stretch };
            content.SelectionChanged += ( _, _ ) => content.SelectedIndex = -1;
            gb.Content = content;
            groups.Add( key, content );
            stkGroups.Children.Add( gb );
        }
        private void addTag( int t ) {
            ToggleButton tag = new ToggleButton() { Content = Data.Tags[Data.TagLocations[t]].Name, Style = (Style)Resources["Tag Button"], Tag = t };
            tag.MouseRightButtonDown += tag_MouseRightButtonDown;
            tag.Checked += tag_Checked;
            tag.Unchecked += tag_Unchecked;
            groups[Data.Tags[Data.TagLocations[t]].Group].Items.Add( tag );
        }
        #endregion
        #region Events
        private void userControl_Loaded( object sender, RoutedEventArgs e ) {
            init = false;
            stkGroups.Dispatcher.BeginInvoke( new Action( loadInformation ) );
        }
        #endregion
        #region Methods
        /// <summary>
        /// Returns the indicies of the tags that are currently active
        /// </summary>
        /// <returns></returns>
        public int[] GetCheckedTagsIndicies() => checkedTags.ToArray();
        /// <summary>
        /// Returns the tags that are currently active
        /// </summary>
        /// <returns></returns>
        public Tag[] GetCheckedTags() {
            Tag[] tags = new Tag[checkedTags.Count];
            for ( int i = 0; i < checkedTags.Count; i++ )
                tags[i] = Data.Tags[Data.TagLocations[checkedTags[i]]];
            return tags;
        }
        /// <summary>
        /// Returns the indicies of the tags that currently have been rejected
        /// </summary>
        /// <returns></returns>
        public int[] GetRejectedTagsIndicies() => rejected.ToArray();
        /// <summary>
        /// Returns the tags that currenthly have been rejected
        /// </summary>
        /// <returns></returns>
        public Tag[] GetRejectedTags() {
            Tag[] tags = new Tag[rejected.Count];
            for ( int i = 0; i < rejected.Count; i++ )
                tags[i] = Data.Tags[Data.TagLocations[rejected[i]]];
            return tags;
        }
        /// <summary>
        /// Unchecks all tags without raising events
        /// </summary>
        public void ClearTags() {
            clearing = true;
            UnrejectTags( rejected.ToArray() );
            UncheckTags( checkedTags.ToArray() );
            checkedTags.Clear();
            clearing = false;
        }
        /// <summary>
        /// Checks all tags within the list
        /// </summary>
        /// <param name="ToCheck">The tags to check</param>
        public void CheckTags( params int[] ToCheck ) {
            SpinWait.SpinUntil( () => init );
            Dispatcher.Invoke( () =>
             {
                 foreach ( int i in ToCheck )
                     foreach ( ToggleButton tb in groups[Data.Tags[Data.TagLocations[i]].Group].Items )
                         if ( (int)tb.Tag == i ) {
                             tb.IsChecked = true;
                             break;
                         }
             } );
        }
        /// <summary>
        /// Unchecks all tags within the list
        /// </summary>
        /// <param name="ToUncheck">The tags to uncheck</param>
        public void UncheckTags( params int[] ToUncheck ) {
            SpinWait.SpinUntil( () => init );
            Dispatcher.Invoke( () =>
            {
                foreach ( int i in ToUncheck )
                    foreach ( ToggleButton tb in groups[Data.Tags[Data.TagLocations[i]].Group].Items )
                        if ( (int)tb.Tag == i ) {
                            tb.IsChecked = false;
                            break;
                        }
            } );
        }
        /// <summary>
        /// Unrejects all tags within the list. Does nothing if rejection is disabled
        /// </summary>
        /// <param name="ToUnreject">The list of tags to unreject</param>
        public void UnrejectTags( params int[] ToUnreject ) {
            SpinWait.SpinUntil( () => init );
            Dispatcher.Invoke( () =>
            {
                if ( AllowRejection ) {
                    foreach ( int i in ToUnreject )
                        foreach ( ToggleButton tb in groups[Data.Tags[Data.TagLocations[i]].Group].Items )
                            if ( (int)tb.Tag == i && rejected.Contains( i ) ) {
                                tag_MouseRightButtonDown( tb, null );
                                break;
                            }
                }
            } );
        }
        /// <summary>
        /// Rejects all tags within the list. Does nothing if rejection is disabled
        /// </summary>
        /// <param name="ToReject">The list of tags to reject</param>
        public void RejectTags( params int[] ToReject ) {
            SpinWait.SpinUntil( () => init );
            Dispatcher.Invoke( () =>
            {
                if ( AllowRejection ) {
                    foreach ( int i in ToReject )
                        foreach ( ToggleButton tb in groups[Data.Tags[Data.TagLocations[i]].Group].Items )
                            if ( (int)tb.Tag == i && !rejected.Contains( i ) ) {
                                tag_MouseRightButtonDown( tb, null );
                                break;
                            }
                }
            } );
        }
        public void Reload() {
            loadInformation();
            int[] toCheck = checkedTags.ToArray();
            int[] toReject = rejected.ToArray();
            
            CheckTags( toCheck );
            RejectTags( toReject );
        }
        #endregion
    }
}
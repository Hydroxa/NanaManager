﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

using NanaManagerAPI.Data;
using NanaManagerAPI.EventArgs;

namespace NanaManagerAPI.UI.Controls
{
    /// <summary>
    /// Interaction logic for TagSelector.xaml
    /// </summary>
    public partial class TagSelector : UserControl
    {
        /// <summary>
        /// A delegate which handles when a tag is checked inside a <see cref="TagSelector"/>
        /// </summary>
        /// <param name="sender">The origin of the event</param>
        /// <param name="E">The information of the tag that was checked</param>
        public delegate void CheckedTagHandler( object sender, TagCheckEventArgs e );
        /// <summary>
        /// Fires whenever a tag is toggled
        /// </summary>
        public event CheckedTagHandler TagChecked;

        #region DependencyProperties
        public Brush GroupBoxBrush
        {
            get { return (Brush)GetValue( GroupBoxBrushProperty ); }
            set { SetValue( GroupBoxBrushProperty, value ); }
        }
        public static readonly DependencyProperty GroupBoxBrushProperty = DependencyProperty.Register( "GroupBoxBrush", typeof( Brush ), typeof( TagSelector ), new PropertyMetadata( Color.FromArgb( 255, 0, 0, 0 ) ) );
        public Brush LoadingBrush
        {
            get { return (Brush)GetValue( LoadingBrushProperty ); }
            set { SetValue( LoadingBrushProperty, value ); }
        }
        public static readonly DependencyProperty LoadingBrushProperty = DependencyProperty.Register( "LoadingBrush", typeof( Brush ), typeof( TagSelector ), new PropertyMetadata( Color.FromArgb( 255, 0, 0, 0 ) ) );
        #endregion
        private readonly Dictionary<int, ListBox> groups = new Dictionary<int, ListBox>();
        private readonly List<int> checkedTags = new List<int>();

        public TagSelector() {
            InitializeComponent();
        }

        #region Generation
        private void LoadInformation() {
            foreach ( KeyValuePair<int, string> s in TagData.Groups )
                addGroup( s.Key, s.Value );
            foreach ( Tag t in TagData.Tags )
                addTag( t.Index ); //Index may not necessarily be the same as location
        }
        private void addGroup( int key, string name ) {
            GroupBox gb = new GroupBox() { Header = name, Style = (Style)Resources["Tag Groupbox"], Margin = new Thickness( 10, 0, 0, 10 ), Width = 171, FontSize = 18 };
            ListBox content = new ListBox() { Background = new SolidColorBrush( Color.FromArgb( 0, 0, 0, 0 ) ), BorderThickness = new Thickness( 0 ), Foreground = (Brush)Application.Current.Resources["DarkText"], HorizontalContentAlignment = HorizontalAlignment.Stretch, VerticalContentAlignment = VerticalAlignment.Stretch };
            content.SelectionChanged += ( _, _ ) => content.SelectedIndex = -1;
            gb.Content = content;
            groups.Add( key, content );
            stkGroups.Children.Add( gb );
        }
        private void addTag( int t ) {
            ToggleButton tag = new ToggleButton() { Content = TagData.Tags[TagData.TagLocations[t]].Name, Style = (Style)Resources["Tag Button"], Tag = t };
            tag.Checked += ( s, ev ) =>
            {
                int id = (int)((ToggleButton)s).Tag;
                if ( !checkedTags.Contains( id ) ) {
                    checkedTags.Add( id );
                    TagChecked?.Invoke( this, new TagCheckEventArgs() { IsActive = true, TagIndex = id } );
                    foreach ( int t in TagData.Tags[TagData.TagLocations[id]].GetAliases() )
                        addTag( t );
                }
            };
            tag.Unchecked += ( s, ev ) =>
            {
                int id = (int)((ToggleButton)s).Tag;
                if ( checkedTags.Contains( id ) ) {
                    checkedTags.Remove( id );
                    TagChecked?.Invoke( this, new TagCheckEventArgs() { IsActive = false, TagIndex = id } );
                }
            };
            groups[TagData.Tags[TagData.TagLocations[t]].Group].Items.Add( tag );
        }
        #endregion
        #region Events
        private void UserControl_Loaded( object sender, RoutedEventArgs e ) {
            stkGroups.Children.Clear();
            groups.Clear();
            addGroup( -1, "Misc" );
            stkGroups.Dispatcher.BeginInvoke( new Action( LoadInformation ) );
        }
        #endregion
    }
}

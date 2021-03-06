﻿using NanaManagerAPI;
using NanaManagerAPI.Types;
using NanaManagerAPI.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace NanaManager
{
    /// <summary>
    /// Interaction logic for TagManager.xaml
    /// </summary>
    public partial class TagManager : Page //TODO - CLEAN THIS THING UP, IT'S UGLY
    {
        private TextBox selectAlias; //The tag which will be aliased to
        private readonly List<string> tagNames = new List<string>(); //All current tag names
        private readonly Dictionary<int, string> groupNames = new Dictionary<int, string>(); //All current group names
        private bool isTyping = false; //If the user is typing within a tag

        public TagManager() {
            InitializeComponent();
        }

        #region Events

        private void stackPanel_Loaded( object sender, RoutedEventArgs e ) {
            stkGroups.Children.Clear(); //Clears the group visuals
            groupNames.Clear(); //Clears the groups
            createGroup( "Misc" ); //Visualises the default "Misc" group
            foreach ( KeyValuePair<int, string> s in Data.Groups )
                createGroup( s.Value ); //Visualises all groups

            foreach ( Tag t in Data.Tags )
                addTag( getContent( t.Group == -1 ? "Misc" : Data.Groups[t.Group] ), t.Name, new tagData( t.Index, t.GetAliases() ) ); //Visualises all tags in their respective locations
        }

        private void handleListKeyPress( object sender, KeyEventArgs e ) { //Handles keypresses on the groups
            ListBox lst = sender as ListBox; //The list box representing the group
            switch ( e.Key ) {
                case Key.Delete: //If the delete key is pressed, delete the selected tag
                    if ( !isTyping && lst.SelectedItems.Count > 0 )
                        lst.Items.RemoveAt( lst.SelectedIndex );
                    break;

                case Key.OemPlus:
                    if ( !isTyping ) { //If the plus key is pressed, add a tag to the selected group
                        int newTags = 1;
                        foreach ( TextBox t in lst.Items )
                            if ( t.Text.Contains( "New Tag" ) )
                                newTags++; //New Tag 1,2,3,4...
                        addTag( lst, "New Tag " + newTags, new tagData( tagNames.Count, Array.Empty<int>() ) );
                    }
                    break;
            }
        }

        private void handleKeyPress( object sender, KeyEventArgs e ) { //Handles keypresses on tags
            TextBox txt = sender as TextBox;
            switch ( e.Key ) {
                case Key.Enter:
                        //(txt.Parent as ListBox).Items.Remove( txt ); //Deletes the tag if saved with an empty name
                    //else { //Depricated because weird usage
                    if ( !string.IsNullOrEmpty( txt.Text ) )
                        stkGroups.Focus(); //Sets focus to the groupbox, and not the tag
                        e.Handled = true; //Tells the handler to not process the key further
                    }
                    break;
            }
        }

        private void btnNewGroup_Click( object sender, RoutedEventArgs e ) { //Fires whenever the new group button is clicked
            int newGroups = 0;
            foreach ( GroupBox gb in stkGroups.Children ) 
                if ( gb.HeaderTemplate?.LoadContent() is TextBox tx && tx.Text.Contains( "New Group" ) ) //Counts all children of the stackpanel that are a groupbox and have a header containing "New Group"
                    newGroups++;

            createGroup( "New Group " + newGroups ); //Create a new with the desired name
        }

        private void btnDone_Click( object sender, RoutedEventArgs e ) { //Fires whenever the Done button is clicked (Finalise edits)
            bool foundMisc = false; //Tracks whether the group box misc has been iterated past. If 
            foreach ( string name in groupNames.Values ) { //Iterates through all loaded groups to check their naming
                if ( (foundMisc && name == "Misc") || string.IsNullOrEmpty( name ) ) { //Check if the name is misc or empty, as such user defined names are not desired
                    MessageBox.Show( $"Cannot have a group \"{(string.IsNullOrEmpty( name.Value ) ? "with an empty name" : $"named {name}")}\"" ); //Alerts the user that they  have invalid names
                    return;
                }
                else if ( name == "Misc" )
                    foundMisc = true; //Prevents the user getting notified of the default Misc group
            }
            List<string> toReplaceG = new List<string>();
            Dictionary<int, Tag> toReplaceT = new Dictionary<int, Tag>();
            
            foreach ( string gbName in groupNames.Values ) { //
                GroupBox gb = getGroupBox( gbName );
                if ( gbName == "Misc" )
                    foreach ( TextBox tx in ((ListView)gb.Content).Items )
                        toReplaceT[((tagData)tx.Tag).tagIndex] = new Tag( tx.Text, ((tagData)tx.Tag).tagIndex, -1, ((tagData)tx.Tag).aliases.ToArray() ); //Adds the tag to the new tag dictionary with Misc group
                else {
                    foreach ( TextBox tx in ((ListView)gb.Content).Items )
                        toReplaceT[((tagData)tx.Tag).tagIndex] = new Tag( tx.Text, ((tagData)tx.Tag).tagIndex, toReplaceG.Count, ((tagData)tx.Tag).aliases.ToArray() ); //Adds the tag to the new tag dictionary with the current group
                    toReplaceG.Add( gbName.Value ); //Adds the group to the dictionary
                }
            }

            Data.Tags = new Tag[toReplaceT.Count]; //Instances the tag array
            Data.TagLocations.Clear();
            Data.Groups.Clear();
            List<int> hiddenKeep = new List<int>();
            int c = 0;
            foreach ( KeyValuePair<int, Tag> i in toReplaceT ) {
                Data.TagLocations.Add( i.Key, c );
                Data.Tags[c++] = i.Value;
                if ( Data.HiddenTags.Contains( i.Key ) )
                    hiddenKeep.Add( i.Key );
            }
            Data.HiddenTags = hiddenKeep.ToArray();

            for ( int i = 0; i < toReplaceG.Count; i++ )
                Data.Groups[i] = toReplaceG[i];
            MessageBox.Show( "Saved Groups and Tags" );
            Paging.LoadPreviousPage();
        }

        private void menuItem_Click( object sender, RoutedEventArgs e ) {
            int newGroups = 0;
            foreach ( GroupBox gb in stkGroups.Children )
                if ( gb.HeaderTemplate?.LoadContent() is TextBox tx && tx.Text.Contains( "New Group" ) )
                    newGroups++;

            createGroup( "New Group " + newGroups );
        }

        private void btnBack_Click( object sender, RoutedEventArgs e ) => Paging.LoadPreviousPage();

        private void clickHandle( object sender, RoutedEventArgs e ) => (sender as UIElement).Focus();

        private void page_PreviewKeyUp( object sender, KeyEventArgs e ) {
            if ( e.Key == Key.Escape ) {
                selectAlias = null;
                bdrTag.Visibility = Visibility.Collapsed;
                foreach ( GroupBox gb in stkGroups.Children ) {
                    ListView lsb = gb.Content as ListView;
                    foreach ( TextBox tx in lsb.Items ) {
                        tx.Style = (Style)Application.Current.Resources["Tag Textbox Deselect"];
                        tx.ForceCursor = false;
                        tx.Cursor = Cursors.IBeam;
                    }
                    gb.ForceCursor = false;
                }
                e.Handled = true;
            }
        }

        private void page_PreviewMouseRightButtonDown( object sender, MouseButtonEventArgs e ) {
            if ( selectAlias != null )
                e.Handled = true;
        }

        #endregion Events

        #region GroupboxData

        #region GBTextboxData

        private void gbtxt_KeyDown( object sender, KeyEventArgs e ) {
            TextBox txt = sender as TextBox;
            int idx = (int)getGroupBox( groupNames[(int)txt.Tag] ).Tag;
            if ( e.Key == Key.Enter ) {
                if ( txt.Text == "Misc" || string.IsNullOrEmpty( txt.Text ) ) {
                    MessageBox.Show( "Group name cannot be " + (txt.Text == "Misc" ? "Misc" : "blank") + ". Please enter a new name" );
                    txt.Select( 0, 0 );
                    return;
                }
                else
                    stkGroups.Focus();
            }
            groupNames[idx] = txt.Text;
        }

        private void gbtxt_LostFocus( object sender, RoutedEventArgs e ) {
            TextBox txt = sender as TextBox;

            int idx = (int)getGroupBox( groupNames[(int)txt.Tag] ).Tag;
            if ( txt.Text == "Misc" || string.IsNullOrEmpty( txt.Text ) ) {
                MessageBox.Show( "Group name cannot be " + (txt.Text == "Misc" ? "Misc" : "blank") + ". Please enter a new name" );
                txt.Select( 0, 0 );
                return;
            }
            groupNames[idx] = txt.Text;
        }

        private void gbtxt_PreviewMouseDown( object sender, RoutedEventArgs e ) { //TODO - Work out why I did this
            if ( selectAlias != null ) {
                Keyboard.ClearFocus();
                ((ListBox)((GroupBox)((TextBox)sender).Parent).Content).Focus();
                e.Handled = true;
            }
        }

        #endregion GBTextboxData

        private void gb_DragLeave( object sender, DragEventArgs e ) {
            if ( sender is ListBox content )
                content.Background = Brushes.Transparent;
        }

        private void gb_DragEnter( object sender, DragEventArgs e ) {
            if ( sender is ListBox content && e.Data.GetDataPresent( typeof( TextBox ) ) )
                content.Background = (Brush)Application.Current.Resources["DarkGlass"];
        }

        private void gb_Drop( object sender, DragEventArgs e ) {
            Debug.WriteLine( "drop" );
            if ( sender != e.OriginalSource ) {
                TextBox data = (TextBox)e.Data.GetData( typeof( TextBox ) );
                ((ListBox)sender).Background = Brushes.Transparent;
                addTag( (ListBox)sender, data.Text, (tagData)data.Tag ); //Casting so an error occurs without a valid ListBox
            }
        }

        private bool dragInProgress = false;

        private void gb_MouseMove( object sender, MouseEventArgs e ) { //Seriously, this needs fixing. It's a patchy mess
            if ( e.LeftButton == MouseButtonState.Pressed && selectAlias == null && !dragInProgress ) {
                dragInProgress = true;
                ListView parent = (ListView)sender;
                dragparent = parent;
                object data = getDataFromListBox( dragparent, e.GetPosition( parent ) );
                if ( data != null ) {
                    try {
                        Application.Current.Dispatcher.Invoke( new Action<ListView, object>( ( p, d ) =>
                        {
                            try {
                                parent.Items.Remove( (TextBox)data );
                                if ( d != null && DragDrop.DoDragDrop( p, d, DragDropEffects.Move ) == DragDropEffects.None ) {
                                    Debug.WriteLine( "DragDropEffects" );
                                    parent.Items.Add( (TextBox)d );
                                }
                                else
                                    dragInProgress = false;
                            } catch ( Exception ev ) {
                                Logging.Write( ev, "Tag Manager" );
                                Debug.WriteLine( "error" );
                                if ( dragInProgress )
                                    parent.Items.Add( (TextBox)d );
                            }
                        } ), System.Windows.Threading.DispatcherPriority.ApplicationIdle, parent, data );
                    } catch ( InvalidOperationException ) {
                    } finally {
                        dragInProgress = false;
                    }
                }
                else
                    dragInProgress = false;
                //Dispatcher.BeginInvoke( (Action<ListView, object>)(( p, d ) => {
                //}), parent, data );
            }
        }

        private void gb_PreviewMouseDown( object sender, RoutedEventArgs e ) {
            ((ListBox)sender).Focus();
        }

        private void gb_SelectionChanged( object sender, RoutedEventArgs e ) {
            ListBox gbContent = (ListBox)sender;
            if ( gbContent.SelectedIndex != -1 ) {
                if ( selectAlias != null ) {
                    TextBox txt = gbContent.SelectedItem as TextBox;
                    if ( selectAlias != txt ) {
                        if ( ((tagData)selectAlias.Tag).aliases.Contains( ((tagData)txt.Tag).tagIndex ) ) {
                            txt.Style = (Style)Application.Current.Resources["Tag Textbox Deselect"];
                            tagData t = selectAlias.Tag as tagData;
                            t.aliases.Remove( ((tagData)txt.Tag).tagIndex );
                            gbContent.Focus();
                        }
                        else {
                            txt.Style = (Style)Application.Current.Resources["Tag Textbox Select"];
                            tagData t = selectAlias.Tag as tagData;
                            t.aliases.Add( ((tagData)txt.Tag).tagIndex );
                            gbContent.Focus();
                        }
                    }
                    Keyboard.ClearFocus();
                }
                gbContent.SelectedIndex = -1;
            }
        }

        #endregion GroupboxData

        #region Creation

        private int curGroupID = 0;

        private GroupBox createGroup( string name ) {
            int idx = curGroupID;
            GroupBox gb = new GroupBox() { Tag = idx };
            groupNames.Add( curGroupID++, name );
            ScrollViewer.SetHorizontalScrollBarVisibility( gb, ScrollBarVisibility.Disabled );
            ContextMenu ctx = new ContextMenu()
            {
                FontSize = 12
            };
            ListView gbContent = new ListView()
            {
                Background = Brushes.Transparent,
                BorderThickness = new Thickness( 0 ),
                Foreground = (Brush)Application.Current.Resources["LightText"],
                Focusable = true,
                Tag = name,
                AllowDrop = true,
                Margin = new Thickness( 0, 0, 0, 3 )
            };
            ScrollViewer.SetHorizontalScrollBarVisibility( gbContent, ScrollBarVisibility.Disabled );
            gbContent.DragEnter += gb_DragEnter;
            gbContent.DragLeave += gb_DragLeave;
            gbContent.Drop += gb_Drop;
            gbContent.MouseMove += gb_MouseMove;
            gbContent.PreviewMouseDown += gb_PreviewMouseDown;
            gbContent.SelectionChanged += gb_SelectionChanged;

            if ( name != "Misc" ) {
                DataTemplate template = new DataTemplate { DataType = typeof( GroupBox ) };
                FrameworkElementFactory factory = new FrameworkElementFactory( typeof( TextBox ) );
                factory.SetValue( TextBox.TextProperty, name );
                factory.SetValue( TextBox.TagProperty, idx );
                factory.SetValue( TextBox.StyleProperty, Application.Current.Resources["Editable Groupbox Textbox"] );
                factory.AddHandler( TextBox.KeyDownEvent, new KeyEventHandler( gbtxt_KeyDown ) );
                factory.AddHandler( TextBox.LostFocusEvent, new RoutedEventHandler( gbtxt_LostFocus ) );
                factory.AddHandler( TextBox.PreviewMouseDownEvent, new MouseButtonEventHandler( gbtxt_PreviewMouseDown ) );
                template.VisualTree = factory;
                gb.HeaderTemplate = template;

                MenuItem rmv = new MenuItem() { Header = "Remove Group" };
                rmv.Click += ( sender, e ) =>
                {
                    groupNames.Remove( idx );
                    stkGroups.Children.Remove( gb );
                };

                ctx.Items.Add( rmv );
            }
            else {
                gb.Header = "Misc";
                gb.Foreground = (Brush)Application.Current.Resources["LightText"];
                gb.FontSize = 18d;
            }
            MenuItem add = new MenuItem() { Header = "Add Tag" };
            add.Click += ( sender, e ) =>
            {
                int newTags = 1;
                foreach ( TextBox t in gbContent.Items )
                    if ( t.Text.Contains( "New Tag" ) )
                        newTags++;
                addTag( gbContent, "New Tag " + newTags, new tagData( tagNames.Count, Array.Empty<int>() ) );
            };
            ctx.Items.Add( add );
            gbContent.KeyDown += handleListKeyPress;
            gbContent.MouseDown += clickHandle;

            gb.ContextMenu = ctx;

            gb.Width = 171;
            gb.Margin = new Thickness( 7, 0, 0, 0 );
            gb.Background = new SolidColorBrush( (Color)ColorConverter.ConvertFromString( "#22FFFFFF" ) );
            gb.Foreground = (Brush)Application.Current.Resources["LightText"];

            gb.Content = gbContent;
            stkGroups.Children.Add( gb );
            return gb;
        }

        private void addTag( ListBox lb, string name, tagData data ) {
            TextBox txt = new TextBox() { HorizontalAlignment = HorizontalAlignment.Stretch, HorizontalContentAlignment = HorizontalAlignment.Center, Text = name, Style = Application.Current.Resources["Tag Textbox Deselect"] as Style, MaxLength = 16, Foreground = (Brush)Application.Current.Resources["LightText"], FontSize = 14, Tag = data, Width = 140 };
            ContextMenu ctx = new ContextMenu()
            {
                FontSize = 12
            };
            MenuItem rmv = new MenuItem() { Header = "Remove" };
            rmv.Click += ( _, ev ) =>
            {
                lb.Items.Remove( txt );
            };
            ctx.Items.Add( rmv );
            MenuItem alias = new MenuItem() { Header = "Manage Aliases" };
            alias.Click += ( _, ev ) =>
            {
                bdrTag.Visibility = Visibility.Visible;
                List<int> aliases = ((tagData)txt.Tag).aliases;
                foreach ( GroupBox gb in stkGroups.Children ) {
                    ListView lsb = gb.Content as ListView;
                    foreach ( TextBox tx in lsb.Items ) {
                        if ( aliases.Contains( ((tagData)tx.Tag).tagIndex ) )
                            tx.Style = (Style)Application.Current.Resources["Tag Textbox Select"];
                        else if ( tx == txt )
                            tx.Style = (Style)Application.Current.Resources["Tag Textbox Highlight"];
                        tx.ForceCursor = true;
                        tx.Cursor = Cursors.Arrow;
                    }
                    gb.ForceCursor = true;
                    gb.Cursor = Cursors.Arrow;
                }
                selectAlias = txt;
                lb.Focus();
            };
            ctx.Items.Add( alias );
            txt.ContextMenu = ctx;
            txt.KeyDown += handleKeyPress;
            txt.GotKeyboardFocus += ( _, ev ) =>
            {
                isTyping = true;
            };
            txt.LostKeyboardFocus += ( _, ev ) =>
            {
                isTyping = false;
                dragInProgress = false;
            };
            txt.PreviewMouseLeftButtonDown += ( _, ev ) =>
            {
                double x = ev.GetPosition( (TextBox)_ ).X;
                if ( selectAlias != null ) {
                    if ( selectAlias != txt ) {
                        if ( ((tagData)selectAlias.Tag).aliases.Contains( ((tagData)txt.Tag).tagIndex ) ) {
                            txt.Style = (Style)Application.Current.Resources["Tag Textbox Deselect"];
                            tagData t = selectAlias.Tag as tagData;
                            t.aliases.Remove( ((tagData)txt.Tag).tagIndex );
                        }
                        else {
                            txt.Style = (Style)Application.Current.Resources["Tag Textbox Select"];
                            tagData t = selectAlias.Tag as tagData;
                            t.aliases.Add( ((tagData)txt.Tag).tagIndex );
                        }
                    }
                    Keyboard.ClearFocus();
                    lb.Focus();
                    ev.Handled = true;
                }
                else if ( x > 45 && x < 95 )
                    dragInProgress = true;
            };

            lb.Items.Add( txt );
            tagNames.Add( name );
        }

        #endregion Creation

        #region DragDrop

        private ListBox dragparent;

        private static object getDataFromListBox( ListBox source, Point point ) {
            if ( source.InputHitTest( point ) is UIElement element ) {
                object data = DependencyProperty.UnsetValue;
                while ( data == DependencyProperty.UnsetValue ) {
                    data = source.ItemContainerGenerator.ItemFromContainer( element );

                    if ( data == DependencyProperty.UnsetValue )
                        element = VisualTreeHelper.GetParent( element ) as UIElement;
                    if ( element == source )
                        return null;
                }

                if ( data != DependencyProperty.UnsetValue )
                    return data;
            }
            return null;
        }

        #endregion DragDrop

        #region Utilities

        private GroupBox getGroupBox( string groupName ) {
            if ( groupName == "Misc" )
                return stkGroups.Children[0] as GroupBox;
            foreach ( KeyValuePair<int, string> kvp in groupNames )
                if ( kvp.Value == groupName )
                    foreach ( GroupBox gb in stkGroups.Children )
                        if ( (int)gb.Tag == kvp.Key )
                            return gb;
            return null;
        }

        private ListBox getContent( string groupName ) => getGroupBox( groupName ).Content as ListBox;

        #endregion Utilities
    }

#pragma warning disable IDE1006 // Naming Styles

    internal class tagData
    {
        public int tagIndex;
        public List<int> aliases;

        public tagData( int tagIndex, int[] aliases ) {
            this.tagIndex = tagIndex;
            this.aliases = aliases.ToList();
        }
    }

#pragma warning restore IDE1006 // Naming Styles
}

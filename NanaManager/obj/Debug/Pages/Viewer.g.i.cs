﻿#pragma checksum "..\..\..\Pages\Viewer.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "3DC1AAB10216E5C2AAB4C775AADFCAC2AD35E3087DBFEA5C00ED2925E28A8B87"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Windows.Themes;
using NanaManager;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace NanaManager {
    
    
    /// <summary>
    /// Viewer
    /// </summary>
    public partial class Viewer : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 70 "..\..\..\Pages\Viewer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lstImages;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\..\Pages\Viewer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Ellipse elpMenuButton;
        
        #line default
        #line hidden
        
        
        #line 89 "..\..\..\Pages\Viewer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnExport;
        
        #line default
        #line hidden
        
        
        #line 90 "..\..\..\Pages\Viewer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnEdit;
        
        #line default
        #line hidden
        
        
        #line 91 "..\..\..\Pages\Viewer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSettings;
        
        #line default
        #line hidden
        
        
        #line 134 "..\..\..\Pages\Viewer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnTags;
        
        #line default
        #line hidden
        
        
        #line 135 "..\..\..\Pages\Viewer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel stkMenu;
        
        #line default
        #line hidden
        
        
        #line 139 "..\..\..\Pages\Viewer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnImport;
        
        #line default
        #line hidden
        
        
        #line 140 "..\..\..\Pages\Viewer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSearch;
        
        #line default
        #line hidden
        
        
        #line 142 "..\..\..\Pages\Viewer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer scrTags;
        
        #line default
        #line hidden
        
        
        #line 154 "..\..\..\Pages\Viewer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel stkTags;
        
        #line default
        #line hidden
        
        
        #line 156 "..\..\..\Pages\Viewer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border bdrSearch;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/NanaManager;component/pages/viewer.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\Viewer.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 10 "..\..\..\Pages\Viewer.xaml"
            ((NanaManager.Viewer)(target)).Loaded += new System.Windows.RoutedEventHandler(this.page_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.lstImages = ((System.Windows.Controls.ListView)(target));
            
            #line 70 "..\..\..\Pages\Viewer.xaml"
            this.lstImages.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.lstImages_SelectionChanged);
            
            #line default
            #line hidden
            
            #line 70 "..\..\..\Pages\Viewer.xaml"
            this.lstImages.PreviewMouseDown += new System.Windows.Input.MouseButtonEventHandler(this.lstImages_PreviewMouseDown);
            
            #line default
            #line hidden
            
            #line 70 "..\..\..\Pages\Viewer.xaml"
            this.lstImages.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.lstImages_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 3:
            this.elpMenuButton = ((System.Windows.Shapes.Ellipse)(target));
            
            #line 77 "..\..\..\Pages\Viewer.xaml"
            this.elpMenuButton.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.image_MouseDown);
            
            #line default
            #line hidden
            
            #line 77 "..\..\..\Pages\Viewer.xaml"
            this.elpMenuButton.MouseEnter += new System.Windows.Input.MouseEventHandler(this.image_MouseEnter);
            
            #line default
            #line hidden
            
            #line 77 "..\..\..\Pages\Viewer.xaml"
            this.elpMenuButton.MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.image_MouseUp);
            
            #line default
            #line hidden
            
            #line 77 "..\..\..\Pages\Viewer.xaml"
            this.elpMenuButton.MouseLeave += new System.Windows.Input.MouseEventHandler(this.image_MouseLeave);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnExport = ((System.Windows.Controls.Button)(target));
            
            #line 89 "..\..\..\Pages\Viewer.xaml"
            this.btnExport.Click += new System.Windows.RoutedEventHandler(this.btnExport_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnEdit = ((System.Windows.Controls.Button)(target));
            
            #line 90 "..\..\..\Pages\Viewer.xaml"
            this.btnEdit.Click += new System.Windows.RoutedEventHandler(this.btnEdit_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnSettings = ((System.Windows.Controls.Button)(target));
            
            #line 91 "..\..\..\Pages\Viewer.xaml"
            this.btnSettings.Click += new System.Windows.RoutedEventHandler(this.btnSettings_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btnTags = ((System.Windows.Controls.Button)(target));
            
            #line 134 "..\..\..\Pages\Viewer.xaml"
            this.btnTags.Click += new System.Windows.RoutedEventHandler(this.btnTags_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.stkMenu = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 9:
            this.btnImport = ((System.Windows.Controls.Button)(target));
            
            #line 139 "..\..\..\Pages\Viewer.xaml"
            this.btnImport.Click += new System.Windows.RoutedEventHandler(this.btnImport_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.btnSearch = ((System.Windows.Controls.Button)(target));
            
            #line 140 "..\..\..\Pages\Viewer.xaml"
            this.btnSearch.Click += new System.Windows.RoutedEventHandler(this.btnSearch_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.scrTags = ((System.Windows.Controls.ScrollViewer)(target));
            return;
            case 12:
            this.stkTags = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 13:
            this.bdrSearch = ((System.Windows.Controls.Border)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}


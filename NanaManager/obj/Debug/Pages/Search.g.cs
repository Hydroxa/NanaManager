﻿#pragma checksum "..\..\..\Pages\Search.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "A4CC1F375597557C99537FDD0ECEDAEE679F54C754DAC1E9AA4F34006C61459D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using NanaManager;
using NanaManagerAPI.UI.Controls;
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
    /// Search
    /// </summary>
    public partial class Search : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 23 "..\..\..\Pages\Search.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox cbxHiddenTags;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\Pages\Search.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal NanaManagerAPI.UI.Controls.TagSelector tslSearch;
        
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
            System.Uri resourceLocater = new System.Uri("/NanaManager;component/pages/search.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\Search.xaml"
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
            
            #line 10 "..\..\..\Pages\Search.xaml"
            ((NanaManager.Search)(target)).Loaded += new System.Windows.RoutedEventHandler(this.page_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.cbxHiddenTags = ((System.Windows.Controls.CheckBox)(target));
            
            #line 23 "..\..\..\Pages\Search.xaml"
            this.cbxHiddenTags.Checked += new System.Windows.RoutedEventHandler(this.cbxHiddenTags_Checked);
            
            #line default
            #line hidden
            
            #line 23 "..\..\..\Pages\Search.xaml"
            this.cbxHiddenTags.Unchecked += new System.Windows.RoutedEventHandler(this.cbxHiddenTags_Unchecked);
            
            #line default
            #line hidden
            return;
            case 3:
            this.tslSearch = ((NanaManagerAPI.UI.Controls.TagSelector)(target));
            return;
            case 4:
            
            #line 25 "..\..\..\Pages\Search.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.button_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 26 "..\..\..\Pages\Search.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.button_Click_2);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 27 "..\..\..\Pages\Search.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.button_Click_1);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}


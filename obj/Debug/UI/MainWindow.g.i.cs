﻿#pragma checksum "..\..\..\UI\MainWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "8C7FD847AFA8D5BB781750A3B9A981F08F8421ECF3FFE74739D221CBBF0D2CB1"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using MinimapGen;
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


namespace MinimapGen {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 19 "..\..\..\UI\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label mapName;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\UI\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image minimap;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\UI\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton style1;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\UI\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton style2;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\UI\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton style3;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\UI\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.RadioButton style4;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\UI\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ChooseColorBtn;
        
        #line default
        #line hidden
        
        
        #line 51 "..\..\..\UI\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox expandEdgeCheck;
        
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
            System.Uri resourceLocater = new System.Uri("/MinimapGen;component/ui/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\UI\MainWindow.xaml"
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
            
            #line 13 "..\..\..\UI\MainWindow.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.onAbout);
            
            #line default
            #line hidden
            return;
            case 2:
            this.mapName = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.minimap = ((System.Windows.Controls.Image)(target));
            return;
            case 4:
            
            #line 26 "..\..\..\UI\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.onChooseMap);
            
            #line default
            #line hidden
            return;
            case 5:
            this.style1 = ((System.Windows.Controls.RadioButton)(target));
            
            #line 39 "..\..\..\UI\MainWindow.xaml"
            this.style1.Checked += new System.Windows.RoutedEventHandler(this.onChooseStyle);
            
            #line default
            #line hidden
            return;
            case 6:
            this.style2 = ((System.Windows.Controls.RadioButton)(target));
            
            #line 40 "..\..\..\UI\MainWindow.xaml"
            this.style2.Checked += new System.Windows.RoutedEventHandler(this.onChooseStyle);
            
            #line default
            #line hidden
            return;
            case 7:
            this.style3 = ((System.Windows.Controls.RadioButton)(target));
            
            #line 41 "..\..\..\UI\MainWindow.xaml"
            this.style3.Checked += new System.Windows.RoutedEventHandler(this.onChooseStyle);
            
            #line default
            #line hidden
            return;
            case 8:
            this.style4 = ((System.Windows.Controls.RadioButton)(target));
            
            #line 42 "..\..\..\UI\MainWindow.xaml"
            this.style4.Checked += new System.Windows.RoutedEventHandler(this.onChooseStyle);
            
            #line default
            #line hidden
            return;
            case 9:
            this.ChooseColorBtn = ((System.Windows.Controls.Button)(target));
            
            #line 45 "..\..\..\UI\MainWindow.xaml"
            this.ChooseColorBtn.Click += new System.Windows.RoutedEventHandler(this.onChooseColor);
            
            #line default
            #line hidden
            return;
            case 10:
            this.expandEdgeCheck = ((System.Windows.Controls.CheckBox)(target));
            
            #line 51 "..\..\..\UI\MainWindow.xaml"
            this.expandEdgeCheck.Checked += new System.Windows.RoutedEventHandler(this.onCheckExpandEdge);
            
            #line default
            #line hidden
            
            #line 52 "..\..\..\UI\MainWindow.xaml"
            this.expandEdgeCheck.Unchecked += new System.Windows.RoutedEventHandler(this.onCheckExpandEdge);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 57 "..\..\..\UI\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.onPreview);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 58 "..\..\..\UI\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.onSave);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}


﻿#pragma checksum "..\..\EmployeeWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "737A19578D29D5CE870BFB61911725225684A6DBA63720148A550390D227D7DE"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ten kod został wygenerowany przez narzędzie.
//     Wersja wykonawcza:4.0.30319.42000
//
//     Zmiany w tym pliku mogą spowodować nieprawidłowe zachowanie i zostaną utracone, jeśli
//     kod zostanie ponownie wygenerowany.
// </auto-generated>
//------------------------------------------------------------------------------

using CarRentalSystem;
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


namespace CarRentalSystem {
    
    
    /// <summary>
    /// EmployeeWindow
    /// </summary>
    public partial class EmployeeWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 15 "..\..\EmployeeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label HelloLabel;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\EmployeeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label TitleLabel;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\EmployeeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox BrandTextBox;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\EmployeeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ModelTextBox;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\EmployeeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ProductionYearTextBox;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\EmployeeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox RegistrationNumberTextBox;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\EmployeeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ColorTextBox;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\EmployeeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox CarTypeTextBox;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\EmployeeWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox PricePerDayTextBox;
        
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
            System.Uri resourceLocater = new System.Uri("/CarRentalSystem;component/employeewindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\EmployeeWindow.xaml"
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
            this.HelloLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 2:
            this.TitleLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.BrandTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 21 "..\..\EmployeeWindow.xaml"
            this.BrandTextBox.GotFocus += new System.Windows.RoutedEventHandler(this.TextBox_GotFocus);
            
            #line default
            #line hidden
            
            #line 21 "..\..\EmployeeWindow.xaml"
            this.BrandTextBox.LostFocus += new System.Windows.RoutedEventHandler(this.TextBox_LostFocus);
            
            #line default
            #line hidden
            return;
            case 4:
            this.ModelTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 22 "..\..\EmployeeWindow.xaml"
            this.ModelTextBox.GotFocus += new System.Windows.RoutedEventHandler(this.TextBox_GotFocus);
            
            #line default
            #line hidden
            
            #line 22 "..\..\EmployeeWindow.xaml"
            this.ModelTextBox.LostFocus += new System.Windows.RoutedEventHandler(this.TextBox_LostFocus);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ProductionYearTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 23 "..\..\EmployeeWindow.xaml"
            this.ProductionYearTextBox.GotFocus += new System.Windows.RoutedEventHandler(this.TextBox_GotFocus);
            
            #line default
            #line hidden
            
            #line 23 "..\..\EmployeeWindow.xaml"
            this.ProductionYearTextBox.LostFocus += new System.Windows.RoutedEventHandler(this.TextBox_LostFocus);
            
            #line default
            #line hidden
            return;
            case 6:
            this.RegistrationNumberTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 24 "..\..\EmployeeWindow.xaml"
            this.RegistrationNumberTextBox.GotFocus += new System.Windows.RoutedEventHandler(this.TextBox_GotFocus);
            
            #line default
            #line hidden
            
            #line 24 "..\..\EmployeeWindow.xaml"
            this.RegistrationNumberTextBox.LostFocus += new System.Windows.RoutedEventHandler(this.TextBox_LostFocus);
            
            #line default
            #line hidden
            return;
            case 7:
            this.ColorTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 25 "..\..\EmployeeWindow.xaml"
            this.ColorTextBox.GotFocus += new System.Windows.RoutedEventHandler(this.TextBox_GotFocus);
            
            #line default
            #line hidden
            
            #line 25 "..\..\EmployeeWindow.xaml"
            this.ColorTextBox.LostFocus += new System.Windows.RoutedEventHandler(this.TextBox_LostFocus);
            
            #line default
            #line hidden
            return;
            case 8:
            this.CarTypeTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 26 "..\..\EmployeeWindow.xaml"
            this.CarTypeTextBox.GotFocus += new System.Windows.RoutedEventHandler(this.TextBox_GotFocus);
            
            #line default
            #line hidden
            
            #line 26 "..\..\EmployeeWindow.xaml"
            this.CarTypeTextBox.LostFocus += new System.Windows.RoutedEventHandler(this.TextBox_LostFocus);
            
            #line default
            #line hidden
            return;
            case 9:
            this.PricePerDayTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 27 "..\..\EmployeeWindow.xaml"
            this.PricePerDayTextBox.GotFocus += new System.Windows.RoutedEventHandler(this.TextBox_GotFocus);
            
            #line default
            #line hidden
            
            #line 27 "..\..\EmployeeWindow.xaml"
            this.PricePerDayTextBox.LostFocus += new System.Windows.RoutedEventHandler(this.TextBox_LostFocus);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 28 "..\..\EmployeeWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AddCarButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

﻿#pragma checksum "C:\Users\Ostiv\source\repos\New\InterfaceSecureSphere\JobMenuPage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "F45DDE9596B2E429C0DAE769600875B857636EBE3CB00F88F8C1ED829D2B1B20"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InterfaceSecureSphere
{
    partial class JobMenuPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 0.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // JobMenuPage.xaml line 118
                {
                    this.JobsListView = (global::Windows.UI.Xaml.Controls.ListView)(target);
                }
                break;
            case 3: // JobMenuPage.xaml line 109
                {
                    global::Windows.UI.Xaml.Controls.Button element3 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element3).Click += this.OnAddButtonClick;
                }
                break;
            case 4: // JobMenuPage.xaml line 112
                {
                    global::Windows.UI.Xaml.Controls.Button element4 = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)element4).Click += this.OnRunAllButtonClick;
                }
                break;
            case 5: // JobMenuPage.xaml line 94
                {
                    this.DeleteButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.DeleteButton).Click += this.OnDeleteButtonClick;
                }
                break;
            case 6: // JobMenuPage.xaml line 86
                {
                    this.RunButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.RunButton).Click += this.OnRunButtonClick;
                }
                break;
            case 7: // JobMenuPage.xaml line 81
                {
                    this.TypeTextBlock = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 8: // JobMenuPage.xaml line 74
                {
                    this.TargetTextBlock = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 9: // JobMenuPage.xaml line 66
                {
                    this.SourceTextBlock = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 10: // JobMenuPage.xaml line 59
                {
                    this.NameTextBlock = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 0.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}


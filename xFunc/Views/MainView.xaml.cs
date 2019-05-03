﻿// Copyright 2012-2014 Dmitry Kischenko
//
// Licensed under the Apache License, Version 2.0 (the "License"); 
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software 
// distributed under the License is distributed on an "AS IS" BASIS, 
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either 
// express or implied. 
// See the License for the specific language governing permissions and 
// limitations under the License.
using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml.Linq;
using System.IO;
using xFunc.Maths;
using xFunc.Maths.Expressions;
using xFunc.Maths.Expressions.Collections;
using xFunc.Presenters;
using xFunc.Properties;
using xFunc.Resources;
using xFunc.ViewModels;

namespace xFunc.Views
{

    public partial class MainView : Fluent.MetroWindow
    {

        private Processor processor;

        private MathPresenter mathPresenter;
        private LogicPresenter logicPresenter;
        private GraphsPresenter graphsPresenter;
        private TruthTablePresenter truthTablePresenter;
        private Updater updater;
        private string fileName;

        #region Commands

        public static RoutedCommand NewCommand = new RoutedCommand();
        public static RoutedCommand OpenCommand = new RoutedCommand();
        public static RoutedCommand SaveCommand = new RoutedCommand();
        public static RoutedCommand SaveAsCommand = new RoutedCommand();

        public static RoutedCommand DegreeCommand = new RoutedCommand();
        public static RoutedCommand RadianCommand = new RoutedCommand();
        public static RoutedCommand GradianCommand = new RoutedCommand();

        public static RoutedCommand BinCommand = new RoutedCommand();
        public static RoutedCommand OctCommand = new RoutedCommand();
        public static RoutedCommand DecCommand = new RoutedCommand();
        public static RoutedCommand HexCommand = new RoutedCommand();

        public static RoutedCommand VariablesCommand = new RoutedCommand();
        public static RoutedCommand FunctionsCommand = new RoutedCommand();

        public static RoutedCommand DeleteExpCommand = new RoutedCommand();
        public static RoutedCommand ClearCommand = new RoutedCommand();

        public static RoutedCommand ConverterCommand = new RoutedCommand();

        public static RoutedCommand AboutCommand = new RoutedCommand();
        public static RoutedCommand SettingsCommand = new RoutedCommand();
        public static RoutedCommand ExitCommand = new RoutedCommand();

        #endregion Commands

        private VariableView variableView;
        private FunctionView functionView;
        private Converter converterView;

        // 時間計測用変数
        Boolean KeisokuJissichu;
        DateTime KeisokuStartTime;
        DateTime KeisokuEndtTime;


        public MainView()
        {
            InitializeComponent();

            processor = new Processor();

            mathPresenter = new MathPresenter(this.mathControl, processor);
            mathPresenter.PropertyChanged += mathPresenter_PropertyChanged;
            this.mathControl.Presenter = mathPresenter;
            logicPresenter = new LogicPresenter(this.logicControl);
            this.logicControl.Presenter = logicPresenter;
            graphsPresenter = new GraphsPresenter(this.graphsControl, processor);
            this.graphsControl.Presenter = graphsPresenter;
            truthTablePresenter = new TruthTablePresenter();
            this.truthTableControl.Presenter = truthTablePresenter;
            updater = new Updater();

            LoadSettings();

            SetFocus();

            KeisokuJissichu = false;
        }

        private void mathPresenter_PropertyChanged(object o, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == "AngleMeasurement")
            {
                switch (mathPresenter.AngleMeasurement)
                {
                    case AngleMeasurement.Degree:
                        radianButton.IsChecked = false;
                        gradianButton.IsChecked = false;
                        degreeButton.IsChecked = true;
                        break;
                    case AngleMeasurement.Radian:
                        degreeButton.IsChecked = false;
                        gradianButton.IsChecked = false;
                        radianButton.IsChecked = true;
                        break;
                    case AngleMeasurement.Gradian:
                        degreeButton.IsChecked = false;
                        radianButton.IsChecked = false;
                        gradianButton.IsChecked = true;
                        break;
                }
            }
            else if (args.PropertyName == "Base")
            {
                switch (mathPresenter.Base)
                {
                    case NumeralSystem.Binary:
                        octButton.IsChecked = false;
                        decButton.IsChecked = false;
                        hexButton.IsChecked = false;
                        binButton.IsChecked = true;
                        break;
                    case NumeralSystem.Octal:
                        binButton.IsChecked = false;
                        decButton.IsChecked = false;
                        hexButton.IsChecked = false;
                        octButton.IsChecked = true;
                        break;
                    case NumeralSystem.Decimal:
                        binButton.IsChecked = false;
                        octButton.IsChecked = false;
                        hexButton.IsChecked = false;
                        decButton.IsChecked = true;
                        break;
                    case NumeralSystem.Hexidecimal:
                        binButton.IsChecked = false;
                        octButton.IsChecked = false;
                        decButton.IsChecked = false;
                        hexButton.IsChecked = true;
                        break;
                }
            }
        }

        private void this_Loaded(object o, RoutedEventArgs args)
        {
            if (Settings.Default.CheckUpdates)
            {
                var updaterTask = Task.Factory.StartNew(() => updater.CheckUpdates());
                updaterTask.ContinueWith(t =>
                {
                    if (t.Result)
                    {
                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            this.updateText.Text = Resource.AvailableUpdate;
                            this.statusUpdate.Visibility = Visibility.Visible;
                        }));
                    }
                });
            }
        }

        private void UpdateText_MouseUp(object o, MouseButtonEventArgs args)
        {
            if (updater.HasUpdates)
            {
                Process.Start(updater.UpdateUrl);
            }
        }

        private void hideNotification_Click(object o, RoutedEventArgs args)
        {
            this.updateText.Text = string.Empty;
            this.statusUpdate.Visibility = Visibility.Collapsed;
        }

        private void dontCheckUpdates_Click(object o, RoutedEventArgs args)
        {
            hideNotification_Click(o, args);

            Settings.Default.CheckUpdates = false;
            Settings.Default.Save();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            SaveSettings();

            base.OnClosing(e);
        }

        private void LoadSettings()
        {
            if (Settings.Default.SaveUserFunction && Settings.Default.UserFunctions != null)
                foreach (var func in Settings.Default.UserFunctions)
                    processor.Solve(func);

            if (Settings.Default.WindowState != WindowState.Minimized)
            {
                WindowState = Settings.Default.WindowState;

                if (Settings.Default.WindowTop == 0 || Settings.Default.WindowLeft == 0)
                {
                    Top = (SystemParameters.PrimaryScreenHeight - Height) / 2;
                    Left = (SystemParameters.PrimaryScreenWidth - Width) / 2;
                }
                else
                {
                    Top = Settings.Default.WindowTop;
                    Left = Settings.Default.WindowLeft;
                }
            }
            Width = Settings.Default.WindowWidth;
            Height = Settings.Default.WindowHeight;

            tabControl.SelectedIndex = Settings.Default.SelectedTabIndex;

            mathPresenter.AngleMeasurement = Settings.Default.AngleMeasurement;
            mathPresenter.Base = Settings.Default.NumberBase;

            numberToolBar.IsExpanded = Settings.Default.NumbersExpanded;
            standartMathToolBar.IsExpanded = Settings.Default.StandartMathExpanded;
            trigonometricToolBar.IsExpanded = Settings.Default.TrigonometricExpanded;
            hyperbolicToolBar.IsExpanded = Settings.Default.HyperbolicExpanded;
            matrixToolBar.IsExpanded = Settings.Default.MatrixExpanded;
            bitwiseToolBar.IsExpanded = Settings.Default.BitwiseExpanded;
            progToolBar.IsExpanded = Settings.Default.ProgExpanded;
            constantsMathToolBar.IsExpanded = Settings.Default.ConstantsMathExpanded;
            additionalMathToolBar.IsExpanded = Settings.Default.AdditionalMathExpanded;

            standartLogicToolBar.IsExpanded = Settings.Default.StandartLogicExpanded;
            constantsLogicToolBar.IsExpanded = Settings.Default.ConstantsLogicExpanded;
            additionalLogicToolBar.IsExpanded = Settings.Default.AdditionalLogicExpanded;
        }

        private void SaveSettings()
        {
            if (Settings.Default.SaveUserFunction)
            {
                if (processor.UserFunctions.Count > 0)
                    Settings.Default.UserFunctions = new System.Collections.Specialized.StringCollection();
                foreach (var item in processor.UserFunctions)
                    Settings.Default.UserFunctions.Add(string.Format("{0}:={1}", item.Key, item.Value));
            }

            if (Settings.Default.RememberSizeAndPosition)
            {
                if (WindowState != WindowState.Minimized)
                    Settings.Default.WindowState = WindowState;

                Settings.Default.WindowTop = Top;
                Settings.Default.WindowLeft = Left;

                Settings.Default.WindowWidth = Width;
                Settings.Default.WindowHeight = Height;

                Settings.Default.SelectedTabIndex = tabControl.SelectedIndex;
            }
            else
            {
                Settings.Default.WindowState = (WindowState)Enum.Parse(typeof(WindowState), Settings.Default.Properties["WindowState"].DefaultValue.ToString());

                Settings.Default.WindowTop = double.Parse(Settings.Default.Properties["WindowTop"].DefaultValue.ToString());
                Settings.Default.WindowLeft = double.Parse(Settings.Default.Properties["WindowLeft"].DefaultValue.ToString());

                Settings.Default.WindowWidth = double.Parse(Settings.Default.Properties["WindowWidth"].DefaultValue.ToString());
                Settings.Default.WindowHeight = double.Parse(Settings.Default.Properties["WindowHeight"].DefaultValue.ToString());

                Settings.Default.SelectedTabIndex = int.Parse(Settings.Default.Properties["SelectedTabIndex"].DefaultValue.ToString());
            }

            if (Settings.Default.RememberBaseAndAngle)
            {
                Settings.Default.AngleMeasurement = mathPresenter.AngleMeasurement;
                Settings.Default.NumberBase = mathPresenter.Base;
            }

            if (Settings.Default.RememberRightToolBar)
            {
                Settings.Default.NumbersExpanded = numberToolBar.IsExpanded;
                Settings.Default.StandartMathExpanded = standartMathToolBar.IsExpanded;
                Settings.Default.TrigonometricExpanded = trigonometricToolBar.IsExpanded;
                Settings.Default.HyperbolicExpanded = hyperbolicToolBar.IsExpanded;
                Settings.Default.MatrixExpanded = matrixToolBar.IsExpanded;
                Settings.Default.BitwiseExpanded = bitwiseToolBar.IsExpanded;
                Settings.Default.ProgExpanded = progToolBar.IsExpanded;
                Settings.Default.ConstantsMathExpanded = constantsMathToolBar.IsExpanded;
                Settings.Default.AdditionalMathExpanded = additionalMathToolBar.IsExpanded;

                Settings.Default.StandartLogicExpanded = standartLogicToolBar.IsExpanded;
                Settings.Default.ConstantsLogicExpanded = constantsLogicToolBar.IsExpanded;
                Settings.Default.AdditionalLogicExpanded = additionalLogicToolBar.IsExpanded;
            }
            else
            {
                Settings.Default.NumbersExpanded = bool.Parse(Settings.Default.Properties["NumbersExpanded"].DefaultValue.ToString());
                Settings.Default.StandartMathExpanded = bool.Parse(Settings.Default.Properties["StandartMathExpanded"].DefaultValue.ToString());
                Settings.Default.TrigonometricExpanded = bool.Parse(Settings.Default.Properties["TrigonometricExpanded"].DefaultValue.ToString());
                Settings.Default.HyperbolicExpanded = bool.Parse(Settings.Default.Properties["HyperbolicExpanded"].DefaultValue.ToString());
                Settings.Default.BitwiseExpanded = bool.Parse(Settings.Default.Properties["BitwiseExpanded"].DefaultValue.ToString());
                Settings.Default.ConstantsMathExpanded = bool.Parse(Settings.Default.Properties["ConstantsMathExpanded"].DefaultValue.ToString());
                Settings.Default.AdditionalMathExpanded = bool.Parse(Settings.Default.Properties["AdditionalMathExpanded"].DefaultValue.ToString());

                Settings.Default.StandartLogicExpanded = bool.Parse(Settings.Default.Properties["StandartLogicExpanded"].DefaultValue.ToString());
                Settings.Default.ConstantsLogicExpanded = bool.Parse(Settings.Default.Properties["ConstantsLogicExpanded"].DefaultValue.ToString());
                Settings.Default.AdditionalLogicExpanded = bool.Parse(Settings.Default.Properties["AdditionalLogicExpanded"].DefaultValue.ToString());
            }

            Settings.Default.Save();
        }

        private void Serialize(string path)
        {
            var exps = new XElement("expressions", mathPresenter.Workspace.Select(exp => new XElement("expression", exp.StringExpression)));
            var vars = new XElement("variables",
                from @var in processor.Parameters
                where @var.Type != ParameterType.Constant
                select new XElement("add",
                        new XAttribute("key", @var.Key),
                        new XAttribute("value", @var.Value.ToString(CultureInfo.InvariantCulture)),
                        new XAttribute("readonly", @var.Type == ParameterType.ReadOnly ? true : false)));
            var funcs = new XElement("functions",
                from func in processor.UserFunctions
                select new XElement("add",
                        new XAttribute("key", func.Key.ToString()),
                        new XAttribute("value", func.Value.ToString())));

            var root = new XElement("xfunc",
                                    exps.IsEmpty ? null : exps,
                                    vars.IsEmpty ? null : vars,
                                    funcs.IsEmpty ? null : funcs);
            var doc = new XDocument(new XDeclaration("1.0", "UTF-8", null), root);

            doc.Save(path);
        }

        private void Deserialize(string path)
        {
            var doc = XDocument.Load(path);
            var vars = doc.Root.Element("variables");
            if (vars != null)
                foreach (var item in vars.Elements("add"))
                    processor.Parameters.Add(new Parameter(item.Attribute("key").Value, double.Parse(item.Attribute("value").Value), bool.Parse(item.Attribute("readonly").Value) ? ParameterType.ReadOnly : ParameterType.Normal));

            var funcs = doc.Root.Element("functions");
            if (funcs != null)
                foreach (var item in funcs.Elements("add"))
                    processor.Solve(string.Format("{0}:={1}", item.Attribute("key").Value, item.Attribute("value").Value));

            var exps = doc.Root.Element("expressions");
            if (exps != null)
                foreach (var item in exps.Elements("expression").Select(exp => exp.Value))
                    mathPresenter.Add(item);
        }

        private void SetFocus()
        {
            if (tabControl.SelectedItem == mathTab)
                this.mathControl.mathExpressionBox.Focus();
            if (tabControl.SelectedItem == logicTab)
                this.logicControl.logicExpressionBox.Focus();
            if (tabControl.SelectedItem == graphsTab)
                this.graphsControl.graphExpressionBox.Focus();
            if (tabControl.SelectedItem == truthTableTab)
                this.truthTableControl.truthTableExpressionBox.Focus();
        }

        #region Commands

        private void NewCommand_Execute(object o, ExecutedRoutedEventArgs args)
        {
            mathPresenter.Clear();
            processor.Parameters.Clear();
            fileName = null;
        }

        private void OpenCommand_Execute(object o, ExecutedRoutedEventArgs args)
        {
            var ofd = new OpenFileDialog()
            {
                FileName = "xFunc Document",
                DefaultExt = ".xml",
                Filter = "xFunc File (*.xml)|*.xml|All Files (*.*)|*.*",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            if (ofd.ShowDialog() == true)
            {
                Deserialize(ofd.FileName);
                fileName = ofd.FileName;
            }
        }

        private void SaveCommand_Execute(object o, ExecutedRoutedEventArgs args)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                SaveAsCommand_Execute(o, args);
            else
                Serialize(fileName);
        }

        private void SaveAsCommand_Execute(object o, ExecutedRoutedEventArgs args)
        {
            var sfd = new SaveFileDialog()
            {
                FileName = "xFunc Document",
                DefaultExt = ".xml",
                Filter = "xFunc File (*.xml)|*.xml|All Files (*.*)|*.*",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            if (sfd.ShowDialog() == true)
            {
                Serialize(sfd.FileName);
                fileName = sfd.FileName;
            }
        }

        private void DegreeButton_Execute(object o, ExecutedRoutedEventArgs args)
        {
            mathPresenter.AngleMeasurement = AngleMeasurement.Degree;
        }

        private void RadianButton_Execute(object o, ExecutedRoutedEventArgs args)
        {
            mathPresenter.AngleMeasurement = AngleMeasurement.Radian;
        }

        private void GradianButton_Execute(object o, ExecutedRoutedEventArgs args)
        {
            mathPresenter.AngleMeasurement = AngleMeasurement.Gradian;
        }

        private void AndleButtons_CanExecute(object o, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = tabControl.SelectedItem == mathTab;
        }

        private void BinCommand_Execute(object o, ExecutedRoutedEventArgs args)
        {
            mathPresenter.Base = NumeralSystem.Binary;
        }

        private void OctCommand_Execute(object o, ExecutedRoutedEventArgs args)
        {
            mathPresenter.Base = NumeralSystem.Octal;
        }

        private void DecCommand_Execute(object o, ExecutedRoutedEventArgs args)
        {
            mathPresenter.Base = NumeralSystem.Decimal;
        }

        private void HexCommand_Execute(object o, ExecutedRoutedEventArgs args)
        {
            mathPresenter.Base = NumeralSystem.Hexidecimal;
        }

        private void BaseCommands_CanExecute(object o, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = tabControl.SelectedItem == mathTab;
        }

        private void VariablesCommand_Execute(object o, ExecutedRoutedEventArgs args)
        {
            if (variableView == null)
            {
                variableView = new VariableView(this.processor)
                {
                    Owner = this,
                    Top = Settings.Default.VarWindowTop == -1 ? this.Top + 100 : Settings.Default.VarWindowTop,
                    Left = Settings.Default.VarWindowLeft == -1 ? this.Left + this.Width - 300 : Settings.Default.VarWindowLeft,
                    Width = Settings.Default.VarWindowWidth,
                    Height = Settings.Default.VarWindowHeight
                };
                variableView.Closed += (lo, larg) =>
                {
                    if (Settings.Default.RememberSizeAndPosition)
                    {
                        Settings.Default.VarWindowTop = variableView.Top;
                        Settings.Default.VarWindowLeft = variableView.Left;
                        Settings.Default.VarWindowWidth = variableView.Width;
                        Settings.Default.VarWindowHeight = variableView.Height;
                    }
                    else
                    {
                        Settings.Default.VarWindowTop = double.Parse(Settings.Default.Properties["VarWindowTop"].DefaultValue.ToString());
                        Settings.Default.VarWindowLeft = double.Parse(Settings.Default.Properties["VarWindowLeft"].DefaultValue.ToString());

                        Settings.Default.VarWindowWidth = double.Parse(Settings.Default.Properties["VarWindowWidth"].DefaultValue.ToString());
                        Settings.Default.VarWindowHeight = double.Parse(Settings.Default.Properties["VarWindowHeight"].DefaultValue.ToString());
                    }
                    variableView = null;
                };
            }

            if (variableView.Visibility == Visibility.Visible)
                variableView.Activate();
            else
                variableView.Visibility = Visibility.Visible;
        }

        private void VariablesCommand_CanExecute(object o, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = tabControl.SelectedItem == mathTab;
        }

        private void FunctionsCommand_Execute(object o, ExecutedRoutedEventArgs args)
        {
            if (functionView == null)
            {
                functionView = new FunctionView(processor)
                {
                    Owner = this,
                    Top = Settings.Default.FuncWindowTop == -1 ? this.Top + 100 : Settings.Default.FuncWindowTop,
                    Left = Settings.Default.FuncWindowLeft == -1 ? this.Left + this.Width - 300 : Settings.Default.FuncWindowLeft,
                    Width = Settings.Default.FuncWindowWidth,
                    Height = Settings.Default.FuncWindowHeight
                };
                functionView.Closed += (lo, larg) =>
                {
                    if (Settings.Default.RememberSizeAndPosition)
                    {
                        Settings.Default.FuncWindowTop = functionView.Top;
                        Settings.Default.FuncWindowLeft = functionView.Left;
                        Settings.Default.FuncWindowWidth = functionView.Width;
                        Settings.Default.FuncWindowHeight = functionView.Height;
                    }
                    else
                    {
                        Settings.Default.FuncWindowTop = double.Parse(Settings.Default.Properties["FuncWindowTop"].DefaultValue.ToString());
                        Settings.Default.FuncWindowLeft = double.Parse(Settings.Default.Properties["FuncWindowLeft"].DefaultValue.ToString());

                        Settings.Default.FuncWindowWidth = double.Parse(Settings.Default.Properties["FuncWindowWidth"].DefaultValue.ToString());
                        Settings.Default.FuncWindowHeight = double.Parse(Settings.Default.Properties["FuncWindowHeight"].DefaultValue.ToString());
                    }
                    functionView = null;
                };
            }

            if (functionView.Visibility == Visibility.Visible)
                functionView.Activate();
            else
                functionView.Visibility = Visibility.Visible;
        }

        private void FunctionsCommand_CanExecute(object o, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = tabControl.SelectedItem == mathTab;
        }

        private void DeleteExp_Execute(object o, ExecutedRoutedEventArgs args)
        {
            if (tabControl.SelectedItem == mathTab)
            {
                var item = (MathWorkspaceItemViewModel)this.mathControl.mathExpsListBox.SelectedItem;

                mathPresenter.Remove(item);
            }
            else if (tabControl.SelectedItem == logicTab)
            {
                var item = (LogicWorkspaceItemViewModel)this.logicControl.logicExpsListBox.SelectedItem;

                logicPresenter.Remove(item);
            }
            else if (tabControl.SelectedItem == graphsTab)
            {
                var item = (GraphItemViewModel)this.graphsControl.graphsList.SelectedItem;

                graphsPresenter.Remove(item);
            }
        }

        private void DeleteExp_CanExecute(object o, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = (tabControl.SelectedItem == mathTab && this.mathControl.mathExpsListBox.SelectedItem != null) ||
                              (tabControl.SelectedItem == logicTab && this.logicControl.logicExpsListBox.SelectedItem != null) ||
                              (tabControl.SelectedItem == graphsTab && this.graphsControl.graphsList.SelectedItem != null);
        }

        private void Clear_Execute(object o, ExecutedRoutedEventArgs args)
        {
            if (tabControl.SelectedItem == mathTab)
                mathPresenter.Clear();
            else if (tabControl.SelectedItem == logicTab)
                logicPresenter.Clear();
            else if (tabControl.SelectedItem == graphsTab)
                graphsPresenter.Clear();
        }

        private void Clear_CanExecute(object o, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = tabControl.SelectedItem == mathTab ||
                              tabControl.SelectedItem == logicTab ||
                              tabControl.SelectedItem == graphsTab;
        }

        private void ConverterCommand_Execute(object o, ExecutedRoutedEventArgs args)
        {
            if (converterView == null)
            {
                converterView = new Converter()
                {
                    Owner = this,
                    Top = Settings.Default.ConverterTop == -1 ? this.Top + 100 : Settings.Default.ConverterTop,
                    Left = Settings.Default.ConverterLeft == -1 ? this.Left + 300 : Settings.Default.ConverterLeft
                };
                converterView.Closed += (obj, args1) =>
                {
                    if (Settings.Default.RememberSizeAndPosition)
                    {
                        Settings.Default.ConverterTop = converterView.Top;
                        Settings.Default.ConverterLeft = converterView.Left;
                    }
                    else
                    {
                        Settings.Default.ConverterTop = double.Parse(Settings.Default.Properties["ConverterTop"].DefaultValue.ToString());
                        Settings.Default.ConverterLeft = double.Parse(Settings.Default.Properties["ConverterLeft"].DefaultValue.ToString());
                    }

                    converterView = null;
                };
            }

            if (converterView.Visibility == Visibility.Visible)
                converterView.Activate();
            else
                converterView.Visibility = Visibility.Visible;
        }

        private void AboutCommand_Execute(object o, ExecutedRoutedEventArgs args)
        {
            AboutView aboutView = new AboutView { Owner = this };
            aboutView.ShowDialog();
        }

        private void SettingsCommand_Execute(object o, ExecutedRoutedEventArgs args)
        {
            var settingsView = new SettingsView()
            {
                Owner = this
            };
            if (settingsView.ShowDialog() == true)
            {
                Settings.Default.Lang = settingsView.ProgramLanguage;
                Settings.Default.RememberSizeAndPosition = settingsView.RememberStateAndPosition;
                Settings.Default.RememberRightToolBar = settingsView.RememberRightToolBar;
                Settings.Default.RememberBaseAndAngle = settingsView.RememberNumberAndAngle;
                if (!settingsView.RememberNumberAndAngle)
                {
                    Settings.Default.AngleMeasurement = settingsView.Angle;
                    Settings.Default.NumberBase = settingsView.Base;

                    mathPresenter.AngleMeasurement = settingsView.Angle;
                    mathPresenter.Base = settingsView.Base;
                }
                Settings.Default.MaxCountOfExpressions = settingsView.MaxCountOfExps;
                Settings.Default.SaveUserFunction = settingsView.SaveUserFunctions;
                Settings.Default.CheckUpdates = settingsView.CheckUpdates;

                Settings.Default.Save();
            }
            else
            {
                Settings.Default.Reload();
            }
        }

        private void ExitCommand_Execute(object o, ExecutedRoutedEventArgs args)
        {
            Application.Current.Shutdown();
        }

        #endregion Commands

        private TextBox GetSelectedTextBox()
        {
            if (tabControl.SelectedItem == mathTab)
                return this.mathControl.mathExpressionBox;
            if (tabControl.SelectedItem == logicTab)
                return this.logicControl.logicExpressionBox;
            if (tabControl.SelectedItem == graphsTab)
                return this.graphsControl.graphExpressionBox;
            if (tabControl.SelectedItem == truthTableTab)
                return this.truthTableControl.truthTableExpressionBox;

            return null;
        }

        private void InsertChar_Click(object o, RoutedEventArgs args)
        {
            var tag = ((Button)o).Tag.ToString();
            TextBox tb = GetSelectedTextBox();

            var prevSelectionStart = tb.SelectionStart;
            tb.Text = tb.Text.Insert(prevSelectionStart, tag);
            tb.SelectionStart = prevSelectionStart + tag.Length;
            tb.Focus();
        }

        private void InsertFunc_Click(object o, RoutedEventArgs args)
        {
            string func = ((Button)o).Tag.ToString();
            TextBox tb = GetSelectedTextBox();

            var prevSelectionStart = tb.SelectionStart;

            if (tb.SelectionLength > 0)
            {
                var prevSelectionLength = tb.SelectionLength;

                tb.Text = tb.Text.Insert(prevSelectionStart, func + "(").Insert(prevSelectionStart + prevSelectionLength + func.Length + 1, ")");
                tb.SelectionStart = prevSelectionStart + func.Length + prevSelectionLength + 2;
            }
            else
            {
                tb.Text = tb.Text.Insert(prevSelectionStart, func + "()");
                tb.SelectionStart = prevSelectionStart + func.Length + 1;
            }

            tb.Focus();
        }

        private void InsertInv_Click(object o, RoutedEventArgs args)
        {
            string func = ((Button)o).Tag.ToString();
            TextBox tb = GetSelectedTextBox();

            var prevSelectionStart = tb.SelectionStart;

            if (tb.SelectionLength > 0)
            {
                var prevSelectionLength = tb.SelectionLength;

                tb.Text = tb.Text.Insert(prevSelectionStart, "(").Insert(prevSelectionStart + prevSelectionLength + 1, ")" + func);
                tb.SelectionStart = prevSelectionStart + prevSelectionLength + func.Length + 2;
            }
            else
            {
                tb.Text = tb.Text.Insert(prevSelectionStart, func);
                tb.SelectionStart = prevSelectionStart + func.Length;
            }

            tb.Focus();
        }

        private void InsertDoubleArgFunc_Click(object o, RoutedEventArgs args)
        {
            string func = ((Button)o).Tag.ToString();
            TextBox tb = GetSelectedTextBox();

            var prevSelectionStart = tb.SelectionStart;

            if (tb.SelectionLength > 0)
            {
                var prevSelectionLength = tb.SelectionLength;

                tb.Text = tb.Text.Insert(prevSelectionStart, func + "(").Insert(prevSelectionStart + prevSelectionLength + func.Length + 1, ", )");
                tb.SelectionStart = prevSelectionStart + func.Length + prevSelectionLength + 3;
            }
            else
            {
                tb.Text = tb.Text.Insert(prevSelectionStart, func + "(, )");
                tb.SelectionStart = prevSelectionStart + func.Length + 1;
            }

            tb.Focus();
        }

        private void EnterButton_Click(object o, RoutedEventArgs args)
        {
            if (!string.IsNullOrWhiteSpace(this.mathControl.mathExpressionBox.Text))
                this.mathControl.MathExpEnter();
        }

        private void tabControl_SelectionChanged(object o, SelectionChangedEventArgs args)
        {
            if (tabControl.SelectedItem == logicTab || tabControl.SelectedItem == truthTableTab)
            {
                numberToolBar.Visibility = Visibility.Collapsed;
                standartMathToolBar.Visibility = Visibility.Collapsed;
                trigonometricToolBar.Visibility = Visibility.Collapsed;
                hyperbolicToolBar.Visibility = Visibility.Collapsed;
                matrixToolBar.Visibility = Visibility.Collapsed;
                bitwiseToolBar.Visibility = Visibility.Collapsed;
                progToolBar.Visibility = Visibility.Collapsed;
                constantsMathToolBar.Visibility = Visibility.Collapsed;
                additionalMathToolBar.Visibility = Visibility.Collapsed;

                standartLogicToolBar.Visibility = Visibility.Visible;
                constantsLogicToolBar.Visibility = Visibility.Visible;
                additionalLogicToolBar.Visibility = Visibility.Visible;
            }
            else
            {
                if (tabControl.SelectedItem == mathTab)
                    matrixToolBar.Visibility = Visibility.Visible;
                else
                    matrixToolBar.Visibility = Visibility.Collapsed;

                numberToolBar.Visibility = Visibility.Visible;
                standartMathToolBar.Visibility = Visibility.Visible;
                trigonometricToolBar.Visibility = Visibility.Visible;
                hyperbolicToolBar.Visibility = Visibility.Visible;
                bitwiseToolBar.Visibility = Visibility.Visible;
                progToolBar.Visibility = Visibility.Visible;
                constantsMathToolBar.Visibility = Visibility.Visible;
                additionalMathToolBar.Visibility = Visibility.Visible;

                standartLogicToolBar.Visibility = Visibility.Collapsed;
                constantsLogicToolBar.Visibility = Visibility.Collapsed;
                additionalLogicToolBar.Visibility = Visibility.Collapsed;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string KeisokuData;
            string FileName;
            TimeSpan SousaJikan;

            if (KeisokuJissichu)
            {
                KeisokuJissichu = false;
                KeisokuEndtTime = DateTime.Now;

                SousaJikan = KeisokuEndtTime - KeisokuStartTime;

                FileName = Directory.GetCurrentDirectory();
                FileName += "\\sokuteidata";
                Directory.CreateDirectory(FileName);
                FileName += "\\";
                FileName += KeisokuStartTime.ToString("yyyyMMddHHmmss") + ".csv";

                KeisokuData = "";
                KeisokuData += "測定開始時間,";
                KeisokuData += KeisokuStartTime.ToString("HHmmss");
                KeisokuData += "\n";
                KeisokuData += "測定終了時間,";
                KeisokuData += KeisokuEndtTime.ToString("HHmmss");
                KeisokuData += "\n";
                KeisokuData += "操作時間,";
                KeisokuData += SousaJikan.TotalSeconds;
                KeisokuData += "\n";

                File.WriteAllText(FileName, KeisokuData);
            }
            else
            {
                KeisokuJissichu = true;
                KeisokuStartTime = DateTime.Now;
            }
        }
    }

}

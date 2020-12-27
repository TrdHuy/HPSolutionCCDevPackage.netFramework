﻿using HPSolutionCCDevPackage.netFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace HPSolutionCCDevPackage.netFramework
{
    public enum AnubisMessageBoxType
    {
        YesNo = 1,
        YesNoCancle = 2,
        Default = 3,
    }

    public enum AnubisMessgaeResult
    {
        ResultNon = 0,
        ResultYes = 1,
        ResultNo = 2,
        ResultCancle = 3,
        ResultOK = 4
    }

    public enum AnubisMessageImage
    {
        Non = 0,
        Error = 1,
        Hand = 2,
        Stop = 3,
        Info = 4,
        Question = 5
    }

    public class AnubisMessageBox : Window
    {

        #region TitleBarBackground
        public static readonly DependencyProperty TitleBarBackgroundProperty =
            DependencyProperty.Register("TitleBarBackground", typeof(Brush), typeof(AnubisMessageBox),
              new FrameworkPropertyMetadata(
                    defaultTitleBarBackground,
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    null));

        /// <summary>
        /// Color of the window title bar
        /// </summary>
        public Brush TitleBarBackground
        {
            get { return (Brush)GetValue(TitleBarBackgroundProperty); }
            set { SetValue(TitleBarBackgroundProperty, value); }
        }
        #endregion

        #region TitleBarHeight
        public static readonly DependencyProperty TitleBarHeightProperty =
            DependencyProperty.Register("TitleBarHeight", typeof(Double), typeof(AnubisMessageBox),
                new FrameworkPropertyMetadata(
                    defaultTitleBarHeight,
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    null));

        /// <summary>
        /// Height of the window title bar
        /// </summary>
        public double TitleBarHeight
        {
            get { return (double)GetValue(TitleBarHeightProperty); }
            set { SetValue(TitleBarHeightProperty, value); }
        }
        #endregion

        #region CustomMessageContent
        public static readonly DependencyProperty CustomMessageContentProperty =
            DependencyProperty.Register("CustomMessageContent", typeof(object), typeof(AnubisMessageBox),
                 new FrameworkPropertyMetadata(
                    defaulCustomMessageContent,
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    null));

        /// <summary>
        /// Content of the menu tab
        /// </summary>
        public object CustomMessageContent
        {
            get { return GetValue(CustomMessageContentProperty); }
            set { SetValue(CustomMessageContentProperty, value); }
        }
        #endregion

        #region CaptionContent
        public static readonly DependencyProperty CaptionContentProperty =
            DependencyProperty.Register("CaptionContent", typeof(string), typeof(AnubisMessageBox),
                 new FrameworkPropertyMetadata(
                    defaulCaptionContent,
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    null));

        /// <summary>
        /// Content of title bar
        /// </summary>
        public string CaptionContent
        {
            get { return (string)GetValue(CaptionContentProperty); }
            set { SetValue(CaptionContentProperty, value); }
        }
        #endregion

        #region YesButtonStyle
        public static readonly DependencyProperty YesButtonStyleProperty =
            DependencyProperty.Register("YesButtonStyle", typeof(Style), typeof(AnubisMessageBox),
                new FrameworkPropertyMetadata(
                    defaultYesButtonStyle,
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    null));

        /// <summary>
        /// Height of the window title bar
        /// </summary>
        public Style YesButtonStyle
        {
            get { return (Style)GetValue(YesButtonStyleProperty); }
            set { SetValue(YesButtonStyleProperty, value); }
        }
        #endregion

        #region NoButtonStyle
        public static readonly DependencyProperty NoButtonStyleProperty =
            DependencyProperty.Register("NoButtonStyle", typeof(Style), typeof(AnubisMessageBox),
                new FrameworkPropertyMetadata(
                    defaultNoButtonStyle,
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    null));

        /// <summary>
        /// Height of the window title bar
        /// </summary>
        public Style NoButtonStyle
        {
            get { return (Style)GetValue(NoButtonStyleProperty); }
            set { SetValue(NoButtonStyleProperty, value); }
        }
        #endregion

        #region OKButtonStyle
        public static readonly DependencyProperty OKButtonStyleProperty =
            DependencyProperty.Register("OKButtonStyle", typeof(Style), typeof(AnubisMessageBox),
                new FrameworkPropertyMetadata(
                    defaultOKButtonStyle,
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    null));

        /// <summary>
        /// Height of the window title bar
        /// </summary>
        public Style OKButtonStyle
        {
            get { return (Style)GetValue(OKButtonStyleProperty); }
            set { SetValue(OKButtonStyleProperty, value); }
        }
        #endregion

        #region CancleButtonStyle
        public static readonly DependencyProperty CancleButtonStyleProperty =
            DependencyProperty.Register("CancleButtonStyle", typeof(Style), typeof(AnubisMessageBox),
                new FrameworkPropertyMetadata(
                    defaultCancleButtonStyle,
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    null));

        /// <summary>
        /// Height of the window title bar
        /// </summary>
        public Style CancleButtonStyle
        {
            get { return (Style)GetValue(CancleButtonStyleProperty); }
            set { SetValue(CancleButtonStyleProperty, value); }
        }
        #endregion

        #region AnubisMesType
        public static readonly DependencyProperty AnubisMesTypeProperty =
            DependencyProperty.Register("AnubisMesType", typeof(AnubisMessageBoxType), typeof(AnubisMessageBox),
                new FrameworkPropertyMetadata(
                    defaultAnubisMesType,
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    null));

        /// <summary>
        ///Type of anubis messsage box
        /// </summary>
        public AnubisMessageBoxType AnubisMesType
        {
            get { return (AnubisMessageBoxType)GetValue(AnubisMesTypeProperty); }
            set { SetValue(AnubisMesTypeProperty, value); }
        }
        #endregion

        #region AnubisMesResult
        public static readonly DependencyProperty AnubisMesResultProperty =
            DependencyProperty.Register("AnubisMesResult", typeof(AnubisMessgaeResult), typeof(AnubisMessageBox),
                new FrameworkPropertyMetadata(
                    defaultAnubisMesResult,
                    null,
                    null));

        /// <summary>
        ///Type of anubis messsage box
        /// </summary>
        public AnubisMessgaeResult AnubisMesResult
        {
            get { return (AnubisMessgaeResult)GetValue(AnubisMesResultProperty); }
        }
        #endregion

        #region AnubisMessage
        public static readonly DependencyProperty AnubisMessageProperty =
            DependencyProperty.Register("AnubisMessage", typeof(string), typeof(AnubisMessageBox),
                new FrameworkPropertyMetadata(
                    defaultAnubisMessage,
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    new PropertyChangedCallback(AnubisMessageChangeCallBack)));

        private static void AnubisMessageChangeCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            AnubisMessageBox ctrl = d as AnubisMessageBox;
        }

        /// <summary>
        /// Content string of messsage box
        /// </summary>
        public string AnubisMessage
        {
            get { return (string)GetValue(AnubisMessageProperty); }
            set { SetValue(AnubisMessageProperty, value); }
        }
        #endregion

        #region AnubisMesIcon
        public static readonly DependencyProperty AnubisMesIconProperty =
            DependencyProperty.Register("AnubisMesIcon", typeof(AnubisMessageImage), typeof(AnubisMessageBox),
                new FrameworkPropertyMetadata(
                    defaultAnubisMesIcon,
                    null,
                    null));

        /// <summary>
        /// Icon of messsage box
        /// </summary>
        public AnubisMessageImage AnubisMesIcon
        {
            get { return (AnubisMessageImage)GetValue(AnubisMesIconProperty); }
            set { SetValue(AnubisMesIconProperty, value); }
        }
        #endregion

        private double _minWindowHeight = 150;
        private double _minWindowWidth = 300;
        private Button _closeButtonElement;
        private Button _minimizeButtonElement;
        private TextBox _defaultContentTextBoxElement;
        private Button _oKButtonElement;
        private Button _yesButtonElement;
        private Button _cancleButtonElement;
        private Button _noButtonElement;

        #region Window Button area
        private Button CloseButtonElement
        {
            get
            {
                return _closeButtonElement;
            }
            set
            {
                if (_closeButtonElement != null)
                {
                    _closeButtonElement.Click -=
                        new RoutedEventHandler(CloseButtonElement_Click);
                }

                _closeButtonElement = value;

                if (_closeButtonElement != null)
                {
                    _closeButtonElement.Click +=
                        new RoutedEventHandler(CloseButtonElement_Click);
                }
            }
        }
        private Button MinimizeButtonElement
        {
            get
            {
                return _minimizeButtonElement;
            }
            set
            {
                if (_minimizeButtonElement != null)
                {
                    _minimizeButtonElement.Click -=
                        new RoutedEventHandler(MinimizeButtonElement_Click);
                }
                _minimizeButtonElement = value;

                if (_minimizeButtonElement != null)
                {
                    _minimizeButtonElement.Click +=
                        new RoutedEventHandler(MinimizeButtonElement_Click);
                }
            }
        }
        private Button YesButtonElement
        {
            get
            {
                return _yesButtonElement;
            }
            set
            {
                if (_yesButtonElement != null)
                {
                    _yesButtonElement.Click -=
                        new RoutedEventHandler(YesButtonElement_Click);
                }

                _yesButtonElement = value;

                if (_yesButtonElement != null)
                {
                    _yesButtonElement.Click +=
                        new RoutedEventHandler(YesButtonElement_Click);
                }
            }
        }
        private Button NoButtonElement
        {
            get
            {
                return _noButtonElement;
            }
            set
            {
                if (_noButtonElement != null)
                {
                    _noButtonElement.Click -=
                        new RoutedEventHandler(NoButtonElement_Click);
                }

                _noButtonElement = value;

                if (_noButtonElement != null)
                {
                    _noButtonElement.Click +=
                        new RoutedEventHandler(NoButtonElement_Click);
                }
            }
        }
        private Button CancleButtonElement
        {
            get
            {
                return _cancleButtonElement;
            }
            set
            {
                if (_cancleButtonElement != null)
                {
                    _cancleButtonElement.Click -=
                        new RoutedEventHandler(CancleButtonElement_Click);
                }

                _cancleButtonElement = value;

                if (_cancleButtonElement != null)
                {
                    _cancleButtonElement.Click +=
                        new RoutedEventHandler(CancleButtonElement_Click);
                }
            }
        }
        private Button OKButtonElement
        {
            get
            {
                return _oKButtonElement;
            }
            set
            {
                if (_oKButtonElement != null)
                {
                    _oKButtonElement.Click -=
                        new RoutedEventHandler(OKButtonElement_Click);
                }

                _oKButtonElement = value;

                if (_oKButtonElement != null)
                {
                    _oKButtonElement.Click +=
                        new RoutedEventHandler(OKButtonElement_Click);
                }
            }
        }
        private TextBox DefaultContentTextBoxElement
        {
            get
            {
                return _defaultContentTextBoxElement;
            }
            set
            {
                _defaultContentTextBoxElement = value;
                DefaultContentBoxMeasure();
            }
        }


        private void NoButtonElement_Click(object sender, RoutedEventArgs e)
        {
            SetValue(AnubisMesResultProperty, AnubisMessgaeResult.ResultNo);
            this.Close();
        }
        private void OKButtonElement_Click(object sender, RoutedEventArgs e)
        {
            SetValue(AnubisMesResultProperty, AnubisMessgaeResult.ResultOK);
            this.Close();
        }
        private void CancleButtonElement_Click(object sender, RoutedEventArgs e)
        {
            SetValue(AnubisMesResultProperty, AnubisMessgaeResult.ResultCancle);
            this.Close();
        }
        private void YesButtonElement_Click(object sender, RoutedEventArgs e)
        {
            SetValue(AnubisMesResultProperty, AnubisMessgaeResult.ResultYes);
            this.Close();
        }
        private void MinimizeButtonElement_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void CloseButtonElement_Click(object sender, RoutedEventArgs e)
        {
            SetValue(AnubisMesResultProperty, AnubisMessgaeResult.ResultNon);
            Close();
        }
        #endregion

        private static Brush defaultTitleBarBackground = new SolidColorBrush(Color.FromArgb(40, 26, 195, 237));
        private static double defaultTitleBarHeight = 42d;
        private static object defaulCustomMessageContent = null;
        private static Style defaultYesButtonStyle = new Style { TargetType = typeof(IconButton) };
        private static Style defaultNoButtonStyle = new Style { TargetType = typeof(IconButton) };
        private static Style defaultCancleButtonStyle = new Style { TargetType = typeof(IconButton) };
        private static Style defaultOKButtonStyle = new Style { TargetType = typeof(IconButton)};
        private static AnubisMessageBoxType defaultAnubisMesType = AnubisMessageBoxType.Default;
        private static AnubisMessgaeResult defaultAnubisMesResult = AnubisMessgaeResult.ResultNon;
        private static string defaultAnubisMessage = "";
        private static string defaulCaptionContent = "Warning!!";
        private static AnubisMessageImage defaultAnubisMesIcon = AnubisMessageImage.Non;

        #region Constructor area
        public AnubisMessageBox(string message)
        {
            Instantiate(null,
                message,
                null,
                AnubisMessageBoxType.Default,
                AnubisMessageImage.Non,
                WindowStartupLocation.CenterScreen);
        }
        public AnubisMessageBox(
            string message,
            AnubisMessageBoxType anubisType
            )
        {
            Instantiate(null,
                message,
                null,
                anubisType,
                AnubisMessageImage.Non,
                WindowStartupLocation.CenterScreen);
        }
        public AnubisMessageBox(
            string message,
            AnubisMessageBoxType anubisType,
            AnubisMessageImage anubisIcon)
        {
            Instantiate(null,
                message,
                null,
                anubisType,
                anubisIcon,
                WindowStartupLocation.CenterScreen);
        }
        public AnubisMessageBox(
           string message,
           AnubisMessageBoxType anubisType,
           AnubisMessageImage anubisIcon,
           string caption)
        {
            Instantiate(null,
                message,
                null,
                anubisType,
                anubisIcon,
                WindowStartupLocation.CenterScreen,
                caption);
        }
        public AnubisMessageBox(
            Window ownerWindow,
            string message,
            AnubisMessageBoxType anubisType)
        {
            Instantiate(ownerWindow,
                message,
                null,
                anubisType,
                AnubisMessageImage.Non,
                WindowStartupLocation.CenterOwner);
        }
        public AnubisMessageBox(
            Window ownerWindow,
            string message,
            AnubisMessageBoxType anubisType,
            AnubisMessageImage anubisIcon)
        {
            Instantiate(ownerWindow,
                message,
                null,
                anubisType,
                anubisIcon,
                WindowStartupLocation.CenterOwner);
        }
        public AnubisMessageBox(
            Window ownerWindow,
            object customMessageContent,
            AnubisMessageBoxType anubisType)
        {
            Instantiate(ownerWindow,
                null,
                customMessageContent,
                anubisType,
                AnubisMessageImage.Non,
                WindowStartupLocation.CenterOwner);
        }
        public AnubisMessageBox(
            Window ownerWindow,
            object customMessageContent,
            AnubisMessageBoxType anubisType,
            AnubisMessageImage anubisIcon)
        {
            Instantiate(ownerWindow,
                null,
                customMessageContent,
                anubisType,
                anubisIcon,
                WindowStartupLocation.CenterOwner);
        }
        public AnubisMessageBox(
            Window ownerWindow,
            object customMessageContent,
            AnubisMessageBoxType anubisType,
            AnubisMessageImage anubisIcon,
            string caption)
        {
            Instantiate(ownerWindow,
                null,
                customMessageContent,
                anubisType,
                anubisIcon,
                WindowStartupLocation.CenterOwner,
                caption);
        }

        private void Instantiate(
            Window ownerWindow = null,
            string Message = "Anubis message box",
            object customMessageContent = null,
            AnubisMessageBoxType anubisType = AnubisMessageBoxType.Default,
            AnubisMessageImage anubisIcon = AnubisMessageImage.Non,
            WindowStartupLocation startupLocation = WindowStartupLocation.CenterScreen,
            string caption = "Warning!!")
        {
            DefaultStyleKey = typeof(AnubisMessageBox);
            this.WindowStyle = WindowStyle.None;
            this.ResizeMode = ResizeMode.NoResize;
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            this.MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
            this.Width = _minWindowWidth;
            this.Height = _minWindowHeight;
            this.AnubisMessage = Message;
            this.AnubisMesType = anubisType;
            this.CustomMessageContent = customMessageContent;
            this.Owner = ownerWindow;
            this.AnubisMesIcon = anubisIcon;
            this.WindowStartupLocation = startupLocation;
            this.CaptionContent = caption;
        }
        #endregion


        //Measure size for content text box
        private void DefaultContentBoxMeasure()
        {
            _defaultContentTextBoxElement.Text = AnubisMessage;
            _defaultContentTextBoxElement.Measure(new Size(this.MaxWidth, this.MaxHeight));

            double textboxratiowh = 2d; // width : height = 2
            double textboxwidth = _defaultContentTextBoxElement.DesiredSize.Width;
            double textboxheight = _defaultContentTextBoxElement.DesiredSize.Height;
            double textboxarea = textboxheight * textboxwidth;

            double newtexboxheight = Math.Sqrt(textboxarea / textboxratiowh);
            double newtexboxwidth = newtexboxheight * textboxratiowh;

            newtexboxwidth = newtexboxwidth <= _minWindowWidth ? _minWindowWidth : newtexboxwidth;
            newtexboxheight = newtexboxheight <= _minWindowHeight ? _minWindowHeight : newtexboxheight;

            _defaultContentTextBoxElement.Width = newtexboxwidth;
            _defaultContentTextBoxElement.Height = newtexboxheight;

            MeasureWindowSizeAccordToTextBoxSize(newtexboxwidth, newtexboxheight);
        }

        private void MeasureWindowSizeAccordToTextBoxSize(double newTexboxWidth, double newTexboxHeight)
        {
            double windowAndTextBoxRatio = 1.3d;

            double oldWidth = this.Width;
            double oldHeight = this.Height;

            // Recaculate window size
            this.Width = newTexboxWidth * windowAndTextBoxRatio;
            this.Height = newTexboxHeight * windowAndTextBoxRatio + TitleBarHeight;

            //update window location
            this.Left = this.Left - (this.Width - oldWidth) / 2;
            this.Top = this.Top - (this.Height - oldHeight) / 2;
        }

        public override void OnApplyTemplate()
        {
            MinimizeButtonElement = GetTemplateChild("MinimizeButton") as Button;
            CloseButtonElement = GetTemplateChild("CloseButton") as Button;
            DefaultContentTextBoxElement = GetTemplateChild("DefaultContentTextBox") as TextBox;

            if (AnubisMesType == AnubisMessageBoxType.Default)
            {
                OKButtonElement = GetTemplateChild("OKButton") as Button;
            }
            else if (AnubisMesType == AnubisMessageBoxType.YesNo)
            {
                YesButtonElement = GetTemplateChild("YesButton") as Button;
                NoButtonElement = GetTemplateChild("NoButton") as Button;
            }
            else if (AnubisMesType == AnubisMessageBoxType.YesNoCancle)
            {
                CancleButtonElement = GetTemplateChild("CancleButton") as Button;
                YesButtonElement = GetTemplateChild("YesButton") as Button;
                NoButtonElement = GetTemplateChild("NoButton") as Button;
            }
        }

        protected override void OnChildDesiredSizeChanged(UIElement child)
        {
            base.OnChildDesiredSizeChanged(child);
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
        }


        public new AnubisMessgaeResult Show()
        {
            base.ShowDialog();

            return AnubisMessgaeResult.ResultNon;
        }

        /// <summary>
        /// Recomend to use Show() method instead
        /// </summary>
        public new AnubisMessgaeResult ShowDialog()
        {
            return this.Show();
        }
    }
}

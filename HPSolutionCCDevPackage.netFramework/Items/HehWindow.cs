﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;

namespace HPSolutionCCDevPackage.netFramework
{
    public enum HehWindowContentType
    {
        PageType = 0,
        NormalContentType = 1
    }
    public class HehWindow : Window
    {
        public HehWindow()
        {
            DefaultStyleKey = typeof(HehWindow);
            this.WindowStyle = WindowStyle.None;
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        #region Public properties

        #region TitleBarBackground

        public static readonly DependencyProperty TitleBarBackgroundProperty =
            DependencyProperty.Register("TitleBarBackground", typeof(Brush), typeof(HehWindow),
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
            DependencyProperty.Register("TitleBarHeight", typeof(Double), typeof(HehWindow),
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

        #region  IsEnableMenuTab
        public static readonly DependencyProperty IsEnableMenuTabProperty =
           DependencyProperty.Register("IsEnableMenuTab", typeof(bool), typeof(HehWindow),
                new FrameworkPropertyMetadata(
                    defaultEnableMenuTab,
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    null));

        /// <summary>
        /// Enable the menu tab layout of the window dashboard
        /// </summary>
        public bool IsEnableMenuTab
        {
            get { return (bool)GetValue(IsEnableMenuTabProperty); }
            set { SetValue(IsEnableMenuTabProperty, value); }
        }
        #endregion

        #region MenuTabWidth

        public static readonly DependencyProperty MenuTabWidthProperty =
           DependencyProperty.Register("MenuTabWidth", typeof(double), typeof(HehWindow),
             new FrameworkPropertyMetadata(
                    defaultMenuTabWidth,
                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    null));

        /// <summary>
        /// Width of the menu tab docked on dashboard window
        /// </summary>
        public double MenuTabWidth
        {
            get { return (double)GetValue(MenuTabWidthProperty); }
            set { SetValue(MenuTabWidthProperty, value); }
        }
        #endregion

        #region MenuTabExpandedWidth

        public static readonly DependencyProperty MenuTabExpandedWidthProperty =
            DependencyProperty.Register("MenuTabExpandedWidth", typeof(double), typeof(HehWindow),
              new FrameworkPropertyMetadata(
                    defaultMenuTabExpandedWidth,
                    FrameworkPropertyMetadataOptions.AffectsMeasure |
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    null));

        /// <summary>
        /// Width of the menu tab when expanding on the window 
        /// </summary>
        public double MenuTabExpandedWidth
        {
            get { return (double)GetValue(MenuTabExpandedWidthProperty); }
            set { SetValue(MenuTabExpandedWidthProperty, value); }
        }
        #endregion

        #region MenuTabContent
        public static readonly DependencyProperty MenuTabContentProperty =
            DependencyProperty.Register("MenuTabContent", typeof(object), typeof(HehWindow),
                 new FrameworkPropertyMetadata(
                    defaultMenuTabContent,
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    null));

        /// <summary>
        /// Content of the menu tab
        /// </summary>
        public object MenuTabContent
        {
            get { return GetValue(MenuTabContentProperty); }
            set { SetValue(MenuTabContentProperty, value); }
        }
        #endregion

        #region PageSource
        public static readonly DependencyProperty PageSourceProperty =
            DependencyProperty.Register("PageSource", typeof(Uri), typeof(HehWindow),
              new PropertyMetadata(
                  defaultPageSource,
                  new PropertyChangedCallback(PageSourceChangeCallBack)));

        /// <summary>
        /// Source of the window when using page type display
        /// </summary>
        public Uri PageSource
        {
            get { return (Uri)GetValue(PageSourceProperty); }
            set { SetValue(PageSourceProperty, value); }
        }

        public static readonly RoutedEvent PageSourceChangedEvent =
            EventManager.RegisterRoutedEvent("PageSourceChanged", RoutingStrategy.Direct,
                          typeof(PageSourceEventHandler), typeof(HehWindow));

        public event PageSourceEventHandler PageSourceChanged
        {
            add { AddHandler(PageSourceChangedEvent, value); }
            remove { RemoveHandler(PageSourceChangedEvent, value); }
        }

        #endregion

        #region WaitingIconSource
        public static readonly DependencyProperty WaitingIconSourceProperty =
        DependencyProperty.Register(
                "WaitingIconSource",
                typeof(ImageSource),
                typeof(HehWindow),
                new FrameworkPropertyMetadata(
                        default(ImageSource),
                        FrameworkPropertyMetadataOptions.AffectsMeasure |
                        FrameworkPropertyMetadataOptions.AffectsRender,
                        null,
                        null),
                null);

        public ImageSource WaitingIconSource
        {
            get { return (ImageSource)GetValue(WaitingIconSourceProperty); }
            set { SetValue(WaitingIconSourceProperty, value); }
        }
        #endregion

        #region WindowShownEvent
        public static readonly RoutedEvent WindowShownEvent =
            EventManager.RegisterRoutedEvent("WindowShown", RoutingStrategy.Direct,
                          typeof(WindowShownEventHandler), typeof(HehWindow));

        public event WindowShownEventHandler WindowShown
        {
            add { AddHandler(WindowShownEvent, value); }
            remove { RemoveHandler(WindowShownEvent, value); }
        }

        #endregion

        #region ContentType
        public static readonly DependencyProperty ContentTypeProperty =
            DependencyProperty.Register("ContentType", typeof(HehWindowContentType), typeof(HehWindow),
                 new FrameworkPropertyMetadata(
                    defaultContentType,
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    null));

        /// <summary>
        /// Content type of window
        /// </summary>
        public HehWindowContentType ContentType
        {
            get { return (HehWindowContentType)GetValue(ContentTypeProperty); }
            set { SetValue(ContentTypeProperty, value); }
        }
        #endregion

        #region CloseWindowCommand
        public static readonly DependencyProperty CloseWindowCommandProperty =
            DependencyProperty.Register("CloseWindowCommand", typeof(ICommand), typeof(HehWindow),
                  null);

        /// <summary>
        /// Command of button close window
        /// </summary>
        public ICommand CloseWindowCommand
        {
            get { return (ICommand)GetValue(CloseWindowCommandProperty); }
            set { SetValue(CloseWindowCommandProperty, value); }
        }
        #endregion

        #region IsPageLoading
        public static readonly DependencyProperty IsPageLoadingProperty =
           DependencyProperty.Register("IsPageLoading", typeof(bool), typeof(HehWindow),
                new FrameworkPropertyMetadata(
                    defaultIsPageLoading,
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    null));

        /// <summary>
        /// Return true when page is loading
        /// </summary>
        public bool IsPageLoading
        {
            get { return (bool)GetValue(IsPageLoadingProperty); }
            set { SetValue(IsPageLoadingProperty, value); }
        }
        #endregion

        #region IsPageLoaded
        public static readonly DependencyProperty IsPageLoadedProperty =
           DependencyProperty.Register("IsPageLoaded", typeof(bool), typeof(HehWindow),
                new FrameworkPropertyMetadata(
                    defaultIsPageLoaded,
                    FrameworkPropertyMetadataOptions.AffectsRender,
                    null));

        /// <summary>
        /// Return true when page is loading
        /// </summary>
        public bool IsPageLoaded
        {
            get { return (bool)GetValue(IsPageLoadedProperty); }
            set { SetValue(IsPageLoadedProperty, value); }
        }
        #endregion

        #region PageLoadingDelayTime

        public static readonly DependencyProperty PageLoadingDelayTimeProperty =
           DependencyProperty.Register("PageLoadingDelayTime", typeof(long), typeof(HehWindow),
             new PropertyMetadata(defaultPageLoadingDelayTime));

        /// <summary>
        /// Delay a period of specifictime when navigate to a new source
        /// </summary>
        public long PageLoadingDelayTime
        {
            get { return (long)GetValue(PageLoadingDelayTimeProperty); }
            set { SetValue(PageLoadingDelayTimeProperty, value); }
        }
        #endregion

        #region PreviewPageNavigateEvent
        public static readonly RoutedEvent PreviewPageNavigateEvent =
            EventManager.RegisterRoutedEvent("PreviewPageNavigate", RoutingStrategy.Direct,
                          typeof(PreviewPageNavigateHandler), typeof(HehWindow));

        public event PreviewPageNavigateHandler PreviewPageNavigate
        {
            add { AddHandler(PreviewPageNavigateEvent, value); }
            remove { RemoveHandler(PreviewPageNavigateEvent, value); }
        }

        #endregion

        #endregion

        private static Brush defaultTitleBarBackground = new SolidColorBrush(Color.FromArgb(40, 26, 195, 237));
        private static double defaultTitleBarHeight = 42d;
        private static bool defaultEnableMenuTab = default(bool);
        private static bool defaultIsPageLoading = default(bool);
        private static bool defaultIsPageLoaded = default(bool);
        private static double defaultMenuTabWidth = 0.0d;
        private static double defaultMenuTabExpandedWidth = 0.0d;
        private static object defaultMenuTabContent = default(object);
        private static Uri defaultPageSource = default(Uri);
        private static HehWindowContentType defaultContentType = HehWindowContentType.PageType;
        private static long defaultPageLoadingDelayTime = 0;

        private Frame _dWPageHostFrameElement;
        private Button _closeButtonElement;
        private Button _maximizeButtonElement;
        private Button _minimizeButtonElement;
        private Button _previousNavigationButtonElement;
        private Button _nextNavigationButtonElement;

        #region Page host field
        private Frame DWPageHostFrameElement
        {
            get { return _dWPageHostFrameElement; }
            set
            {
                if (_dWPageHostFrameElement != null)
                {
                    _dWPageHostFrameElement.Navigating -=
                        new NavigatingCancelEventHandler(OnPageNavigating);
                    _dWPageHostFrameElement.Navigated -=
                        new NavigatedEventHandler(OnPageNavigated);
                }
                _dWPageHostFrameElement = value;
                if (_dWPageHostFrameElement != null)
                {
                    _dWPageHostFrameElement.Navigating +=
                        new NavigatingCancelEventHandler(OnPageNavigating);
                    _dWPageHostFrameElement.Navigated +=
                        new NavigatedEventHandler(OnPageNavigated);
                }
            }
        }
        private Stopwatch _pageLoadingWatacher;
        private SemaphoreSlim _pageLoadLocker = new SemaphoreSlim(1, 1);

        private async void OnPageNavigated(object sender, NavigationEventArgs e)
        {
            // Lock
            await _pageLoadLocker.WaitAsync();

            //Handle
            try
            {
                if (_pageLoadingWatacher != null)
                {
                    _pageLoadingWatacher.Stop();
                    long restLoadingTime = PageLoadingDelayTime - _pageLoadingWatacher.ElapsedMilliseconds;

                    if (restLoadingTime >= 0)
                    {
                        await Task.Delay(Convert.ToInt32(restLoadingTime));
                    }
                }

                _pageLoadingWatacher = null;
                IsPageLoading = false;
                IsPageLoaded = true;
            }
            finally
            {
                // Release
                _pageLoadLocker.Release();
            }

        }

        private async void OnPageNavigating(object sender, NavigatingCancelEventArgs e)
        {
            _pageLoadingWatacher = Stopwatch.StartNew();
            // Lock 
            await _pageLoadLocker.WaitAsync();

            // Handle 
            try
            {
                IsPageLoading = true;
                IsPageLoaded = false;
            }
            finally
            {
                // Release
                _pageLoadLocker.Release();
            }

        }
        #endregion

        #region Window button field
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

                if (CloseWindowCommand != null && _closeButtonElement != null)
                {
                    _closeButtonElement.Command = CloseWindowCommand;
                }
                else if (_closeButtonElement != null)
                {
                    _closeButtonElement.Click +=
                        new RoutedEventHandler(CloseButtonElement_Click);
                }
            }
        }
        private Button MaximizeButtonElement
        {
            get
            {
                return _maximizeButtonElement;
            }
            set
            {
                if (_maximizeButtonElement != null)
                {
                    _maximizeButtonElement.Click -=
                        new RoutedEventHandler(MaximizeButtonElement_Click);
                }
                _maximizeButtonElement = value;

                if (_maximizeButtonElement != null)
                {
                    _maximizeButtonElement.Click +=
                        new RoutedEventHandler(MaximizeButtonElement_Click);
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
        private Button PreviousNavigationButtonElement
        {
            get
            {
                return _previousNavigationButtonElement;
            }
            set
            {
                if (_previousNavigationButtonElement != null)
                {
                    _previousNavigationButtonElement.Click -=
                        new RoutedEventHandler(PrevisousNavigationButtonElement_Click);
                }
                _previousNavigationButtonElement = value;

                if (_previousNavigationButtonElement != null)
                {
                    _previousNavigationButtonElement.Click +=
                        new RoutedEventHandler(PrevisousNavigationButtonElement_Click);
                }
            }
        }
        private Button NextNavigationButtonElement
        {
            get
            {
                return _nextNavigationButtonElement;
            }
            set
            {
                if (_nextNavigationButtonElement != null)
                {
                    _nextNavigationButtonElement.Click -=
                        new RoutedEventHandler(NextNavigationButtonElement_Click);
                }
                _nextNavigationButtonElement = value;

                if (_nextNavigationButtonElement != null)
                {
                    _nextNavigationButtonElement.Click +=
                        new RoutedEventHandler(NextNavigationButtonElement_Click);
                }
            }
        }

        private void PrevisousNavigationButtonElement_Click(object sender, RoutedEventArgs e)
        {
            if (DWPageHostFrameElement != null)
            {
                if (DWPageHostFrameElement.NavigationService.CanGoBack)
                {
                    var ex = new PreviewPageNavigateArgs(HehWindow.PreviewPageNavigateEvent);
                    OnPreviewPageNavigate(ex);
                    if (!ex.Handled)
                    {
                        DWPageHostFrameElement.NavigationService.GoBack();
                    }
                }
            }
        }

        protected virtual void OnPreviewPageNavigate(PreviewPageNavigateArgs previewPageNavigateArgs)
        {
            RaiseEvent(previewPageNavigateArgs);
        }

        private void NextNavigationButtonElement_Click(object sender, RoutedEventArgs e)
        {
            if (DWPageHostFrameElement != null)
            {
                if (DWPageHostFrameElement.NavigationService.CanGoForward)
                {
                    var ex = new PreviewPageNavigateArgs(HehWindow.PreviewPageNavigateEvent);
                    OnPreviewPageNavigate(ex);
                    if (!ex.Handled)
                    {
                        DWPageHostFrameElement.NavigationService.GoForward();
                    }
                }
            }
        }
        private void MinimizeButtonElement_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void CloseButtonElement_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void MaximizeButtonElement_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
            }
            else if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }
        }
        #endregion

        private static void PageSourceChangeCallBack(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            HehWindow ctl = (HehWindow)obj;
            Uri newValue = (Uri)args.NewValue;

            // Call OnValueChanged to raise the ValueChanged event.
            ctl.OnPageSourceChanged(
                new PageSourceEventArgs(HehWindow.PageSourceChangedEvent,
                    newValue));
        }

        protected virtual void OnPageSourceChanged(PageSourceEventArgs e)
        {
            RaiseEvent(e);
        }

        protected virtual void OnWindowShown(WindowShownEventArgs e)
        {
            RaiseEvent(e);
        }

        public override void OnApplyTemplate()
        {
            if (ContentType == HehWindowContentType.PageType)
            {
                PreviousNavigationButtonElement = GetTemplateChild("PreviousNavigateButton") as Button;
                NextNavigationButtonElement = GetTemplateChild("NextNavigateButton") as Button;

            }
            DWPageHostFrameElement = GetTemplateChild("DWPageHostFrame") as Frame;
            MaximizeButtonElement = GetTemplateChild("MaximizeButton") as Button;
            MinimizeButtonElement = GetTemplateChild("MinimizeButton") as Button;
            CloseButtonElement = GetTemplateChild("CloseButton") as Button;

        }

        public void Navigate(Uri destinationPage)
        {
            if (DWPageHostFrameElement != null)
            {
                var ex = new PreviewPageNavigateArgs(HehWindow.PreviewPageNavigateEvent);
                OnPreviewPageNavigate(ex);
                if (!ex.Handled)
                {
                    DWPageHostFrameElement.NavigationService.Navigate(destinationPage);
                }
            }
        }

        public void Navigate(object content)
        {
            if (DWPageHostFrameElement != null)
            {
                var ex = new PreviewPageNavigateArgs(HehWindow.PreviewPageNavigateEvent);
                OnPreviewPageNavigate(ex);
                if (!ex.Handled)
                {
                    DWPageHostFrameElement.NavigationService.Navigate(content);
                }
            }
        }

        public new void Close()
        {
            if (CloseWindowCommand != null)
            {
                CloseWindowCommand?.Execute(this);
            }
            else
            {
                base.Close();
            }
        }

        public new void Show()
        {
            base.Show();
            OnWindowShown(new WindowShownEventArgs(WindowShownEvent));
        }

        public void ForceClose()
        {
            base.Close();
        }
    }

    public delegate void PageSourceEventHandler(object sender, PageSourceEventArgs e);
    public delegate void WindowShownEventHandler(object sender, WindowShownEventArgs e);
    public delegate void PreviewPageNavigateHandler(object sender, PreviewPageNavigateArgs e);

    public class PageSourceEventArgs : RoutedEventArgs
    {
        private Uri _value;

        public PageSourceEventArgs(RoutedEvent id, Uri val)
        {
            _value = val;
            RoutedEvent = id;
        }

        public Uri Value
        {
            get { return _value; }
        }
    }

    public class WindowShownEventArgs : RoutedEventArgs
    {
        public WindowShownEventArgs(RoutedEvent id)
        {
            RoutedEvent = id;
        }
    }

    public class PreviewPageNavigateArgs : RoutedEventArgs
    {
        public bool Handled { get; set; } = false;

        public PreviewPageNavigateArgs(RoutedEvent id)
        {
            RoutedEvent = id;
            Handled = false;
        }
    }

}

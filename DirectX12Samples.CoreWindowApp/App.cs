﻿using System;
using DirectX12Samples.Engine;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;

#nullable enable

namespace DirectX12Samples.CoreWindowApp
{
    public sealed class App : IFrameworkViewSource, IFrameworkView
    {
        private Game? game;
        private GameContext? gameContext;

        public IFrameworkView CreateView() => this;

        public void Initialize(CoreApplicationView applicationView)
        {
            applicationView.Activated += ApplicationView_Activated;
            CoreApplication.Suspending += CoreApplication_Suspending;
        }

        private void ApplicationView_Activated(CoreApplicationView sender, IActivatedEventArgs args)
        {
            gameContext = new GameContextCoreWindow();

            sender.CoreWindow.Activate();
        }

        public void SetWindow(CoreWindow window)
        {
            ExtendViewIntoTitleBar(true);
        }

        public void Load(string entryPoint)
        {
        }

        public void Run()
        {
            if (gameContext is null) throw new InvalidOperationException();

            game = new Game(gameContext);
            game.Run();
        }

        public void Uninitialize()
        {
        }

        private void CoreApplication_Suspending(object sender, SuspendingEventArgs e)
        {
            SuspendingDeferral deferral = e.SuspendingOperation.GetDeferral();
            //game.Dispose();
            deferral.Complete();
        }

        private static void ExtendViewIntoTitleBar(bool extendViewIntoTitleBar)
        {
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = extendViewIntoTitleBar;

            if (extendViewIntoTitleBar)
            {
                ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
                titleBar.ButtonBackgroundColor = Colors.Transparent;
                titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
            }
        }
    }

    public class Program
    {
        private static void Main()
        {
            CoreApplication.Run(new App());
        }
    }
}

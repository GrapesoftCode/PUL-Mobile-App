﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PUL.GS.App.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(false)]
    public partial class PulerPage : ContentPage
    {
        bool selection1Active = true;
        bool selection2Active = false;
        private double valueX, valueY;
        private bool IsTurnX, IsTurnY;

        public PulerPage()
        {
            InitializeComponent();
        }

        private void PanGestureRecognizer_PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            var x = e.TotalX; // TotalX Left/Right
            var y = e.TotalY; // TotalY Up/Down

            // StatusType
            switch (e.StatusType)
            {
                case GestureStatus.Started:
                    Debug.WriteLine("Started");
                    break;
                case GestureStatus.Running:
                    Debug.WriteLine("Running");

                    // Check that the movement is x or y
                    if ((x >= 5 || x <= -5) && !IsTurnX && !IsTurnY)
                    {
                        IsTurnX = true;
                    }

                    if ((y >= 5 || y <= -5) && !IsTurnY && !IsTurnX)
                    {
                        IsTurnY = true;
                    }

                    // X (Horizontal)
                    if (IsTurnX && !IsTurnY)
                    {
                        if (x <= valueX)
                        {
                            // Left
                            if (selection2Active)
                            {
                                //make selection1 move to selection 2

                                selection1Active = true;
                                selection2Active = false;
                                text2.TextColor = Color.Black;
                                text1.TextColor = Color.White;
                                runningFrame.TranslateTo(runningFrame.X, 0, 120);
                                var duration = TimeSpan.FromSeconds(1);
                                Vibration.Vibrate(duration);

                            }
                        }

                        if (x >= valueX)
                        {
                            // Right
                            if (selection1Active)
                            {
                                //make selection2 move to selection 1
                                selection1Active = false;
                                selection2Active = true;
                                text2.TextColor = Color.White;
                                text1.TextColor = Color.Black;
                                runningFrame.TranslateTo(runningFrame.X + 100, 0, 120);
                                var duration = TimeSpan.FromSeconds(1);
                                Vibration.Vibrate(duration);

                            }
                        }
                    }


                    break;
                case GestureStatus.Completed:
                    Debug.WriteLine("Completed");

                    valueX = x;
                    valueY = y;

                    IsTurnX = false;
                    IsTurnY = false;

                    break;
                case GestureStatus.Canceled:
                    Debug.WriteLine("Canceled");
                    break;
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (selection2Active)
            {
                //make selection1 move to selection 2

                selection1Active = true;
                selection2Active = false;
                text2.TextColor = Color.Red;
                text1.TextColor = Color.White;
                runningFrame.TranslateTo(runningFrame.X, 0, 120);
                var duration = TimeSpan.FromSeconds(1);
                Vibration.Vibrate(duration);
            }
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            if (selection1Active)
            {
                //make selection2 move to selection 1
                selection1Active = false;
                selection2Active = true;
                text2.TextColor = Color.White;
                text1.TextColor = Color.Red;
                runningFrame.TranslateTo(runningFrame.X + 100, 0, 120);
                var duration = TimeSpan.FromSeconds(1);
                Vibration.Vibrate(duration);
            }
        }
    }
}
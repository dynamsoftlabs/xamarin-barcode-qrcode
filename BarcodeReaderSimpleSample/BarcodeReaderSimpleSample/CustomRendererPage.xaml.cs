﻿using System;
using System.Collections.Generic;

using Xamarin.Forms;

using DCVXamarin;

namespace BarcodeReaderSimpleSample
{
    public partial class CustomRendererPage : ContentPage, IBarcodeResultListener
    {
        public CustomRendererPage()
        {
            InitializeComponent();
            label.Text = "null";
            App.dbr.AddResultListener(this);
            App.dbr.UpdateRuntimeSettings(EnumDBRPresetTemplate.VIDEO_SINGLE_BARCODE);
            App.dbr.SetCameraEnhancer(App.dce);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            App.dbr.StartScanning();
            App.dce.Open();        
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            App.dbr.StopScanning();
            App.dce.Close();           
        }

        public void BarcodeResultCallback(int frameID, BarcodeResult[] textResults)
        {
            if (textResults != null && textResults.Length > 0)
            {
                if (textResults[0].BarcodeText != null && label != null)
                {
                    Device.BeginInvokeOnMainThread(() => {
                        label.Text = textResults[0].BarcodeText;
                    }
                        );
                }

            }
        }
    }
}

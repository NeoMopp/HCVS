﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Kinect;

namespace KinectController
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        KinectSensor sensor;
        const int SKELETON_COUNT = 6;
        Skeleton[] allSkeletons = new Skeleton[SKELETON_COUNT];

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (KinectSensor.KinectSensors.Count > 0)
                sensor = KinectSensor.KinectSensors[0];
            

            if (sensor.Status == KinectStatus.Connected)
            {
                sensor.ColorStream.Enable();
                sensor.DepthStream.Enable();
                sensor.SkeletonStream.Enable();
                //for windows kinect only
                //sensor.DepthStream.Range = DepthRange.Near;
                //sensor.SkeletonStream.EnableTrackingInNearRange = true;
                //sensor.SkeletonStream.TrackingMode = SkeletonTrackingMode.Seated;

                sensor.AllFramesReady += new EventHandler<AllFramesReadyEventArgs>(sensor_AllFramesReady);
                sensor.Start();
            }
        }

        void sensor_AllFramesReady(object sender, AllFramesReadyEventArgs e)
        {
            //throw new NotImplementedException();
            using (ColorImageFrame colourFrame = e.OpenColorImageFrame())
            {
                if (colourFrame == null)
                    return;

                byte[] pixels = new byte[colourFrame.PixelDataLength];
                colourFrame.CopyPixelDataTo(pixels);
                int stride = colourFrame.Width * 4;
                Vid.Source = BitmapSource.Create(colourFrame.Width, colourFrame.Height, 96, 96, PixelFormats.Bgr32, null, pixels, stride);
            }
            Skeleton me = null;
            getSkeleton(e, ref me);
            if (me == null)
                return;

            GetCameraPoint(me, e);
        }

        private void getSkeleton(AllFramesReadyEventArgs e, ref Skeleton me)
        {
            using (SkeletonFrame skeletonFrameData = e.OpenSkeletonFrame())
            {
                if (skeletonFrameData == null)
                    return;

                skeletonFrameData.CopySkeletonDataTo(allSkeletons);
                me = (from s in allSkeletons where s.TrackingState == SkeletonTrackingState.Tracked select s).FirstOrDefault();
            }
        }

        private void GetCameraPoint(Skeleton me, AllFramesReadyEventArgs e)
        {
            outputBox.Text = null;
            using (DepthImageFrame depth = e.OpenDepthImageFrame())
            {
                if (depth == null || sensor == null)
                    return;

                DepthImagePoint handLeftDepthPoint =   depth.MapFromSkeletonPoint(me.Joints[JointType.HandLeft].Position);
                ColorImagePoint handLeftColourPoint = depth.MapToColorImagePoint(handLeftDepthPoint.X, handLeftDepthPoint.Y, ColorImageFormat.RgbResolution640x480Fps30);

                DepthImagePoint handRightDepthPoint = depth.MapFromSkeletonPoint(me.Joints[JointType.HandRight].Position);
                ColorImagePoint handRightColourPoint = depth.MapToColorImagePoint(handRightDepthPoint.X, handRightDepthPoint.Y, ColorImageFormat.RgbResolution640x480Fps30);
                

                Canvas.SetLeft(face, handLeftColourPoint.X - face.Width / 2);
                Canvas.SetTop(face, handLeftColourPoint.Y - face.Height / 2);

                Canvas.SetLeft(face2, handRightColourPoint.X - face2.Width / 2);
                Canvas.SetTop(face2, handRightColourPoint.Y - face.Height / 2);


                outputBox.Text += "Left Hand X: " + handLeftColourPoint.X +" Left Hand Y: "+ handLeftColourPoint.Y +"\nRight Hand X: "+ handRightColourPoint.X +" Right Hand Y: "+ handRightColourPoint.Y;

                  
            }
        }

        //private int forceGenerated(int start, int end, int mass, int frames)
        //{
                //int force = mass*()
                //return force
        //}

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            sensor.Stop();
        }
    }
}

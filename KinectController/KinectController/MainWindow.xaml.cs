using System;
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
        //int previousX=0, previousY =0;
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
            //int diffX , diffY;
            using (DepthImageFrame depth = e.OpenDepthImageFrame())
            {
                if (depth == null || sensor == null)
                    return;

                DepthImagePoint headDepthPoint = depth.MapFromSkeletonPoint(me.Joints[JointType.HandLeft].Position);
                ColorImagePoint headColourPoint = depth.MapToColorImagePoint(headDepthPoint.X, headDepthPoint.Y, ColorImageFormat.RgbResolution640x480Fps30);
                //if (previousX == 0 && previousY == 0)
                //{
                //    previousX = headDepthPoint.X;
                //    previousY = headDepthPoint.Y;
                //}
                //else
                //{
                //    diffX = headDepthPoint.X - previousX;
                //    diffY = headDepthPoint.Y-  previousY;
                //    previousX = headDepthPoint.X;
                //    previousY = headDepthPoint.Y;
                //    Canvas.SetLeft(face2, previousX - face.Width / 2);
                //    Canvas.SetTop(face2, previousY - face.Height / 2);
                   
                //}

                Canvas.SetLeft(face, headColourPoint.X - face.Width / 2);
                Canvas.SetTop(face, headColourPoint.Y - face.Height / 2);            

            }
        }

        //private void DistanceTravelled()
        //{

        //}

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            sensor.Stop();
        }
    }
}

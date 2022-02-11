using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace AndroidAntipodalApp
{
    internal class MainPageViewModel : INotifyPropertyChanged
    {
        private double dms_Degrees;
        private double deg_Radians;
        private double radians_Deg;
        private string deg_DMS;
        private double greatCircle_Calulation;
        private string viewableMileage_AtHeight;
        private double[] get_AntiPodal;
                
        public double DMS_Degrees
        {
            get => dms_Degrees;
            set
            {
                DMS_Degrees = value;
                OnPropertyChanged();
            }
        }
        public double Deg_Radians
        { 
            get => deg_Radians;
            set
            {
                deg_Radians = value;
                OnPropertyChanged();
            }
        }
        public double Radians_Deg
        { 
            get => radians_Deg;
            set
            {
                radians_Deg = value;
                OnPropertyChanged();
            }  
        }
        public string Deg_DMS
        {
            get => deg_DMS;
            set
            {
                deg_DMS = value;
                OnPropertyChanged();
            }
        }
        public double GreatCircle_Calulation
        { 
            get => greatCircle_Calulation;
            set
            {
                greatCircle_Calulation = value;
                OnPropertyChanged();
            }
            
        }
        public string ViewableMileage_AtHeight
        {
            get => viewableMileage_AtHeight; 
            set
            {
                viewableMileage_AtHeight = value;
                OnPropertyChanged();
            }
            
        }
        public double[] Get_AntiPodal 
        { 
            get => get_AntiPodal; 
            set
            {
                get_AntiPodal = value;
                OnPropertyChanged();
            }
            
        }

        public double Convert_DMS_Degrees(double deg, double min, double sec)
        {
            double Min = min / 60;
            double Sec = sec / 3600;
            double Deg = deg * Math.Sign(deg);
            double Degrees = Deg + Min + Sec;
            double Negation = Math.Sign(deg);
            return (Degrees * Negation);
        }

        public double Convert_Deg_Radians(double deg)
        {
            double Deg = deg * Math.Sign(deg);
            double answer = (Math.PI / 180) * Deg;
            double Negation = Math.Sign(deg);
            return (answer * Negation);
        }

        public double Convert_Radians_Deg(double rad)
        {
            double Rad = rad * Math.Sign(rad);
            double answer = (180 / Math.PI) * Rad;
            double Negation = Math.Sign(rad);
            return (answer * Negation);
        }

        public string Convert_Deg_DMS(double deg)
        {
            double Negation = Math.Sign(deg);
            double Deg = deg * Negation;
            double DecimalPart = (Deg - Math.Truncate(Deg));
            double IntegerPart = Math.Truncate(Deg);
            double Min = DecimalPart * 60;
            double MinDecimalPart = Min - Math.Truncate(Min);
            double MinIntegerPart = Math.Truncate(Min);
            double Sec = Math.Round(MinDecimalPart * 60, 0);
            double IntegerWithSign = Math.Sign(deg) * IntegerPart;

            string result = $"{IntegerWithSign}° {MinIntegerPart}' {Sec}\"";

            return result;
        }

        public double Convert_GreatCircle_Calulation(double LatDeg1, double LongDeg1, double LatDeg2, double LongDeg2)
        {
            LatDeg1 = Convert_Deg_Radians(LatDeg1);
            LongDeg1 = Convert_Deg_Radians(LongDeg1);
            LatDeg2 = Convert_Deg_Radians(LatDeg2);
            LongDeg2 = Convert_Deg_Radians(LongDeg2);

            double result = (Math.Acos(Math.Sin(LatDeg1) * Math.Sin(LatDeg2) + Math.Cos(LatDeg1) * Math.Cos(LatDeg2) * Math.Cos(LongDeg1 - LongDeg2)) * 3959);
            result = Math.Round(result, 1);
            return result;
        }

        public void Convert_ViewableMileage_AtHeight()
        {
            Console.WriteLine("Height above earth in feet");

            double height = double.Parse(Console.ReadLine());
            height = (height / 5280) + 3959;
            double angle = (Math.Asin((3959 / height)) * (180 / Math.PI));
            angle = Math.Round(angle * 2, 1);
            double answer = Math.Round(180 - angle, 1);
            double visible = Math.PI * 2 * answer;
            Console.WriteLine($"Viewing {answer} degrees of 360 in two directions");
            double EarthCircumference = Math.PI * 3959 * 2;
            double arch = answer / 360;
            answer = arch * EarthCircumference;
            answer = Math.Round(answer / 2, 0);
            Console.WriteLine($"visible part {answer} miles in one direction.");
            Console.ReadKey();
        }

        public double[] Convert_Get_AntiPodal(double latdeg, double latmin, double latsec, double longdeg, double longmin, double longsec)
        {
            double Lat = Convert_DMS_Degrees(latdeg, latmin, latsec);
            double Long = Convert_DMS_Degrees(longdeg, longmin, longsec);
            Lat = -Lat;
            Long = Long + 180;
            return new double[2] { Lat, Long };
        }

        #region Inotifyproperychanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }


}

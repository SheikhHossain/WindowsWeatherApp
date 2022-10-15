﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net;

namespace Weather_Application
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string apiKey = "ef2002525752e661a403f9fa65ab3745";

        private void btnSeach_Click(object sender, EventArgs e)
        {
            getWeather();
        }
        
        void getWeather()
        {
            using (WebClient web = new WebClient())
            {
                string url = string.Format("https://api.openweathermap.org/data/2.5/weather?q={0}&appid={1}", TBCity.Text, apiKey);
                var json = web.DownloadString(url);
                WeatherInfo.root Info = JsonConvert.DeserializeObject<WeatherInfo.root>(json);

                picIcon.ImageLocation = "https://api.openweathermap.org/img/w/" + Info.weather[0].icon + ".png";
                labCondition.Text = Info.weather[0].main;
                labDetails.Text = Info.weather[0].description;
                labFeelsLike.Text = ((int)(Info.main.feels_like-273)).ToString();
                labSunrise.Text = convertDateTime(Info.sys.sunrise).ToShortTimeString();
                labSunset.Text = convertDateTime(Info.sys.sunset).ToShortTimeString();
                labCity.Text = TBCity.Text;
                labWindSpeed.Text = Info.wind.speed.ToString();
                labPressure.Text = Info.main.pressure.ToString();
                labTemp.Text = ((int)(Info.main.temp-273)).ToString();
            }
        }

        DateTime convertDateTime(long sec)
        {
            DateTime day = new DateTime(1970,1,1,0,0,0,0,System.DateTimeKind.Utc).ToLocalTime();
            day = day.AddSeconds(sec).ToLocalTime();
            return day;
                 
        }

    }
}

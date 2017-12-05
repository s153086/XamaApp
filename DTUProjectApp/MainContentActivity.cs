﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using RESTXama.Models;
using DTUProjectApp.Toolbox;
using Newtonsoft.Json;

namespace DTUProjectApp
{
    [Activity(Label = "MainContentActivity")]
    public class MainContentActivity : Activity
    {

        ListView listView;
        Users[] ListedUsers { get; set; }
       

        public MainContentActivity()
        {
            
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            RequestWindowFeature(Android.Views.WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.MainContent);
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);

            listView = FindViewById<ListView>(Resource.Id.barLister);
            ListedUsers = JsonConvert
                .DeserializeObject<Users[]>(Intent.GetStringExtra("userString"));
            BarAdapter adapter = new BarAdapter(this, ListedUsers.ToList<Users>());

            listView.Adapter = adapter;
            listView.ItemClick += ListView_ItemClick;

            bool amISignedInQuestionMark = Intent.GetBooleanExtra("LegitUser", false);

            if (amISignedInQuestionMark)
            {
                //Only apply toolbar if the user is signed in 
                string userSignedIn = Intent.GetStringExtra("Username");

                SetActionBar(toolbar);

                ActionBar.Title = userSignedIn;
            }

           
        }

        private void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {

            
            StartActivity(typeof(BarContent));
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {

            MenuInflater.Inflate(Resource.Menu.toolbaritems, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {

            switch (item.ItemId)
            {


                case Resource.Id.menu_edit:
                    {

                        var i = new Intent(this, typeof(EditProfileContent));
                        string userSignedIn = Intent.GetStringExtra("Username");

                        i.PutExtra("Username", userSignedIn);

                        StartActivity(i);
                        break;
                    }


                case Resource.Id.menu_LogOut:
                    {
                        StartActivity(typeof(MainActivity));
                        break;
                    }
            }
            return base.OnOptionsItemSelected(item);
        }


    }
}
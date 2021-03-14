using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using PokeAppXamarin.Models;
using PokeAppXamarin.Network;
using Refit;
using System;
using System.Collections.Generic;

namespace PokeAppXamarin
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : Activity
    {
        // UI
        Button buttonSearch;
        EditText editSearch;
        ListView listViewUsers;

        private IListAdapter userListAdapter;
        private IApi api;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_main);

            userListAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1);
            api = RestService.For<IApi>("https://api.github.com");

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Converters = { new StringEnumConverter() }
            };

            buttonSearch = FindViewById<Button>(Resource.Id.btn_search_users);
            editSearch = FindViewById<EditText>(Resource.Id.edit_search);
            listViewUsers = FindViewById<ListView>(Resource.Id.listview_users);
            listViewUsers.Adapter = userListAdapter;

            buttonSearch.Click += ButtonSearch_Click;
        }

        private void ButtonSearch_Click(object sender, EventArgs e)
        {
            getUsers();
        }

        private async void getUsers()
        {
            try
            {
                ApiResponse apiResponse = await api.GetUsers(editSearch.Text);
                var users = apiResponse.items;

                List<string> userNames = new List<string>();
                foreach (var user in users)
                {
                    userNames.Add(string.IsNullOrEmpty(user.login) ? "N/A" : user.login);
                }

                userListAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, userNames);
                listViewUsers.Adapter = userListAdapter;
            }
            catch (Exception e)
            {
                Toast.MakeText(this, e.StackTrace, ToastLength.Long).Show();
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
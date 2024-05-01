using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Service.Autofill;
using Android.Graphics;
using Android.Text;
using TP2.Lib;
using TP2.Lib.MC;
using TP2.Lib.Database;
using Android.Support.Design.Widget;
using Newtonsoft.Json;
using TP2.Lib.Data;

namespace TP2.Activities
{
    [Activity(Label = "AddMovieActivity")]
    public class AddMovieActivity : MovieActivity
    {
        public const string KEY_MC_ADD = "KEY_MC_ADD";

        private Button btnAddMovie;
        private Button btnCancel;
        private EditText txtMoviePictureUrl;
        private EditText txtMovieName;

        private AddMovieMC mc;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            bool isLandscape = Resources.Configuration.Orientation == Android.Content.Res.Orientation.Landscape;

            if (isLandscape)
                SetContentView(Resource.Layout.activity_addmovie_land);
            else
                SetContentView(Resource.Layout.activity_addmovie);

            DataAccessLibrary.Instance.InitializeDatabase(MainActivity.fullDBPath);

            rootView = FindViewById<View>(Resource.Id.rootView);

            // TODO: A COMPLETER
            btnAddMovie = FindViewById<Button>(Resource.Id.btnAddMovie);
            btnCancel = FindViewById<Button>(Resource.Id.btnCancel);
            txtMovieName = FindViewById<EditText>(Resource.Id.txtMovieName);
            txtMoviePictureUrl = FindViewById<EditText>(Resource.Id.txtMoviePictureUrl);
            base.imgView = FindViewById<ImageView>(Resource.Id.imgMoviePicture);

            mc = new AddMovieMC(new MovieRepository());

            btnAddMovie.Click += OnAddMovie;
            btnCancel.Click += OnCancel;
            txtMoviePictureUrl.TextChanged += OnUrlChanged;

            if(savedInstanceState != null)
            {
                mc.Movie = JsonConvert.DeserializeObject<MovieData>(savedInstanceState.GetString(KEY_MC_ADD));
            }
        }

        protected override void OnResume()
        {
            base.OnResume();
            Update();
        }
        protected override void OnStop()
        {
            base.OnStop();
            DataAccessLibrary.Instance.CloseDatabase();
        }

        public void OnAddMovie(object sender, EventArgs arg)
        {
            bool result = mc.AddMovie(txtMovieName.Text, txtMoviePictureUrl.Text);
            if(result)
            {
                Snackbar.Make(rootView, Resource.String.success_add, Snackbar.LengthLong).Show();
            }
            else
            {
                Snackbar.Make(rootView, Resource.String.fail_add, Snackbar.LengthLong).Show();
            }
            Update();
        }

        public void OnCancel(object sender, EventArgs arg)
        {
            OnBackPressed();
        }
        protected override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutString(KEY_MC_ADD, JsonConvert.SerializeObject(mc.Movie));
            base.OnSaveInstanceState(outState);
        }

        private void Update()
        {
            // TODO: A COMPLETER
            // Aller chercher le nom et l'url dans le modèle-contrôleur
            txtMoviePictureUrl.Text = mc.Movie.MoviePictureUrl;
            txtMovieName.Text = mc.Movie.MovieName;

        // Chargement de l'image à partir de l'url à l'aide de la librairie Picasso.
        // Code fourni en raison des techniques utilisées.
        imgView.SetImageResource(Resource.Drawable.placeholder);
            LoadMoviePicture(mc.Movie.MoviePictureUrl,
                                delegate
                                {
                                    mc.IsUrlValid = true;
                                },
                                delegate (Java.Lang.Exception e)
                                {
                                    mc.IsUrlValid = false;
                                    imgView.SetImageResource(Resource.Drawable.placeholder);
                                });
        }

        public void OnUrlChanged(object sender, TextChangedEventArgs a)
        {
            LoadMoviePicture(txtMoviePictureUrl.Text,
                                   delegate
                                   {
                                       mc.IsUrlValid = true;
                                   },
                                   delegate (Java.Lang.Exception e)
                                   {
                                       mc.IsUrlValid = false;
                                       imgView.SetImageResource(Resource.Drawable.placeholder);
                                   });

        }
    }
}
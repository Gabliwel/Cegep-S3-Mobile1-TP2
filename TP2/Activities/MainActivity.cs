using Android.App;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Widget;
using System;
using System.Net;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.Text;
using TP2.Activities;
using TP2.Lib.Database;
using Xamarin.Essentials;
using Orientation = Android.Content.Res.Orientation;
using TP2.Lib.MC;
using TP2.Lib;
using Android.Views;
using Android.Support.Design.Widget;
using Newtonsoft.Json;
using TP2.Lib.Data;

namespace TP2
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : MovieActivity
    {
        public const string DB_PATH = "my_dbMoviesTP2_1.db";
        public static string fullDBPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal) + "\\" + DB_PATH;

        public const string EXTRA_MAIN_ACTIVITY_TO_VOTE_ACTIVITY = "EXTRA_MC_CURRENT_MOVIE";
        public const string KEY_MC = "KEY_MC_LAST_RAND_MOVIE";

        private MainActivityMC mainMC;
        private Button btnAddMovie;
        private Button btnVote;
        private bool fromBundle = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);
      
            bool isLandscape = Resources.Configuration.Orientation == Orientation.Landscape;

            if (isLandscape)
                SetContentView(Resource.Layout.activity_main_land);
            else
                SetContentView(Resource.Layout.activity_main);

            DataAccessLibrary.Instance.InitializeDatabase(fullDBPath);

            rootView = FindViewById<View>(Resource.Id.rootView);


            // TODO: A COMPLETER         
            btnAddMovie = FindViewById<Button>(Resource.Id.btnAddMovie);
            btnVote = FindViewById<Button>(Resource.Id.btnVote);
            base.imgView = FindViewById<ImageView>(Resource.Id.imgPlaceHolder);

            btnAddMovie.Click += OnAddMovie;
            btnVote.Click += OnVote;

            if (savedInstanceState != null)
            {
                mainMC = new MainActivityMC(new MovieRepository());
                mainMC.CurrentMovie = JsonConvert.DeserializeObject<MovieData>(savedInstanceState.GetString(KEY_MC));
                fromBundle = true;
            }
        }

        protected override void OnResume()
        {
            base.OnResume();

            if (DataAccessLibrary.Instance.Connection == null)
            {
                DataAccessLibrary.Instance.InitializeDatabase(fullDBPath);
            }

            // TODO: A COMPLETER
            if(mainMC == null)
            {
                mainMC = new MainActivityMC(new MovieRepository());
                mainMC.UseRandomMovie();
            }
            else
            {
                if (!fromBundle) { mainMC.UseRandomMovie(); } else { fromBundle = false; }
            }

            Update();
        }

        public void OnAddMovie(object sender, EventArgs arg)
        {
            //TODO : Démarrage de l'intent vers le formulaire
            Intent intent = new Intent(this, typeof(AddMovieActivity));
            StartActivity(intent);
        }

        public void OnVote(object sender, EventArgs arg)
        {
            //TODO : Démarrage de l'intent vers le formulaire
            Intent intent = new Intent(this, typeof(VoteActivity));
            intent.PutExtra(EXTRA_MAIN_ACTIVITY_TO_VOTE_ACTIVITY, JsonConvert.SerializeObject(mainMC.CurrentMovie));
            StartActivity(intent);
        }

        protected override void OnPause()
        {
            DataAccessLibrary.Instance.CloseDatabase();
            base.OnPause();
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutString(KEY_MC, JsonConvert.SerializeObject(mainMC.CurrentMovie));
            base.OnSaveInstanceState(outState);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        
        private void Update()
        {
            // TODO: A DECOMMENTER LORSQUE LE MODÈLE-CONTROLEUR SERA ALLOUÉ
            if(mainMC.CurrentMovie != null)
            {
                LoadMoviePicture(mainMC.CurrentMovie.MoviePictureUrl);
                btnAddMovie.Enabled = true;
                btnVote.Enabled = true;
            }
            else
            {
                Snackbar.Make(rootView, Resource.String.error_open_database, Snackbar.LengthLong).Show();
                btnAddMovie.Enabled = false;
                btnVote.Enabled = false;
            }
        } 
    }
}
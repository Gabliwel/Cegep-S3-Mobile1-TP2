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
using Android.Graphics;
using TP2.Lib;
using TP2.Lib.MC;
using TP2.Lib.Database;
using Newtonsoft.Json;
using TP2.Lib.Data;
using Android.Support.Design.Widget;

namespace TP2.Activities
{
    [Activity(Label = "VoteActivity")]
    public class VoteActivity : MovieActivity
    {
        public const string KEY_MC_VOTE = "KEY_MC_VOTE";
        public const string KEY_MC_HAS_BEEN_VOTED = "KEY_MC_HAS_BEEN_VOTED";

        private VoteActivityMC voteMC;
        private TextView lblMovieName;
        private Button btnLike;
        private TextView lblPctLike;
        private Button btnDislike;
        private TextView lblPctDislike;
        private Button btnNextMovie;
        private Button btnBack;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            bool isLandscape = Resources.Configuration.Orientation == Android.Content.Res.Orientation.Landscape;

            if (isLandscape)
                SetContentView(Resource.Layout.activity_vote_land);
            else
                SetContentView(Resource.Layout.activity_vote);

            DataAccessLibrary.Instance.InitializeDatabase(MainActivity.fullDBPath);

            rootView = FindViewById<View>(Resource.Id.rootView);

            // TODO: A COMPLETER
            lblMovieName = FindViewById<TextView>(Resource.Id.lblMovieName);
            base.imgView = FindViewById<ImageView>(Resource.Id.imgMoviePicture);
            btnLike = FindViewById<Button>(Resource.Id.btnLikeMovie);
            lblPctLike = FindViewById<TextView>(Resource.Id.lblPctLike);
            btnDislike = FindViewById<Button>(Resource.Id.btnDislikeMovie);
            lblPctDislike = FindViewById<TextView>(Resource.Id.lblPctDisLike);
            btnNextMovie = FindViewById<Button>(Resource.Id.btnNextMovie);
            btnBack = FindViewById<Button>(Resource.Id.btnBack);

            voteMC = new VoteActivityMC(new MovieRepository());

            btnBack.Click += OnBack;
            btnLike.Click += OnLike;
            btnDislike.Click += OnDislike;
            btnNextMovie.Click += OnNextMovie;

            if(savedInstanceState != null)
            {
                voteMC.CurrentMovie = JsonConvert.DeserializeObject<MovieData>(savedInstanceState.GetString(KEY_MC_VOTE));
                voteMC.HasBeenLikedOrDisliked = savedInstanceState.GetBoolean(KEY_MC_HAS_BEEN_VOTED);
            }
            else
            {
                string myExtra = Intent.GetStringExtra(MainActivity.EXTRA_MAIN_ACTIVITY_TO_VOTE_ACTIVITY);
                voteMC.CurrentMovie = JsonConvert.DeserializeObject<MovieData>(myExtra);
            }

            if(voteMC.HasBeenLikedOrDisliked)
            {
                AfterVoteUpdate();
            }
            else
            {
                Update();
            }
        }

        public void OnBack(object sender, EventArgs arg)
        {
            OnBackPressed();
        }

        public void OnLike(object sender, EventArgs arg)
        {
            bool result = voteMC.LikeVote();
            if(result)
            {
                AfterVoteUpdate();
            }
            else
            {
                Snackbar.Make(rootView, Resource.String.fail_vote, Snackbar.LengthLong).Show();
            }   
        }

        public void OnDislike(object sender, EventArgs arg)
        {
            bool result = voteMC.DislikeVote();
            if(result)
            {
                AfterVoteUpdate();
            }
            else
            {
                Snackbar.Make(rootView, Resource.String.fail_vote, Snackbar.LengthLong).Show();
            }
        }

        public void OnNextMovie(object sender, EventArgs arg)
        {
            voteMC.GetNextRandMovie();
            Update();
        }

        private void AfterVoteUpdate()
        {
            lblPctLike.Visibility = Android.Views.ViewStates.Visible;
            lblPctDislike.Visibility = Android.Views.ViewStates.Visible;
            btnNextMovie.Visibility = Android.Views.ViewStates.Visible;

            btnLike.Enabled = false;
            btnDislike.Enabled = false;

            lblPctLike.Text = voteMC.PctLikes.ToString("0.00") + "%";
            lblPctDislike.Text = voteMC.PctDislikes.ToString("0.00") + "%";
        }

        private void Update()
        {
            // TODO: A COMPLETER    

            lblPctLike.Visibility = Android.Views.ViewStates.Invisible;
            lblPctDislike.Visibility = Android.Views.ViewStates.Invisible;
            btnNextMovie.Visibility = Android.Views.ViewStates.Invisible;

            btnLike.Enabled = true;
            btnDislike.Enabled = true;

            lblMovieName.Text = voteMC.CurrentMovie.MovieName;
            LoadMoviePicture(voteMC.CurrentMovie.MoviePictureUrl);
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutString(KEY_MC_VOTE, JsonConvert.SerializeObject(voteMC.CurrentMovie));
            outState.PutBoolean(KEY_MC_HAS_BEEN_VOTED, voteMC.HasBeenLikedOrDisliked);
            base.OnSaveInstanceState(outState);
        }

        protected override void OnStop()
        {
            base.OnStop();
            DataAccessLibrary.Instance.CloseDatabase();
        }
     }
}
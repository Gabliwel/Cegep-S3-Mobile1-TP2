using Android.App;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Square.Picasso;
using System;
namespace TP2.Activities
{
    [Activity(Label = "MovieActivity")]
    public class MovieActivity : AppCompatActivity
    {
        protected ImageView imgView;
        protected View rootView;
        public MovieActivity()
        {

        }


        protected void LoadMoviePicture(string url, Action onSuccess = null, Action<Java.Lang.Exception> onError = null)
        {
            // Chargement à l'aide de la librairie Picasso.
            if (!string.IsNullOrEmpty(url))
                Picasso.Get().Load(url).Into(imgView, onSuccess, onError);

        }
    }
}
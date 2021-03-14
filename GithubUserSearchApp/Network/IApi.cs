using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PokeAppXamarin.Models;
using Refit;
using System.Threading.Tasks;

namespace PokeAppXamarin.Network
{
    [Headers("User-Agent: :request:")]
    interface IApi
    {
        [Get("/search/users")]
        Task<ApiResponse> GetUsers(
            [Query("q")] string q
        );
    }
}
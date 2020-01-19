using Android.Content;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Text;
using Android.Views.InputMethods;
using BestBeforeApp.Droid;
using BestBeforeApp.Products;
using BestBeforeApp.Shared;
using Plugin.CurrentActivity;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Toolbar = Android.Support.V7.Widget.Toolbar;

//[assembly: ExportRenderer(typeof(ProductsPage), typeof(SearchPageRenderer))]
namespace BestBeforeApp.Droid
{
    public class SearchPageRenderer : PageRenderer
    {
        public SearchPageRenderer(Context context) : base(context)
        {

        }

        protected override void OnAttachedToWindow()
        {
            base.OnAttachedToWindow();

            if (Element is ISearchPage && Element is Page page && page.Parent is NavigationPage navigationPage)
                navigationPage.Popped += HandleNavigationPagePopped;
        }

        protected override void Dispose(bool disposing)
        {
            if (GetToolbar() is Toolbar toolBar)
                toolBar.Menu?.RemoveItem(Resource.Menu.MainMenu);

            base.Dispose(disposing);
        }

        protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
        {
            base.OnSizeChanged(w, h, oldw, oldh);

            if (Element is ISearchPage && Element is Page page)
                AddSearchToToolbar(page.Title);
        }

        private void HandleNavigationPagePopped(object sender, NavigationEventArgs e)
        {
            if (sender is NavigationPage navigationPage
                && navigationPage.CurrentPage is ISearchPage)
            {
                AddSearchToToolbar(navigationPage.CurrentPage.Title);
            }
        }

        private void AddSearchToToolbar(in string pageTitle)
        {
            var getToolbar = GetToolbar();
            if (GetToolbar() is Toolbar toolBar
                && toolBar.Menu?.FindItem(Resource.Id.ActionSearch)?.ActionView?.JavaCast<SearchView>().GetType() != typeof(SearchView))
            {
                toolBar.Title = pageTitle;
                toolBar.InflateMenu(Resource.Menu.MainMenu);

                if (toolBar.Menu?.FindItem(Resource.Id.ActionSearch)?.ActionView?.JavaCast<SearchView>() is SearchView searchView)
                {
                    searchView.QueryTextChange += HandleQueryTextChange;
                    searchView.ImeOptions = (int)ImeAction.Search;
                    searchView.InputType = (int)InputTypes.TextVariationFilter;
                    searchView.MaxWidth = int.MaxValue; //Set to full width - http://stackoverflow.com/questions/31456102/searchview-doesnt-expand-full-width
                }
            }
        }

        private void HandleQueryTextChange(object sender, SearchView.QueryTextChangeEventArgs e)
        {
            if (Element is ISearchPage searchPage)
                searchPage.OnSearchBarTextChanged(e.NewText);
        }

        private Toolbar GetToolbar()
        {
            var toolbar = CrossCurrentActivity.Current.Activity.FindViewById(Resource.Id.toolbar);
            if (toolbar is Toolbar toolBar)
                return toolBar;

            return null;
        }
    }
}

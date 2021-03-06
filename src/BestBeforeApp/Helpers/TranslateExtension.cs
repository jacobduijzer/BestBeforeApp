using System;
using System.Reflection;
using System.Resources;
using BestBeforeApp.Shared;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BestBeforeApp.Helpers
{
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension, ITranslator
    {
        private const string RESOURCEID = "BestBeforeApp.Resources.AppResources";

        private static readonly Lazy<ResourceManager> _resmgr = new Lazy<ResourceManager>(() => new ResourceManager(RESOURCEID, typeof(TranslateExtension).GetTypeInfo().Assembly));

        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider) => DoTranslate();

        public string Translate(string text)
        {
            Text = text;
            return DoTranslate();
        }

        private string DoTranslate()
        {
            if (Text == null)
                return "";

            var translation = _resmgr.Value.GetString(Text, App.AppCulture);

            if (translation == null)
            {
#if DEBUG
                throw new ArgumentException(string.Format("Key '{0}' was not found in resources '{1}' for culture '{2}'.", Text, RESOURCEID, App.AppCulture.Name), "Text");
#else
                translation = Text; // returns the key, which GETS DISPLAYED TO THE USER
#endif
            }
            return translation;
        }
    }
}

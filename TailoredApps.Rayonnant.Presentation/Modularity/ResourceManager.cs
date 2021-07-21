using System;
using System.Windows;
using TailoredApps.Rayonnant.Interface.Modularity;

namespace TailoredApps.Rayonnant.Presentation.Modularity
{
    /// <summary>
    ///     <see cref="IResourceManager" />
    /// </summary>
    public class ResourceManager : IResourceManager
    {
        /// <summary>
        ///     <see cref="IResourceManager.MergeViewModelTemplates" />
        /// </summary>
        public void MergeViewModelTemplates()
        {
            MergeResource("TailoredApps.Rayonnant.Presentation;component/Resources/ViewModelTemplates.xaml");
        }

        /// <summary>
        ///     Method for merging resource to current app resource dict.
        ///     <see cref="IResourceManager.MergeResource" />
        /// </summary>
        /// <param name="relativeUri">uri to res.</param>
        public void MergeResource(string relativeUri)
        {
            var resourceDict = Application.LoadComponent(new Uri(relativeUri, UriKind.Relative)) as ResourceDictionary;
            if (resourceDict != null)
            {
                Application.Current.Resources.MergedDictionaries.Add(resourceDict);
            }
        }
    }
}
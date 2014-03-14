namespace Wise.Framework.Interface.Modularity
{
    /// <summary>
    /// The resource manager used to merging resources with application resources
    /// </summary>
    public interface IResourceManager
    {
        /// <summary>
        /// used to merging view model templates
        /// </summary>
        void MergeViewModelTemplates();
        /// <summary>
        /// Method for merging resources by uri
        /// </summary>
        /// <param name="relativeUri">uri to  resources</param>
        void MergeResource(string relativeUri);
    }
}

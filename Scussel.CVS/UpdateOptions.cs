namespace Scussel.CVS
{
    /// <summary>
    /// Options for update command
    /// </summary>
    public struct UpdateOptions
    {
        /// <summary>
        /// Prune empty directories (-P).
        /// </summary>
        public bool Prune { get; set; }

        /// <summary>
        /// Create any directories that exist in the repository if they're missing from the working directory. 
        /// Normally, update acts only on directories and files that were already enrolled in your working directory. (-d)
        /// </summary>
        public bool CreateDirectories { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Concordion.NET.Internal
{
    /// <summary>
    /// Loads the configuration file for a specification assembly
    /// </summary>
    public class SpecificationConfig
    {
        #region Properties

        /// <summary>
        /// Gets or sets the base input directory.
        /// </summary>
        /// <value>The base input directory.</value>
        public string BaseInputDirectory
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the base output directory.
        /// </summary>
        /// <value>The base output directory.</value>
        public string BaseOutputDirectory
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets names of extensions.
        /// </summary>
        /// <seealso cref="IConcordionExtension"/>
        /// <value>Qualified type names together with assembly names of Concordion extensions.</value>
        public IDictionary<string, string> ConcordionExtensions { get; set; }

        /// <summary>
        /// Gets or sets the suffix to be used for specification files.
        /// </summary>
        /// <value>The file suffix of specification documents (e.g. "html").</value>
        public List<string> SpecificationFileExtensions { get; set; }

        #endregion

        #region Constructors

        public SpecificationConfig()
        {
            this.BaseInputDirectory = null; //a value of null indicates that specifications are embedded in DLL file
            this.BaseOutputDirectory = Environment.GetEnvironmentVariable("TEMP");
            this.SpecificationFileExtensions = new List<string> {"html"};
            this.ConcordionExtensions = new Dictionary<string, string>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Loads the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public SpecificationConfig Load(Type type)
        {
            Load(type.Assembly);
            return this;
        }

        /// <summary>
        /// Loads the specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns></returns>
        public SpecificationConfig Load(Assembly assembly)
        {
            var assemblyCodebase = new Uri(assembly.CodeBase);
            if (assemblyCodebase.IsFile)
            {
                Load(assemblyCodebase.LocalPath);
            }
            return this;
        }

        /// <summary>
        /// Loads the specified path to assembly.
        /// </summary>
        /// <param name="pathToAssembly">The path to assembly.</param>
        /// <returns></returns>
        private SpecificationConfig Load(string pathToAssembly)
        {
            var configFileName = Path.ChangeExtension(pathToAssembly, ".config");
            if (File.Exists(configFileName))
            {
                var specificationConfigParser = new SpecificationConfigParser(this);
                specificationConfigParser.Parse(new StreamReader(configFileName));
            }

            return this;
        }

        #endregion

    }
}

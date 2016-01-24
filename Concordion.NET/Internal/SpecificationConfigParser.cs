using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using Concordion.NET.Internal.Runner;

namespace Concordion.NET.Internal
{
    /// <summary>
    /// 
    /// </summary>
    public class SpecificationConfigParser
    {
        #region Properties
        
        /// <summary>
        /// Gets or sets the config.
        /// </summary>
        /// <value>The config.</value>
        public SpecificationConfig Config
        {
            get;
            private set;
        } 

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SpecificationConfigParser"/> class.
        /// </summary>
        /// <param name="config">The config.</param>
        public SpecificationConfigParser(SpecificationConfig config)
        {
            this.Config = config;
        } 

        #endregion

        /// <summary>
        /// Parses the specified reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public void Parse(TextReader reader)
        {
            var document = XDocument.Load(reader);
            LoadConfiguration(document);
        }

        /// <summary>
        /// Loads the configuration.
        /// </summary>
        /// <param name="document">The document.</param>
        private void LoadConfiguration(XDocument document)
        {
            var configElement = document.Root;

            if (configElement.Name == "Specification")
            {
                LoadBaseInputDirectory(configElement);
                LoadBaseOutputDirectory(configElement);
                LoadConcordionExtensions(configElement);
                LoadSpecificationSuffix(configElement);
                LoadRunners(configElement);
            }
        }

        /// <summary>
        /// Loads the base output directory.
        /// </summary>
        /// <param name="element">The element.</param>
        private void LoadBaseOutputDirectory(XElement element)
        {
            var baseOutputDirectory = element.Element("BaseOutputDirectory");

            if (baseOutputDirectory != null)
            {
                var pathAttribute = baseOutputDirectory.Attribute("path");

                if (pathAttribute != null)
                {
                    Config.BaseOutputDirectory = pathAttribute.Value;
                }
            }
        }

        /// <summary>
        /// Loads the base input directory.
        /// </summary>
        /// <param name="element">The element.</param>
        private void LoadBaseInputDirectory(XElement element)
        {
            var baseInputDirectory = element.Element("BaseInputDirectory");

            if (baseInputDirectory != null)
            {
                var pathAttribute = baseInputDirectory.Attribute("path");

                if (pathAttribute != null)
                {
                    Config.BaseInputDirectory = pathAttribute.Value;
                }
            }
        }

        /// <summary>
        /// Loads the runners.
        /// </summary>
        /// <param name="element">The element.</param>
        private void LoadRunners(XElement element)
        {
            java.lang.System.setProperty("concordion.runner.concordion.net",
                "Concordion.NET.Internal.Runner.DefaultConcordionRunner, Concordion.NET, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
            
            var runners = element.Element("Runners");
            if (runners == null) return;

            foreach (var runner in runners.Elements("Runner"))
            {
                var alias = runner.Attribute("alias");
                var runnerTypeText = runner.Attribute("type");
                if (alias == null || runnerTypeText == null) continue;
                java.lang.System.setProperty("concordion.runner.",
                    runnerTypeText.Value);
            }
        }

        private void LoadConcordionExtensions(XElement element)
        {
            var concordionExtensions = element.Element("ConcordionExtensions");
            if (concordionExtensions == null) return;

            Config.ConcordionExtensions = new Dictionary<string, string>();
            foreach (var extensionDefinition in concordionExtensions.Elements("Extension"))
            {
                var typeName = extensionDefinition.Attribute("type").Value;
                var assemblyName = extensionDefinition.Attribute("assembly").Value;
                Config.ConcordionExtensions.Add(typeName, assemblyName);
            }
        }

        private void LoadSpecificationSuffix(XElement element)
        {
            var defaultSpecificationFileExtensions = new List<string> {"html", "xhtml"};
            Config.SpecificationFileExtensions = defaultSpecificationFileExtensions;
            var specificationExtensions = element.Element("SpecificationFileExtensions");
            if (specificationExtensions == null) return;
            var extensionNames = (from extension in specificationExtensions.Elements("FileExtension").Attributes().ToList() 
                                  where extension != null select extension.Value).ToList();
            Config.SpecificationFileExtensions = extensionNames;
        }
    }
}

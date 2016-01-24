using System;
using System.Collections.Generic;
using System.Linq;
using Concordion.NET.Internal;
using Concordion.NET.Internal.Extension;
using org.concordion.api;
using org.concordion.api.extension;
using org.concordion.@internal;
using SimpleEvaluatorFactory = Concordion.NET.Internal.SimpleEvaluatorFactory;

namespace Concordion.Spec.Support
{
    public class TestRig
    {
        public SpecificationConfig Configuration { get; set; }

        private object Fixture
        {
            get;
            set;
        }

        private EvaluatorFactory EvaluatorFactory
        {
            get;
            set;
        }

        private StubSource Source
        {
            get;
            set;
        }

        private StubTarget Target
        {
            get;
            set;
        }

        private ConcordionExtension Extension { get; set; }

        public TestRig()
        {
            this.EvaluatorFactory = new SimpleEvaluatorFactory();
            this.Source = new StubSource();
            this.Configuration = new SpecificationConfig();
        }

        public TestRig WithFixture(object fixture)
        {
            this.Fixture = fixture;
            return this;
        }

        public ProcessingResult Process(Resource resource)
        {
            var eventRecorder = new EventRecorder();
            this.Target = new StubTarget();

            var concordionBuilder = new ConcordionBuilder()
                .withEvaluatorFactory(this.EvaluatorFactory)
                .withSource(this.Source)
                .withTarget(this.Target)
                .withAssertEqualsListener(eventRecorder)
                .withThrowableListener(eventRecorder);

            if (this.Fixture != null)
            {
                new ExtensionLoader(this.Configuration).AddExtensions(this.Fixture, concordionBuilder);
            }

            if (this.Extension != null)
            {
                this.Extension.addTo(concordionBuilder);
            }

            var concordion = concordionBuilder.build();

            try
            {
                ResultSummary resultSummary = concordion.process(resource, this.Fixture);
                string xml = this.Target.GetWrittenString(resource);
                return new ProcessingResult(resultSummary, eventRecorder, xml);
            }
            catch (Exception e)
            {
                throw new Exception("Test rig failed to process specification", e);
            }
        }

        public ProcessingResult Process(string html)
        {
            var resource = new Resource("/testrig");
            this.WithResource(resource, html);
            return this.Process(resource);
        }

        public TestRig WithResource(Resource resource, string html)
        {
            this.Source.AddResource(resource, html);
            return this;
        }

        public TestRig WithStubbedEvaluationResult(object evaluationResult)
        {
            this.EvaluatorFactory = new StubEvaluator(this.Fixture).WithStubbedResult(evaluationResult);
            return this;
        }

        public ProcessingResult ProcessFragment(string fragment)
        {
            return this.Process(this.WrapFragment(fragment));
        }

        private string WrapFragment(string fragment)
        {
            var wrappedFragment = "<body><fragment>" + fragment + "</fragment></body>";
            return this.WrapWithNamespaceDeclaration(wrappedFragment);
        }

        private string WrapWithNamespaceDeclaration(string fragment)
        {
            return "<html xmlns:concordion='"
                + "http://www.concordion.org/2007/concordion" + "'>"
                + fragment
                + "</html>";
        }

        public bool HasCopiedResource(Resource resource)
        {
            return this.Target.HasCopiedResource(resource);
        }

        public TestRig WithExtension(ConcordionExtension extension)
        {
            this.Extension = extension;
            return this;
        }
    }
}

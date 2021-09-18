using ElasticPMTServer.Models;
using Nest;
using Newtonsoft.Json;
using System.IO;

namespace ElasticPMTServer.Services
{
    public class IndexService : IIndexService
    {
        private readonly ElasticClient _elasticClient;
        private readonly ConnectionSettings _settings;

        public IndexService()
        {
            _settings = new ConnectionSettings()
                             .DefaultMappingFor<CustomControl>(m => m
                               .DisableIdInference()
                               .IndexName("jsonindex")
                               );

            _elasticClient = new ElasticClient(_settings);
        }

        public bool indexExists()
        {
            if (_elasticClient.Indices.Exists("jsonindex").Exists)
            {
                return true;
            }
            return false;
        }

        public IndexResponse populateIndex()
        {
            var json = File.ReadAllText("C:\\Users\\Mario\\Desktop\\DIPLOMSKI\\NIST-LOW-BASELINE-PROFILE.json");
            IndexResponse response = new IndexResponse();
            if (!indexExists())
            {
                var createIndexResponse = createIndex();
                if (!createIndexResponse.IsValid)
                {
                    return new IndexResponse();
                }
                Root root = JsonConvert.DeserializeObject<Root>(json);

                foreach (Group group in root.Catalog.Groups)
                {
                    string groupTitle = group.Title;
                    foreach (Control control in group.Controls)
                    {
                        string controlId = control.Id;
                        string controlClass = control.Class;
                        string controlTitle = control.Title;
                        foreach (Part part in control.Parts)
                        {
                            if (part.Parts != null)
                            {
                                foreach (Part innerPart in part.Parts)
                                {
                                    if (innerPart.Parts != null)
                                    {
                                        foreach (Part doubleInnerPart in innerPart.Parts)
                                        {
                                            if (doubleInnerPart.Parts != null)
                                            {
                                                foreach (Part tripleInnerPart in doubleInnerPart.Parts)
                                                {
                                                    if (tripleInnerPart.Parts != null)
                                                    {
                                                        foreach (Part quadrupleInnerPart in tripleInnerPart.Parts)
                                                        {
                                                            string partId = quadrupleInnerPart.Id;
                                                            string partProse = quadrupleInnerPart.Prose;
                                                            CustomControl newControl = new CustomControl(groupTitle, controlId, controlClass, controlTitle, partId, partProse);
                                                            _elasticClient.Index(newControl, i => i.Index("jsonindex"));
                                                        }
                                                    }
                                                    else
                                                    {
                                                        string partId = tripleInnerPart.Id;
                                                        string partProse = tripleInnerPart.Prose;
                                                        CustomControl newControl = new CustomControl(groupTitle, controlId, controlClass, controlTitle, partId, partProse);
                                                        _elasticClient.Index(newControl, i => i.Index("jsonindex"));
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                string partId = doubleInnerPart.Id;
                                                string partProse = doubleInnerPart.Prose;
                                                CustomControl newControl = new CustomControl(groupTitle, controlId, controlClass, controlTitle, partId, partProse);
                                                _elasticClient.Index(newControl, i => i.Index("jsonindex"));
                                            }
                                        }
                                    }
                                    else
                                    {
                                        string partId = innerPart.Id;
                                        string partProse = innerPart.Prose;
                                        CustomControl newControl = new CustomControl(groupTitle, controlId, controlClass, controlTitle, partId, partProse);
                                        _elasticClient.Index(newControl, i => i.Index("jsonindex"));
                                    }
                                }
                            }
                            else
                            {
                                string partId = part.Id;
                                string partProse = part.Prose;
                                CustomControl newControl = new CustomControl(groupTitle, controlId, controlClass, controlTitle, partId, partProse);
                                response = _elasticClient.Index(newControl, i => i.Index("jsonindex"));
                            }
                        }
                    }
                }
            }

            return response;
        }

        CreateIndexResponse createIndex()
        {
            return _elasticClient.Indices.Create("jsonindex", c => c
                     .Settings(st => st
                         .Setting(UpdatableIndexSettings.MaxNGramDiff, 7)
                         .Analysis(an => an
                             .Analyzers(anz => anz
                                 .Custom("ngram_analyzer", na => na
                                 .Tokenizer("ngram_tokenizer")
                                 .Filters("lowercase"))
                                 )
                             .Tokenizers(tz => tz
                                 .NGram("ngram_tokenizer", td => td
                                     .MinGram(4)
                                     .MaxGram(5)
                                     .TokenChars(
                                         TokenChar.Letter,
                                         TokenChar.Digit,
                                         TokenChar.Symbol
                                     )
                                 )
                             )
                         )
                     )
                         .Map<CustomControl>(m => m.AutoMap())
                 );
        }
    }
}
